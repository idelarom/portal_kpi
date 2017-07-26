using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio.Componentes;
using System.Data;
using System.IO;
using System.Text;
using ClosedXML.Excel;

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
            }
        }

        public static DataTable GetDashboardBonos(DateTime? fecha_ini, DateTime? fecha_fin, string pLstEmpleados,
            string lstCC, string Usr, int pidgrupo)
        {
            DashboardBonosCOM bonos = new DashboardBonosCOM();
            DataSet ds = bonos.Sps_DashBoardReport_Bonos(fecha_ini, fecha_fin, pLstEmpleados,
            lstCC, Usr, pidgrupo);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            return dt;
        }

        protected void lnkfiltros_Click(object sender, EventArgs e)
        {
            List<DateTime> list_dates = funciones.RangoFechasTrimestre(DateTime.Now);
            txtfechainicio.Text = list_dates[0].ToString("yyyy-MM-dd");
            txtfechafinal.Text = list_dates[1].ToString("yyyy-MM-dd");
            ModalShow("#myModal");
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime? fecha_ini = null;
                DateTime? fecha_fin = null;
                string pLstEmpleados = "CGALVEZR,CBONILLR,OORTIZO,MCRUZM,EHERNANG,LLOZANOS,EDIAZB,CGARCIAV,DRAMIREL ";
                string lstCC = "4176,4177";
                string Usr = Session["usuario"] as string;
                int pidgrupo = 1;
                DataTable dt = GetDashboardBonos(fecha_ini, fecha_fin, pLstEmpleados, lstCC, Usr, pidgrupo);
                div_reporte.Visible = false;
                ViewState[hdfsessionid.Value + "-dt_reporte"] = null;
                if (dt.Rows.Count > 0)
                {
                    ViewState[hdfsessionid.Value + "-dt_reporte"] = dt_reporte_bonos(dt);
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
            catch (Exception ex)
            {
                Toast.Error("Error al generar el reporte: " + ex.Message, this);
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
                                           "reporte_dashbonos_"+date+".xlsx", Page.Response);
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
                    string mensaje = Export.ToPdf("reporte_dashbonos_"+date, ListaTables, 1, Nombres, Page.Response);
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
            if (tipo_filtro == 1)
            {
                List<DateTime> list_dates = funciones.RangoFechasTrimestre(Convert.ToDateTime(txtfechainicio.Text.ToString()));
                txtfechainicio.Text = list_dates[0].ToString("yyyy-MM-dd");
                txtfechafinal.Text = list_dates[1].ToString("yyyy-MM-dd");
                lblinfotipofiltro.Text = "Permite seleccionar solo por rangos de trimestres.";
            }
            else
            {
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
    }
}