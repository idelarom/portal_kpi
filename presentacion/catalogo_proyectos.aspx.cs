using datos.Modelos;
using negocio.Componentes;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class catalogo_proyectos : System.Web.UI.Page
    {
        #region FUNCIONES

        private void ModalShow(string modalname)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                             "ModalShow('" + modalname + "');", true);
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
                    entidad.id_proyecto = Convert.ToInt32(txtid_proyecto.Text.Trim());
                    entidad.proyecto = proyecto;
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
                funciones.ActualizaAvances();
                return vmensaje;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        private void CargarProyectos(int id_proyecto, string usuario, bool administrador)
        {
            try
            {
                ProyectosCOM proyectos = new ProyectosCOM();
                DataTable dt = proyectos.sp_get_proyects_info(id_proyecto, usuario, administrador, Convert.ToInt32(Session["id_cliente"]),"").Tables[0];
                DataRow row = dt.Rows[0];
                txtid_proyecto.Text = id_proyecto.ToString();
                rtxtproyecto.Text = row["proyecto"].ToString();
                rtxtdescripcion.Text = row["descripcion"].ToString();
                rtxtavance.Text = row["avance"].ToString();
                if (row["fecha_inicio"] != DBNull.Value)
                {
                    rdtpinicio.SelectedDate = Convert.ToDateTime(row["fecha_inicio"]);
                    rdtpfin.SelectedDate = Convert.ToDateTime(row["fecha_fin"]);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        public string AgregarProyecto()
        {
            try
            {
                int id_proyect = 0;
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
                    entidad.proyecto = proyecto;
                    entidad.descripcion = descripcion;
                    entidad.fecha_inicio = dinicial;
                    entidad.codigo_proyecto = "1";
                    entidad.fecha_fin = difinal;
                    entidad.fecha_inicio_str = fechainicio;
                    entidad.fecha_fin_str = fechafin;
                    entidad.usuario = Session["usuario"] as string;
                    entidad.avance = Convert.ToByte(avance);
                    entidad.duración = (difinal.Date - dinicial.Date).TotalDays == 1 ?
                        (difinal.Date - dinicial.Date).TotalHours.ToString() + " horas" : (difinal.Date - dinicial.Date).TotalDays.ToString() + " dias";
                    ProyectosCOM proyecto_ = new ProyectosCOM();
                    string[] return_array = proyecto_.Agregar(entidad);
                    vmensaje = return_array[0];
                    id_proyect = Convert.ToInt32(return_array[1]);
                    usuarios entidadus = new usuarios();
                    entidadus.id_cliente = null;
                    entidadus.id_uperfil = 3;
                    entidadus.usuario = Convert.ToString(Session["usuario"]).Trim().ToUpper();
                    entidadus.password = Convert.ToString(Session["contraseña"]).Trim().ToUpper();
                    UsuariosCOM usuarios = new UsuariosCOM();                   
                    if (vmensaje == "")
                    {
                        vmensaje = usuarios.Agregar(entidadus)[0];
                    }
                }
                if (vmensaje == "")
                {
                    funciones.ActualizaAvances();
                    ProyectosCOM proyecto_actualiza = new ProyectosCOM();
                    DataSet dsss = proyecto_actualiza.actualizar_avances();
                 
                }
                return vmensaje;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        private void CargarProyectos(string usuario, bool administrador)
        {
            try
            {
                ProyectosCOM proyectos = new ProyectosCOM();
                DataTable dt = proyectos.sp_get_proyects_info(0, usuario, administrador, Convert.ToInt32(Session["id_cliente"]),"").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    bool cliente = Convert.ToBoolean(Session["cliente"]);
                    grid_proyectos.DataSource = dt;
                    grid_proyectos.DataBind();
                    grid_proyectos.Columns[0].Visible = !cliente;
                    grid_proyectos.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// Actualiza el Proyecto mediante archivo de Excel
        /// </summary>
        private void AgregarProyectoExcel(Byte[] archivo, string file_name, string ext, string tamaño, string contentType, bool publico)
        {
            try
            {
                DataTable dt = ViewState["dt_comidas"] as DataTable;
                int id_proyect = 0;
                if (dt.Rows.Count > 0)
                {
                    bool isEnglish = funciones.Format(dt);
                    string vmensaje = "";
                    foreach (DataRow row in dt.Rows)
                    {
                        int nivel = Convert.ToInt32(row["NIVEL_DE_ESQUEMA"]);
                        if (id_proyect == 0 && nivel >= 2)
                        {
                            break;
                        }
                        switch (nivel)
                        {
                            case 1:
                                proyectos entidad = new proyectos();
                                entidad.codigo_proyecto = row["ID"].ToString().Trim();
                                entidad.proyecto = row["NOMBRE"].ToString().Trim();
                                entidad.duración = row["DURACIÓN"].ToString().Trim();
                                entidad.fecha_inicio = Convert.ToDateTime(funciones.RetunrFirmatDate(row["COMIENZO"].ToString().Trim(), isEnglish));
                                entidad.fecha_inicio_str = Convert.ToDateTime(entidad.fecha_inicio).ToString("dddd dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
                                entidad.fecha_fin = Convert.ToDateTime(funciones.RetunrFirmatDate(row["FIN"].ToString().Trim(), isEnglish));
                                entidad.fecha_fin_str = Convert.ToDateTime(entidad.fecha_fin).ToString("dddd dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
                                entidad.usuario = Session["usuario"] as string;
                                entidad.avance = Convert.ToByte(row["Porcentaje_completado"].ToString().Replace("%", "").Trim() == "" ? "0" : row["Porcentaje_completado"].ToString().Replace("%", "").Trim());
                                ProyectosCOM proyectos = new ProyectosCOM();
                                string[] return_array = proyectos.Agregar(entidad);
                                vmensaje = return_array[0];
                                if (vmensaje != "")
                                {
                                    div_errormodal2.Visible = true;
                                    lblerrormodal2.Text = "Error al subir el proyecto." + vmensaje.ToString();
                                    ModalShow("#myModalExcel");
                                    break;
                                }
                                else
                                {
                                    id_proyect = Convert.ToInt32(return_array[1]);
                                    usuarios entidadus = new usuarios();
                                    entidadus.id_cliente = null;
                                    entidadus.id_uperfil = 3;
                                    entidadus.usuario = Convert.ToString(Session["usuario"]).Trim().ToUpper();
                                    entidadus.password = Convert.ToString(Session["contraseña"]).Trim().ToUpper();
                                    UsuariosCOM pusuarios = new UsuariosCOM();
                                    if (pusuarios.Agregar(entidadus)[0] != "")
                                    {
                                        div_errormodal2.Visible = true;
                                        lblerrormodal2.Text = "Error al subir el proyecto. Hubo un error al anexar al usuario. Verifique esta situación con su administrador.";
                                        ModalShow("#myModalExcel");
                                        break;
                                    }
                                }
                                break;

                            case 2:
                            default:
                                proyectos_tareas entidad2 = new proyectos_tareas();
                                entidad2.id_proyecto = id_proyect;
                                entidad2.codigo_tarea = row["ID"].ToString().Trim();
                                entidad2.tarea = row["NOMBRE"].ToString().Trim();
                                entidad2.duración = row["DURACIÓN"].ToString().Trim();
                                entidad2.fecha_inicio = Convert.ToDateTime(funciones.RetunrFirmatDate(row["COMIENZO"].ToString().Trim(), isEnglish));
                                entidad2.fecha_inicio_str = Convert.ToDateTime(entidad2.fecha_inicio).ToString("dddd dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
                                entidad2.fecha_fin = Convert.ToDateTime(funciones.RetunrFirmatDate(row["FIN"].ToString().Trim(), isEnglish));
                                entidad2.fecha_fin_str = Convert.ToDateTime(entidad2.fecha_fin).ToString("dddd dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
                                entidad2.usuario = Session["usuario"] as string;
                                entidad2.avance = Convert.ToByte(row["Porcentaje_completado"].ToString().Replace("%", "").Trim() == "" ? "0" : row["Porcentaje_completado"].ToString().Replace("%", "").Trim());
                                entidad2.recursos = row["Nombres_de_los_recursos"].ToString().Trim(); ;
                                entidad2.actividades_predecesoras = row["Predecesoras"].ToString().Trim();
                                entidad2.nivel_esquema = Convert.ToByte(row["Nivel_de_esquema"].ToString().Replace("%", "").Trim() == "" ? "0" : row["Nivel_de_esquema"].ToString().Replace("%", "").Trim());
                                TareasCOM tareas = new TareasCOM();
                                string[] return_array2 = tareas.Agregar(entidad2);
                                vmensaje = return_array2[0];
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
                    if (vmensaje == "")
                    {
                        TareasCOM componente = new TareasCOM();
                        DataSet ds = componente.sp_genera_tareas_padres(id_proyect);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (vmensaje == "")
                        {
                            funciones.ActualizaAvances();
                            ProyectosCOM proyecto_actualiza = new ProyectosCOM();
                            DataSet dsss = proyecto_actualiza.actualizar_avances();

                            AgregarDocumento(id_proyect, archivo, file_name, ext, tamaño, contentType, true);
                            string url = "proyecto_general.aspx?id_proyecto=" + funciones.deTextoa64(id_proyect.ToString());
                            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                                "ModalClose();", true);
                            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                                "AlertGO('Proyecto Subido Correctamente', '" + url + "');", true);
                        }
                        else
                        {
                            div_errormodal2.Visible = true;
                            lblerrormodal2.Text = "ERROR CON LA RELACION DE TAREAS";
                            ModalShow("#myModalExcel");
                        }
                    }
                    else
                    {
                        div_errormodal2.Visible = true;
                        lblerrormodal2.Text = vmensaje;
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

        #endregion FUNCIONES

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string usuario = Session["usuario"] as string;
                CargarProyectos(usuario, Convert.ToBoolean(Session["administrador"]));
                Boolean cliente = Convert.ToBoolean(Session["cliente"]);
                lnknuevoproyecto.Visible = !cliente;
                lnknuevoproyectoExcel.Visible = !cliente;
            }
        }

        protected void lnkeditar_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            int id_proyecto = Convert.ToInt32(lnk.CommandArgument);
            CargarProyectos(id_proyecto, Session["usuario"] as string, Convert.ToBoolean(Session["administrador"]));
            ModalShow("#myModal");
        }

        protected void lnkeditar_cambios_Click(object sender, EventArgs e)
        {
            div_errormodal.Visible = false;
            lblerrormodal.Text = "";
            string vmensaje = txtid_proyecto.Text==""? AgregarProyecto(): ActualizarProyecto();
            if (vmensaje == "")
            {
                string url = "catalogo_proyectos.aspx";
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                    "ModalClose();", true);
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                    "AlertGO('Proyecto Guardado Correctamente', '" + url + "');", true);
            }
            else
            {
                div_errormodal.Visible = true;
                lblerrormodal.Text = vmensaje;
            }
        }

        private String AgregarDocumento(int id_proyecto, Byte[] archivo, string file_name, string ext, string tamaño, string contentType, bool publico)
        {
            try
            {
                proyectos_documentos entidad = new proyectos_documentos();
                DocumentosCOM documentos = new DocumentosCOM();
                entidad.id_proyecto = id_proyecto;
                entidad.archivo = archivo;
                entidad.extension = ext;
                entidad.nombre = file_name.Replace(ext, "");
                entidad.tamaño = tamaño;
                entidad.contentType = contentType;
                entidad.usuario = Session["usuario"] as string;
                entidad.publico = publico;
                entidad.archivo_proyecto = true;
                entidad.kit_cliente = false;
                entidad.documento_cierre = false;
                entidad.encuesta = false;
                string[] return_array = documentos.Agregar(entidad);
                return return_array[0].ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
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
                        lblerrormodal2.Text = "EL ARCHIVO CSV NO TIENE EL FORMATO COMPLETO, NO SE ENCONTRO LA COLUMNA: Nivel_de_esquema";
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
                        AgregarProyectoExcel(archivo, filename, ext, tamaño, contenttype, true);
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

        protected void lnknuevoproyecto_Click(object sender, EventArgs e)
        {
            div_errormodal.Visible = false;
            lblerrormodal.Text = "";
            txtid_proyecto.Text = "";
            ModalShow("#myModal");
        }

        protected void lnknuevoproyectoExcel_Click(object sender, EventArgs e)
        {
            ModalShow("#myModalExcel");
            
        }
    }
}