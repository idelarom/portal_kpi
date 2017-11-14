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

        private string Agregar(riesgos_estatus id_riesgos_estatus)
        {
            RiesgosEstatusCOM PE = new RiesgosEstatusCOM();
            string vmensaje = PE.Agregar(id_riesgos_estatus);

            return vmensaje;
        }
        private string Editar(riesgos_estatus id_riesgos_estatus)
        {
            RiesgosEstatusCOM PE = new RiesgosEstatusCOM();
            string vmensaje = PE.Editar(id_riesgos_estatus);

            return vmensaje;
        }

        private string Eliminar(int id_riesgos_estatus)
        {
            RiesgosEstatusCOM PE = new RiesgosEstatusCOM();
            string vmensaje = PE.Eliminar(id_riesgos_estatus);

            return vmensaje;
        }

        protected void lnknuevotipobono_Click(object sender, EventArgs e)
        {
            ModalShow("#ModalTipoBonos");
        }

        protected void btneventgrid_Click(object sender, EventArgs e)
        {

        }

        protected void btneliminar_Click(object sender, EventArgs e)
        {

        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {

        }
    }
}