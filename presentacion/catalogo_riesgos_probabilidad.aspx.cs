using datos;
using datos.Model;
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
    public partial class catalogo_riesgos_probabilidad : System.Web.UI.Page
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
                CargarCatalogo();
            }
        }

        private DataTable GetRiesgosEstatus()
        {
            DataTable dt = new DataTable();
            try
            {

                RiesgosProbabilidadCOM PE = new RiesgosProbabilidadCOM();
                dt = PE.SelectAll();
            }
            catch (Exception)
            {
                dt = new DataTable();
            }
            return dt;
        }

        private riesgos_probabilidad GetProyectoEstatus(int id_riesgo_probabilidad)
        {
            riesgos_probabilidad dt = new riesgos_probabilidad();
            try
            {
                RiesgosProbabilidadCOM PE = new RiesgosProbabilidadCOM();
                dt = PE.impacto(id_riesgo_probabilidad);
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }

        private void CargarCatalogo()
        {
            try
            {
                DataTable dt = GetRiesgosEstatus();
                repeat_riesgosestrategias.DataSource = dt;
                repeat_riesgosestrategias.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar catalogo: " + ex.Message, this);
            }
        }

        private string Agregar(riesgos_probabilidad id_riesgo_probabilidad)
        {
            RiesgosProbabilidadCOM PE = new RiesgosProbabilidadCOM();
            string vmensaje = PE.Agregar(id_riesgo_probabilidad);
            return vmensaje;
        }
        private string Editar(riesgos_probabilidad id_riesgo_probabilidad)
        {
            RiesgosProbabilidadCOM PE = new RiesgosProbabilidadCOM();
            string vmensaje = PE.Editar(id_riesgo_probabilidad);
            return vmensaje;
        }

        private string Eliminar(int id_riesgo_probabilidad)
        {
            RiesgosProbabilidadCOM PE = new RiesgosProbabilidadCOM();
            string vmensaje = PE.Eliminar(id_riesgo_probabilidad);
            return vmensaje;
        }

        protected void lnknuevoproyectoestatus_Click(object sender, EventArgs e)
        {
            txtestatus.Text = "";
            txtnumdias.Text = "";
            chkactivo.Checked = true;
            ModalShow("#ModalProyectoestatus");
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            try
            {
                string vmensaje = string.Empty;
                int id_riesgo_probabilidad = Convert.ToInt32(hdfid_riesgo_probabilidad.Value == "" ? "0" : hdfid_riesgo_probabilidad.Value);
                riesgos_probabilidad PE = new riesgos_probabilidad();
                PE.nombre = txtestatus.Text;
                PE.valor = txtnumdias.Text == "" ? Convert.ToByte(0) : Convert.ToByte(txtnumdias.Text);
                if (id_riesgo_probabilidad > 0) { PE.id_riesgo_probabilidad = id_riesgo_probabilidad; }
                PE.activo = chkactivo.Checked;
                PE.usuario = Session["usuario"] as string;
                if (PE.nombre == "")
                {
                    ModalShow("#ModalProyectoestatus");
                    Toast.Error("Error al procesar Probabilidad : Ingrese un titulo", this);
                }
                else if (PE.valor < 0)
                {
                    ModalShow("#ModalProyectoestatus");
                    Toast.Error("Error al procesar Probabilidad : Ingrese un porcentaje de probabilidad mayor a cero.", this);
                }
                else
                {
                    vmensaje = id_riesgo_probabilidad > 0 ? Editar(PE) : Agregar(PE);
                    if (vmensaje == "")
                    {
                        txtestatus.Text = "";
                        chkactivo.Checked = false;
                        hdfid_riesgo_probabilidad.Value = "";
                        CargarCatalogo();
                        Toast.Success("Probabilidad agregada correctamente.", "Mensaje del sistema", this);
                    }
                    else
                    {
                        ModalShow("#ModalProyectoestatus");
                        Toast.Error("Error al procesar Probabilidad : " + vmensaje, this);
                    }
                }
            }
            catch (Exception ex)
            {
                ModalShow("#ModalProyectoestatus");
                Toast.Error("Error al procesar Probabilidad : " + ex.Message, this);
            }
        }

        protected void btneventgrid_Click(object sender, EventArgs e)
        {
            try
            {

                int id_riesgo_probabilidad = Convert.ToInt32(hdfid_riesgo_probabilidad.Value == "" ? "0" : hdfid_riesgo_probabilidad.Value);
                if (id_riesgo_probabilidad > 0)
                {
                    riesgos_probabilidad PE = GetProyectoEstatus(id_riesgo_probabilidad);
                    if (PE != null)
                    {

                        txtnumdias.Text = PE.valor.ToString();
                        txtestatus.Text = PE.nombre;
                        chkactivo.Checked = PE.activo;
                        ModalShow("#ModalProyectoestatus");
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar Probabilidad : " + ex.Message, this);
            }
        }

        protected void btneliminar_Click(object sender, EventArgs e)
        {

            try
            {
                int id_riesgo_probabilidad = Convert.ToInt32(hdfid_riesgo_probabilidad.Value == "" ? "0" : hdfid_riesgo_probabilidad.Value);
                riesgos_probabilidad PE = new riesgos_probabilidad();
                PE.id_riesgo_probabilidad = id_riesgo_probabilidad;
                string vmensaje = Eliminar(id_riesgo_probabilidad);
                if (vmensaje == "")
                {
                    CargarCatalogo();
                    Toast.Success("Probabilidad eliminada correctamente.", "Mensaje del sistema", this);
                }
                else
                {
                    Toast.Error("Error al eliminar Probabilidad: " + vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al eliminar Probabilidad: " + ex.Message, this);
            }
        }
    }
}