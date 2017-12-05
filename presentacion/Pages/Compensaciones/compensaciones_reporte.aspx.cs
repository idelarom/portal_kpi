using ClosedXML.Excel;
using negocio.Componentes;
using negocio.Entidades;
using negocio.Componentes.Compensaciones;
using datos;
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

namespace presentacion.Pages.Compensaciones
{
    public partial class compensaciones_reporte : System.Web.UI.Page
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
                string usuario = Session["usuario"] as string;
                hdfguid.Value = Guid.NewGuid().ToString();
                BonosCOM bonos = new BonosCOM();
                DataTable dtValidateUser = bonos.sp_Validate_User(usuario).Tables[0];
                if (dtValidateUser.Rows.Count > 0)
                {
                    Session["id_profile"] = Convert.ToInt32(dtValidateUser.Rows[0]["id_profile"]);
                    Session["employee_number"] = Convert.ToInt32(dtValidateUser.Rows[0]["employee_number"]);
                }
                else
                {
                    Session["id_profile"] = 0;
                    Session["employee_number"] = Convert.ToInt32(dtValidateUser.Rows[0]["employee_number"]);
                }
            } 
        }

        private void Cargar_Tipo_bonos()
        {
            TipoBonosCOM Bonos = new TipoBonosCOM();
            DataTable dt = new DataTable();
            dt = Bonos.SelectAll();
            ddlbonos.DataValueField = "id_bond_type";
            ddlbonos.DataTextField = "name";
            ddlbonos.DataSource = dt;
            ddlbonos.DataBind();
            ddlbonos.Items.Insert(0, "Seleccione un bono");
            ddlbonos.Visible = true;
        }

        private void Cargar_Tipo_Estatus()
        {
            EstatusSolicitudBonosCOM Estatus = new EstatusSolicitudBonosCOM();
            DataTable dt = new DataTable();
            dt = Estatus.SelectAll();
            ddlEstatus.DataValueField = "id_request_status";
            ddlEstatus.DataTextField = "name";
            ddlEstatus.DataSource = dt;
            ddlEstatus.DataBind();
            ddlEstatus.Items.Insert(0, "Seleccione un estatus");
            ddlEstatus.Visible = true;
        }

        protected void CargarDatosFiltros(string filtro)
        {
            try
            {
                DateTime now = DateTime.Now;
                rdpfechainicial.SelectedDate = new DateTime(now.Year, now.Month, 1); // DateTime.Today.AddDays(-7);
                rdpfechafinal.SelectedDate = new DateTime(now.Year, now.Month, 1 + 5) ;// DateTime.Today;
                int NumJefe = Convert.ToInt32(Session["NumJefe"]);
                int num_empleado = Convert.ToInt32(Session["num_empleado"]);
                Boolean ver_Todos_los_empleados = Convert.ToBoolean(Session["ver_Todos_los_empleados"]);
                EmpleadosCOM empleados = new EmpleadosCOM();
                DataTable dt_empleados = new DataTable();
                bool no_activos = false;
                DataSet ds = empleados.sp_listado_empleados(num_empleado, ver_Todos_los_empleados, no_activos);
                if (filtro != "")
                {
                    DataView dv_empleados = ds.Tables[0].DefaultView;
                    dv_empleados.RowFilter = "nombre like '%" + filtro + "%'";
                    dt_empleados = dv_empleados.ToTable();
                    if (dt_empleados.Rows.Count == 1)
                    {
                        //int num_jefe = Convert.ToInt32(ddlempleado_a_consultar.SelectedValue);
                        //CargarListadoEmpleado(num_jefe, false);
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
                ddlempleado_a_consultar.Items.Insert(0, "Seleccione un empleado");
                if (filtro != "")
                {
                    ddlempleado_a_consultar.SelectedValue = dt_empleados.Rows[0]["num_empleado"].ToString();
                }
                    //ddlempleado_a_consultar.Visible = true;

                    //div_filtro_empleados.Visible = true;
                    //if (!ver_Todos_los_empleados)
                    //{
                    //    div_filtro_empleados.Visible = false;
                    //    ddlempleado_a_consultar.Enabled = false;
                    //    CargarListadoEmpleado(num_empleado, false);
                    //    ddlempleado_a_consultar.SelectedValue = num_empleado.ToString();
                    //    lnkagregartodos_Click(null, null);
                    //}

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

        public void CargarTablaArchivos()
        {
            try
            {
                if (Session[hdfguid.Value + "list_documentos"] == null)
                {
                    List<files_requests_bonds> list_fr = new List<files_requests_bonds>();
                    Session[hdfguid.Value + "list_documentos"] = list_fr;
                }
                List<files_requests_bonds> list = Session[hdfguid.Value + "list_documentos"] as List<files_requests_bonds>;
                repeater_archivos.DataSource = list;
                repeater_archivos.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar archivos: " + ex.Message, this);
            }
        }

        protected void lnkfiltros_Click(object sender, EventArgs e)
        {           
            if (div_reporte.Visible)
            {                
                CargarDatosFiltros("");
                Cargar_Tipo_bonos();
                Cargar_Tipo_Estatus();
            }
            else
            {
                CargarDatosFiltros("");
                Cargar_Tipo_bonos();
                Cargar_Tipo_Estatus();
            }
            ModalShow("#ModalBonos");
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
                    string[] Nombres = new string[] { "Reporte de Solicitudes de_Bonos" };
                    string mensaje = Export.ToPdf("Reporte_de_Solicitudes_de_Bonos_" + date, ListaTables, 1, Nombres, Page.Response);
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
                    string[] Nombres = new string[] { "Reporte de Solicitudes de_Bonos" };
                    string mensaje = Export.toExcel("Reporte Preventa Ingenieria", XLColor.White, XLColor.Black, 18, true, DateTime.Now.ToString(), XLColor.White,
                                           XLColor.Black, 10, ListaTables, XLColor.CelestialBlue, XLColor.White, Nombres, 1,
                                           "Reporte_de_Solicitudes_de_Bonos_" + date + ".xlsx", Page.Response);

                    if (mensaje != "")
                    {
                        Toast.Error("Error al exportar el reporte a excel: " + mensaje, this);
                    }
                    //ExporttoExcel(dt);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al exportar el reporte a excel: " + ex.Message, this);
            }

        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            try
            {  
                //string pLstEmpleados = CadenaUsuariosFiltro();
                DateTime fechaInicial = Convert.ToDateTime(rdpfechainicial.SelectedDate);
                DateTime fechaFinal = Convert.ToDateTime(rdpfechafinal.SelectedDate);

                //hdfFechaInicial.Value = fechaInicial.ToString();
                //hdfFechaFinal.Value = fechaFinal.ToString();

                int? id_request_bond;
                int? id_bond_type;
                int? id_request_status;
                int? employee_number;
                string period_date_of;
                string period_date_to;
                //total = 0;

                int id_profile = Convert.ToInt32(Session["id_profile"]); //Convert.ToInt32(hdfid_profile.Value);

                if (this.txtsolicitud.Text.Trim() != String.Empty)
                {
                    id_request_bond = Convert.ToInt32(this.txtsolicitud.Text.Trim());
                }
                else
                {
                   id_request_bond = null;
                }
                if (Convert.ToInt32(ddlbonos.SelectedValue == "Seleccione un bono" ? "0" : ddlbonos.SelectedValue) != 0)
                {
                    id_bond_type = Convert.ToInt32(this.ddlbonos.SelectedValue);
                }
                else
                {
                    id_bond_type = null;
                }
                if (Convert.ToInt32(ddlEstatus.SelectedValue == "Seleccione un estatus" ? "0" : ddlEstatus.SelectedValue) != 0)
                {
                    id_request_status = Convert.ToInt32(this.ddlEstatus.SelectedValue);
                }
                else
                {
                    id_request_status = null;
                }
                if (Convert.ToInt32(ddlempleado_a_consultar.SelectedValue == "Seleccione un empleado" ? "0" : ddlempleado_a_consultar.SelectedValue) != 0)
                {
                    employee_number = Convert.ToInt32(this.ddlempleado_a_consultar.SelectedValue);
                }
                else
                {
                    employee_number = null;
                }
                //if (fechaInicial.ToString()!= String.Empty)
                //{
                //    period_date_of = fechaInicial.ToString("yyyyMMdd");
                //}
                //else
                //{
                //    period_date_of = String.Empty;
                //}
                //if (fechaFinal.ToString() != String.Empty)
                //{
                //    period_date_to = fechaFinal.ToString("yyyyMMdd");
                //}
                //else
                //{
                //    period_date_to = String.Empty;
                //}

                if (fechaInicial > fechaFinal)
                {
                    ModalShow("#ModalBonos");
                    Toast.Error("La fecha inicial no puede ser mayor a la fecha final seleccionada.", this);
                }
                //else if (fechaFinal > DateTime.Now)
                //{
                //    ModalShow("#ModalBonos");
                //    Toast.Error("la fecha final no puede ser mayor a la fecha actual", this);
                //}
                else
                {

                    string Usr = Session["usuario"] as string;
                    int employee_number_Usr = Convert.ToInt32(Session["employee_number"]);
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    BonosCOM Bonos = new BonosCOM();
                    //ds = PerformanceIngenieria.spq_Ingenieros_Performance(fechaInicial, fechaFinal, pLstEmpleados, Usr, 0);

                    if (id_profile == 201 | id_profile == 206)
                    {
                        ds = Bonos.sp_GetRequests_Bonds(id_bond_type, id_request_status, id_request_bond, employee_number, null, fechaInicial, fechaFinal); //period_date_of, period_date_to);
                    }
                    else
                    {//Session["employee_number"]
                        ds = Bonos.sp_GetRequests_Bonds(id_bond_type, id_request_status, id_request_bond, employee_number, employee_number_Usr, fechaInicial, fechaFinal);// period_date_of, period_date_to);
                    }



                    dt = ds.Tables[0];
                    ViewState[hdfsessionid.Value + "-dt_reporte"] = null;
                    if (dt.Rows.Count > 0)
                    {
                        lblfechaini.Text = Convert.ToDateTime(fechaInicial).ToString("dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX")).ToUpper();
                        lblfechafin.Text = Convert.ToDateTime(fechaFinal).ToString("dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX")).ToUpper();
                        ViewState[hdfsessionid.Value + "-dt_reporte"] = dt;
                        repeater_reporte_bonos.DataSource = dt;
                        repeater_reporte_bonos.DataBind();
                        div_reporte.Visible = true;
                        ModalClose("#ModalBonos");
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

        }

        protected void btnviewrequest_Click(object sender, EventArgs e)
        {
            try
            {
                int id_request = Convert.ToInt32(hdfid_request_bond.Value == "" ? "0" : hdfid_request_bond.Value);
                if (id_request > 0)
                {
                    //ClearFields();
                    hdfid_request_bond.Value = id_request.ToString();
                    BonosCOM bonos = new BonosCOM();
                    List<files_requests_bonds> list = new List<files_requests_bonds>();
                    list = bonos.get_files(id_request);
                    Session[hdfguid.Value + "list_documentos"] = list;
                    CargarTablaArchivos();
                    //ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "Init('#table_bonos');", true);
                    ModalShow("#modal_archivos");
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar información del bono: " + ex.Message.ToString(), this);
            }
            finally
            {
                //InitTables();
                //UnBlockUI();
            }
        }

        protected void lnkdescargas_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = hdfpath.Value;
                RutasArchivosCOM rutas = new RutasArchivosCOM();
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/"));//path localDateTime localDate = DateTime.Now;
                string path_local = dirInfo + "files/documents/temp/";
                string server = @rutas.path(1);
                int id_request = Convert.ToInt32(hdfid_request_bond.Value == "" ? "0" : hdfid_request_bond.Value);
                string path = id_request > 0 ? @server + id_request.ToString() + "\\" + filename : path_local + filename;
                if (File.Exists(@path))
                {
                    //Response.ContentType = "doc/docx";
                    //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(@path));
                    //Response.TransmitFile(@path);
                    //Response.End();
                    // Limpiamos la salida
                    Response.Clear();
                    // Con esto le decimos al browser que la salida sera descargable
                    Response.ContentType = "application/octet-stream";
                    // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(@path));
                    // Escribimos el fichero a enviar
                    Response.WriteFile(@path);
                    // volcamos el stream
                    Response.Flush();
                    // Enviamos todo el encabezado ahora
                    Response.End();
                }
                else
                {
                    ModalShow("#modal_archivos");
                    Toast.Error("No es encuentra el documento especificado en la ruta: " + path, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al descargar documento: " + ex.Message, this);
            }
            finally
            {
                //InitTables();
                //UnBlockUI();
            }
        }
    }
}