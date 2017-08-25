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
                CargarCatalogoUsuarios();
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

                //cargamos los perfiles
                CargarListadoPerfiles("");
            }
        }
        protected void btnver_Click(object sender, EventArgs e)
        {
            try
            {
                div_addperfil.Visible = false;
                div_menus.Visible = false;
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
        protected void lnkaddperfil_Click(object sender, EventArgs e)
        {
            div_addperfil.Visible = true;
            div_menus.Visible = false;
        }

        protected void lnkaddmenus_Click(object sender, EventArgs e)
        {
            div_menus.Visible = true;
            div_addperfil.Visible = false;
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
                }
                else
                {
                    if (dt_original.Select("perfil like '%" + filtro + "%'").Length > 0)
                    {
                        dt = filtro == "" ? dt_original : dt_original.Select("perfil like '%" + filtro + "%'").CopyToDataTable();
                    }
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
                }
                else
                {
                    if (dt_original.Select("name like '%" + filtro + "%'").Length > 0)
                    {
                        dt = filtro == "" ? dt_original : dt_original.Select("name like '%" + filtro + "%'").CopyToDataTable();
                    }
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
            catch (Exception ex)
            {
                Toast.Error("Error al cargar lista de menus. " + ex.Message, this);
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
    }
}