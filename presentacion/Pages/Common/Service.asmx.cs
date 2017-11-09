using datos.NAVISION;
using negocio.Componentes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace presentacion.Pages.Common
{
    /// <summary>
    /// Descripción breve de Service
    /// </summary>
    [WebService(Namespace = "http://apps.migesa.com.mx/portal_connext/Pages/Common/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {
        public String DevicesConecteds()
        {
            try
            {
                string value = "";
                if (Convert.ToBoolean(Session["alerta_inicio_sesion"]))
                {
                    EmpleadosCOM empleados = new EmpleadosCOM();
                    string session = Session["usuario"] as string;
                    if (session != "")
                    {
                        DataTable dt = empleados.sp_usuario_sesiones(session, false).Tables[0];
                        int devices_count = Convert.ToInt32(Session["devices_conectados"]);
                        bool mas_dispositivos = dt.Rows.Count > devices_count;
                        Session["devices_conectados"] = dt.Rows.Count;
                        if (mas_dispositivos)
                        {
                            value = "Se detecto un nuevo dispositivo conectado. Da clic sobre este mensaje para ver mas opciones.";
                        }
                    }
                }
                return value;
            }
            catch (Exception ex)
            {
                return "Error al actualizar la lista de dispositivo(s) conectado(s): " + ex.Message;
            }
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public String checaItem()
        {
            string value = DevicesConecteds();
            return value;
        }




        [System.Web.Services.WebMethod(EnableSession = true)]
        public String Login(string username, string password, string dominio)
        {
            try
            {
                string value = "";
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, dominio))
                {
                    Boolean isValid = false;
                    // validate the credentials
                    isValid = pc.ValidateCredentials(username, password);
                    EmpleadosCOM empleados = new EmpleadosCOM();
                    DataTable dt = empleados.GetLogin(username, "");
                    if (isValid && dt.Rows.Count > 0)
                    {
                        value = JsonConvert.SerializeObject(dt);
                    }
                }
                return value;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public String ProyectosWidget(int num_empleado)
        {
            try
            {
                string value = "";
                ProyectosCOM proyectos = new ProyectosCOM();
                DataTable dt = new DataTable();
                dt = proyectos.SelectWidget(num_empleado, false);
                value = JsonConvert.SerializeObject(dt);
                return value;
            }
            catch (Exception ex)
            {
                return "";
            }
        }


        [System.Web.Services.WebMethod(EnableSession = true)]
        public String GetAvisos(string usuario)
        {
            try
            {
                string value = "";
                NotificacionesCOM proyectos = new NotificacionesCOM();
                DataTable dt = new DataTable();
                dt = proyectos.notificaciones(usuario.ToUpper());
                value = JsonConvert.SerializeObject(dt);
                return value;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public String LeerNotificacion(string usuario)
        {
            try
            {
                NotificacionesCOM proyectos = new NotificacionesCOM();
                return proyectos.MarcarLeido(usuario);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



    }
}
