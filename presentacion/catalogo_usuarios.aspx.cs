using datos.Model;
using datos.NAVISION;
using negocio.Componentes;
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
    public partial class catalogo_usuarios : System.Web.UI.Page
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
                ViewState["dt_usuarios"] = null;
                CargarCatalogoUsuarios();
            }
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

        private void CheckValuesListUsuarios()
        {
            try
            {
                foreach (RepeaterItem item in rdllista_empleados.Items)
                {
                    CheckBox check = item.FindControl("mycheck") as CheckBox;
                    check.Checked = ExistUsuarioDelegados(check.ToolTip.Trim().ToUpper());
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar lista de usuario: " + ex.Message, this);
            }
        }

        private Boolean ExistUsuarioDelegados(string usuario)
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

        private void CargarCatalogoUsuarios()
        {
            try
            {
                int NumJefe = Convert.ToInt32(Session["NumJefe"]);
                int num_empleado = Convert.ToInt32(Session["num_empleado"]);
                Boolean ver_Todos_los_empleados = true;// Convert.ToBoolean(Session["ver_Todos_los_empleados"]);
                EmpleadosCOM empleados = new EmpleadosCOM();
                bool no_activos = false;
                DataSet ds = empleados.sp_listado_empleados(num_empleado, ver_Todos_los_empleados, no_activos);
                ViewState["dt_empleados"] = null;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    repeat_usuarios.DataSource = ds.Tables[0];
                    repeat_usuarios.DataBind();
                    ViewState["dt_empleados"] = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar usuarios: " + ex.Message, this);
            }
        }

        protected void LlenarInformacionModal()
        {
            ViewState["dt_usuarios"] = null;
            string usuario = hdfusuario.Value;
            int NumJefe = Convert.ToInt32(Session["NumJefe"]);
            int num_empleado = Convert.ToInt32(Session["num_empleado"]);
            Boolean ver_Todos_los_empleados = true;// Convert.ToBoolean(Session["ver_Todos_los_empleados"]);
            EmpleadosCOM empleados = new EmpleadosCOM();
            bool no_activos = false;
            DataSet ds = empleados.sp_listado_empleados(num_empleado, ver_Todos_los_empleados, no_activos);
            DataTable dt = ds.Tables[0];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "usuario = '" + usuario.Trim().ToUpper() + "'";
            if (dv.ToTable().Rows.Count > 0)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/img/users/"));
                string imagen = usuario + ".png";
                if (imagen != "" && File.Exists(dirInfo.ToString().Trim() + imagen))
                {
                    DateTime localDate = DateTime.Now;
                    string date = localDate.ToString();
                    date = date.Replace("/", "_");
                    date = date.Replace(":", "_");
                    date = date.Replace(" ", "");
                    img_employee.ImageUrl = "~/img/users/" + imagen + "?date=" + date;
                }
                else
                {
                    imagen = "user.png";
                    DateTime localDate = DateTime.Now;
                    string date = localDate.ToString();
                    date = date.Replace("/", "_");
                    date = date.Replace(":", "_");
                    date = date.Replace(" ", "");
                    img_employee.ImageUrl = "~/img/" + imagen + "?date=" + date;
                }
                DataRow empleado = dv.ToTable().Rows[0];
                lblnombre.Text = empleado["nombre"].ToString();
                lblpuesto.Text = empleado["puesto"].ToString();
                lblusuario.Text = empleado["usuario"].ToString();
                lblperfil.Text = empleado["perfil"].ToString();

                //cargos los menus disponibles para el usuario
                CargarMenus(usuario);

                CargarPermisos(usuario);

                CargarDelegados(usuario);
                //cargamos los perfiles
                CargarListadoPerfiles("");

                //cargamos los permisos
                CargarListadoPermisos("");
            }
        }
        protected void btndelegados_Click(object sender, EventArgs e)
        {
            try
            {
                div_addperfil.Visible = false;
                div_menus.Visible = false;
                div_permiso.Visible = false;
                LlenarInformacionModal();
                ModalShow("#modal_delegados");
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar usuario: " + ex.Message, this);
            }
        }
        protected void btnver_Click(object sender, EventArgs e)
        {
            try
            {
                div_addperfil.Visible = false;
                div_menus.Visible = false;
                div_permiso.Visible = false;
                LlenarInformacionModal();
                ModalShow("#ModalEmpleado");                
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar usuario: " + ex.Message, this);
            }
        }

        /// <summary>
        /// Carga los menus disponibles para el usuario
        /// </summary>
        /// <param name="usuario"></param>
        protected void CargarMenus(string usuario)
        {
            try
            {

                MenusCOM menus = new MenusCOM();
                DataTable dt = menus.sp_menus_usuarios(usuario).Tables[0];
                repeater_menu.DataSource = dt;
                repeater_menu.DataBind();
                CargarListadoMenus("");
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar usuario: " + ex.Message, this);
            }
        }

        protected void CargarPermisos(string usuario)
        {
            try
            {
                UsuariosCOM usuarios = new UsuariosCOM();
                DataTable dt = usuarios.GetUsuariosPermisos(usuario);
                repeater_permisos.DataSource = dt;
                repeater_permisos.DataBind();
                CargarListadoPermisos("");
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar usuario: " + ex.Message, this);
            }
        }

        protected void CargarDelegados(string usuario)
        {
            try
            {
                EmpleadosCOM usuarios = new EmpleadosCOM();
                DataTable dt = usuarios.GetDelegados(usuario);
                if (dt.Rows.Count > 0)
                {
                    System.Data.DataView view = new System.Data.DataView(dt);
                    System.Data.DataTable selected = view.ToTable("Selected", false, "usuario_Red");
                    selected.Columns["usuario_red"].ColumnName = "usuario";
                    ViewState["dt_usuarios"] = selected;
                    rdllista_empleados.DataSource = dt;
                    rdllista_empleados.DataBind();
                    CheckValuesListUsuarios();
                }
                else
                {
                    rdllista_empleados.DataSource = null;
                    rdllista_empleados.DataBind();

                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar usuario: " + ex.Message, this);
            }
        }

        protected void lnkaddperfil_Click(object sender, EventArgs e)
        {
            div_addperfil.Visible = true;
            div_menus.Visible = false;
            div_permiso.Visible = false;
        }

        protected void lnkaddmenus_Click(object sender, EventArgs e)
        {
            div_menus.Visible = true;
            div_addperfil.Visible = false;
            div_permiso.Visible = false;
        }

        protected void lnkbuscarmenu_Click(object sender, EventArgs e)
        {
            if (txtbuscarmenu.Text.Trim().Length > 2 || txtbuscarmenu.Text.Trim().Length == 0)
            {
                CargarListadoMenus(txtbuscarmenu.Text.Trim());
                imgmenu.Style["display"] = "none";
                lblmenu.Style["display"] = "none";
            }
            else
            {
                Toast.Info("Ingrese un minimo de 3 caracteres para realizar la busqueda.", "Mensaje del Sistema", this);
            }
        }

        protected void lnkbuscarperfil_Click(object sender, EventArgs e)
        {
            if (txtbuscarperfil.Text.Trim().Length > 2 || txtbuscarperfil.Text.Trim().Length == 0)
            {
                CargarListadoPerfiles(txtbuscarmenu.Text.Trim());
                imgperfil.Style["display"] = "none";
                lblperfilb.Style["display"] = "none";
            }
            else
            {
                Toast.Info("Ingrese un minimo de 3 caracteres para realizar la busqueda.", "Mensaje del Sistema", this);
            }
        }
               
        private void CargarListadoPerfiles(string filtro)
        {
            try
            {
                PerfilesCOM perfiles = new PerfilesCOM();
                DataSet ds = perfiles.sp_catalogo_perfiles(0);
                DataTable dt_original = ds.Tables[0];

                DataTable dt = new DataTable();
                if (filtro == "")
                {
                    dt = dt_original;
                    if (dt.Rows.Count > 0)
                    {
                        ddlperfiles.DataTextField = "perfil";
                        ddlperfiles.DataValueField = "id_perfil";
                        ddlperfiles.DataSource = dt;
                        ddlperfiles.DataBind();
                    }
                }
                else
                {
                    if (dt_original.Select("perfil like '%" + filtro + "%'").Length > 0)
                    {
                        dt = filtro == "" ? dt_original : dt_original.Select("perfil like '%" + filtro + "%'").CopyToDataTable();
                    }

                    if (dt.Rows.Count > 0)
                    {
                        ddlperfiles.DataTextField = "perfil";
                        ddlperfiles.DataValueField = "id_perfil";
                        ddlperfiles.DataSource = dt;
                        ddlperfiles.DataBind();
                    }
                    else
                    {
                        Toast.Info("No se encontro ninguna coincidencia. Intentelo nuevamente.", "Mensaje del Sistema", this);
                    }
                }

            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar lista de menus. " + ex.Message, this);
            }
        }

        private void CargarListadoMenus(string filtro)
        {
            try
            {
                MenusCOM widgets = new MenusCOM();
                DataTable dt_original = widgets.sp_catalogo_menus(0).Tables[0];

                DataTable dt = new DataTable();
                if (filtro == "")
                {
                    dt = dt_original;
                    if (dt.Rows.Count > 0)
                    {
                        DataView dv_f = dt.DefaultView;
                        dv_f.RowFilter = "id_menu_padre <> 0";
                        ddlmenus.DataTextField = "name";
                        ddlmenus.DataValueField = "id_menu";
                        ddlmenus.DataSource = dv_f.ToTable();
                        ddlmenus.DataBind();
                    }
                }
                else
                {
                    if (dt_original.Select("name like '%" + filtro + "%'").Length > 0)
                    {
                        dt = filtro == "" ? dt_original : dt_original.Select("name like '%" + filtro + "%'").CopyToDataTable();
                    }
                    if (dt.Rows.Count > 0)
                    {
                        DataView dv_f = dt.DefaultView;
                        dv_f.RowFilter = "id_menu_padre <> 0";
                        ddlmenus.DataTextField = "name";
                        ddlmenus.DataValueField = "id_menu";
                        ddlmenus.DataSource = dv_f.ToTable();
                        ddlmenus.DataBind();
                    }
                    else
                    {
                        Toast.Info("No se encontro ninguna coincidencia. Intentelo nuevamente.", "Mensaje del Sistema", this);
                    }
                }

                
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar lista de menus. " + ex.Message, this);
            }
        }

        private void CargarListadoPermisos(string filtro)
        {
            try
            {
                PermisosCOM permisos = new PermisosCOM();
                DataTable dt_original = permisos.SelectAll();

                DataTable dt = new DataTable();
                if (filtro == "")
                {
                    dt = dt_original;

                    if (dt.Rows.Count > 0)
                    {
                        ddlpermiso.DataTextField = "permiso";
                        ddlpermiso.DataValueField = "id_permiso";
                        ddlpermiso.DataSource = dt;
                        ddlpermiso.DataBind();
                    }
                }
                else
                {
                    if (dt_original.Select("permiso like '%" + filtro + "%'").Length > 0)
                    {
                        dt = filtro == "" ? dt_original : dt_original.Select("permiso like '%" + filtro + "%'").CopyToDataTable();
                    }
                    if (dt.Rows.Count > 0)
                    {
                        ddlpermiso.DataTextField = "permiso";
                        ddlpermiso.DataValueField = "id_permiso";
                        ddlpermiso.DataSource = dt;
                        ddlpermiso.DataBind();
                    }
                    else
                    {
                        Toast.Info("No se encontro ninguna coincidencia. Intentelo nuevamente.", "Mensaje del Sistema", this);
                    }
                }

                
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar lista de permisos. " + ex.Message, this);
            }
        }

        private bool ExistUsuarioinUsuario(string usuario, int id_menu)
        {
            try
            {
                MenusCOM menus = new MenusCOM();
                DataTable dt = menus.sp_menus_usuarios(usuario).Tables[0];
                DataView dv = dt.DefaultView;
                dv.RowFilter = "id_menu = '" + id_menu.ToString().Trim().ToUpper() + "'";
                return dv.ToTable().Rows.Count > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected void lnkaddmenu_Click(object sender, EventArgs e)
        {
            try
            {
                MenusCOM menus = new MenusCOM();
                string usuario = hdfusuario.Value.ToUpper();
                string id_menu = ddlmenus.SelectedValue;
                if (ExistUsuarioinUsuario(usuario, Convert.ToInt32(id_menu)))
                {
                    Toast.Info("El usuario ya cuenta con el menu "+ddlmenus.SelectedItem,"Mensaje del sistema",this);
                }
                else {
                    string vmensaje = menus.Agregar(usuario.ToUpper(),Convert.ToInt32(id_menu));
                    if (vmensaje != "")
                    {
                        Toast.Error("Error al cargar guardar menu: " + vmensaje, this);
                    }
                    else {
                        LlenarInformacionModal();
                        Toast.Success("Menu asignado al usuario de manera correcta.","Mensaje del sistema", this);
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar guardar menu: " + ex.Message, this);
            }
        }

        protected void lnkasaveperfil_Click(object sender, EventArgs e)
        {
            try
            {
                PerfilesCOM perfil = new PerfilesCOM();
                string usuario = hdfusuario.Value.ToUpper();
                string id_perfil = ddlperfiles.SelectedValue;
                string vmensaje = "";
                if (perfil.ExistUsuarioPerfil(usuario))
                {
                    perfil.Eliminar(usuario);
                }

                if (vmensaje == "")
                {
                    vmensaje = perfil.Agregar(usuario.ToUpper(), Convert.ToInt32(id_perfil), Session["usuario"] as string);
                }

                if (vmensaje != "")
                {
                    Toast.Error("Error al asignar perfil: " + vmensaje, this);
                }
                else
                {
                    LlenarInformacionModal();
                    Toast.Success("Perfil asignado al usuario de manera correcta.", "Mensaje del sistema", this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al asignar perfil: " + ex.Message, this);
            }
        }

        protected void lnkaddpermisos_Click(object sender, EventArgs e)
        {
            div_menus.Visible = false;
            div_addperfil.Visible = false;
            div_permiso.Visible = true;

        }

        protected void lnkbuscarpermiso_Click(object sender, EventArgs e)
        {
            if (txtbuscarpermiso.Text.Trim().Length > 2 || txtbuscarpermiso.Text.Trim().Length == 0)
            {
                CargarListadoPermisos(txtbuscarpermiso.Text.Trim());
                imgpermiso.Style["display"] = "none";
                lblpermiso.Style["display"] = "none";
            }
            else
            {
                Toast.Info("Ingrese un minimo de 3 caracteres para realizar la busqueda.", "Mensaje del Sistema", this);
            }
        }

        protected void lnkaddpermiso_Click(object sender, EventArgs e)
        {
            try
            {
                
                usuarios_permisos permiso = new usuarios_permisos();
                permiso.id_permiso = Convert.ToInt32(ddlpermiso.SelectedValue);
                permiso.usuario = hdfusuario.Value.ToUpper();
                UsuariosCOM usuarios = new UsuariosCOM();

                string vmensaje = usuarios.AgregarPermiso(permiso);
                if (vmensaje != "")
                {
                    Toast.Error("Error al asignar permiso: " + vmensaje, this);
                }
                else
                {
                    LlenarInformacionModal();
                    Toast.Success("Permiso asignado al usuario de manera correcta.", "Mensaje del sistema", this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al asignar permiso: " + ex.Message, this);
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

        protected void cbxcheckall_empleados_CheckedChanged(object sender, EventArgs e)
        {
            ViewState["dt_usuarios"] = null;
            if (cbxcheckall_empleados.Checked)
            {
                DataTable dt_new = new DataTable();
                dt_new.Columns.Add("usuario");
                foreach (RepeaterItem item in rdllista_empleados.Items)
                {
                    CheckBox check = item.FindControl("mycheck") as CheckBox;
                    check.Checked = true;
                    DataRow row = dt_new.NewRow();
                    row["usuario"] = check.ToolTip.Trim().ToUpper();
                    dt_new.Rows.Add(row);
                }
                ViewState["dt_usuarios"] = dt_new;
            }
            else
            {
                foreach (RepeaterItem item in rdllista_empleados.Items)
                {
                    CheckBox check = item.FindControl("mycheck") as CheckBox;
                    check.Checked = false;
                }
            }
        }

        protected void mycheck_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox item = sender as CheckBox;
            string usuario = item.ToolTip.ToString().Trim().ToUpper();
            bool exist = ExistUsuarioDelegados(usuario);
            EliminarUsuarioPerfiles(usuario);
            if (item.Checked)
            {
                if (!exist)
                {
                    AgregarUsuarioPerfiles(usuario);
                }
                else
                {
                    item.Checked = false;
                    Toast.Error("El usuario " + usuario.ToUpper() + " ya existe en la lista.", this);
                }
            }
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> cadena_usuarios = CadenaUsuarios();
                string usuario_jefe = hdfusuario.Value.ToUpper();
                EmpleadosCOM empleados = new EmpleadosCOM();
                string vmensaje = empleados.AgregarDelegados(usuario_jefe, cadena_usuarios);
                if (vmensaje != "")
                {
                    Toast.Error("Error al guardar delegados: " + vmensaje, this);
                }
                else
                {
                    ViewState["dt_usuarios"] = null;
                    LlenarInformacionModal();
                    Toast.Success("Configuración guardada correctamente", "Mensaje del sistema", this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Erro al guardar esta configuración: "+ex.Message,this);
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
                Toast.Error("Error al eliminar usuario: " + ex.Message, this);
            }
        }

        private List<String> CadenaUsuarios()
        {
            try
            {
                List<String> cadena = new List<string>();
                DataTable dt = ViewState["dt_usuarios"] as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    cadena.Add(row["usuario"].ToString().Trim().ToUpper());
                }
                return cadena;
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }

      
    }
}