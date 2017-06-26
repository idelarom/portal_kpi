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

        private String UsuarioTienePerfil(string usuario, int id_perfil)
        {
            try
            {
                Employee entidad = new Employee();
                EmpleadosCOM empleados = new EmpleadosCOM();
                DataTable dt_original = empleados.GetUsers(entidad);

                DataTable dt = new DataTable();
                if (dt_original.Select("usuario_red = '" + usuario.Trim().ToUpper() + "'").Length > 0)
                {
                    dt = dt_original.Select("usuario_red = '" + usuario.Trim().ToUpper() + "'").CopyToDataTable();
                }
                int vid_perfil = Convert.ToInt32(dt.Rows[0]["id_perfil"]);
                string perfil = dt.Rows[0]["perfil"].ToString().Trim();
                perfil = id_perfil == vid_perfil ? "" : perfil;
                return perfil;
            }
            catch (Exception ex)
            {
                return "";
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

        private String CadenaUsuarios()
        {
            try
            {
                string cadena = "";
                DataTable dt = ViewState["dt_usuarios"] as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    cadena = cadena + row["usuario"].ToString().Trim().ToUpper() + ";";
                }
                return cadena;
            }
            catch (Exception ex)
            {
                return "";
            }
        }


        private int TotalCadenaUsuarios()
        {
            try
            {
                DataTable dt = ViewState["dt_usuarios"] as DataTable;
                return dt.Rows.Count;
            }
            catch (Exception ex)
            {
                return 0;
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
                if (id_perfil > 0)
                {
                    DataTable dt_usuarios_original = perfiles.sp_usuarios_perfiles(id_perfil).Tables[0];
                    if (dt_usuarios_original.Rows.Count > 0)
                    {
                        rtxtperfil.Text = dt.Rows[0]["perfil"].ToString();
                        txtid_perfil.Text = id_perfil.ToString();
                        System.Data.DataView view = new System.Data.DataView(dt_usuarios_original);
                        System.Data.DataTable selected = view.ToTable("Selected", false, "usuario");
                        ViewState["dt_usuarios"] = selected;
                        rdllista_empleados.DataSource = dt_usuarios_original;
                        rdllista_empleados.DataBind();
                        CheckValuesListUsuarios();
                    }
                }
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

        private void EditarPerfil(int idperfil,string perfil, string cadena_usuarios, int total_cadena_usuarios, string cadena_widgets, int total_cadena_widgets)
        {
            div_error.Visible = false;
            try
            {

                PerfilesCOM perfiles = new PerfilesCOM();
                string usuario = Session["usuario"] as string;
                DataSet ds = perfiles.sp_editar_perfiles(idperfil,perfil, usuario, cadena_usuarios, total_cadena_usuarios, cadena_widgets, total_cadena_widgets);
                DataTable dt = ds.Tables[0];
                string vmensaje = (dt.Rows.Count == 0 || !dt.Columns.Contains("mensaje")) ? "Error al editar perfil. Intentelo Nuevamente." : dt.Rows[0]["mensaje"].ToString().Trim();
                if (vmensaje == "")
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                  "AlertGO('Perfil Guardado Correctamente', 'catalogo_perfiles.aspx');", true);
                }
                else
                {
                    div_error.Visible = true;
                    lblerror.Text = vmensaje;
                }
            }
            catch (Exception ex)
            {
                lblerror.Text = "Error al editar perfil: " + ex.Message;
            }
        }

        private void EliminarPerfil(int id_perfil,string comenatrios)
        {
            try
            {

                PerfilesCOM perfiles = new PerfilesCOM();
                string usuario = Session["usuario"] as string;
                DataSet ds = perfiles.sp_borrar_perfiles(id_perfil, usuario, comenatrios);
                DataTable dt = ds.Tables[0];
                string vmensaje = (dt.Rows.Count == 0 || !dt.Columns.Contains("mensaje")) ? "Error al eliminar perfil. Intentelo Nuevamente." : dt.Rows[0]["mensaje"].ToString().Trim();
                if (vmensaje == "")
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                  "AlertGO('Perfil Guardado Correctamente', 'catalogo_perfiles.aspx');", true);
                }
                else
                {
                    Toast.Error("Error al eliminar perfil: " + vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al eliminar perfil: " + ex.Message,this);
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
            div_empleados.Visible = true;
            div_perfil.Visible = true;
            txtid_perfil.Text = "";
            rtxtperfil.Text = "";
            txtbuscarempleado.Text = "";
            rdllista_empleados.DataSource = null;
            rdllista_empleados.DataBind();
            ModalShow("#myModal");
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            string cadena_perfiles = CadenaUsuarios(); 
            int total_cadena_perfiles = TotalCadenaUsuarios();
            string cadena_widgets = "";
            int total_cadena_widgets = 0;
            string perfil = rtxtperfil.Text.Trim();
            if (txtid_perfil.Text == "")
            {
                AgregarPerfil(perfil, cadena_perfiles, total_cadena_perfiles, cadena_widgets, total_cadena_widgets);
            }
            else {
                EditarPerfil(Convert.ToInt32(txtid_perfil.Text),perfil, cadena_perfiles, total_cadena_perfiles, cadena_widgets, total_cadena_widgets);
            }
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
            string perfil = UsuarioTienePerfil(usuario, Convert.ToInt32(txtid_perfil.Text==""?"0": txtid_perfil.Text));
            EliminarUsuarioPerfiles(usuario);
            if (item.Checked)
            {
                if (perfil == "")
                {
                    AgregarUsuarioPerfiles(usuario);
                }
                else
                {
                    item.Checked = false;
                    Toast.Error("El usuario " + usuario.ToUpper() + " ya esta relacionado al perfil: " + perfil.ToUpper(), this);
                }
            }
           

        }

        protected void lnkcommand2_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            int id_perfil = Convert.ToInt32(lnk.CommandArgument);
            switch (lnk.CommandName.ToLower())
            {
                case "eliminar":
                    EliminarPerfil(id_perfil, hdfmotivos.Value.Trim());
                    break;
            }
        }

        protected void btneventgrid_Click(object sender, EventArgs e)
        {
            int id_perfil = Convert.ToInt32(hdfid_perfil.Value);
            div_empleados.Visible = false;
            div_perfil.Visible = false;
            switch (hdfcommand.Value.ToLower())
            {
                case "actualizar":
                    div_empleados.Visible = true;
                    div_perfil.Visible = true;
                    CargarCatalogo(id_perfil);
                    ModalShow("#myModal");
                    break;
                case "usuarios":
                    div_empleados.Visible = true;
                    div_perfil.Visible = false;
                    CargarCatalogo(id_perfil);
                    ModalShow("#myModal");
                    break;
            }
        }
    }
}