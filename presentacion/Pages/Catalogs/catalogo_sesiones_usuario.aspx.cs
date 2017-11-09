using negocio.Componentes;
using System;
using System.Data;

namespace presentacion.Pages.Catalogs
{
    public partial class catalogo_sesiones_usuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string usuario = Session["usuario"] as string;
                UpdateDevices(usuario);
            }
        }

        protected void UpdateDevices(string usuario)
        {
            try
            {
                EmpleadosCOM empleados = new EmpleadosCOM();
                DataTable dt = empleados.sp_usuario_sesiones(usuario, true).Tables[0];
                repeat_devices2.DataSource = dt;
                repeat_devices2.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al actualizar la lista de dispositivo(s) conectado(s): " + ex.Message, this.Page);
            }
        }

        protected void btncerrarsesion_Click(object sender, EventArgs e)
        {
            try
            {
                string usuario = Session["usuario"] as string;
                string command = hdfcommand2.Value;
                int idc_usuario_sesion = Convert.ToInt32(hdfid_usuario_sesion2.Value);
                bool bloquear = command.Trim() == "bloquear";
                if (idc_usuario_sesion > 0)
                {
                    EmpleadosCOM empleados = new EmpleadosCOM();
                    if (command == "desbloquear")
                    {
                        DataSet ds = empleados.sp_desbloquear_dispositivo(idc_usuario_sesion);
                        Toast.Success("Dispositivo desbloqueado correctamente.", "Mensaje del sistema", this.Page);
                    }
                    else
                    {
                        DataSet ds = empleados.sp_eliminar_usuario_sesiones(idc_usuario_sesion, bloquear);
                        Toast.Success("Dispositivo desconectado correctamente.", "Mensaje del sistema", this.Page);
                    }
                    UpdateDevices(usuario);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al modificar sesiones: " + ex.Message, this.Page);
            }
        }

        protected void lnkactualizar2_Click(object sender, EventArgs e)
        {

            string usuario = Session["usuario"] as string;
            UpdateDevices(usuario);
        }
    }
}