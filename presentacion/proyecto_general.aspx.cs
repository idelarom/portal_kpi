using datos.Modelos;
using datos.NAVISION;
using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace presentacion
{
    public partial class proyecto_general : System.Web.UI.Page
    {
        #region FUNCIONES

        private static int pid_proyecto = 0;

        private void ModalShow(string modalname)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                             "ModalShow('" + modalname + "');", true);
        }

        /// <summary>
        /// Carga la pestaña que se mando por parametro url
        /// </summary>
        /// <param name="tab"></param>
        private void VisibleTab(string tab)
        {
            int id_proyecto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
            tinfo.Attributes["class"] = "";
            tinvo.Attributes["class"] = "";
            tmin.Attributes["class"] = "";
            tdoc.Attributes["class"] = "";
            tcomun.Attributes["class"] = "";
            tadmin.Attributes["class"] = "";
            tab_info.Attributes["class"] = "tab-pane";
            tab_invo.Attributes["class"] = "tab-pane";
            tab_minu.Attributes["class"] = "tab-pane";
            tab_docs.Attributes["class"] = "tab-pane";
            tab_com.Attributes["class"] = "tab-pane";
            tab_admin.Attributes["class"] = "tab-pane";
            //CargarListadoEmpleados("");
            CargarGrafica(id_proyecto);
            CargarEntregables(id_proyecto);
            CargarTareas(id_proyecto);
            CargarDocumentos(id_proyecto);
            switch (tab.ToLower())
            {
                //tab informacion general
                case "tinfo":
                default:
                    tinfo.Attributes["class"] = "active";
                    tab_info.Attributes["class"] = "tab-pane active";
                    break;
                //tab involucrados
                case "tinvo":
                    CargarComboRoles();
                    CargarDiagramas();
                    tinvo.Attributes["class"] = "active";
                    tab_invo.Attributes["class"] = "tab-pane active";
                    break;
                //tab minutas
                case "tmin":
                    CargarInvolucrado(id_proyecto);
                    CargarMinutas(id_proyecto);
                    tmin.Attributes["class"] = "active";
                    tab_minu.Attributes["class"] = "tab-pane active";
                    break;
                //tab documentos
                case "tdoc":
                    tdoc.Attributes["class"] = "active";
                    tab_docs.Attributes["class"] = "tab-pane active";
                    break;
                //tab comunicacion
                case "tcomun":
                    CargarCorreos(id_proyecto);
                    tcomun.Attributes["class"] = "active";
                    tab_com.Attributes["class"] = "tab-pane active";
                    break;
                //tab administracion
                case "tadmin":
                    CargarEmpleadosProyecto(id_proyecto);
                    tadmin.Attributes["class"] = "active";
                    tab_admin.Attributes["class"] = "tab-pane active";
                    break;
            }
        }

        private void CargarDiagramas()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                "CargarDiagrama();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                "CargarDiagramaEmpleados();", true);
        }

        /// <summary>
        /// Carga la informacion de Roles de INvolucrados
        /// </summary>
        private void CargarComboRoles()
        {
            try
            {
                roles_proyecto entidad = new roles_proyecto();
                entidad.id_rol = 0;
                RolesCOM roles = new RolesCOM();
                DataTable dt = roles.Get(entidad);
                if (dt.Rows.Count > 0)
                {
                    ddlrol.DataValueField = "id_rol";
                    ddlrol.DataTextField = "rol";
                    ddlrol.DataSource = dt;
                    ddlrol.DataBind();
                    ddlrol.Items.Insert(0, new ListItem("--Seleccione un Rol--", "0"));
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// Carga la información completa del proyecto
        /// </summary>
        /// <param name="id_proyecto"></param>
        /// <param name="usuario"></param>
        /// <param name="administrador"></param>
        private void CargarProyectos(int id_proyecto, string usuario, bool administrador)
        {
            try
            {
                ProyectosCOM proyectos = new ProyectosCOM();
                DataTable dt = proyectos.sp_get_proyects_info(id_proyecto, usuario, administrador, Convert.ToInt32(Session["id_cliente"]),"").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    ViewState["correo_bienvenida"] = Convert.ToBoolean(row["correo_bienvenida"]);
                    ViewState["es_creador"] = Convert.ToString(Session["usuario"]).ToUpper().Trim() == row["usuario"].ToString().Trim().ToUpper();
                    lblproyect.Text = row["proyecto"].ToString();
                    lblfecha_inicio.Text = row["fecha_inicio_str"].ToString();
                    lblfecha_fin.Text = row["fecha_fin_str"].ToString();
                    lblresumen.Text = row["descripcion"].ToString();
                    lblavancee.Text = row["avance"].ToString();
                    lblavance2.Text = row["avance"].ToString();
                    barprog.Style["width"] = row["avance"].ToString() + "%";
                    rtxtproyecto.Text = row["proyecto"].ToString();
                    rtxtdescripcion.Text = row["descripcion"].ToString();
                    rtxtavance.Text = row["avance"].ToString();
                    lnkobjetivos.CommandName = row["objetivos"].ToString();
                    lnkdescsolucion.CommandName = row["descripcion_solucion"].ToString();
                    lnksupuestos.CommandName = row["supuestos"].ToString();
                    lnkfueraalcance.CommandName = row["fuera_alcance"].ToString();
                    lnkriesgos.CommandName = row["riesgos_alto_nivel"].ToString();
                    lnkobjetivos.CssClass = lnkobjetivos.CommandName == "" ? "btn btn-default btn-flat" : "btn btn-primary btn-flat";
                    lnkdescsolucion.CssClass = lnkdescsolucion.CommandName == "" ? "btn btn-default btn-flat" : "btn btn-primary btn-flat";
                    lnksupuestos.CssClass = lnksupuestos.CommandName == "" ? "btn btn-default btn-flat" : "btn btn-primary btn-flat";
                    lnkfueraalcance.CssClass = lnkfueraalcance.CommandName == "" ? "btn btn-default btn-flat" : "btn btn-primary btn-flat";
                    lnkriesgos.CssClass = lnkriesgos.CommandName == "" ? "btn btn-default btn-flat" : "btn btn-primary btn-flat";
                    hdfid_cliente.Value = row["idcliente"].ToString().Trim();
                    if (row["fecha_inicio"] != DBNull.Value)
                    {
                        rdtpinicio.SelectedDate = Convert.ToDateTime(row["fecha_inicio"]);
                        rdtpfin.SelectedDate = Convert.ToDateTime(row["fecha_fin"]);
                    }
                  
                    if (Request.QueryString["tab"] == null)
                    {
                        VisibleTab("tinfo");
                    }
                    else
                    {
                        VisibleTab(Request.QueryString["tab"]);
                    }

                    
                }
                else
                {
                    string url = "inicio.aspx";
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                        "ModalClose();", true);
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                        "AlertGO('Este Proyecto No existe', '" + url + "');", true);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        private void CargarEmpleadosProyecto(int id_proyecto)
        {
            try
            {
                ProyectosCOM proyectos = new ProyectosCOM();
                DataTable dt = proyectos.ListadoEmpleadoProyecto(id_proyecto).Tables[0];
                repeat_proyectos_empleados.DataSource = dt;
                repeat_proyectos_empleados.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError("Error al cargar empleados del proyecto. " + ex.ToString(), this);
            }
        }

        private void CargarCorreosCliente()
        {
            try
            {
                proyectos_clientes_contactos entidad = new proyectos_clientes_contactos();
                entidad.id_proyecto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
                ClientesCOM clientes = new ClientesCOM();
                DataTable dt = clientes.Get(entidad);
                string correos = "";
                foreach (DataRow row in dt.Rows)
                {
                    correos = correos + row["correo"].ToString().Trim().ToLower() + ";";
                }
                Session["correo_clientes"] = correos;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// Carga la información de los entregables
        /// </summary>
        /// <param name="id_proyecto"></param>
        private void CargarEntregables(int id_proyecto)
        {
            try
            {
                //entregables
                EntregablesCOM componente = new EntregablesCOM();
                proyectos_entregables entidad = new proyectos_entregables();
                DataTable dt_entregables = componente.GetAll(id_proyecto);
                div_chartentregables.Visible = dt_entregables.Rows.Count > 0;
                if (dt_entregables.Rows.Count > 0)
                {
                    grid_entregables_hide.DataSource = dt_entregables;
                    grid_entregables_hide.DataBind();
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        private void CargarDocumentos(int id_proyecto)
        {
            try
            {
                //entregables
                DocumentosCOM componente = new DocumentosCOM();
                proyectos_documentos entidad = new proyectos_documentos();
                DataTable dt_entregables = componente.GetAll(id_proyecto);

                repeater_documentos.DataSource = dt_entregables;
                repeater_documentos.DataBind();
                repeater_docs.DataSource = dt_entregables;
                repeater_docs.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        private void CargarTareas(int id_proyecto)
        {
            try
            {
                TareasCOM tareas = new TareasCOM();
                grid_tareas.DataSource = tareas.GetAll(id_proyecto);
                grid_tareas.DataBind();
                DataTable dt = tareas.GetAll(id_proyecto);
                List<SiteDataItem> siteData = new List<SiteDataItem>();
                foreach (DataRow row in dt.Rows)
                {
                    siteData.Add(new SiteDataItem(
                        Convert.ToInt32(row["id_tarea"]),
                        Convert.ToInt32(row["id_tarea_padre"]),
                        row["tarea"].ToString() + " | " + row["avance"].ToString() + " %"
                        ));
                }
                rtrvProyectWorks.DataTextField = "Text";
                rtrvProyectWorks.DataValueField = "ID";
                rtrvProyectWorks.DataFieldID = "ID";
                rtrvProyectWorks.DataFieldParentID = "ParentID";
                rtrvProyectWorks.DataSource = siteData;
                rtrvProyectWorks.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        private void CargarCorreos(int id_proyecto)
        {
            try
            {
                CorreosCOM correos = new CorreosCOM();
                DataTable dt = correos.GetAll(id_proyecto);
                if (dt.Rows.Count > 0)
                {
                    rgri_correos.DataSource = dt;
                    rgri_correos.DataBind();
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        private void CargarInvolucrado(int id_proyecto)
        {
            try
            {
                InvolucradosCOM involucraods = new InvolucradosCOM();
                DataSet ds = involucraods.sp_arbol_involucradosall(id_proyecto, false);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    rdlinvolucrados.DataTextField = "nombre_tipo";
                    rdlinvolucrados.DataValueField = "id_pinvolucrado";
                    rdlinvolucrados.DataSource = ds.Tables[0];
                    rdlinvolucrados.DataBind();
                    rdlinvopendientes.DataTextField = "nombre_tipo";
                    rdlinvopendientes.DataValueField = "id_pinvolucrado";
                    rdlinvopendientes.DataSource = ds.Tables[0];
                    rdlinvopendientes.DataBind();
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        [System.Web.Services.WebMethod]
        public static List<ArbolInvolucrados> GetInvolucrados()
        {
            List<ArbolInvolucrados> tree_involucrados = new List<ArbolInvolucrados>();
            try
            {
                InvolucradosCOM involucraods = new InvolucradosCOM();
                DataSet ds = involucraods.sp_arbol_involucrados(pid_proyecto, false);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    tree_involucrados.Add(new ArbolInvolucrados
                    {
                        idpinvolucrado = Convert.ToInt32(row["id_pinvolucrado"]),
                        nivel = Convert.ToInt32(row["nivel"]),
                        nombre = row["nombre"].ToString(),
                        correo = row["correo"].ToString(),
                        telefono = row["telefono"].ToString(),
                        rol = row["rol"].ToString(),
                        id_parent = Convert.ToInt32(row["id_parent"])
                    });
                }
                return tree_involucrados;
            }
            catch (Exception ex)
            {
                return tree_involucrados;
            }
        }

        [System.Web.Services.WebMethod]
        public static List<ArbolInvolucrados> GetEmpleadosInvolucrados()
        {
            List<ArbolInvolucrados> tree_involucrados = new List<ArbolInvolucrados>();
            try
            {
                InvolucradosCOM involucraods = new InvolucradosCOM();
                DataSet ds = involucraods.sp_arbol_involucrados(pid_proyecto, true);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    tree_involucrados.Add(new ArbolInvolucrados
                    {
                        idpinvolucrado = Convert.ToInt32(row["id_pinvolucrado"]),
                        nivel = Convert.ToInt32(row["nivel"]),
                        nombre = row["nombre"].ToString(),
                        correo = row["correo"].ToString(),
                        telefono = row["telefono"].ToString(),
                        rol = row["rol"].ToString(),
                        id_parent = Convert.ToInt32(row["id_parent"])
                    });
                }
                return tree_involucrados;
            }
            catch (Exception ex)
            {
                return tree_involucrados;
            }
        }

        /// <summary>
        /// Agrega un Nuevo Entregable
        /// </summary>
        /// <returns></returns>
        private string AgregarEntregable()
        {
            try
            {
                string vmensaje = "";
                proyectos_entregables entidad = new proyectos_entregables();
                EntregablesCOM entregables = new EntregablesCOM();
                entidad.id_proyecto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
                entidad.entregable = rtxtentregable.Text;
                if (rtxtentregable.Text == "")
                {
                    vmensaje = "Ingrese el Nombre del Entregable";
                }
                else if (!rdtfechaentregable.SelectedDate.HasValue)
                {
                    vmensaje = "Ingrese una Fecha para el Entregable";
                }
                else if (rtxtavanceentregable.Text == "")
                {
                    vmensaje = "Ingrese un Avance del Entregable";
                }
                else if (Convert.ToInt32(rtxtavanceentregable.Text) > 100)
                {
                    rtxtavanceentregable.Text = "100";
                    vmensaje = "Ingrese un Avance del Entregable menor a 100";
                }
                else if (entregables.Exist(entidad))
                {
                    vmensaje = "Ya existe un Entregable con el Nombre " + rtxtentregable.Text;
                }
                else
                {
                    entidad.entregable = rtxtentregable.Text;
                    entidad.fecha = rdtfechaentregable.SelectedDate.Value;
                    entidad.avance = Convert.ToByte((rtxtavanceentregable.Text==""|| rtxtavanceentregable.Text=="0"?"1": rtxtavanceentregable.Text));
                    entidad.usuario = Session["usuario"] as string;
                    string[] return_array = entregables.Agregar(entidad);
                    vmensaje = return_array[0];
                }
                return vmensaje;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        private String EliminarEntregable(int id_entregable, string comentarios)
        {
            try
            {
                string vmensaje = "";
                proyectos_entregables entidad = new proyectos_entregables();
                entidad.id_proyecto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
                entidad.id_entregable = id_entregable;
                entidad.comentarios_borrado = comentarios;
                entidad.usuario_borrado = Session["usuario"] as string;
                EntregablesCOM entregables = new EntregablesCOM();
                vmensaje = entregables.Borrar(entidad);
                return vmensaje;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /// <summary>
        /// Edita un Entregable
        /// </summary>
        /// <returns></returns>
        private string EditarEntregable()
        {
            try
            {
                string vmensaje = "";
                proyectos_entregables entidad = new proyectos_entregables();
                EntregablesCOM entregables = new EntregablesCOM();
                entidad.id_proyecto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
                entidad.entregable = rtxtentregable.Text;
                if (rtxtentregable.Text == "")
                {
                    vmensaje = "Ingrese el Nombre del Entregable";
                }
                else if (!rdtfechaentregable.SelectedDate.HasValue)
                {
                    vmensaje = "Ingrese una Fecha para el Entregable";
                }
                else if (rtxtavanceentregable.Text == "")
                {
                    vmensaje = "Ingrese un Avance del Entregable";
                }
                else if (Convert.ToInt32(rtxtavanceentregable.Text) > 100)
                {
                    rtxtavanceentregable.Text = "100";
                    vmensaje = "Ingrese un Avance del Entregable menor a 100";
                }
                else
                {
                    entidad.id_entregable = Convert.ToInt32(txtid_entregable.Text.Trim());
                    entidad.entregable = rtxtentregable.Text;
                    entidad.fecha = rdtfechaentregable.SelectedDate.Value;
                    entidad.avance = Convert.ToByte(rtxtavanceentregable.Text);
                    entidad.usuario_edicion = Session["usuario"] as string;
                    vmensaje = entregables.Editar(entidad);
                }
                return vmensaje;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /// <summary>
        /// Actualiza el proyecto
        /// </summary>
        /// <returns></returns>
        private string ActualizarProyecto()
        {
            try
            {
                string vmensaje = "";
                if (rtxtproyecto.Text == "")
                {
                    vmensaje = "Ingrese el Nombre del Proyecto";
                }
                else if (rtxtdescripcion.Text == "")
                {
                    vmensaje = "Ingrese la Descripción del Proyecto";
                }
                else if (!rdtpinicio.SelectedDate.HasValue)
                {
                    vmensaje = "Ingrese la Fecha Inicial del Proyecto";
                }
                else if (!rdtpfin.SelectedDate.HasValue)
                {
                    vmensaje = "Ingrese la Fecha Final del Proyecto";
                }
                else if (rtxtavance.Text == "")
                {
                    vmensaje = "Ingrese un Avance del Proyecto";
                }
                else if (Convert.ToInt32(rtxtavance.Text) > 100)
                {
                    rtxtavance.Text = "100";
                    vmensaje = "Ingrese un Avance del Proyecto menor a 100";
                }
                else
                {
                    DateTime dinicial = rdtpinicio.SelectedDate.Value;
                    DateTime difinal = rdtpfin.SelectedDate.Value;
                    string fechainicio = dinicial.ToString("dddd dd MMMM, yyyy hh:mm:ss:tt", CultureInfo.CreateSpecificCulture("es-MX"));
                    string fechafin = difinal.ToString("dddd dd MMMM , yyyy hh:mm:ss:tt", CultureInfo.CreateSpecificCulture("es-MX"));
                    string proyecto = rtxtproyecto.Text;
                    string descripcion = rtxtdescripcion.Text;
                    int avance = Convert.ToInt32(rtxtavance.Text);
                    proyectos entidad = new proyectos();
                    entidad.id_proyecto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
                    entidad.proyecto = proyecto;
                    entidad.descripcion = descripcion;
                    entidad.fecha_inicio = dinicial;
                    entidad.fecha_fin = difinal;
                    entidad.fecha_inicio_str = fechainicio;
                    entidad.fecha_fin_str = fechafin;
                    entidad.usuario_edicion = Session["usuario"] as string;
                    entidad.avance = Convert.ToByte(avance);
                    entidad.duración = (difinal.Date - dinicial.Date).TotalDays == 1 ?
                        (difinal.Date - dinicial.Date).TotalHours.ToString() + " horas" : (difinal.Date - dinicial.Date).TotalDays.ToString() + " dias";
                    ProyectosCOM proyecto_ = new ProyectosCOM();
                    vmensaje = proyecto_.Editar(entidad);
                }
                return vmensaje;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /// <summary>
        /// Actualiza el Proyecto mediante archivo de Excel
        /// </summary>
        private void ActualizarProyectoExcel(Byte[] archivo, string file_name, string ext, string tamaño, string contentType, bool publico)
        {
            try
            {
                DataTable dt = ViewState["dt_comidas"] as DataTable;
                int id_proyect = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
                TareasCOM componente2 = new TareasCOM();
                proyectos_tareas ent2 = new proyectos_tareas();
                ent2.id_proyecto = id_proyect;
                div_errormodal2.Visible = false;
                ent2.usuario_borrado = Session["usuario"] as string;
                if (dt.Rows.Count > 0 && componente2.BorrarAll(ent2))
                {
                    bool isEnglish = funciones.Format(dt);
                    string vmensaje = "";
                    foreach (DataRow row in dt.Rows)
                    {
                        int nivel = Convert.ToInt32(row["NIVEL_DE_ESQUEMA"]);
                        switch (nivel)
                        {
                            case 1:
                                proyectos entidad = new proyectos();
                                entidad.id_proyecto = id_proyect;
                                entidad.codigo_proyecto = row["ID"].ToString().Trim();
                                entidad.proyecto = row["NOMBRE"].ToString().Trim();
                                entidad.descripcion = rtxtdescripcion.Text == "" ? row["NOMBRE"].ToString().Trim() : rtxtdescripcion.Text;
                                entidad.duración = row["DURACIÓN"].ToString().Trim(); ;
                                entidad.fecha_inicio = Convert.ToDateTime(funciones.RetunrFirmatDate(row["COMIENZO"].ToString().Trim(), isEnglish));
                                entidad.fecha_inicio_str = Convert.ToDateTime(entidad.fecha_inicio).ToString("dddd dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
                                entidad.fecha_fin = Convert.ToDateTime(funciones.RetunrFirmatDate(row["FIN"].ToString().Trim(), isEnglish));
                                entidad.fecha_fin_str = Convert.ToDateTime(entidad.fecha_inicio).ToString("dddd dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX"));

                                entidad.usuario_edicion = Session["usuario"] as string;
                                entidad.avance = Convert.ToByte(row["Porcentaje_completado"].ToString().Replace("%", "").Trim() == "" ? "0" : row["Porcentaje_completado"].ToString().Replace("%", "").Trim());
                                ProyectosCOM proyectos = new ProyectosCOM();
                                vmensaje = proyectos.Editar(entidad);
                                if (vmensaje != "")
                                {
                                    div_errormodal2.Visible = true;
                                    lblerrormodal2.Text = "ERROR EN EL PROYECTO CON ID " + row["ID"].ToString().Trim() + " " + vmensaje.ToString();
                                    ModalShow("#myModalExcel");
                                    break;
                                }
                                break;

                            case 2:
                            default:
                                proyectos_tareas entidad_tarea = new proyectos_tareas();
                                entidad_tarea.id_proyecto = id_proyect;
                                entidad_tarea.codigo_tarea = row["ID"].ToString().Trim();
                                entidad_tarea.tarea = row["NOMBRE"].ToString().Trim();
                                entidad_tarea.duración = row["DURACIÓN"].ToString().Trim();
                                entidad_tarea.fecha_inicio = Convert.ToDateTime(funciones.RetunrFirmatDate(row["COMIENZO"].ToString().Trim(), isEnglish));
                                entidad_tarea.fecha_inicio_str = Convert.ToDateTime(entidad_tarea.fecha_inicio).ToString("dddd dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
                                entidad_tarea.fecha_fin = Convert.ToDateTime(funciones.RetunrFirmatDate(row["FIN"].ToString().Trim(), isEnglish));
                                entidad_tarea.fecha_fin_str = Convert.ToDateTime(entidad_tarea.fecha_fin).ToString("dddd dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
                                entidad_tarea.avance = Convert.ToByte(row["Porcentaje_completado"].ToString().Replace("%", "").Trim() == "" ? "0" : row["Porcentaje_completado"].ToString().Replace("%", "").Trim());
                                entidad_tarea.recursos = row["Nombres_de_los_recursos"].ToString().Trim(); ;
                                entidad_tarea.usuario_edicion = Session["usuario"] as string;
                                entidad_tarea.usuario = Session["usuario"] as string;
                                entidad_tarea.actividades_predecesoras = row["Predecesoras"].ToString().Trim();
                                entidad_tarea.nivel_esquema = Convert.ToByte(row["NIVEL_DE_ESQUEMA"].ToString().Replace("%", "").Trim() == "" ? "0" : row["NIVEL_DE_ESQUEMA"].ToString().Replace("%", "").Trim());
                                TareasCOM tareas = new TareasCOM();
                                vmensaje = tareas.ExistDelete(entidad_tarea) ? tareas.EditarCodigo(entidad_tarea) : tareas.Agregar(entidad_tarea)[0];
                                if (vmensaje != "")
                                {
                                    div_errormodal2.Visible = true;
                                    lblerrormodal2.Text = "ERROR EN LA TAREA CON ID " + row["ID"].ToString().Trim() + " " + vmensaje.ToString();
                                    ModalShow("#myModalExcel");
                                    break;
                                }
                                break;
                        }
                    }
                    TareasCOM componente = new TareasCOM();
                    DataSet ds = componente.sp_genera_tareas_padres(id_proyect);
                    vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    if (vmensaje == "")
                    {
                        AgregarDocumento("archivo_principal_de_proyecto", archivo, file_name, ext, tamaño, contentType, true, false, false, false);
                        string url = "proyecto_general.aspx?id_proyecto=" + Request.QueryString["id_proyecto"];
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                            "ModalClose();", true);
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                            "AlertGO('Proyecto Actualizado Correctamente', '" + url + "');", true);
                    }
                    else
                    {
                        div_errormodal2.Visible = true;
                        lblerrormodal2.Text = "ERROR CON LA RELACION DE TAREAS";
                        ModalShow("#myModalExcel");
                    }
                }
            }
            catch (Exception ex)
            {
                div_errormodal2.Visible = true;
                lblerrormodal2.Text = ex.ToString();
                ModalShow("#myModalExcel");
            }
        }

        private String ActualizarTarea()
        {
            try
            {
                string vmensaje = "";
                if (txtid_tarea.Text == "")
                {
                    vmensaje = "ERROR INESPERADO. VERIFIQUE ESTA SITUACIÓN CON SU ADMINISTRADOR";
                }
                else if (rtxttarea.Text == "")
                {
                    vmensaje = "INGRESE UN NOMBRE PARA LA TAREA";
                }
                else if (rtxttarea.Text == "")
                {
                    vmensaje = "INGRESE UN NOMBRE PARA LA TAREA";
                }
                else if (!rdfechainiciotarea.SelectedDate.HasValue)
                {
                    vmensaje = "Ingrese la Fecha Inicial";
                }
                else if (!rdfechafintarea.SelectedDate.HasValue)
                {
                    vmensaje = "Ingrese la Fecha Final";
                }
                else if (rtxtxavancetarea.Text == "")
                {
                    vmensaje = "Ingrese un Avance del Proyecto";
                }
                else if (Convert.ToInt32(rtxtavance.Text) > 100)
                {
                    rtxtxavancetarea.Text = "100";
                    vmensaje = "Ingrese un Avance del Proyecto menor a 100";
                }
                else
                {
                    proyectos_tareas entidad = new proyectos_tareas();
                    TareasCOM tareas = new TareasCOM();
                    entidad.id_proyecto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
                    entidad.id_tarea = Convert.ToInt32(txtid_tarea.Text.Trim());
                    entidad.tarea = rtxttarea.Text;
                    entidad.fecha_inicio = rdfechainiciotarea.SelectedDate.Value;
                    entidad.fecha_inicio_str = Convert.ToDateTime(entidad.fecha_inicio).ToString("dddd dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
                    entidad.fecha_fin = rdfechafintarea.SelectedDate.Value;
                    entidad.fecha_fin_str = Convert.ToDateTime(entidad.fecha_fin).ToString("dddd dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
                    entidad.avance = Convert.ToByte(rtxtxavancetarea.Text);
                    entidad.recursos = rtxtrecursos.Text.Trim();
                    entidad.codigo_tarea = txtcodigo_tarea.Text;
                    entidad.duración = rtxtduracion.Text;
                    entidad.usuario_edicion = Session["usuario"] as string;
                    vmensaje = tareas.Editar(entidad);
                }

                return vmensaje;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /// <summary>
        /// Agrega un Nuevo Documento a la BD
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="file_name"></param>
        /// <param name="ext"></param>
        /// <param name="tamaño"></param>
        /// <param name="contentType"></param>
        /// <param name="publico"></param>
        /// <returns></returns>
        private String AgregarDocumento(string nombre, Byte[] archivo, string file_name, string ext, string tamaño, string contentType, bool publico, bool kit, bool doc_cierre, bool encuesta)
        {
            try
            {
                proyectos_documentos entidad = new proyectos_documentos();
                //DocumentosENT entidad = new DocumentosENT();
                DocumentosCOM documentos = new DocumentosCOM();
                entidad.id_proyecto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
                entidad.archivo = archivo;
                entidad.extension = ext;
                entidad.nombre = nombre;
                entidad.tamaño = tamaño;
                entidad.contentType = contentType;
                entidad.usuario = Session["usuario"] as string;
                entidad.publico = publico;
                entidad.archivo_proyecto = false;
                entidad.kit_cliente = kit;
                entidad.documento_cierre = doc_cierre;
                entidad.encuesta = encuesta;
                string[] return_array = documentos.Agregar(entidad);
                return return_array[0].ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /// <summary>
        /// Envia un Correo
        /// </summary>
        /// <param name="mail_to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        private String EnviarCorreo(string mail_to, string subject, string body, int caso_de_envio)
        {
            try
            {
                string vmensaje = "";
                proyectos_correos_historial entidad = new proyectos_correos_historial();
                entidad.id_proyecto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
                entidad.mail_to = mail_to;
                entidad.subject = subject;
                entidad.body = body;
                entidad.usuario = Session["usuario"] as string;
                int idcliente = Convert.ToInt32(hdfid_cliente.Value == "" ? "0" : hdfid_cliente.Value);
                string remitente = (Session["nombre"] as string) + " | " + (Session["puesto"] as string);
                string proyecto = lblproyect.Text;
                string informacion_adicional = "";
                string cliente = "";
                if (idcliente > 0)
                {
                    ClientesCOM clientes = new ClientesCOM();
                    UsuariosCOM usuarios = new UsuariosCOM();
                    DataTable dt_clientes = clientes.ListadoClientes(idcliente).Tables[0];
                    cliente = "Cliente";
                }
                else if (mail_to == "")
                {
                    remitente = "";
                    proyecto = "";
                    informacion_adicional = "";
                    cliente = "";
                    caso_de_envio = 0;
                }

                CorreosCOM correos = new CorreosCOM();
                DataSet ds = new DataSet();
                switch (caso_de_envio)
                {
                    //no aplica
                    case 0:
                        break;
                    //correo directo
                    case 1:
                        remitente = "";
                        proyecto = "";
                        informacion_adicional = "";
                        cliente = "";
                        ds = correos.Enviar(entidad, cliente, informacion_adicional, proyecto, remitente, caso_de_envio);
                        break;
                    //asignacion de proyecto a cliente
                    case 2:

                        //entidad.usuario = rtxtusuario.Text.Trim();
                        //entidad.password = funciones.deTextoa64(rtxtcontraseña.Text.Trim());
                        informacion_adicional = @"se ha sido asignado el proyecto <strong>" + proyecto.ToUpper().Trim() + "</strong></p>" +
                            //"<br><br>"+
                            //"<p>Adjuntamos la información de Inicio de Sesión para nuestro portal.</p><br>" +
                            //"<p><strong>Usuario: </strong>"+rtxtusuario.Text.Trim()+" </p>" +
                            //"<p><strong>Password: </strong>"+ rtxtcontraseña.Text.Trim()+" </p>" +
                            "<br><br>" +
                            "<br><br>" +
                            "<p>El Proyect Manager asignado a su proyecto es <strong>" + remitente + "</strong></p>" +
                             "<p>A la Brevedad el tendra contacto con usted.</p><br>";
                        ds = correos.Enviar(entidad, cliente, informacion_adicional, proyecto, remitente, caso_de_envio);
                        break;

                    case 3:
                        DataTable dt_participantes = ViewState["dt_participantes"] == null ? new DataTable() : ViewState["dt_participantes"] as DataTable;
                        DataTable dt_pendientes = ViewState["dt_pendientes"] == null ? new DataTable() : ViewState["dt_pendientes"] as DataTable;
                        if (dt_participantes.Rows.Count > 0)
                        {
                            dt_participantes.Columns.Remove("id_pinvolucrado");
                        }
                        if (dt_pendientes.Rows.Count > 0)
                        {
                            dt_pendientes.Columns.Remove("id_pinvolucrado");
                        }
                        subject = subject + "'" + proyecto + "'";
                        entidad.subject = subject;
                        Boolean es_cliente = Convert.ToBoolean(Session["cliente"]);
                        informacion_adicional =
                            "<h4 style='text-align:center;'>ASISTENTE DE JUNTAS DE TRABAJO Y MINUTA</p><HR NOSHADE WIDTH=400 SIZE=6 COLOR='#e53935' style='border: 2px;border-color:red;width:100%; '>" +
                            "<br><br>" +
                            "<table>" +
                            "<tr>" +
                                "<th style=' width:50px;background-color: #e53935 ;color:white;padding: 15px;text-align: left;'>Proyecto</th>" +
                                "<th style='width: 450px;background-color: #eeeeee;padding: 15px;text-align: left;'>" + proyecto + "</th>" +
                                "<th style='width:50px;background-color: #e53935 ;color:white;padding: 15px;text-align: left;'>Fecha</th>" +
                                "<th style='width:350px;background-color: #eeeeee;padding: 15px;text-align: left;'>" + Convert.ToDateTime(rdpfechaminuta.SelectedDate).ToString("dddd dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX")) + "</th>" +
                                "</tr>" +
                                "<tr>" +
                                "<th style=' width:50px;background-color: #e53935 ;color:white;padding: 15px;text-align: left;'>" + (es_cliente ? "Solicitante" : "PM") + "</th>" +
                                "<th style='width: 450px;background-color: #eeeeee;padding: 15px;text-align: left;'>" + remitente + "</th>" +
                                "<th style='width:50px;background-color: #e53935 ;color:white;padding: 15px;text-align: left;'>Lugar</th>" +
                                "<th style='width:350px;background-color: #eeeeee;padding: 15px;text-align: left;'>" + rtxtlugarminuta.Text.Trim() + "</th>" +
                                "</tr>" +
                                "<tr>" +
                                "<th style=' width:50px;background-color: #e53935 ;color:white;padding: 15px;text-align: left;'>Asunto</th>" +
                                "<th style='width: 450px;background-color: #eeeeee;padding: 15px;text-align: left;'>" + rtxtasuntominuta.Text.Trim() + "</th>" +
                                "</tr>" +
                            "</table>" +

                            "<br><br>" +
                            "<h4>Propósito de la junta</h4><HR NOSHADE WIDTH=400 SIZE=6 COLOR='#e53935' style='border: 2px;border-color:red;width:100%; '>" +
                            "<p>" + rtxtpropositos.Text.Trim() + "</p>" +
                            "<br><br>" +
                            "<h4>Resultados (entregables) a obtener al terminar la reunión</h4><HR NOSHADE WIDTH=400 SIZE=6 COLOR='#e53935' style='border: 2px;border-color:red;width:100%; '>" +
                            "<p>" + rtxtresultados.Text.Trim() + "</p>" +
                            "<br><br>" +
                            "<h4>Personas que participan</h4><HR NOSHADE WIDTH=400 SIZE=6 COLOR='#e53935' style='border: 2px;border-color:red;width:100%; '>" +
                            funciones.TableDinamic(dt_participantes, "tab_parti") +
                            "<br><br>" +
                            "<h4>Acuerdos tomados y resoluciones</h4><HR NOSHADE WIDTH=400 SIZE=6 COLOR='#e53935' style='border: 2px;border-color:red;width:100%; '>" +
                            "<p>" + rtxtacuerdos.Text.Trim() + "</p>" +
                            "<br><br>" +
                            "<h4>Acciones a realizar y asuntos pendientes</h4><HR NOSHADE WIDTH=400 SIZE=6 COLOR='#e53935' style='border: 2px;border-color:red;width:100%; '>" +
                            funciones.TableDinamic(dt_pendientes, "tab_pendinte");
                        ds = correos.Enviar(entidad, cliente, informacion_adicional, proyecto, remitente, caso_de_envio);

                        break;
                    //se agrego involucrado
                    case 4:
                        InvolucradosCOM involucraods = new InvolucradosCOM();
                        DataSet ds_ = involucraods.sp_arbol_involucradosall(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"])), false);
                        DataTable dt_invo = ds_.Tables[0];
                        dt_invo.Columns.Remove("id_pinvolucrado");
                        dt_invo.Columns.Remove("nivel");
                        dt_invo.Columns.Remove("id_parent");
                        dt_invo.Columns.Remove("no_empleado");
                        dt_invo.Columns.Remove("nombre_tipo");
                        //foreach (DataRow row in dt_invo.Rows)
                        //{
                        //    if (row["tipo"].ToString().Trim().ToUpper() == "CLIENTE")
                        //    {
                        //        row["tipo"] = cliente;
                        //    }
                        //}
                        string dt_str = funciones.TableDinamic(dt_invo, "table_invo").ToString();
                        mail_to = mail_to + proyecto.ToUpper().Trim();
                        informacion_adicional = @"se agrego un integrante al proyecto <strong>" + proyecto.ToUpper().Trim() + "</strong>" +
                            "</p><br><p>Se Adjuntan los Involucrados al Proyecto:</p><br>" + dt_str + "<br><br>" +
                            "<p>El personal que realizo esta operación fue: <strong>" + remitente + "</strong>";
                        ds = correos.Enviar(entidad, cliente, informacion_adicional, proyecto, remitente, caso_de_envio);
                        break;

                    case 5:
                        informacion_adicional = @" el proyecto <strong>" + proyecto.ToUpper().Trim() + " </strong>" +
                            "ha sido terminado el <strong>" + DateTime.Now.ToString("dddd dd MMMM, yyyy hh:mm:ss:tt", CultureInfo.CreateSpecificCulture("es-MX")) + "</strong></p>" +
                            "<p>Este movimiento fue realizado por <strong>" + remitente + "</strong></p>";
                        ds = correos.Enviar(entidad, cliente, informacion_adicional, proyecto, remitente, caso_de_envio);
                        break;
                    case 6:
                        informacion_adicional = @"usted ha sido relacionado  al proyecto <strong>" + proyecto.ToUpper().Trim() + " </strong>" +
                             "<p>Este movimiento fue realizado por <strong>" + remitente + "</strong></p>";
                        ds = correos.Enviar(entidad, cliente, informacion_adicional, proyecto, remitente, caso_de_envio);
                        break;
                }

                return vmensaje;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /// <summary>
        /// Agrega un Nueva Minuta
        /// </summary>
        /// <returns></returns>
        private string AgregarMinuta()
        {
            try
            {
                string vmensaje = "";
                proyectos_minutas entidad = new proyectos_minutas();
                MinutasCOM minutas = new MinutasCOM();
                entidad.id_proyecto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
                entidad.asunto = rtxtasuntominuta.Text;
                entidad.fecha = rdpfechaminuta.SelectedDate.Value;
                entidad.lugar = rtxtlugarminuta.Text;
                entidad.acuerdos = rtxtacuerdos.Text;
                entidad.resultados = rtxtresultados.Text;
                entidad.propósito = rtxtpropositos.Text.Trim();
                entidad.usuario = Session["usuario"] as string;
                DataTable dt_participantes = ViewState["dt_participantes"] == null ? new DataTable() : ViewState["dt_participantes"] as DataTable;
                DataTable dt_pendientes = ViewState["dt_pendientes"] == null ? new DataTable() : ViewState["dt_pendientes"] as DataTable;
                string[] return_array = minutas.Agregar(entidad, dt_participantes, dt_pendientes);
                vmensaje = return_array[0];
                return vmensaje;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /// <summary>
        /// Edita  una Minuta
        /// </summary>
        /// <returns></returns>
        private string EditarMinuta(int id_minuta, string estatus)
        {
            try
            {
                string vmensaje = "";
                proyectos_minutas entidad = new proyectos_minutas();
                MinutasCOM minutas = new MinutasCOM();
                entidad.id_minuta = id_minuta;
                entidad.id_proyecto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
                entidad.asunto = rtxtasuntominuta.Text;
                entidad.fecha = rdpfechaminuta.SelectedDate.Value;
                entidad.lugar = rtxtlugarminuta.Text;
                entidad.acuerdos = rtxtacuerdos.Text;
                entidad.resultados = rtxtresultados.Text;
                entidad.estatus = estatus;
                entidad.propósito = rtxtpropositos.Text.Trim();
                entidad.usuario_edicion = Session["usuario"] as string;
                DataTable dt_participantes = ViewState["dt_participantes"] == null ? new DataTable() : ViewState["dt_participantes"] as DataTable;
                DataTable dt_pendientes = ViewState["dt_pendientes"] == null ? new DataTable() : ViewState["dt_pendientes"] as DataTable;
                vmensaje = minutas.Editar(entidad, dt_participantes, dt_pendientes);

                return vmensaje;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /// <summary>
        /// Elimina  una Minuta
        /// </summary>
        /// <returns></returns>
        private string EliminarMinuta(int id_minuta, string comentarios)
        {
            try
            {
                string vmensaje = "";
                proyectos_minutas entidad = new proyectos_minutas();
                MinutasCOM minutas = new MinutasCOM();
                entidad.id_minuta = id_minuta;
                entidad.id_proyecto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
                entidad.usuario_borrado = Session["usuario"] as string;
                entidad.comentarios_borrado = comentarios;
                vmensaje = minutas.Borrar(entidad);

                return vmensaje;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        private void CargarMinutas(int id_proyecto)
        {
            try
            {
                MinutasCOM minutas = new MinutasCOM();
                DataTable dt = minutas.GetAll(id_proyecto);
                dt.Columns.Add("visible");
                foreach (DataRow row in dt.Rows)
                {
                    if (Convert.ToBoolean(Session["cliente"]))
                    {
                        row["visible"] = row["usuario"].ToString().Trim().ToUpper() == Convert.ToString(Session["usuario"]).Trim().ToUpper();
                    }
                    else
                    {
                        row["visible"] = true;
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    repeater_minutas.DataSource = dt;
                    repeater_minutas.DataBind();
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// Carga los Participantes de una minuta
        /// </summary>
        private void CargarParticipantes()
        {
            if (ViewState["dt_participantes"] != null)
            {
                DataTable dt = ViewState["dt_participantes"] as DataTable;
                DataTable dtcopy = dt.Copy();
                rgrid_participantes.DataSource = dtcopy;
                rgrid_participantes.DataBind();
            }
            else
            {
                rgrid_participantes.DataSource = null;
                rgrid_participantes.DataBind();
            }
        }

        private void DeleteTableParticipantes(string nombre, string rol, string organizacion)
        {
            try
            {
                DataTable dt = ViewState["dt_participantes"] as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    if (nombre == row["nombre"].ToString())
                    {
                        row.Delete();
                        break;
                    }
                }
                ViewState["dt_participantes"] = dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private String AddTableParticipantes(string nombre, string rol, string organizacion, int id_pinvolucrado)
        {
            try
            {
                if (ViewState["dt_participantes"] == null)
                {
                    DataTable ndt = new DataTable();
                    ndt.Columns.Add("nombre");
                    ndt.Columns.Add("rol");
                    ndt.Columns.Add("organizacion");
                    ndt.Columns.Add("id_pinvolucrado");
                    ViewState["dt_participantes"] = ndt;
                }
                DeleteTableParticipantes(nombre, rol, organizacion);
                DataTable dt = ViewState["dt_participantes"] as DataTable;
                DataRow row = dt.NewRow();
                row["nombre"] = nombre;
                row["rol"] = rol;
                row["organizacion"] = organizacion;
                row["id_pinvolucrado"] = id_pinvolucrado;
                dt.Rows.Add(row);
                ViewState["dt_participantes"] = dt;
                CargarParticipantes();
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        private String AddTablePendientes(string responsable, string descripcion, DateTime fecha, int id_pinvolucrado)
        {
            try
            {
                if (ViewState["dt_pendientes"] == null)
                {
                    DataTable ndt = new DataTable();
                    ndt.Columns.Add("responsable");
                    ndt.Columns.Add("descripcion");
                    ndt.Columns.Add("fecha");
                    ndt.Columns.Add("id_pinvolucrado");
                    ViewState["dt_pendientes"] = ndt;
                }
                DeleteTablePendientes(responsable, descripcion);
                DataTable dt = ViewState["dt_pendientes"] as DataTable;
                DataRow row = dt.NewRow();
                row["responsable"] = responsable;
                row["descripcion"] = descripcion;
                row["fecha"] = fecha;
                row["id_pinvolucrado"] = id_pinvolucrado;
                dt.Rows.Add(row);
                ViewState["dt_pendientes"] = dt;
                CargarPendientes();
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        private void DeleteTablePendientes(string responsable, string descripcion)
        {
            try
            {
                DataTable dt = ViewState["dt_pendientes"] as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    if (responsable == row["responsable"].ToString().Trim() && descripcion == row["descripcion"].ToString().Trim())
                    {
                        row.Delete();
                        break;
                    }
                }
                ViewState["dt_pendientes"] = dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CargarPendientes()
        {
            if (ViewState["dt_pendientes"] != null)
            {
                DataTable dt = ViewState["dt_pendientes"] as DataTable;
                DataTable dtcopy = dt.Copy();
                grid_pendiente.DataSource = dtcopy;
                grid_pendiente.DataBind();
            }
            else
            {
                grid_pendiente.DataSource = null;
                grid_pendiente.DataBind();
            }
        }

        private void CargarListadoEmpleados(string filtro)
        {
            try
            {
                Employee entidad = new Employee();
                EmpleadosCOM empleados = new EmpleadosCOM();
                DataTable dt_original = empleados.Get(entidad);
                DataTable dt = new DataTable();
                if (filtro == "")
                {
                    dt = dt_original;
                }
                else {
                    if (dt_original.Select("nombre_completo like '%" + filtro + "%'").Length > 0)
                    {
                        dt = filtro == "" ? dt_original : dt_original.Select("nombre_completo like '%" + filtro + "%'").CopyToDataTable();
                    }
                }
               

                if (dt.Rows.Count > 0)
                {
                    ViewState["dt_empleados"] = dt;
                    rdllista_empleados.DataTextField = "nombre_completo";
                    rdllista_empleados.DataValueField = "No_";
                    rdllista_empleados.DataSource = dt;
                    rdllista_empleados.DataBind();
                    rdlempleadosproyecto.DataTextField = "nombre_completo";
                    rdlempleadosproyecto.DataValueField = "No_";
                    rdlempleadosproyecto.DataSource = dt;
                    rdlempleadosproyecto.DataBind();
                }
                else {

                    Alert.ShowAlertError("No se encontro ninguna coincidencia. Intentelo nuevamente.", this);
                }                
               
                
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError("Error al cargar lista de empleados. ", this);
            }
        }

        private void CargarContactoClientes()
        {
            try
            {
                int id_cliente = Convert.ToInt32(rdlclientes.SelectedValue);
                txtid_cliente.Text = id_cliente.ToString().Trim();

                ClientesCOM clientes = new ClientesCOM();
                UsuariosCOM usuarios = new UsuariosCOM();
                div_usuarios.Visible = !usuarios.ExistUserCliente(id_cliente);
                DataTable dt_clientes = clientes.ListadoClientes(id_cliente).Tables[0];
                if (dt_clientes.Rows.Count > 0)
                {
                    DataRow row = dt_clientes.Rows[0];
                    rtxtcliente_direccion.Text = row["dir"].ToString();
                    DataTable dt_contactos = clientes.ListadoContactos(id_cliente, 0).Tables[0];
                    if (dt_contactos.Rows.Count > 0)
                    {
                        ViewState["dt_contactos_original"] = dt_contactos;
                        div_nombre_cliente.Visible = true;
                        rdlcontacto_clientes.DataTextField = "nombre_completo";
                        rdlcontacto_clientes.DataValueField = "CveContacto";
                        rdlcontacto_clientes.DataSource = dt_contactos;
                        rdlcontacto_clientes.DataBind();
                    }
                    else
                    {
                        Alert.ShowAlertError("Este Cliente no Tiene contactos relacionados. Comuniquese con su administrador.", this);
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError("Erro al cargar lista de clientes: " + ex.ToString(), this);
            }
        }

        private void CargarListaClientes(string filtro)
        {
            try
            {
                ClientesCOM clientes = new ClientesCOM();
                DataTable dt = clientes.ListadoClientes(0).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataTable dt_clientes = new DataTable();
                    if (dt.Select("razon_social like '%" + filtro + "%'").Length > 0 && filtro != "")
                    {
                        dt_clientes = dt.Select("razon_social like '%" + filtro + "%'").CopyToDataTable();
                    }
                    else
                    {
                        dt_clientes = dt;
                    }

                    rdlclientes.DataTextField = "razon_social";
                    rdlclientes.DataValueField = "idcliente";
                    rdlclientes.DataSource = dt_clientes;
                    rdlclientes.DataBind();
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError("Erro al cargar lista de clientes: " + ex.ToString(), this);
            }
        }

        private void CargarTiposUsuarios(int id_tipo)
        {
            try
            {
                UsuariosCOM usuarios = new UsuariosCOM();
                DataTable dt = usuarios.GetTiposUsuariosClientes();
                ddltipos_usuarios.DataValueField = "id_uperfil";
                ddltipos_usuarios.DataTextField = "perfil";
                ddltipos_usuarios.DataSource = dt;
                ddltipos_usuarios.DataBind();
                ddltipos_usuarios.Items.Insert(0, new ListItem("--Seleccione un Tipo de Usuario", "0"));
                ddltipos_usuarios.SelectedValue = id_tipo.ToString();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError("Erro al cargar lista de tipos de usuarios: " + ex.ToString(), this);
            }
        }

        #endregion FUNCIONES

        #region EVENTOS

        /// <summary>
        /// Cargar un script para la grafica
        /// </summary>
        private void CargarGrafica(int id_proyecto)
        {
            try
            {
                //entregables
                EntregablesCOM componente = new EntregablesCOM();
                proyectos_entregables entidad = new proyectos_entregables();
                DataTable dt_entregables = componente.GetAll(id_proyecto);
                string colores = "";
                foreach (DataRow row in dt_entregables.Rows)
                {
                    DateTime fecha = Convert.ToDateTime(row["fecha"]);
                    int avance = Convert.ToInt32(row["avance"]);



                    if (avance < 100 && DateTime.Now > fecha)
                    {
                        //rojo
                        colores = colores + "'#C42C2C',";
                    }
                    else {

                        if (avance < 30)
                        {
                            //azul
                            colores = colores + "'#1565c0',";
                        }
                        else if (avance < 80 && avance > 30)
                        {
                            //amarillo
                            colores = colores + "'#ffeb3b',";
                        }
                        else if (avance > 80)
                        {
                            //azul
                            colores = colores + "'#00897b',";
                        }
                    }
                }
                colores = colores.TrimEnd(',');

                StringBuilder sb = new StringBuilder();
                sb.Append("<script type='text/javascript'>");
                sb.Append(""
                            + "var title = document.getElementById('ContentPlaceHolder1_lblproyect').innerText;"
                            + "Highcharts.chart('container', {"
                            + "colors: ["+colores+"],"
                            + "data: {"
                            + "table: 'ContentPlaceHolder1_grid_entregables_hide'"
                            + "},"
                            + "chart: {"
                            + "type: 'column'"
                            + " },"
                            + " title: {"
                            + "     text: ''"
                            + " },"
                            + "  yAxis: {"
                               + "max: 100,"
                               + "min:0,"
                            + "      allowDecimals: false,"
                            + "      title: {"
                            + "          text: title"
                            + "      }"
                            + " },"
                            + "  tooltip: {"
                            + "      formatter: function () {"
                            + "          return '<b>' + this.series.name + '</b><br />' +"
                            + "              this.point.y + ' % ' + this.point.name.toLowerCase().split('-')[1];"
                            + "       }"
                            + "   },"
                            + "  plotOptions: {"
                            + "      column: {"
                            + "          colorByPoint: true"
                            + "      },"
                            + "      series: {"
                            + "          cursor: 'name',"
                            + "          point: {"
                            + "              events: {"
                            + "                   click: function () {"
                            + "                       LoadPage();"
                            + "                       OpenModalEntregableForGraph(this.name.split('-')[0]);"
                            + "                       }"
                            + "                     }"
                            + "                  }"
                            + "               }"
                            + "             }"
                            + "          });"
                            + "      ");
                sb.Append("</script>");
                ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), sb.ToString());
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            int id_proyecto = Request.QueryString["id_proyecto"] == null ? 0 : Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
            string usuario = Session["usuario"] as string;
            if (id_proyecto == 0)
            {
                Alert.ShowAlertError("ERROR AL INGRESAR A LA PAGINA.", this);
            }
            else
            {
                if (!IsPostBack)
                {

                    proyecto_general.pid_proyecto = id_proyecto;
                    ViewState["dt_participantes"] = null;
                    ViewState["dt_empleados"] = null;
                    ViewState["dt_comidas"] = null;
                    ViewState["es_creador"] = null;
                    ViewState["correo_bienvenida"] = null;
                    ViewState["dt_contactos_original"] = null;
                    ViewState["dt_pendientes"] = null;
                    ViewState["pendiente_responsable"] = null;
                    ViewState["pendiente_descripcion"] = null;
                    CargarProyectos(id_proyecto, usuario, Convert.ToBoolean(Session["administrador"]));
                    funciones.ActualizaAvances();
                 
                    Boolean cliente = Convert.ToBoolean(Session["cliente"]);
                    lnkguardarcliente.Visible = !cliente;
                    //lnkeliminarinvolucrado.Visible = !cliente;
                    //lnkguardarinvolucrado.Visible = !cliente;
                    lnkguardarcharter.Visible = !cliente;
                    lnkaddpendientes.Visible = !cliente;
                    lnkaddparticipante.Visible = !cliente;
                    lnkguardarminuta.Visible = !cliente;
                    lnknuevaminuta.Visible = !cliente;
                    cbxpublico.Checked = false;
                    cbxpublico.Enabled = !cliente;
                    lnkguardartarea_modal.Visible = !cliente;
                    lnkeliminartarea_modal.Visible = !cliente;
                    lnkGuardarEntregable.Visible = !cliente;
                    lnkeliminarentregable.Visible = !cliente;
                    lnksubirexcel.Visible = !cliente;
                    lnkeditar_cambios.Visible = !cliente;
                    lnkactualiza_excel.Visible = !cliente;
                    lnknuevo_entregable.Visible = !cliente;
                    lnkagregarempleado.Visible = !cliente;
                    tadmin.Visible = !cliente;

                    bool es_creador = Convert.ToBoolean(ViewState["es_creador"]);
                    bool administrador = Convert.ToBoolean(Session["administrador"]);
                    lnkterminacíon.Visible = ((es_creador || administrador) && !cliente);
                  
                }
            }
        }

        private void CargarGraficaMailstones()
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void lnkeditar_cambios_Click(object sender, EventArgs e)
        {
            div_errormodal.Visible = false;
            lblerrormodal.Text = "";
            string vmensaje = ActualizarProyecto();
            if (vmensaje == "")
            {
                string url = "proyecto_general.aspx?id_proyecto=" + Request.QueryString["id_proyecto"];
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                    "ModalClose();", true);
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                    "AlertGO('Proyecto Actualizado Correctamente', '" + url + "');", true);
            }
            else
            {
                div_errormodal.Visible = true;
                lblerrormodal.Text = vmensaje;
            }
        }

        protected void lnkeditar_Click(object sender, EventArgs e)
        {
            int id_proyecto = Request.QueryString["id_proyecto"] == null ? 0 : Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
            string usuario = Session["usuario"] as string;
            CargarProyectos(id_proyecto, usuario, Convert.ToBoolean(Session["administrador"]));
        }

        protected void lnksubirexcel_Click(object sender, EventArgs e)
        {
            try
            {
                div_errormodal2.Visible = false;
                lblerrormodal2.Text = "";
                if (fuparchivos.HasFile)
                {
                    string randomNumber = Session["usuario"] == null ? "0" : Session["usuario"] as string;
                    randomNumber = randomNumber + "_";
                    DateTime localDate = DateTime.Now;
                    string date = localDate.ToString();
                    date = date.Replace("/", "_");
                    date = date.Replace(":", "_");
                    date = date.Replace(" ", "");
                    DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/files/"));//path local
                    string name = dirInfo + randomNumber.Trim() + "csv_" + date + Path.GetExtension(fuparchivos.FileName);
                    funciones.UploadFile(fuparchivos, name.Trim(), this.Page);
                    string filePath = name;
                    string filename = fuparchivos.FileName;
                    string ext = Path.GetExtension(filename);
                    string contenttype = fuparchivos.PostedFile.ContentType; // funciones.ContentType(ext);//funciones.ContentType(ext);
                    string tamaño = fuparchivos.PostedFile.ContentLength.ToString();

                    DataTable dt = funciones.GetDataTableFromCsv(name, true);
                    if (dt.Rows.Count == 0)
                    {
                        div_errormodal2.Visible = true;
                        lblerrormodal2.Text = "EL ARCHIVO CSV NO PUDO SER LEIDO. UTILIZE EL FORMATO CORRECTO.";
                        ModalShow("#myModalExcel");
                    }
                    else if (!dt.Columns.Contains("Nivel_de_esquema"))
                    {
                        div_errormodal2.Visible = true;
                        lblerrormodal2.Text = "EL ARCHIVO CSV NO TIENE EL FORMATO COMPLETO, NO SE ENCONTRO LA COLUMNA: NIVEL DE ESQUEMA";
                        ModalShow("#myModalExcel");
                    }
                    else if (!dt.Columns.Contains("ID"))
                    {
                        div_errormodal2.Visible = true;
                        lblerrormodal2.Text = "EL ARCHIVO CSV NO TIENE EL FORMATO COMPLETO, NO SE ENCONTRO LA COLUMNA: ID";
                        ModalShow("#myModalExcel");
                    }
                    else if (!dt.Columns.Contains("NOMBRE"))
                    {
                        div_errormodal2.Visible = true;
                        lblerrormodal2.Text = "EL ARCHIVO CSV NO TIENE EL FORMATO COMPLETO, NO SE ENCONTRO LA COLUMNA: NOMBRE";
                        ModalShow("#myModalExcel");
                    }
                    else if (!dt.Columns.Contains("DURACIÓN"))
                    {
                        div_errormodal2.Visible = true;
                        lblerrormodal2.Text = "EL ARCHIVO CSV NO TIENE EL FORMATO COMPLETO, NO SE ENCONTRO LA COLUMNA: DURACIÓN";
                        ModalShow("#myModalExcel");
                    }
                    else if (!dt.Columns.Contains("COMIENZO"))
                    {
                        div_errormodal2.Visible = true;
                        lblerrormodal2.Text = "EL ARCHIVO CSV NO TIENE EL FORMATO COMPLETO, NO SE ENCONTRO LA COLUMNA: COMIENZO";
                        ModalShow("#myModalExcel");
                    }
                    else if (!dt.Columns.Contains("FIN"))
                    {
                        div_errormodal2.Visible = true;
                        lblerrormodal2.Text = "EL ARCHIVO CSV NO TIENE EL FORMATO COMPLETO, NO SE ENCONTRO LA COLUMNA: FIN";
                        ModalShow("#myModalExcel");
                    }
                    else if (!dt.Columns.Contains("Porcentaje_completado"))
                    {
                        div_errormodal2.Visible = true;
                        lblerrormodal2.Text = "EL ARCHIVO CSV NO TIENE EL FORMATO COMPLETO, NO SE ENCONTRO LA COLUMNA: Porcentaje_completado";
                        ModalShow("#myModalExcel");
                    }
                    else if (!dt.Columns.Contains("Predecesoras"))
                    {
                        div_errormodal2.Visible = true;
                        lblerrormodal2.Text = "EL ARCHIVO CSV NO TIENE EL FORMATO COMPLETO, NO SE ENCONTRO LA COLUMNA: PREDECESORAS";
                        ModalShow("#myModalExcel");
                    }
                    else
                    {
                        ViewState["dt_comidas"] = dt;
                        Stream fs = fuparchivos.PostedFile.InputStream;
                        BinaryReader br = new BinaryReader(fs);
                        Byte[] archivo = br.ReadBytes((Int32)fs.Length);
                        ActualizarProyectoExcel(archivo, filename, ext, tamaño, contenttype, true);
                    }
                }
                else
                {
                    div_errormodal2.Visible = true;
                    lblerrormodal2.Text = "SELECCIONE UN ARCHIVO CSV";
                    ModalShow("#myModalExcel");
                }
            }
            catch (Exception ex)
            {
                div_errormodal2.Visible = true;
                lblerrormodal2.Text = ex.ToString();
                ModalShow("#myModalExcel");
            }
            finally
            {
                lnkloadingexcel.Style["display"] = "none";
                lnksubirexcel.Visible = true;
            }
        }

        protected void lnknuevo_entregable_Click(object sender, EventArgs e)
        {
            div_comentarios_eliminacionentregable.Visible = false;
            lnkeliminarentregable.Visible = false;
            div_error_entregabe.Visible = false;
            lblerrorentregable.Text = "";
            rtxtentregable.Text = "";
            txtid_entregable.Text = "";
            rdtfechaentregable.Clear();
            rtxtavanceentregable.Text = "";
            ModalShow("#myModalEntregablle");
        }

        protected void lnkGuardarEntregable_Click(object sender, EventArgs e)
        {
            div_error_entregabe.Visible = false;
            lblerrorentregable.Text = "";
            string vmensaje = txtid_entregable.Text == "" ? AgregarEntregable() : EditarEntregable();
            if (vmensaje == "")
            {
                div_comentarios_eliminacionentregable.Visible = false;
                lnkeliminarentregable.Visible = false;
                div_error_entregabe.Visible = false;
                lblerrorentregable.Text = "";
                rtxtentregable.Text = "";
                txtid_entregable.Text = "";
                rdtfechaentregable.Clear();
                rtxtavanceentregable.Text = "";

                string url = "proyecto_general.aspx?id_proyecto=" + Request.QueryString["id_proyecto"];
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                    "ModalClose();", true);
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                    "AlertGO('Milestone Guardado Correctamente', '" + url + "');", true);
            }
            else
            {
                ModalShow("#myModalEntregablle");
                div_error_entregabe.Visible = true;
                lblerrorentregable.Text = vmensaje;
            }
        }

        protected void rtrvProyectWorks_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            int id = Convert.ToInt32(e.Node.Value);
            RadTreeNode node1 = rtrvProyectWorks.FindNodeByValue(id.ToString()) as RadTreeNode;
            if (node1 != null)
            {
                int nodes_children = node1.Nodes.Count;
                if (nodes_children > 0)
                {
                    node1.Expanded = node1.Expanded == true ? false : true;
                }
                TareasCOM tareas = new TareasCOM();
                DataTable dt = tareas.GetAll(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"])));
                DataRow[] row = null;
                if (dt.Rows.Count > 0)
                {
                    row = dt.Select("id_tarea = " + id.ToString() + "");
                }
                if (row.Length > 0)
                {
                    Boolean cliente = Convert.ToBoolean(Session["cliente"]);
                    div_error_tareas.Visible = false;
                    lblerror_tareas.Text = "";
                    lnkeliminartarea_modal.Visible = !cliente;
                    rtxttarea.Text = row[0]["tarea"].ToString();
                    rtxtxavancetarea.Text = row[0]["avance"].ToString();
                    if (row[0]["fecha_inicio"] != DBNull.Value)
                    {
                        rdfechainiciotarea.SelectedDate = Convert.ToDateTime(row[0]["fecha_inicio"]);
                        rdfechafintarea.SelectedDate = Convert.ToDateTime(row[0]["fecha_fin"]);
                        rtxtrecursos.Text = row[0]["recursos"].ToString().Trim();
                        txtcodigo_tarea.Text = row[0]["codigo_tarea"].ToString().Trim();
                        rtxtduracion.Text = row[0]["duración"].ToString().Trim();
                    }
                }
                txtid_tarea.Text = id.ToString();
                if (node1.Expanded && nodes_children > 0)
                {
                    ModalShow("#myModalTareas");
                }
                else if (nodes_children == 0)
                {
                    ModalShow("#myModalTareas");
                }
            }
        }

        protected void btneditarentregablegraph_Click(object sender, EventArgs e)
        {
            int id_entregable = hdfid_entregable.Value == "" ? 0 : Convert.ToInt32(hdfid_entregable.Value.Trim());
            if (id_entregable > 0)
            {
                div_comentarios_eliminacionentregable.Visible = false;
                div_error_entregabe.Visible = false;
                lblerrorentregable.Text = "";
                rtxtentregable.Text = "";
                rdtfechaentregable.Clear();
                txtid_entregable.Text = "";
                rtxtavanceentregable.Text = "";
                EntregablesCOM entregables = new EntregablesCOM();
                DataTable dt = entregables.Get(id_entregable);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    rtxtentregable.Text = row["entregable"].ToString();
                    rdtfechaentregable.SelectedDate = Convert.ToDateTime(row["fecha"]);
                    rtxtavanceentregable.Text = row["avance"].ToString();
                    txtid_entregable.Text = row["id_entregable"].ToString().Trim();
                }
                Boolean cliente = Convert.ToBoolean(Session["cliente"]);
                lnkeliminarentregable.Visible = !cliente;

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                                 "ModalCloseGlobal('#myModalLoad');", true);
                ModalShow("#myModalEntregablle");
            }
        }

        protected void rtxtxsearchtarea_TextChanged(object sender, EventArgs e)
        {
            if (rtxtxsearchtarea.Text.Length > 3)
            {
                //TareasCOM tareas = new TareasCOM();
                //DataTable dt = tareas.GetAll(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]))).
                //                Select("tarea like '%"+rtxtxsearchtarea.Text+"%'").CopyToDataTable();
                //foreach (DataRow row in dt.Rows)
                //{
                //    int id = Convert.ToInt32(row["id_tarea"]);
                //    RadTreeNode node1 = rtrvProyectWorks.FindNodeByValue(id.ToString()) as RadTreeNode;
                //    if (node1 != null)
                //    {
                //        node1.Selected = true;
                //    }
                //}
            }
        }

        protected void lnkeliminarentregable_Click(object sender, EventArgs e)
        {
            string comentarios = hdfmotivos.Value.ToString().Trim();
            int id_entregable = Convert.ToInt32(txtid_entregable.Text.Trim());
            if (id_entregable > 0)
            {
                string vmensaje = EliminarEntregable(id_entregable, comentarios);
                if (vmensaje == "")
                {
                    div_comentarios_eliminacionentregable.Visible = false;
                    lnkeliminarentregable.Visible = false;
                    div_error_entregabe.Visible = false;
                    lblerrorentregable.Text = "";
                    rtxtentregable.Text = "";
                    txtid_entregable.Text = "";
                    rdtfechaentregable.Clear();
                    rtxtavanceentregable.Text = "";

                    string url = "proyecto_general.aspx?id_proyecto=" + Request.QueryString["id_proyecto"];
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                        "ModalClose();", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                        "AlertGO('Entregable Eliminado Correctamente', '" + url + "');", true);
                }
                else
                {
                    ModalShow("#myModalEntregablle");
                    div_error_entregabe.Visible = true;
                    lblerrorentregable.Text = vmensaje;
                }
            }
        }

        protected void lnkguardartarea_modal_Click(object sender, EventArgs e)
        {
            string vmensaje = ActualizarTarea();
            if (vmensaje == "")
            {
                string url = "proyecto_general.aspx?id_proyecto=" + Request.QueryString["id_proyecto"];
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                    "ModalClose();", true);
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                    "AlertGO('Tarea Actualizada Correctamente', '" + url + "');", true);
            }
            else
            {
                ModalShow("#myModalTareas");
                div_error_tareas.Visible = true;
                lblerror_tareas.Text = vmensaje;
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            div_error_Documentos.Visible = false;
            lblerror_documento.Text = "";
            if (fupDocumentos.HasFile)
            {
                string randomNumber = Session["usuario"] == null ? "0" : Session["usuario"] as string;
                randomNumber = randomNumber + "_";
                DateTime localDate = DateTime.Now;
                string date = localDate.ToString();
                date = date.Replace("/", "_");
                date = date.Replace(":", "_");
                date = date.Replace(" ", "");
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/files/"));//path local
                string name = dirInfo + randomNumber.Trim() + "doc_" + date + Path.GetExtension(fupDocumentos.FileName);
                funciones.UploadFile(fupDocumentos, name.Trim(), this.Page);
                // Read the file and convert it to Byte Array
                string filePath = name;
                string filename = fupDocumentos.FileName;
                string ext = Path.GetExtension(filename);
                string contenttype = fupDocumentos.PostedFile.ContentType; // funciones.ContentType(ext);//funciones.ContentType(ext);
                string tamaño = fupDocumentos.PostedFile.ContentLength.ToString();
                if (contenttype != "")
                {
                    Stream fs = fupDocumentos.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] archivo = br.ReadBytes((Int32)fs.Length);
                    string vmensaje = AgregarDocumento(filename.Replace(ext,""), archivo, filename, ext, tamaño, contenttype, cbxpublico.Checked, false, false, false);
                    if (vmensaje == "")
                    {
                        string url = "proyecto_general.aspx?tab=tdoc&id_proyecto=" + Request.QueryString["id_proyecto"];
                        ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                            "ModalClose();", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                            "AlertGO('Documento Subido Correctamente', '" + url + "');", true);
                    }
                    else
                    {
                        div_error_Documentos.Visible = true;
                        lblerror_documento.Text = vmensaje;
                        ModalShow("#myModalDocumentos");
                    }
                }
                else
                {
                    div_error_Documentos.Visible = true;
                    lblerror_documento.Text = "LOS ARCHIVOS CON EXTENSIÓN " + ext.ToString().ToUpper() + " NO SON VALIDOS. CONTACTE A SU ADMINISTRADOR PARA MAS INFORMACIÓN.";
                    ModalShow("#myModalDocumentos");
                }
            }
            else
            {
                div_error_Documentos.Visible = true;
                lblerror_documento.Text = "El sistema no detecto ningun documento.";
                ModalShow("#myModalDocumentos");
            }
        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            int id = int.Parse((sender as LinkButton).CommandArgument);
            string evento = (sender as LinkButton).CommandName;
            proyectos_documentos entidad = new proyectos_documentos();
            DocumentosCOM documentos = new DocumentosCOM();
            if (evento == "Delete")
            {
                entidad.id_documento = id;
                entidad.usuario_borrado = Session["usuario"] as string;
                entidad.comentarios_borrado = hdfmotivos.Value;
                string vmensaje = documentos.Borrar(entidad);
                if (vmensaje == "")
                {
                    string url = "proyecto_general.aspx?tab=tdoc&id_proyecto=" + Request.QueryString["id_proyecto"];
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                        "ModalClose();", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                        "AlertGO('Documento Eliminado Correctamente', '" + url + "');", true);
                }
                else
                {
                    Alert.ShowAlertError(vmensaje, this);
                }
            }
            else
            {
                byte[] bytes;
                string fileName, contentType;
                DataTable dt = documentos.Get(id);
                if (dt.Rows.Count > 0)
                {
                    DataRow sdr = dt.Rows[0]; bytes = (byte[])sdr["archivo"];
                    contentType = sdr["ContentType"].ToString();
                    fileName = sdr["nombre"].ToString().Trim() + sdr["extension"].ToString().Trim();
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = contentType;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        protected void tinfo_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            string url = "proyecto_general.aspx?tab=" + lnk.CommandName + "&id_proyecto=" + Request.QueryString["id_proyecto"];
            Response.Redirect(url);
        }

        protected void lnkenviarcorreo_Click(object sender, EventArgs e)
        {
            try
            {
                div_error_correos.Visible = false;
                lblerrorcorreos.Text = "";
                string mail_to = txtto.Text.Trim();
                string subject = txtsubject.Text.Trim();
                string body = txtbody.Text;
                if (mail_to == "")
                {
                    ModalShow("#myModalCorreos");
                    div_error_correos.Visible = true;
                    lblerrorcorreos.Text = "El correo destinatario es requerido";
                }
                else if (subject == "")
                {
                    ModalShow("#myModalCorreos");
                    div_error_correos.Visible = true;
                    lblerrorcorreos.Text = "El Asunto es requerido";
                }
                else if (body == "")
                {
                    ModalShow("#myModalCorreos");
                    div_error_correos.Visible = true;
                    lblerrorcorreos.Text = "El contenido del correo es requerido";
                }
                else
                {
                    string vmensaje = EnviarCorreo(mail_to, subject, body, 1);
                    if (vmensaje == "")
                    {
                        string url = "proyecto_general.aspx?tab=tcomun&id_proyecto=" + Request.QueryString["id_proyecto"];
                        ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                            "ModalClose();", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                            "AlertGO('Correo Enviado Correctamente', '" + url + "');", true);
                    }
                    else
                    {
                        div_error_correos.Visible = true;
                        lblerrorcorreos.Text = vmensaje;
                        ModalShow("#myModalCorreos");
                    }
                }
            }
            catch (Exception ex)
            {
                div_error_correos.Visible = true;
                lblerrorcorreos.Text = ex.ToString();
                ModalShow("#myModalCorreos");
            }
            finally
            {
                lnkenviandocorreo.Style["display"] = "none";
                lnkenviarcorreo.Visible = true;
            }
        }

        protected void lnknuevocorreo_Click(object sender, EventArgs e)
        {
            div_body.Visible = false;
            txtbody.Visible = true;
            lnkenviarcorreo.Visible = true;
            txtto.ReadOnly = false;
            txtsubject.ReadOnly = false;
            bool es_cliente = Convert.ToBoolean(Session["cliente"]);
            if (es_cliente)
            {
                ProyectosCOM proyectos = new ProyectosCOM();
                DataTable dt_empleados = proyectos.ListadoEmpleadoProyecto(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]))).Tables[0];
                string correos = "";
                foreach (DataRow row in dt_empleados.Rows)
                {
                    correos = correos + row["correo"].ToString().Trim() + ";";
                    Session["correo_pm"] = correos;
                }
                txtto.Text = Session["correo_pm"] as string;
            }
            else
            {
                txtto.Text = Session["correo_clientes"] as string;
            }
            txtsubject.Text = "";
            txtbody.Text = "";
            ModalShow("#myModalCorreos");
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "IniciarEditor();", true);
        }

        protected void LinkButton21_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = sender as LinkButton;
                int id_pcorreo = Convert.ToInt32(lnk.CommandArgument);
                proyectos_correos_historial entidad = new proyectos_correos_historial();
                entidad.id_pcorreo = id_pcorreo;
                CorreosCOM correos = new CorreosCOM();
                DataTable dt = correos.Get(entidad);
                if (dt.Rows.Count > 0)
                {
                    lnkenviarcorreo.Visible = false;
                    div_body.Visible = true;
                    txtbody.Visible = false;
                    txtto.ReadOnly = true;
                    txtsubject.ReadOnly = true;
                    ModalShow("#myModalCorreos");
                    DataRow row = dt.Rows[0];
                    txtbody.Text = row["body"].ToString();
                    txtsubject.Text = row["subject"].ToString();
                    txtto.Text = row["mail_to"].ToString();
                    plhbody.Controls.Add(new Literal { Text = row["body"].ToString() });
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        protected void lnknuevaminuta_Click(object sender, EventArgs e)
        {
            rtxtasuntominuta.Text = "";
            rtxtlugarminuta.Text = "";
            rtxtpropositos.Text = "";
            txtid_minuta.Text = "";
            ViewState["dt_participantes"] = null;
            rdpfechaminuta.SelectedDate = null;
            div_errorminuta.Visible = false;
            CargarParticipantes();
            ModalShow("#myModalMinutas");
        }

        protected void lnkguardarminuta_Click(object sender, EventArgs e)
        {
            try
            {
                div_errorminuta.Visible = false;
                string vmensaje = "";
                if (rtxtasuntominuta.Text == "")
                {
                    vmensaje = "Ingrese un Asunto para la minuta";
                }
                else if (rtxtlugarminuta.Text == "")
                {
                    vmensaje = "Ingrese un Lugar";
                }
                else if (rtxtpropositos.Text == "")
                {
                    vmensaje = "Ingrese al menos un proposito";
                }
                else if (!rdpfechaminuta.SelectedDate.HasValue)
                {
                    vmensaje = "Debe Ingresa la fecha de la minuta";
                }
                else
                {
                    vmensaje = txtid_minuta.Text == "" ? AgregarMinuta() : EditarMinuta(Convert.ToInt32(txtid_minuta.Text), "BORRADOR");
                }

                if (vmensaje == "")
                {
                    string url = "proyecto_general.aspx?tab=tmin&id_proyecto=" + Request.QueryString["id_proyecto"];
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                        "ModalClose();", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                        "AlertGO('Minuta Guardada Correctamente', '" + url + "');", true);
                }
                else
                {
                    div_errorminuta.Visible = true;
                    lblerrorminuta.Text = vmensaje;
                    ModalShow("#myModalMinutas");
                }
            }
            catch (Exception ex)
            {
                div_errorminuta.Visible = true;
                lblerrorminuta.Text = ex.ToString();
                ModalShow("#myModalMinutas");
            }
            finally
            {
                lnkcargandoMinuta.Style["display"] = "none";
                lnkguardarminuta.Visible = true;
            }
        }

        protected void lnkeditminuta_Click(object sender, EventArgs e)
        {
            try
            {
                div_errorminuta.Visible = false;
                LinkButton lnk = sender as LinkButton;
                int id_minuta = Convert.ToInt32(lnk.CommandArgument);
                string vmensaje = "";
                switch (lnk.CommandName.ToLower())
                {
                    case "editar":
                    case "terminar":
                        proyectos_minutas entidad = new proyectos_minutas();
                        entidad.id_minuta = id_minuta;
                        MinutasCOM minutas = new MinutasCOM();
                        DataTable dt = minutas.Get(entidad);
                        if (dt.Rows.Count > 0)
                        {
                            txtid_minuta.Text = id_minuta.ToString();
                            rtxtasuntominuta.Text = dt.Rows[0]["asunto"].ToString();
                            rtxtlugarminuta.Text = dt.Rows[0]["lugar"].ToString();
                            rtxtpropositos.Text = dt.Rows[0]["propósito"].ToString();
                            rtxtresultados.Text = dt.Rows[0]["resultados"].ToString();
                            rtxtacuerdos.Text = dt.Rows[0]["acuerdos"].ToString();
                            rdpfechaminuta.SelectedDate = Convert.ToDateTime(dt.Rows[0]["fecha"]);
                        }
                        DataTable dt_participantes = minutas.GetAllParticipante(id_minuta);
                        if (dt_participantes.Rows.Count > 0)
                        {
                            DataView view = new System.Data.DataView(dt_participantes);
                            DataTable selected = view.ToTable("Selected", false, "nombre", "organización", "rol", "id_pinvolucrado");
                            selected.Columns["organización"].ColumnName = "organizacion";

                            ViewState["dt_participantes"] = selected;
                            CargarParticipantes();
                        }
                        else
                        {
                            ViewState["dt_participantes"] = null;
                            CargarParticipantes();
                        }
                        DataTable dt_pendientes = minutas.GetAllPendientes(Convert.ToInt32(txtid_minuta.Text == "" ? "0" : txtid_minuta.Text));
                        if (dt_pendientes.Rows.Count > 0)
                        {
                            DataView view = new System.Data.DataView(dt_pendientes);
                            DataTable selected = view.ToTable("Selected", false, "responsable", "descripcion", "fecha_planeada", "id_pinvolucrado");
                            selected.Columns["fecha_planeada"].ColumnName = "fecha";
                            ViewState["dt_pendientes"] = selected;
                            CargarPendientes();
                        }
                        else
                        {
                            ViewState["dt_pendientes"] = null;
                            CargarPendientes();
                        }
                        if (lnk.CommandName.ToLower() == "terminar")
                        {
                            if (hdfid_cliente.Value == "" || hdfid_cliente.Value == "0")
                            {
                                vmensaje = "Para Enviar la Minuta, debe relacionar este proyecto a un cliente.";
                            }
                            else
                            {
                                CargarCorreosCliente();
                                bool es_cliente = Convert.ToBoolean(Session["cliente"]);
                                string correos_clientes = "";
                                if (es_cliente)
                                {
                                    ProyectosCOM proyectos = new ProyectosCOM();
                                    DataTable dt_empleados = proyectos.ListadoEmpleadoProyecto(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]))).Tables[0];
                                    string correos = "";
                                    foreach (DataRow row in dt_empleados.Rows)
                                    {
                                        correos = correos + row["correo"].ToString().Trim() + ";";
                                        Session["correo_pm"] = correos;
                                    }
                                    correos_clientes = Session["correo_pm"] as string;
                                }
                                else
                                {
                                    correos_clientes = Session["correo_clientes"] as string;
                                }

                                EnviarCorreo(correos_clientes, "Nueva Minuta para el Proyecto ", "", 3);
                                entidad.estatus = "ENVIADA";
                                entidad.usuario_edicion = Session["usuario"] as string;
                                vmensaje = minutas.EditarEstatus(entidad);
                            }
                            if (vmensaje == "")
                            {
                                string url = "proyecto_general.aspx?tab=tmin&id_proyecto=" + Request.QueryString["id_proyecto"];
                                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                                    "ModalClose();", true);
                                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                                    "AlertGO('Minuta Enviada Correctamente', '" + url + "');", true);
                            }
                            else
                            {
                                Alert.ShowAlertError(vmensaje, this);
                            }
                        }
                        else
                        {
                            ModalShow("#myModalMinutas");
                        }
                        break;

                    case "delete":
                        string comentarios = hdfmotivos.Value;
                        vmensaje = EliminarMinuta(id_minuta, comentarios);
                        if (vmensaje == "")
                        {
                            string url = "proyecto_general.aspx?tab=tmin&id_proyecto=" + Request.QueryString["id_proyecto"];
                            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                                "ModalClose();", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                                "AlertGO('Minuta Eliminada Correctamente', '" + url + "');", true);
                        }
                        else
                        {
                            Alert.ShowAlertError(vmensaje, this);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                div_errorminuta.Visible = true;
                lblerrorminuta.Text = ex.ToString();
                ModalShow("#myModalMinutas");
            }
            finally
            {
                lnkcargandoMinuta.Style["display"] = "none";
                lnkguardarminuta.Visible = true;
            }
        }

        protected void lnkaddparticipante_Click(object sender, EventArgs e)
        {
            try
            {
                div_participantes.Visible = false;

                IList<RadListBoxItem> collection = rdlinvolucrados.SelectedItems;
                if (collection.Count > 0)
                {
                    foreach (RadListBoxItem item in collection)
                    {
                        proyectos_involucrados entidad = new proyectos_involucrados();
                        entidad.id_pinvolucrado = Convert.ToInt32(item.Value);
                        InvolucradosCOM involucrados = new InvolucradosCOM();
                        DataTable dt = involucrados.Get(entidad);
                        if (dt.Rows.Count > 0)
                        {
                            lnkeliminarinvolucrado.Visible = true;
                            diverror_invo.Visible = false;
                            AddTableParticipantes(dt.Rows[0]["nombre"].ToString(), dt.Rows[0]["rol"].ToString(), dt.Rows[0]["rol"].ToString(), Convert.ToInt32(item.Value));
                            rtxtnombreparticipante.Text = "";
                            rtxtrol.Text = "";
                            rtxtorganizacion.Text = "";
                        }
                    }
                }
                else
                {
                    if (rtxtnombreparticipante.Text == "")
                    {
                        div_participantes.Visible = true;
                        lblparticipantes.Text = "Ingrese el Nombre del Participante";
                    }
                    else if (rtxtrol.Text == "")
                    {
                        div_participantes.Visible = true;
                        lblparticipantes.Text = "Ingrese el Rol del Participante";
                    }
                    else if (rtxtorganizacion.Text == "")
                    {
                        div_participantes.Visible = true;
                        lblparticipantes.Text = "Ingrese la Organización del Participante";
                    }
                    else
                    {
                        AddTableParticipantes(rtxtnombreparticipante.Text, rtxtrol.Text, rtxtorganizacion.Text, 0);
                        rtxtnombreparticipante.Text = "";
                        rtxtrol.Text = "";
                        rtxtorganizacion.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                div_participantes.Visible = true;
                lblparticipantes.Text = ex.ToString();
                ModalShow("#myModalMinutas");
            }
        }

        protected void lnkeliminarparticipante_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = sender as LinkButton;
                string nombre = lnk.CommandArgument;
                DeleteTableParticipantes(nombre, "", "");
                CargarParticipantes();
            }
            catch (Exception ex)
            {
                div_participantes.Visible = true;
                lblparticipantes.Text = ex.ToString();
                ModalShow("#myModalMinutas");
            }
        }

        protected void lnkeliminarpendiente_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = sender as LinkButton;
                string nombre = lnk.CommandArgument;
                string pendiente = lnk.CommandName;
                DeleteTablePendientes(nombre, pendiente);
                CargarPendientes();
            }
            catch (Exception ex)
            {
                div_pendientes.Visible = true;
                lblerrorpendientes.Text = ex.ToString();
                ModalShow("#myModalPendientes");
            }
        }

        protected void lnkparticipantes_Click(object sender, EventArgs e)
        {
            MinutasCOM minutas = new MinutasCOM();
            DataTable dt_participantes = minutas.GetAllParticipante(Convert.ToInt32(txtid_minuta.Text == "" ? "0" : txtid_minuta.Text));
            if (dt_participantes.Rows.Count > 0)
            {
                DataView view = new System.Data.DataView(dt_participantes);
                DataTable selected = view.ToTable("Selected", false, "nombre", "organización", "rol", "id_pinvolucrado");
                selected.Columns["organización"].ColumnName = "organizacion";
                ViewState["dt_participantes"] = selected;
                CargarParticipantes();
            }
            else
            {
                CargarParticipantes();
            }
            div_participantes.Visible = false;
            div_addparticipante.Visible = false;
            div_selectedinvo.Visible = true;
            rdlinvolucrados.ClearSelection();
            ModalShow("#myModalParticipantes");
        }

        protected void lnkpendientes_Click(object sender, EventArgs e)
        {
            rtxtresponsable.Text = "";
            rtxtpendiente.Text = "";
            MinutasCOM minutas = new MinutasCOM();
            DataTable dt_pendientes = minutas.GetAllPendientes(Convert.ToInt32(txtid_minuta.Text == "" ? "0" : txtid_minuta.Text));
            if (dt_pendientes.Rows.Count > 0)
            {
                DataView view = new System.Data.DataView(dt_pendientes);
                DataTable selected = view.ToTable("Selected", false, "responsable", "descripcion", "fecha_planeada", "id_pinvolucrado");
                selected.Columns["fecha_planeada"].ColumnName = "fecha";
                ViewState["dt_pendientes"] = selected;
                CargarPendientes();
            }
            else
            {
                CargarPendientes();
            }
            rdlinvopendientes.ClearSelection();
            div_pendientes.Visible = false;
            ModalShow("#myModalPendientes");
        }

        protected void lnkobjetivos_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            String content = lnk.CommandName;
            String type = lnk.CommandArgument.ToString().Trim().ToUpper();
            lblcharter.Text = lnk.Text;
            rtxtcontent.Text = content;
            txttipo_charter.Text = type;
            ModalShow("#myModalCharter");
        }

        protected void lnkagregarinvolucrado_Click(object sender, EventArgs e)
        {
            div_listempleados.Visible = false;
            div_nievoinvo.Visible = true;
            rdllista_empleados.Items.Clear();
            txtid_invo.Text = "";
            rtxtnombreinvo.Text = "";
            rtxttelefonoinvo.Text = "";
            rtxtcorreoinvo.Text = "";
            lnkeliminarinvolucrado.Visible = false;
            diverror_invo.Visible = false;
            ModalShow("#myModalInvolucrados");
        }

        protected void lnkguardarinvolucrado_Click(object sender, EventArgs e)
        {
            try
            {
                string vmensaje = "";
                diverror_invo.Visible = false;
                if (div_listempleados.Visible && ViewState["dt_empleados"] != null)
                {
                    if (rdllista_empleados.SelectedItems.Count == 0)
                    {
                        vmensaje = "Seleccione un empleado";
                    }
                }
                else
                {
                    if (rtxtnombreinvo.Text == "")
                    {
                        vmensaje = "Ingrese el Nombre del Involucrado";
                    }
                    if (rtxttelefonoinvo.Text == "" && txtno_empleado.Text == "")
                    {
                        vmensaje = "Ingrese el Telefono del Involucrado";
                    }
                    if (rtxtcorreoinvo.Text == "" && txtno_empleado.Text == "")
                    {
                        vmensaje = "Ingrese el Correo del Involucrado";
                    }
                    if (ddlrol.SelectedValue == "0")
                    {
                        vmensaje = "Seleccione el Rol del Involucrado";
                    }
                }

                if (vmensaje == "")
                {
                    proyectos_involucrados entidad = new proyectos_involucrados();
                    entidad.id_proyecto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
                    entidad.nombre = rtxtnombreinvo.Text;
                    if (txtno_empleado.Text != "") { entidad.no_empleado = Convert.ToInt32(txtno_empleado.Text); }
                    entidad.telefono = rtxttelefonoinvo.Text;
                    entidad.celular = rtxtcelularinvo.Text;
                    entidad.correo = rtxtcorreoinvo.Text;
                    entidad.id_rol = Convert.ToInt32(ddlrol.SelectedValue);
                    entidad.usuario = Session["usuario"] as string;
                    entidad.usuario_edicion = Session["usuario"] as string;
                    int id_envolucrado = Convert.ToInt32(hdfid_involucrado.Value == "" ? "0" : hdfid_involucrado.Value);
                    if (id_envolucrado > 0) { entidad.id_pinvolucrado = id_envolucrado; }
                    InvolucradosCOM involucrados = new InvolucradosCOM();
                    vmensaje = id_envolucrado > 0 ? involucrados.Editar(entidad) : involucrados.Agregar(entidad);
                }

                if (vmensaje == "")
                {
                    CargarCorreosCliente();
                    string correos_clientes = Session["correo_clientes"] as string;
                    EnviarCorreo(correos_clientes, "Nuevo Integrante del Proyecto ", "", 4);
                    string url = "proyecto_general.aspx?tab=tinvo&id_proyecto=" + Request.QueryString["id_proyecto"];
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                        "AlertGO('Involucrado Guardado Correctamente', '" + url + "');", true);
                }
                else
                {
                    diverror_invo.Visible = true;
                    lblerrorinvo.Text = vmensaje;
                    ModalShow("#myModalInvolucrados");
                }
            }
            catch (Exception ex)
            {
                diverror_invo.Visible = true;
                lblerrorinvo.Text = ex.ToString();
                ModalShow("#myModalInvolucrados");
            }
        }

        protected void btneditarinvol_Click(object sender, EventArgs e)
        {
            try
            {
                proyectos_involucrados entidad = new proyectos_involucrados();
                entidad.id_pinvolucrado = Convert.ToInt32(hdfid_involucrado.Value.ToString().Trim());
                InvolucradosCOM involucrados = new InvolucradosCOM();
                DataTable dt = involucrados.Get(entidad);
                if (dt.Rows.Count > 0)
                {
                    txtid_invo.Text = hdfid_involucrado.Value.Trim();
                    rtxtnombreinvo.Text = dt.Rows[0]["nombre"].ToString();
                    rtxttelefonoinvo.Text = dt.Rows[0]["telefono"].ToString();
                    rtxtcelularinvo.Text = dt.Rows[0]["celular"].ToString();
                    rtxtcorreoinvo.Text = dt.Rows[0]["correo"].ToString();
                    ddlrol.SelectedValue = dt.Rows[0]["id_rol"].ToString();
                    txtno_empleado.Text = dt.Rows[0]["no_empleado"].ToString();
                    lnkeliminarinvolucrado.Visible = true;
                    diverror_invo.Visible = false;
                    CargarResponsabilidadesRoles();
                    div_nievoinvo.Visible = true;
                    div_listempleados.Visible = false;
                    ModalShow("#myModalInvolucrados");
                }
            }
            catch (Exception ex)
            {
                diverror_invo.Visible = true;
                lblerrorinvo.Text = ex.ToString();
                ModalShow("#myModalInvolucrados");
            }
        }

        protected void lnkeliminarinvolucrado_Click(object sender, EventArgs e)
        {
            try
            {
                proyectos_involucrados entidad = new proyectos_involucrados();
                entidad.id_pinvolucrado = Convert.ToInt32(txtid_invo.Text.ToString().Trim());
                entidad.usuario_borrado = Session["usuario"] as string;
                entidad.comentarios_borrado = hdfmotivos.Value;
                InvolucradosCOM involucrados = new InvolucradosCOM();
                string vmensaje = involucrados.Borrar(entidad);
                if (vmensaje == "")
                {
                    string url = "proyecto_general.aspx?tab=tinvo&id_proyecto=" + Request.QueryString["id_proyecto"];
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                        "AlertGO('Involucrado Eliminado Correctamente', '" + url + "');", true);
                }
                else
                {
                    diverror_invo.Visible = true;
                    lblerrorinvo.Text = vmensaje;
                    ModalShow("#myModalInvolucrados");
                }
            }
            catch (Exception ex)
            {
                diverror_invo.Visible = true;
                lblerrorinvo.Text = ex.ToString();
                ModalShow("#myModalInvolucrados");
            }
        }

        protected void lnkguardarcharter_Click(object sender, EventArgs e)
        {
            try
            {
                div_error_charter.Visible = false;
                proyectos entidad = new proyectos();
                entidad.id_proyecto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
                string tipo = txttipo_charter.Text.ToUpper();
                if (tipo == "OBJETIVOS")
                {
                    entidad.objetivos = rtxtcontent.Text;
                }
                else if (tipo == "SOLUCION")
                {
                    entidad.descripcion_solucion = rtxtcontent.Text;
                }
                else if (tipo == "SUPUESTOS")
                {
                    entidad.supuestos = rtxtcontent.Text;
                }
                else if (tipo == "ALCANCE")
                {
                    entidad.fuera_alcance = rtxtcontent.Text;
                }
                else if (tipo == "RIESGOS")
                {
                    entidad.riesgos_alto_nivel = rtxtcontent.Text;
                }
                entidad.usuario_edicion = Session["usuario"] as string;
                ProyectosCOM proyectos = new ProyectosCOM();
                string vmensaje = proyectos.EditarCharter(entidad);
                if (vmensaje == "")
                {
                    string url = "proyecto_general.aspx?id_proyecto=" + Request.QueryString["id_proyecto"];
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                        "AlertGO('Datos Guardados Correctamente', '" + url + "');", true);
                }
                else
                {
                    div_error_charter.Visible = true;
                    lblerrorcharter.Text = vmensaje.ToString();
                    ModalShow("#myModalCharter");
                }
            }
            catch (Exception ex)
            {
                div_error_charter.Visible = true;
                lblerrorcharter.Text = ex.ToString();
                ModalShow("#myModalCharter");
            }
        }

        protected void lnkaddpendientes_Click(object sender, EventArgs e)
        {
            try
            {
                div_pendientes.Visible = false;

                IList<RadListBoxItem> collection = rdlinvopendientes.SelectedItems;
                if (rtxtresponsable.Text == "" && collection.Count == 0)
                {
                    div_pendientes.Visible = true;
                    lblerrorpendientes.Text = "Ingrese el Nombre del Responsable o Seleccione uno de la lista";
                }
                else if (rtxtpendiente.Text == "")
                {
                    div_pendientes.Visible = true;
                    lblerrorpendientes.Text = "Ingrese la descripcion del pendiete";
                }
                else if (!rdtfecha_planeada.SelectedDate.HasValue)
                {
                    div_pendientes.Visible = true;
                    lblerrorpendientes.Text = "Ingrese la fecha planeada";
                }
                else
                {
                    if (ViewState["pendiente_responsable"] != null && ViewState["pendiente_descripcion"] != null)
                    {
                        string nombre = ViewState["pendiente_responsable"] as string;
                        string pendiente = ViewState["pendiente_descripcion"] as string;
                        DeleteTablePendientes(nombre, pendiente);

                    }
                    if (collection.Count > 0)
                    {
                        foreach (RadListBoxItem item in collection)
                        {
                            proyectos_involucrados entidad = new proyectos_involucrados();
                            entidad.id_pinvolucrado = Convert.ToInt32(item.Value);
                            InvolucradosCOM involucrados = new InvolucradosCOM();
                            DataTable dt = involucrados.Get(entidad);
                            if (dt.Rows.Count > 0)
                            {
                                AddTablePendientes(dt.Rows[0]["nombre"].ToString(), rtxtpendiente.Text, Convert.ToDateTime(rdtfecha_planeada.SelectedDate), Convert.ToInt32(item.Value));
                            }
                        }
                    }
                    else
                    {
                        AddTablePendientes(rtxtresponsable.Text, rtxtpendiente.Text, Convert.ToDateTime(rdtfecha_planeada.SelectedDate), 0);
                    }
                    rtxtpendiente.Text = "";
                    rdtfecha_planeada.SelectedDate = null;
                    rtxtorganizacion.Text = "";
                    rtxtresponsable.Text = "";
                    rdlinvopendientes.SelectedItems.Clear();
                    ViewState["pendiente_responsable"] = null;
                    ViewState["pendiente_descripcion"] = null;
                }
            }
            catch (Exception ex)
            {
                div_pendientes.Visible = true;
                lblerrorpendientes.Text = ex.ToString();
                ModalShow("#myModalPendientes");
            }
        }

        protected void lnkagregar_Click(object sender, EventArgs e)
        {
            div_addparticipante.Visible = !div_addparticipante.Visible;
        // div_selectedinvo.Visible = !div_selectedinvo.Visible;
        }

        protected void lnkagregarempleado_Click(object sender, EventArgs e)
        {
            rdllista_empleados.Items.Clear();
            div_nievoinvo.Visible = false;
            div_listempleados.Visible = true;
            txtbuscarempleado.Text = "";
            txtid_invo.Text = "";
            rtxtnombreinvo.Text = "";
            rtxttelefonoinvo.Text = "";
            rtxtcorreoinvo.Text = "";
            lnkeliminarinvolucrado.Visible = false;
            diverror_invo.Visible = false;

            ModalShow("#myModalInvolucrados");
        }

        protected void txtbuscarempleado_TextChanged(object sender, EventArgs e)
        {
            CargarListadoEmpleados(txtbuscarempleado.Text.Trim());
        }

        protected void rdllista_empleados_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdllista_empleados.SelectedItems.Count > 0)
                {
                    DataTable dt_original = ViewState["dt_empleados"] as DataTable;
                    DataTable dt = dt_original.Select("No_ = '" + rdllista_empleados.SelectedValue.ToString().Trim() + "'").CopyToDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        txtno_empleado.Text = rdllista_empleados.SelectedValue.ToString().Trim();
                        rtxtnombreinvo.Text = dt.Rows[0]["nombre_completo"].ToString();
                        rtxtcorreoinvo.Text = dt.Rows[0]["Company_E_Mail"].ToString();
                        rtxttelefonoinvo.Text = dt.Rows[0]["Mobile_Phone_No_"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        protected void ddlrol_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarResponsabilidadesRoles();
        }

        private void CargarResponsabilidadesRoles()
        {
            try
            {
                int id_rol = Convert.ToInt32(ddlrol.SelectedValue);
                if (id_rol > 0)
                {
                }
                roles_proyecto entidad = new roles_proyecto();
                entidad.id_rol = id_rol;
                RolesCOM roles = new RolesCOM();
                DataTable dt = roles.Get(entidad);
                if (dt.Rows.Count > 0)
                {
                    txtresponsabilidades.Text = dt.Rows[0]["responsabilidades"].ToString();
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        protected void lnkseleccionarcliente_Click(object sender, EventArgs e)
        {
            div_listaclientes.Visible = true;
            div_nombre_cliente.Visible = false;
            txtbuscarclientes.Visible = true;
            rdlclientes.Enabled = true;
            txtbuscarclientes.Text = "";
            int idcliente = Convert.ToInt32(hdfid_cliente.Value);
            div_usuarios.Visible = true;
            CargarTiposUsuarios(4);
            CargarListaClientes("");
            ddltipos_usuarios.Enabled = false;
            div_addnewcontact.Visible = false;
            rdlcontacto_clientes.Visible = true;
            lnkbuscarcliente.Visible = true;
            if (idcliente > 0)
            {
                ClientesCOM clientes = new ClientesCOM();
                UsuariosCOM usuarios = new UsuariosCOM();
                div_usuarios.Visible = !usuarios.ExistUserCliente(idcliente);
                DataTable dt_clientes = clientes.ListadoClientes(idcliente).Tables[0];
                txtbuscarclientes.Text = dt_clientes.Rows[0]["razon_social"].ToString();
                lnkbuscarcliente_Click(null, null);
                txtbuscarclientes.Visible = false;
                lnkbuscarcliente.Visible = false;
                rdlclientes.Enabled = false;
                rdlclientes.SelectedValue = idcliente.ToString();
                CargarContactoClientes();
            }
            ModalShow("#myModalClientes");
        }

        protected void txtbuscarclientes_TextChanged(object sender, EventArgs e)
        {
            CargarListaClientes(txtbuscarclientes.Text.Trim());
        }

        protected void rdlclientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarContactoClientes();
        }

        protected void lnkguardarcliente_Click(object sender, EventArgs e)
        {
            try
            {
                string vmensaje = "";
                usuarios entidad_usuarios = new usuarios();
                entidad_usuarios.usuario = rtxtusuario.Text;
                UsuariosCOM usuarios_check = new UsuariosCOM();
                if (div_usuarios.Visible && usuarios_check.GetExists(entidad_usuarios).Rows.Count > 0)
                {

                    vmensaje = "Ya existe un usuario llamado: "+rtxtusuario.Text;
                }
                else if (rdlclientes.SelectedItems.Count == 0)
                {
                    vmensaje = "Seleccione un cliente";
                }
                else if (rdlcontacto_clientes.SelectedItems.Count == 0)
                {
                    vmensaje = "Seleccione un contacto";
                }
                else if (rtxtcliente_direccion.Text == "")
                {
                    vmensaje = "La direccion del Cliente es necesaria.";
                }
                else if (div_usuarios.Visible && rtxtusuario.Text == "")
                {
                    vmensaje = "Es necesario asignar un usuario y contraseña";
                }
                else if (div_usuarios.Visible && rtxtcontraseña.Text == "")
                {
                    vmensaje = "Es necesario asignar un usuario y contraseña";
                }
                else if (div_usuarios.Visible && ddltipos_usuarios.SelectedValue == "0")
                {
                    vmensaje = "Es necesario seleccionar un tipo de usuario";
                }
                else if (rtxtcliente_direccion.Text == "")
                {
                    vmensaje = "La direccion del Cliente es necesaria.";
                }
                else
                {
                    proyectos entidad_p = new proyectos();
                    entidad_p.id_proyecto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
                    entidad_p.id_cliente = Convert.ToInt32(rdlclientes.SelectedValue);
                    ClientesCOM clientes = new ClientesCOM();
                   
                        vmensaje = clientes.RelacionarAProyecto(entidad_p);
                    if (vmensaje == "")
                    {
                        proyectos_clientes_contactos entidadballl = new proyectos_clientes_contactos();
                        entidadballl.id_proyecto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
                        entidadballl.usuario_borrado = Session["usuario"] as string;
                        if (div_addnewcontact.Visible)
                        {
                            if (rtxtnombre_newcontact.Text.Trim() == "")
                            {
                                vmensaje = "Ingrese el nombre del nuevo contacto";
                            }
                            else if (rtxttelefeno_newcontact.Text.Trim() == "")
                            {
                                vmensaje = "Ingrese el telefono del nuevo contacto";
                            }
                            else if (rtxtcorreoo_newcontact.Text.Trim() == "")
                            {
                                vmensaje = "Ingrese el correo del nuevo contacto";
                            }
                            else
                            {
                                proyectos_clientes_contactos entidad = new proyectos_clientes_contactos();
                                entidad.nombre = rtxtnombre_newcontact.Text.Trim(); ;
                                entidad.telefono = rtxttelefeno_newcontact.Text.Trim();
                                entidad.correo = rtxtcorreoo_newcontact.Text.Trim();
                                entidad.usuario = Session["usuario"] as string;
                                entidad.id_proyecto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
                                entidad.CveContacto = null;
                                vmensaje = clientes.Exist(entidad) ? "" : clientes.AgregarContacto(entidad);
                            }
                        }
                        IList<RadListBoxItem> collection = rdlcontacto_clientes.SelectedItems;
                        if (collection.Count > 0)
                        {
                            clientes.BorrarAll(entidadballl);
                            foreach (RadListBoxItem item in collection)
                            {
                                int cvecontacto = Convert.ToInt32(item.Value);
                                DataTable dt_contactos = clientes.ListadoContactos(Convert.ToInt32(rdlclientes.SelectedValue), cvecontacto).Tables[0];
                                proyectos_clientes_contactos entidad = new proyectos_clientes_contactos();
                                entidad.nombre = dt_contactos.Rows[0]["nombre_completo"].ToString();
                                entidad.telefono = "";
                                entidad.correo = dt_contactos.Rows[0]["email"].ToString();
                                entidad.usuario = Session["usuario"] as string;
                                entidad.id_proyecto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
                                entidad.CveContacto = cvecontacto;
                                vmensaje = clientes.Exist(entidad) ? "" : clientes.AgregarContacto(entidad);
                                if (vmensaje != "") { break; }
                            }
                        }
                    }
                    if (div_usuarios.Visible && vmensaje == "")
                    {
                        usuarios entidad = new usuarios();
                        entidad.id_cliente = Convert.ToInt32(rdlclientes.SelectedValue);
                        entidad.id_uperfil = Convert.ToInt32(ddltipos_usuarios.SelectedValue);
                        entidad.usuario = rtxtusuario.Text.Trim();
                        entidad.password = funciones.deTextoa64(rtxtcontraseña.Text.Trim());
                        UsuariosCOM usuarios = new UsuariosCOM();
                        vmensaje = usuarios.Agregar(entidad)[0];
                    }
                }

                if (vmensaje == "")
                {
                    hdfid_cliente.Value = rdlclientes.SelectedValue.Trim();
                    CargarCorreosCliente();
                    string correos_clientes = Session["correo_clientes"] as string;
                    Boolean correo_bienvenida = Convert.ToBoolean(ViewState["correo_bienvenida"]);
                    if (!correo_bienvenida)
                    {
                        ProyectosCOM proyects = new ProyectosCOM();
                        proyects.CorreoBienvenida(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"])));
                        EnviarCorreo(correos_clientes, "Asignación de Nuevo Proyecto", "", 2);
                    }
                    string url = "proyecto_general.aspx?id_proyecto=" + Request.QueryString["id_proyecto"];
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                        "AlertGO('Información del Cliente Guardada Correctamente', '" + url + "');", true);
                }
                else
                {
                    div_errorclientes.Visible = true;
                    lblerrorclientes.Text = vmensaje;
                    ModalShow("#myModalClientes");
                }
            }
            catch (Exception ex)
            {
                div_errorclientes.Visible = true;
                lblerrorclientes.Text = ex.ToString();
                ModalShow("#myModalClientes");
            }
        }

        protected void rdlcontacto_clientes_ItemDataBound(object sender, RadListBoxItemEventArgs e)
        {
            DataRowView dataSourceRow = (DataRowView)e.Item.DataItem;
            proyectos_clientes_contactos entidad = new proyectos_clientes_contactos();
            entidad.CveContacto = Convert.ToInt32(dataSourceRow["cvecontacto"]);
            entidad.id_proyecto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
            ClientesCOM clientes = new ClientesCOM();
            e.Item.Selected = clientes.Get(entidad).Rows.Count > 0;
        }

        protected void txtbuscarempleadoproyecto_TextChanged(object sender, EventArgs e)
        {
           
        }

        protected void lnkagregarempleadoaproyecto_Click(object sender, EventArgs e)
        {
            rdlempleadosproyecto.Items.Clear();
            txtbuscarempleadoproyecto.Text = "";
            
            ModalShow("#myModalEmpleados");
        }

        protected void lnkguardarempleado_Click(object sender, EventArgs e)
        {
            IList<RadListBoxItem> collection = rdlempleadosproyecto.SelectedItems;
            string vmensaje = "";
            try
            {
                div_errorempleados.Visible = false;
                lblerrorempleados.Text = "";

                if (collection.Count == 0)
                {
                    vmensaje = "Seleccione minimo 1 empleado.";
                }
                string correos_pm = "";
                if (vmensaje == "")
                {
                    ProyectosCOM proyectos = new ProyectosCOM();
                    foreach (RadListBoxItem item in collection)
                    {
                        int no_ = Convert.ToInt32(item.Value);
                        proyectos_empleados entidad = new proyectos_empleados();
                        InvolucradosCOM empleados = new InvolucradosCOM();
                        entidad.id_proyecto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
                        entidad.no_ = no_;
                        entidad.creador = false;
                        entidad.usuario = Session["usuario"] as string;
                        vmensaje = empleados.AgregarPM(entidad);

                        DataTable dt_empleados = proyectos.ListadoEmpleadoProyecto(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]))).Tables[0];

                        foreach (DataRow row in dt_empleados.Rows)
                        {
                            int nom_empl = Convert.ToInt32(row["no_"]);
                            if (no_ == nom_empl)
                            {
                                correos_pm = correos_pm + row["correo"].ToString().Trim() + ";";
                            }
                        }
                        if (vmensaje != "") { break; }
                    }
                }

                if (vmensaje == "")
                {
                    //aqui es we
                    if (correos_pm != "")
                    {
                        EnviarCorreo(correos_pm, "Asignación de Proyecto", "", 6);
                    }
                    string url = "proyecto_general.aspx?tab=tadmin&id_proyecto=" + Request.QueryString["id_proyecto"];
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                        "AlertGO('Configuración Guardada Correctamente', '" + url + "');", true);
                }
                else
                {
                    div_errorempleados.Visible = true;
                    lblerrorempleados.Text = vmensaje;
                }
            }
            catch (Exception ex)
            {
                div_errorempleados.Visible = true;
                lblerrorempleados.Text = ex.ToString();
            }
        }

        protected void lnkeliminarempleadoproyecto_Click(object sender, EventArgs e)
        {
            string vmensaje = "";
            try
            {
                div_errorempleados.Visible = false;
                lblerrorempleados.Text = "";
                LinkButton lnk = sender as LinkButton;
                int id_pempleado = Convert.ToInt32(lnk.CommandArgument);
                proyectos_empleados entidad = new proyectos_empleados();
                InvolucradosCOM empleados = new InvolucradosCOM();
                entidad.id_proyecto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
                entidad.id_pempleado = id_pempleado;
                entidad.comentarios_borrado = hdfmotivos.Value;
                entidad.usuario_borrado = Session["usuario"] as string;
                vmensaje = empleados.BorrarPM(entidad);

                if (vmensaje == "")
                {
                    string url = "proyecto_general.aspx?tab=tadmin&id_proyecto=" + Request.QueryString["id_proyecto"];
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                        "AlertGO('Empleado Eliminado Correctamente', '" + url + "');", true);
                }
                else
                {
                    Alert.ShowAlertError(vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        protected void lnksubirencuestas_Click(object sender, EventArgs e)
        {
            lblerrorterminacion.Text = "";
            div_errorterminacion.Visible = false;
            try
            {
                bool encuestas = fupencuestas.HasFile;
                bool cierre = fupdocierre.HasFile;
                bool kit = fupkit.HasFile;
                LinkButton lnk = sender as LinkButton;
                string command = lnk.CommandName;
                string vmensaje = "";
                string randomNumber = Session["usuario"] == null ? "0" : Session["usuario"] as string;
                randomNumber = randomNumber + "_";
                DateTime localDate = DateTime.Now;
                string date = localDate.ToString();
                date = date.Replace("/", "_");
                date = date.Replace(":", "_");
                date = date.Replace(" ", "");
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/files/"));//path local
                string name = "";
                string filePath = "";
                string filename = "";
                string ext = "";
                string contenttype = "";
                string tamaño = "";
                Stream fs;
                BinaryReader br;
                Byte[] archivo = null;

                DocumentosCOM documentos = new DocumentosCOM();
                DataTable dt = documentos.sp_existe_documentacion_cierre(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]))).Tables[0];
                int id_documento_encuesta = 0;
                int id_documento_cierre = 0;
                int id_documento_kit = 0;
                int id_documento;
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    id_documento_encuesta = Convert.ToInt32(row["id_encuesta"].ToString().Trim());
                    id_documento_cierre = Convert.ToInt32(row["id_cierre"].ToString().Trim());
                    id_documento_kit = Convert.ToInt32(row["id_kit"].ToString().Trim());
                }

                proyectos_documentos entidad = new proyectos_documentos();
                switch (command)
                {
                    case "encuestas":
                        if (!encuestas)
                        {
                            vmensaje = "Seleccione un documento para la encuesta";
                            break;
                        }
                        name = dirInfo + randomNumber.Trim() + "csv_" + date + Path.GetExtension(fupencuestas.FileName);
                        funciones.UploadFile(fupencuestas, name.Trim(), this.Page);
                        filePath = name;
                        filename = fupencuestas.FileName;
                        ext = Path.GetExtension(filename);
                        contenttype = fupencuestas.PostedFile.ContentType; // funciones.ContentType(ext);//funciones.ContentType(ext);
                        tamaño = fupencuestas.PostedFile.ContentLength.ToString();
                        fs = fupencuestas.PostedFile.InputStream;
                        br = new BinaryReader(fs);
                        archivo = br.ReadBytes((Int32)fs.Length);
                        if (id_documento_encuesta > 0)
                        {
                            entidad.id_documento = id_documento_encuesta;
                            entidad.usuario_borrado = Session["usuario"] as string;
                            entidad.comentarios_borrado = "Borrado por actualizacion";
                            documentos.Borrar(entidad);
                        }
                        break;

                    case "cierre":
                        if (!cierre)
                        {
                            vmensaje = "Seleccione un documento para los archivos de cierre";
                            break;
                        }
                        name = dirInfo + randomNumber.Trim() + "csv_" + date + Path.GetExtension(fupdocierre.FileName);
                        funciones.UploadFile(fupdocierre, name.Trim(), this.Page);
                        filePath = name;
                        filename = fupdocierre.FileName;
                        ext = Path.GetExtension(filename);
                        contenttype = fupdocierre.PostedFile.ContentType; // funciones.ContentType(ext);//funciones.ContentType(ext);
                        tamaño = fupdocierre.PostedFile.ContentLength.ToString();
                        fs = fupdocierre.PostedFile.InputStream;
                        br = new BinaryReader(fs);
                        archivo = br.ReadBytes((Int32)fs.Length);
                        if (id_documento_cierre > 0)
                        {
                            entidad.id_documento = id_documento_cierre;
                            entidad.usuario_borrado = Session["usuario"] as string;
                            entidad.comentarios_borrado = "Borrado por actualizacion";
                            documentos.Borrar(entidad);
                        }
                        break;

                    case "kit":
                        if (!kit)
                        {
                            vmensaje = "Seleccione un documento para el kit de cliente";
                            break;
                        }
                        name = dirInfo + randomNumber.Trim() + "csv_" + date + Path.GetExtension(fupkit.FileName);
                        funciones.UploadFile(fupkit, name.Trim(), this.Page);
                        filePath = name;
                        filename = fupkit.FileName;
                        ext = Path.GetExtension(filename);
                        contenttype = fupkit.PostedFile.ContentType; // funciones.ContentType(ext);//funciones.ContentType(ext);
                        tamaño = fupkit.PostedFile.ContentLength.ToString();
                        fs = fupkit.PostedFile.InputStream;
                        br = new BinaryReader(fs);
                        archivo = br.ReadBytes((Int32)fs.Length);
                        if (id_documento_kit > 0)
                        {
                            entidad.id_documento = id_documento_kit;
                            entidad.usuario_borrado = Session["usuario"] as string;
                            entidad.comentarios_borrado = "Borrado por actualizacion";
                            documentos.Borrar(entidad);
                        }
                        break;
                }
                if (vmensaje == "")
                {
                    AgregarDocumento("documento_de_" + command, archivo, filename, ext, tamaño, contenttype, true, kit, cierre, encuestas);
                }

                if (vmensaje == "")
                {
                    ValidarDocumentaciónCierre();
                    Alert.ShowAlert("Documento Guardado Correctamente", "Mensaje del Sistema", this);
                    ModalShow("#myModalTerminacion");
                }
                else
                {
                    lblerrorterminacion.Text = vmensaje;
                    div_errorterminacion.Visible = true;
                    ModalShow("#myModalTerminacion");
                }
            }
            catch (Exception ex)
            {
                lblerrorterminacion.Text = ex.ToString();
                div_errorterminacion.Visible = true;
                ModalShow("#myModalTerminacion");
            }
        }

        private void ValidarDocumentaciónCierre()
        {
            div_okencuesta.Visible = false;
            div_okcierre.Visible = false;
            div_okkit.Visible = false;
            try
            {
                DocumentosCOM documentos = new DocumentosCOM();
                DataTable dt = documentos.sp_existe_documentacion_cierre(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]))).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    bool encuesta = Convert.ToBoolean(row["encuesta"]);
                    bool cierre = Convert.ToBoolean(row["doc_cierre"]);
                    bool kit = Convert.ToBoolean(row["kit"]);
                    string id_encuesta = row["id_encuesta"].ToString().Trim();
                    string id_cierre = row["id_cierre"].ToString().Trim();
                    string id_kit = row["id_kit"].ToString().Trim();
                    div_okencuesta.Visible = encuesta;
                    div_okcierre.Visible = cierre;
                    div_okkit.Visible = kit;
                    div_cargacierre.Visible = !cierre;
                    div_cargaencuesta.Visible = !encuesta;
                    div_cargakit.Visible = !kit;
                    lnkdeescargaencuesta.CommandArgument = id_encuesta;
                    lnkdeescargacierre.CommandArgument = id_cierre;
                    lnkdeescargakit.CommandArgument = id_kit;
                    lnkterminaproyecto.Visible = false;
                    if (encuesta && cierre && kit)
                    {
                        lnkterminaproyecto.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private String TerminarProyecto(decimal costo_real, decimal valor_ganado)
        {
            div_okencuesta.Visible = false;
            div_okcierre.Visible = false;
            div_okkit.Visible = false;
            try
            {
                string vmensaje = "";

                proyectos entidad = new proyectos();
                entidad.id_proyecto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
                entidad.costo_real = costo_real;
                entidad.valor_ganado = valor_ganado;
                ProyectosCOM proyectos = new ProyectosCOM();
                vmensaje = proyectos.Terminar(entidad);
                return vmensaje;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        protected void lnkterminacíon_Click(object sender, EventArgs e)
        {
            ValidarDocumentaciónCierre();
            lnksubirencuestas.Visible = true;
            lnkcargandoencuesta.Style["display"] = "none";
            lnkdoccierre.Visible = true;
            lnkcargandocierre.Style["display"] = "none";
            lnkkit.Visible = true;
            lnkcargandokit.Style["display"] = "none";
            ModalShow("#myModalTerminacion");
        }

        #endregion EVENTOS

        protected void lnkguardarempleado_Click1(object sender, EventArgs e)
        {
        }

        protected void lnkterminaproyecto_Click(object sender, EventArgs e)
        {
            div_errorterminacion.Visible = false;
            try
            {
                string vmensaje = "";
                double costor = 0;
                bool costor_isdouble = Double.TryParse(rtxtcostoreal.Text.Trim(), out costor);
                double valorg = 0;
                bool valorganadoisdouble = Double.TryParse(rtxtvalorganado.Text.Trim(), out valorg);

                if (!costor_isdouble)
                {
                    vmensaje = "El valor del costo real no es un valor numerico, verifique esta situación.";
                }
                else if (!valorganadoisdouble)
                {
                    vmensaje = "El valor ganado no es un valor numerico, verifique esta situación.";
                }
                if (vmensaje == "")
                {
                    vmensaje = TerminarProyecto(Convert.ToDecimal(costor), Convert.ToDecimal(valorg));
                }

                if (vmensaje == "")
                {
                    ProyectosCOM proyectos = new ProyectosCOM();
                    DataTable dt_empleados = proyectos.ListadoEmpleadoProyecto(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]))).Tables[0];
                    string correos = "";
                    foreach (DataRow row in dt_empleados.Rows)
                    {
                        correos = correos + row["correo"].ToString().Trim() + ";";
                        Session["correo_pm"] = correos;
                    }
                    string correos_pm = Session["correo_pm"] as string;
                    CargarCorreosCliente();
                    string correos_clientes = Session["correo_clientes"] as string;
                    EnviarCorreo(correos_clientes + correos_pm, "Terminación de Proyecto", "", 5);
                    string url = "inicio.aspx";
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                        "AlertGO('Proyecto Terminado Correctamente.', '" + url + "');", true);
                }
                else
                {
                    div_errorterminacion.Visible = true;
                    lblerrorterminacion.Text = vmensaje;
                }
            }
            catch (Exception ex)
            {
                div_errorterminacion.Visible = true;
                lblerrorterminacion.Text = ex.ToString();
            }
        }

        protected void lnkcargdenuevo_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            switch (lnk.CommandArgument)
            {
                case "cierre":
                    div_cargacierre.Visible = true;
                    div_okcierre.Visible = false;
                    break;

                case "encuesta":
                    div_cargaencuesta.Visible = true;
                    div_okencuesta.Visible = false;
                    break;

                case "kit":
                    div_cargakit.Visible = true;
                    div_okkit.Visible = false;
                    break;
            }
        }

        protected void lnkagregarnuevocontacto_Click(object sender, EventArgs e)
        {
           // rdlcontacto_clientes.Visible = !rdlcontacto_clientes.Visible;
            div_addnewcontact.Visible = !div_addnewcontact.Visible;
        }

        protected void lnkeditarparticipante_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = sender as LinkButton;
                string nombre = lnk.CommandArgument;
                string pendiente = lnk.CommandName;
                if (ViewState["dt_pendientes"] != null)
                {
                    DataTable dt = ViewState["dt_pendientes"] as DataTable;
                    DataTable dt_filter = dt.Select("descripcion = '"+pendiente.Trim()+ "' and responsable = '"+nombre+"'").CopyToDataTable();
                    if (dt_filter.Rows.Count > 0)
                    {
                        rtxtresponsable.Text = dt_filter.Rows[0]["responsable"].ToString();
                        rtxtpendiente.Text = dt_filter.Rows[0]["descripcion"].ToString();
                        rdtfecha_planeada.SelectedDate = Convert.ToDateTime(dt_filter.Rows[0]["fecha"]);
                        ViewState["pendiente_responsable"] = nombre;
                        ViewState["pendiente_descripcion"] = pendiente;
                    }
                    else {

                        div_pendientes.Visible = true;
                        lblerrorpendientes.Text = "Se genero un error al buscar el pendiente. Contacte a su administrador.";
                        ModalShow("#myModalPendientes");
                    }
                }
            }
            catch (Exception ex)
            {
                div_pendientes.Visible = true;
                lblerrorpendientes.Text = ex.ToString();
                ModalShow("#myModalPendientes");
            }
        }

        protected void btnbuscarempleado_Click(object sender, EventArgs e)
        {
            if (txtbuscarempleadoproyecto.Text.Trim().Length > 2)
            {
                CargarListadoEmpleados(txtbuscarempleadoproyecto.Text.Trim());
                imgloadempleados.Style["display"] = "none";
                lblbe.Style["display"] = "none";
            }
            else
            {
                Alert.ShowAlertInfo("Ingrese un minimo de 3 caracteres para realizar la busqueda.", "Mensaje del Sistema", this);
            }

        }

        protected void btnbuscarempleado2_Click(object sender, EventArgs e)
        {
            if (txtbuscarempleado.Text.Trim().Length > 2)
            {
                CargarListadoEmpleados(txtbuscarempleado.Text.Trim());
                imgloadempleados.Style["display"] = "none";
                lblbe2.Style["display"] = "none";
            }
            else {
                Alert.ShowAlertInfo("Ingrese un minimo de 3 caracteres para realizar la busqueda.","Mensaje del Sistema",this);
            }

        }

        protected void lnkbuscarcliente_Click(object sender, EventArgs e)
        {
            if (txtbuscarclientes.Text.Trim().Length > 2)
            {
                CargarListaClientes(txtbuscarclientes.Text.Trim());
                imgloadcliente.Style["display"] = "none";
                lblloadcliente.Style["display"] = "none";
            }
            else
            {
                Alert.ShowAlertInfo("Ingrese un minimo de 3 caracteres para realizar la busqueda.", "Mensaje del Sistema", this);
            }
        }
    }
}