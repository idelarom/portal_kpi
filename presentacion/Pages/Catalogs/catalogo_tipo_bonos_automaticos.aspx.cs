using datos;
using datos.Model;
using negocio.Componentes.Compensaciones;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion.Pages.Catalogs
{
    public partial class catalogo_tipo_bonos_automaticos : System.Web.UI.Page
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

        private DataTable GetAlltipobonos()
        {
            DataTable dt = new DataTable();
            try
            {
                TipoBonosAutomaticosCOM getall = new TipoBonosAutomaticosCOM();
                dt = getall.SelectAll();
            }
            catch (Exception ex)
            {
                dt = new DataTable();
            }
            return dt;
        }

        private bons_automatic_types Gettipobonos(int id_bond_type)
        {
            bons_automatic_types dt = new bons_automatic_types();
            try
            {
                TipoBonosAutomaticosCOM getbt = new TipoBonosAutomaticosCOM();
                dt = getbt.bono(id_bond_type);
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
                DataTable dt = GetAlltipobonos();
                repeat_bonds_type.DataSource = dt;
                repeat_bonds_type.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar catalogo: " + ex.Message, this);
            }
        }

        private string Agregar(bons_automatic_types id_bond_type)
        {
            TipoBonosAutomaticosCOM bono = new TipoBonosAutomaticosCOM();
            string vmensaje = bono.Agregar(id_bond_type);

            return vmensaje;
        }
        private string Editar(bons_automatic_types id_bond_type)
        {
            TipoBonosAutomaticosCOM bono = new TipoBonosAutomaticosCOM();
            string vmensaje = bono.Editar(id_bond_type);

            return vmensaje;
        }

        private string Eliminar(int id_bond_type)
        {
            TipoBonosAutomaticosCOM bono = new TipoBonosAutomaticosCOM();
            string vmensaje = bono.Eliminar(id_bond_type);

            return vmensaje;
        }

        protected void lnknuevotipobono_Click(object sender, EventArgs e)
        {
            txtbono.Text = "";
            ModalShow("#ModalTipoBonos");
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            try
            {
                string vmensaje = string.Empty;
                int id_bond_type = Convert.ToInt32(hdfid_bond_type.Value == "" ? "0" : hdfid_bond_type.Value);
                bons_automatic_types bono = new bons_automatic_types();

                if (id_bond_type > 0) { bono.IdBonds = id_bond_type; }
                bono.NameBonds = txtbono.Text;               
                bono.Create_by = Session["usuario"] as string;
                bono.Created = DateTime.Now;
                if (bono.NameBonds == "")
                {
                    ModalShow("#ModalTipoBonos");
                    Toast.Error("Error al procesar bono : Ingrese un titulo", this);
                }
                else
                {
                    vmensaje = id_bond_type > 0 ? Editar(bono) : Agregar(bono);
                    if (vmensaje == "")
                    {
                        txtbono.Text = "";
                        hdfid_bond_type.Value = "";
                        CargarCatalogo();
                        Toast.Success("Estatus agregado correctamente.", "Mensaje del sistema", this);
                    }
                    else
                    {
                        ModalShow("#ModalTipoBonos");
                        Toast.Error("Error al procesar estatus : " + vmensaje, this);
                    }
                }
            }
            catch (Exception ex)
            {
                ModalShow("#ModalTipoBonos");
                Toast.Error("Error al procesar estatus : " + ex.Message, this);
            }
        }

        protected void btneventgrid_Click(object sender, EventArgs e)
        {
            try
            {

                int id_bond_type = Convert.ToInt32(hdfid_bond_type.Value == "" ? "0" : hdfid_bond_type.Value);
                if (id_bond_type > 0)
                {
                    bons_automatic_types bono = Gettipobonos(id_bond_type);
                    if (bono != null)
                    {
                        txtbono.Text = bono.NameBonds;
                        ModalShow("#ModalTipoBonos");
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar bono : " + ex.Message, this);
            }
        }

        protected void btneliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int id_bond_type = Convert.ToInt32(hdfid_bond_type.Value == "" ? "0" : hdfid_bond_type.Value);
                bons_automatic_types bono = new bons_automatic_types();
                bono.IdBonds = id_bond_type;
                string vmensaje = Eliminar(id_bond_type);
                if (vmensaje == "")
                {
                    CargarCatalogo();
                    Toast.Success("Bono eliminado correctamente.", "Mensaje del sistema", this);
                }
                else
                {
                    Toast.Error("Error al eliminar bono: " + vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al eliminar bono: " + ex.Message, this);
            }
        }
    }
}