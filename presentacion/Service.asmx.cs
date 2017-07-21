using negocio.Componentes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace presentacion
{
    /// <summary>
    /// Descripción breve de Service
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
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
                EmpleadosCOM empleados = new EmpleadosCOM();
                DataTable dt = empleados.sp_usuario_sesiones(Session["usuario"] as string).Tables[0];
                int devices_count = Convert.ToInt32(Session["devices_conectados"]);
                bool mas_dispositivos = dt.Rows.Count > devices_count;
                Session["devices_conectados"] = dt.Rows.Count;
                if (mas_dispositivos)
                {
                    value = "Se detecto un nuevo dispositivo conectado. Da clic sobre este mensaje para ver mas opciones.";
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
    }
}
