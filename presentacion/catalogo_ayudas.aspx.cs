using negocio.Componentes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class catalogo_ayudas : System.Web.UI.Page
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

        private DataTable GetAyudas()
        {
            DataTable dt = new DataTable();
            try
            {
                AyudaCOM ayudas = new AyudaCOM();
                dt = ayudas.SelectAll();
            }
            catch (Exception)
            {
                dt = new DataTable();
            }
            return dt;
        }

        private void CargarCatalogo()
        {
            try
            {
                DataTable dt = GetAyudas();
                repeat_ayudas.DataSource = dt;
                repeat_ayudas.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar catalogo: "+ex.Message,this);
            }
        }

        protected void lnknuevaayuda_Click(object sender, EventArgs e)
        {
            ModalShow("#ModalAyudas");
        }

        protected void btneventgrid_Click(object sender, EventArgs e)
        {

        }

        protected void btneliminarpermiso_Click(object sender, EventArgs e)
        {

        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {

        }
    }
}