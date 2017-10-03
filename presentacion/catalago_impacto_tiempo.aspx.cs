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
    public partial class catalago_impacto_tiempo : System.Web.UI.Page
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

        private DataTable GetImpactosTiempos()
        {
            DataTable dt = new DataTable();
            try
            {

               RiesgosImpactoTiempoCOM IT = new RiesgosImpactoTiempoCOM();
                dt = IT.SelectAll();
            }
            catch (Exception)
            {
                dt = new DataTable();
            }
            return dt;
        }

        private riesgos_impacto_tiempo GetImpactoTiempo(int id_riesgo_impacto_tiempo)
        {
            riesgos_impacto_tiempo dt = new riesgos_impacto_tiempo();
            try
            {
                RiesgosImpactoTiempoCOM IT = new RiesgosImpactoTiempoCOM();
                dt = IT.impacto(id_riesgo_impacto_tiempo);
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
                DataTable dt = GetImpactosTiempos();
                repeat_impactotiempo.DataSource = dt;
                repeat_impactotiempo.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar catalogo: " + ex.Message, this);
            }
        }

        private string Agregar(riesgos_impacto_tiempo id_riesgo_impacto_tiempo)
        {
            RiesgosImpactoTiempoCOM IT = new RiesgosImpactoTiempoCOM();
            string vmensaje = IT.Agregar(id_riesgo_impacto_tiempo);
            if (vmensaje == "")
            {
                return "";
            }
            else
            {
                return vmensaje;
            }
        }
        private string Editar(riesgos_impacto_tiempo id_riesgo_impacto_tiempo)
        {
            RiesgosImpactoTiempoCOM IT = new RiesgosImpactoTiempoCOM();
            string vmensaje = IT.Editar(id_riesgo_impacto_tiempo);
            if (vmensaje == "")
            {
                return "";
            }
            else
            {
                return vmensaje;
            }
        }

        private string Eliminar(int id_riesgo_impacto_tiempo)
        {
            RiesgosImpactoTiempoCOM IT = new RiesgosImpactoTiempoCOM();
            string vmensaje = IT.Eliminar(id_riesgo_impacto_tiempo);
            if (vmensaje == "")
            {
                return "";
            }
            else
            {
                return vmensaje;
            }
        }

        protected void lnknuevoimpacto_Click(object sender, EventArgs e)
        {
            txtnombre.Text = "";
            txtporcentaje.Text = "";
            chkactivo.Checked = false;
            ModalShow("#ModalImpactotiempo");
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            try
            {
                string vmensaje = string.Empty;
                int id_riesgo_impacto_tiempo = Convert.ToInt32(hdfid_riesgo_impacto_tiempo.Value == "" ? "0" : hdfid_riesgo_impacto_tiempo.Value);
                riesgos_impacto_tiempo IT = new riesgos_impacto_tiempo();
                IT.nombre = txtnombre.Text;

                if (id_riesgo_impacto_tiempo > 0) { IT.id_riesgo_impacto_tiempo = id_riesgo_impacto_tiempo; }
                IT.porcentaje = Convert.ToDecimal(txtporcentaje.Text);
                IT.activo = chkactivo.Checked;
                IT.usuario = Session["usuario"] as string;
                if (IT.nombre == "")
                {
                    ModalShow("#ModalImpactotiempo");
                    Toast.Error("Error al procesar impacto tiempo : Ingrese un nombre", this);
                }
                else if (IT.porcentaje <= 0)
                {
                    ModalShow("#ModalImpactotiempo");
                    Toast.Error("Error al procesar impacto tiempo : Ingrese un valor del porcentaje", this);
                }
                else
                {
                    vmensaje = id_riesgo_impacto_tiempo > 0 ? Editar(IT) : Agregar(IT);
                    if (vmensaje == "")
                    {
                        txtnombre.Text = "";
                        txtporcentaje.Text = "";
                        chkactivo.Checked = false;
                        CargarCatalogo();
                        Toast.Success("impacto tiempo agregado correctamente.", "Mensaje del sistema", this);
                    }
                    else
                    {
                        ModalShow("#ModalImpactotiempo");
                        Toast.Error("Error al procesar impacto tiempo : " + vmensaje, this);
                    }
                }
            }
            catch (Exception ex)
            {
                ModalShow("#ModalImpactotiempo");
                Toast.Error("Error al procesar impacto tiempo : " + ex.Message, this);
            }
        }

        protected void btneventgrid_Click(object sender, EventArgs e)
        {
            try
            {
                int id_riesgo_impacto_tiempo = Convert.ToInt32(hdfid_riesgo_impacto_tiempo.Value == "" ? "0" : hdfid_riesgo_impacto_tiempo.Value);
                if (id_riesgo_impacto_tiempo > 0)
                {
                    riesgos_impacto_tiempo IT = GetImpactoTiempo(id_riesgo_impacto_tiempo);
                    if (IT != null)
                    {
                        txtnombre.Text = IT.nombre;
                        txtporcentaje.Text = IT.nombre;
                        chkactivo.Checked = IT.activo;
                        ModalShow("#ModalImpactotiempo");
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar Estrategia : " + ex.Message, this);
            }
        }

        protected void btneliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int id_riesgo_impacto_tiempo = Convert.ToInt32(hdfid_riesgo_impacto_tiempo.Value == "" ? "0" : hdfid_riesgo_impacto_tiempo.Value);
                riesgos_impacto_tiempo IT = new riesgos_impacto_tiempo();
                IT.id_riesgo_impacto_tiempo = id_riesgo_impacto_tiempo;
                string vmensaje = Eliminar(id_riesgo_impacto_tiempo);
                if (vmensaje == "")
                {
                    CargarCatalogo();
                    Toast.Success("impacto tiempo eliminado correctamente.", "Mensaje del sistema", this);
                }
                else
                {
                    Toast.Error("Error al eliminar impacto tiempo: " + vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al eliminar impacto tiempo: " + ex.Message, this);
            }
        }
    }
}