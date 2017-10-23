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
                    //ViewState[hdfguid.Value + "id_proyecto_tecnologia"] = proyecto["id_proyecto_tecnologia"].ToString();
                    lblproyect.Text = proyecto["proyecto"].ToString();
                    lblperiodo.Text = proyecto["periodo"].ToString();
                }
                CargarCombos();

            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar información del proyecto. " + ex.Message, this);
            }
        }

        /// <summary>
        /// Indica si el proyecto fue terminado
        /// </summary>
        /// <returns></returns>
        private bool ProyectoTerminado()
        {
            try
            {
                ProyectosCOM proyectos = new ProyectosCOM();
                bool terminado = proyectos.ProyectoTerminado(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"])));
                return terminado;
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar información del cierre de proyecto: " + ex.Message, this);
                return false;
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
        private void CargarRiesgosHistorial(int id_proyecto)
        {
            try
            {
                RiesgosCOM riesgos = new RiesgosCOM();
                DataTable dt_riesgos = riesgos.riesgos_historial(id_proyecto);
                if(dt_riesgos.Rows.Count > 0)
                {
                    repetaer_historial_riesgos.DataSource = dt_riesgos;
                    repetaer_historial_riesgos.DataBind();
                    ScriptManager.RegisterStartupScript(this,GetType(),Guid.NewGuid().ToString(), "InitPagging('#tabla_historial');", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "InitPagging('#tabla_historial_acciones');", true);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar información del historial " + ex.Message, this);
            }
        }

        /// <summary>
        /// Cargar el historial de acciones con tecnologia similar
        /// </summary>
        /// <param name="id_proyecto_tecnologia"></param>
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
                ddlimpacto_costo.DataValueField = "id_riesgo_impacto";
                ddlimpacto_costo.DataSource = dt_costos;
                ddlimpacto_costo.DataBind();
                ddlimpacto_costo.Items.Insert(0, new ListItem("--Seleccionar--", "0"));

                //tipos_actividades
                ActividadesTiposCOM tipos = new ActividadesTiposCOM();
                DataTable dt_tipos = tipos.SelectAll();
                ddltipo_actividad.DataTextField = "tipo";
                ddltipo_actividad.DataValueField = "id_actividad_tipo";
                ddltipo_actividad.DataSource = dt_tipos;
                ddltipo_actividad.DataBind();
                ddltipo_actividad.Items.Insert(0, new ListItem("--Seleccionar--", "0"));

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
        private void CargarCombosinRepeater(DropDownList ddlprobabilidad, DropDownList ddlimpacto_costo)
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
                ddlimpacto_costo.DataValueField = "id_riesgo_impacto";
                ddlimpacto_costo.DataSource = dt_costos;
                ddlimpacto_costo.DataBind();
                ddlimpacto_costo.Items.Insert(0, new ListItem("--Seleccionar--", "0"));

               
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
                if (!ProyectoTerminado())
                {
                    int id_proyecto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
                    DateTime fecha_evaluacion = fecha_siguiente_evaluacion(id_proyecto, Convert.ToInt32(hdf_dias_periodo.Value == "" ? "0" : hdf_dias_periodo.Value));
                    string vmensaje = AgregarEvaluacion(id_proyecto, fecha_evaluacion);
                    if (vmensaje == "")
                    {
                        Toast.Success("Evaluación agregada correctamente.", "Mensaje del sistema", this);
                        CargarInformacionInicial(id_proyecto);
                    }
                    else
                    {
                        Toast.Error("Error al generar nueva evaluación: " + vmensaje, this);
                    }

                }
                else
                {
                    Toast.Error("El proyecto ya fue cerrado y no puede generarse ninguna información adicional.", this);

                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al generar nueva evaluación. " + ex.Message, this);
            }
            finally
            {
                InicializarTablas();
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
            //DropDownList ddlestrategia_rep = e.Item.FindControl("ddlestrategia_rep") as DropDownList;
            CargarCombosinRepeater(ddlprobabilidad_rep, ddlimpacto_costo_rep);
            int id_riesgo = Convert.ToInt32(DataBinder.Eval(dbr, "id_riesgo"));
            int id_riesgo_probabilidad = Convert.ToInt32(DataBinder.Eval(dbr, "id_riesgo_probabilidad"));
            int id_riesgo_impacto_costo = Convert.ToInt32(DataBinder.Eval(dbr, "id_riesgo_impacto"));
            int id_riesgo_estrategia = Convert.ToInt32(DataBinder.Eval(dbr, "id_riesgo_estrategia"));
            ddlprobabilidad_rep.SelectedValue =
                ddlprobabilidad_rep.Items.FindByValue(id_riesgo_probabilidad.ToString()) != null ? id_riesgo_probabilidad.ToString() : "0";
            ddlimpacto_costo_rep.SelectedValue =
                ddlimpacto_costo_rep.Items.FindByValue(id_riesgo_impacto_costo.ToString()) != null ? id_riesgo_impacto_costo.ToString() : "0";
            
            //ddlestrategia_rep.SelectedValue =
            //    ddlestrategia_rep.Items.FindByValue(id_riesgo_estrategia.ToString()) != null ? id_riesgo_estrategia.ToString() : "0";
            ddlprobabilidad_rep.Attributes["id_riesgo_probabilidad_selected"] =
                ddlprobabilidad_rep.Items.FindByValue(id_riesgo_probabilidad.ToString()) != null ? id_riesgo_probabilidad.ToString() : "0";
            ddlimpacto_costo_rep.Attributes["id_riesgo_impacto_costo_selected"] =
                ddlimpacto_costo_rep.Items.FindByValue(id_riesgo_impacto_costo.ToString()) != null ? id_riesgo_impacto_costo.ToString() : "0";
           
            //ddlestrategia_rep.Attributes["id_riesgo_estrategia_selected"] =
            //    ddlestrategia_rep.Items.FindByValue(id_riesgo_estrategia.ToString()) != null ? id_riesgo_estrategia.ToString() : "0";
            ddlprobabilidad_rep.Attributes["id_riesgo"] = id_riesgo.ToString();
            ddlimpacto_costo_rep.Attributes["id_riesgo"] = id_riesgo.ToString();          
            //ddlestrategia_rep.Attributes["id_riesgo"] = id_riesgo.ToString();
        }

        protected void repeater_riesgos_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            DropDownList ddlprobabilidad_rep = e.Item.FindControl("ddlprobabilidad_rep") as DropDownList;
            DropDownList ddlimpacto_costo_rep = e.Item.FindControl("ddlimpacto_costo_rep") as DropDownList;
            ddlprobabilidad_rep.SelectedIndexChanged += ddlprobabilidad_rep_SelectedIndexChanged;
            ddlimpacto_costo_rep.SelectedIndexChanged += ddlimpacto_costo_rep_SelectedIndexChanged;
        }


        protected void repeater_evaluaciones_details_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string command = e.CommandName.ToLower();
            hdf_id_proyecto_evaluacion.Value = e.CommandArgument.ToString();
            if (!ProyectoTerminado())
            {
                switch (command)
                {
                    case "nuevo_riesgo":
                        Session[hdfguid.Value + "list_actividades"] = null;
                        Session[hdfguid.Value + "list_documentos"] = null;
                        hdf_id_riesgo.Value = "";
                        CargarCombos();
                        txtriesgo.Text = "";
                        txtestrategia.Text = "";
                        txtestrategia_det.Text = "";
                        hdfid_estrategia.Value = "";
                        hdfvalor_riesgo.Value = "";
                        ModalShow("#modal_riesgo");
                        break;
                    case "importar_riesgos":
                        
                        CargarRiesgosHistorial(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"])));
                        ModalShow("#modal_historial");
                        break;
                }

                InicializarTablas();
            }
            else
            {
                Toast.Error("El proyecto ya fue cerrado y no puede generarse ninguna información adicional.", this);

            }
            
        }

        protected void lnkeliminarevaluacion_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ProyectoTerminado())
                {
                    string motivos = hdfmotivos.Value;
                    LinkButton lnk = sender as LinkButton;
                    ProyectosEvaluacionesCOM evaluaciones = new ProyectosEvaluacionesCOM();
                    proyectos_evaluaciones evaluacion = new proyectos_evaluaciones();
                    evaluacion.usuario_borrado = Session["usuario"] as string;
                    evaluacion.comentarios_borrado = motivos;
                    evaluacion.id_proyecto_evaluacion = Convert.ToInt32(lnk.CommandArgument);
                    string vmensaje = evaluaciones.Eliminar(evaluacion);
                    if (vmensaje == "")
                    {
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                                    "AlertGO('Evaluación eliminada correctamente.','proyectos_evaluacion_riesgos.aspx?id_proyecto="+ Request.QueryString["id_proyecto"] + "');", true);
                    }
                    else
                    {
                        Toast.Error("Error al eliminar evaluación: " + vmensaje, this);

                    }
                }
                else {

                    Toast.Error("El proyecto ya fue cerrado y no puede generarse ninguna información adicional.", this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al eliminar evaluación.", this);
            }

        }      
        public string GuardarRiesgo(riesgos riesgo,  List<actividades> lst_actividades, List<documentos> lstdocumentos)
        {
            RiesgosCOM riesgos = new RiesgosCOM();
            return riesgos.Agregar(riesgo,lst_actividades,lstdocumentos, lblproyect.Text, Session["nombre"] as string);
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
                riesgo.id_riesgo_impacto = Convert.ToInt32(ddlimpacto_costo.SelectedValue);
                riesgo.id_riesgo_estrategia = Convert.ToInt32(hdfid_estrategia.Value == "" ? "0" : hdfid_estrategia.Value);
                riesgo.usuario = Session["usuario"] as string;
                riesgo.usuario_edicion = Session["usuario"] as string;
                riesgo.usuario_resp = riesgo.usuario;
                if (id_riesgo > 0) { riesgo.id_riesgo = id_riesgo; }
                riesgo.estrategia = txtestrategia_det.Text;
                riesgo.valor = Convert.ToByte(hdfvalor_riesgo.Value == "" ? "0" : hdfvalor_riesgo.Value);
                List<actividades> lstactividades = Session[hdfguid.Value + "list_actividades"] as List<datos.actividades>;
                List<documentos> lstdocumentos = Session[hdfguid.Value + "list_documentos"] as List<datos.documentos>;
                if (ProyectoTerminado())
                {
                    Toast.Error("Error al guardar riesgo: El proyecto ya fue cerrado, y no puede generarse ninguna información adicional.", this);

                }
                else if (riesgo.riesgo == "")
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
                else if (riesgo.id_riesgo_impacto == 0)
                {
                    Toast.Error("Error al guardar riesgo: Ingrese un impacto para el riesgo.", this);
                }
                else if (riesgo.id_riesgo_estrategia == 0)
                {
                    Toast.Error("Error al guardar riesgo: Ingrese una estrategia para el riesgo.", this);
                }
                else
                {
                    vmensaje = id_riesgo == 0 ? GuardarRiesgo(riesgo, lstactividades, lstdocumentos) : EditarRiesgo(riesgo, lstactividades, lstdocumentos);
                    if (vmensaje == "")
                    {
                        hdfid_riesgo.Value = "";
                        hdf_id_riesgo.Value = "";
                        hdfvalor_riesgo.Value = "";
                        hdfid_estrategia.Value = "";
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
            finally {

                InicializarTablas();
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
        public void AgregarAccionTemporal(int id_actividad,int id_tipo,string resultado,bool terminada, bool recomendada,int id_proyecto, int id_riesgo, string nombre, string usuario_resp, string empleado_resp, DateTime? fecha_ejecucion,
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
                actividad.id_actividad_tipo = id_tipo;
                actividad.usuario_resp = usuario_resp;
                actividad.fecha_asignacion = Convert.ToDateTime(fecha_Asignacion);
                actividad.usuario = Session["usuario"] as string;
                actividad.usuario_edicion = Session["usuario"] as string;
                actividad.resultado = resultado;
                actividad.recomendada = recomendada;
                actividad.terminada = terminada;
                actividad.fecha_ejecucion = fecha_ejecucion;


                List<documentos> lstdocumentos = Session[hdfguid.Value + "list_documentos"] as List<datos.documentos>;
                documentos documento = new documentos();
                if (size_documento != "" && size_documento != "0")
                {

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
                    lstdocumentos.Add(documento);
                }


                List<actividades> lstactividades = Session[hdfguid.Value+"list_actividades"] as List<datos.actividades>;
                lstactividades.Add(actividad);
                Session[hdfguid.Value+"list_actividades"] = lstactividades;


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
                        actividad.fecha_asignacion = Convert.ToDateTime(fecha_Asignacion);
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
                repeater_acciones.DataSource = lstactividades;
                repeater_acciones.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar grid de acciones: " + ex.Message, this);
            }
        }

        protected void lnkacciones_Click(object sender, EventArgs e)
        {
            CargarDatosFiltros("");
            CargarGridAcciones();
            txtaccion.Text = "";
            div_nueva_Accion.Visible = true;
            div_cierre_actividad.Visible = false;
            txtfilterempleado.Text = "";
            txtfechaejecuacion.SelectedDate = DateTime.Now;
            ModalShow("#modal_acciones");

            InicializarTablas();
        }


        protected void lnkcancelar_Click(object sender, EventArgs e)
        {
            txtresultado.Text = "";
            cbxleccionesapren.Checked = true;
            cbxleccionesapren.Checked = true;
            ViewState["id_actividad"] = "";
            div_nueva_Accion.Visible = true;
            div_cierre_actividad.Visible = false;

            InicializarTablas();

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
                InicializarTablas();
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "InitPagging('#tabla_historial');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "InitPagging('#tabla_historial_acciones');", true);
                imgloadempleado.Style["display"] = "none";
                lblbemp.Style["display"] = "none";
            }
        }

        protected void AsyncUpload1_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            try
            {
                //GUARDAMOS LOS RESULTADOS DE LA ACTIVIDAD
                int r = AsyncUpload1.UploadedFiles.Count;
                int id_riesgo = Convert.ToInt32(hdfid_riesgo.Value == "" ? "0" : hdfid_riesgo.Value);
                int id_proyecto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
                int id_actividad_tipo = Convert.ToInt32(ddltipo_actividad.SelectedValue);
                if (ProyectoTerminado())
                {
                    Toast.Error("Error al guardar resultado: El proyecto fue terminado y no puede generarse ninguna información adicional.", this);
                }
                
                else if (txtresultado.Text == "")
                {
                    Toast.Error("Error al guardar resultado: Ingrese el reusltado de la acción ejecutada.", this);
                }
                else
                {
                    string name = "";
                    //si existe un id de riesgo, quiere decir que estamos dentro de un modal de edicion, y se agregara directamente el registro a la base de datos.
                    if (id_riesgo > 0)
                    {
                        ActividadesCOM actividades = new ActividadesCOM();
                        string id_actividad = ViewState["id_actividad"] as string;
                        actividades actividad = actividades.actividad(Convert.ToInt32(id_actividad == "0" ? "" : id_actividad));                       
                        actividad.id_riesgo = id_riesgo;                      
                        actividad.usuario_edicion = Session["usuario"] as string;
                        actividad.resultado = txtresultado.Text;
                        actividad.recomendada = true;
                        actividad.recomendada = cbxrecomendado.Checked;
                        List<documentos> lstdocumentos = new List<documentos>();

                        DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/"));//path localDateTime localDate = DateTime.Now;
                        string path_local = "files/documents/actividades/";
                        DateTime localDate = DateTime.Now;
                        string date = localDate.ToString();
                        date = date.Replace("/", "_");
                        date = date.Replace(":", "_");
                        date = date.Replace(" ", "");
                        name = path_local + Path.GetFileNameWithoutExtension(e.File.FileName) + "_" + date + Path.GetExtension(e.File.FileName);
                        //funciones.UploadFile(fuparchivo, dirInfo.ToString() + name.Trim(), this.Page);
                        e.File.SaveAs(dirInfo.ToString() + name.Trim());

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
                        lstdocumentos.Add(documento);

                        
                        string vmensaje = actividades.GuardarResultado(actividad, lstdocumentos);
                        if (vmensaje == "")
                        {   //ENVIAMOS NOTIFICACION
                            string usuario_resp = actividad.usuario_resp;
                            EmpleadosCOM usuarios = new EmpleadosCOM();
                            DataTable dt_usuario = usuarios.GetUsers();
                            DataView dv = dt_usuario.DefaultView;
                            dv.RowFilter = "usuario_red = '" + usuario_resp.Trim().ToUpper() + "'";
                            DataTable dt_result = dv.ToTable();
                            if (dt_result.Rows.Count > 0)
                            {

                                string saludo = DateTime.Now.Hour > 13 ? "Buenas tardes" : "Buenos dias";
                                DataRow usuario = dt_result.Rows[0];
                                string mail_to = usuario["mail"].ToString() == "" ? "" : (usuario["mail"].ToString() + ";");
                                string subject = "Módulo de proyectos - Resultado de actividad";
                                string mail = "<div>" + saludo + " <strong>" +
                                    System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(usuario["empleado"].ToString().ToLower())
                                    + "</strong> <div>" +
                                    "<br>" +
                                    "<p>Fue ingreso el resultado de una acción, dentro de una evaluación en el proyecto <strong>" + lblproyect.Text + "</strong>" +
                                    "</p>" +
                                    "<p>A continuación, se muestra la información completa:</p>" +
                                         "<p><strong>Riesgo</strong><br/> " +
                                      txtriesgo.Text + "</p> " +
                                       "<p><strong>Actividad/Acción</strong><br/> " +
                                       txtaccion_title.Text + "</p> " +
                                       "<p><strong>Resultado</strong><br/> " +
                                       txtresultado.Text + "</p> " +
                                    "<br/><p>Este movimiento fue realizado por <strong>" +
                                    System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Session["nombre"].ToString().ToLower())
                                    + "</strong> el dia <strong>" +
                                    DateTime.Now.ToString("dddd dd MMMM, yyyy hh:mm:ss tt", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX")) + "</strong>" +
                                    "</p>";
                                CorreosCOM correos = new CorreosCOM();
                                bool correct = correos.SendMail(mail, subject, mail_to);
                            }
                            ViewState["id_actividad"] = "";
                            hdfid_actividad.Value = "";
                            btneditarriesgo_Click(null, null);
                            Toast.Success("Resultado guardado correctamente", "Mensaje del sistema", this);
                        }
                        else
                        {

                            Toast.Error("Error al guardar resultado: " + vmensaje, this);
                        }


                    }
                  
                  
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al guardar resultado: " + ex.Message, this);
            }
            finally {
                InicializarTablas();
            }
        }

        protected void lnkguardaracciones_Click(object sender, EventArgs e)
        {
            int r = AsyncUpload1.UploadedFiles.Count;
            if (r == 0)
            {
                try
                {
                    int id_actividad_tipo = Convert.ToInt32(ddltipo_actividad.SelectedValue);
                    int id_riesgo = Convert.ToInt32(hdfid_riesgo.Value == "" ? "0" : hdfid_riesgo.Value);
                    int id_proyecto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
                    if (ProyectoTerminado())
                    {
                        Toast.Error("Error al guardar acción: El proyecto fue terminado y no puede generarse ninguna información adicional.", this);
                    }
                    else if (id_actividad_tipo == 0)
                    {
                        Toast.Error("Error al guardar acción: Seleccione un tipo de actividad", this);
                    }
                    else if (txtaccion.Text == "")
                    {
                        Toast.Error("Error al guardar acción: Ingrese la acción ejecutada.", this);
                    }
                    else if (!txtfechaejecuacion.SelectedDate.HasValue)
                    {
                        Toast.Error("Error al guardar acción: Ingrese la fecha de asignación.", this);
                    }
                    else
                    {
                        string name = "";
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
                            actividad.fecha_asignacion = Convert.ToDateTime(txtfechaejecuacion.SelectedDate);
                            actividad.usuario = Session["usuario"] as string;
                            actividad.resultado = "";
                            actividad.recomendada = true;
                            actividad.id_actividad_tipo = id_actividad_tipo;
                            List<documentos> lstdocumentos = new List<documentos>();
                       

                            ActividadesCOM actividades = new ActividadesCOM();
                            id_actividad = actividades.Agregar(actividad, lstdocumentos);

                            //ENVIAMOS NOTIFICACION
                            string usuario_resp = actividad.usuario_resp;
                            EmpleadosCOM usuarios = new EmpleadosCOM();
                            DataTable dt_usuario = usuarios.GetUsers();
                            DataView dv = dt_usuario.DefaultView;
                            dv.RowFilter = "usuario_red = '" + usuario_resp.Trim().ToUpper() + "'";
                            DataTable dt_result = dv.ToTable();
                            if (dt_result.Rows.Count > 0)
                            {

                                string saludo = DateTime.Now.Hour > 13 ? "Buenas tardes" : "Buenos dias";
                                DataRow usuario = dt_result.Rows[0];
                                string mail_to = usuario["mail"].ToString() == "" ? "" : (usuario["mail"].ToString() + ";");
                                string subject = "Módulo de proyectos - Actividad relacionada";
                                string mail = "<div>" + saludo + " <strong>" +
                                    System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(usuario["empleado"].ToString().ToLower())
                                    + "</strong> <div>" +
                                    "<br>" +
                                    "<p>Le fue asignada una actividad, dentro de una evaluación en el proyecto <strong>" + lblproyect.Text + "</strong>" +
                                    "</p>" +
                                    "<p>A continuación, se muestra la información completa:</p>" +
                                         "<p><strong>Riesgo</strong><br/> " +
                                      txtriesgo.Text + "</p> " +
                                       "<p><strong>Actividad/Acción</strong><br/> " +
                                       txtaccion.Text + "</p> " +
                                    "<br/><p>Este movimiento fue realizado por <strong>" +
                                    System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Session["nombre"].ToString().ToLower())
                                    + "</strong> el dia <strong>" +
                                    DateTime.Now.ToString("dddd dd MMMM, yyyy hh:mm:ss tt", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX")) + "</strong>" +
                                    "</p>";
                                CorreosCOM correos = new CorreosCOM();
                                bool correct = correos.SendMail(mail, subject, mail_to);
                            }

                        }
                        AgregarAccionTemporal(id_actividad, id_actividad_tipo,"",false, true, id_proyecto, id_riesgo, txtaccion.Text, ddlempleado_a_consultar.SelectedValue,
                            ddlempleado_a_consultar.SelectedItem.ToString(),
                           null, txtfechaejecuacion.SelectedDate, funciones.deTextoa64(name), "0", true, 0);
                        CargarGridAcciones();
                        txtaccion.Text = "";
                        div_nueva_Accion.Visible = true;
                        div_cierre_actividad.Visible = false;
                        txtfilterempleado.Text = "";
                        txtfechaejecuacion.SelectedDate = DateTime.Now;
                        CargarDatosFiltros("");
                        Toast.Success("Acción agregada correctamente", "Mensaje del sistema", this);
                    }
                }
                catch (Exception ex)
                {
                    Toast.Error("Error al guardar acción: " + ex.Message, this);
                }
                finally
                {
                    InicializarTablas();
                }
            }
        }

        protected void lnkeliminarparticipante_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ProyectoTerminado())
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
                else
                {
                    Toast.Error("El proyecto ha sido terminado y no puede generarse ninguna información adicional.", this);
                }
              
              
               
            }
            catch (Exception ex)
            {
                Toast.Error("Error al eliminar acción: " + ex.Message, this);
            }
            finally
            {
                InicializarTablas();
                ModalShow("#modal_acciones");
            }
        }

        protected void ddlprobabilidad_rep_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!ProyectoTerminado())
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
                        string vmensaje = riesgos.EditarProbabilidad(id_riesgo, id_riesgo_probabilidad,Session["usuario"] as string);
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
                else
                {
                    Toast.Error("El proyecto ha sido terminado y no puede generarse ninguna información adicional.", this);
                }
                
            }
            catch (Exception ex)
            {
                Toast.Error("Error al editar probabilidad: " + ex.Message, this);
            }
            finally
            {
                load_cumpli_compromisos.Style["display"] = "none";
                InicializarTablas();
            }
        }

        protected void ddlimpacto_costo_rep_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                if (!ProyectoTerminado())
                {
                    DropDownList ddl = sender as DropDownList;
                    RiesgosCOM riesgos = new RiesgosCOM();
                    int id_riesgo_impacto_costo = Convert.ToInt32(ddl.SelectedValue);
                    int id_riesgo = Convert.ToInt32(ddl.Attributes["id_riesgo"]);
                    int id_riesgo_impacto_costo_selected = Convert.ToInt32(ddl.Attributes["id_riesgo_impacto_costo_selected"]);
                    if (id_riesgo_impacto_costo > 0 && id_riesgo_impacto_costo != id_riesgo_impacto_costo_selected)
                    {
                        RiesgosImpactoCostosCOM probabilidades = new RiesgosImpactoCostosCOM();
                        riesgos_impactos probabilidad = probabilidades.impacto(id_riesgo_impacto_costo);
                        string vmensaje = riesgos.EditarImpacto(id_riesgo, id_riesgo_impacto_costo, Session["usuario"] as string);
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
                else
                {
                    Toast.Error("El proyecto ha sido terminado y no puede generarse ninguna información adicional.", this);
                }
               
            }
            catch (Exception ex)
            {
                Toast.Error("Error al editar impacto costo: " + ex.Message, this);
            }
            finally
            {
                load_cumpli_compromisos.Style["display"] = "none";

                InicializarTablas();
            }
        }
              
       
        protected void btneditarriesgo_Click(object sender, EventArgs e)
        {
            try
            {
                int id_riesgo = Convert.ToInt32(hdf_id_riesgo.Value == "" ? "0" : hdf_id_riesgo.Value);
                int command = Convert.ToInt32(hdfcommandgrid.Value == "" ? "2" : hdfcommandgrid.Value);
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
                            ddlimpacto_costo.Items.FindByValue(riesgo["id_riesgo_impacto"].ToString()) != null ? riesgo["id_riesgo_impacto"].ToString() : "0";

                        ddlprobabilidad_SelectedIndexChanged(null,null);
                        txtestrategia_det.Text = riesgo["estrategia_detalle"].ToString();
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
                                if (row["fecha_ejecucion"] != DBNull.Value)
                                {
                                    fecha_ejecucion = Convert.ToDateTime(row["fecha_ejecucion"]);
                                }
                                DateTime? fecha_asignacion = null;
                                if (row["fecha_asignacion"] != DBNull.Value)
                                {
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
                                AgregarAccionTemporal(Convert.ToInt32(row["id_actividad"]), Convert.ToInt32(row["id_actividad_tipo"]),
                                    row["resultado"].ToString(), Convert.ToBoolean(row["terminada"]), Convert.ToBoolean(row["recomendada"]),
                                    id_proyecto, id_riesgo, nombre, usuario_resp,
                                    empleado_resp, fecha_ejecucion, fecha_asignacion, path, size, true, id_documento);
                            }
                        }

                        if (command == 1)
                        {
                            ModalShow("#modal_riesgo");
                        }
                        else
                        {
                            lnkacciones_Click(null, null);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar riesgo: " + ex.Message, this);
            }
            finally {
                load_cumpli_compromisos.Style["display"] = "none";
                InicializarTablas();
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
                        Response.ContentType = "doc/docx";
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + documento.nombre);
                        Response.TransmitFile(dirInfo.ToString() + funciones.de64aTexto(documento.path));
                        Response.End();
                        hdfid_actividad.Value = "";
                    }
                    else {

                        Toast.Error("No es encuentra el documento especificado", this);
                    }
                }
                else
                {

                    Toast.Error("Esta actividad no contiene documento.", this);
                }

            }
            catch (Exception ex)
            {
                Toast.Error("Error al descargar documento: " + ex.Message, this);
            }
            finally {
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
                if (!ProyectoTerminado())
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
                            riesgos riesgo = new riesgos
                            {
                                id_proyecto_evaluacion = Convert.ToInt32(hdf_id_proyecto_evaluacion.Value),
                                riesgo = cbx.Attributes["name"].ToString(),
                                usuario = Session["usuario"] as string,
                                fecha_registro = DateTime.Now,
                                id_riesgos_estatus = 1,
                                id_riesgo_probabilidad = 1,
                                id_riesgo_impacto = 4,
                                id_riesgo_estrategia = 1,
                                valor = 1,
                                usuario_resp = Session["usuario"] as string,
                                estrategia = cbx.Attributes["estrategia"].ToString()
                            };
                            list_riesgos.Add(riesgo);
                        }
                    }
                    if (list_riesgos.Count > 0)
                    {
                        RiesgosCOM riesgos = new RiesgosCOM();
                        string vmensaje = "";
                        foreach (riesgos riesgo in list_riesgos)
                        {
                            if (!riesgos.Exists(riesgo.riesgo, Convert.ToInt32(hdf_id_proyecto_evaluacion.Value)))
                            {
                                vmensaje = riesgos.Agregar(riesgo, new List<actividades>(), new List<documentos>(),lblproyect.Text,Session["nombre"] as string);
                                if (vmensaje != "")
                                {
                                    break;
                                }
                            }
                        }
                        if (vmensaje == "")
                        {
                            hdfid_riesgo.Value = "";
                            hdf_id_riesgo.Value = "";
                            CargarInformacionInicial(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"])));
                            ModalClose("#modal_historial");
                            Toast.Success("Riesgos importados correctamente", "Mensaje del sistema", this);

                        }
                        else
                        {
                            Toast.Error("Error al seleccionar riesgo: " + vmensaje, this);
                        }
                    }
                    else
                    {
                        Toast.Error("Seleccione al menos un riesgo para importar.", this);
                    }
                }
                else
                {
                    Toast.Error("Error al cargar riesgo: El proyecto ha sido terminado y no puede generarse ninguna información adicional.", this);

                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al seleccionar riesgo: " + ex.Message, this);
            }
            finally {

                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "InitPagging('#tabla_historial');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "InitPagging('#tabla_historial_acciones');", true);
                InicializarTablas();
            }
        }

        /// <summary>
        /// Inicializa las tablas en modo resposinvo
        /// </summary>
        private void InicializarTablas()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "Init('.dvv');", true);
        }

        protected void lnkdashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("proyectos_dashboard.aspx?id_proyecto="+Request.QueryString["id_proyecto"]);
        }

        protected void ddlprobabilidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id_probabilidad = Convert.ToInt32(ddlprobabilidad.SelectedValue);
                int id_impacto = Convert.ToInt32(ddlimpacto_costo.SelectedValue);
                if (id_impacto > 0 && id_probabilidad > 0)
                {
                    RiesgosProbabilidadCOM probabilidades = new RiesgosProbabilidadCOM();
                    riesgos_probabilidad probabilidad = probabilidades.impacto(id_probabilidad);
                    RiesgosImpactoCostosCOM impactos = new RiesgosImpactoCostosCOM();
                    riesgos_impactos impacto = impactos.impacto(id_impacto);
                    int valor = impacto.valor * probabilidad.valor;
                    hdfvalor_riesgo.Value = valor.ToString();
                    RiesgosEstrategiaCOM estrategias = new RiesgosEstrategiaCOM();
                    DataTable dt = estrategias.SelectAll();
                    foreach (DataRow estrategia in dt.Rows)
                    {
                        int value_min = Convert.ToInt16(estrategia["valor_min"]);
                        int value_max = Convert.ToInt16(estrategia["valor_max"]);
                        if (valor >= value_min && valor <= value_max)
                        {
                            hdfid_estrategia.Value = Convert.ToInt32(estrategia["id_riesgo_estrategia"]).ToString();
                            txtestrategia.Text = estrategia["nombre"].ToString();

                            txtestrategia_det.Text = estrategia["descripcion"].ToString();
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al seleccionar probabilidad: " + ex.Message, this);
            }
            finally
            {
                InicializarTablas();
            }
        }

        protected void lnkguardaresultados_Click(object sender, EventArgs e)
        {
            try
            {
                int r = AsyncUpload1.UploadedFiles.Count;
                if (r == 0)
                {
                    Toast.Error("Error al guardar resultado: Seleccione un documento como evidencia", this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al guardar resultado: " + ex.Message, this);
            }
            finally
            {
                InicializarTablas();
            }

        }

        protected void lnkresultado_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton LNK = sender as LinkButton;
                txtresultado.Text = "";
                cbxleccionesapren.Checked = true;
                cbxleccionesapren.Checked = true;
                ViewState["id_actividad"] = LNK.CommandArgument.ToString();
                hdfid_actividad.Value = Convert.ToInt32(LNK.CommandArgument).ToString();
                div_cierre_actividad.Visible = true;
                div_nueva_Accion.Visible = false;

                ActividadesCOM actividades = new ActividadesCOM();
                string id_actividad = ViewState["id_actividad"] as string;
                actividades actividad = actividades.actividad(Convert.ToInt32(id_actividad == "0" ? "" : id_actividad));
                if (actividad != null)
                {
                    txtaccion_title.Text = actividad.nombre;
                    txtresultado.Text = actividad.resultado;
                    cbxleccionesapren.Checked = true;
                    cbxrecomendado.Checked = Convert.ToBoolean(actividad.recomendada);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al visualizar resultado: " + ex.Message, this);
            }
            finally
            {
                InicializarTablas();
                ModalShow("#modal_acciones");
            }

        }

        protected void lnkcargarleccionesaprendidas_Click(object sender, EventArgs e)
        {
            try
            {
                CargarAccionesHistorial(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"])));
                ModalShow("#modal_historial_acciones");
            }
            catch (Exception ex)
            {
                Toast.Error("Error al visualizar resultado: " + ex.Message, this);
            }
            finally
            {
                InicializarTablas();
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "InitPagging('#tabla_historial');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "InitPagging('#tabla_historial_acciones');", true);
            }

        }
    }
}