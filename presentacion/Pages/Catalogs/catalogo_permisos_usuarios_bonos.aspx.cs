using datos;
using datos.Model;
using negocio.Componentes;
using negocio.Componentes.Compensaciones;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using Telerik.Web.UI;


namespace presentacion.Pages.Catalogs
{
    public partial class catalogo_permisos_usuarios_bonos : System.Web.UI.Page
    {
        private void ModalShow(string modalname)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                             "ModalShow('" + modalname + "');", true);
        }

        private void ModalClose(string modalname)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                             "ModalCloseGlobal('" + modalname + "');", true);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCatalogo();
            }
        }

        private DataTable GetAllpermisos()
        {
            DataTable dt = new DataTable();
            try
            {
                PermisosUsuariosCOM getall = new PermisosUsuariosCOM();
                dt = getall.SelectAll();
            }
            catch (Exception ex)
            {
                dt = new DataTable();
            }
            return dt;
        }

        private permissions_users_bonds_types Getpermisos(string login)
        {
            permissions_users_bonds_types dt = new permissions_users_bonds_types();
            try
            {
                PermisosUsuariosCOM getpu = new PermisosUsuariosCOM();
                dt = getpu.Permisos(login);
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }

        private void CargarCatalogo()
        {
            try
            {
                DataTable dt = GetAllpermisos();
                repeat_permissions_users_bonds_types.DataSource = dt;
                repeat_permissions_users_bonds_types.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar catalogo: " + ex.Message, this);
            }
        }

        private string Agregar(permissions_users_bonds_types login)
        {
            PermisosUsuariosCOM bono = new PermisosUsuariosCOM();
            string vmensaje = bono.Agregar(login);

            return vmensaje;
        }
        private string Editar(permissions_users_bonds_types login)
        {
            PermisosUsuariosCOM bono = new PermisosUsuariosCOM();
            string vmensaje = bono.Editar(login);

            return vmensaje;
        }

        private string Eliminar(string login)
        {
            PermisosUsuariosCOM bono = new PermisosUsuariosCOM();
            string vmensaje = bono.Eliminar(login);

            return vmensaje;
        }

        protected void CargarDatosempleados(string filtro)
        {
            try
            {

                int NumJefe = Convert.ToInt32(Session["NumJefe"]);
                int num_empleado = Convert.ToInt32(Session["num_empleado"]);
                Boolean ver_Todos_los_empleados = Convert.ToBoolean(Session["ver_Todos_los_empleados"]);
                if (funciones.Permisos(Session["usuario"] as string, 4))
                {
                    ver_Todos_los_empleados = true;
                }
                EmpleadosCOM empleados = new EmpleadosCOM();
                bool no_activos = false;
                DataSet ds = empleados.sp_listado_empleados(num_empleado, ver_Todos_los_empleados, no_activos);
                DataTable dt_empleados = new DataTable();
                if (filtro != "")
                {
                    DataView dv_empleados = ds.Tables[0].DefaultView;
                    dv_empleados.RowFilter = "nombre like '%" + filtro + "%'";
                    if (dv_empleados.ToTable().Rows.Count <= 0)
                    {
                        dv_empleados.RowFilter = "usuario like '%" + filtro + "%'";
                    }
                    dt_empleados = dv_empleados.ToTable();
                    if (dt_empleados.Rows.Count == 1)
                    {
                        int num_jefe = Convert.ToInt32(dt_empleados.Rows[0]["num_empleado"].ToString());
                        CargarListadoEmpleado(num_jefe, false);
                    }
                }
                else
                {
                    dt_empleados = ds.Tables[0];
                }
                ddlempleado_a_consultar.DataValueField = "num_empleado";
                ddlempleado_a_consultar.DataTextField = "nombre";
                ddlempleado_a_consultar.DataSource = dt_empleados;
                ddlempleado_a_consultar.DataBind();
                if (!ver_Todos_los_empleados)
                {
                    CargarListadoEmpleado(num_empleado, false);
                    ddlempleado_a_consultar.SelectedValue = num_empleado.ToString();
                    //lnkagregartodos_Click(null, null);
                }

                ddlempleado_a_consultar.Enabled = ver_Todos_los_empleados;
                div_filtro_empleados.Visible = ver_Todos_los_empleados;
            }
            catch (Exception ex)
            {
                Toast.Error("Error al iniciar busqueda de empleados: " + ex.Message, this);
            }
            finally
            {

            }
        }

        protected void CargarListadoEmpleado(int num_jefe, Boolean ver_Todos_los_empleados)
        {
            try
            {
                EmpleadosCOM empleados = new EmpleadosCOM();
                bool no_activos = false;
                DataSet ds = empleados.sp_listado_empleados(num_jefe, ver_Todos_los_empleados, no_activos);
                DataTable dt = ds.Tables[0];
                List<SiteDataItem> siteData = new List<SiteDataItem>();
                foreach (DataRow row in dt.Rows)
                {
                    siteData.Add(new SiteDataItem(
                        Convert.ToInt32(row["num_empleado"]),
                        Convert.ToInt32(row["numjefe"]),
                        row["nombre"].ToString(),
                        row["usuario"].ToString()
                        ));
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar listado de empleados: " + ex.Message, this);
            }
        }

        private void CargarddlBonos()
        {
            try
            {
                TipoBonosCOM bonds_type = new TipoBonosCOM();
                DataTable dt_bonds_type = new DataTable();
                dt_bonds_type = bonds_type.SelectAll();
                ddlBonosSolicitud.DataValueField = "id_bond_type";
                ddlBonosSolicitud.DataTextField = "name";
                ddlBonosSolicitud.DataSource = dt_bonds_type;
                ddlBonosSolicitud.DataBind();

                ddlBonosAutorizacion.DataValueField = "id_bond_type";
                ddlBonosAutorizacion.DataTextField = "name";
                ddlBonosAutorizacion.DataSource = dt_bonds_type;
                ddlBonosAutorizacion.DataBind();

            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar combo de estatus : " + ex.Message, this);
            }
        }

        protected void lnknuevoPermiso_Click(object sender, EventArgs e)
        {
            CargarDatosempleados("");
            CargarddlBonos();
            if (Request.QueryString["filter"] != null)
            {
                //lnkfiltros_Click(null, null);
                string num_empleado = Convert.ToString(Session["num_empleado"]);
                ListItem itwm = ddlempleado_a_consultar.Items.FindByValue(num_empleado);
                if (Items != null)
                {
                    ddlempleado_a_consultar.SelectedValue = num_empleado;
                    //ddlempleado_a_consultar_SelectedIndexChanged(null, null);
                    //lnkagregartodos_Click(null, null);
                }
            }
            hdflogin.Value = "";
            chkCC.Checked = false;
            ModalShow("#ModalPermisosBonos");
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            try
            {
                string vmensaje = string.Empty;
                string login = hdflogin.Value == "" ? "0" : hdflogin.Value;
                permissions_users_bonds_types permiso = new permissions_users_bonds_types();

                if (login != "0") { permiso.login = login; }

                string permission_request_bond = "";
                string permision_authorization_bond = "";

                //List<proyectos_historial_tecnologias> tecnologias = new List<proyectos_historial_tecnologias>();
                //string strtecnologias = "";
                IList<RadComboBoxItem> list_BonosSolicitud = ddlBonosSolicitud.CheckedItems;
                foreach (RadComboBoxItem item in list_BonosSolicitud)
                {
                    if (item.Checked)
                    {
                        permission_request_bond = permission_request_bond + item.Value + ",";
                    }
                }
                permission_request_bond = permission_request_bond.Substring(0, permission_request_bond.Length > 0 ? permission_request_bond.Length - 1 : 0);

                IList<RadComboBoxItem> list_BonosAutorizacion = ddlBonosAutorizacion.CheckedItems;
                foreach (RadComboBoxItem item in list_BonosAutorizacion)
                {
                    if (item.Checked)
                    {
                        permision_authorization_bond = permision_authorization_bond + item.Value + ",";
                    }
                }
                permision_authorization_bond = permision_authorization_bond.Substring(0, permision_authorization_bond.Length > 0 ? permision_authorization_bond.Length - 1 : 0);

                permiso.login =ddlempleado_a_consultar.SelectedValue;
                permiso.permission_request_bond = permission_request_bond;
                permiso.permision_authorization_bond = permision_authorization_bond;
                if (permiso.login == "")
                {
                    ModalShow("#ModalPermisosBonos");
                    Toast.Error("Error al procesar permiso : seleccione un empleado", this);
                }
                else if (permiso.permission_request_bond == "" && permiso.permision_authorization_bond == "")
                {
                    ModalShow("#ModalPermisosBonos");
                    Toast.Error("Error al procesar permiso : seleccione algun tipo de bono ya sea en solicitud o autorizacion", this);
                }
                else
                {
                    vmensaje = permiso.login != "" ? Editar(permiso) : Agregar(permiso);
                    if (vmensaje == "")
                    {
                        CargarddlBonos();
                        chkCC.Checked = false;
                        hdflogin.Value = "";
                        CargarCatalogo();
                        Toast.Success("Estatus pago agregado correctamente.", "Mensaje del sistema", this);
                    }
                    else
                    {
                        ModalShow("#ModalPermisosBonos");
                        Toast.Error("Error al procesar Estatus : " + vmensaje, this);
                    }
                }
            }
            catch (Exception ex)
            {
                ModalShow("#ModalPermisosBonos");
                Toast.Error("Error al procesar Estatus : " + ex.Message, this);
            }
        }

        protected void btneventgrid_Click(object sender, EventArgs e)
        {
            try
            {               
                string login = hdflogin.Value == "" ? "0" : hdflogin.Value;
                if (login != "0")
                {
                    CargarddlBonos();
                    CargarDatosempleados(login);
                    permissions_users_bonds_types Permiso = Getpermisos(login);
                    if (Permiso != null)
                    {
                        string[] BonosSolicitud = Permiso.permission_request_bond.Split(',');

                        foreach (RadComboBoxItem item in ddlBonosSolicitud.Items)
                        {
                            foreach (string bs in BonosSolicitud)
                            {
                                if (item.Value == bs.ToString())
                                {
                                    item.Checked = true;
                                }
                            }
                        }
                        string[] BonosAutorizacion = Permiso.permision_authorization_bond.Split(',');
                        foreach (RadComboBoxItem item in ddlBonosAutorizacion.Items)
                        {
                            foreach (string ba in BonosAutorizacion)
                            {
                                if (item.Value == ba.ToString())
                                {
                                    item.Checked = true;
                                }
                            }
                        }
                        chkCC.Checked = Convert.ToBoolean(Permiso.FiltroCC);
                        ModalShow("#ModalPermisosBonos");
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al procesar Estatus : " + ex.Message, this);
            }
        }

        protected void btneliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string login = hdflogin.Value == "" ? "0" : hdflogin.Value);
                permissions_users_bonds_types permiso = new permissions_users_bonds_types();
                permiso.login = login;
                string vmensaje = Eliminar(login);
                if (vmensaje == "")
                {
                    CargarCatalogo();
                    Toast.Success("Usuario eliminado correctamente.", "Mensaje del sistema", this);
                }
                else
                {
                    Toast.Error("Error al eliminar usuario : " + vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al eliminar usuario : " + ex.Message, this);
            }
        }

        protected void lnksearch_Click(object sender, EventArgs e)
        {
            string filter = txtfilterempleado.Text;
            try
            {
                if (filter.Length == 0 || filter.Length > 3)
                {
                    CargarDatosempleados(filter);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al filtrar empleados: " + ex.Message, this);
            }
            finally
            {
                imgloadempleado.Style["display"] = "none";
                lblbemp.Style["display"] = "none";
            }
        }
    }
}