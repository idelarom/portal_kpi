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
    public partial class reporte_proyectos : System.Web.UI.Page
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
                CargarDatosFiltros();
                //CargarDatosFiltros(""); if (Request.QueryString["filter"] != null)
                //{
                //    lnkfiltros_Click(null, null);
                //    string num_empleado = Convert.ToString(Session["num_empleado"]);
                //    ListItem itwm = ddlempleado_a_consultar.Items.FindByValue(num_empleado);
                //    if (Items != null)
                //    {
                //        ddlempleado_a_consultar.SelectedValue = num_empleado;
                //        ddlempleado_a_consultar_SelectedIndexChanged(null, null);
                //        lnkagregartodos_Click(null, null);
                //    }
                //}
            }
        }

        private void CargarDatosFiltros()
        {
            rdpfechainicial.SelectedDate = DateTime.Today;
            rdpfechafinal.SelectedDate = DateTime.Today;
            Cargarddlstatus();
        }
        private void Cargarddlstatus()
        {
            try
            {
                ProyectosEstatusCOM estatus = new ProyectosEstatusCOM();
                DataTable dt_estatus = new DataTable();
                dt_estatus = estatus.SelectAll();
                ddlstatus.DataValueField = "id_proyecto_estatus";
                ddlstatus.DataTextField = "estatus";               
                ddlstatus.DataSource = dt_estatus;                
                ddlstatus.DataBind();
                ddlstatus.Items.Insert(0, new ListItem("Todos", "0"));
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar combo de estatus : " + ex.Message, this);
            }
        }

        private DataTable Getreporte_proyectos(int num_empleado, bool ver_Todos_los_empleados, int id_proyecto_estatus, DateTime fecha_inicio, DateTime fecha_fin)
        {
            DataTable dt = new DataTable();
            try
            {                
                ProyectosCOM Proyectos = new ProyectosCOM();
                dt = Proyectos.reporte_proyectos(num_empleado, ver_Todos_los_empleados, id_proyecto_estatus, fecha_inicio, fecha_fin);
            }
            catch (Exception ex)
            {
                dt = new DataTable();
            }
            return dt;
        }

        private void Cargarreporte_proyectos(int num_empleado, bool ver_Todos_los_empleados, int id_proyecto_estatus, DateTime fecha_inicio, DateTime fecha_fin)
        {
            try
            {
                DataTable dt = Getreporte_proyectos(num_empleado, ver_Todos_los_empleados, id_proyecto_estatus, fecha_inicio, fecha_fin);
                repeater_reporte_proyectos.DataSource = dt;
                repeater_reporte_proyectos.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar proyectos: " + ex.Message, this);
            }
        }

        protected void lnkfiltros_Click(object sender, EventArgs e)
        {
            if (div_reporte.Visible)
            {
                CargarDatosFiltros();
            }
            ModalShow("#myModal");
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fechaInicial = Convert.ToDateTime(rdpfechainicial.SelectedDate);
                DateTime fechaFinal = Convert.ToDateTime(rdpfechafinal.SelectedDate);

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
                else
                {                    
                    int num_empleado = Convert.ToInt32(Session["num_empleado"]);
                    Boolean ver_Todos_los_empleados = Convert.ToBoolean(Session["ver_Todos_los_empleados"]);
                    DataTable dt = Getreporte_proyectos(num_empleado, ver_Todos_los_empleados, Convert.ToInt32(ddlstatus.SelectedValue), fechaInicial, fechaFinal);
                    
                    ViewState[hdfsessionid.Value + "-dt_reporte"] = null;
                    if (dt.Rows.Count > 0)
                    {
                        lblfechaini.Text = Convert.ToDateTime(fechaInicial).ToString("dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX")).ToUpper();
                        lblfechafin.Text = Convert.ToDateTime(fechaFinal).ToString("dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX")).ToUpper();
                        ViewState[hdfsessionid.Value + "-dt_reporte"] = dt;
                        repeater_reporte_proyectos.DataSource = dt;
                        repeater_reporte_proyectos.DataBind();
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
    }
}