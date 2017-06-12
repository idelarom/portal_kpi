using System;

namespace presentacion.admon
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("~/login.aspx");
        }
    }
}