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
            string lat = funciones.de64aTexto(Request["lat"]);
            string lon = funciones.de64aTexto(Request["lon"]);
            string desc = funciones.de64aTexto(Request["desc"]);
            //string[] description = desc.Split(',');
            //string descF = "";

            //foreach (string d in description)
            //{
            //    descF = descF + d + "\n";
            //}

            rtxtdesc.Text = desc;
            ClientScript.RegisterStartupScript(GetType(), "verMapa", "ver('" + lat + "','" + lon + "');", true);
        }
    }
}