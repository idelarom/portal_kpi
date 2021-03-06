﻿using datos;
using datos.Model;
using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using Telerik.Web.UI;

namespace presentacion.Pages.Proyectos
{
    public partial class mis_proyectos : System.Web.UI.Page
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
                Cargarddlstatus();
                Cargarddltechnology();
                CargarProyectos(1);
            }
        }

        private DataTable GetProyectos(int id_proyecto_estatus)
        {
            DataTable dt = new DataTable();
            try
            {
                int num_empleado = Convert.ToInt32(Session["num_empleado"]);
                Boolean ver_Todos_los_empleados = Convert.ToBoolean(Session["ver_Todos_los_empleados"]);
                if (funciones.Permisos(Session["usuario"] as string, 4))
                {
                    ver_Todos_los_empleados = true;
                }
                ProyectosCOM Proyectos = new ProyectosCOM();
                dt = Proyectos.SelectAll(num_empleado, ver_Todos_los_empleados, id_proyecto_estatus);
            }
            catch (Exception ex)
            {
                dt = new DataTable();
            }
            return dt;
        }     

        private proyectos GetProyecto(int id_proyecto)
        {
            proyectos dt = new proyectos();
            try
            {
                ProyectosCOM Proyecto = new ProyectosCOM();
                dt = Proyecto.proyecto(id_proyecto);
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }  

        private void CargarProyectos(int id_proyecto_estatus)
        {
            try
            {               
                DataTable dt = GetProyectos(id_proyecto_estatus);
                repeat_proyectos.DataSource = dt;
                repeat_proyectos.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar proyectos: " + ex.Message, this);
            }
        }

        private void Cargarddlstatus()
        {
            try
            {
                ProyectosEstatusCOM estatus = new ProyectosEstatusCOM();
                DataTable dt_estatus = new DataTable();
                dt_estatus = estatus.SelectAll();
                ddlstatus.DataValueField = "id_proyecto_estatus";
                ddlstatus.DataTextField = "estatus";
                ddlstatus.DataSource = dt_estatus;
                ddlstatus.DataBind();
                ddlstatus.Items.Insert(0, new ListItem("Todos", "0"));
                ddlstatus.SelectedValue = "1";
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar combo de estatus : " + ex.Message, this);
            }
        }

        private void Cargarddltechnology()
        {
            try
            {
                ProyectosTecnologiasCOM tegnologia = new ProyectosTecnologiasCOM();
                DataTable dt_tegnologia = new DataTable();
                dt_tegnologia = tegnologia.SelectAll();
                ddltechnology.DataValueField = "id_proyecto_tecnologia";
                ddltechnology.DataTextField = "nombre";
                ddltechnology.DataSource = dt_tegnologia;
                ddltechnology.DataBind();
                ddltechnology.Items.Insert(0, new ListItem("Todos", "0"));
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar combo de tegnologias : " + ex.Message, this);
            }
        }

        private void Cargarddlperiodo()
        {
            try
            {
                ProyectosPeriodosCOM periodo = new ProyectosPeriodosCOM();
                DataTable dt_periodos = new DataTable();
                dt_periodos = periodo.SelectAll();
                ddlperiodo.DataValueField = "id_proyecto_periodo";
                ddlperiodo.DataTextField = "nombre";
                ddlperiodo.DataSource = dt_periodos;
                ddlperiodo.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar combo de periodos: " + ex.Message, this);
            }
        }

        private void Cargarddlestatus()
        {
            try
            {
                ProyectosEstatusCOM estatus = new ProyectosEstatusCOM();
                DataTable dt_estatus = new DataTable();
                dt_estatus = estatus.SelectAll();
                ddlestatus.DataValueField = "id_proyecto_estatus";
                ddlestatus.DataTextField = "estatus";
                ddlestatus.DataSource = dt_estatus;
                ddlestatus.DataBind();
                
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar combo de estatus : " + ex.Message, this);
            }
        }

        private void Cargarddltegnologia()
        {
            try
            {
                ProyectosTecnologiasCOM tegnologia = new ProyectosTecnologiasCOM();
                DataTable dt_tegnologia = new DataTable();
                dt_tegnologia = tegnologia.SelectAll();
                ddltegnologia.DataValueField = "id_proyecto_tecnologia";
                ddltegnologia.DataTextField = "nombre";
                ddltegnologia.DataSource = dt_tegnologia;
                ddltegnologia.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar combo de tegnologias : " + ex.Message, this);
            }
        }

        private string Agregar(proyectos id_proyecto, List<proyectos_historial_tecnologias> tecnologias)
        {
            ProyectosCOM Proyecto = new ProyectosCOM();
            string vmensaje = Proyecto.Agregar(id_proyecto, tecnologias);

            return vmensaje;
        }
        private string Editar(proyectos id_proyecto, List<proyectos_historial_tecnologias> tecnologias)
        {
            ProyectosCOM Proyecto = new ProyectosCOM();
            string vmensaje = Proyecto.Editar(id_proyecto, tecnologias);

            return vmensaje;
        }

        private string Eliminar(proyectos id_proyecto)
        {
            ProyectosCOM Proyecto = new ProyectosCOM();
            string vmensaje = Proyecto.Elimina(id_proyecto);

            return vmensaje;
        }

        private string FolioOportunidad(string cveoport)
        {           
            OportunidadesCOM cveoportunidad = new OportunidadesCOM();
            string vmensaje = cveoportunidad.ExistFolioOport(cveoport);            
            return vmensaje;
        }

        private string foliopmtracker(String Folio_Pm)
        {
            OportunidadesCOM foliopm = new OportunidadesCOM();
            string vmensaje = foliopm.ExistFoliopm(Folio_Pm);
            return vmensaje;
        }

        private CPED Getcped(string documento)
        {
            CpedCOM Documento = new CpedCOM();
            CPED cped = Documento.cped(documento);
            return cped;
        }

        private Boolean Permisos(string usuario, int id_permiso)
        {
            UsuariosCOM usuarioP = new UsuariosCOM();
            Boolean permiso = usuarioP.ExistPermission(usuario, id_permiso);
            return permiso;
        }

        protected void CargarDatosempleados(string filtro)
        {
            try
            {

                int NumJefe = Convert.ToInt32(Session["NumJefe"]);
                int num_empleado = Convert.ToInt32(Session["num_empleado"]);
                Boolean ver_Todos_los_empleados = Convert.ToBoolean(Session["ver_Todos_los_empleados"]);
                if (funciones.Permisos(Session["usuario"] as string, 4))
                {
                    ver_Todos_los_empleados = true;
                }
                EmpleadosCOM empleados = new EmpleadosCOM();
                bool no_activos = false;
                DataSet ds = empleados.sp_listado_empleados(num_empleado, ver_Todos_los_empleados, no_activos);
                DataTable dt_empleados = new DataTable();
                if (filtro != "")
                {
                    DataView dv_empleados = ds.Tables[0].DefaultView;
                    dv_empleados.RowFilter = "nombre like '%" + filtro + "%'";
                    if (dv_empleados.ToTable().Rows.Count <= 0)
                    {
                        dv_empleados.RowFilter = "usuario like '%" + filtro + "%'";
                    }
                    dt_empleados = dv_empleados.ToTable();
                    if (dt_empleados.Rows.Count == 1)
                    {
                        int num_jefe = Convert.ToInt32(dt_empleados.Rows[0]["num_empleado"].ToString());
                        CargarListadoEmpleado(num_jefe, false);
                    }
                }
                else
                {
                    dt_empleados = ds.Tables[0];
                }
                ddlempleado_a_consultar.DataValueField = "num_empleado";
                ddlempleado_a_consultar.DataTextField = "nombre";
                ddlempleado_a_consultar.DataSource = dt_empleados;
                ddlempleado_a_consultar.DataBind();
                if (!ver_Todos_los_empleados)
                {
                    CargarListadoEmpleado(num_empleado, false);
                    ddlempleado_a_consultar.SelectedValue = num_empleado.ToString();
                    //lnkagregartodos_Click(null, null);
                }

                ddlempleado_a_consultar.Enabled = ver_Todos_los_empleados;
                div_filtro_empleados.Visible = ver_Todos_los_empleados;
            }
            catch (Exception ex)
            {
                Toast.Error("Error al iniciar busqueda de empleados: " + ex.Message, this);
            }
            finally
            {

            }
        }

        protected void CargarListadoEmpleado(int num_jefe, Boolean ver_Todos_los_empleados)
        {
            try
            {
                EmpleadosCOM empleados = new EmpleadosCOM();
                bool no_activos = false;
                DataSet ds = empleados.sp_listado_empleados(num_jefe, ver_Todos_los_empleados, no_activos);
                DataTable dt = ds.Tables[0];
                List<SiteDataItem> siteData = new List<SiteDataItem>();
                foreach (DataRow row in dt.Rows)
                {
                    siteData.Add(new SiteDataItem(
                        Convert.ToInt32(row["num_empleado"]),
                        Convert.ToInt32(row["numjefe"]),
                        row["nombre"].ToString(),
                        row["usuario"].ToString()
                        ));
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar listado de empleados: " + ex.Message, this);
            }
        }

        protected void lnknuevoproyecto_Click(object sender, EventArgs e)
        {
            txtnombreproyecto.Text = "";
            txtdescripcion.Text = "";
            ddlperiodo.SelectedIndex = 0;
            ddlestatus.SelectedIndex = 0;
            txtcveop.Text = "";
            txtfolopmt.Text = "";
            txtcped.Text = "";
            txtmonto.Text = "";
            txtmontomn.Text = "";
            Cargarddlperiodo();
            Cargarddlestatus();
            Cargarddltegnologia();
            CargarDatosempleados("");
            rdpfechainicial.SelectedDate = DateTime.Today;
            rdpfechafinal.SelectedDate = DateTime.Today;

            txtcveop.BorderColor = System.Drawing.Color.Silver;
            txtfolopmt.BorderColor = System.Drawing.Color.Silver;
            txtcped.BorderColor = System.Drawing.Color.Silver;

            if (Request.QueryString["filter"] != null)
            {
                //lnkfiltros_Click(null, null);
                string num_empleado = Convert.ToString(Session["num_empleado"]);
                ListItem itwm = ddlempleado_a_consultar.Items.FindByValue(num_empleado);
                if (Items != null)
                {
                    ddlempleado_a_consultar.SelectedValue = num_empleado;
                    //ddlempleado_a_consultar_SelectedIndexChanged(null, null);
                    //lnkagregartodos_Click(null, null);
                }
            }
            ModalShow("#ModalCapturaProyectos");
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            try
            {
                string vmensaje = string.Empty;
                int id_proyecto = Convert.ToInt32(hdfid_proyecto.Value == "" ? "0" : hdfid_proyecto.Value);
                proyectos proyecto = new proyectos();

                proyecto.proyecto = txtnombreproyecto.Text;
                proyecto.descripcion = txtdescripcion.Text;
                proyecto.id_proyecto_periodo = Convert.ToInt32(ddlperiodo.SelectedValue);
                proyecto.id_proyecto_estatus = Convert.ToInt32(ddlestatus.SelectedValue);
                proyecto.folio_op = txtcveop.Text;
                proyecto.folio_pmt = txtfolopmt.Text;

                if (id_proyecto > 0) { proyecto.id_proyecto = id_proyecto; }
                EmpleadosCOM empleados = new EmpleadosCOM();
                int num_empleado = Convert.ToInt32(ddlempleado_a_consultar.SelectedValue);
                DataSet ds = empleados.sp_listado_empleados(num_empleado, false, false);
                DataTable dt = ds.Tables[0];
                string Usuario = dt.Rows[0]["usuario"].ToString();
                proyecto.usuario_resp = Usuario;
                proyecto.usuario = Session["usuario"] as string;

                DateTime fechaInicial = Convert.ToDateTime(rdpfechainicial.SelectedDate);
                DateTime fechaFinal = Convert.ToDateTime(rdpfechafinal.SelectedDate);
                proyecto.fecha_inicio = fechaInicial;
                proyecto.fecha_fin = fechaFinal;
                int dias = ((fechaFinal - fechaInicial)).Days;
                proyecto.duración = Convert.ToInt16(dias);
               
                proyecto.folio_pmt = txtfolopmt.Text.Trim();
                //CPED
                proyecto.cped = txtcped.Text.Trim();


                proyecto.costo_usd = txtmonto.Text != "" ? Convert.ToDecimal(txtmonto.Text.Replace("$", "").Replace(",", "").Replace(" ", "")) : 0; ;
                proyecto.costo_mn = txtmontomn.Text != ""? Convert.ToDecimal(txtmontomn.Text.Replace("$", "").Replace(",", "").Replace(" ", "")):0;
                proyecto.tipo_moneda = txtmoneda.Text;


                List<proyectos_historial_tecnologias> tecnologias = new List<proyectos_historial_tecnologias>();
                string strtecnologias = "";
                IList<RadComboBoxItem> list_tecnologias =  ddltegnologia.CheckedItems;
                foreach (RadComboBoxItem item in list_tecnologias)
                {
                    if (item.Checked) {
                        proyectos_historial_tecnologias tecnologia = new proyectos_historial_tecnologias
                        {
                            id_proyecto_tecnologia = Convert.ToInt32(item.Value)
                        };
                        strtecnologias = strtecnologias + item.Text + ",";
                        tecnologias.Add(tecnologia);
                    }
                }
                strtecnologias = strtecnologias.Substring(0,strtecnologias.Length > 0 ? strtecnologias.Length - 1:0);

                if (proyecto.proyecto == "")
                {
                    ModalShow("#ModalCapturaProyectos");
                    Toast.Error("Error al procesar proyecto : Ingrese el nombre del proyecto", this);
                }
                else if (proyecto.descripcion == "")
                {
                    ModalShow("#ModalCapturaProyectos");
                    Toast.Error("Error al procesar estatus : Ingrese la descripción del proyecto", this);
                }
                else if (tecnologias.Count <= 0)
                {
                    ModalShow("#ModalCapturaProyectos");
                    Toast.Error("Error al procesar tecnologia : Seleccione una tecnología ", this);
                }
                else if (proyecto.id_proyecto_periodo <=  0)
                {
                    ModalShow("#ModalCapturaProyectos");
                    Toast.Error("Error al procesar periodo : Seleccione un periodo", this);
                }
                else if (proyecto.id_proyecto_estatus <=0)
                {
                    ModalShow("#ModalCapturaProyectos");
                    Toast.Error("Error al procesar estatus : Seleccione un estatus", this);
                }
                else if (proyecto.folio_op == "")
                {
                    ModalShow("#ModalCapturaProyectos");
                    Toast.Error("Error al procesar folio pmtracker : Ingrese un numero de oportunidad", this);
                }
                else if (proyecto.folio_pmt == "")
                {
                    ModalShow("#ModalCapturaProyectos");
                    Toast.Error("Error al procesar folio pmtracker : Ingrese un folio pmtracker", this);
                }
                else if (proyecto.cped =="")
                {
                    ModalShow("#ModalCapturaProyectos");
                    Toast.Error("Error al procesar CPED : Favor de ingresar un CPED", this);
                }
                else if (rdpfechainicial.SelectedDate> rdpfechafinal.SelectedDate)
                {
                    ModalShow("#ModalCapturaProyectos");
                    Toast.Error("Error al procesar periodo : la fecha inicial no puede ser mayor a la fecha final ", this);
                }
                else
                {
                    proyecto.usuario_edicion = Session["usuario"] as string;
                    vmensaje = id_proyecto > 0 ? Editar(proyecto, tecnologias) : Agregar(proyecto,tecnologias);
                    if (vmensaje == "")
                    {
                        if (id_proyecto == 0)
                        {

                            string usuario_resp = proyecto.usuario_resp;
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
                                string subject = "Módulo de proyectos - Proyecto creado";
                                string mail = "<div>" + saludo + " <strong>" +
                                    System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(usuario["empleado"].ToString().ToLower())
                                    + "</strong> <div>" +
                                    "<br>" +
                                    "<p>Le fue asignado el proyecto <strong>" + proyecto.proyecto + "</strong>"+
                                    "</p>" +
                                       "<p><strong>Descripción</strong> <br/> " +
                                      (proyecto.descripcion == "" || proyecto.descripcion == null ?proyecto.proyecto:proyecto.descripcion)+"</p> " +
                                       "<p><strong>CPED</strong> <br/> " +
                                       proyecto.cped + "</p> " +
                                       "<p><strong>Tecnología(s)</strong><br/> " +
                                     strtecnologias + "</p> " +
                                       "<p><strong>Costo</strong><br/> " +
                                       txtmonto.Text +" USD / "+ txtmontomn.Text + " MN</p> " +
                                       "<p><strong>Duración</strong><br/> " +
                                      proyecto.duración+ " dia(s). Del "+ 
                                      Convert.ToDateTime(proyecto.fecha_inicio).ToString("dddd dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX")) + " al "+
                                      Convert.ToDateTime(proyecto.fecha_fin).ToString("dddd dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX")) + "</p> " +
                                    "<br/><p>Este movimiento fue realizado por <strong>" +
                                    System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Session["nombre"].ToString().ToLower()) 
                                    + "</strong> el dia <strong>" +
                                    DateTime.Now.ToString("dddd dd MMMM, yyyy hh:mm:ss tt", CultureInfo.CreateSpecificCulture("es-MX")) + "</strong>" +
                                    "</p>";
                                CorreosCOM correos = new CorreosCOM();
                                bool correct = correos.SendMail(mail, subject, mail_to);
                            }
                        }

                        txtnombreproyecto.Text = "";
                        txtdescripcion.Text = "";
                        ddlperiodo.SelectedIndex = 0;
                        ddlestatus.SelectedIndex = 0;
                        txtcveop.Text = "";
                        txtfolopmt.Text = "";
                        txtcped.Text = "";
                        txtmonto.Text = "";
                        txtmontomn.Text = "";
                        txtmoneda.Text = "";
                        Cargarddlperiodo();
                        Cargarddlestatus();
                        Cargarddltegnologia();
                        hdfid_proyecto.Value = "";
                        CargarProyectos(1);
                        ModalClose("#ModalCapturaProyectos");
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                                    "AlertGO('Proyecto agregado correctamente.','mis_proyectos.aspx');", true);

                    }
                    else
                    {
                        ModalClose("#ModalCapturaProyectos");
                        Toast.Error("Error al procesar proyecto : " + vmensaje, this);
                    }
                }
            }
            catch (Exception ex)
            {
                ModalShow("#ModalProyectoestatus");
                Toast.Error("Error al procesar estatus : " + ex.Message, this);
            }
        }

        private long DateDiff(object day, DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }

        protected void btneventgrid_Click(object sender, EventArgs e)
        {
            try
            {
                int id_proyecto = Convert.ToInt32(hdfid_proyecto.Value == "" ? "0" : hdfid_proyecto.Value);
                if (id_proyecto > 0)
                {
                    proyectos proyecto = GetProyecto(id_proyecto);
                    if (proyecto != null)
                    {
                        txtcveop.BorderColor = System.Drawing.Color.Silver;
                        txtfolopmt.BorderColor = System.Drawing.Color.Silver;
                        txtcped.BorderColor = System.Drawing.Color.Silver;
                        Cargarddlperiodo();
                        Cargarddlestatus();
                        Cargarddltegnologia();

                        txtnombreproyecto.Text = proyecto.proyecto;
                        
                        txtdescripcion.Text = proyecto.descripcion;
                        ddlperiodo.SelectedValue = proyecto.id_proyecto_periodo.ToString();
                        ddlestatus.SelectedValue = proyecto.id_proyecto_estatus.ToString();
                        txtcveop.Text = proyecto.folio_op.ToString();
                        txtfolopmt.Text = proyecto.folio_pmt;

                        ICollection<proyectos_historial_tecnologias> tecnologias = proyecto.proyectos_historial_tecnologias;
                        foreach (RadComboBoxItem item in ddltegnologia.Items)
                        {
                            foreach (proyectos_historial_tecnologias tecnologia in tecnologias)
                            {
                                if (item.Value == tecnologia.id_proyecto_tecnologia.ToString())
                                {
                                    item.Checked = true;
                                }
                            }
                        }

                        //ddltegnologia.SelectedValue = proyecto.id_proyecto_tecnologia.ToString();                       
                        CargarDatosempleados(proyecto.usuario_resp);
                        txtcveop.Text = proyecto.cveoport.ToString();
                        txtfolopmt.Text = proyecto.folio_pmt;
                        Boolean permiso = Permisos(Session["usuario"] as string, 2);
                        if (permiso == false)
                        {
                            txtcped.Enabled = false;
                        }
                        else
                        {

                            txtcped.Enabled = true;
                        }
                        txtcped.Text = proyecto.cped;
                        txtmonto.Text = proyecto.costo_usd.ToString("C2");
                        txtmontomn.Text = proyecto.costo_mn.ToString("C2");
                        txtmoneda.Text = proyecto.tipo_moneda;
                        rdpfechainicial.SelectedDate = proyecto.fecha_inicio;
                        rdpfechafinal.SelectedDate = proyecto.fecha_fin;
                        ModalShow("#ModalCapturaProyectos");
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar proyecto : " + ex.Message, this);
            }
            finally {
                table_proyectos.Style["display"] = "none";
            }
        }

        protected void btneliminar_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    int id_proyecto = Convert.ToInt32(hdfid_proyecto.Value == "" ? "0" : hdfid_proyecto.Value);
            //    proyectos proyecto = new proyectos();
            //    proyecto.id_proyecto = id_proyecto;
            //    proyecto.usuario_borrado = Session["usuario"] as string;
            //    string vmensaje = Eliminar(proyecto);
            //    if (vmensaje == "")
            //    {
            //        CargarProyectos();
            //        Toast.Success("Estatus eliminado correctamente.", "Mensaje del sistema", this);
            //    }
            //    else
            //    {
            //        Toast.Error("Error al eliminar proyecto: " + vmensaje, this);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Toast.Error("Error al eliminar proyecto: " + ex.Message, this);
            //}
            ModalShow("#modal_terminacion");
        }

        protected void btnopendashboard_Click(object sender, EventArgs e)
        {
            string id_proyecto = funciones.deTextoa64(hdfid_proyecto.Value);
            Response.Redirect("proyectos_dashboard.aspx?id_proyecto=" + id_proyecto);

        }

        protected void lnksearch_Click(object sender, EventArgs e)
        {
            string filter = txtfilterempleado.Text;
            try
            {
                if (filter.Length == 0 || filter.Length > 3)
                {
                    CargarDatosempleados(filter);
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
                if (r == 0)
                {
                    Toast.Error("Error al terminar proyecto: Seleccione un archivo.", this);
                }
                else
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/"));//path localDateTime localDate = DateTime.Now;
                    string path_local = "files/documents/proyectos/";
                    DateTime localDate = DateTime.Now;
                    string date = localDate.ToString();
                    date = date.Replace("/", "_");
                    date = date.Replace(":", "_");
                    date = date.Replace(" ", "");
                    string name = path_local + Path.GetFileNameWithoutExtension(e.File.FileName) + "_" + date + Path.GetExtension(e.File.FileName);
                    //funciones.UploadFile(fuparchivo, dirInfo.ToString() + name.Trim(), this.Page);
                    e.File.SaveAs(dirInfo.ToString() + name.Trim());
                    int id_proyecto = Convert.ToInt32(hdfid_proyecto.Value);
                    documentos documento = new documentos();
                    documento.id_proyecto = id_proyecto;
                    documento.path = funciones.deTextoa64(name);
                    documento.nombre = Path.GetFileName(funciones.de64aTexto(funciones.deTextoa64(name)));
                    documento.tamaño = e.File.ContentLength.ToString();
                    documento.publico = true;
                    documento.extension = Path.GetExtension(funciones.de64aTexto(funciones.deTextoa64(name)));
                    documento.contentType = funciones.ContentType(documento.extension);
                    documento.fecha = DateTime.Now;
                    documento.usuario = Session["usuario"] as string;
                    documento.comentarios = txtcomentarioscierre.Text;
                    ProyectosCOM proyectos = new ProyectosCOM();
                    string vmensaje = proyectos.Cerrar(id_proyecto, Session["usuario"] as string, documento);
                    if (vmensaje == "")
                    {
                        proyectos proyecto = proyectos.proyecto(id_proyecto);

                        //string usuario_resp = proyecto.usuario_resp;
                        //EmpleadosCOM usuarios = new EmpleadosCOM();
                        //DataTable dt_usuario = usuarios.GetUsers();
                        //DataView dv = dt_usuario.DefaultView;
                        //dv.RowFilter = "usuario_red = '" + usuario_resp.Trim().ToUpper() + "'";
                        //DataTable dt_result = dv.ToTable();
                        ProyectosEmpleadosCOM empleados = new ProyectosEmpleadosCOM();
                        DataTable users = empleados.empleados_proyecto(id_proyecto);

                        if (users.Rows.Count > 0)
                        {
                            foreach (DataRow row in users.Rows)
                            {
                                string saludo = DateTime.Now.Hour > 13 ? "Buenas tardes" : "Buenos dias";
                                DataRow usuario =row;
                                string mail_to = usuario["correo"].ToString() == "" ? "" : (usuario["correo"].ToString() + ";");
                                string subject = "Módulo de proyectos - Proyecto cerrado";
                                string mail = "<div>" + saludo + " <strong>" +
                                    System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(usuario["nombre"].ToString().ToLower())
                                    + "</strong> <div>" +
                                    "<br>" +
                                    "<p>Se le comunica que el proyecto <strong>" + proyecto.proyecto + "</strong>, fue cerrado el dia <strong>"
                                    + DateTime.Now.ToString("dddd dd MMMM, yyyy hh:mm:ss tt", CultureInfo.CreateSpecificCulture("es-MX")) + "</strong>" +
                                    "</p>" +
                                     "<p><strong>Documento de cierre</strong><br>" +
                                    (txtcomentarioscierre.Text == "" ? "" : txtcomentarioscierre.Text) +
                                    "<br><a href='https://apps.migesa.com.mx/portal_connext/" + name +
                                                "' download>Descargar documento</a></p>" +
                                    "<br><p>Este movimiento fue realizado por <strong>" + System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Session["nombre"].ToString().ToLower()) + "</strong>" +
                                    " el dia <strong> " +
                                    DateTime.Now.ToString("dddd dd MMMM, yyyy hh:mm:ss tt", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX")) + "</strong>" +
                                    "</p>";
                                CorreosCOM correos = new CorreosCOM();
                                bool correct = correos.SendMail(mail, subject, mail_to);
                            }
                        }
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                                    "AlertGO('Proyecto terminado correctamente.','mis_proyectos.aspx');", true);
                    }
                    else
                    {
                        Toast.Error("Error al terminar proyecto: " + vmensaje, this);
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al terminar proyecto: " + ex.Message, this);
            }
        }

        protected void lnkguardarhistorial_Click(object sender, EventArgs e)
        {
            int r = AsyncUpload1.UploadedFiles.Count;
            if (r == 0)
            {
                Toast.Error("Error al terminar proyecto: Seleccione un archivo.", this);
            }
        }

        protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id_proyecto_estatus = Convert.ToInt32(ddlstatus.SelectedValue);
            CargarProyectos(id_proyecto_estatus);
        }

        protected void ddltechnology_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id_proyecto_estatus = Convert.ToInt32(ddlstatus.SelectedValue);
            DataTable dt_filtros = GetProyectos(id_proyecto_estatus);
            DataView dv = dt_filtros.DefaultView;
            DataTable dt_result = new DataTable();
            if (ddltechnology.SelectedValue!="0")
            {
                dv.RowFilter = "id_proyecto_tecnologia = " + ddltechnology.SelectedValue.ToString() + "";
                dt_result = dv.ToTable();
            }
            else
            {
                dt_result = dt_filtros;
            }          
            //foreach (DataRow dr in dt_filtros.Rows)
            //{
            //    if (ddltechnology.SelectedItem.Text != dr["Tecnologia"].ToString())
            //    {
            //        //dt_filtros.Rows.Remove(dr);
            //        dr.Delete();
            //    }
            //}           
            //dt_filtros.AcceptChanges();         
            repeat_proyectos.DataSource = dt_result;
            repeat_proyectos.DataBind();
        }

        protected void txtcveop_TextChanged(object sender, EventArgs e)
        {
            if (txtcveop.Text!="")
            {
                string vmansaje = FolioOportunidad(txtcveop.Text);
                if (vmansaje != "")
                {
                    txtcveop.Text = "";
                    txtcveop.BorderStyle = BorderStyle.Solid;
                    txtcveop.BorderColor = System.Drawing.Color.Red;
                    txtcveop.Focus();                    
                    Toast.Error(vmansaje, this);
                }
                else
                {
                    txtcveop.BorderStyle = BorderStyle.Solid;
                    txtcveop.BorderColor = System.Drawing.Color.Green;
                }
                
            }
        }

        protected void txtfolopmt_TextChanged(object sender, EventArgs e)
        {
            if (txtfolopmt.Text != "")
            {
                string vmansaje = foliopmtracker(txtfolopmt.Text.Trim());
                if (vmansaje != "")
                {
                    txtfolopmt.Text = "";
                    txtfolopmt.BorderStyle = BorderStyle.Solid;
                    txtfolopmt.BorderColor = System.Drawing.Color.Red;
                    txtfolopmt.Focus();
                    Toast.Error(vmansaje, this);
                }
                else
                {
                    txtfolopmt.BorderStyle = BorderStyle.Solid;
                    txtfolopmt.BorderColor = System.Drawing.Color.Green;
                }
            }
        }

        protected void txtcped_TextChanged(object sender, EventArgs e)
        {
            if (txtcped.Text != "")
            {
                CPED cped = Getcped(txtcped.Text);
                if (cped== null)
                {
                    Toast.Error("No se encuentra ningun CPED con el folio: " + txtcped.Text, this);
                    txtcped.Text = "";
                    txtmonto.Text = "";
                    txtcped.BorderStyle = BorderStyle.Solid;
                    txtcped.BorderColor = System.Drawing.Color.Red;
                    txtcped.Focus();                    
                }
                else
                {
                    txtmonto.Text = cped.costo_usd.ToString("C2");
                    txtmontomn.Text = cped.costo_mn.ToString("C2");
                    txtmoneda.Text = cped.tipo_moneda;
                    txtcped.BorderStyle = BorderStyle.Solid;
                    txtcped.BorderColor = System.Drawing.Color.Green;
                }
            }
        }

        protected void lnkgenerarexcel_Click(object sender, EventArgs e)
        {            
            try
            {
                //DataTable dt = new DataTable();
                //dt = ViewState[hdfsessionid.Value + "-dt_reporte"] as DataTable;
                int id_proyecto_estatus = Convert.ToInt32(ddlstatus.SelectedValue);
                DataTable dt_filtros = GetProyectos(id_proyecto_estatus);
                DataView dv = dt_filtros.DefaultView;
                DataTable dt_result = new DataTable();
                if (ddltechnology.SelectedValue != "0")
                {
                    dv.RowFilter = "id_proyecto_tecnologia = " + ddltechnology.SelectedValue.ToString() + "";
                    dt_result = dv.ToTable();
                }
                else
                {
                    dt_result = dt_filtros;
                }

                if (dt_result.Rows.Count > 0)
                {
                    Export Export = new Export();
                    //array de DataTables
                    List<DataTable> ListaTables = new List<DataTable>();
                    ListaTables.Add(dt_result);
                    //array de nombre de sheets
                    DateTime localDate = DateTime.Now;
                    string date = localDate.ToString();
                    date = date.Replace("/", "_");
                    date = date.Replace(":", "_");
                    date = date.Replace(".", "_");
                    date = date.Replace(" ", "_");
                    string[] Nombres = new string[] { "Filtrado de mis proyectoss" };
                    string mensaje = Export.toExcel("Filtrado de mis proyectos", XLColor.White, XLColor.Black, 18, true, DateTime.Now.ToString(), XLColor.White,
                                           XLColor.Black, 10, ListaTables, XLColor.CelestialBlue, XLColor.White, Nombres, 1,
                                           "mis proyectos_" + date + ".xlsx", Page.Response);
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
                //DataTable dt = new DataTable();
                //dt = ViewState[hdfsessionid.Value + "-dt_reporte"] as DataTable;
                int id_proyecto_estatus = Convert.ToInt32(ddlstatus.SelectedValue);
                DataTable dt_filtros = GetProyectos(id_proyecto_estatus);
                DataView dv = dt_filtros.DefaultView;
                DataTable dt_result = new DataTable();
                if (ddltechnology.SelectedValue != "0")
                {
                    dv.RowFilter = "id_proyecto_tecnologia = " + ddltechnology.SelectedValue.ToString() + "";
                    dt_result = dv.ToTable();
                }
                else
                {
                    dt_result = dt_filtros;
                }

                if (dt_result.Rows.Count > 0)
                {
                    Export Export = new Export();
                    //array de DataTables
                    List<DataTable> ListaTables = new List<DataTable>();
                    ListaTables.Add(dt_result);
                    //array de nombre de sheets
                    DateTime localDate = DateTime.Now;
                    string date = localDate.ToString();
                    date = date.Replace("/", "_");
                    date = date.Replace(":", "_");
                    date = date.Replace(".", "_");
                    date = date.Replace(" ", "_");
                    string[] Nombres = new string[] { "Filtrado de mis proyectos" };
                    string mensaje = Export.ToPdf("Filtrado_de_mis_proyectos_" + date, ListaTables, 1, Nombres, Page.Response);
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
    }
}