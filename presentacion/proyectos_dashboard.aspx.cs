using ClosedXML.Excel;
using negocio.Componentes;
using negocio.Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace presentacion
{
    public partial class proyectos_dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarInformacionInicial(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"])));
            }
        }

        private void CargarInformacionInicial(int id_proyecto)
        {
            try
            {
                ProyectosCOM proyectos = new ProyectosCOM();
                DataTable dt = proyectos.Select(id_proyecto);
                if (dt.Rows.Count > 0)
                {
                    CargarRiesgos(id_proyecto);
                    DataRow proyecto = dt.Rows[0];
                    hdfid_proyecto.Value = id_proyecto.ToString();
                    lblproyect.Text = proyecto["proyecto"].ToString();
                    lblresumen.Text = proyecto["descripcion"].ToString();
                    lblperiodo.Text = proyecto["periodo"].ToString();
                    lblestatus.Text = proyecto["estatus"].ToString();
                    lbltecnologia.Text = proyecto["tecnologia"].ToString();
                    lblempleado.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(proyecto["empleado"].ToString().ToLower());
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar información del proyecto. "+ex.Message,this);
            }
        }

        private void CargarRiesgos(int id_proyecto)
        {
            try
            {
                RiesgosCOM riesgos = new RiesgosCOM();
                DataTable dt = riesgos.proyectos_riesgos(id_proyecto);
                DataView dv = dt.DefaultView;
                dv.RowFilter = "id_riesgos_estatus = 1";
                DataTable dt_riesgos_abierto = dv.ToTable();
                lblnumriesgos.Text = dt_riesgos_abierto.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar información del proyecto(riesgos): " + ex.Message, this);
            }
        }

        protected void lnkgo_riesgos_Click(object sender, EventArgs e)
        {
            Response.Redirect("proyectos_evaluacion_riesgos.aspx?id_proyecto="+Request.QueryString["id_proyecto"]);
        }
    }
}