using negocio.Componentes;
using System;
using datos;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace presentacion
{
    public partial class proyectos_minutas : System.Web.UI.Page
    {
        protected void lnkdashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("proyectos_dashboard.aspx?id_proyecto=" + Request.QueryString["id_proyecto"]);
        }

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
                    CargarMinutas(id_proyecto);
                    DataRow proyecto = dt.Rows[0];
                    lblproyect.Text = proyecto["proyecto"].ToString();
                    lblmonto.Text = Convert.ToDecimal(proyecto["costo_usd"]).ToString("C2") + " USD / " + Convert.ToDecimal(proyecto["costo_mn"]).ToString("C2") + " MN";

                    lbltecnologia.Text = proyecto["tecnologia"].ToString();
                    lblcped.Text = proyecto["cped"].ToString();
                  
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar información del proyecto. " + ex.Message, this);
            }
        }

        private void CargarMinutas(int id_proyecto)
        {
            try
            {
                ProyectosMinutasCOM proyectos = new ProyectosMinutasCOM();
                DataTable dt = proyectos.GetAll(id_proyecto);
                if (dt.Rows.Count > 0)
                {
                    repeater_minutas.DataSource = dt;
                    repeater_minutas.DataBind();
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar información del proyecto. " + ex.Message, this);
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



        protected void lnknuevaminuta_Click(object sender, EventArgs e)
        {
            rtxtasuntominuta.Text = "";
            rtxtlugarminuta.Text = "";
            rtxtpropositos.Text = "";
            txtid_minuta.Text = "";
            ViewState["dt_participantes"] = null;
            rdpfechaminuta.SelectedDate = null;
            CargarParticipantes();
            ModalShow("#myModalMinutas");
        }

        protected void lnkguardarminuta_Click(object sender, EventArgs e)
        {
            try
            {
                string vmensaje = "";
                if (ProyectoTerminado())
                {
                    vmensaje = "El proyecto fue terminado y no puede generarse información adicional.";
                }else if (rtxtasuntominuta.Text == "")
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
                    vmensaje = txtid_minuta.Text == "" ? AgregarMinuta() : EditarMinuta(Convert.ToInt32(txtid_minuta.Text));
                }

                if (vmensaje == "")
                {
                    string url = "proyectos_minutas.aspx?id_proyecto=" + Request.QueryString["id_proyecto"];
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                        "AlertGO('Minuta guardada correctamente', '" + url + "');", true);
                }
                else
                {
                    Toast.Error("Error al cargar guardar minuta: " +vmensaje, this);
                    ModalShow("#myModalMinutas");
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al guardar minuta: " + ex.Message, this);
                ModalShow("#myModalMinutas");
            }
            finally
            {
                lnkcargandoMinuta.Style["display"] = "none";
                lnkguardarminuta.Visible = true;
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
                datos.proyectos_minutas entidad = new datos.proyectos_minutas();
                ProyectosMinutasCOM minutas = new ProyectosMinutasCOM();
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
                List<proyectos_minutas_participantes> participantes = new List<proyectos_minutas_participantes>();
                List<proyectos_minutas_pendientes> pendientes = new List<proyectos_minutas_pendientes>();

                foreach (DataRow row in dt_pendientes.Rows)
                {
                    proyectos_minutas_pendientes pendiente = new proyectos_minutas_pendientes {
                        usuario_resp = row["usuario_resp"].ToString().ToUpper(),
                        descripcion = row["descripcion"].ToString().ToUpper(),
                        avance = Convert.ToByte(row["avance"]),
                        fecha_planeada = Convert.ToDateTime(row["fecha"]),
                        usuario_registro= Session["usuario"] as string,
                        nombre= System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(row["nombre"].ToString().ToLower()),
                        fecha_registro= DateTime.Now
                    };
                    pendientes.Add(pendiente);
                }
                foreach (DataRow row in dt_participantes.Rows)
                {
                    proyectos_minutas_participantes participante = new proyectos_minutas_participantes
                    {
                        usuario = row["usuario"].ToString().ToUpper(),
                        nombre = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(row["nombre"].ToString().ToLower()),
                        rol = row["rol"].ToString(),
                        organización = row["organización"].ToString(),
                        usuario_registro = Session["usuario"] as string,
                        fecha_registro = DateTime.Now
                    };
                    participantes.Add(participante);
                }

                vmensaje = minutas.Agregar(entidad, participantes, pendientes);
                return vmensaje;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Edita  una Minuta
        /// </summary>
        /// <returns></returns>
        private string EditarMinuta(int id_minuta)
        {
            try
            {
                string vmensaje = "";
                datos.proyectos_minutas entidad = new datos.proyectos_minutas();
                ProyectosMinutasCOM minutas = new ProyectosMinutasCOM();
                entidad.id_proyecto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
                entidad.asunto = rtxtasuntominuta.Text;
                entidad.id_proyectomin = id_minuta;
                entidad.fecha = rdpfechaminuta.SelectedDate.Value;
                entidad.lugar = rtxtlugarminuta.Text;
                entidad.acuerdos = rtxtacuerdos.Text;
                entidad.resultados = rtxtresultados.Text;
                entidad.propósito = rtxtpropositos.Text.Trim();
                entidad.usuario_edicion = Session["usuario"] as string;
                entidad.enviada = false;
                DataTable dt_participantes = ViewState["dt_participantes"] == null ? new DataTable() : ViewState["dt_participantes"] as DataTable;
                DataTable dt_pendientes = ViewState["dt_pendientes"] == null ? new DataTable() : ViewState["dt_pendientes"] as DataTable;
                List<proyectos_minutas_participantes> participantes = new List<proyectos_minutas_participantes>();
                List<proyectos_minutas_pendientes> pendientes = new List<proyectos_minutas_pendientes>();

                foreach(DataRow row in dt_pendientes.Rows)
                {
                    proyectos_minutas_pendientes pendiente = new proyectos_minutas_pendientes
                    {
                        id_proyectomin = entidad.id_proyectomin,
                        id_proyectominpen = Convert.ToInt32(row["id_proyectominpen"]),
                        usuario_resp = row["usuario_resp"].ToString().ToUpper(),
                        descripcion = row["descripcion"].ToString().ToUpper(),
                        avance = Convert.ToByte(row["avance"]),
                        fecha_planeada = Convert.ToDateTime(row["fecha"]),
                        usuario_registro = Session["usuario"] as string,
                        fecha_registro = DateTime.Now,
                        usuario_edicion = Session["usuario"] as string,
                        fecha_edicion = DateTime.Now,
                        nombre = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(row["nombre"].ToString().ToLower())
                    };
                    pendientes.Add(pendiente);
                }

                foreach (DataRow row in dt_participantes.Rows)
                {
                    proyectos_minutas_participantes participante = new proyectos_minutas_participantes
                    {
                        id_proyectomin = entidad.id_proyectomin,
                        id_proyectominpart = Convert.ToInt32(row["id_proyectominpart"]),
                        usuario = row["usuario"].ToString().ToUpper(),
                        nombre = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(row["nombre"].ToString().ToLower()),
                        rol = row["rol"].ToString(),
                        organización = row["organización"].ToString(),
                        usuario_registro = Session["usuario"] as string,
                        fecha_registro = DateTime.Now
                    };
                    participantes.Add(participante);
                }

                vmensaje = minutas.Editar(entidad, participantes, pendientes);
                return vmensaje;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        protected void lnkeditminuta_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProyectoTerminado())
                {
                    Toast.Error("El proyecto fue terminado y no puede generarse información adicional.", this);
                } else
                {
                    LinkButton lnk = sender as LinkButton;
                    int id_minuta = Convert.ToInt32(lnk.CommandArgument);
                    string vmensaje = "";
                    switch (lnk.CommandName.ToLower())
                    {
                        case "editar":
                        case "terminar":
                            datos.proyectos_minutas entidad = new datos.proyectos_minutas();
                            entidad.id_proyectomin = id_minuta;
                            ProyectosMinutasCOM minutas = new ProyectosMinutasCOM();
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
                                DataTable selected = view.ToTable("Selected", false, "usuario", "nombre", "organización", "rol", "id_proyectominpart");


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
                                DataTable selected = view.ToTable("Selected", false, "usuario_resp", "responsable", "descripcion", "fecha_planeada", "id_proyectominpen", "avance");
                                selected.Columns["fecha_planeada"].ColumnName = "fecha";
                                selected.Columns["responsable"].ColumnName = "nombre";
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
                                if (0 > 1) //(hdfid_cliente.Value == "" || hdfid_cliente.Value == "0")
                                {
                                    vmensaje = "Para Enviar la Minuta, debe relacionar este proyecto a un cliente.";
                                }
                                else
                                {
                                    //CargarCorreosCliente();
                                    bool es_cliente = Convert.ToBoolean(Session["cliente"]);
                                    string correos_clientes = "";
                                    //if (es_cliente)
                                    //{
                                    //    ProyectosEmpleadosCOM proyectos = new ProyectosEmpleadosCOM();
                                    //    DataTable dt_empleados = proyectos.empleados_proyecto(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"])));
                                    //    string correos = "";
                                    //    foreach (DataRow row in dt_empleados.Rows)
                                    //    {
                                    //        correos = correos + row["correo"].ToString().Trim() + ";";
                                    //        Session["correo_pm"] = correos;
                                    //    }
                                    //    correos_clientes = Session["correo_pm"] as string;
                                    //}
                                    //else
                                    //{
                                    //    correos_clientes = Session["correo_clientes"] as string;
                                    //}
                                    ProyectosEmpleadosCOM empleados = new ProyectosEmpleadosCOM();
                                    DataTable dt_empleados = empleados.empleados_proyecto(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"])));
                                    DataTable dt_empleados_participantes = empleados.participantes_proyectos(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"])));
                                    string correos = "";
                                    foreach (DataRow row in dt_empleados.Rows)
                                    {
                                        correos = correos + row["correo"].ToString().ToLower().Trim() + ";";
                                        Session["correo_pm"] = correos;
                                    }
                                    foreach (DataRow row in dt_empleados_participantes.Rows)
                                    {
                                        correos = correos + row["correo"].ToString().ToLower().Trim() + ";";
                                        Session["correo_pm"] = correos;
                                    }
                                    // correos_clientes = Session["correo_pm"] as string;
                                    if (Session["correo_pm"] != null)
                                    {
                                        ProyectosCOM proyectos = new ProyectosCOM();
                                        datos.proyectos proyecto = proyectos.proyecto(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"])));
                                        string saludo = DateTime.Now.Hour > 13 ? "Buenas tardes" : "Buenos dias";

                                        DataView view = new System.Data.DataView(dt_participantes);
                                        DataTable dt_participantes2 = view.ToTable("Selected", false, "nombre", "correo", "puesto", "organización", "rol");

                                        DataView view2 = new System.Data.DataView(dt_pendientes);
                                        DataTable dt_pendientes2 = view2.ToTable("Selected", false, "responsable", "correo", "puesto", "descripcion", "fecha_planeada");
                                        dt_pendientes2.Columns["fecha_planeada"].ColumnName = "fecha planeada";
                                        dt_pendientes2.Columns["descripcion"].ColumnName = "pendiente";

                                        string mail_to = Session["correo_pm"] as string;
                                        string subject = "Módulo de proyectos - Nueva Minuta";
                                        string informacion =
                                                 "<h4 style='text-align:left;'>Asistente de trabajo y minutas</p><HR NOSHADE WIDTH=400 SIZE=6 COLOR='#e53935' style='border: 2px;border-color:red;width:100%; '>" +
                                                 "<br><br>" +
                                                 "<table style='font-size: 12px;font-family: Verdana; '>" +
                                                 "<tr>" +
                                                     "<th style=' width:50px;background-color: #e53935 ;color:white;padding: 15px;text-align: left;'>Proyecto</th>" +
                                                     "<th style='width: 450px;background-color: #eeeeee;padding: 15px;text-align: left;'>" + proyecto.proyecto + "</th>" +
                                                     "<th style='width:50px;background-color: #e53935 ;color:white;padding: 15px;text-align: left;'>Fecha</th>" +
                                                     "<th style='width:350px;background-color: #eeeeee;padding: 15px;text-align: left;'>" + Convert.ToDateTime(rdpfechaminuta.SelectedDate).ToString("dddd dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX")) + "</th>" +
                                                     "</tr>" +
                                                     "<tr>" +
                                                     "<th style=' width:50px;background-color: #e53935 ;color:white;padding: 15px;text-align: left;'>" + (es_cliente ? "Solicitante" : "PM") + "</th>" +
                                                     "<th style='width: 450px;background-color: #eeeeee;padding: 15px;text-align: left;'>" + System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Convert.ToString(Session["nombre"]).ToString().ToLower()) + "</th>" +
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
                                                 funciones.TableDinamic(dt_participantes2, "tab_parti") +
                                                 "<br><br>" +
                                                 "<h4>Acuerdos tomados y resoluciones</h4><HR NOSHADE WIDTH=400 SIZE=6 COLOR='#e53935' style='border: 2px;border-color:red;width:100%; '>" +
                                                 "<p>" + rtxtacuerdos.Text.Trim() + "</p>" +
                                                 "<br><br>" +
                                                 "<h4>Acciones a realizar y asuntos pendientes</h4><HR NOSHADE WIDTH=400 SIZE=6 COLOR='#e53935' style='border: 2px;border-color:red;width:100%; '>" +
                                                 funciones.TableDinamic(dt_pendientes2, "tab_pendinte");

                                        string mail = "<div>" + saludo + "<div>" +
                                           "<br>" +
                                           "<p>Se agrego una nueva Minuta para el proyecto <strong>" + proyecto.proyecto + "</strong>" +
                                            informacion +
                                           "<br/><p>Este movimiento fue realizado por <strong>" +
                                           System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Session["nombre"].ToString().ToLower())
                                           + "</strong> el dia <strong>" +
                                           DateTime.Now.ToString("dddd dd MMMM, yyyy hh:mm:ss tt", CultureInfo.CreateSpecificCulture("es-MX")) + "</strong>" +
                                           "</p>";
                                        CorreosCOM sendcorreos = new CorreosCOM();
                                        bool correct = sendcorreos.SendMail(mail, subject, mail_to);
                                    }
                                    //Enviamos correos

                                    vmensaje = minutas.Enviar(id_minuta, Session["usuario"] as string);
                                }
                                if (vmensaje == "")
                                {
                                    string url = "proyectos_minutas.aspx?id_proyecto=" + Request.QueryString["id_proyecto"];
                                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                                        "AlertGO('Minuta enviada correctamente', '" + url + "');", true);
                                }
                                else
                                {
                                    Toast.Error("Error al enviar minuta: " + vmensaje, this);
                                }
                            }
                            else
                            {
                                ModalShow("#myModalMinutas");
                            }
                            break;

                        case "delete":
                            string comentarios = hdfmotivos.Value;
                            ProyectosMinutasCOM minutasview = new ProyectosMinutasCOM();
                            vmensaje = minutasview.Eliminar(id_minuta, comentarios, comentarios);
                            if (vmensaje == "")
                            {
                                string url = "proyectos_minutas.aspx?id_proyecto=" + Request.QueryString["id_proyecto"];
                                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                                    "AlertGO('Minuta eliminada correctamente', '" + url + "');", true);
                            }
                            else
                            {
                                Toast.Error("Error al eliminar minuta: " + vmensaje, this);
                            }
                            break;
                    }
                }
    
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar minuta: " + ex.Message, this);
                ModalShow("#myModalMinutas");
            }
            finally
            {
                lnkcargandoMinuta.Style["display"] = "none";
                lnkguardarminuta.Visible = true;
            }
        }


        protected void lnkagregar_Click(object sender, EventArgs e)
        {
            rtxtnombreparticipante.ReadOnly = !rtxtnombreparticipante.ReadOnly;
            rtxtrol.ReadOnly = !rtxtrol.ReadOnly;
            rtxtorganizacion.ReadOnly = !rtxtorganizacion.ReadOnly;
            rtxtnombreparticipante.Text = "";
            rtxtrol.Text = "";
            rtxtorganizacion.Text = "";
            hdf_usuario_participante.Value = "";
        }

        protected void lnkpendientes_Click(object sender, EventArgs e)
        {
            CargarDatosFiltros("");
            rtxtpendiente.Text = "";
            ProyectosMinutasCOM minutas = new ProyectosMinutasCOM();
            DataTable dt_pendientes = minutas.GetAllPendientes(Convert.ToInt32(txtid_minuta.Text == "" ? "0" : txtid_minuta.Text));
            if (dt_pendientes.Rows.Count > 0)
            {
                DataView view = new System.Data.DataView(dt_pendientes);
                DataTable selected = view.ToTable("Selected", false, "usuario_resp", "responsable", "descripcion", "fecha_planeada", "id_proyectominpen", "avance");
                selected.Columns["fecha_planeada"].ColumnName = "fecha";
                selected.Columns["responsable"].ColumnName = "nombre";
                ViewState["dt_pendientes"] = selected;
                CargarPendientes();
            }
            else
            {
                CargarPendientes();
            }
            rdlinvopendientes.ClearSelection();
            ModalShow("#myModalPendientes");
        }

        protected void lnkeliminarparticipante_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProyectoTerminado())
                {
                    Toast.Error("El proyecto fue terminado y no puede generarse información adicional.", this);
                }
                else
                {
                    LinkButton lnk = sender as LinkButton;
                    string nombre = lnk.CommandArgument;
                    DeleteTableParticipantes(nombre, "", "");
                    CargarParticipantes();
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar guardar participante: " + ex.Message, this);
                ModalShow("#myModalMinutas");
            }
        }

        protected void lnkparticipantes_Click(object sender, EventArgs e)
        {
            ProyectosMinutasCOM minutas = new ProyectosMinutasCOM();
            DataTable dt_participantes = minutas.GetAllParticipante(Convert.ToInt32(txtid_minuta.Text == "" ? "0" : txtid_minuta.Text));
            if (dt_participantes.Rows.Count > 0)
            {
                DataView view = new System.Data.DataView(dt_participantes);
                DataTable selected = view.ToTable("Selected", false, "usuario", "nombre", "organización", "rol", "id_proyectominpart");


                ViewState["dt_participantes"] = selected;
                CargarParticipantes();
            }
            else
            {
                CargarParticipantes();
            }
            CargarDatosFiltros("");
            rtxtnombreparticipante.ReadOnly = true;
            rtxtrol.ReadOnly = true;
            rtxtorganizacion.ReadOnly = true;
            rtxtnombreparticipante.Text = "";
            rtxtrol.Text = "";
            hdf_usuario_participante.Value = "";
            rtxtorganizacion.Text = "";
            ModalShow("#myModalParticipantes");
        }

        protected void lnkaddparticipante_Click(object sender, EventArgs e)
        {
            try
            {
                string usuario = hdf_usuario_participante.Value.ToUpper();
                if (ProyectoTerminado())
                {
                    Toast.Error("El proyecto fue terminado y no puede generarse información adicional.", this);
                }else if (rtxtnombreparticipante.Text == "")
                {
                    Toast.Error("Error al cargar guardar participante: " + "Ingrese el Nombre del Participante", this);
                }
                else if (rtxtrol.Text == "")
                {
                    Toast.Error("Error al cargar guardar participante: " + "Ingrese el Rol del Participante", this);
                }
                else if (rtxtorganizacion.Text == "")
                {
                    Toast.Error("Error al cargar guardar participante: " + "Ingrese la Organización del Participante", this);
                }
                else
                {
                    AddTableParticipantes(0, usuario, rtxtnombreparticipante.Text, rtxtrol.Text, rtxtorganizacion.Text, 0);
                    rtxtnombreparticipante.Text = "";
                    rtxtrol.Text = "";
                    hdf_usuario_participante.Value = "";
                    rtxtorganizacion.Text = "";
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar guardar participante: " + ex.Message, this);
                ModalShow("#myModalMinutas");
            }
        }

        protected void lnkaddpendientes_Click(object sender, EventArgs e)
        {
            try
            {

                IList<RadListBoxItem> collection = rdlinvopendientes.SelectedItems;
                string usuario_Resp = ddlempleado_a_consultar.SelectedValue;
                string nombre_Responsable = ddlempleado_a_consultar.SelectedItem.ToString();
                if (ProyectoTerminado())
                {
                    Toast.Error("El proyecto fue terminado y no puede generarse información adicional.", this);
                }
                else if (usuario_Resp == "" && collection.Count == 0)
                {
                    Toast.Error("Error al cargar guardar pendientes: " + "Ingrese el Nombre del Responsable o Seleccione uno de la lista", this);
                }
                else if (rtxtpendiente.Text == "")
                {
                    Toast.Error("Error al cargar guardar pendientes: " + "Ingrese la descripcion del pendiete", this);
                }
                else if (!rdtfecha_planeada.SelectedDate.HasValue)
                {
                    Toast.Error("Error al cargar guardar pendientes: " + "Ingrese la fecha planeada", this);
                }
                else
                {
                    txtavancependientes.Text = txtavancependientes.Text == "" ? "0" : txtavancependientes.Text;
                    if (ViewState["pendiente_responsable"] != null && ViewState["pendiente_descripcion"] != null)
                    {
                        string nombre = ViewState["pendiente_responsable"] as string;
                        string pendiente = ViewState["pendiente_descripcion"] as string;
                        DeleteTablePendientes(nombre, pendiente);
                    }
                    int id_pendiente = hdf_id_proyectominpen.Value == "" ? 0 : Convert.ToInt32(hdf_id_proyectominpen.Value);
                    AddTablePendientes(id_pendiente, nombre_Responsable, usuario_Resp, rtxtpendiente.Text,
                           Convert.ToDateTime(rdtfecha_planeada.SelectedDate), 0, Convert.ToInt32(txtavancependientes.Text));
                    rtxtpendiente.Text = "";
                    txtavancependientes.Text = "0";
                    rdtfecha_planeada.SelectedDate = DateTime.Now;
                    rtxtorganizacion.Text = "";
                    CargarDatosFiltros("");
                    rdlinvopendientes.ClearSelection();
                    ViewState["pendiente_responsable"] = null;
                    ViewState["pendiente_descripcion"] = null;
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar guardar pendientes: " + ex.Message, this);
                ModalShow("#myModalPendientes");
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

        protected void lnkeditarparticipante_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProyectoTerminado())
                {
                    Toast.Error("El proyecto fue terminado y no puede generarse información adicional.", this);
                }
                else
                {
                    LinkButton lnk = sender as LinkButton;
                    string nombre = lnk.CommandArgument;
                    string pendiente = lnk.CommandName;
                    int id_proyectominpen = Convert.ToInt32(lnk.Attributes["id_proyectominpen"]);
                    hdf_id_proyectominpen.Value = "";
                    if (ViewState["dt_pendientes"] != null)
                    {
                        DataTable dt = ViewState["dt_pendientes"] as DataTable;
                        DataTable dt_filter = dt.Select("descripcion = '" + pendiente.Trim() + "' and usuario_resp = '" + nombre + "'").CopyToDataTable();
                        if (dt_filter.Rows.Count > 0)
                        {
                            CargarDatosFiltros("");
                            hdf_id_proyectominpen.Value = id_proyectominpen.ToString();
                            txtavancependientes.Text = dt_filter.Rows[0]["avance"].ToString();
                            ddlempleado_a_consultar.SelectedValue = dt_filter.Rows[0]["usuario_resp"].ToString();
                            rtxtpendiente.Text = dt_filter.Rows[0]["descripcion"].ToString();
                            rdtfecha_planeada.SelectedDate = Convert.ToDateTime(dt_filter.Rows[0]["fecha"]);
                            ViewState["pendiente_responsable"] = nombre;
                            ViewState["pendiente_descripcion"] = pendiente;
                        }
                        else
                        {
                            Toast.Error("Error al cargar pendiente: " + "Se genero un error al buscar el pendiente. Contacte a su administrador.", this);
                            ModalShow("#myModalPendientes");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar pendiente: " + ex.Message, this);
                ModalShow("#myModalPendientes");
            }
        }

        protected void lnkeliminarpendiente_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProyectoTerminado())
                {
                    Toast.Error("El proyecto fue terminado y no puede generarse información adicional.", this);
                }
                else
                {
                    rdtfecha_planeada.SelectedDate = DateTime.Now;
                    txtavancependientes.Text = "0";
                    rtxtpendiente.Text = "";
                    LinkButton lnk = sender as LinkButton;
                    string nombre = lnk.CommandArgument;
                    string pendiente = lnk.CommandName;
                    DeleteTablePendientes(nombre, pendiente);
                    CargarPendientes();
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al eliminar pendiente: " + ex.Message, this);
                ModalShow("#myModalPendientes");
            }
        }

        private String AddTableParticipantes(int id_proyectominpart, string usuario,string nombre, string rol, string organizacion, int id_pinvolucrado)
        {
            try
            {
                if (ViewState["dt_participantes"] == null)
                {
                    DataTable ndt = new DataTable();
                    ndt.Columns.Add("usuario");
                    ndt.Columns.Add("nombre");
                    ndt.Columns.Add("rol");
                    ndt.Columns.Add("organización");
                    ndt.Columns.Add("id_proyectominpart");
                   // ndt.Columns.Add("id_pinvolucrado");
                    ViewState["dt_participantes"] = ndt;
                }
                DeleteTableParticipantes(usuario, rol, organizacion);
                DataTable dt = ViewState["dt_participantes"] as DataTable;
                DataRow row = dt.NewRow();
                row["usuario"] = usuario;
                row["nombre"] = nombre;
                row["rol"] = rol;
                row["id_proyectominpart"] = id_proyectominpart;
                row["organización"] = organizacion;
               // row["id_pinvolucrado"] = id_pinvolucrado;
                dt.Rows.Add(row);
                ViewState["dt_participantes"] = dt;
                CargarParticipantes();
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private String AddTablePendientes(int id_proyectominpen, string nombre,string responsable, string descripcion, DateTime fecha, int id_pinvolucrado, int avance)
        {
            try
            {
                if (ViewState["dt_pendientes"] == null)
                {
                    DataTable ndt = new DataTable();
                    ndt.Columns.Add("usuario_resp");
                    ndt.Columns.Add("descripcion");
                    ndt.Columns.Add("fecha");
                    ndt.Columns.Add("avance");
                    ndt.Columns.Add("nombre");
                    ndt.Columns.Add("id_proyectominpen");
                    //ndt.Columns.Add("id_pinvolucrado");
                    ViewState["dt_pendientes"] = ndt;
                }
                DeleteTablePendientes(responsable, descripcion);
                DataTable dt = ViewState["dt_pendientes"] as DataTable;
                DataRow row = dt.NewRow();
                row["usuario_resp"] = responsable;
                row["descripcion"] = descripcion;
                row["fecha"] = fecha;
                row["avance"] = avance;
                row["nombre"] = nombre;
                row["id_proyectominpen"] = id_proyectominpen;
                //row["id_pinvolucrado"] = id_pinvolucrado;
                dt.Rows.Add(row);
                ViewState["dt_pendientes"] = dt;
                CargarPendientes();
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
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
                ddlempleado_participante.DataValueField = "usuario";
                ddlempleado_participante.DataTextField = "nombre";
                ddlempleado_participante.DataSource = dt_empleados;
                ddlempleado_participante.DataBind();

                //ddlempleado_a_consultar.Enabled = ver_Todos_los_empleados;
                //div_filtro_empleados.Visible = ver_Todos_los_empleados;
            }
            catch (Exception ex)
            {
                Toast.Error("Error al iniciar modal de filtros: " + ex.Message, this);
            }
        }

        protected void lnksearch_Click(object sender, EventArgs e)
        {
            string filter = txtfilterempleado.Text;
            try
            {
                if (ProyectoTerminado())
                {
                    Toast.Error("El proyecto fue terminado y no puede generarse información adicional.", this);
                }
                else
                {
                    if (filter.Length == 0 || filter.Length > 3)
                    {
                        CargarDatosFiltros(filter);
                    }
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

        private void DeleteTablePendientes(string responsable, string descripcion)
        {
            try
            {
                DataTable dt = ViewState["dt_pendientes"] as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    if (responsable == row["usuario_resp"].ToString().Trim() && descripcion == row["descripcion"].ToString().Trim())
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

        protected void lnkserach2_Click(object sender, EventArgs e)
        {
            string filter = txtfiltroempleado2.Text;
            try
            {
                if (ProyectoTerminado())
                {
                    Toast.Error("El proyecto fue terminado y no puede generarse información adicional.", this);
                }
                else
                {
                    if (filter.Length == 0 || filter.Length > 3)
                    {
                        CargarDatosFiltros(filter);
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al filtrar empleados: " + ex.Message, this);
            }
            finally
            {
                imgempleado.Style["display"] = "none";
                lblbemp.Style["display"] = "none";
            }

        }

        protected void ddlempleado_participante_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ProyectoTerminado())
            {
                Toast.Error("El proyecto fue terminado y no puede generarse información adicional.", this);
            }
            else
            {
                rtxtnombreparticipante.ReadOnly = true;
                rtxtrol.ReadOnly = true;
                rtxtorganizacion.ReadOnly = true;
                string usuario = ddlempleado_participante.SelectedValue.ToString();
                string nombre = ddlempleado_participante.SelectedItem.ToString();
                rtxtnombreparticipante.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(nombre.ToLower());
                hdf_usuario_participante.Value = usuario;
                rtxtrol.Text = "Empleado Connext";
                rtxtorganizacion.Text = "Connext";
            }
        }

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

    }
}