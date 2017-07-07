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
            //if (!IsPostBack)
            //{
            //}

            //string lat = "25.6667";
            //string lon = "-100.3167";
            string lat = Request["lat"];
            string lon = Request["lon"];
            string desc = Request["desc"];
            rtxtdesc.Text = desc;
            ClientScript.RegisterStartupScript(GetType(), "verMapa", "ver('" + lat + "','" + lon + "');", true);
        }
    }
}