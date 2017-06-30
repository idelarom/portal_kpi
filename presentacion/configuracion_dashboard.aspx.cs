using negocio.Componentes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace presentacion
{
    public partial class configuracion_dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarListadoWidgets(Session["usuario"] as string);
            }
        }

        private void CargarListadoWidgets(string usuario)
        {
            try
            {
                ConfiguracionDashboardCOM config = new ConfiguracionDashboardCOM();
                DataSet ds = config.sp_usuario_widgets(usuario);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    rdl_widgets_permitidos.DataTextField = "widget";
                    rdl_widgets_permitidos.DataValueField = "id_widget";
                    rdl_widgets_permitidos.DataSource = dt;
                    rdl_widgets_permitidos.DataBind();
                    DataTable dts = dt.Select("visible = 1").CopyToDataTable().Rows.Count > 0 ? dt.Select("visible = 1").CopyToDataTable() : new DataTable();

                    rdl_widgets_actuales.DataTextField = "widget";
                    rdl_widgets_actuales.DataValueField = "id_widget";
                    rdl_widgets_actuales.DataSource = dts;
                    rdl_widgets_actuales.DataBind();
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar listado: " + ex.Message, this);
            }
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            string cadena_widgets = CadenaWidgets();
            int total_cadena_widgets = TotalCadenaWidgets();
            string usuario = Convert.ToString(Session["usuario"]);
            GuardarConfiguracion(usuario, cadena_widgets, total_cadena_widgets);
        }

        private void GuardarConfiguracion(string usuario, string cadena_widgets, int total_cadena_widgets)
        {
            try
            {
                ConfiguracionDashboardCOM config = new ConfiguracionDashboardCOM();
                DataSet ds = config.sp_guardar_usuarios_config(usuario, cadena_widgets, total_cadena_widgets);
                DataTable dt = ds.Tables[0];
                string vmensaje = (dt.Rows.Count == 0 || !dt.Columns.Contains("mensaje")) ? "Error al guardar con figuración. Intentelo Nuevamente." : dt.Rows[0]["mensaje"].ToString().Trim();
                if (vmensaje == "")
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                  "AlertGO('Configuración Guardada Correctamente', 'configuracion_dashboard.aspx');", true);
                }
                else
                {
                    Toast.Error("Error al guardar configuración: " + vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al guardar configuración: " + ex.Message, this);
            }
        }

        private string CadenaWidgets()
        {
            try
            {
                string cadena = "";
                IList<RadListBoxItem> collection = rdl_widgets_actuales.Items;
                int orden = 0;
                foreach (RadListBoxItem item in collection)
                {
                    orden++;
                    cadena = cadena + item.Value + ";" + orden.ToString() + ";";
                }
                return cadena;
            }
            catch (Exception ex)
            {
                Toast.Error("Error al generar cadena de widgets: " + ex.Message, this);
                return "";
            }
        }

        protected int TotalCadenaWidgets()
        {
            try
            {
                string cadena = "";
                IList<RadListBoxItem> collection = rdl_widgets_actuales.Items;
                return collection.Count;
            }
            catch (Exception ex)
            {
                Toast.Error("Error al generar cadena de widgets: " + ex.Message, this);
                return 0;
            }
        }

        protected void btnview_html_Click(object sender, EventArgs e)
        {
            try
            {
                string id_widget = hdfid_widget.Value.Trim();
                ConfiguracionDashboardCOM config = new ConfiguracionDashboardCOM();
                DataSet ds = config.sp_usuario_widgets(Session["usuario"] as string);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataTable dts = dt.Select("id_widget = " + id_widget + "").CopyToDataTable().Rows.Count > 0 ?
                                        dt.Select("id_widget = " + id_widget + "").CopyToDataTable() : new DataTable();
                    string html = dts.Rows.Count > 0 ? dts.Rows[0]["codigo_html"].ToString() : "";
                    if (html != "")
                    {
                        PlaceHolder1.Controls.Add(new Literal() { Text = html });
                        ModalShow("#myModal");
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al visualizar ejemplo html: " + ex.Message, this);
            }
        }

        private void ModalShow(string modalname)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                             "ModalShow('" + modalname + "');", true);
        }
    }
}