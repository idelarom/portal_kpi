using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class reporte_performance_preventa : System.Web.UI.Page
    {
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
    }
}