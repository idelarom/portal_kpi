using negocio.Componentes;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class Global : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.MaintainScrollPositionOnPostBack = true;
            string usuario = (Session["usuario"] as string) == null ? "" : (Session["usuario"] as string);
            String puesto = (Session["puesto"] as string) == null ? "" : (Session["puesto"] as string);
            if (usuario == "" || puesto == "")
            {
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                CargarImagen();
                CargarMenu();
                string nombre = Session["nombre"] == null ? "" : Session["nombre"] as string;
                lblname.Text = nombre;
                lblname2.Text = nombre;
                lblname3.Text = nombre;
                lblpuesto.Text = puesto;
                lblperfil.Text = Session["perfil"] as string;
                CargarImagen();
                UpdateDevices();
                if (Convert.ToInt32(Session["id_perfil"]) == 0)
                {
                    Toast.Info("El usuario en uso: "+Convert.ToString(Session["usuario"]).ToUpper()+", no cuenta con un perfil asignado. Por favor, comuniquese con su administrador.","Mensaje del sistema",this.Page);
                }
            }
        }
        private void CargarImagen()
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/img/users/"));
                string imagen = Session["imagen"] as string;
                if (imagen != "" && File.Exists(dirInfo.ToString().Trim() + imagen))
                {
                    DateTime localDate = DateTime.Now;
                    string date = localDate.ToString();
                    date = date.Replace("/", "_");
                    date = date.Replace(":", "_");
                    date = date.Replace(" ", "");
                    imguser.ImageUrl = "~/img/users/" + imagen + "?date=" + date;
                    imguser2.ImageUrl = "~/img/users/" + imagen + "?date=" + date;
                    imguser3.ImageUrl = "~/img/users/" + imagen + "?date=" + date;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }
        private DataTable TableMenu(int id_menu_padre)
        {
            try
            {
                EmpleadosCOM componente = new EmpleadosCOM();
                DataSet ds = componente.sp_menu(id_menu_padre, Convert.ToString(Session["usuario"]));
                return ds.Tables[0];

            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar el menu: "+ex.Message,this.Page);
                return null;
            }
        }
        protected void repeat_menu_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            Repeater repeater_menu_multi = (Repeater)e.Item.FindControl("repeater_menu_multi");
            HtmlGenericControl ml = (HtmlGenericControl)e.Item.FindControl("ml");
            HtmlGenericControl mml = (HtmlGenericControl)e.Item.FindControl("mml");
            int id_menu = Convert.ToInt32(DataBinder.Eval(dbr, "id_menu"));
            int total_menu = TableMenu(id_menu).Rows.Count;
            ml.Visible = total_menu == 0;
            mml.Visible = total_menu > 0;
            if (total_menu > 0)
            {
                try
                {
                    DataTable dt_menu = TableMenu(id_menu);
                    repeater_menu_multi.DataSource = dt_menu;
                    repeater_menu_multi.DataBind();
                }
                catch (Exception ex)
                {
                    Toast.Error("Error al cargar menus. " + ex.Message, this.Page);
                }
            }

        }

        private void CargarMenu()
        {
            try
            {
                DataTable dt_menu = TableMenu(0);
                repeat_menu.DataSource = dt_menu;
                repeat_menu.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar menus. "+ex.Message, this.Page);
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string url = "login.aspx";
            EmpleadosCOM empleados = new EmpleadosCOM();
            
            DataSet ds = empleados.sp_eliminar_usuario_sesiones(
                Session["usuario"] as string, Session["os"] as string, Session["os_vers"] as string,
                Session["browser"] as string, Session["device"] as string);
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect(url);
        }

        protected void UpdateDevices()
        {
            try
            {
                EmpleadosCOM empleados = new EmpleadosCOM();
                DataTable dt = empleados.sp_usuario_sesiones(Session["usuario"] as string).Tables[0];
                lbldispo.Text = dt.Rows.Count.ToString();
                repeat_devices.DataSource = dt;
                repeat_devices.DataBind();

            }
            catch (Exception ex)
            {
                Toast.Error("Error al actualizar la lista de dispositivos conectados: "+ex.Message,this.Page);
            }
        }

        protected void lnkactualizar_Click(object sender, EventArgs e)
        {
            UpdateDevices();
        }
    }
}