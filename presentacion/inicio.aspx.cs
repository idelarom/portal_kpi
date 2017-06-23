using datos.NAVISION;
using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace presentacion
{
    public partial class inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string usuario = Session["usuario"] as string;
                CargarDivs();
            }
        }

        private void CargarDivs()
        {
            try
            {
                EmpleadosCOM componente = new EmpleadosCOM();
                DataSet ds = componente.sp_order_divs_prueba();
                DataTable dt = ds.Tables[0];
                string value = dt.Rows[0]["order"].ToString().Trim();
                StringBuilder sb = new StringBuilder();
                sb.Append("<script type='text/javascript'>");
                sb.Append("$(document).ready(function () {");
                sb.Append(value);
                sb.Append("});</script>");
                ScriptManager.RegisterStartupScript(this,GetType(),Guid.NewGuid().ToString(),sb.ToString(),false);
            }
            catch (Exception ex)
            {
                Toast.Error(ex.Message,this);
            }
        }
        private void ModalShow(string modalname)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                             "ModalShow('" + modalname + "');", true);
        }

        
    }
}