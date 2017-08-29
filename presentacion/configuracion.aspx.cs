using datos.Model;
using datos.NAVISION;
using negocio.Componentes;
using negocio.Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace presentacion
{
    public partial class configuracion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string usuario = Session["usuario"] as string;
                Cargar(usuario);
            }
        }

        private void AltaUser(string usuario)
        {
            try
            {
                UsuariosConfiguracionesCOM usuarios_config = new UsuariosConfiguracionesCOM();
                usuarios_configuraciones entidad = new usuarios_configuraciones();
                entidad.usuario = usuario;
                entidad.nombre = Session["nombre"] as string;
                if (!usuarios_config.Exist(usuario))
                {
                    string vmensaje = usuarios_config.Agregar(entidad);
                    if (vmensaje != "")
                    {
                        Toast.Error("Error al dar de alta el usuario en configuraciones: " + vmensaje, this);
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al dar de alta el usuario en configuraciones: "+ex.Message,this);
            }
        }

        private void Cargar(string usuario)
        {
            try
            {
                AltaUser(usuario);
                UsuariosConfiguracionesCOM usuarios_config = new UsuariosConfiguracionesCOM();
                usuarios_configuraciones entidad = new usuarios_configuraciones();
                DataTable dt = usuarios_config.Select(usuario);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    bool alerta_inicio_sesion = row["alerta_inicio_sesion"] == DBNull.Value ? true : Convert.ToBoolean(row["alerta_inicio_sesion"]);
                    bool mostrar_recordatorios = row["mostrar_recordatorios"] == DBNull.Value ? true : Convert.ToBoolean(row["mostrar_recordatorios"]);
                    bool sincronizacion_automatica = row["sincronizacion_automatica"] == DBNull.Value ? false : Convert.ToBoolean(row["sincronizacion_automatica"]);
                    string nombre = row["nombre"] == DBNull.Value ? "" : Convert.ToString(row["nombre"]);
                    cbxalerta_inicio_sesion.Checked = alerta_inicio_sesion;
                    txtnombre.Text = nombre;
                    cbxrecordatorios.Checked = mostrar_recordatorios;
                    cbxsincronizacion.Checked = sincronizacion_automatica;
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar configuraciones: " + ex.Message, this);
            }

        }

        protected void cbxalerta_inicio_sesion_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string usuario = Session["usuario"] as string;
                UsuariosConfiguracionesCOM usuarios_config = new UsuariosConfiguracionesCOM();
                usuarios_configuraciones entidad = new usuarios_configuraciones();
                entidad.usuario = usuario;
                entidad.alerta_inicio_sesion = cbxalerta_inicio_sesion.Checked;
                string vmensaje = usuarios_config.EditarAlertaInicioSesion(entidad);
                if (vmensaje != "")
                {
                    Toast.Error("Error al cambiar configuracion de inicio de sesión: " + vmensaje, this);
                }
                else {

                    Session["alerta_inicio_sesion"] = cbxalerta_inicio_sesion.Checked;
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cambiar configuracion de inicio de sesión: " + ex.Message, this);
            }

        }

        protected void lnkguardarnombre_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtnombre.Text;
                string usuario = Session["usuario"] as string;
                UsuariosConfiguracionesCOM usuarios_config = new UsuariosConfiguracionesCOM();
                usuarios_configuraciones entidad = new usuarios_configuraciones();
                entidad.usuario = usuario;
                entidad.nombre = nombre;
                string vmensaje = usuarios_config.EditarNombre(entidad);
                if (vmensaje != "")
                {
                    Toast.Error("Error al guardar nombre de usuario: " + vmensaje, this);
                }
                else {
                    Session["nombre"] = nombre;
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                             "AlertGO('Su configuración fue guardada correctamente','configuracion.aspx');", true);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al guardar nombre de usuario: " + ex.Message, this);
            }
            finally {
                lnkcargandoguardarnombre.Style["display"] = "none";
                lnkguardarnombre.Visible = true;
            }
        }

        protected void lnksincronizar_Click(object sender, EventArgs e)
        {
            try
            {
                string username = Session["usuario"] as string;
                string password = Session["contraseña"] as string;
                string mail = Session["mail"] as string;
                string mail_user = username + mail.Replace(mail.Split('@')[0], "");
                EWSHelper calendar = new EWSHelper();
                calendar.GetAllCalendar(mail_user, password);
                Toast.Success("Sincronización realizada correctamente.", "Mensaje del sistema", this.Page);
            }
            catch (Exception ex)
            {
                Toast.Error("Error al sincornizar con el servidor: " + ex.Message, this.Page);
            }
            finally
            {
                lnksincronizar.Visible = true;
                lnkloadsinc.Style["display"] = "none";
            }
        }

        protected void cbxrecordatorios_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string usuario = Session["usuario"] as string;
                UsuariosConfiguracionesCOM usuarios_config = new UsuariosConfiguracionesCOM();
                usuarios_configuraciones entidad = new usuarios_configuraciones();
                entidad.usuario = usuario;
                entidad.mostrar_recordatorios = cbxrecordatorios.Checked;
                string vmensaje = usuarios_config.EditarMostrarRecordatorios(entidad);
                if (vmensaje != "")
                {
                    Toast.Error("Error al cambiar configuracion de recordatorios: " + vmensaje, this);
                }
                else {

                    Session["mostrar_recordatorios"] = cbxrecordatorios.Checked;
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cambiar configuracion de recordatorios: " + ex.Message, this);
            }

        }

        protected void lnksubirimagen_Click(object sender, EventArgs e)
        {
            try
            {
                if (fupfotoperfil.HasFile)
                {
                    if (Path.GetExtension(fupfotoperfil.FileName).ToLower() != ".jpg"
                        || Path.GetExtension(fupfotoperfil.FileName).ToLower() != ".png"
                        || Path.GetExtension(fupfotoperfil.FileName).ToLower() != ".jpeg")
                    {

                        DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/img/users/"));//path localDateTime localDate = DateTime.Now;

                        string name = dirInfo + (Session["usuario"] as string).Trim() +".png";
                        funciones.UploadFile(fupfotoperfil, name.Trim(), this.Page);
                        Session["imagen"] = (Session["usuario"] as string).Trim() + ".png";
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                            "AlertGO('Su configuración fue guardada correctamente','configuracion.aspx');", true);
                    }
                    else
                    {
                        Toast.Error("Solo se admiten formatos de imagen JPG, PNG Y JPEG", this.Page);
                    }
                }

            }
            catch (Exception ex)
            {
                Toast.Error("Error al guardar imagen de usuario: " + ex.Message, this.Page);
            }
            finally
            {
                lnksubirimagen.Visible = true;
                lnkloadimagen.Style["display"] = "none";
            }
        }

        protected void cbxsincronizacion_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string usuario = Session["usuario"] as string;
                UsuariosConfiguracionesCOM usuarios_config = new UsuariosConfiguracionesCOM();
                usuarios_configuraciones entidad = new usuarios_configuraciones();
                entidad.usuario = usuario;
                entidad.sincronizacion_automatica = cbxsincronizacion.Checked;
                string vmensaje = usuarios_config.EditarSincronizacionAutomatica(entidad);
                if (vmensaje != "")
                {
                    Toast.Error("Error al cambiar configuracion de sincronización: " + vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cambiar configuracion de sincronización: " + ex.Message, this);
            }

        }
    }
}