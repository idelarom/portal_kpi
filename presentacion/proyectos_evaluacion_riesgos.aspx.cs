using datos;
using negocio.Componentes;
using System;
using System.Collections.Generic;
using System.Data;
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
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar información. " + ex.Message, this);
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
            //LinkButton btn = (LinkButton)e.Item.FindControl("lnkarchi2");
            //btn.Attributes.Add("idc_usuario", Convert.ToInt32(DataBinder.Eval(dbr, "idc_usuario")).ToString());
            //btn.Attributes.Add("leido", Convert.ToString(DataBinder.Eval(dbr, "leido")));
            //int idc_movimiento = Request.QueryString["idc_tarea_movimiento"] == null ? 0 : Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_tarea_movimiento"]));
            //int idc_movimientoastcual = Convert.ToInt32(DataBinder.Eval(dbr, "idc_tarea_historial"));
            //if (idc_movimiento == idc_movimientoastcual)
            //{
            //    btn.CssClass = "btn btn-default btn-block";
            //}
            //if (Convert.ToBoolean(DataBinder.Eval(dbr, "leido")))
            //{
            //    btn.CssClass = "btn btn-success btn-block";
            //}
        }
        
        protected void repeater_evaluaciones_details_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string command = e.CommandName.ToLower();
            switch (command)
            {
                case "nuevo_riesgo":
                    ModalShow("#modal_riesgo");
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
            else {
                decimal value = Convert.ToDecimal(texto);
               txtpprobabilidad.Text = value.ToString();
            }
        }

    }
}