using datos;
using negocio.Componentes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class proyectos_evaluacion_riesgos : System.Web.UI.Page
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
                AsyncUpload1.FileUploaded += new Telerik.Web.UI.FileUploadedEventHandler(AsyncUpload1_FileUploaded);
                hdfguid.Value = Guid.NewGuid().ToString();
                CargarInformacionInicial(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"])));
            }
        }

        private void CargarInformacionInicial(int id_proyecto)
        {
            try
            {
                ProyectosCOM proyectos = new ProyectosCOM();
                DataTable dt = proyectos.Select(id_proyecto);
                if (dt.Rows.Count > 0)
                {
                    DataRow proyecto = dt.Rows[0];
                    CargarEvaluaciones(id_proyecto);
                    ProyectosPeriodosCOM periodos = new ProyectosPeriodosCOM();
                    int id_proyecto_periodo = Convert.ToInt32(proyecto["id_proyecto_periodo"]);
                    proyectos_periodos periodo = periodos.proyectos_periodo(id_proyecto_periodo);
                    hdf_dias_periodo.Value = periodo.dias.ToString();
                    ViewState[hdfguid.Value + "id_proyecto_tecnologia"] = proyecto["id_proyecto_tecnologia"].ToString();
                    //hdfid_proyecto.Value = id_proyecto.ToString();
                    //lblproyect.Text = proyecto["proyecto"].ToString();
                    //lblresumen.Text = proyecto["descripcion"].ToString();
                    //lblperiodo.Text = proyecto["periodo"].ToString();
                    //lblestatus.Text = proyecto["estatus"].ToString();
                    //lblempleado.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(proyecto["empleado"].ToString().ToLower());
                }
                CargarCombos();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar información del proyecto. " + ex.Message, this);
            }
        }

        private void CargarEvaluaciones(int id_proyecto)
        {
            try
            {
                ProyectosCOM proyectos = new ProyectosCOM();
                DataTable dt_evaluaciones = proyectos.evaluaciones(id_proyecto);
                repeater_evaluaciones.DataSource = dt_evaluaciones;
                repeater_evaluaciones.DataBind();
                repeater_evaluaciones_details.DataSource = dt_evaluaciones;
                repeater_evaluaciones_details.DataBind();
                if (dt_evaluaciones.Rows.Count > 0)
                {
                    int id_proyecto_evaluacion = ViewState[hdfguid.Value + "id_proyecto_evaluacion"] == null ? Convert.ToInt32(dt_evaluaciones.Rows[0]["id_proyecto_evaluacion"]) :
                        Convert.ToInt32(ViewState[hdfguid.Value + "id_proyecto_evaluacion"]);
                   
                    hdf_id_proyecto_evaluacion.Value = id_proyecto_evaluacion.ToString();
                    MostrarDivPrinciapal(id_proyecto_evaluacion);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar información. " + ex.Message, this);
            }
        }

        /// <summary>
        /// Cargar el historial de riesgos con tecnologia similar
        /// </summary>
        /// <param name="id_proyecto_tecnologia"></param>
        private void CargarRiesgosHistorial(int id_proyecto_tecnologia)
        {
            try
            {
                RiesgosCOM riesgos = new RiesgosCOM();
                DataTable dt_riesgos = riesgos.riesgos_historial(id_proyecto_tecnologia);
                if(dt_riesgos.Rows.Count > 0)
                {
                    repetaer_historial_riesgos.DataSource = dt_riesgos;
                    repetaer_historial_riesgos.DataBind();
                    ScriptManager.RegisterStartupScript(this,GetType(),Guid.NewGuid().ToString(), "Init('#tabla_historial');", true);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar información del historial " + ex.Message, this);
            }
        }

        private void CargarCombos()
        {
            try
            {
                //estatus
                RiesgosEstatusCOM estatus = new RiesgosEstatusCOM();
                DataTable dt_Estatus = estatus.SelectAll();
                ddlestatus_riesgo.DataTextField = "estatus";
                ddlestatus_riesgo.DataValueField = "id_riesgos_estatus";
                ddlestatus_riesgo.DataSource = dt_Estatus;
                ddlestatus_riesgo.DataBind();
                //ddlestatus_riesgo.Items.Insert(0, new ListItem("--Seleccionar--", "0"));

                //probabilidad
                RiesgosProbabilidadCOM probabilidades = new RiesgosProbabilidadCOM();
                DataTable dt_probabilidad = probabilidades.SelectAll();
                ddlprobabilidad.DataTextField = "nombre";
                ddlprobabilidad.DataValueField = "id_riesgo_probabilidad";
                ddlprobabilidad.DataSource = dt_probabilidad;
                ddlprobabilidad.DataBind();
                ddlprobabilidad.Items.Insert(0,new ListItem("--Seleccionar--","0"));

                //impacto costo
                RiesgosImpactoCostosCOM costos = new RiesgosImpactoCostosCOM();
                DataTable dt_costos = costos.SelectAll();
                ddlimpacto_costo.DataTextField = "nombre";
                ddlimpacto_costo.DataValueField = "id_riesgo_impacto_costo";
                ddlimpacto_costo.DataSource = dt_costos;
                ddlimpacto_costo.DataBind();
                ddlimpacto_costo.Items.Insert(0, new ListItem("--Seleccionar--", "0"));

                //impacto tiempo
                RiesgosImpactoTiempoCOM tiempos = new RiesgosImpactoTiempoCOM();
                DataTable dt_tiempos = tiempos.SelectAll();
                ddlimpacto_tiempo.DataTextField = "nombre";
                ddlimpacto_tiempo.DataValueField = "id_riesgo_impacto_tiempo";
                ddlimpacto_tiempo.DataSource = dt_tiempos;
                ddlimpacto_tiempo.DataBind();
                ddlimpacto_tiempo.Items.Insert(0, new ListItem("--Seleccionar--", "0"));

                //estrategias
                RiesgosEstrategiaCOM estrategias = new RiesgosEstrategiaCOM();
                DataTable dt_estrategias = estrategias.SelectAll();
                ddlestrategias.DataTextField = "nombre";
                ddlestrategias.DataValueField = "id_riesgo_estrategia";
                ddlestrategias.DataSource = dt_estrategias;
                ddlestrategias.DataBind();
                ddlestrategias.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar información de combos. " + ex.Message, this);
            }
        }

        /// <summary>
        /// Carga los combos de riesgos, dentro del control repeater
        /// </summary>
        /// <param name="ddlprobabilidad"></param>
        /// <param name="ddlimpacto_costo"></param>
        /// <param name="ddlimpacto_tiempo"></param>
        /// <param name="ddlestrategias"></param>
        private void CargarCombosinRepeater(DropDownList ddlprobabilidad, DropDownList ddlimpacto_costo, DropDownList ddlimpacto_tiempo, DropDownList ddlestrategias)
        {
            try
            {
            //    //estatus
            //    RiesgosEstatusCOM estatus = new RiesgosEstatusCOM();
            //    DataTable dt_Estatus = estatus.SelectAll();
            //    ddlestatus_riesgo.DataTextField = "estatus";
            //    ddlestatus_riesgo.DataValueField = "id_riesgos_estatus";
            //    ddlestatus_riesgo.DataSource = dt_Estatus;
            //    ddlestatus_riesgo.DataBind();
            //    //ddlestatus_riesgo.Items.Insert(0, new ListItem("--Seleccionar--", "0"));

                //probabilidad
                RiesgosProbabilidadCOM probabilidades = new RiesgosProbabilidadCOM();
                DataTable dt_probabilidad = probabilidades.SelectAll();
                ddlprobabilidad.DataTextField = "nombre";
                ddlprobabilidad.DataValueField = "id_riesgo_probabilidad";
                ddlprobabilidad.DataSource = dt_probabilidad;
                ddlprobabilidad.DataBind();
                ddlprobabilidad.Items.Insert(0, new ListItem("--Seleccionar--", "0"));

                //impacto costo
                RiesgosImpactoCostosCOM costos = new RiesgosImpactoCostosCOM();
                DataTable dt_costos = costos.SelectAll();
                ddlimpacto_costo.DataTextField = "nombre";
                ddlimpacto_costo.DataValueField = "id_riesgo_impacto_costo";
                ddlimpacto_costo.DataSource = dt_costos;
                ddlimpacto_costo.DataBind();
                ddlimpacto_costo.Items.Insert(0, new ListItem("--Seleccionar--", "0"));

                //impacto tiempo
                RiesgosImpactoTiempoCOM tiempos = new RiesgosImpactoTiempoCOM();
                DataTable dt_tiempos = tiempos.SelectAll();
                ddlimpacto_tiempo.DataTextField = "nombre";
                ddlimpacto_tiempo.DataValueField = "id_riesgo_impacto_tiempo";
                ddlimpacto_tiempo.DataSource = dt_tiempos;
                ddlimpacto_tiempo.DataBind();
                ddlimpacto_tiempo.Items.Insert(0, new ListItem("--Seleccionar--", "0"));

                //estrategias
                RiesgosEstrategiaCOM estrategias = new RiesgosEstrategiaCOM();
                DataTable dt_estrategias = estrategias.SelectAll();
                ddlestrategias.DataTextField = "nombre";
                ddlestrategias.DataValueField = "id_riesgo_estrategia";
                ddlestrategias.DataSource = dt_estrategias;
                ddlestrategias.DataBind();
                ddlestrategias.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar información de combos del repetidor de datos de riesgos. " + ex.Message, this);
            }
        }

        /// <summary>
        /// Regresa la fecha de la siguiente evaluacion, tomando en cuenta los dias segun el proyecto
        /// </summary>
        /// <param name="id_proyecto"></param>
        /// <param name="dias"></param>
        /// <returns></returns>
        private DateTime fecha_siguiente_evaluacion(int id_proyecto, int dias)
        {
            try
            {
                ProyectosEvaluacionesCOM evaluaciones = new ProyectosEvaluacionesCOM();
                return evaluaciones.fecha_siguiente_Evaluacion(id_proyecto,dias);
            }
            catch (Exception ex)
            {
                Toast.Error("Error al calcular fecha de la siguiente evaluación. " + ex.Message, this);
                return DateTime.Now;
            }
        }

        private String AgregarEvaluacion(int id_proyecto, DateTime fecha_evaluacion)
        {
            try
            {
                ProyectosEvaluacionesCOM evaluaciones = new ProyectosEvaluacionesCOM();
                proyectos_evaluaciones evaluacion = new proyectos_evaluaciones();
                evaluacion.id_proyecto = id_proyecto;
                evaluacion.usuario = Session["usuario"] as string;
                evaluacion.fecha_evaluacion = fecha_evaluacion;
                int id_proyecto_eval = evaluaciones.Agregar(evaluacion);
                return id_proyecto_eval > 0 ? "" : "Hubo un problema al generar una nueva evaluación.";
                
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        protected void lnknuevaevaluacion_Click(object sender, EventArgs e)
        {
            try
            {
                int id_proyecto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
                DateTime fecha_evaluacion = fecha_siguiente_evaluacion(id_proyecto, Convert.ToInt32(hdf_dias_periodo.Value == ""?"0":hdf_dias_periodo.Value));
                string vmensaje = AgregarEvaluacion(id_proyecto, fecha_evaluacion);
                if (vmensaje == "")
                {
                    Toast.Success("Evaluación agregada correctamente.","Mensaje del sistema", this);
                    CargarInformacionInicial(id_proyecto);
                }
                else
                {
                    Toast.Error("Error al generar nueva evaluación: " + vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al generar nueva evaluación. " + ex.Message, this);
            }
        }

        protected void repeater_evaluaciones_details_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            HtmlGenericControl div_no_hay_riesgos = e.Item.FindControl("div_no_hay_riesgos") as HtmlGenericControl;
            RiesgosCOM riesgos = new RiesgosCOM();
            int id_proyecto_evaluacion = Convert.ToInt32(DataBinder.Eval(dbr, "id_proyecto_evaluacion"));
            DataTable dt_riesgos = riesgos.evaluacion_riesgos(id_proyecto_evaluacion);
            div_no_hay_riesgos.Visible = dt_riesgos.Rows.Count == 0;
            Repeater repeater_riesgos = e.Item.FindControl("repeater_riesgos") as Repeater;
            repeater_riesgos.DataSource = dt_riesgos;
            repeater_riesgos.DataBind();

        }

        protected void repeater_riesgos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            DropDownList ddlprobabilidad_rep = e.Item.FindControl("ddlprobabilidad_rep") as DropDownList;
            DropDownList ddlimpacto_costo_rep = e.Item.FindControl("ddlimpacto_costo_rep") as DropDownList;
            DropDownList ddlimpacto_tiempo_rep = e.Item.FindControl("ddlimpacto_tiempo_rep") as DropDownList;
            DropDownList ddlestrategia_rep = e.Item.FindControl("ddlestrategia_rep") as DropDownList;
            CargarCombosinRepeater(ddlprobabilidad_rep, ddlimpacto_costo_rep, ddlimpacto_tiempo_rep, ddlestrategia_rep);
            int id_riesgo = Convert.ToInt32(DataBinder.Eval(dbr, "id_riesgo"));
            int id_riesgo_probabilidad = Convert.ToInt32(DataBinder.Eval(dbr, "id_riesgo_probabilidad"));
            int id_riesgo_impacto_costo = Convert.ToInt32(DataBinder.Eval(dbr, "id_riesgo_impacto_costo"));
            int id_riesgo_impacto_tiempo = Convert.ToInt32(DataBinder.Eval(dbr, "id_riesgo_impacto_tiempo"));
            int id_riesgo_estrategia = Convert.ToInt32(DataBinder.Eval(dbr, "id_riesgo_estrategia"));
            ddlprobabilidad_rep.SelectedValue =
                ddlprobabilidad_rep.Items.FindByValue(id_riesgo_probabilidad.ToString()) != null ? id_riesgo_probabilidad.ToString() : "0";
            ddlimpacto_costo_rep.SelectedValue =
                ddlimpacto_costo_rep.Items.FindByValue(id_riesgo_impacto_costo.ToString()) != null ? id_riesgo_impacto_costo.ToString() : "0";
            ddlimpacto_tiempo_rep.SelectedValue =
                ddlimpacto_tiempo_rep.Items.FindByValue(id_riesgo_impacto_tiempo.ToString()) != null ? id_riesgo_impacto_tiempo.ToString() : "0";
            ddlestrategia_rep.SelectedValue =
                ddlestrategia_rep.Items.FindByValue(id_riesgo_estrategia.ToString()) != null ? id_riesgo_estrategia.ToString() : "0";
            ddlprobabilidad_rep.Attributes["id_riesgo_probabilidad_selected"] =
                ddlprobabilidad_rep.Items.FindByValue(id_riesgo_probabilidad.ToString()) != null ? id_riesgo_probabilidad.ToString() : "0";
            ddlimpacto_costo_rep.Attributes["id_riesgo_impacto_costo_selected"] =
                ddlimpacto_costo_rep.Items.FindByValue(id_riesgo_impacto_costo.ToString()) != null ? id_riesgo_impacto_costo.ToString() : "0";
            ddlimpacto_tiempo_rep.Attributes["id_riesgo_impacto_tiempo_selected"] =
                ddlimpacto_tiempo_rep.Items.FindByValue(id_riesgo_impacto_tiempo.ToString()) != null ? id_riesgo_impacto_tiempo.ToString() : "0";
            ddlestrategia_rep.Attributes["id_riesgo_estrategia_selected"] =
                ddlestrategia_rep.Items.FindByValue(id_riesgo_estrategia.ToString()) != null ? id_riesgo_estrategia.ToString() : "0";
            ddlprobabilidad_rep.Attributes["id_riesgo"] = id_riesgo.ToString();
            ddlimpacto_costo_rep.Attributes["id_riesgo"] = id_riesgo.ToString();
            ddlimpacto_tiempo_rep.Attributes["id_riesgo"] = id_riesgo.ToString();
            ddlestrategia_rep.Attributes["id_riesgo"] = id_riesgo.ToString();
        }

        protected void repeater_riesgos_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            DropDownList ddlprobabilidad_rep = e.Item.FindControl("ddlprobabilidad_rep") as DropDownList;
            DropDownList ddlimpacto_costo_rep = e.Item.FindControl("ddlimpacto_costo_rep") as DropDownList;
            DropDownList ddlimpacto_tiempo_rep = e.Item.FindControl("ddlimpacto_tiempo_rep") as DropDownList;
            DropDownList ddlestrategia_rep = e.Item.FindControl("ddlestrategia_rep") as DropDownList;
            ddlprobabilidad_rep.SelectedIndexChanged += ddlprobabilidad_rep_SelectedIndexChanged;
            ddlimpacto_costo_rep.SelectedIndexChanged += ddlimpacto_costo_rep_SelectedIndexChanged;
            ddlimpacto_tiempo_rep.SelectedIndexChanged += ddlimpacto_tiempo_rep_SelectedIndexChanged;
            ddlestrategia_rep.SelectedIndexChanged += ddlestrategia_rep_SelectedIndexChanged;
        }

        /// <summary>
        /// Genera los porcentaje de los valores de riesgos del modal_riesgos
        /// </summary>
        private void GenerarValoresRiesgos()
        {
            try
            {
                decimal probabilidad = Convert.ToDecimal(txtpprobabilidad.Text == "" ? "0.00" : txtpprobabilidad.Text.Replace(" %", ""));
                decimal impacto_tiempo = Convert.ToDecimal(txtimpacto_tiempo.Text == "" ? "0.00" : txtimpacto_tiempo.Text.Replace(" %", ""));
                decimal impacto_costo = Convert.ToDecimal(txtimpacto_costo.Text == "" ? "0.00" : txtimpacto_costo.Text.Replace(" %", ""));
                if (ddlestatus_riesgo.SelectedValue == "1")
                {
                    txtriesgo_costo.Text = Convert.ToDouble(((probabilidad * impacto_costo)/100)/100).ToString("P");
                    txtriesgo_tiempo.Text = Convert.ToDouble(((probabilidad * impacto_tiempo)/100)/100).ToString("P");
                }
                else {
                    txtriesgo_costo.Text = "0.00 %";
                    txtriesgo_tiempo.Text = "0.00 %";
                }

            }
            catch (Exception ex)
            {
                Toast.Error("Error al calcular riesgos. " + ex.Message, this);
            }
        }

        protected void repeater_evaluaciones_details_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string command = e.CommandName.ToLower();
            hdf_id_proyecto_evaluacion.Value = e.CommandArgument.ToString();
            switch (command)
            {
                case "nuevo_riesgo":
                    Session[hdfguid.Value + "list_actividades"] = null;
                    Session[hdfguid.Value + "list_documentos"] = null;
                    hdf_id_riesgo.Value = "";
                    CargarCombos();
                    txtriesgo.Text = "";
                    txtimpacto_costo.Text = "";
                    txtimpacto_tiempo.Text = "";
                    txtpprobabilidad.Text = "";
                    ModalShow("#modal_riesgo");
                    break;
                case "importar_riesgos":

                    int id_proyecto_tecnologia = Convert.ToInt32(ViewState[hdfguid.Value + "id_proyecto_tecnologia"]);
                    CargarRiesgosHistorial(id_proyecto_tecnologia);
                    ModalShow("#modal_historial");
                    break;
            }
        }

        protected void ddlprobabilidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id_riesgo_probabilidad = Convert.ToInt32(ddlprobabilidad.SelectedValue);
                if (id_riesgo_probabilidad > 0)
                {
                    RiesgosProbabilidadCOM probabilidades = new RiesgosProbabilidadCOM();
                    riesgos_probabilidad probabilidad = probabilidades.impacto(id_riesgo_probabilidad);
                    txtpprobabilidad.Text = probabilidad.porcentaje.ToString();
                    txtpprobabilidad_TextChanged(null,null);
                }
                else
                {
                    txtpprobabilidad.Text = "";
                }
                GenerarValoresRiesgos();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al calcular probabilidad. " + ex.Message, this);
            }
        }

        protected void txtpprobabilidad_TextChanged(object sender, EventArgs e)
        {
            string texto = txtpprobabilidad.Text;
           
            if (texto == "")
            {
                txtpprobabilidad.Text = "";
            }
            else
            {
                decimal value = Convert.ToDecimal(texto) / 100;
                txtpprobabilidad.Text = value.ToString("P");
            }
        }

        protected void txtimpacto_costo_TextChanged(object sender, EventArgs e)
        {
            string texto = txtimpacto_costo.Text;

            if (texto == "")
            {
                txtimpacto_costo.Text = "";
            }
            else
            {
                decimal value = Convert.ToDecimal(texto) / 100;
                txtimpacto_costo.Text = value.ToString("P");
            }

        }

        protected void txtimpacto_tiempo_TextChanged(object sender, EventArgs e)
        {
            string texto = txtimpacto_tiempo.Text;

            if (texto == "")
            {
                txtimpacto_tiempo.Text = "";
            }
            else
            {
                decimal value = Convert.ToDecimal(texto) / 100;
                txtimpacto_tiempo.Text = value.ToString("P");
            }

        }

        protected void ddlimpacto_costo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id_impacto_costo = Convert.ToInt32(ddlimpacto_costo.SelectedValue);
                if (id_impacto_costo > 0)
                {
                    RiesgosImpactoCostosCOM costos = new RiesgosImpactoCostosCOM();
                    riesgos_impacto_costo costo = costos.impacto(id_impacto_costo);
                    txtimpacto_costo.Text = costo.porcentaje.ToString();
                    txtimpacto_costo_TextChanged(null, null);
                }
                else
                {
                    txtimpacto_costo.Text = "";
                }
                GenerarValoresRiesgos();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al calcular impacto costo. " + ex.Message, this);
            }

        }

        protected void ddlimpacto_tiempo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id_impacto_tiempo = Convert.ToInt32(ddlimpacto_tiempo.SelectedValue);
                if (id_impacto_tiempo > 0)
                {
                    RiesgosImpactoTiempoCOM costos = new RiesgosImpactoTiempoCOM();
                    riesgos_impacto_tiempo costo = costos.impacto(id_impacto_tiempo);
                    txtimpacto_tiempo.Text = costo.porcentaje.ToString();
                    txtimpacto_tiempo_TextChanged(null, null);
                }
                else
                {
                    txtimpacto_tiempo.Text = "";
                }
                GenerarValoresRiesgos();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al calcular impacto tiempo. " + ex.Message, this);
            }

        }

        public string GuardarRiesgo(riesgos riesgo,  List<actividades> lst_actividades, List<documentos> lstdocumentos)
        {
            RiesgosCOM riesgos = new RiesgosCOM();
            return riesgos.Agregar(riesgo,lst_actividades,lstdocumentos);
        }
        
        public string EditarRiesgo(riesgos riesgo, List<actividades> lst_actividades, List<documentos> lstdocumentos)
        {
            RiesgosCOM riesgos = new RiesgosCOM();
            return riesgos.Editar(riesgo, lst_actividades, lstdocumentos);
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session[hdfguid.Value + "list_actividades"] == null)
                {
                    List<actividades> actividades = new List<actividades>();
                    Session[hdfguid.Value + "list_actividades"] = actividades;
                }
                if (Session[hdfguid.Value + "list_documentos"] == null)
                {
                    List<documentos> documentos = new List<documentos>();
                    Session[hdfguid.Value + "list_documentos"] = documentos;
                }
                int id_riesgo = hdf_id_riesgo.Value == "" ? 0 : Convert.ToInt32(hdf_id_riesgo.Value);
                string vmensaje = "";
                riesgos riesgo = new riesgos();
                riesgo.riesgo = txtriesgo.Text;
                riesgo.id_proyecto_evaluacion = Convert.ToInt32(hdf_id_proyecto_evaluacion.Value == "" ? "0" : hdf_id_proyecto_evaluacion.Value);
                riesgo.id_riesgos_estatus = Convert.ToInt32(ddlestatus_riesgo.SelectedValue);
                riesgo.id_riesgo_probabilidad = Convert.ToInt32(ddlprobabilidad.SelectedValue);
                riesgo.porc_probabilidad = Convert.ToDecimal(txtpprobabilidad.Text == "" ? "0" : txtpprobabilidad.Text.Replace(" %", ""));
                riesgo.id_riesgo_impacto_costo = Convert.ToInt32(ddlimpacto_costo.SelectedValue);
                riesgo.porc_impcosto = Convert.ToDecimal(txtimpacto_costo.Text == "" ? "0" : txtimpacto_costo.Text.Replace(" %", ""));
                riesgo.id_riesgo_impacto_tiempo = Convert.ToInt32(ddlimpacto_tiempo.SelectedValue);
                riesgo.porc_imptiempo = Convert.ToDecimal(txtimpacto_tiempo.Text == "" ? "0" : txtimpacto_tiempo.Text.Replace(" %", ""));
                riesgo.riesgo_costo = Convert.ToDecimal(txtriesgo_costo.Text == "" ? "0" : txtriesgo_costo.Text.Replace(" %", ""));
                riesgo.riesgo_tiempo = Convert.ToDecimal(txtriesgo_tiempo.Text == "" ? "0" : txtriesgo_tiempo.Text.Replace(" %", ""));
                riesgo.id_riesgo_estrategia = Convert.ToInt32(ddlestrategias.SelectedValue);
                riesgo.usuario = Session["usuario"] as string;
                riesgo.usuario_edicion = Session["usuario"] as string;
                if (id_riesgo > 0) { riesgo.id_riesgo = id_riesgo; }

                List<actividades> lstactividades = Session[hdfguid.Value + "list_actividades"] as List<datos.actividades>;
                List<documentos> lstdocumentos = Session[hdfguid.Value + "list_documentos"] as List<datos.documentos>;
                
                if (riesgo.riesgo == "")
                {
                    Toast.Error("Error al guardar riesgo: Ingrese un nombre para el riesgo.", this);
                }
                else if (riesgo.id_riesgos_estatus == 0)
                {
                    Toast.Error("Error al guardar riesgo: Seleccione un estatus para el riesgo.", this);
                }
                else if (riesgo.id_riesgo_probabilidad == 0)
                {
                    Toast.Error("Error al guardar riesgo: Ingrese una probabilidad para el riesgo.", this);
                }
                else if (riesgo.id_riesgo_impacto_costo == 0)
                {
                    Toast.Error("Error al guardar riesgo: Ingrese un impacto costo para el riesgo.", this);
                }
                else if (riesgo.id_riesgo_impacto_tiempo == 0)
                {
                    Toast.Error("Error al guardar riesgo: Ingrese un impacto tiempo para el riesgo.", this);
                }
                else if (riesgo.id_riesgo_estrategia == 0)
                {
                    Toast.Error("Error al guardar riesgo: Ingrese una estrategia para el riesgo.", this);
                }
                else
                {
                    vmensaje = id_riesgo==0? GuardarRiesgo(riesgo, lstactividades, lstdocumentos): EditarRiesgo(riesgo, lstactividades, lstdocumentos);
                    if (vmensaje == "")
                    {
                        hdfid_riesgo.Value = "";
                        hdf_id_riesgo.Value = "";
                        CargarInformacionInicial(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"])));
                        Toast.Success("Riesgo guardado de manera correcta.", "Mensaje del sistema", this);
                        ModalClose("#modal_riesgo");
                    }
                    else
                    {
                        Toast.Error("Error al guardar riesgo: " + vmensaje, this);
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al guardar riesgo: " + ex.Message, this);
            }
        }

        /// <summary>
        /// Obtiene un el siguiente id de la lista local de actividades
        /// </summary>
        /// <returns></returns>
        public int FindMaxacciones(List<actividades> list)
        {
            int maxAge = 0;
            foreach (actividades type in list)
            {
                if (type.id_actividad > maxAge)
                {
                    maxAge = type.id_actividad;
                }
            }
            return maxAge;
        }

        /// <summary>
        /// Obtiene un el siguiente id de la lista local de documentos
        /// </summary>
        /// <returns></returns>
        public int FindMaxdocumentos(List<documentos> list)
        {
            int maxAge = 0;
            foreach (documentos type in list)
            {
                if (type.id_documento > maxAge)
                {
                    maxAge = type.id_documento;
                }
            }
            return maxAge;
        }

        /// <summary>
        /// Obtiene un el siguiente id de la lista local de actividades
        /// </summary>
        /// <returns></returns>
        public int get_id_actividad()
        {
            List<actividades> lstactividades = Session[hdfguid.Value+"list_actividades"] as List<datos.actividades>;
            
            return FindMaxacciones(lstactividades) + 1;
        }

        public int get_id_documento()
        {
            List<documentos> lstdocumentos = Session[hdfguid.Value+"list_documentos"] as List<datos.documentos>;

            return FindMaxdocumentos(lstdocumentos) + 1;
        }

        /// <summary>
        /// Agrega una actividadd y sus documentos en una lista local
        /// </summary>
        /// <param name="id_actividad"></param>
        /// <param name="id_proyecto"></param>
        /// <param name="id_riesgo"></param>
        /// <param name="nombre"></param>
        /// <param name="usuario_resp"></param>
        /// <param name="empleado_resp"></param>
        /// <param name="fecha_ejecucion"></param>
        /// <param name="fecha_Asignacion"></param>
        /// <param name="path_documento"></param>
        /// <param name="size_documento"></param>
        /// <param name="publico"></param>
        /// <param name="id_documento"></param>
        public void AgregarAccionTemporal(int id_actividad,int id_proyecto, int id_riesgo, string nombre, string usuario_resp, string empleado_resp, DateTime? fecha_ejecucion,
            DateTime? fecha_Asignacion, string path_documento, string size_documento, bool publico, int id_documento)
        {
            try
            {
                if (Session[hdfguid.Value+"list_actividades"] == null)
                {
                    List<actividades> actividades = new List<actividades>();
                    Session[hdfguid.Value+"list_actividades"] = actividades;
                }
                if (Session[hdfguid.Value+"list_documentos"] == null)
                {
                    List<documentos> documentos = new List<documentos>();
                    Session[hdfguid.Value+"list_documentos"] = documentos;
                }
                actividades actividad = new actividades();
                actividad.id_actividad = id_actividad> 0 ? id_actividad: get_id_actividad();
                actividad.id_proyecto = id_proyecto;
                actividad.id_riesgo = id_riesgo;
                actividad.empleado_resp = empleado_resp;
                actividad.nombre = nombre;
                actividad.usuario_resp = usuario_resp;
                actividad.fecha_ejecucion = fecha_ejecucion;
                actividad.fecha_asignacion = fecha_Asignacion;
                actividad.usuario = Session["usuario"] as string;
                actividad.usuario_edicion = Session["usuario"] as string;

                documentos documento = new documentos();
                if (id_documento > 0) { documento.id_documento = id_documento; }
                documento.id_actividad = actividad.id_actividad;
                documento.path = path_documento;
                documento.nombre = Path.GetFileName(funciones.de64aTexto(path_documento));
                documento.tamaño = size_documento;
                documento.publico = publico;
                documento.extension = Path.GetExtension(funciones.de64aTexto(path_documento));
                documento.contentType = funciones.ContentType(documento.extension);
                documento.usuario = Session["usuario"] as string;
                documento.usuario_edicion = Session["usuario"] as string;


                List<actividades> lstactividades = Session[hdfguid.Value+"list_actividades"] as List<datos.actividades>;
                lstactividades.Add(actividad);
                Session[hdfguid.Value+"list_actividades"] = lstactividades;


                List<documentos> lstdocumentos = Session[hdfguid.Value+"list_documentos"] as List<datos.documentos>;
                lstdocumentos.Add(documento);
                Session[hdfguid.Value+"list_documentos"] = lstdocumentos;


            }
            catch (Exception ex)
            {
                Toast.Error("Error al guardar accion local: " + ex.Message, this);
            }
        }

        public void EditarAccionTemporal(int id_actividad,int id_proyecto, int id_riesgo, string nombre, string usuario_resp, DateTime? fecha_ejecucion,
          DateTime? fecha_Asignacion, string path_documento, string size_documento, bool publico, int id_documento)
        {
            try
            {
                List<actividades> lstactividades = Session[hdfguid.Value+"list_actividades"] as List<datos.actividades>;
                List<documentos> lstdocumentos = Session[hdfguid.Value+"list_documentos"] as List<datos.documentos>;
                foreach (actividades actividad in lstactividades)
                {
                    if (actividad.id_actividad == id_actividad)
                    {
                        actividad.id_proyecto = id_proyecto;
                        actividad.id_riesgo = id_riesgo;
                        actividad.nombre = nombre;
                        actividad.usuario_resp = usuario_resp;
                        actividad.fecha_ejecucion = fecha_ejecucion;
                        actividad.fecha_asignacion = fecha_Asignacion;
                        actividad.usuario = Session["usuario"] as string;
                        actividad.usuario_edicion = Session["usuario"] as string;
                    }
                }
                foreach (documentos documento in lstdocumentos)
                {
                    if (documento.id_documento == id_documento)
                    {
                        documento.path = path_documento;
                        documento.nombre = Path.GetFileName(funciones.de64aTexto(path_documento));
                        documento.tamaño = size_documento;
                        documento.publico = publico;
                        documento.extension = Path.GetExtension(funciones.de64aTexto(path_documento));
                        documento.contentType = funciones.ContentType(documento.extension);
                        documento.usuario = Session["usuario"] as string;
                        documento.usuario_edicion = Session["usuario"] as string;
                    }
                }
                Session[hdfguid.Value+"list_actividades"] = lstactividades;
                Session[hdfguid.Value+"list_documentos"] = lstdocumentos;

            }
            catch (Exception ex)
            {
                Toast.Error("Error al editar accion local: " + ex.Message, this);
            }
        }

        /// <summary>
        /// Elimina una actividad y sus documentos de la lista local
        /// </summary>
        /// <param name="id_actividad"></param>
        public void EliminarAccionTemporal(int id_actividad)
        {
            try
            {
                List<actividades> lstactividades = Session[hdfguid.Value+"list_actividades"] as List<datos.actividades>;
                List<documentos> lstdocumentos = Session[hdfguid.Value+"list_documentos"] as List<datos.documentos>;
                foreach (actividades actividad in lstactividades)
                {
                    if (actividad.id_actividad == id_actividad)
                    {
                        lstactividades.Remove(actividad);
                        break;
                    }
                }
                foreach (documentos documento in lstdocumentos)
                {
                    if (documento.id_actividad == id_actividad)
                    {
                        lstdocumentos.Remove(documento);
                        break;
                    }
                }
                Session[hdfguid.Value+"list_actividades"] = lstactividades;
                Session[hdfguid.Value+"list_documentos"] = lstdocumentos;

            }
            catch (Exception ex)
            {
                Toast.Error("Error al editar accion local: " + ex.Message, this);
            }
        }

        protected void CargarDatosFiltros(string filtro)
        {
            try
            {

                int NumJefe = Convert.ToInt32(Session["NumJefe"]);
                int num_empleado = Convert.ToInt32(Session["num_empleado"]);
                Boolean ver_Todos_los_empleados = Convert.ToBoolean(Session["ver_Todos_los_empleados"]);
                EmpleadosCOM empleados = new EmpleadosCOM();
                bool no_activos = false;
                DataSet ds = empleados.sp_listado_empleados(num_empleado, ver_Todos_los_empleados, no_activos);
                DataTable dt_empleados = new DataTable();
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
                ddlempleado_a_consultar.DataValueField = "usuario";
                ddlempleado_a_consultar.DataTextField = "nombre";
                ddlempleado_a_consultar.DataSource = dt_empleados;
                ddlempleado_a_consultar.DataBind();

                ddlempleado_a_consultar.Enabled = ver_Todos_los_empleados;
                div_filtro_empleados.Visible = ver_Todos_los_empleados;
            }
            catch (Exception ex)
            {
                Toast.Error("Error al iniciar modal de filtros: " + ex.Message, this);
            }
        }

        /// <summary>
        /// Carga el grid con las acciones por riesgo
        /// </summary>
        private void CargarGridAcciones()
        {
            try
            {

                List<actividades> lstactividades = Session[hdfguid.Value + "list_actividades"] as List<datos.actividades>;
                grid_acciones.DataSource = lstactividades;
                grid_acciones.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar grid de acciones: " + ex.Message, this);
            }
        }

        protected void lnkacciones_Click(object sender, EventArgs e)
        {
            CargarDatosFiltros(""); CargarGridAcciones();
            txtaccion.Text = "";
            txtfilterempleado.Text = "";
            txtfechaejecuacion.Text = DateTime.Now.ToString("yyyy-MM-dd").Replace(' ', 'T');
            ModalShow("#modal_acciones");
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

        protected void AsyncUpload1_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            try
            {
                int r = AsyncUpload1.UploadedFiles.Count;
                int id_riesgo = Convert.ToInt32(hdfid_riesgo.Value == "" ? "0" : hdfid_riesgo.Value);
                int id_proyecto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
                if (r == 0)
                {
                    Toast.Error("Error al guardar acción: Ingrese seleccione un archivo.", this);
                }
                else if (txtaccion.Text == "")
                {
                    Toast.Error("Error al guardar acción: Ingrese la acción ejecutada.", this);
                }
                else if (txtfechaejecuacion.Text == "")
                {
                    Toast.Error("Error al guardar acción: Ingrese la fecha de ejecución.", this);
                }
                else
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/"));//path localDateTime localDate = DateTime.Now;
                    string path_local = "files/documents/";
                    DateTime localDate = DateTime.Now;
                    string date = localDate.ToString();
                    date = date.Replace("/", "_");
                    date = date.Replace(":", "_");
                    date = date.Replace(" ", "");
                    string name = path_local + Path.GetFileNameWithoutExtension(e.File.FileName) + "_" + date + Path.GetExtension(e.File.FileName);
                    //funciones.UploadFile(fuparchivo, dirInfo.ToString() + name.Trim(), this.Page);
                    e.File.SaveAs(dirInfo.ToString() + name.Trim());
                    int id_actividad = 0;
                    //si existe un id de riesgo, quiere decir que estamos dentro de un modal de edicion, y se agregara directamente el registro a la base de datos.
                    if (id_riesgo > 0)
                    {
                        actividades actividad = new actividades();
                        actividad.id_proyecto = id_proyecto;
                        actividad.id_riesgo = id_riesgo;
                        actividad.nombre = txtaccion.Text;
                        actividad.empleado_resp = ddlempleado_a_consultar.SelectedItem.ToString();
                        actividad.usuario_resp = ddlempleado_a_consultar.SelectedValue;
                        actividad.fecha_ejecucion = Convert.ToDateTime(txtfechaejecuacion.Text);
                        actividad.usuario = Session["usuario"] as string;

                        documentos documento = new documentos();
                        documento.id_actividad = actividad.id_actividad;
                        documento.path = funciones.deTextoa64(name);
                        documento.nombre = Path.GetFileName(funciones.de64aTexto(funciones.deTextoa64(name)));
                        documento.tamaño = e.File.ContentLength.ToString();
                        documento.publico = true;
                        documento.extension = Path.GetExtension(funciones.de64aTexto(funciones.deTextoa64(name)));
                        documento.contentType = funciones.ContentType(documento.extension);
                        documento.fecha = DateTime.Now;
                        documento.usuario = Session["usuario"] as string;
                        List<documentos> lstdocumentos = new List<documentos>();
                        lstdocumentos.Add(documento);
                        ActividadesCOM actividades = new ActividadesCOM();
                        id_actividad = actividades.Agregar(actividad, lstdocumentos);

                    }
                    AgregarAccionTemporal(id_actividad, id_proyecto, id_riesgo, txtaccion.Text, ddlempleado_a_consultar.SelectedValue,
                             ddlempleado_a_consultar.SelectedItem.ToString(), Convert.ToDateTime(txtfechaejecuacion.Text),
                            null, funciones.deTextoa64(name), e.File.ContentLength.ToString(), true, 0);
                    CargarGridAcciones();
                    txtaccion.Text = "";
                    txtfilterempleado.Text = "";
                    txtfechaejecuacion.Text = DateTime.Now.ToString("yyyy-MM-dd").Replace(' ', 'T');
                    CargarDatosFiltros("");
                    Toast.Success("Acción agregada correctamente", "Mensaje del sistema", this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al guardar acción: " + ex.Message, this);
            }
        }

        protected void lnkguardaracciones_Click(object sender, EventArgs e)
        {
            int r = AsyncUpload1.UploadedFiles.Count;
            if (r == 0)
            {
                Toast.Error("Error al guardar acción: Ingrese seleccione un archivo.", this);
            }            
        }

        protected void lnkeliminarparticipante_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = sender as LinkButton;
                int id_riesgo = Convert.ToInt32(hdfid_riesgo.Value == "" ? "0" : hdfid_riesgo.Value);
                int id_actividad = Convert.ToInt32(lnk.CommandArgument);
                string vmensaje = "";
                //si tiene id de riesgo, quiere decir que estamos dentro de un modal de edicion y eliminaremos el registro directo en la bd
                if (id_riesgo > 0)
                {
                    ActividadesCOM actividades = new ActividadesCOM();
                    vmensaje = actividades.Eliminar(id_actividad, Session["usuario"] as string);

                }

                if (vmensaje == "")
                {
                    EliminarAccionTemporal(id_actividad);
                    CargarGridAcciones();
                    Toast.Success("Acción eliminada correctamente", "Mensaje del sistema", this);
                }
                else
                {
                    Toast.Error("Error al eliminar acción: " + vmensaje, this);

                }
              
               
            }
            catch (Exception ex)
            {
                Toast.Error("Error al eliminar acción: " + ex.Message, this);
            }
            finally
            {
                ModalShow("#modal_riesgo");
                ModalShow("#modal_acciones");
            }
        }

        protected void ddlprobabilidad_rep_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl = sender as DropDownList;
                RiesgosCOM riesgos = new RiesgosCOM();
                int id_riesgo_probabilidad = Convert.ToInt32(ddl.SelectedValue);
                int id_riesgo = Convert.ToInt32(ddl.Attributes["id_riesgo"]);
                int id_riesgo_probabilidad_selected = Convert.ToInt32(ddl.Attributes["id_riesgo_probabilidad_selected"]);
                if (id_riesgo_probabilidad > 0 && id_riesgo_probabilidad != id_riesgo_probabilidad_selected)
                {
                    RiesgosProbabilidadCOM probabilidades = new RiesgosProbabilidadCOM();
                    riesgos_probabilidad probabilidad = probabilidades.impacto(id_riesgo_probabilidad);
                    string vmensaje = riesgos.EditarProbabilidad(id_riesgo, id_riesgo_probabilidad, probabilidad.porcentaje, Session["usuario"] as string);
                    if (vmensaje == "")
                    {
                        ddl.Attributes["id_riesgo_probabilidad_selected"] = id_riesgo_probabilidad.ToString();
                        CargarInformacionInicial(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"])));
                    }
                    else
                    {
                        Toast.Error("Error al editar riesgo: " + vmensaje, this);
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al editar probabilidad: " + ex.Message, this);
            }
            finally {
                foreach (RepeaterItem item in repeater_evaluaciones_details.Items)
                {
                    HtmlGenericControl div = item.FindControl("load_cumpli_compromisos") as HtmlGenericControl;
                    div.Style["display"] = "none";
                }
            }
        }

        protected void ddlimpacto_costo_rep_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                DropDownList ddl = sender as DropDownList;
                RiesgosCOM riesgos = new RiesgosCOM();
                int id_riesgo_impacto_costo = Convert.ToInt32(ddl.SelectedValue);
                int id_riesgo = Convert.ToInt32(ddl.Attributes["id_riesgo"]);
                int id_riesgo_impacto_costo_selected = Convert.ToInt32(ddl.Attributes["id_riesgo_impacto_costo_selected"]);
                if (id_riesgo_impacto_costo > 0 && id_riesgo_impacto_costo != id_riesgo_impacto_costo_selected)
                {
                    RiesgosImpactoCostosCOM probabilidades = new RiesgosImpactoCostosCOM();
                    riesgos_impacto_costo probabilidad = probabilidades.impacto(id_riesgo_impacto_costo);
                    string vmensaje = riesgos.EditarImpactoCosto(id_riesgo, id_riesgo_impacto_costo, probabilidad.porcentaje, Session["usuario"] as string);
                    if (vmensaje == "")
                    {
                        ddl.Attributes["id_riesgo_impacto_costo_selected"] = id_riesgo_impacto_costo.ToString();
                        CargarInformacionInicial(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"])));
                    }
                    else
                    {
                        Toast.Error("Error al editar riesgo: " + vmensaje, this);
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al editar impacto costo: " + ex.Message, this);
            }
            finally
            {
                foreach (RepeaterItem item in repeater_evaluaciones_details.Items)
                {
                    HtmlGenericControl div = item.FindControl("load_cumpli_compromisos") as HtmlGenericControl;
                    div.Style["display"] = "none";
                }
            }
        }

        protected void ddlimpacto_tiempo_rep_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl = sender as DropDownList;
                RiesgosCOM riesgos = new RiesgosCOM();
                int id_riesgo_impacto_tiempo = Convert.ToInt32(ddl.SelectedValue);
                int id_riesgo = Convert.ToInt32(ddl.Attributes["id_riesgo"]);
                int id_riesgo_impacto_tiempo_selected = Convert.ToInt32(ddl.Attributes["id_riesgo_impacto_tiempo_selected"]);
                if (id_riesgo_impacto_tiempo > 0 && id_riesgo_impacto_tiempo  != id_riesgo_impacto_tiempo_selected)
                {
                    RiesgosImpactoTiempoCOM probabilidades = new RiesgosImpactoTiempoCOM();
                    riesgos_impacto_tiempo probabilidad = probabilidades.impacto(id_riesgo_impacto_tiempo);
                    string vmensaje = riesgos.EditarImpactoTiempo(id_riesgo, id_riesgo_impacto_tiempo, probabilidad.porcentaje, Session["usuario"] as string);
                    if (vmensaje == "")
                    {
                        ddl.Attributes["id_riesgo_impacto_tiempo_selected"] = id_riesgo_impacto_tiempo.ToString();
                        CargarInformacionInicial(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"])));
                    }
                    else
                    {
                        Toast.Error("Error al editar riesgo: " + vmensaje, this);
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al editar impacto tiempo: " + ex.Message, this);
            }
            finally
            {
                foreach (RepeaterItem item in repeater_evaluaciones_details.Items)
                {
                    HtmlGenericControl div = item.FindControl("load_cumpli_compromisos") as HtmlGenericControl;
                    div.Style["display"] = "none";
                }
            }

        }

        protected void ddlestrategia_rep_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl = sender as DropDownList;
                RiesgosCOM riesgos = new RiesgosCOM();
                int id_estrategia = Convert.ToInt32(ddl.SelectedValue);
                int id_riesgo = Convert.ToInt32(ddl.Attributes["id_riesgo"]);
                int id_riesgo_estrategia_selected = Convert.ToInt32(ddl.Attributes["id_riesgo_estrategia_selected"]);
                if (id_estrategia > 0 && id_estrategia != id_riesgo_estrategia_selected)
                {
                    string vmensaje = riesgos.EditarImpactoEstrategia(id_riesgo, id_estrategia, Session["usuario"] as string);
                    if (vmensaje == "")
                    {
                        ddl.Attributes["id_riesgo_estrategia_selected"] = id_riesgo.ToString();
                        CargarInformacionInicial(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"])));
                    }
                    else
                    {
                        Toast.Error("Error al editar riesgo: " + vmensaje, this);
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al editar estrategia: " + ex.Message, this);
            }
            finally
            {
                foreach (RepeaterItem item in repeater_evaluaciones_details.Items)
                {
                    HtmlGenericControl div = item.FindControl("load_cumpli_compromisos") as HtmlGenericControl;
                    div.Style["display"] = "none";
                }
            }

        }
        
        protected void btneditarriesgo_Click(object sender, EventArgs e)
        {
            try
            {
                int id_riesgo = Convert.ToInt32(hdf_id_riesgo.Value == "" ? "0" : hdf_id_riesgo.Value);
                int command = Convert.ToInt32(hdfcommandgrid.Value == "" ? "1" : hdfcommandgrid.Value);
                if (id_riesgo > 0)
                {
                    RiesgosCOM riesgos = new RiesgosCOM();
                    DataTable dt = riesgos.riesgo(id_riesgo);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow riesgo = dt.Rows[0];
                        txtriesgo.Text = riesgo["riesgo"].ToString();
                        ddlestatus_riesgo.SelectedValue =
                       ddlestatus_riesgo.Items.FindByValue(riesgo["id_riesgos_estatus"].ToString()) != null ? riesgo["id_riesgos_estatus"].ToString() : "0";

                        ddlprobabilidad.SelectedValue =
                         ddlprobabilidad.Items.FindByValue(riesgo["id_riesgo_probabilidad"].ToString()) != null ? riesgo["id_riesgo_probabilidad"].ToString() : "0";
                        ddlimpacto_costo.SelectedValue =
                            ddlimpacto_costo.Items.FindByValue(riesgo["id_riesgo_impacto_costo"].ToString()) != null ? riesgo["id_riesgo_impacto_costo"].ToString() : "0";
                        ddlimpacto_tiempo.SelectedValue =
                            ddlimpacto_tiempo.Items.FindByValue(riesgo["id_riesgo_impacto_tiempo"].ToString()) != null ? riesgo["id_riesgo_impacto_tiempo"].ToString() : "0";
                        ddlestrategias.SelectedValue =
                            ddlestrategias.Items.FindByValue(riesgo["id_riesgo_estrategia"].ToString()) != null ? riesgo["id_riesgo_estrategia"].ToString() : "0";

                        txtpprobabilidad.Text = (Convert.ToDecimal(riesgo["p_probabilidad"]) / 100).ToString("P");
                        txtimpacto_costo.Text = (Convert.ToDecimal(riesgo["p_impacto_costo"]) / 100).ToString("P");
                        txtimpacto_tiempo.Text = (Convert.ToDecimal(riesgo["p_impacto_tiempo"]) / 100).ToString("P");
                        txtriesgo_costo.Text = (Convert.ToDecimal(riesgo["riesgo_costo"]) / 100).ToString("P");
                        txtriesgo_tiempo.Text = (Convert.ToDecimal(riesgo["riesgo_tiempo"]) / 100).ToString("P");


                        Session[hdfguid.Value + "list_actividades"] = null;
                        Session[hdfguid.Value + "list_documentos"] = null;
                        ActividadesCOM actividades = new ActividadesCOM();
                        DataSet ds = actividades.actividades_riesgo(id_riesgo);
                        if (ds != null)
                        {
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                string nombre = row["nombre"].ToString();
                                int id_proyecto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
                                DateTime? fecha_ejecucion = null;
                                if (row["fecha_ejecucion"] != DBNull.Value) {
                                    fecha_ejecucion =Convert.ToDateTime(row["fecha_ejecucion"]);
                                }
                                DateTime? fecha_asignacion = null;
                                if (row["fecha_asignacion"] != DBNull.Value) {
                                    fecha_asignacion = Convert.ToDateTime(row["fecha_asignacion"]);
                                }
                                string usuario_resp = row["usuario_resp"].ToString();
                                string empleado_resp = row["empleado_resp"].ToString();
                                string path = "";
                                string size = "";
                                int id_documento = 0;
                                DataView dv = ds.Tables[1].DefaultView;
                                dv.RowFilter = "id_actividad = " + row["id_actividad"].ToString() + "";
                                DataTable dt_documento = dv.ToTable();
                                if (dt_documento.Rows.Count > 0)
                                {
                                    path = dt_documento.Rows[0]["path"].ToString();
                                    size = dt_documento.Rows[0]["tamaño"].ToString();
                                    id_documento = Convert.ToInt32(dt_documento.Rows[0]["id_documento"]);
                                }
                                AgregarAccionTemporal(Convert.ToInt32(row["id_actividad"]), id_proyecto, id_riesgo, nombre, usuario_resp, 
                                    empleado_resp, fecha_ejecucion, fecha_asignacion, path, size, true, id_documento);
                            }
                        }

                        ModalShow("#modal_riesgo");
                        if (command == 2)
                        {
                            lnkacciones_Click(null,null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar riesgo: " + ex.Message, this);
            }
        }

        protected void lnkdescargararchivo_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = sender as LinkButton;
                byte[] bytes;
                string fileName, contentType;
                int id = Convert.ToInt32(hdfid_actividad.Value == "" ? "0" : hdfid_actividad.Value);
                DocumentosCOM documentos_ = new DocumentosCOM();

                List<documentos> lstdocumentos = Session[hdfguid.Value + "list_documentos"] as List<datos.documentos>;
                documentos documento = null;
                foreach (documentos documento_ in lstdocumentos)
                {
                    if (documento_.id_actividad == id)
                    {
                        documento = documento_;
                    }
                }
                
                if (documento != null)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/"));
                    if (File.Exists(dirInfo.ToString() + funciones.de64aTexto(documento.path)))
                    {
                        Response.ContentType = ContentType;
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + documento.nombre);
                        Response.WriteFile(dirInfo.ToString() + funciones.de64aTexto(documento.path));
                        Response.End();
                        hdfid_actividad.Value = "";
                    }
                    else {

                        Toast.Error("No es encuentra el documento especificado", this);
                    }
                }
                else
                {

                    Toast.Error("Error al descargar documento", this);
                }

            }
            catch (Exception ex)
            {
                Toast.Error("Error al descargar documento: " + ex.Message, this);
            }
            finally {

                ModalShow("#modal_riesgo");
                ModalShow("#modal_acciones");
            }
        }
     
        /// <summary>
        /// Muestra los controles dentro de un repeater relacionados a un id_evaluacion
        /// </summary>
        /// <param name="id_proyecto_evaluacion"></param>
        private void MostrarDivPrinciapal(int id_proyecto_evaluacion)
        {
            foreach (RepeaterItem item in repeater_evaluaciones.Items)
            {
                var li = (HtmlGenericControl)item.FindControl("link_view");
                li.Attributes["class"] = li.Attributes["id_eval"] ==id_proyecto_evaluacion.ToString()?"active":"";
            }
            foreach (RepeaterItem item in repeater_evaluaciones_details.Items)
            {
                Panel div_principal = item.FindControl("div_principal") as Panel;
                div_principal.Visible = div_principal.CssClass == id_proyecto_evaluacion.ToString();
            }
        }

        protected void repeater_evaluaciones_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id_proyecto_evaluacion = Convert.ToInt32(e.CommandArgument);
            hdf_id_proyecto_evaluacion.Value = id_proyecto_evaluacion.ToString();
            MostrarDivPrinciapal(id_proyecto_evaluacion);
            ViewState[hdfguid.Value + "id_proyecto_evaluacion"] = id_proyecto_evaluacion;
        }

        protected void lnkguardarhistorial_Click(object sender, EventArgs e)
        {
            try
            {

                List<riesgos> list_riesgos = new List<riesgos>();
                foreach (RepeaterItem item in repetaer_historial_riesgos.Items)
                {
                    //HUMBERTO 06/10/2017 06:19 pm
                    //AQUI ME QUEDE

                    //INSERTAMOS LOS RIESGOS SELECCIONADOS
                    CheckBox cbx = item.FindControl("cbxseleccionado") as CheckBox;
                    if (cbx.Checked)
                    {
                        int id_riesgo = Convert.ToInt32(cbx.CssClass);
                        riesgos riesgo = new riesgos
                        {
                            id_proyecto_evaluacion = Convert.ToInt32(ViewState[hdfguid.Value + "id_proyecto_evaluacion"]),
                            riesgo = cbx.Attributes["name"].ToString(),
                            usuario= Session["usuario"] as string,
                            fecha_registro = DateTime.Now,
                            id_riesgos_estatus = 1,
                            id_riesgo_probabilidad = 1,
                            porc_probabilidad = 0,
                            id_riesgo_impacto_costo = 1,
                            porc_impcosto = 0,
                            id_riesgo_impacto_tiempo = 1,
                            porc_imptiempo = 0,
                            riesgo_costo = 0,
                            riesgo_tiempo =0,
                            id_riesgo_estrategia =  Convert.ToInt32(cbx.Attributes["id_riesgo_estrategia"])
                            //usuario = entidad.usuario,
                            //fecha_registro = DateTime.Now
                        };
                        list_riesgos.Add(riesgo);
                    }
                }
                if (list_riesgos.Count > 0)
                {

                }
                else
                {
                    Toast.Error("Seleccione al menos un riesgo para importar.", this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al seleccionar riesgo: " + ex.Message, this);
            }
            finally {

                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "Init('#tabla_historial');", true);
            }
        }

       
    }
}