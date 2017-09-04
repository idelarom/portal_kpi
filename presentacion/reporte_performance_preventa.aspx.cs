using negocio.Componentes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class reporte_performance_preventa : System.Web.UI.Page
    {
        private void ModalShow(string modalname)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                             "ModalShow('" + modalname + "');", true);
        }

        private void ModalClose(string modalname)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                             "ModalCloseGlobal('" + modalname + "');", true);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerarGraficaCumplimientoCompromisos(DateTime.Now.AddDays(-30), DateTime.Now,"","",1);
            }
        }

        /// <summary>
        /// Devuelve cursor de datos tipo_consulta = 1 grafica, tipo consulta = 2 detalles
        /// </summary>
        /// <param name="fi"></param>
        /// <param name="ff"></param>
        /// <param name="ingeniero"></param>
        /// <param name="tipo"></param>
        /// <param name="tipo_consulta"></param>
        /// <returns></returns>
        private DataSet CumplimientoCompromisos(DateTime fi, DateTime ff, string ingeniero, string tipo, int tipo_consulta)
        {
            try
            {
                PerformancePreventaCOM preventa = new PerformancePreventaCOM();
                return preventa.sp_Preventa_Ingenieria_reportecompromisos_detalle_test(fi, ff, ingeniero, tipo,tipo_consulta);
            }
            catch (Exception ex)
            {
                Toast.Error("Error al generar cargar datos de cumplimiento compromisos: " + ex.Message, this);
                return new DataSet();
            }
        }

        /// <summary>
        /// Genera la grafica de cumplimiento compromisos, incluye la grafica y el calculo para la misma
        /// </summary>
        /// <param name="fi"></param>
        /// <param name="ff"></param>
        /// <param name="ingeniero"></param>
        /// <param name="tipo"></param>
        private void GenerarGraficaCumplimientoCompromisos(DateTime fi, DateTime ff, string ingeniero, string tipo, int tipo_consulta)
        {
            try
            {
                DataSet ds = CumplimientoCompromisos(fi, ff, ingeniero, tipo,tipo_consulta);
                DataTable dt_grid_compromisos = ds.Tables[0];
                if (dt_grid_compromisos.Rows.Count > 0)
                {
                    repeater_cumplimiento_compromisos.DataSource = dt_grid_compromisos;
                    repeater_cumplimiento_compromisos.DataBind();
                    DataTable dt_values_graph = ds.Tables[1];
                    DataRow dr_values_graph = dt_values_graph.Rows[0];
                    string data =
                   "               {name: 'Terminados a tiempo'," +
                   "                y: "+dr_values_graph["value_terminados_a_tiempo"].ToString()+",color:'#00897b'" +
                   "            }, {" +
                   "                name: 'Terminados fuera de tiempo'," +
                   "                y: " + dr_values_graph["value_terminados_fuera_de_tiempo"].ToString() + ",color:'#ffc400 '" +
                   "            }, {" +
                   "                name: 'No terminados dentro de tiempo'," +
                   "                y: " + dr_values_graph["value_no_terminados_dentro_de_tiempo"].ToString() + ",color:'#1e88e5'" +
                   "            }, {" +
                   "                name: 'No terminados fuera de tiempo'," +
                   "                y:" + dr_values_graph["value_no_terminados_fuera_de_tiempo"].ToString() + ",color:'#e53935'" +
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
                    "            data: [" + data + "]" +
                    "        }]" +
                    "    });";
                    sb.Append(script);
                    sb.Append("</script>");
                    ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), sb.ToString());
                    load_cumpli_compromisos.Style["display"] = "none";
                }
       
            }
            catch (Exception ex)
            {
                Toast.Error("Error al generar grafica cumplimiento compromisos: "+ex.Message,this);
            }
        }

        protected void lnkviewcumpli_compromisos_detalles_Click(object sender, EventArgs e)
        {
            try
            {
                string ingeniero = hdfingenierofiltrar.Value;
                string tipo = hdftipocompromisosfiltrar.Value;
                int tipo_consulta = 2;
                DataSet ds = CumplimientoCompromisos(DateTime.Now.AddDays(-30), DateTime.Now, ingeniero, tipo, tipo_consulta);
                DataTable dt_grid_compromisos = ds.Tables[0];
                if (dt_grid_compromisos.Rows.Count > 0)
                {
                    repeater_cumplimiento_compromisos_detalles.DataSource = dt_grid_compromisos;
                    repeater_cumplimiento_compromisos_detalles.DataBind();
                    hdfingenierofiltrar.Value = "";
                    hdftipocompromisosfiltrar.Value = "";
                    ModalShow("#mymodalcumplimientos_compromisos");
                }

            }
            catch (Exception ex)
            {
                Toast.Error("Error al generar detalles cumplimiento compromisos: " + ex.Message, this);
            }
        }
    }
}