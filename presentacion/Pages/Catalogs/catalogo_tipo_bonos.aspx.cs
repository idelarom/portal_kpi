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
    public partial class catalogo_tipo_bonos : System.Web.UI.Page
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
                TipoBonosCOM getall = new TipoBonosCOM();
                dt = getall.SelectAll();
            }
            catch (Exception ex)
            {
                dt = new DataTable();
            }
            return dt;
        }
        
        private bonds_types Gettipobonos(int id_bond_type)
        {
            bonds_types dt = new bonds_types();
            try
            {
                TipoBonosCOM getbt = new TipoBonosCOM();
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

        private string Agregar(bonds_types id_bond_type)
        {
            TipoBonosCOM bono = new TipoBonosCOM();
            string vmensaje = bono.Agregar(id_bond_type);

            return vmensaje;
        }
        private string Editar(bonds_types id_bond_type)
        {
            TipoBonosCOM bono = new TipoBonosCOM();
            string vmensaje = bono.Editar(id_bond_type);

            return vmensaje;
        }

        private string Eliminar(int id_bond_type)
        {
            TipoBonosCOM bono = new TipoBonosCOM();
            string vmensaje = bono.Eliminar(id_bond_type);

            return vmensaje;
        }



        protected void lnknuevotipobono_Click(object sender, EventArgs e)
        {
            txtbono.Text ="";
            txtdescripcion.Text = "";
            chkRmonto.Checked = false;
            txtmonto.Text = "";
            chkperiodicidad.Checked = false;
            chksemana.Checked = false;
            chkfolipm.Checked = false;
            chkproyecto.Checked = false;
            chkcliente.Checked = false;
            chkhoras.Checked = false;
            chkcamount.Checked = false;
            chkgrid.Checked = false;
            chkinperiodo.Checked = false;
            chkfinperiodo.Checked = false;
            txtporcentaje.Text = "";
            chkmesselect.Checked = false;
            chkfile.Checked = false;
            ModalShow("#ModalTipoBonos");
        }

        protected void btneventgrid_Click(object sender, EventArgs e)
        {
            try
            {

                int id_bond_type = Convert.ToInt32(hdfid_bond_type.Value == "" ? "0" : hdfid_bond_type.Value);
                if (id_bond_type > 0)
                {
                    bonds_types bono = Gettipobonos(id_bond_type);
                    if (bono != null)
                    {
                        txtbono.Text = bono.name;
                        txtdescripcion.Text = bono.description;
                        chkRmonto.Checked =Convert.ToBoolean(bono.amount_required);
                        txtmonto.Text= bono.amount.ToString();
                        chkperiodicidad.Checked = Convert.ToBoolean(bono.periodicity_required);
                        chksemana.Checked = Convert.ToBoolean(bono.week_detail_required);
                        chkfolipm.Checked = Convert.ToBoolean(bono.folio_pmtracker_required);
                        chkproyecto.Checked = Convert.ToBoolean(bono.project_name_required);
                        chkcliente.Checked = Convert.ToBoolean(bono.customer_name_required);
                        chkhoras.Checked = Convert.ToBoolean(bono.number_hours_required);
                        chkcamount.Checked = Convert.ToBoolean(bono.authorization_amount_capture);
                        chkgrid.Checked = Convert.ToBoolean(bono.grid_requisitions_required);
                        chkinperiodo.Checked = Convert.ToBoolean(bono.period_date_of_capture);
                        chkfinperiodo.Checked = Convert.ToBoolean(bono.period_date_to_capture);
                        txtporcentaje.Text = bono.percentage_extra.ToString();
                        chkmesselect.Checked = Convert.ToBoolean(bono.month_select);
                        chkfile.Checked = Convert.ToBoolean(bono.files_required);
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
                bonds_types bono = new bonds_types();
                bono.id_bond_type = id_bond_type;
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

        protected void lnkguardar_Click(object sender, EventArgs e)
        {

            try
            {
                string vmensaje = string.Empty;
                int id_bond_type = Convert.ToInt32(hdfid_bond_type.Value == "" ? "0" : hdfid_bond_type.Value);
                bonds_types bono = new bonds_types();

                if (id_bond_type > 0) { bono.id_bond_type = id_bond_type; }
                bono.name = txtbono.Text;
                bono.description = txtdescripcion.Text;
                bono.amount_required = chkRmonto.Checked;
                txtmonto.Text = bono.amount.ToString();
                bono.periodicity_required = chkperiodicidad.Checked;
                bono.week_detail_required = chksemana.Checked;
                bono.folio_pmtracker_required = chkfolipm.Checked;
                bono.project_name_required = chkproyecto.Checked;
                bono.customer_name_required = chkcliente.Checked;
                bono.number_hours_required = chkhoras.Checked;

                bono.authorization_amount_capture = chkcamount.Checked;
                bono.grid_requisitions_required = chkgrid.Checked;
                bono.period_date_of_capture = chkinperiodo.Checked;
                bono.period_date_to_capture = chkfinperiodo.Checked;
                bono.percentage_extra = Convert.ToDecimal(txtporcentaje.Text);
                bono.month_select = chkmesselect.Checked;
                bono.files_required = chkfile.Checked;
                bono.created_by = Session["usuario"] as string;
                bono.created = DateTime.Now;
                bono.modified_by = Session["usuario"] as string;
                bono.modified = DateTime.Now;
                if (bono.name == "")
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
                        txtdescripcion.Text = "";
                        chkRmonto.Checked = false;
                        txtmonto.Text = "";
                        chkperiodicidad.Checked = false;
                        chksemana.Checked = false;
                        chkfolipm.Checked = false;
                        chkproyecto.Checked = false;
                        chkcliente.Checked = false;
                        chkhoras.Checked = false;
                        chkcamount.Checked = false;
                        chkgrid.Checked = false;
                        chkinperiodo.Checked = false;
                        chkfinperiodo.Checked = false;
                        txtporcentaje.Text = "";
                        chkmesselect.Checked = false;
                        chkfile.Checked = false;
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
    }
}