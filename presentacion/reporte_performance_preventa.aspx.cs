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
        private void AgregarItemSleccionado(IList<RadTreeNode> collection)
        {
            try
            {
                lblcountselecteds.Text = collection.Count.ToString();
                foreach (RadTreeNode node in collection)
                {
                    RadListBoxItem item = new RadListBoxItem(node.Text, node.Value);
                    rdtselecteds.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al agregar seleccion de empleados: " + ex.Message, this);
            }
        }
        protected void CargarDatosFiltros(string filtro)
        {
            try
            {
                rdpfechainicial.SelectedDate = DateTime.Today;
                rdpfechafinal.SelectedDate = DateTime.Today;
                int num_empleado = Convert.ToInt32(Session["num_empleado"]);
                Boolean ver_Todos_los_empleados =  Convert.ToBoolean(Session["ver_Todos_los_empleados"]);
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
                    ddlempleado_a_consultar.SelectedValue = num_empleado.ToString();
                    lnkagregartodos_Click(null,null);
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


        /// <summary>
        /// tipo_tiempo int = 0 --1 = ASIGNACION, 2 = VENTAS, 3 = APLAZAMIENTO, tiempo int  = 0  1= 1 DIA, 2 = 2 DIAS, 3 = 3 DIAS, 4 = MAYOR 3 DIAS, 5 = MISMO DIA
        /// </summary>
        /// <param name="fecha_ini"></param>
        /// <param name="fecha_fin"></param>
        /// <param name="ingeniero"></param>
        /// <param name="tipo"></param>
        /// <param name="tipo_consulta"></param>
        /// <param name="tipo_tiempo"></param>
        /// <param name="tiempo"></param>
        /// <returns></returns>
        private DataSet CumplimientoCompromisos(DateTime? fecha_ini, DateTime? fecha_fin, string ingeniero, string tipo, int tipo_consulta, int tipo_tiempo, 
            int tiempo, int mes)
        {
            PerformancePreventaCOM preventa = new PerformancePreventaCOM();
            return preventa.sp_Preventa_Ingenieria_reportecompromisos_detalle_test(fecha_ini,fecha_fin,ingeniero,tipo,tipo_consulta,tipo_tiempo,tiempo, mes);

        }

        private DataSet CompromisosBackLog(DateTime? fecha_ini)
        {
            PerformancePreventaCOM preventa = new PerformancePreventaCOM();
            return preventa.sps_backlogCompromisos(fecha_ini);

        }
        private void GenerarGraficaCumplimientoCompromisos(DateTime? fecha_ini, DateTime? fecha_fin, string ingeniero, string tipo, int tipo_consulta)
        {
            try
            {
                DataSet ds = CumplimientoCompromisos(fecha_ini, fecha_fin, ingeniero, tipo, tipo_consulta,0,0,0);
                DataTable dt_grid_cumpli_compromisos = ds.Tables[0];
                if (dt_grid_cumpli_compromisos.Rows.Count > 0)
                {
                    repeater_cumpli_compromisos.DataSource = dt_grid_cumpli_compromisos;
                    repeater_cumpli_compromisos.DataBind();
                    DataRow row_graph_cumpli_compromisos = ds.Tables[1].Rows[0];
                    lbltt.Text = row_graph_cumpli_compromisos["terminados_a_tiempo"].ToString();
                    lbltft.Text = row_graph_cumpli_compromisos["terminados_fuera_de_tiempo"].ToString();
                    lblndt.Text = row_graph_cumpli_compromisos["no_terminados_dentro_de_tiempo"].ToString();
                    lblnft.Text = row_graph_cumpli_compromisos["no_terminados_fuera_de_tiempo"].ToString();
                    string data =
                  "               {name: 'Terminados a tiempo'," +
                  "                y: "+row_graph_cumpli_compromisos["value_terminados_a_tiempo"].ToString()+",color:'#00897b'" +
                  "            }, {" +
                  "                name: 'Terminados fuera de tiempo'," +
                  "                y: " + row_graph_cumpli_compromisos["value_terminados_fuera_de_tiempo"].ToString() + ",color:'#ffc400 '" +
                  "            }, {" +
                  "                name: 'No terminados dentro de tiempo'," +
                  "                y: " + row_graph_cumpli_compromisos["value_no_terminados_dentro_de_tiempo"].ToString() + ",color:'#1e88e5'" +
                  "            }, {" +
                  "                name: 'No terminados fuera de tiempo'," +
                  "                y:" + row_graph_cumpli_compromisos["value_no_terminados_fuera_de_tiempo"].ToString() + ",color:'#e53935'" +
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
                    "                showInLegend: false" +
                    "            }" +
                    "        }," +
                    "        series: [{" +
                    "            name: 'Compromisos'," +
                    "            colorByPoint: true," +
                    "            data: [" + data + "]," +
                    "            point:{" +
                    "               events: {"+
                    "                   click: function () {"+
                    "                       return ViewDetailsCumpCompro('',this.name,1);" +
                    "                       }"+
                    "                     }"+
                    "            }" +
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

        private void GenerarGraficaTiemposCompromisos(DateTime? fecha_ini, DateTime? fecha_fin, string ingeniero, string tipo, int tipo_consulta)
        {
            try
            {
                DataSet ds = CumplimientoCompromisos(fecha_ini, fecha_fin, ingeniero, tipo, tipo_consulta,0,0,0);
                DataTable dt_grid_cumpli_compromisos = ds.Tables[0];
                if (dt_grid_cumpli_compromisos.Rows.Count > 0)
                {
                    DataTable dt_grafica_barras_tiempos_compromisos = ds.Tables[1];
                    DataRow row_graph_cumpli_compromisos = ds.Tables[1].Rows[0];
                    //llenamos el grid para llenar la grafica de barras
                    DataTable dt_tiempos_compro = new DataTable();
                    dt_tiempos_compro.Columns.Add("Name");
                    dt_tiempos_compro.Columns.Add("Asignación ingenieria");
                    dt_tiempos_compro.Columns.Add("Ventas");
                    dt_tiempos_compro.Columns.Add("Aplazamiento de ingenieria");

                    DataRow row = dt_tiempos_compro.NewRow();
                    row["Name"] = "Mismo dia";
                    row["Asignación ingenieria"] = row_graph_cumpli_compromisos["AsignadaMismoDia"].ToString();
                    row["Ventas"] = row_graph_cumpli_compromisos["VentaMismoDia"].ToString();
                    row["Aplazamiento de ingenieria"] = row_graph_cumpli_compromisos["AplazadasMismoDia"].ToString();
                    dt_tiempos_compro.Rows.Add(row);

                    DataRow row2 = dt_tiempos_compro.NewRow();
                    row2["Name"] = "1 dia";
                    row2["Asignación ingenieria"] = row_graph_cumpli_compromisos["Asignada1Dia"].ToString();
                    row2["Ventas"] = row_graph_cumpli_compromisos["Venta1Dia"].ToString();
                    row2["Aplazamiento de ingenieria"] = row_graph_cumpli_compromisos["Aplazadas1Dia"].ToString();
                    dt_tiempos_compro.Rows.Add(row2);

                    DataRow row3 = dt_tiempos_compro.NewRow();
                    row3["Name"] = "2 dias";
                    row3["Asignación ingenieria"] = row_graph_cumpli_compromisos["Asignada2Dia"].ToString();
                    row3["Ventas"] = row_graph_cumpli_compromisos["Venta2Dia"].ToString();
                    row3["Aplazamiento de ingenieria"] = row_graph_cumpli_compromisos["Aplazadas2Dia"].ToString();
                    dt_tiempos_compro.Rows.Add(row3);

                    DataRow row4 = dt_tiempos_compro.NewRow();
                    row4["Name"] = "3 dias";
                    row4["Asignación ingenieria"] = row_graph_cumpli_compromisos["Asignada3Dia"].ToString();
                    row4["Ventas"] = row_graph_cumpli_compromisos["Venta3Dia"].ToString();
                    row4["Aplazamiento de ingenieria"] = row_graph_cumpli_compromisos["Aplazadas3Dia"].ToString();
                    dt_tiempos_compro.Rows.Add(row4);

                    DataRow row5 = dt_tiempos_compro.NewRow();
                    row5["Name"] = "Mayor a 3 dias";
                    row5["Asignación ingenieria"] = row_graph_cumpli_compromisos["AsignadaMayor3Dia"].ToString();
                    row5["Ventas"] = row_graph_cumpli_compromisos["VentaMayor3Dia"].ToString();
                    row5["Aplazamiento de ingenieria"] = row_graph_cumpli_compromisos["AplazadasMayor3Dia"].ToString();
                    dt_tiempos_compro.Rows.Add(row5);
                    grid_tiempo_compromisos.Columns.Clear();
                    grid_tiempo_compromisos.DataSource = dt_tiempos_compro;
                    grid_tiempo_compromisos.DataBind();

                    //LLENAMOS TABLA DE INFORMACION ADICIONL
                    string Cumple = "0";
                    string NoCumple = "0";
                    string NoTerminadosPorCumplir = "0";
                    string NoTerminadosSinCumplir = "0";

                    string VentaMismoDia = "0";
                    string Venta1Dia = "0";
                    string Venta2Dia = "0";
                    string Venta3Dia = "0";
                    string VentaMayor3Dia = "0";

                    string AsignadaMismoDia = "0";
                    string Asignada1Dia = "0";
                    string Asignada2Dia = "0";
                    string Asignada3Dia = "0";
                    string AsignadaMayor3Dia = "0";

                    string AplazadasMismoDia = "0";
                    string Aplazadas1Dia = "0";
                    string Aplazadas2Dia = "0";
                    string Aplazadas3Dia = "0";
                    string AplazadasMayor3Dia = "0";
                    Cumple = ds.Tables[1].Rows[0]["Perc_Cumple"].ToString();
                    NoCumple = ds.Tables[1].Rows[0]["Perc_NoCumple"].ToString();
                    NoTerminadosPorCumplir = ds.Tables[1].Rows[0]["Perc_NoTerminadosPorCumplir"].ToString();
                    NoTerminadosSinCumplir = ds.Tables[1].Rows[0]["Perc_NoTerminadosSinCumplir"].ToString();
                    //ventas
                    VentaMismoDia = ds.Tables[1].Rows[0]["VentaMismoDia"].ToString();
                    Venta1Dia = ds.Tables[1].Rows[0]["Venta1Dia"].ToString();
                    Venta2Dia = ds.Tables[1].Rows[0]["Venta2Dia"].ToString();
                    Venta3Dia = ds.Tables[1].Rows[0]["Venta3Dia"].ToString();
                    VentaMayor3Dia = ds.Tables[1].Rows[0]["VentaMayor3Dia"].ToString();
                    //Aplazamiento
                    AplazadasMismoDia = ds.Tables[1].Rows[0]["AplazadasMismoDia"].ToString();
                    Aplazadas1Dia = ds.Tables[1].Rows[0]["Aplazadas1Dia"].ToString();
                    Aplazadas2Dia = ds.Tables[1].Rows[0]["Aplazadas2Dia"].ToString();
                    Aplazadas3Dia = ds.Tables[1].Rows[0]["Aplazadas3Dia"].ToString();
                    AplazadasMayor3Dia = ds.Tables[1].Rows[0]["AplazadasMayor3Dia"].ToString();
                    //Asignacion
                    AsignadaMismoDia = ds.Tables[1].Rows[0]["AsignadaMismoDia"].ToString();
                    Asignada1Dia = ds.Tables[1].Rows[0]["Asignada1Dia"].ToString();
                    Asignada2Dia = ds.Tables[1].Rows[0]["Asignada2Dia"].ToString();
                    Asignada3Dia = ds.Tables[1].Rows[0]["Asignada3Dia"].ToString();
                    AsignadaMayor3Dia = ds.Tables[1].Rows[0]["AsignadaMayor3Dia"].ToString();

                    //Asignar texto a labels
                    //Asignacion
                    txtAsignadaMismoDia.Text = AsignadaMismoDia;
                    txtPerc_AsignadaMismoDia.Text = ds.Tables[1].Rows[0]["Perc_AsignadaMismoDia"].ToString() + " %";
                    txtAsignada1Dia.Text = Asignada1Dia;
                    txtPerc_Asignada1Dia.Text = ds.Tables[1].Rows[0]["Perc_Asignada1Dia"].ToString() + " %";
                    txtAsignada2Dia.Text = Asignada2Dia;
                    txtPerc_Asignada2Dia.Text = ds.Tables[1].Rows[0]["Perc_Asignada2Dia"].ToString() + " %";
                    txtAsignada3Dia.Text = Asignada3Dia;
                    txtPerc_Asignada3Dia.Text = ds.Tables[1].Rows[0]["Perc_Asignada3Dia"].ToString() + " %";
                    txtAsignadaMayor3Dia.Text = AsignadaMayor3Dia;
                    txtPerc_AsignadaMayor3Dia.Text = ds.Tables[1].Rows[0]["Perc_AsignadaMayor3Dia"].ToString() + " %";
                    //ventas
                    txtVentaMismoDia.Text = VentaMismoDia;
                    txtPerc_VentaMismoDia.Text = ds.Tables[1].Rows[0]["Perc_VentaMismoDia"].ToString() + " %";
                    txtVenta1Dia.Text = Venta1Dia;
                    txtPerc_Venta1Dia.Text = ds.Tables[1].Rows[0]["Perc_Venta1Dia"].ToString() + " %";
                    txtVenta2Dia.Text = Venta2Dia;
                    txtPerc_Venta2Dia.Text = ds.Tables[1].Rows[0]["Perc_Venta2Dia"].ToString() + " %";
                    txtVenta3Dia.Text = Venta3Dia;
                    txtPerc_Venta3Dia.Text = ds.Tables[1].Rows[0]["Perc_Venta3Dia"].ToString() + " %";
                    txtVentaMayor3Dia.Text = VentaMayor3Dia;
                    txtPerc_VentaMayor3Dia.Text = ds.Tables[1].Rows[0]["Perc_VentaMayor3Dia"].ToString() + " %";
                    //Aplazamiento
                    txtAplazadasMismoDia.Text = AplazadasMismoDia;
                    txtPerc_AplazadasMismoDia.Text = ds.Tables[1].Rows[0]["Perc_AplazadasMismoDia"].ToString() + " %";
                    txtAplazadas1Dia.Text = Aplazadas1Dia;
                    txtPerc_Aplazadas1Dia.Text = ds.Tables[1].Rows[0]["Perc_Aplazadas1Dia"].ToString() + " %";
                    txtAplazadas2Dia.Text = Aplazadas2Dia;
                    txtPerc_Aplazadas2Dia.Text = ds.Tables[1].Rows[0]["Perc_Aplazadas2Dia"].ToString() + " %";
                    txtAplazadas3Dia.Text = Aplazadas3Dia;
                    txtPerc_Aplazadas3Dia.Text = ds.Tables[1].Rows[0]["Perc_Aplazadas3Dia"].ToString() + " %";
                    txtAplazadasMayor3Dia.Text = AplazadasMayor3Dia;
                    txtPerc_AplazadasMayor3Dia.Text = ds.Tables[1].Rows[0]["Perc_AplazadasMayor3Dia"].ToString() + " %";

                    txtNoAsignados.Text = " " + ds.Tables[1].Rows[0]["NoAsignado"].ToString();
                    txtPerc_NoAsignados.Text = " " + ds.Tables[1].Rows[0]["Perc_NoAsignado"].ToString() + "%";
                    txtNoAsignados2.Text = " " + ds.Tables[1].Rows[0]["NoAsignado"].ToString();
                    txtPerc_NoAsignados2.Text = " " + ds.Tables[1].Rows[0]["Perc_NoAsignado"].ToString() + "%";


                    StringBuilder sb = new StringBuilder();
                    sb.Append("<script type='text/javascript'>");
                    string script = "Highcharts.chart('tiempo_compromisos', {"
                           // + "colors: ['#d81b60','#00897b','#1e88e5','#d81b60','#00897b','#1e88e5','#d81b60','#00897b','#1e88e5','#d81b60','#00897b','#1e88e5','#d81b60','#00897b','#1e88e5'],"
                            + "data: {"
                            + "table: 'ContentPlaceHolder1_grid_tiempo_compromisos'"
                            + "},"
                            + "chart: {"
                            + "type: 'column'"
                            + " },"
                            + " title: {"
                            + "     text: ''"
                            + " },"
                            + "  yAxis: {"
                            + "      allowDecimals: false,"
                            + "      title: {"
                            + "          text: 'Compromisos'"
                            + "      }"
                            + " },"
                            + "  tooltip: {"
                            + "      formatter: function () {"
                            + "          return '<b>' + this.series.name + '</b><br />' +"
                            + "              this.point.y + ' compromiso(s) ' + this.point.name;"
                            + "       }"
                            + "   },"
                            + "  plotOptions: {"
                            + "      column: {"
                            + "          colorByPoint: false"
                            + "      },"
                            + "      series: {"
                            + "          cursor: 'name',"
                            + "          point: {"
                            + "              events: {"
                            + "                   click: function () {"
                             + "                         return ViewDetailsTiemposCompromisos(this.name,this.series.name);"
                            + "                       }"
                            + "                     }"
                            + "                  }"
                            + "               }"
                            + "             }"
                            + "          });";
                    sb.Append(script);
                    sb.Append("</script>");
                    ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), sb.ToString());
                    load_cumpli_compromisos.Style["display"] = "none";
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al generar grafica tiempos compromisos: " + ex.Message, this);
            }
        }


        private void GenerarGraficaBackLogCompromisos(DateTime? fecha_ini)
        {
            try
            {
                DataSet ds = CompromisosBackLog(fecha_ini);
                DataTable dt_grid_cumpli_compromisos = ds.Tables[0];
                if (dt_grid_cumpli_compromisos.Rows.Count > 0)
                {
                    //DataTable dt_grafica_barras_tiempos_compromisos = ds.Tables[1];
                    //DataRow row_graph_cumpli_compromisos = ds.Tables[1].Rows[0];
                    ////llenamos el grid para llenar la grafica de barras
                    //DataTable dt_tiempos_compro = new DataTable();
                    //dt_tiempos_compro.Columns.Add("Name");
                    //dt_tiempos_compro.Columns.Add("Asignación ingenieria");
                    //dt_tiempos_compro.Columns.Add("Ventas");
                    //dt_tiempos_compro.Columns.Add("Aplazamiento de ingenieria");

                    //DataRow row = dt_tiempos_compro.NewRow();
                    //row["Name"] = "Mismo dia";
                    //row["Asignación ingenieria"] = row_graph_cumpli_compromisos["AsignadaMismoDia"].ToString();
                    //row["Ventas"] = row_graph_cumpli_compromisos["VentaMismoDia"].ToString();
                    //row["Aplazamiento de ingenieria"] = row_graph_cumpli_compromisos["AplazadasMismoDia"].ToString();
                    //dt_tiempos_compro.Rows.Add(row);

                    //DataRow row2 = dt_tiempos_compro.NewRow();
                    //row2["Name"] = "1 dia";
                    //row2["Asignación ingenieria"] = row_graph_cumpli_compromisos["Asignada1Dia"].ToString();
                    //row2["Ventas"] = row_graph_cumpli_compromisos["Venta1Dia"].ToString();
                    //row2["Aplazamiento de ingenieria"] = row_graph_cumpli_compromisos["Aplazadas1Dia"].ToString();
                    //dt_tiempos_compro.Rows.Add(row2);

                    //DataRow row3 = dt_tiempos_compro.NewRow();
                    //row3["Name"] = "2 dias";
                    //row3["Asignación ingenieria"] = row_graph_cumpli_compromisos["Asignada2Dia"].ToString();
                    //row3["Ventas"] = row_graph_cumpli_compromisos["Venta2Dia"].ToString();
                    //row3["Aplazamiento de ingenieria"] = row_graph_cumpli_compromisos["Aplazadas2Dia"].ToString();
                    //dt_tiempos_compro.Rows.Add(row3);

                    //DataRow row4 = dt_tiempos_compro.NewRow();
                    //row4["Name"] = "3 dias";
                    //row4["Asignación ingenieria"] = row_graph_cumpli_compromisos["Asignada3Dia"].ToString();
                    //row4["Ventas"] = row_graph_cumpli_compromisos["Venta3Dia"].ToString();
                    //row4["Aplazamiento de ingenieria"] = row_graph_cumpli_compromisos["Aplazadas3Dia"].ToString();
                    //dt_tiempos_compro.Rows.Add(row4);

                    //DataRow row5 = dt_tiempos_compro.NewRow();
                    //row5["Name"] = "Mayor a 3 dias";
                    //row5["Asignación ingenieria"] = row_graph_cumpli_compromisos["AsignadaMayor3Dia"].ToString();
                    //row5["Ventas"] = row_graph_cumpli_compromisos["VentaMayor3Dia"].ToString();
                    //row5["Aplazamiento de ingenieria"] = row_graph_cumpli_compromisos["AplazadasMayor3Dia"].ToString();
                    //dt_tiempos_compro.Rows.Add(row5);
                    int año_actual = fecha_ini.Value.Year;
                    int año_anterior = (fecha_ini.Value.Year)-1;

                    DataTable dt_graph = ds.Tables[0];                   
                    DataView dv_aant = dt_graph.DefaultView;
                    dv_aant.RowFilter = "idYear = "+año_anterior.ToString()+"";
                    DataTable dt_anterior = dv_aant.ToTable();
                    grid_backlog_compromisos_anterior.Columns.Clear();
                    grid_backlog_compromisos_anterior.DataSource = dt_anterior;
                    grid_backlog_compromisos_anterior.DataBind();

                    DataTable dt_graph2 = ds.Tables[0];
                    DataView dv_actual = dt_graph2.DefaultView;
                    dv_actual.RowFilter = "idYear = " + año_actual.ToString() + "";
                    DataTable dt_actual = dv_actual.ToTable();
                    grid_backlog_compromisos_actual.Columns.Clear();
                    grid_backlog_compromisos_actual.DataSource = dt_actual;
                    grid_backlog_compromisos_actual.DataBind();

                    ////LLENAMOS TABLA DE INFORMACION ADICIONAL
                    //string Cumple = "0";
                    //string NoCumple = "0";
                    //string NoTerminadosPorCumplir = "0";
                    //string NoTerminadosSinCumplir = "0";

                    //string VentaMismoDia = "0";
                    //string Venta1Dia = "0";
                    //string Venta2Dia = "0";
                    //string Venta3Dia = "0";
                    //string VentaMayor3Dia = "0";

                    //string AsignadaMismoDia = "0";
                    //string Asignada1Dia = "0";
                    //string Asignada2Dia = "0";
                    //string Asignada3Dia = "0";
                    //string AsignadaMayor3Dia = "0";

                    //string AplazadasMismoDia = "0";
                    //string Aplazadas1Dia = "0";
                    //string Aplazadas2Dia = "0";
                    //string Aplazadas3Dia = "0";
                    //string AplazadasMayor3Dia = "0";
                    //Cumple = ds.Tables[1].Rows[0]["Perc_Cumple"].ToString();
                    //NoCumple = ds.Tables[1].Rows[0]["Perc_NoCumple"].ToString();
                    //NoTerminadosPorCumplir = ds.Tables[1].Rows[0]["Perc_NoTerminadosPorCumplir"].ToString();
                    //NoTerminadosSinCumplir = ds.Tables[1].Rows[0]["Perc_NoTerminadosSinCumplir"].ToString();
                    ////ventas
                    //VentaMismoDia = ds.Tables[1].Rows[0]["VentaMismoDia"].ToString();
                    //Venta1Dia = ds.Tables[1].Rows[0]["Venta1Dia"].ToString();
                    //Venta2Dia = ds.Tables[1].Rows[0]["Venta2Dia"].ToString();
                    //Venta3Dia = ds.Tables[1].Rows[0]["Venta3Dia"].ToString();
                    //VentaMayor3Dia = ds.Tables[1].Rows[0]["VentaMayor3Dia"].ToString();
                    ////Aplazamiento
                    //AplazadasMismoDia = ds.Tables[1].Rows[0]["AplazadasMismoDia"].ToString();
                    //Aplazadas1Dia = ds.Tables[1].Rows[0]["Aplazadas1Dia"].ToString();
                    //Aplazadas2Dia = ds.Tables[1].Rows[0]["Aplazadas2Dia"].ToString();
                    //Aplazadas3Dia = ds.Tables[1].Rows[0]["Aplazadas3Dia"].ToString();
                    //AplazadasMayor3Dia = ds.Tables[1].Rows[0]["AplazadasMayor3Dia"].ToString();
                    ////Asignacion
                    //AsignadaMismoDia = ds.Tables[1].Rows[0]["AsignadaMismoDia"].ToString();
                    //Asignada1Dia = ds.Tables[1].Rows[0]["Asignada1Dia"].ToString();
                    //Asignada2Dia = ds.Tables[1].Rows[0]["Asignada2Dia"].ToString();
                    //Asignada3Dia = ds.Tables[1].Rows[0]["Asignada3Dia"].ToString();
                    //AsignadaMayor3Dia = ds.Tables[1].Rows[0]["AsignadaMayor3Dia"].ToString();

                    ////Asignar texto a labels
                    ////Asignacion
                    //txtAsignadaMismoDia.Text = AsignadaMismoDia;
                    //txtPerc_AsignadaMismoDia.Text = ds.Tables[1].Rows[0]["Perc_AsignadaMismoDia"].ToString() + " %";
                    //txtAsignada1Dia.Text = Asignada1Dia;
                    //txtPerc_Asignada1Dia.Text = ds.Tables[1].Rows[0]["Perc_Asignada1Dia"].ToString() + " %";
                    //txtAsignada2Dia.Text = Asignada2Dia;
                    //txtPerc_Asignada2Dia.Text = ds.Tables[1].Rows[0]["Perc_Asignada2Dia"].ToString() + " %";
                    //txtAsignada3Dia.Text = Asignada3Dia;
                    //txtPerc_Asignada3Dia.Text = ds.Tables[1].Rows[0]["Perc_Asignada3Dia"].ToString() + " %";
                    //txtAsignadaMayor3Dia.Text = AsignadaMayor3Dia;
                    //txtPerc_AsignadaMayor3Dia.Text = ds.Tables[1].Rows[0]["Perc_AsignadaMayor3Dia"].ToString() + " %";
                    ////ventas
                    //txtVentaMismoDia.Text = VentaMismoDia;
                    //txtPerc_VentaMismoDia.Text = ds.Tables[1].Rows[0]["Perc_VentaMismoDia"].ToString() + " %";
                    //txtVenta1Dia.Text = Venta1Dia;
                    //txtPerc_Venta1Dia.Text = ds.Tables[1].Rows[0]["Perc_Venta1Dia"].ToString() + " %";
                    //txtVenta2Dia.Text = Venta2Dia;
                    //txtPerc_Venta2Dia.Text = ds.Tables[1].Rows[0]["Perc_Venta2Dia"].ToString() + " %";
                    //txtVenta3Dia.Text = Venta3Dia;
                    //txtPerc_Venta3Dia.Text = ds.Tables[1].Rows[0]["Perc_Venta3Dia"].ToString() + " %";
                    //txtVentaMayor3Dia.Text = VentaMayor3Dia;
                    //txtPerc_VentaMayor3Dia.Text = ds.Tables[1].Rows[0]["Perc_VentaMayor3Dia"].ToString() + " %";
                    ////Aplazamiento
                    //txtAplazadasMismoDia.Text = AplazadasMismoDia;
                    //txtPerc_AplazadasMismoDia.Text = ds.Tables[1].Rows[0]["Perc_AplazadasMismoDia"].ToString() + " %";
                    //txtAplazadas1Dia.Text = Aplazadas1Dia;
                    //txtPerc_Aplazadas1Dia.Text = ds.Tables[1].Rows[0]["Perc_Aplazadas1Dia"].ToString() + " %";
                    //txtAplazadas2Dia.Text = Aplazadas2Dia;
                    //txtPerc_Aplazadas2Dia.Text = ds.Tables[1].Rows[0]["Perc_Aplazadas2Dia"].ToString() + " %";
                    //txtAplazadas3Dia.Text = Aplazadas3Dia;
                    //txtPerc_Aplazadas3Dia.Text = ds.Tables[1].Rows[0]["Perc_Aplazadas3Dia"].ToString() + " %";
                    //txtAplazadasMayor3Dia.Text = AplazadasMayor3Dia;
                    //txtPerc_AplazadasMayor3Dia.Text = ds.Tables[1].Rows[0]["Perc_AplazadasMayor3Dia"].ToString() + " %";

                    //txtNoAsignados.Text = " " + ds.Tables[1].Rows[0]["NoAsignado"].ToString();
                    //txtPerc_NoAsignados.Text = " " + ds.Tables[1].Rows[0]["Perc_NoAsignado"].ToString() + "%";
                    //txtNoAsignados2.Text = " " + ds.Tables[1].Rows[0]["NoAsignado"].ToString();
                    //txtPerc_NoAsignados2.Text = " " + ds.Tables[1].Rows[0]["Perc_NoAsignado"].ToString() + "%";


                    StringBuilder sb = new StringBuilder();
                    sb.Append("<script type='text/javascript'>");
                    string script = "Highcharts.chart('backlog_compromisos_anterior', {"
                            // + "colors: ['#d81b60','#00897b','#1e88e5','#d81b60','#00897b','#1e88e5','#d81b60','#00897b','#1e88e5','#d81b60','#00897b','#1e88e5','#d81b60','#00897b','#1e88e5'],"
                            + "data: {"
                            + "table: 'ContentPlaceHolder1_grid_backlog_compromisos_anterior'"
                            + "},"
                            + "chart: {"
                            + "type: 'column'"
                            + " },"
                            + " title: {"
                            + "     text: ''"
                            + " },"
                            + "  yAxis: {"
                            + "      allowDecimals: false,"
                            + "      title: {"
                            + "          text: 'Compromisos'"
                            + "      }"
                            + " },"
                            + "  tooltip: {"
                            + "      formatter: function () {"
                            + "          return '<b>' + this.series.name + '</b><br />' +"
                            + "              this.point.y + ' compromiso(s) en "+año_anterior.ToString()+"';"
                            + "       }"
                            + "   },"
                            + "  plotOptions: {"
                            + "      column: {"
                            + "          colorByPoint: false"
                            + "      },"
                            + "      series: {"
                            + "          cursor: 'name',"
                            + "          point: {"
                            + "              events: {"
                            + "                   click: function () {"
                            + "                         return ViewDetailsBacklogCompromisos( " + año_anterior.ToString() + ",this.series.name);"
                            + "                       }"
                            + "                     }"
                            + "                  }"
                            + "               }"
                            + "             }"
                            + "          });";
                    sb.Append(script);
                    sb.Append("</script>");

                    StringBuilder sb2 = new StringBuilder();
                    sb2.Append("<script type='text/javascript'>");
                    string script2 = "Highcharts.chart('backlog_compromisos_actual', {"
                            // + "colors: ['#d81b60','#00897b','#1e88e5','#d81b60','#00897b','#1e88e5','#d81b60','#00897b','#1e88e5','#d81b60','#00897b','#1e88e5','#d81b60','#00897b','#1e88e5'],"
                            + "data: {"
                            + "table: 'ContentPlaceHolder1_grid_backlog_compromisos_actual'"
                            + "},"
                            + "chart: {"
                            + "type: 'column'"
                            + " },"
                            + " title: {"
                            + "     text: ''"
                            + " },"
                            + "  yAxis: {"
                            + "      allowDecimals: false,"
                            + "      title: {"
                            + "          text: 'Compromisos'"
                            + "      }"
                            + " },"
                            + "  tooltip: {"
                            + "      formatter: function () {"
                            + "          return '<b>' + this.series.name + '</b><br />' +"
                            + "              this.point.y + ' compromiso(s) en " + año_actual.ToString() + "';"
                            + "       }"
                            + "   },"
                            + "  plotOptions: {"
                            + "      column: {"
                            + "          colorByPoint: false"
                            + "      },"
                            + "      series: {"
                            + "          cursor: 'name',"
                            + "          point: {"
                            + "              events: {"
                            + "                   click: function () {"
                            + "                         return ViewDetailsBacklogCompromisos( " + año_actual.ToString() + ",this.series.name);"
                            + "                       }"
                            + "                     }"
                            + "                  }"
                            + "               }"
                            + "             }"
                            + "          });";
                    sb2.Append(script2);
                    sb2.Append("</script>");
                    ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), sb.ToString());
                    ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), sb2.ToString());
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al generar grafica tiempos compromisos: " + ex.Message, this);
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatosFiltros("");
            }
        }

        protected void btnfiltrocumcompro_Click(object sender, EventArgs e)
        {
            try
            {
                string ingeniero = hdfingeniero.Value;
                string tipo_compro = hdftipocompromisos.Value;
                int tipo_tiempo = hdftipo_tiempo.Value == "" ? 0:Convert.ToInt32(hdftipo_tiempo.Value);
                int tiempo = hdftiempo.Value == "" ? 0 : Convert.ToInt32(hdftiempo.Value);
                int año = hdfaño.Value == "" ? 0 : Convert.ToInt32(hdfaño.Value); ;
                int mes = hdfmes.Value == "" ? 0 : Convert.ToInt32(hdfmes.Value); ;
                DateTime fi = rdpfechainicial.SelectedDate.Value == null ? DateTime.Now.AddDays(-30) : Convert.ToDateTime(rdpfechainicial.SelectedDate);
                DateTime ff = rdpfechafinal.SelectedDate.Value == null ? DateTime.Now: Convert.ToDateTime(rdpfechafinal.SelectedDate);
                if (mes > 0)
                {            
                    DateTime firstDayOfMonth = new DateTime(año, mes, 1);
                    DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                    fi = firstDayOfMonth;
                    ff = lastDayOfMonth;
                }
                DataSet ds = CumplimientoCompromisos(fi,ff, ingeniero, tipo_compro, 2,tipo_tiempo,tiempo,mes);
                DataTable dt_grid_cumpli_compromisos = ds.Tables[0];
                if (dt_grid_cumpli_compromisos.Rows.Count > 0)
                {
                    repeater_cumpli_compromisos_detalles.DataSource = dt_grid_cumpli_compromisos;
                    repeater_cumpli_compromisos_detalles.DataBind();
                   
                    ModalShow("#modal_cumpl_compromisos");
                }
                hdfingeniero.Value = "";
                hdftipocompromisos.Value = "";
                hdftipo_tiempo.Value = "";
                hdftiempo.Value = "";
                hdfaño.Value = "";
                hdfmes.Value = "";
            }
            catch (Exception ex)
            {
                Toast.Error("Error al generar modal detalles cumplimiento compromisos: " + ex.Message, this);
            }
        }


        protected void lnkfiltros_Click(object sender, EventArgs e)
        {
            if (div_reporte.Visible)
            {
                CargarDatosFiltros("");
            }
            ModalShow("#myModal");
        }
   
        protected void lnkagregarseleccion_Click(object sender, EventArgs e)
        {
            IList<RadTreeNode> collection = rtvListEmpleado.SelectedNodes;
            IList<RadTreeNode> collection_validate = rtvListEmpleado.SelectedNodes;
            foreach (RadTreeNode node in collection_validate)
            {
                IList<RadTreeNode> child_node = node.GetAllNodes();
                if (child_node.Count > 0)
                {
                    foreach (RadTreeNode node_c in child_node)
                    {
                        collection.Add(node_c);
                    }
                }
            }
            AgregarItemSleccionado(collection);
        }

        protected void lnkagregartodos_Click(object sender, EventArgs e)
        {
            IList<RadTreeNode> collection = rtvListEmpleado.GetAllNodes();
            AgregarItemSleccionado(collection);

        }

        protected void lnklimpiar_Click(object sender, EventArgs e)
        {
            rdtselecteds.Items.Clear();
            lblcountselecteds.Text = "0";
        }

        protected void lnkeliminarselecion_Click(object sender, EventArgs e)
        {
            IList<RadListBoxItem> collection = rdtselecteds.SelectedItems;
            foreach (RadListBoxItem node in collection)
            {
                RadListBoxItem item = rdtselecteds.FindItemByValue(node.Value);
                item.Remove();
            }

            lblcountselecteds.Text = rdtselecteds.Items.Count.ToString();
        }

  

        protected void txtfilterempleado_TextChanged(object sender, EventArgs e)
        {
            lnksearch_Click(null, null);
        }

        protected void lnksearch_Click(object sender, EventArgs e)
        {
            string filter = txtfilterempleado.Text;
            try
            {
                if (filter.Length == 0 || filter.Length > 3)
                {
                    CargarDatosFiltros(filter);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al filtrar empleados: " + ex.Message, this);
            }
            finally
            {
                imgloadempleado.Style["display"] = "none";
                lblbemp.Style["display"] = "none";
            }

        }

        protected void ddlempleado_a_consultar_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int num_jefe = Convert.ToInt32(ddlempleado_a_consultar.SelectedValue);
                CargarListadoEmpleado(num_jefe, false);
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar lista de empleados: " + ex.Message, this);
            }

        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            string cadena = CadenaUsuariosFiltro();
            if (!rdpfechainicial.SelectedDate.HasValue || !rdpfechafinal.SelectedDate.HasValue)
            {
                Toast.Error("Seleccione un rango de fechas para generar el reporte.", this);
            }
            else if (cadena == "")
            {

                Toast.Error("Seleccione un empleado para generar el reporte.", this);
            }
            else
            {
                DateTime fi = rdpfechainicial.SelectedDate.Value == null ? DateTime.Now.AddDays(-30) : Convert.ToDateTime(rdpfechainicial.SelectedDate);
                DateTime ff = rdpfechafinal.SelectedDate.Value == null ? DateTime.Now : Convert.ToDateTime(rdpfechafinal.SelectedDate);
                GenerarGraficaCumplimientoCompromisos(fi, ff, "", "", 1);
                GenerarGraficaTiemposCompromisos(fi,ff,"","",2);
                GenerarGraficaBackLogCompromisos(fi);
                div_reporte.Visible = true;
            }
        }
    }
}