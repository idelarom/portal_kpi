﻿using negocio.Componentes;
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


        private DataSet CumplimientoCompromisos(DateTime? fecha_ini, DateTime? fecha_fin, string ingeniero, string tipo, int tipo_consulta)
        {
            PerformancePreventaCOM preventa = new PerformancePreventaCOM();
            return preventa.sp_Preventa_Ingenieria_reportecompromisos_detalle_test(fecha_ini,fecha_fin,ingeniero,tipo,tipo_consulta);

        }
        private void GenerarGraficaCumplimientoCompromisos(DateTime? fecha_ini, DateTime? fecha_fin, string ingeniero, string tipo, int tipo_consulta)
        {
            try
            {
                DataSet ds = CumplimientoCompromisos(fecha_ini, fecha_fin, ingeniero, tipo, tipo_consulta);
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
                DateTime fi = rdpfechainicial.SelectedDate.Value == null ? DateTime.Now.AddDays(-30) : Convert.ToDateTime(rdpfechainicial.SelectedDate);
                DateTime ff = rdpfechafinal.SelectedDate.Value == null ? DateTime.Now: Convert.ToDateTime(rdpfechafinal.SelectedDate);
                DataSet ds = CumplimientoCompromisos(fi,ff, ingeniero, tipo_compro, 2);
                DataTable dt_grid_cumpli_compromisos = ds.Tables[0];
                if (dt_grid_cumpli_compromisos.Rows.Count > 0)
                {
                    repeater_cumpli_compromisos_detalles.DataSource = dt_grid_cumpli_compromisos;
                    repeater_cumpli_compromisos_detalles.DataBind();
                    ModalShow("#modal_cumpl_compromisos");
                }

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
                div_reporte.Visible = true;
            }
        }
    }
}