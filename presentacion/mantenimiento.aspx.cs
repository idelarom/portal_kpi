﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class mantenimiento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string FixTime = funciones.de64aTexto(Request.QueryString["FixTime"]);
                //string[] Tiempo = FixTime.Split('*');
                //hdfDias.Value = Tiempo[0].ToString().Trim();
                //hdfHoras.Value = Tiempo[1].ToString().Trim();
                //hdfMinutos.Value = Tiempo[2].ToString().Trim();
                //hdfSegundos.Value = Tiempo[3].ToString().Trim();

                hdfFixTime.Value = FixTime;
            }

        }
    }
}