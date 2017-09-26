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
using System.Reflection;
using System.Web;

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
                rdpfechainicial.SelectedDate = DateTime.Today;
                rdpfechafinal.SelectedDate = DateTime.Today;
                hdfsessionid.Value = Guid.NewGuid().ToString();
                ViewState[hdfsessionid.Value + "-dt_reporte"] = null;
                CargarDatosFiltros(""); if (Request.QueryString["filter"] != null)
                {
                    lnkfiltros_Click(null, null);
                    string num_empleado = Convert.ToString(Session["num_empleado"]);
                    ListItem itwm = ddlempleado_a_consultar.Items.FindByValue(num_empleado);
                    if (Items != null)
                    {
                        ddlempleado_a_consultar.SelectedValue = num_empleado;
                        ddlempleado_a_consultar_SelectedIndexChanged(null, null);
                        lnkagregartodos_Click(null, null);
                    }
                }
            }
        }

        protected void CargarDatosFiltros(string filtro)
        {
            try
            {
                rdpfechainicial.SelectedDate = DateTime.Today.AddDays(-7);
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
                    if (dt_empleados.Rows.Count==1)
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

                div_filtro_empleados.Visible = true;
                if (!ver_Todos_los_empleados)
                {
                    div_filtro_empleados.Visible = false;
                    ddlempleado_a_consultar.Enabled = false;
                    CargarListadoEmpleado(num_empleado, false);
                    ddlempleado_a_consultar.SelectedValue = num_empleado.ToString();
                    lnkagregartodos_Click(null, null);
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

        [System.Web.Services.WebMethod]
        public static String GetPerformanceIngenieria_Individual(string lista_usuarios, string usuario)
        {
            try
            {
                DataTable dt = GetPerformanceIngenieria(null, null, lista_usuarios, usuario);
                foreach (DataColumn column in dt.Columns)
                {
                    column.ColumnName = column.ColumnName.Replace("%", "_");
                    //.ColumnName = column.ColumnName.Replace(" ", "_");
                }
                string value = JsonConvert.SerializeObject(dt);
                return value;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        [System.Web.Services.WebMethod]
        public static String GetPerformanceIngenieriaValues(int num_empleado, string usuario, string ver_todos_empleados)
        {
            try
            {
                EmpleadosCOM empleados = new EmpleadosCOM();
                bool ver_Todos = Convert.ToBoolean(ver_todos_empleados);
                DataSet ds = empleados.sp_listado_empleados(num_empleado, false, false);
                DataTable dt_list_empleados = ds.Tables[1];
                string value = JsonConvert.SerializeObject("");
                if (dt_list_empleados.Rows.Count == 1)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    string userinrow = row["usuario"].ToString().Trim().ToUpper();
                    if (userinrow != usuario)
                    {
                        string lista_empleados = dt_list_empleados.Rows[0]["lista_empleados"].ToString();
                        lista_empleados = lista_empleados.Remove(lista_empleados.Length - 1);
                        value = GetPerformanceIngenieria_Individual(lista_empleados, usuario);
                    }
                }
                else if (dt_list_empleados.Rows.Count > 1)
                {
                    string lista_empleados = dt_list_empleados.Rows[0]["lista_empleados"].ToString();
                    lista_empleados = lista_empleados.Remove(lista_empleados.Length - 1);
                    value = GetPerformanceIngenieria_Individual(lista_empleados, usuario);
                }
                return value;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static DataTable GetPerformanceIngenieria(DateTime? fecha_ini, DateTime? fecha_fin, string pLstEmpleados, string Usr)
        {
            DataTable dt = new DataTable();
            try
            {
                PerformanceIngenieriaCOM PerformanceIngenieria = new PerformanceIngenieriaCOM();
                DataSet ds = PerformanceIngenieria.spq_Ingenieros_Performance(fecha_ini, fecha_fin, pLstEmpleados, Usr,1);
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
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
                DateTime fechaInicial = Convert.ToDateTime(rdpfechainicial.SelectedDate);
                DateTime fechaFinal = Convert.ToDateTime(rdpfechafinal.SelectedDate);

                hdfFechaInicial.Value = fechaInicial.ToString();
                hdfFechaFinal.Value = fechaFinal.ToString();

                if (fechaInicial > fechaFinal)
                {
                    ModalShow("#myModal");
                    Toast.Error("La fecha inicial no puede ser mayor a la fecha final seleccionada.", this);
                }
                else if (fechaFinal > DateTime.Now)
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
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    PerformanceIngenieriaCOM PerformanceIngenieria = new PerformanceIngenieriaCOM();
                    ds = PerformanceIngenieria.spq_Ingenieros_Performance(fechaInicial, fechaFinal, pLstEmpleados, Usr,0);
                    dt = ds.Tables[0];
                    ViewState[hdfsessionid.Value + "-dt_reporte"] = null;
                    if (dt.Rows.Count > 0)
                    {
                        lblfechaini.Text = Convert.ToDateTime(fechaInicial).ToString("dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX")).ToUpper();
                        lblfechafin.Text = Convert.ToDateTime(fechaFinal).ToString("dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX")).ToUpper();
                        ViewState[hdfsessionid.Value + "-dt_reporte"] = dt;
                        gridPerformance.DataSource = dt;
                        gridPerformance.DataBind();
                        div_reporte.Visible = true;
                        ModalClose("#myModal");
                    }
                    else
                    {
                        ModalShow("#myModal");
                        Toast.Info("El filtro no arrojo ningun resultado, puede intentarlo nuevamente.", "Ningun resultado encontrado", this);
                    }
                }
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

            DateTime fechaInicial = Convert.ToDateTime(hdfFechaInicial.Value);
            DateTime fechaFinal = Convert.ToDateTime(hdfFechaFinal.Value);
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
            try
            {
                DataTable dt = new DataTable();
                dt = ViewState[hdfsessionid.Value + "-dt_reporte"] as DataTable;
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

                DataTable dt = new DataTable();
                dt = ViewState[hdfsessionid.Value + "-dt_reporte"] as DataTable;
                if (dt.Rows.Count > 0)
                {
                    //Export Export = new Export();
                    ////array de DataTables
                    //List<DataTable> ListaTables = new List<DataTable>();
                    //ListaTables.Add(dt);
                    ////array de nombre de sheets
                    //DateTime localDate = DateTime.Now;
                    //string date = localDate.ToString();
                    //date = date.Replace("/", "_");
                    //date = date.Replace(":", "_");
                    //date = date.Replace(".", "_");
                    //date = date.Replace(" ", "_");
                    //string[] Nombres = new string[] { "Reporte Preventa Ingenieria" };
                    //string mensaje = Export.toExcel("Reporte Preventa Ingenieria", XLColor.White, XLColor.Black, 18, true, DateTime.Now.ToString(), XLColor.White,
                    //                       XLColor.Black, 10, ListaTables, XLColor.CelestialBlue, XLColor.White, Nombres, 1,
                    //                       "Reporte_Preventa_Ingenieria_" + date + ".xlsx", Page.Response);

                    //if (mensaje != "")
                    //{
                    //    Toast.Error("Error al exportar el reporte a excel: " + mensaje, this);
                    //}
                    ExporttoExcel(dt);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al exportar el reporte a excel: " + ex.Message, this);
            }
        }

        private void ExporttoExcel(DataTable table)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            //HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");
            DateTime localDate = DateTime.Now;
            string date = localDate.ToString();
            date = date.Replace("/", "_");
            date = date.Replace(":", "_");
            date = date.Replace(".", "_");
            date = date.Replace(" ", "_");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reporte_Preventa_Ingenieria_" + date + ".xls");
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
              "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
              "style='font-size:10.0pt; font-family:Calibri; background:CelestialBlue;'> <TR>");
            //am getting my grid's column headers
            int columnscount = table.Columns.Count;

            for (int j = 0; j < columnscount; j++)
            {      //write in new column
                HttpContext.Current.Response.Write("<Td>");
                //Get column headers  and make it as bold in excel columns
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(table.Columns[j].ToString());
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");
            }
            HttpContext.Current.Response.Write("</TR>");
            foreach (DataRow row in table.Rows)
            {//write in new row
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        protected void btnverempleadodetalles_Click(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/img/users/"));
                string imagen = hdfuserselected.Value.ToUpper() + ".png";
                if (imagen != "" && File.Exists(dirInfo.ToString().Trim() + imagen))
                {
                    DateTime localDate = DateTime.Now;
                    string date = localDate.ToString();
                    date = date.Replace("/", "_");
                    date = date.Replace(":", "_");
                    date = date.Replace(" ", "");
                    img_employee.ImageUrl = "~/img/users/" + imagen + "?date=" + date;
                }
                else
                {
                    imagen = "user.png";
                    DateTime localDate = DateTime.Now;
                    string date = localDate.ToString();
                    date = date.Replace("/", "_");
                    date = date.Replace(":", "_");
                    date = date.Replace(" ", "");
                    img_employee.ImageUrl = "~/img/" + imagen + "?date=" + date;
                }
                lblnombre.Text = hdfnombre.Value;
                lblpuesto.Text = hdfusr.Value;
                lblprev.Text = hdfpreventa.Value;
                lblimple.Text = hdfimplementacion.Value;
                lblsopo.Text = hdfsoporte.Value;
                lblcompro.Text = hdfocompro.Value;
                ModalShow("#ModalEmpleado");
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }


    }
}