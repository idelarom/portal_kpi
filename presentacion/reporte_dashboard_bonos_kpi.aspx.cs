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
    public partial class reporte_dashboard_bonos_kpi : System.Web.UI.Page
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


        [System.Web.Services.WebMethod]
        public static String GetDashboardBonosValues_Individual(string lista_usuarios, string usuario)
        {
            try
            {
                DataTable dt = GetDashboardBonos(null, null, lista_usuarios, usuario);
                foreach (DataColumn column in dt.Columns)
                {
                    column.ColumnName = column.ColumnName.Replace("%", "");
                    column.ColumnName = column.ColumnName.Replace(" ", "_");
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
        public static String GetDashboardbonosValues(int num_empleado, string usuario, string ver_todos_empleados)
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
                        value = GetDashboardBonosValues_Individual(lista_empleados, usuario);
                    }
                }
                else if (dt_list_empleados.Rows.Count > 1)
                {
                    string lista_empleados = dt_list_empleados.Rows[0]["lista_empleados"].ToString();
                    lista_empleados = lista_empleados.Remove(lista_empleados.Length - 1);
                    value = GetDashboardBonosValues_Individual(lista_empleados, usuario);
                }
                return value;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static DataTable GetDashboardBonos(DateTime? fecha_ini, DateTime? fecha_fin, string pLstEmpleados,string Usr)
        {
            DataTable dt = new DataTable();
            try
            {
                DashboardBonosCOM bonos = new DashboardBonosCOM();
                DataSet ds = bonos.Sps_DashBoardReport_Bonos(fecha_ini, fecha_fin, pLstEmpleados,
                "", Usr, 0);
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
            lnkfiltros.Visible = true;
            nkcargandofiltros.Style["display"] = "none";
            ModalShow("#myModal");
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            try
            {
                string pLstEmpleados = CadenaUsuariosFiltro();
                if (txtfechafinal.Text == "" || txtfechainicio.Text == "" && ddltipofiltro.SelectedValue == "2")
                {
                    ModalShow("#myModal");
                    Toast.Error("Ingrese ambas fechas para generar el reporte.", this);
                }
                else if (txtfechafinal.Text == "" || txtfechainicio.Text == "" && ddltipofiltro.SelectedValue == "1")
                {
                    ModalShow("#myModal");
                    Toast.Error("Seleccione un trimestre para generar el reporte.", this);
                }
                else if (pLstEmpleados == "")
                {
                    ModalShow("#myModal");
                    Toast.Error("Seleccione un empleado para generar el reporte.", this);
                }
                else
                {
                    DateTime? fecha_ini = Convert.ToDateTime(txtfechainicio.Text.ToString());
                    DateTime? fecha_fin = Convert.ToDateTime(txtfechafinal.Text.ToString());

                    
                    string Usr = Session["usuario"] as string;
                    DataTable dt = GetDashboardBonos(fecha_ini, fecha_fin, pLstEmpleados, Usr);
                    div_reporte.Visible = false;
                    ViewState[hdfsessionid.Value + "-dt_reporte"] = null;
                    if (dt.Rows.Count > 0)
                    {
                        lblfechaini.Text = Convert.ToDateTime(fecha_ini).ToString("dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX")).ToUpper();
                        lblfechafin.Text = Convert.ToDateTime(fecha_fin).ToString("dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX")).ToUpper();
                        ViewState[hdfsessionid.Value + "-dt_reporte"] = dt;
                        div_reporte.Visible = true;
                        repeater_bonos.DataSource = dt;
                        repeater_bonos.DataBind();
                        ModalClose("#myModal");
                    }
                    else
                    {
                        Toast.Info("El filtro no arrojo ningun resultado, puede intentarlo nuevamente.", "Ningun resultado encontrado", this);
                    }
                }
            }
            catch (Exception ex)
            {
                ModalShow("#myModal");
                Toast.Error("Error al generar el reporte: " + ex.Message, this);
            }
            finally { 
                lnkguardar.Visible = true;
                lnkcargando.Style["display"] = "none";
                div_modalbodyfiltros.Visible = true;
            }
        }

        protected void CargarDatosFiltros(string filtro)
        {
            try
            {
                List<DateTime> list_dates = funciones.RangoFechasTrimestre(DateTime.Now);
                txtfechainicio.Text = list_dates[0].ToString("yyyy-MM-dd");
                txtfechafinal.Text = list_dates[1].ToString("yyyy-MM-dd");
                ddltrimestres.DataValueField = "fecha";
                ddltrimestres.DataTextField = "trimestre";
                DataTable dt = funciones.tabla_trimestres();
                ddltrimestres.DataSource = dt;
                ddltrimestres.DataBind();
                ddltrimestres.Items.Insert(0, new ListItem("--Seleccione un trimestre", "0"));
                ddltrimestres.SelectedValue = list_dates[0].ToString();
                int NumJefe = Convert.ToInt32(Session["NumJefe"]);
                int num_empleado = Convert.ToInt32(Session["num_empleado"]);
                Boolean ver_Todos_los_empleados = Convert.ToBoolean(Session["ver_Todos_los_empleados"]);
                EmpleadosCOM empleados = new EmpleadosCOM();
                bool no_activos = cbxnoactivo.Checked;
                DataSet ds = empleados.sp_listado_empleados(num_empleado, ver_Todos_los_empleados, no_activos);
                DataTable dt_empleados = new DataTable();
                if (filtro != "")
                {
                    DataView dv_empleados = ds.Tables[0].DefaultView;
                    dv_empleados.RowFilter = "nombre like '%"+filtro+"%'";
                    dt_empleados = dv_empleados.ToTable();
                    if (dt_empleados.Rows.Count == 1)
                    {
                        int num_jefe = Convert.ToInt32(ddlempleado_a_consultar.SelectedValue);
                        CargarListadoEmpleado(num_jefe, false);
                    }
                }
                else {
                    dt_empleados = ds.Tables[0];
                }
                ddlempleado_a_consultar.DataValueField = "num_empleado";
                ddlempleado_a_consultar.DataTextField = "nombre";
                ddlempleado_a_consultar.DataSource = dt_empleados;
                ddlempleado_a_consultar.DataBind();
                if (!ver_Todos_los_empleados || dt_empleados.Rows.Count == 1)
                {
                    CargarListadoEmpleado(num_empleado, false);
                }

                ddlempleado_a_consultar.Enabled = ver_Todos_los_empleados;
            }
            catch (Exception ex)
            {
                Toast.Error("Error al iniciar modal de filtros: " + ex.Message, this);
            }
            finally {

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
        protected DataTable dt_reporte_bonos(DataTable dt_original)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Nombre");
                dt.Columns.Add("CC");
                dt.Columns.Add("Monto Bono");
                dt.Columns.Add("KPI Individual");
                dt.Columns.Add("KPI Grupo");
                dt.Columns.Add("% Individual");
                dt.Columns.Add("% Grupal");
                dt.Columns.Add("Bono");
                dt.Columns.Add("% Cump. Compromisos");
                dt.Columns.Add("Total Final");
                dt.Columns.Add("% Total Final");
                foreach (DataRow row in dt_original.Rows)
                {
                    DataRow nrow = dt.NewRow();
                    nrow["Nombre"] = row["nombre"].ToString();
                    nrow["CC"] = row["CC"].ToString();
                    nrow["Monto Bono"] = Convert.ToDecimal(row["amount"]).ToString("C");
                    nrow["KPI Individual"] = Convert.ToDecimal(row["kpiind"]).ToString("P2");
                    nrow["KPI Grupo"] = Convert.ToDecimal(row["kpigroup"]).ToString("P2");
                    nrow["% Individual"] = Convert.ToDecimal(row["porcind"]).ToString("P0");
                    nrow["% Grupal"] = Convert.ToDecimal(row["porcgrupal"]).ToString("P0");
                    nrow["Bono"] = Convert.ToDecimal(row["resultadototal"]).ToString("C");
                    nrow["% Cump. Compromisos"] = (Convert.ToInt32(row["cumplimiento_compromisos"]) * 100).ToString();
                    nrow["Total Final"] = Convert.ToDecimal(row["resultadototal"]).ToString("C");
                    nrow["% Total Final"] = (Convert.ToInt32(row["totalpor100"]) * 100).ToString();
                    dt.Rows.Add(nrow);
                }
                return dt;
            }
            catch (Exception ex)
            {
                Toast.Error("Error al crear tabla: " + ex.Message, this);
                return null;
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
                    string[] Nombres = new string[] { "Reporte Dashboard Bonos" };
                    string mensaje = Export.toExcel("Reporte Dashboard Bonos", XLColor.White, XLColor.Black, 18, true, DateTime.Now.ToString(), XLColor.White,
                                           XLColor.Black, 10, ListaTables, XLColor.CelestialBlue, XLColor.White, Nombres, 1,
                                           "reporte_dashbonos_" + date + ".xlsx", Page.Response);
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
                    string[] Nombres = new string[] { "Reporte Dashboard Bonos" };
                    string mensaje = Export.ToPdf("reporte_dashbonos_" + date, ListaTables, 1, Nombres, Page.Response);
                    if (mensaje != "")
                    {
                        Toast.Error("Error al exportar el reporte a PDF: " + mensaje, this);
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al exportar el reporte a PDF: " + ex.Message, this);
            }
        }

        protected void ddltipofiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtfechafinal.ReadOnly = true;
            txtfechainicio.ReadOnly = false;
            int tipo_filtro = Convert.ToInt32(ddltipofiltro.SelectedValue);
            // 1 por trimestre    2 libre
            ddltrimestres.Visible = false;
            txtfechainicio.Visible = false;
            if (tipo_filtro == 1)
            {
                ddltrimestres.Visible = true;
                List<DateTime> list_dates = funciones.RangoFechasTrimestre(Convert.ToDateTime(txtfechainicio.Text.ToString()));
                txtfechafinal.Text = list_dates[1].ToString("yyyy-MM-dd");
                lblinfotipofiltro.Text = "Permite seleccionar solo por rangos de trimestres.";
            }
            else
            {
                List<DateTime> list_dates = funciones.RangoFechasTrimestre(DateTime.Now);
                txtfechainicio.Text = list_dates[0].ToString("yyyy-MM-dd");
                txtfechafinal.Text = list_dates[1].ToString("yyyy-MM-dd");
                txtfechainicio.Visible = true;
                lblinfotipofiltro.Text = "Permite seleccionar las fechas libremente. Entre mas larga sea la fecha, mayor sera el tiempo de carga.";
            }
            txtfechafinal.ReadOnly = tipo_filtro == 1;
        }

        protected void txtfechainicio_TextChanged(object sender, EventArgs e)
        {
            int tipo_filtro = Convert.ToInt32(ddltipofiltro.SelectedValue);
            // 1 por trimestre    2 libre
            if (tipo_filtro == 1)
            {
                List<DateTime> list_dates = funciones.RangoFechasTrimestre(Convert.ToDateTime(txtfechainicio.Text.ToString()));
                txtfechainicio.Text = list_dates[0].ToString("yyyy-MM-dd");
                txtfechafinal.Text = list_dates[1].ToString("yyyy-MM-dd");
            }
        }

        protected void ddltrimestres_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtfechainicio.Text = "";
                txtfechafinal.Text = "";
                if (ddltrimestres.SelectedValue != "0")
                {
                    txtfechainicio.Text = ddltrimestres.SelectedValue.ToString();
                    txtfechainicio_TextChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar trimestre: " + ex.Message, this);
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

        private String CadenaUsuariosFiltro()
        {
            string cadena = "";
            IList<RadListBoxItem> collection = rdtselecteds.Items;
            foreach (RadListBoxItem node in collection)
            {
                if (node.Value != "")
                {
                    cadena = cadena +  node.Value.ToUpper().Trim() + ",";
                }
            }
            cadena = cadena.Remove(cadena.Length - 1);
            return cadena;
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
            finally {
                imgloadempleado.Style["display"] = "none";
                lblbemp.Style["display"] = "none";
            }
        }

        protected void txtfilterempleado_TextChanged(object sender, EventArgs e)
        {
            lnksearch_Click(null,null);
        }

        protected void btnverempleadodetalles_Click(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/img/users/"));
                string imagen = hdfuserselected.Value.ToUpper() + ".png" ;
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
                lblpuesto.Text = hdfpuesto.Value;
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