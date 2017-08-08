using ClosedXML.Excel;
using negocio.Componentes;
using negocio.Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace presentacion
{
    public partial class reporte_performance_ingenieria_netdiario : System.Web.UI.Page
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
                hdfsessionid.Value = Guid.NewGuid().ToString();
                ViewState[hdfsessionid.Value + "-dt_reporte"] = null;
                CargarDatosFiltros("");
            }
        }

        protected void CargarDatosFiltros(string filtro)
        {
            try
            {
                //rdpfechainicial.SelectedDate = DateTime.Now;
                //rdpfechafinal.SelectedDate = DateTime.Now;
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

        protected void lnkfiltros_Click(object sender, EventArgs e)
        {
            if (div_reporte.Visible)
            {
                CargarDatosFiltros("");
            }
            ModalShow("#myModal");
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


        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            try
            {
                string pLstEmpleados = CadenaUsuariosFiltro();
                if (rdpfechainicial.SelectedDate.Value > rdpfechafinal.SelectedDate.Value)
                {
                    ModalShow("#myModal");
                    Toast.Error("La fecha inicial no puede ser mayor a la fecha final seleccionada.", this);
                }
                else if (rdpfechafinal.SelectedDate.Value > DateTime.Now)
                {
                    ModalShow("#myModal");
                    Toast.Error("la fecha final no puede ser mayor a la fecha actual", this);
                }
                else if (pLstEmpleados == "")
                {
                    ModalShow("#myModal");
                    Toast.Error("Seleccione un empleado para generar el reporte.", this);
                }
                else
                {
                    string Usr = Session["usuario"] as string;
                    lblfechaini.Text = Convert.ToDateTime(rdpfechainicial.SelectedDate).ToString("dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX")).ToUpper();
                    lblfechafin.Text = Convert.ToDateTime(rdpfechafinal.SelectedDate).ToString("dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX")).ToUpper();
                    DateTime fechaInicial = rdpfechainicial.SelectedDate.Value;
                    DateTime fechaFinal = rdpfechafinal.SelectedDate.Value;
                    DataSet ds = new DataSet();
                    PerformanceIngenieriaCOM PerformanceIngenieria = new PerformanceIngenieriaCOM();
                    ds = PerformanceIngenieria.spq_Ingenieros_Performance(fechaInicial, fechaFinal, pLstEmpleados, Usr);
                    gridPerformance.DataSource = ds;
                    gridPerformance.DataBind();
                    div_reporte.Visible = true;
                    ModalClose("#myModal");
                }
                //if (txtfechafinal.Text == "" || txtfechainicio.Text == "" && ddltipofiltro.SelectedValue == "2")
                //{
                //    ModalShow("#myModal");
                //    Toast.Error("Ingrese ambas fechas para generar el reporte.", this);
                //}
                //else if (txtfechafinal.Text == "" || txtfechainicio.Text == "" && ddltipofiltro.SelectedValue == "1")
                //{
                //    ModalShow("#myModal");
                //    Toast.Error("Seleccione un trimestre para generar el reporte.", this);
                //}
                //else if (pLstEmpleados == "")
                //{
                //    ModalShow("#myModal");
                //    Toast.Error("Seleccione un empleado para generar el reporte.", this);
                //}
                //else
                //{
                //    DateTime? fecha_ini = Convert.ToDateTime(txtfechainicio.Text.ToString());
                //    DateTime? fecha_fin = Convert.ToDateTime(txtfechafinal.Text.ToString());


                //    string Usr = Session["usuario"] as string;
                //    DataTable dt = GetDashboardBonos(fecha_ini, fecha_fin, pLstEmpleados, Usr);
                //    div_reporte.Visible = false;
                //    ViewState[hdfsessionid.Value + "-dt_reporte"] = null;
                //    if (dt.Rows.Count > 0)
                //    {
                //        lblfechaini.Text = Convert.ToDateTime(fecha_ini).ToString("dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX")).ToUpper();
                //        lblfechafin.Text = Convert.ToDateTime(fecha_fin).ToString("dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX")).ToUpper();
                //        ViewState[hdfsessionid.Value + "-dt_reporte"] = dt;
                //        div_reporte.Visible = true;
                //        repeater_bonos.DataSource = dt;
                //        repeater_bonos.DataBind();
                //        ModalClose("#myModal");
                //    }
                //    else
                //    {
                //        ModalShow("#myModal");
                //        Toast.Info("El filtro no arrojo ningun resultado, puede intentarlo nuevamente.", "Ningun resultado encontrado", this);
                //    }
                //}
            }
            catch (Exception ex)
            {
                ModalShow("#myModal");
                Toast.Error("Error al generar el reporte: " + ex.Message, this);
            }
            finally
            {
                lnkguardar.Visible = true;
                lnkcargando.Style["display"] = "none";
                div_modalbodyfiltros.Visible = true;
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

        protected void gridPerformance_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            
            DateTime fechaInicial = rdpfechainicial.SelectedDate.Value.Date;
            DateTime fechaFinal = rdpfechafinal.SelectedDate.Value.Date;
            PerformanceIngenieriaCOM PerformanceIngenieria = new PerformanceIngenieriaCOM();

            GridDataItem dataItem = ((GridDataItem)e.DetailTableView.ParentItem);
            string Login = dataItem.GetDataKeyValue("Login").ToString();
            //GridTableView nestedTableView = (RadGrid1.MasterTableView.Items[0] as GridDataItem).ChildItem.NestedTableViews[0];
            if (dataItem.Edit)
            {
                return;
            }
            if (e.DetailTableView.DetailTableIndex == 0)
            {
                //DataSet ds = (DataSet)e.DetailTableView.DataSource;
                //e.DetailTableView.DataSource = ds.Tables["Dashboard_Preventa_Ingenieria"].Select("Login = '" + dataItem["Login"].Text + "'");
                e.DetailTableView.DataSource = PerformanceIngenieria.spq_Dashboard_Preventa_Ingenieria(fechaInicial, fechaFinal, Login);
            }
            else if (e.DetailTableView.DetailTableIndex == 1)
            {
                //DataSet ds = (DataSet)e.DetailTableView.DataSource;
                //e.DetailTableView.DataSource = ds.Tables["Performance_Ingenieria"].Select("Login = '" + dataItem["Login"].Text + "'");
                e.DetailTableView.DataSource = PerformanceIngenieria.spq_Performance_Ingenieria(fechaInicial, fechaFinal, Login);
            }
            else if (e.DetailTableView.DetailTableIndex == 2)
            {
                //DataSet ds = (DataSet)e.DetailTableView.DataSource;
                //e.DetailTableView.DataSource = ds.Tables["Sailine"].Select("Login = '" + dataItem["Login"].Text + "'");
                e.DetailTableView.DataSource = PerformanceIngenieria.spq_Performance_Ingenieria_sailine(fechaInicial, fechaFinal, Login);
            }            
        }

        protected void gridPerformance_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

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

        protected void lnkgenerarpdf_Click(object sender, EventArgs e)
        {
            //gridPerformance.ExportSettings.ExportOnlyData = true;
            //gridPerformance.ExportSettings.IgnorePaging = true;
            //gridPerformance.ExportSettings.OpenInNewWindow = true;
            //gridPerformance.ExportSettings.FileName = "Reporte_Performance_Ingenieria";
            //gridPerformance.MasterTableView.ExportToPdf();

            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Login");
                dt.Columns.Add("Ingeniero");
                dt.Columns.Add("Soporte");
                dt.Columns.Add("Preventa");
                dt.Columns.Add("Desarrollo_Negocio");
                dt.Columns.Add("Capacitacion");
                dt.Columns.Add("Administrativas");
                //dt.Columns.Add("Logistica_Traslados");
                dt.Columns.Add("Incapacidad");
                dt.Columns.Add("Uptime");
                dt.Columns.Add("Proyectos_Internos_Facturables");
                dt.Columns.Add("Proyectos_Internos_No_Facturables");
                dt.Columns.Add("Proyectos");
                dt.Columns.Add("Tiempo_Personal");
                dt.Columns.Add("Usabilidad");
                dt.Columns.Add("Total_Horas");



                //dt = ViewState[hdfsessionid.Value + "-dt_reporte"] as DataTable;

                foreach (GridDataItem row in gridPerformance.Items)
                {
                    DataRow dtrow = dt.NewRow();
                    dtrow["Login"] = row["Login"].Text;
                    dtrow["Ingeniero"] = row["Ingeniero"].Text;
                    dtrow["Soporte"] = row["Soporte"].Text;
                    dtrow["Preventa"] = row["Preventa"].Text;
                    dtrow["Desarrollo_Negocio"] = row["Desarrollo_Negocio"].Text;
                    dtrow["Capacitacion"] = row["Capacitacion"].Text;
                    dtrow["Administrativas"] = row["Administrativas"].Text;
                    //dtrow["Logistica_Traslados"] = row["Logistica_Traslados"].Text;
                    dtrow["Incapacidad"] = row["Incapacidad"].Text;
                    dtrow["Uptime"] = row["Uptime"].Text;
                    dtrow["Proyectos_Internos_Facturables"] = row["Proyectos_Internos_Facturables"].Text;
                    dtrow["Proyectos_Internos_No_Facturables"] = row["Proyectos_Internos_No_Facturables"].Text;
                    dtrow["Proyectos"] = row["Proyectos"].Text;
                    dtrow["Tiempo_Personal"] = row["Tiempo_Personal"].Text;
                    dtrow["Usabilidad"] = row["Usabilidad"].Text;
                    dtrow["Total_Horas"] = row["Total_Horas"].Text;
                    dt.Rows.Add(dtrow);
                }


                if (dt.Rows.Count > 0)
                {
                    Export Export = new Export();
                    //array de DataTables
                    List<DataTable> ListaTables = new List<DataTable>();
                    ListaTables.Add(dt);
                    //array de nombre de sheets
                    DateTime localDate = DateTime.Now;
                    string date = localDate.ToString();
                    date = date.Replace("/", "_");
                    date = date.Replace(":", "_");
                    date = date.Replace(".", "_");
                    date = date.Replace(" ", "_");
                    string[] Nombres = new string[] { "Reporte Preventa Ingenieria" };
                    string mensaje = Export.ToPdf("Reporte_Preventa_Ingenieria_" + date, ListaTables, 1, Nombres, Page.Response);
                    if (mensaje != "")
                    {
                        Toast.Error("Error al exportar el reporte a PDF: " + mensaje, this);
                    }
                }

            }
            catch (Exception ex)
            {
                Toast.Error("Error al exportar el reporte a excel: " + ex.Message, this);
            }
        }

        protected void lnkgenerarexcel_Click(object sender, EventArgs e)
        {
            try
            {

                //gridPerformance.ExportSettings.ExportOnlyData = true;
                //gridPerformance.ExportSettings.IgnorePaging = true;
                //gridPerformance.ExportSettings.OpenInNewWindow = true;
                //gridPerformance.ExportSettings.FileName = "Reporte_Performance_Ingenieria";
                //gridPerformance.MasterTableView.ExportToExcel();

                DataTable dt = new DataTable();
                dt.Columns.Add("Login");
                dt.Columns.Add("Ingeniero");
                dt.Columns.Add("Soporte");
                dt.Columns.Add("Preventa");
                dt.Columns.Add("Desarrollo_Negocio");
                dt.Columns.Add("Capacitacion");
                dt.Columns.Add("Administrativas");
                //dt.Columns.Add("Logistica_Traslados");
                dt.Columns.Add("Incapacidad");
                dt.Columns.Add("Uptime");
                dt.Columns.Add("Proyectos_Internos_Facturables");
                dt.Columns.Add("Proyectos_Internos_No_Facturables");
                dt.Columns.Add("Proyectos");
                dt.Columns.Add("Tiempo_Personal");
                dt.Columns.Add("Usabilidad");
                dt.Columns.Add("Total_Horas");
                              


                //dt = ViewState[hdfsessionid.Value + "-dt_reporte"] as DataTable;

                foreach (GridDataItem row in gridPerformance.Items)
                {
                    DataRow dtrow = dt.NewRow();
                    dtrow["Login"] = row["Login"].Text;
                    dtrow["Ingeniero"] = row["Ingeniero"].Text;
                    dtrow["Soporte"] = row["Soporte"].Text;
                    dtrow["Preventa"] = row["Preventa"].Text;
                    dtrow["Desarrollo_Negocio"] = row["Desarrollo_Negocio"].Text;
                    dtrow["Capacitacion"] = row["Capacitacion"].Text;
                    dtrow["Administrativas"] = row["Administrativas"].Text;
                    //dtrow["Logistica_Traslados"] = row["Logistica_Traslados"].Text;
                    dtrow["Incapacidad"] = row["Incapacidad"].Text;
                    dtrow["Uptime"] = row["Uptime"].Text;
                    dtrow["Proyectos_Internos_Facturables"] = row["Proyectos_Internos_Facturables"].Text;
                    dtrow["Proyectos_Internos_No_Facturables"] = row["Proyectos_Internos_No_Facturables"].Text;
                    dtrow["Proyectos"] = row["Proyectos"].Text;
                    dtrow["Tiempo_Personal"] = row["Tiempo_Personal"].Text;
                    dtrow["Usabilidad"] = row["Usabilidad"].Text;
                    dtrow["Total_Horas"] = row["Total_Horas"].Text;
                    dt.Rows.Add(dtrow);
                }


                if (dt.Rows.Count > 0)
                {
                    Export Export = new Export();
                    //array de DataTables
                    List<DataTable> ListaTables = new List<DataTable>();
                    ListaTables.Add(dt);
                    //array de nombre de sheets
                    DateTime localDate = DateTime.Now;
                    string date = localDate.ToString();
                    date = date.Replace("/", "_");
                    date = date.Replace(":", "_");
                    date = date.Replace(".", "_");
                    date = date.Replace(" ", "_");
                    string[] Nombres = new string[] { "Reporte Preventa Ingenieria" };
                    string mensaje = Export.toExcel("Reporte Preventa Ingenieria", XLColor.White, XLColor.Black, 18, true, DateTime.Now.ToString(), XLColor.White,
                                           XLColor.Black, 10, ListaTables, XLColor.CelestialBlue, XLColor.White, Nombres, 1,
                                           "Reporte_Preventa_Ingenieria_" + date + ".xlsx", Page.Response);
                    if (mensaje != "")
                    {
                        Toast.Error("Error al exportar el reporte a excel: " + mensaje, this);
                    }
                }

            }
            catch (Exception ex)
            {
                Toast.Error("Error al exportar el reporte a excel: " + ex.Message, this);
            }
        }

    }
}