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
    public partial class catalogo_impacto_costo : System.Web.UI.Page
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

        private DataTable GetImpactosCostos()
        {
            DataTable dt = new DataTable();
            try
            {

                RiesgosImpactoCostosCOM IC = new RiesgosImpactoCostosCOM();
                dt = IC.SelectAll();
            }
            catch (Exception)
            {
                dt = new DataTable();
            }
            return dt;
        }

        private riesgos_impactos GetImpactoCosto(int id_riesgo_impacto)
        {
            riesgos_impactos dt = new riesgos_impactos();
            try
            {
                RiesgosImpactoCostosCOM IC = new RiesgosImpactoCostosCOM();
                dt = IC.impacto(id_riesgo_impacto);
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
                DataTable dt = GetImpactosCostos();
                repeat_impactocosto.DataSource = dt;
                repeat_impactocosto.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar catalogo: " + ex.Message, this);
            }
        }

        private string Agregar(riesgos_impactos id_riesgo_impacto)
        {
            RiesgosImpactoCostosCOM IC = new RiesgosImpactoCostosCOM();
            string vmensaje = IC.Agregar(id_riesgo_impacto);
            if (vmensaje == "")
            {
                return "";
            }
            else
            {
                return vmensaje;
            }
        }
        private string Editar(riesgos_impactos id_riesgo_impacto)
        {
            RiesgosImpactoCostosCOM IC = new RiesgosImpactoCostosCOM();
            string vmensaje = IC.Editar(id_riesgo_impacto);
            if (vmensaje == "")
            {
                return "";
            }
            else
            {
                return vmensaje;
            }
        }

        private string Eliminar(int id_riesgo_impacto)
        {
            RiesgosImpactoCostosCOM IC = new RiesgosImpactoCostosCOM();
            string vmensaje = IC.Eliminar(id_riesgo_impacto);
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
            chkactivo.Checked = true;
            ModalShow("#ModalImpactocosto");
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            try
            {
                string vmensaje = string.Empty;
                int id_riesgo_impacto = Convert.ToInt32(hdfid_riesgo_impacto_costo.Value == "" ? "0" : hdfid_riesgo_impacto_costo.Value);
                riesgos_impactos IC = new riesgos_impactos();
                IC.nombre = txtnombre.Text;

                if (id_riesgo_impacto > 0) { IC.id_riesgo_impacto = id_riesgo_impacto; }
                IC.valor = Convert.ToByte(txtporcentaje.Text);
                IC.activo = chkactivo.Checked;
                IC.usuario = Session["usuario"] as string;
                if (IC.nombre == "")
                {
                    ModalShow("#ModalImpactocosto");
                    Toast.Error("Error al procesar impacto costo : Ingrese un nombre", this);
                }
                else if (IC.valor < 0)
                {
                    ModalShow("#ModalImpactocosto");
                    Toast.Error("Error al procesar impacto costo : Ingrese un valor del porcentaje", this);
                }
                else
                {
                    vmensaje = id_riesgo_impacto > 0 ? Editar(IC) : Agregar(IC);
                    if (vmensaje == "")
                    {
                        txtnombre.Text = "";
                        txtporcentaje.Text = "";
                        hdfid_riesgo_impacto_costo.Value = "";
                        chkactivo.Checked = true;
                        CargarCatalogo();
                        Toast.Success("impacto tiempo agregado correctamente.", "Mensaje del sistema", this);
                    }
                    else
                    {
                        ModalShow("#ModalImpactocosto");
                        Toast.Error("Error al procesar impacto costo : " + vmensaje, this);
                    }
                }
            }
            catch (Exception ex)
            {
                ModalShow("#ModalImpactocosto");
                Toast.Error("Error al procesar impacto costo : " + ex.Message, this);
            }
        }

        protected void btneventgrid_Click(object sender, EventArgs e)
        {
            try
            {
                int id_riesgo_impacto = Convert.ToInt32(hdfid_riesgo_impacto_costo.Value == "" ? "0" : hdfid_riesgo_impacto_costo.Value);
                if (id_riesgo_impacto > 0)
                {
                    riesgos_impactos IC = GetImpactoCosto(id_riesgo_impacto);
                    if (IC != null)
                    {
                        txtnombre.Text = IC.nombre;
                        txtporcentaje.Text = IC.valor.ToString();
                        chkactivo.Checked = IC.activo;
                        ModalShow("#ModalImpactocosto");
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar impacto costo : " + ex.Message, this);
            }
        }

        protected void btneliminar_Click(object sender, EventArgs e)
        {

            try
            {
                int id_riesgo_impacto = Convert.ToInt32(hdfid_riesgo_impacto_costo.Value == "" ? "0" : hdfid_riesgo_impacto_costo.Value);
                riesgos_impactos IC = new riesgos_impactos();
                IC.id_riesgo_impacto = id_riesgo_impacto;
                string vmensaje = Eliminar(id_riesgo_impacto);
                if (vmensaje == "")
                {
                    CargarCatalogo();
                    Toast.Success("impacto costo eliminado correctamente.", "Mensaje del sistema", this);
                }
                else
                {
                    Toast.Error("Error al eliminar impacto costo: " + vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al eliminar impacto costo: " + ex.Message, this);
            }
        }
    }
}