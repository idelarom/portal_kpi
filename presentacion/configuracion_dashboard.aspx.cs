using datos.Model;
using negocio.Componentes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
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
                ViewState["dt_orden"] = null;
                CargarListadoWidgets(Session["usuario"] as string);
                WidgetsCOM widget = new WidgetsCOM();
                //si no existe ninguna configuracion, creamos una nueva para el usuario
                if (!widget.ExistAnyConfig(Session["usuario"] as string))
                {
                    string cadena_widgets = CadenaWidgets2();
                    int total_cadena_widgets = TotalCadenaWidgets2();
                    string usuario = Convert.ToString(Session["usuario"]);
                    GuardarConfiguracion(usuario.ToUpper(), cadena_widgets, total_cadena_widgets);
                }
            }
        }

        /// <summary>
        /// Carga el listado de configuraciones para el usuario
        /// </summary>
        /// <param name="usuario"></param>
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
                    //DataTable dts = dt.Select("visible = 1").CopyToDataTable().Rows.Count > 0 ? dt.Select("visible = 1").CopyToDataTable() : new DataTable();

                    //rdl_widgets_actuales.DataTextField = "widget";
                    //rdl_widgets_actuales.DataValueField = "id_widget";
                    //rdl_widgets_actuales.DataSource = dts;
                    //rdl_widgets_actuales.DataBind();
                    foreach (DataRow row in dt.Rows)
                    {
                        int id_widget = Convert.ToInt32(row["id_Widget"]);
                        int orden = Convert.ToInt32(row["orden"]);
                        bool visible = Convert.ToBoolean(row["visible"]);
                        string widget = row["widget"].ToString();
                        string nombre_codigo = row["nombre_codigo"].ToString();
                        string texto_ayuda = row["texto_ayuda"].ToString();
                        string codigo_html = row["codigo_html"].ToString();
                        bool individual = Convert.ToBoolean(row["individual"]);
                        //agregamos a tabla temporal
                        AddTableOrden(id_widget,visible,orden,widget, codigo_html, nombre_codigo,individual,texto_ayuda);

                    }
                    //cargamos repeates
                    CargarRepeats(false);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar listado: " + ex.Message, this);
            }
        }

        /// <summary>
        /// Agrega un nuevo registro a la tabla de configuraciones locales
        /// </summary>
        /// <param name="id_widget"></param>
        /// <param name="visible"></param>
        /// <param name="orden"></param>
        /// <param name="widget"></param>
        /// <param name="codigo_html"></param>
        /// <param name="nombre_codigo"></param>
        /// <param name="individual"></param>
        /// <param name="texto_ayuda"></param>
        /// <returns></returns>
        private string AddTableOrden(int id_widget, bool visible, int orden, string widget,string codigo_html, string nombre_codigo, bool individual, string texto_ayuda)
        {
            try
            {
                if (ViewState["dt_orden"] == null)
                {
                    DataTable dt_o = new DataTable();
                    dt_o.Columns.Add("id_widget");
                    dt_o.Columns.Add("widget");
                    dt_o.Columns.Add("nombre_codigo");
                    dt_o.Columns.Add("codigo_html");
                    dt_o.Columns.Add("individual", System.Type.GetType("System.Boolean"));
                    dt_o.Columns.Add("texto_ayuda");
                    dt_o.Columns.Add("visible", System.Type.GetType("System.Boolean"));
                    dt_o.Columns.Add("orden");
                    dt_o.Columns.Add("fecha_edicion");
                    ViewState["dt_orden"] = dt_o;
                }

                DataTable dt_orden = ViewState["dt_orden"] as DataTable;
                DataRow row = dt_orden.NewRow();
                row["id_widget"] = id_widget;
                row["widget"] = widget;
                row["codigo_html"] = codigo_html;
                row["nombre_codigo"] = nombre_codigo;
                row["individual"] = individual;
                row["texto_ayuda"] = texto_ayuda;
                row["visible"] = visible;
                row["orden"] = orden;
                row["fecha_edicion"] = DateTime.Now;
                dt_orden.Rows.Add(row);

                ViewState["dt_orden"] = dt_orden;
                return "";
            }
            catch (Exception ex)
            {
                return "Error al editar widget: " + ex.Message;
            }

        }

        /// <summary>
        /// Edita un registro de la tabla de configuraciones locales
        /// </summary>
        /// <param name="id_widget"></param>
        /// <param name="visible"></param>
        /// <param name="orden"></param>
        /// <returns></returns>
        private string EditTableOrden(int id_widget, bool? visible, int? orden)
        {
            try
            {
                if (ViewState["dt_orden"] != null)
                {
                    DataTable dt_orden = ViewState["dt_orden"] as DataTable;
                    foreach (DataRow row in dt_orden.Rows)
                    {
                        int id_widget_ = Convert.ToInt32(row["id_widget"]);
                        if (id_widget == id_widget_)
                        {
                            if (visible != null) { row["visible"] = visible; }
                            if (orden != null) { row["orden"] = orden; }
                            row["fecha_edicion"] = DateTime.Now;
                            break;
                        }
                    }
                    ViewState["dt_orden"] = dt_orden;
                }                
                return "";
            }
            catch (Exception ex)
            {
                return "Error al editar widget: "+ex.Message;
            }
          
        }


        /// <summary>
        /// Carga los repeaters
        /// </summary>
        /// <param name="filtro_adelante"></param>
        private void CargarRepeats(bool filtro_adelante)
        {
            DataTable dt_orden = ViewState["dt_orden"] as DataTable;

            DataTable dt_initial = dt_orden.Copy();
            dt_initial.Rows.Clear();
            //SEPARAMOS LOS WIDGETS INNDIVIDUALES DE LOS GRUPALES
            int individuales = dt_orden.Select("individual = True").Length;
            DataTable dt_indv = individuales > 0 ?
                                  dt_orden.Select("individual = True").CopyToDataTable() : dt_initial;

            int grupales= dt_orden.Select("individual = False").Length;
            DataTable dt_grupal = grupales > 0 ?
                                    dt_orden.Select("individual = False").CopyToDataTable() : dt_initial;
            //ASIGNAMOS LLAVE PRIMARIA PARA HACER MERGE
            dt_grupal.PrimaryKey = new DataColumn[] { dt_grupal.Columns["id_widget"] };
            dt_indv.PrimaryKey = new DataColumn[] { dt_indv.Columns["id_widget"] };

            // CONTAMOS VALORES PARA LLENAR LOS DROPDOWNLIST
            int end = dt_indv.Rows.Count + 1;
            List<int> list = new List<int>();
            for (int i = 1; i < end; i++) { list.Add(i); }
            ViewState["list_count_indv"] = list;

            // ORDENAMOS LAS TABLAS
            DataView dv_indv = new DataView(dt_indv);
            dv_indv.Sort = filtro_adelante ? "orden, fecha_edicion asc" : "orden, fecha_edicion desc";
            DataTable dt_find = dv_indv.ToTable();
            int orden = 1;
            foreach (DataRow row in dt_find.Rows)
            {
                row["orden"] = orden;
                orden++;
            }
            repeat_widgets_ind.DataSource = dt_find;
            repeat_widgets_ind.DataBind();
            // CONTAMOS VALORES PARA LLENAR LOS DROPDOWNLIST
            int end_ = dt_grupal.Rows.Count + 1;
            List<int> list_ = new List<int>();
            for (int i = 1; i < end_; i++) { list_.Add(i); }
            ViewState["list_count"] = list_;
            // ORDENAMOS LAS TABLAS
            DataView dv_g = new DataView(dt_grupal);
            dv_g.Sort = filtro_adelante ? "orden, fecha_edicion asc" : "orden, fecha_edicion desc";
            DataTable dt_fig= dv_g.ToTable();
            orden = 1;
            foreach (DataRow row in dt_fig.Rows)
            {
                row["orden"] = orden;
                orden++;
            }
            repeat_widgets.DataSource = dt_fig;
            repeat_widgets.DataBind();

            //HACEMOS MERGE A LAS TABLAS
            dt_fig.Merge(dt_find);
            ViewState["dt_orden"] = dt_fig;
            DataView dv = dt_fig.DefaultView;
            dv.RowFilter = "visible = True";
            DataTable dt_cadena = dv.ToTable();
            //Guardamos configuracion
            string cadena_widgets = "";
            int total_cadena_widgets = dt_cadena.Rows.Count;
            foreach (DataRow row in dt_cadena.Rows)
            {
                cadena_widgets = cadena_widgets + row["id_widget"].ToString().Trim() + ";" +
                                    row["orden"].ToString().Trim() + ";";
            }
            string usuario = Convert.ToString(Session["usuario"]);
            GuardarConfiguracion(usuario, cadena_widgets, total_cadena_widgets);
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
                  //  System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                  //"AlertGO('Configuración Guardada Correctamente', 'configuracion_dashboard.aspx');", true);
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
        private string CadenaWidgets2()
        {
            try
            {
                string cadena = "";
                IList<RadListBoxItem> collection = rdl_widgets_permitidos.Items;
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

        protected int TotalCadenaWidgets2()
        {
            try
            {
                string cadena = "";
                IList<RadListBoxItem> collection = rdl_widgets_permitidos.Items;
                return collection.Count;
            }
            catch (Exception ex)
            {
                Toast.Error("Error al generar cadena de widgets: " + ex.Message, this);
                return 0;
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

        protected void repeat_widgets_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            int id_widget = Convert.ToInt32(DataBinder.Eval(dbr, "id_widget"));
            bool individual = Convert.ToBoolean(DataBinder.Eval(dbr, "individual"));
            int orden = Convert.ToInt32(e.Item.ItemIndex +1);
            DropDownList ddlorden = (DropDownList)e.Item.FindControl("ddlorden");
            ddlorden.Attributes.Add("id_widget", id_widget.ToString());
            ddlorden.Attributes.Add("orden_anterior", orden.ToString()); 
            ddlorden.DataSource = individual ? ViewState["list_count_indv"] as List<int> : ViewState["list_count"] as List<int>;
            ddlorden.DataBind();
            ddlorden.SelectedValue = orden.ToString();
        }

        protected void ddlorden_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                WidgetsCOM widget = new WidgetsCOM();
                usuarios_widgets entidad = new usuarios_widgets();
                DropDownList ddl = sender as DropDownList;
                int id_widget = Convert.ToInt32(ddl.Attributes["id_widget"]);
                int orden = Convert.ToInt32(ddl.SelectedValue);
                int orden_anterior = Convert.ToInt32(ddl.Attributes["orden_anterior"]);
                string usuario = Session["usuario"] as string;
                entidad.id_widget = id_widget;
                entidad.usuario = usuario.ToUpper();
                entidad.usuario_edicion = usuario;
                entidad.orden = orden;
                string mensaje = EditTableOrden(id_widget,null,orden);
                if (mensaje == "")
                {
                    CargarRepeats(orden_anterior < orden);
                }
                else
                {
                    Toast.Error("Error al modificar widget: " + mensaje, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al modificar widget: " + ex.Message, this);
            }
        }

        protected void lnkactivarwidget_Click(object sender, EventArgs e)
        {
            try
            {
                WidgetsCOM widget = new WidgetsCOM();
                usuarios_widgets entidad = new usuarios_widgets();
                LinkButton lnk = sender as LinkButton;
                lnk.CssClass = lnk.CssClass == "btn btn-primary btn-flat btn-sm pull-left" ? "btn btn-default btn-flat btn-sm pull-left" : "btn btn-primary btn-flat btn-sm pull-left";
                int id_widget = Convert.ToInt32(lnk.CommandArgument);
               
                string usuario = Session["usuario"] as string;
                entidad.id_widget = id_widget;
                entidad.usuario = usuario.ToUpper();
                entidad.usuario_edicion = usuario;
                bool visible = lnk.CssClass == "btn btn-primary btn-flat btn-sm pull-left";
                string mensaje = EditTableOrden(id_widget, visible, null);
                if (mensaje == "")
                {
                    CargarRepeats(false);
                }
                else
                {
                    Toast.Error("Error al modificar widget: " + mensaje, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al modificar widget: " + ex.Message, this);
            }
        }

        protected void lnkbtnmostrarhtml_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = sender as LinkButton;
                string html = lnk.CommandArgument.ToString();
                string title = lnk.CommandName.ToString();
                if (html != "")
                {
                    lblwidgetname.Text = title;
                    PlaceHolder1.Controls.Add(new Literal() { Text = html });
                    ModalShow("#myModal");
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al visualizar ejemplo html: " + ex.Message, this);
            }
        }
    }
}