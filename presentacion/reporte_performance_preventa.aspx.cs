using negocio.Componentes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using negocio.Entidades;
using Telerik.Web.UI;

namespace presentacion
{
    public partial class reporte_performance_preventa : System.Web.UI.Page
    {
        

        protected void CargarDatosFiltros(string filtro)
        {
            try
            {
                rdpfechainicial.SelectedDate = DateTime.Today;
                rdpfechafinal.SelectedDate = DateTime.Today;
                int NumJefe = Convert.ToInt32(Session["NumJefe"]);
                int num_empleado = Convert.ToInt32(Session["num_empleado"]);
                Boolean ver_Todos_los_empleados = Convert.ToBoolean(Session["ver_Todos_los_empleados"]);
                EmpleadosCOM empleados = new EmpleadosCOM();
                bool no_activos = cbxnoactivo.Checked;
                DataTable dt_empleados = new DataTable();
                DataSet ds = empleados.sp_listado_empleados(num_empleado, ver_Todos_los_empleados, no_activos);
                if (filtro != "")
                {
                    DataView dv_empleados = ds.Tables[0].DefaultView;
                    dv_empleados.RowFilter = "nombre like '%" + filtro + "%'";
                    dt_empleados = dv_empleados.ToTable();
                    if (dt_empleados.Rows.Count == 1)
                    {
                        int num_jefe = Convert.ToInt32(ddlempleado_a_consultar.SelectedValue);
                        CargarListadoEmpleado(num_jefe, false);
                    }
                }
                else
                {
                    dt_empleados = ds.Tables[0];
                }
                ddlempleado_a_consultar.DataValueField = "num_empleado";
                ddlempleado_a_consultar.DataTextField = "nombre";
                ddlempleado_a_consultar.DataSource = ds.Tables[0];
                ddlempleado_a_consultar.DataBind();
                if (!ver_Todos_los_empleados)
                {
                    ddlempleado_a_consultar.Enabled = false;
                    CargarListadoEmpleado(num_empleado, false);
                }

            }
            catch (Exception ex)
            {
                Toast.Error("Error al iniciar modal de filtros: " + ex.Message, this);
            }
            finally
            {

                lnkfiltros.Visible = true;
                nkcargandofiltros.Style["display"] = "none";
            }
        }

        protected void CargarListadoEmpleado(int num_jefe, Boolean ver_Todos_los_empleados)
        {
            try
            {
                EmpleadosCOM empleados = new EmpleadosCOM();
                bool no_activos = cbxnoactivo.Checked;
                DataSet ds = empleados.sp_listado_empleados(num_jefe, ver_Todos_los_empleados, no_activos);
                DataTable dt = ds.Tables[0];
                List<SiteDataItem> siteData = new List<SiteDataItem>();
                foreach (DataRow row in dt.Rows)
                {
                    siteData.Add(new SiteDataItem(
                        Convert.ToInt32(row["num_empleado"]),
                        Convert.ToInt32(row["numjefe"]),
                        row["nombre"].ToString(),
                        row["usuario"].ToString()
                        ));
                }
                rtvListEmpleado.DataTextField = "Text";
                rtvListEmpleado.DataValueField = "Value";
                rtvListEmpleado.DataFieldID = "ID";
                rtvListEmpleado.DataFieldParentID = "ParentID";
                rtvListEmpleado.DataSource = siteData;
                rtvListEmpleado.DataBind();
                lblcountlistempleados.Text = siteData.Count.ToString();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar listado de empleados: " + ex.Message, this);
            }
        }

        private String CadenaUsuariosFiltro()
        {
            string cadena = "";
            IList<RadListBoxItem> collection = rdtselecteds.Items;
            foreach (RadListBoxItem node in collection)
            {
                if (node.Value != "")
                {
                    cadena = cadena + node.Value.ToUpper().Trim() + ",";
                }
            }
            cadena = cadena.Remove(cadena.Length - 1);
            return cadena;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerarGraficaCumplimientoCompromisos();
            }
        }

        private void GenerarGraficaCumplimientoCompromisos()
        {
            try
            {
                string data = 
                "               {name: 'Terminados a tiempo'," +
                "                y: 56.33,color:'#00897b'" +
                "            }, {" +
                "                name: 'Terminados fuera de tiempo'," +
                "                y: 24.03,color:'#ffc400 '" +
                "            }, {" +
                "                name: 'No terminados dentro de tiempo'," +
                "                y: 10.38,color:'#1e88e5'" +
                "            }, {" +
                "                name: 'No terminados fuera de tiempo'," +
                "                y: 4.77,color:'#e53935'" +
                "            }";
                StringBuilder sb = new StringBuilder();
                sb.Append("<script type='text/javascript'>");
                string script = 
                "   Highcharts.chart('cumpli_compromisos', {" +
                "        chart: {" +
                "            plotBackgroundColor: null," +
                "            plotBorderWidth: null," +
                "            plotShadow: false," +
                "            type: 'pie'" +
                "        }," +
                "        title: {" +
                "            text: 'Cumplimiento compromisos'" +
                "        }," +
                "        tooltip: {" +
                "            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'" +
                "        }," +
                "        plotOptions: {" +
                "            pie: {" +
                "                allowPointSelect: true," +
                "                cursor: 'pointer'," +
                "                dataLabels: {" +
                "                    enabled: false" +
                "                }," +
                "                showInLegend: true" +
                "            }" +
                "        }," +
                "        series: [{" +
                "            name: 'Compromisos'," +
                "            colorByPoint: true," +
                "            data: ["+ data + "]" +
                "        }]" +
                "    });";
                sb.Append(script);
                sb.Append("</script>");
                ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), sb.ToString());
                load_cumpli_compromisos.Style["display"] = "none";
            }
            catch (Exception ex)
            {
                Toast.Error("Error al generar grafica cumplimiento compromisos: "+ex.Message,this);
            }
        }

        protected void lnkfiltros_Click(object sender, EventArgs e)
        {
            //if (div_reporte.Visible)
            //{
            //    CargarDatosFiltros("");
            //}
            //ModalShow("#myModal");
        }

        protected void lnkagregarseleccion_Click(object sender, EventArgs e)
        {

        }

        protected void lnkagregartodos_Click(object sender, EventArgs e)
        {

        }

        protected void lnklimpiar_Click(object sender, EventArgs e)
        {

        }

        protected void lnkeliminarselecion_Click(object sender, EventArgs e)
        {

        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {

        }
    }
}