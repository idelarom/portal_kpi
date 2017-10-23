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
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Linq;
using datos;

namespace presentacion
{
    public partial class reporte_riesgos : System.Web.UI.Page
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
                //hdfsessionid.Value = Guid.NewGuid().ToString();
                hdfguid.Value = Guid.NewGuid().ToString();
                ViewState[hdfsessionid.Value + "-dt_reporte"] = null;
                CargarDatosFiltros();
            }
        }

        private void CargarDatosFiltros()
        {
            rdpfechainicial.SelectedDate = DateTime.Today;
            rdpfechafinal.SelectedDate = DateTime.Today;
        }

        private DataTable Getreporte_proyectos_riesgos(int num_empleado, bool ver_Todos_los_empleados, DateTime fecha_inicio, DateTime fecha_fin)
        {
            DataTable dt = new DataTable();
            try
            {
              RiesgosCOM Riesgos = new RiesgosCOM();
                dt = Riesgos.proyectos_riesgos_reporte(num_empleado, ver_Todos_los_empleados, fecha_inicio, fecha_fin);
            }
            catch (Exception ex)
            {
                dt = new DataTable();
            }
            return dt;
        }

        protected void lnkfiltros_Click(object sender, EventArgs e)
        {
            if (div_reporte.Visible)
            {
                CargarDatosFiltros();
            }
            ModalShow("#myModal");
        }

        private void CargarGridAcciones()
        {
            try
            {
                ActividadesCOM actividad = new ActividadesCOM();
                //List<actividades> lstactividades = Session[hdfid_riesgo.Value + "list_actividades"] as List<datos.actividades>;//Session[hdfguid.Value + "list_actividades"] as List<datos.actividades>;

                //repeater_acciones.DataSource = lstactividades;
                repeater_acciones.DataSource = actividad.actividades_riesgo(Convert.ToInt32(hdfid_riesgo.Value));
                repeater_acciones.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar grid de acciones: " + ex.Message, this);
            }
        }

        private void CargarAccionesHistorial(int id_proyecto)
        {
            try
            {
                ActividadesCOM actividades = new ActividadesCOM();
                DataTable dt_riesgos = actividades.actividades_tecnologia(id_proyecto);
                if (dt_riesgos.Rows.Count > 0)
                {
                    repeter_hisitorial_acciones.DataSource = dt_riesgos;
                    repeter_hisitorial_acciones.DataBind();
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "InitPagging('#tabla_historial');", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "InitPagging('#tabla_historial_acciones');", true);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar información del historial de acciones, " + ex.Message, this);
            }
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fechaInicial = Convert.ToDateTime(string.Format("{0:dd/MM/yyyy HH:mm:ss}", string.Format("{0:dd/MM/yyyy}",rdpfechainicial.SelectedDate) + " 00:00:00"));
                DateTime fechaFinal = Convert.ToDateTime(string.Format("{0:dd/MM/yyyy HH:mm:ss}", string.Format("{0:dd/MM/yyyy}", rdpfechafinal.SelectedDate) + " 23:59:59"));


                if (Convert.ToDateTime(string.Format("{0:dd/MM/yyyy}", fechaInicial)) > Convert.ToDateTime(string.Format("{0:dd/MM/yyyy}", fechaFinal)))
                {
                    ModalShow("#myModal");
                    Toast.Error("La fecha inicial no puede ser mayor a la fecha final seleccionada.", this);
                }
                else if (Convert.ToDateTime(string.Format("{0:dd/MM/yyyy}", fechaFinal)) > Convert.ToDateTime(string.Format("{0:dd/MM/yyyy}", DateTime.Now)))
                {
                    ModalShow("#myModal");
                    Toast.Error("la fecha final no puede ser mayor a la fecha actual", this);
                }
                else
                {
                    int num_empleado = Convert.ToInt32(Session["num_empleado"]);
                    Boolean ver_Todos_los_empleados = Convert.ToBoolean(Session["ver_Todos_los_empleados"]);
                    DataTable dt = Getreporte_proyectos_riesgos(num_empleado, ver_Todos_los_empleados, fechaInicial, fechaFinal);

                    ViewState[hdfsessionid.Value + "-dt_reporte"] = null;
                    if (dt.Rows.Count > 0)
                    {
                        lblfechaini.Text = Convert.ToDateTime(fechaInicial).ToString("dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX")).ToUpper();
                        lblfechafin.Text = Convert.ToDateTime(fechaFinal).ToString("dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX")).ToUpper();
                        ViewState[hdfsessionid.Value + "-dt_reporte"] = dt;
                        repeater_reporte_riesgos.DataSource = dt;
                        repeater_reporte_riesgos.DataBind();
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
                    string[] Nombres = new string[] { "Reporte Dashboard proyectos" };
                    string mensaje = Export.ToPdf("reporte_dashbonos_proyectos_" + date, ListaTables, 1, Nombres, Page.Response);
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
                    string[] Nombres = new string[] { "Reporte Dashboard proyectos" };
                    string mensaje = Export.toExcel("Reporte Dashboard proyectos", XLColor.White, XLColor.Black, 18, true, DateTime.Now.ToString(), XLColor.White,
                                           XLColor.Black, 10, ListaTables, XLColor.CelestialBlue, XLColor.White, Nombres, 1,
                                           "reporte_dashbonos_proyectos_" + date + ".xlsx", Page.Response);
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

        protected void btnacciones_Click(object sender, EventArgs e)
        {
            //CargarDatosFiltros("");
            CargarGridAcciones();
            //txtaccion.Text = "";
            div_nueva_Accion.Visible = true;
            //div_cierre_actividad.Visible = false;
            //txtfilterempleado.Text = "";
            //txtfechaejecuacion.SelectedDate = DateTime.Now;
            ModalShow("#modal_acciones");
        }

        protected void lnkcargarleccionesaprendidas_Click(object sender, EventArgs e)
        {
            try
            {
                CargarAccionesHistorial(Convert.ToInt32(hdfid_proyecto.Value));//(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"])));
                ModalShow("#modal_historial_acciones");
            }
            catch (Exception ex)
            {
                Toast.Error("Error al visualizar resultado: " + ex.Message, this);
            }
            finally
            {
                //InicializarTablas();
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "InitPagging('#tabla_historial');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "InitPagging('#tabla_historial_acciones');", true);
            }
        }
    }
}