using datos.Modelos;
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
                Response.Redirect("login.aspx" + (Convert.ToBoolean(Session["cliente"]) ? "?c=1" : ""));
            }
            if (!IsPostBack)
            {
                contraseña.Visible = Convert.ToBoolean(Session["cliente"]);
                lnkguardarconfiguracion22.Visible = Convert.ToBoolean(Session["cliente"]);
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
            string url = "login.aspx" + (Convert.ToBoolean(Session["cliente"]) ? "?c=1" : "");
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect(url);
        }


        private string EditarUsuario(int id_usuario, string usuario, string contraseña, int id_uperfil)
        {
            try
            {
                usuarios entidad = new usuarios();
                UsuariosCOM usuarios = new UsuariosCOM();
                entidad.id_usuario = id_usuario;
                entidad.password = usuario;
                entidad.usuario = contraseña;
                entidad.id_uperfil = id_uperfil;
                entidad.usuario_edicion = "Sistema";
                return  usuarios.Editar(entidad);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        private string EditarUsuarioImagen(int id_usuario, string imagen)
        {
            try
            {
                usuarios entidad = new usuarios();
                UsuariosCOM usuarios = new UsuariosCOM();
                entidad.id_usuario = id_usuario;
                entidad.img_profile = imagen;
                entidad.usuario_edicion = "Sistema";
                return usuarios.EditarImagen(entidad);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        protected void lnkguardarconfiguracion_Click(object sender, EventArgs e)
        {
            string vmensaje = "";
            try
            {
                if (contraseña.Visible && rtxtcontraseña.Text == "")
                {
                    vmensaje = "Ingrese una Contraseña";
                }
                else if (contraseña.Visible && rtxtcontraseña.Text != "")
                {
                    vmensaje = EditarUsuario(Convert.ToInt32(Session["id_usuario"]), funciones.deTextoa64(rtxtcontraseña.Text.Trim()), (Session["usuario"] as string).ToUpper().Trim(), Convert.ToInt32(Session["id_uperfil"]));

                    Session["password"] = rtxtcontraseña.Text.Trim();
                }
                else {

                }
            }
            catch (Exception ex)
            {
                vmensaje = ex.ToString();
            }
            finally {
                lnkcargandotermina22.Style["display"]= "none";
                lnkguardarconfiguracion22.Visible = true;
                if (vmensaje == "")
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                            "ModalClose('#myModalUserConfig');", true);
                    Alert.ShowAlert("Configuración guardada correctamente","Mensaje del Sistema",this.Page);
                }
                else {
                    Alert.ShowAlertError(vmensaje,this.Page);
                }
            }
        }

        protected void lnkconfig_Click(object sender, EventArgs e)
        {
             rtxtcontraseña.Text= Session["password"] as string;
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
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }

        protected void lnksubirfotoperfil_Click(object sender, EventArgs e)
        {
            try
            {
                if (fupfotoperfil.HasFile)
                {
                    if (Path.GetExtension(fupfotoperfil.FileName).ToLower() != ".jpg"
                        || Path.GetExtension(fupfotoperfil.FileName).ToLower() != ".png"
                        || Path.GetExtension(fupfotoperfil.FileName).ToLower() != ".gif"
                        || Path.GetExtension(fupfotoperfil.FileName).ToLower() != ".jpeg")
                    {

                        DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/files/users/"));//path localDateTime localDate = DateTime.Now;
                       
                        string name = dirInfo + (Session["usuario"] as string).Trim() + Path.GetExtension(fupfotoperfil.FileName);
                        funciones.UploadFile(fupfotoperfil, name.Trim(), this.Page);
                        Session["imagen"] = (Session["usuario"] as string).Trim() + Path.GetExtension(fupfotoperfil.FileName);
                        EditarUsuarioImagen(Convert.ToInt32(Session["id_usuario"]),(Session["usuario"] as string).Trim() + Path.GetExtension(fupfotoperfil.FileName));
                        CargarImagen();
                        Alert.ShowAlert("Configuración guardada correctamente", "Mensaje del Sistema", this.Page);
                    }
                    else
                    {
                        Alert.ShowAlertError("Solo se admiten formatos de imagen JPG, PNG, GIF Y JPEG", this.Page);
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }
    }
}