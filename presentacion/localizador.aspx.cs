using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class localizador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string lat = "25.6667";
          string lon = "-100.3167";
            ClientScript.RegisterStartupScript(GetType(), "verMapa", "ver('" + lat + "','" + lon + "');", true);
        }
    }
}