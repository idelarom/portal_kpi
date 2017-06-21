using negocio.Componentes;
using System;
using System.Data;
using System.IO;

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
                
                string nombre = Session["nombre"] == null ? "" : Session["nombre"] as string;
                lblname.Text = nombre;
                lblname2.Text = nombre;
                lblname3.Text = nombre;
                lblpuesto.Text = puesto;
                CargarImagen();

            }
        }


        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string url = "login.aspx";
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect(url);
        }


       

        private void CargarImagen()
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/files/users/"));
                string imagen = Session["imagen"] as string;
                if (imagen != "" && File.Exists(dirInfo.ToString().Trim()+imagen))
                {
                    DateTime localDate = DateTime.Now;
                    string date = localDate.ToString();
                    date = date.Replace("/", "_");
                    date = date.Replace(":", "_");
                    date = date.Replace(" ", "");
                    imguser.ImageUrl = "~/files/users/" + imagen+"?date="+ date;
                    imguser2.ImageUrl = "~/files/users/" + imagen + "?date=" + date;
                    imguser3.ImageUrl = "~/files/users/" + imagen + "?date=" + date;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this.Page);
            }
        }

      
    }
}