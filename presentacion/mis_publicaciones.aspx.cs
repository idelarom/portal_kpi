using negocio.Componentes;
using datos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace presentacion
{
    public partial class mis_publicaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarMisPublicaciones();
            }
        }

        private void CargarMisPublicaciones()
        {
            try
            {

                PublicacionesCOM publicaciones = new PublicacionesCOM();
                DataSet ds = publicaciones.SelectForUser(Session["usuario"] as string);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DateTime localDate = DateTime.Now;
                    string date = localDate.ToString();
                    date = date.Replace("/", "_");
                    date = date.Replace(":", "_");
                    date = date.Replace(" ", "");
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/img/users/"));
                        if (File.Exists(dirInfo.ToString().Trim() + row["imagen_usuario"].ToString()))
                        {
                            row["imagen_usuario"] = "users/" + row["imagen_usuario"].ToString() + "?date=" + date;

                        }
                        else {
                            row["imagen_usuario"] = "user.png" + "?date=" + date; 
                        }
                    }
                    repeater_publicaciones.DataSource = ds.Tables[0];
                    repeater_publicaciones.DataBind();
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Hubo un problema al generar tus publicaciones: " + ex.Message, this);
            }
        }
        
        protected void btnpublicar_Click(object sender, EventArgs e)
        {
            try
            {
                string text = editor1.InnerHtml;
                String Output = Server.HtmlDecode(text);
                Output = Output.Replace("&oacute;", "ó");
                Output = Output.Replace("&Ntilde;", "ñ");
                string titulo = "";
                string usuario = Session["usuario"] as string;
                publicaciones publicacion = new publicaciones();
                publicacion.usuario = usuario;
                publicacion.titulo = titulo;
                publicacion.descripcion = Output;
                PublicacionesCOM publicaciones = new PublicacionesCOM();
                string vmensaje = publicaciones.Agregar(publicacion, new List<publicaciones_imagenes>());
                if (vmensaje == "")
                {
                    Toast.Success("Publicación realizada de manera correcta.", "Mensaje del sistema", this);
                }
                else
                {
                    Toast.Error("Hubo un problema al generar la publicación: " + vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Hubo un problema al generar la publicación: "+ ex.Message,this);
            }

        }
    }
}