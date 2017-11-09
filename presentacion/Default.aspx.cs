
using negocio.Componentes;
using System;
using System.Data;

namespace presentacion
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Common/login.aspx");
        }
       

    }
}