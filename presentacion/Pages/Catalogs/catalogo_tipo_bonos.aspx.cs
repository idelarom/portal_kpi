using System;
using System.Collections.Generic;
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