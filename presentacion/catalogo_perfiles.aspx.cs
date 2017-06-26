using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio.Componentes;
using System.Data;
using datos.NAVISION;
using Telerik.Web.UI;

namespace presentacion
{
    public partial class catalogo_perfiles : System.Web.UI.Page
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

        private void CargarListadoEmpleados(string filtro)
        {
            try
            {

                Employee entidad = new Employee();
                EmpleadosCOM empleados = new EmpleadosCOM();
                DataTable dt_original = empleados.GetUsers(entidad);
                DataTable dt = new DataTable();
                if (filtro == "")
                {
                    dt = dt_original;
                }
                else
                {
                    if (dt_original.Select("nombre_usuario like '%" + filtro + "%'").Length > 0)
                    {
                        dt = filtro == "" ? dt_original : dt_original.Select("nombre_usuario like '%" + filtro + "%'").CopyToDataTable();
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    rdllista_empleados.DataSource = dt;
                    rdllista_empleados.DataBind();
                    CheckValuesListUsuarios();
                }
                else
                {
                    Toast.Info("No se encontro ninguna coincidencia. Intentelo nuevamente.", "Mensaje del Sistema", this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar lista de empleados. " + ex.Message, this);
            }
        }

        private void AgregarUsuarioPerfiles(string usuario)
        {
            try
            {
                if (ViewState["dt_usuarios"] == null)
                {
                    DataTable dt_new = new DataTable();
                    dt_new.Columns.Add("usuario");
                    ViewState["dt_usuarios"] = dt_new;
                }
                DataTable dt = ViewState["dt_usuarios"] as DataTable;
                DataRow row = dt.NewRow();
                row["usuario"] = usuario;
                dt.Rows.Add(row);
                ViewState["dt_usuarios"] = dt;
            }
            catch (Exception ex)
            {
                Toast.Error("Error al relacionar usuario: " + ex.Message, this);
            }
        }

        private void EliminarUsuarioPerfiles(string usuario)
        {
            try
            {
                if (ViewState["dt_usuarios"] == null)
                {
                    DataTable dt_new = new DataTable();
                    dt_new.Columns.Add("usuario");
                    ViewState["dt_usuarios"] = dt_new;
                }
                DataTable dt = ViewState["dt_usuarios"] as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    if (usuario.Trim().ToUpper() == row["usuario"].ToString().Trim().ToUpper())
                    {
                        row.Delete();
                        break;
                    }
                }
                ViewState["dt_usuarios"] = dt;

            }
            catch (Exception ex)
            {
                Toast.Error("Error al relacionar usuario: " + ex.Message, this);
            }
        }

        private Boolean ExistUsuarioPerfiles(string usuario)
        {
            try
            {
                bool exist = false;
                DataTable dt = ViewState["dt_usuarios"] as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    if (usuario.Trim().ToUpper() == row["usuario"].ToString().Trim().ToUpper())
                    {
                        exist = true;
                        break;
                    }
                }
                return exist;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void CheckValuesListUsuarios()
        {
            try
            {
                foreach (RepeaterItem item in rdllista_empleados.Items)
                {
                    CheckBox check = item.FindControl("mycheck") as CheckBox;
                    check.Checked = ExistUsuarioPerfiles(check.ToolTip.Trim().ToUpper());
                }

            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar lista de usuario: " + ex.Message, this);
            }
        }

        private void CargarCatalogo(int id_perfil)
        {
            try
            {
                PerfilesCOM perfiles = new PerfilesCOM();
                DataSet ds = perfiles.sp_catalogo_perfiles(id_perfil);
                DataTable dt = ds.Tables[0];
                grid_perfiles.DataSource = dt;
                grid_perfiles.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar catalogo principal: " + ex.Message, this);
            }
        }

        private void AgregarPerfil(string perfil, string cadena_usuarios, int total_cadena_usuarios, string cadena_widgets, int total_cadena_widgets)
        {
            div_error.Visible = false;
            try
            {

                PerfilesCOM perfiles = new PerfilesCOM();
                string usuario = Session["usuario"] as string;
                DataSet ds = perfiles.sp_agregar_perfiles(perfil,usuario,cadena_usuarios,total_cadena_usuarios,cadena_widgets,total_cadena_widgets);
                DataTable dt = ds.Tables[0];
                string vmensaje = (dt.Rows.Count == 0 || !dt.Columns.Contains("mensaje")) ? "Error al guardar perfil. Intentelo Nuevamente." : dt.Rows[0]["mensaje"].ToString().Trim();
                if (vmensaje == "")
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                  "AlertGO('Perfil Guardado Correctamente', 'catalogo_perfiles.aspx');", true);
                }
                else {
                    div_error.Visible = true;
                    lblerror.Text =vmensaje;
                }
            }
            catch (Exception ex)
            {
                lblerror.Text="Error al guardar perfil: " + ex.Message;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["dt_usuarios"] = null;
                CargarCatalogo(0);
            }

        }


        protected void lnknuevoperfil_Click(object sender, EventArgs e)
        {
            txtid_perfil.Text = "";
            rtxtperfil.Text = "";
            ModalShow("#myModal");
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            string cadena_perfiles = "";
            int total_cadena_perfiles = 0;
            string cadena_widgets = "";
            int total_cadena_widgets = 0;
            string perfil = rtxtperfil.Text.Trim();
            AgregarPerfil(perfil,cadena_perfiles,total_cadena_perfiles,cadena_widgets,total_cadena_widgets);
        }

        protected void btnbuscarempleado2_Click(object sender, EventArgs e)
        {
            if (txtbuscarempleado.Text.Trim().Length > 2 || txtbuscarempleado.Text.Trim().Length == 0)
            {
                CargarListadoEmpleados(txtbuscarempleado.Text.Trim());
                imgloadempleado_.Style["display"] = "none";
                lblbe2.Style["display"] = "none";
            }
            else
            {
                Toast.Info("Ingrese un minimo de 3 caracteres para realizar la busqueda.", "Mensaje del Sistema", this);
            }
        }


        protected void mycheck_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox item = sender as CheckBox;
            string usuario = item.ToolTip.ToString().Trim().ToUpper();
            EliminarUsuarioPerfiles(usuario);
            if (item.Checked)
            {
                AgregarUsuarioPerfiles(usuario);
            }
        }
    }
}