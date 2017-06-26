using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio.Componentes;
using System.Data;

namespace presentacion
{
    public partial class catalogo_widgets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Cargar_catalogo_widgets(0);
            }
        }
        private void Cargar_catalogo_widgets(int id_widget)
        {
            try
            {
                WidgetsCOM widcom = new WidgetsCOM();
                DataSet ds = widcom.sp_catalogo_widgets(id_widget);
                grid_widgets.DataSource = ds.Tables[0];
                grid_widgets.DataBind();
            }
            catch (Exception ex)
            {

                Toast.Error("Error al cargar el catalogo de widgets: " + ex.Message,this);
            }
        }

        protected void lnknuevowidget_Click(object sender, EventArgs e)
        {

        }
    }
}