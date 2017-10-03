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
    public partial class catalogo_proyectos_periodos : System.Web.UI.Page
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

                ProyectosPeriodosCOM PE = new ProyectosPeriodosCOM();
                dt = PE.SelectAll();
            }
            catch (Exception)
            {
                dt = new DataTable();
            }
            return dt;
        }

        private proyectos_periodos GetProyectoEstatus(int id_proyecto_periodo)
        {
            proyectos_periodos dt = new proyectos_periodos();
            try
            {
                ProyectosPeriodosCOM PE = new ProyectosPeriodosCOM();
                dt = PE.proyectos_periodo(id_proyecto_periodo);
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

        private string Agregar(proyectos_periodos id_proyecto_periodo)
        {
            ProyectosPeriodosCOM PE = new ProyectosPeriodosCOM();
            string vmensaje = PE.Agregar(id_proyecto_periodo);
            return vmensaje;
        }
        private string Editar(proyectos_periodos id_proyecto_periodo)
        {
            ProyectosPeriodosCOM PE = new ProyectosPeriodosCOM();
            string vmensaje = PE.Editar(id_proyecto_periodo);
            return vmensaje;
        }

        private string Eliminar(int id_proyecto_periodo)
        {
            ProyectosPeriodosCOM PE = new ProyectosPeriodosCOM();
            string vmensaje = PE.Eliminar(id_proyecto_periodo);
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
                int id_proyecto_periodo = Convert.ToInt32(hdfid_proyecto_periodo.Value == "" ? "0" : hdfid_proyecto_periodo.Value);
                proyectos_periodos PE = new proyectos_periodos();
                PE.nombre = txtestatus.Text;
                PE.dias = txtnumdias.Text == "" ? Convert.ToByte(0) : Convert.ToByte(txtnumdias.Text);
                if (id_proyecto_periodo > 0) { PE.id_proyecto_periodo = id_proyecto_periodo; }
                PE.activo = chkactivo.Checked;
                PE.usuario = Session["usuario"] as string;
                if (PE.nombre == "")
                {
                    ModalShow("#ModalProyectoestatus");
                    Toast.Error("Error al procesar Periodo : Ingrese un titulo", this);
                }
                else if (PE.dias <= 0)
                {
                    ModalShow("#ModalProyectoestatus");
                    Toast.Error("Error al procesar Periodo : Ingrese un numero de dias mayor a cero.", this);
                }
                else
                {
                    vmensaje = id_proyecto_periodo > 0 ? Editar(PE) : Agregar(PE);
                    if (vmensaje == "")
                    {
                        txtestatus.Text = "";
                        chkactivo.Checked = false;
                        CargarCatalogo();
                        Toast.Success("Periodo agregado correctamente.", "Mensaje del sistema", this);
                    }
                    else
                    {
                        ModalShow("#ModalProyectoestatus");
                        Toast.Error("Error al procesar Periodo : " + vmensaje, this);
                    }
                }
            }
            catch (Exception ex)
            {
                ModalShow("#ModalProyectoestatus");
                Toast.Error("Error al procesar Periodo : " + ex.Message, this);
            }
        }

        protected void btneventgrid_Click(object sender, EventArgs e)
        {
            try
            {

                int id_proyecto_periodo = Convert.ToInt32(hdfid_proyecto_periodo.Value == "" ? "0" : hdfid_proyecto_periodo.Value);
                if (id_proyecto_periodo > 0)
                {
                    proyectos_periodos PE = GetProyectoEstatus(id_proyecto_periodo);
                    if (PE != null)
                    {

                        txtnumdias.Text = PE.dias.ToString();
                        txtestatus.Text = PE.nombre;
                        chkactivo.Checked = PE.activo;
                        ModalShow("#ModalProyectoestatus");
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar Periodo : " + ex.Message, this);
            }
        }

        protected void btneliminar_Click(object sender, EventArgs e)
        {

            try
            {
                int id_proyecto_periodo = Convert.ToInt32(hdfid_proyecto_periodo.Value == "" ? "0" : hdfid_proyecto_periodo.Value);
                proyectos_periodos PE = new proyectos_periodos();
                PE.id_proyecto_periodo = id_proyecto_periodo;
                string vmensaje = Eliminar(id_proyecto_periodo);
                if (vmensaje == "")
                {
                    CargarCatalogo();
                    Toast.Success("Periodo eliminado correctamente.", "Mensaje del sistema", this);
                }
                else
                {
                    Toast.Error("Error al eliminar Periodo: " + vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al eliminar Periodo: " + ex.Message, this);
            }
        }
    }
}