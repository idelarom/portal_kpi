using System;

namespace presentacion.Pages.Common
{
    public partial class Login : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.MaintainScrollPositionOnPostBack = true;
            Session["usuario"] = "idelarom";
            Session["nombre"] = "Isaac De la Rosa";
        }
    }
}