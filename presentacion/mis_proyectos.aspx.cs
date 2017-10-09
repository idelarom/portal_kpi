using datos;
using datos.Model;
using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class mis_proyectos : System.Web.UI.Page
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
                CargarProyectos();
            }
        }

        private DataTable GetProyectos()
        {
            DataTable dt = new DataTable();
            try
            {
                int num_empleado = Convert.ToInt32(Session["num_empleado"]);
                Boolean ver_Todos_los_empleados = Convert.ToBoolean(Session["ver_Todos_los_empleados"]);
                ProyectosCOM Proyectos = new ProyectosCOM();
                dt = Proyectos.SelectAll(num_empleado, ver_Todos_los_empleados, 1);
            }
            catch (Exception ex)
            {
                dt = new DataTable();
            }
            return dt;
        }     

        private proyectos GetProyecto(int id_proyecto)
        {
            proyectos dt = new proyectos();
            try
            {
                ProyectosCOM Proyecto = new ProyectosCOM();
                dt = Proyecto.proyecto(id_proyecto);
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }

        private void CargarProyectos()
        {
            try
            {
                DataTable dt = GetProyectos();
                repeat_proyectos.DataSource = dt;
                repeat_proyectos.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar proyectos: " + ex.Message, this);
            }
        }

        private void Cargarddlperiodo()
        {
            try
            {
                ProyectosPeriodosCOM periodo = new ProyectosPeriodosCOM();
                DataTable dt_periodos = new DataTable();
                dt_periodos = periodo.SelectAll();
                ddlperiodo.DataValueField = "id_proyecto_periodo";
                ddlperiodo.DataTextField = "nombre";
                ddlperiodo.DataSource = dt_periodos;
                ddlperiodo.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar combo de periodos: " + ex.Message, this);
            }
        }

        private void Cargarddlestatus()
        {
            try
            {
                ProyectosEstatusCOM estatus = new ProyectosEstatusCOM();
                DataTable dt_estatus = new DataTable();
                dt_estatus = estatus.SelectAll();
                ddlestatus.DataValueField = "id_proyecto_estatus";
                ddlestatus.DataTextField = "estatus";
                ddlestatus.DataSource = dt_estatus;
                ddlestatus.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar combo de estatus : " + ex.Message, this);
            }
        }

        private void Cargarddltegnologia()
        {
            try
            {
                ProyectosTecnologiasCOM tegnologia = new ProyectosTecnologiasCOM();
                DataTable dt_tegnologia = new DataTable();
                dt_tegnologia = tegnologia.SelectAll();
                ddltegnologia.DataValueField = "id_proyecto_tecnologia";
                ddltegnologia.DataTextField = "nombre";
                ddltegnologia.DataSource = dt_tegnologia;
                ddltegnologia.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar combo de tegnologias : " + ex.Message, this);
            }
        }

        private string Agregar(proyectos id_proyecto)
        {
            ProyectosCOM Proyecto = new ProyectosCOM();
            string vmensaje = Proyecto.Agregar(id_proyecto);

            return vmensaje;
        }
        private string Editar(proyectos id_proyecto)
        {
            ProyectosCOM Proyecto = new ProyectosCOM();
            string vmensaje = Proyecto.Editar(id_proyecto);

            return vmensaje;
        }

        private string Eliminar(proyectos id_proyecto)
        {
            ProyectosCOM Proyecto = new ProyectosCOM();
            string vmensaje = Proyecto.Elimina(id_proyecto);

            return vmensaje;
        }

        protected void CargarDatosempleados(string filtro)
        {
            try
            {

                int NumJefe = Convert.ToInt32(Session["NumJefe"]);
                int num_empleado = Convert.ToInt32(Session["num_empleado"]);
                Boolean ver_Todos_los_empleados = Convert.ToBoolean(Session["ver_Todos_los_empleados"]);
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
                //rtvListEmpleado.DataTextField = "Text";
                //rtvListEmpleado.DataValueField = "Value";
                //rtvListEmpleado.DataFieldID = "ID";
                //rtvListEmpleado.DataFieldParentID = "ParentID";
                //rtvListEmpleado.DataSource = siteData;
                //rtvListEmpleado.DataBind();
                //lblcountlistempleados.Text = siteData.Count.ToString();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar listado de empleados: " + ex.Message, this);
            }
        }

        protected void lnknuevoproyecto_Click(object sender, EventArgs e)
        {
            txtnombreproyecto.Text = "";
            txtdescripcion.Text = "";
            ddlperiodo.SelectedIndex = 0;
            ddlestatus.SelectedIndex = 0;
            txtcveop.Text = "";
            txtfolopmt.Text = "";
            Cargarddlperiodo();
            Cargarddlestatus();
            Cargarddltegnologia();
            CargarDatosempleados("");
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
            ModalShow("#ModalCapturaProyectos");
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            try
            {
                string vmensaje = string.Empty;
                int id_proyecto = Convert.ToInt32(hdfid_proyecto.Value == "" ? "0" : hdfid_proyecto.Value);
                proyectos proyecto = new proyectos();

                proyecto.proyecto = txtnombreproyecto.Text;
                proyecto.descripcion = txtdescripcion.Text;
                proyecto.id_proyecto_periodo = Convert.ToInt32(ddlperiodo.SelectedValue);
                proyecto.id_proyecto_estatus = Convert.ToInt32(ddlestatus.SelectedValue);
                proyecto.cveoport = Convert.ToInt32(txtcveop.Text);
                proyecto.folio_pmt = txtfolopmt.Text;
                proyecto.id_proyecto_tecnologia = Convert.ToInt32(ddltegnologia.SelectedValue);

                if (id_proyecto > 0) { proyecto.id_proyecto = id_proyecto; }
                EmpleadosCOM empleados = new EmpleadosCOM();
                int num_empleado = Convert.ToInt32(ddlempleado_a_consultar.SelectedValue);
                DataSet ds = empleados.sp_listado_empleados(num_empleado, false, false);
                DataTable dt = ds.Tables[0];
                string Usuario = dt.Rows[0]["usuario"].ToString();
                proyecto.usuario = Usuario;//Session["usuario"] as string;
                if (proyecto.proyecto == "")
                {
                    ModalShow("#ModalCapturaProyectos");
                    Toast.Error("Error al procesar proyecto : Ingrese un proyecto", this);
                }
                else if (proyecto.descripcion == "")
                {
                    ModalShow("#ModalCapturaProyectos");
                    Toast.Error("Error al procesar estatus : Ingresela descripcion del proyecto", this);
                }
                else if (proyecto.id_proyecto_periodo <=  0)
                {
                    ModalShow("#ModalCapturaProyectos");
                    Toast.Error("Error al procesar periodo : Seleccione un periodo", this);
                }
                else if (proyecto.id_proyecto_estatus <=0)
                {
                    ModalShow("#ModalCapturaProyectos");
                    Toast.Error("Error al procesar estatus : Seleccione un estatus", this);
                }
                else if (proyecto.cveoport <=0)
                {
                    ModalShow("#ModalCapturaProyectos");
                    Toast.Error("Error al procesar folio pmtracker : Ingrese un folio pmtracker", this);
                }
                else if (proyecto.folio_pmt == "")
                {
                    ModalShow("#ModalCapturaProyectos");
                    Toast.Error("Error al procesar folio pmtracker : Ingrese un folio pmtracker", this);
                }
                else if (proyecto.id_proyecto_tecnologia <= 0)
                {
                    ModalShow("#ModalCapturaProyectos");
                    Toast.Error("Error al procesar tecnologia : Seleccione una tecnologia ", this);
                }
                else
                {
                    proyecto.usuario_edicion = Session["usuario"] as string;
                    vmensaje = id_proyecto > 0 ? Editar(proyecto) : Agregar(proyecto);
                    if (vmensaje == "")
                    {
                        txtnombreproyecto.Text = "";
                        txtdescripcion.Text = "";
                        ddlperiodo.SelectedIndex = 0;
                        ddlestatus.SelectedIndex = 0;
                        txtcveop.Text = "";
                        txtfolopmt.Text = "";
                        Cargarddlperiodo();
                        Cargarddlestatus();
                        Cargarddltegnologia();
                        hdfid_proyecto.Value = "";
                        CargarProyectos();
                        Toast.Success("proyecto agregado correctamente.", "Mensaje del sistema", this);
                    }
                    else
                    {
                        ModalShow("#ModalProyectoestatus");
                        Toast.Error("Error al procesar proyecto : " + vmensaje, this);
                    }
                }
            }
            catch (Exception ex)
            {
                ModalShow("#ModalProyectoestatus");
                Toast.Error("Error al procesar estatus : " + ex.Message, this);
            }
        }

        protected void btneventgrid_Click(object sender, EventArgs e)
        {
            try
            {            
                int id_proyecto = Convert.ToInt32(hdfid_proyecto.Value == "" ? "0" : hdfid_proyecto.Value);
                if (id_proyecto > 0)
                {
                    proyectos proyecto = GetProyecto(id_proyecto);
                    if (proyecto != null)
                    {
                        Cargarddlperiodo();
                        Cargarddlestatus();
                        Cargarddltegnologia();

                        txtnombreproyecto.Text = proyecto.proyecto;
                        txtdescripcion.Text = proyecto.descripcion;
                        ddlperiodo.SelectedValue= proyecto.id_proyecto_periodo.ToString();
                        ddlestatus.SelectedValue = proyecto.id_proyecto_estatus.ToString();
                        txtcveop.Text = proyecto.cveoport.ToString();
                        txtfolopmt.Text = proyecto.folio_pmt;
                        ddltegnologia.SelectedValue = proyecto.id_proyecto_tecnologia.ToString();
                        CargarDatosempleados(proyecto.usuario);

                        ModalShow("#ModalCapturaProyectos");
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar proyecto : " + ex.Message, this);
            }
        }

        protected void btneliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int id_proyecto = Convert.ToInt32(hdfid_proyecto.Value == "" ? "0" : hdfid_proyecto.Value);
                proyectos proyecto = new proyectos();
                proyecto.id_proyecto = id_proyecto;
                proyecto.usuario_borrado = Session["usuario"] as string;
                string vmensaje = Eliminar(proyecto);
                if (vmensaje == "")
                {
                    CargarProyectos();
                    Toast.Success("Estatus eliminado correctamente.", "Mensaje del sistema", this);
                }
                else
                {
                    Toast.Error("Error al eliminar proyecto: " + vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al eliminar proyecto: " + ex.Message, this);
            }
        }

        protected void btnopendashboard_Click(object sender, EventArgs e)
        {
            string id_proyecto = funciones.deTextoa64(hdfid_proyecto.Value);
            Response.Redirect("proyectos_dashboard.aspx?id_proyecto=" + id_proyecto);

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