﻿using ClosedXML.Excel;
using datos;
using negocio.Componentes;
using negocio.Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace presentacion.Pages.Proyectos
{
    public partial class proyectos_dashboard : System.Web.UI.Page
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
                    CargarRiesgos(id_proyecto);
                    CargarRecursos(id_proyecto);
                    CargarMinutas(id_proyecto);
                    DataRow proyecto = dt.Rows[0];
                    hdfid_proyecto.Value = id_proyecto.ToString();
                    lblproyect.Text = proyecto["proyecto"].ToString();
                    //lblresumen.Text = proyecto["descripcion"].ToString();
                    lblmonto.Text = Convert.ToDecimal(proyecto["costo_usd"]).ToString("C2")+ " USD / " + Convert.ToDecimal(proyecto["costo_mn"]).ToString("C2") + " MN";
                    lblperiodo.Text = proyecto["periodo"].ToString();
                    lblestatus.Text = proyecto["estatus"].ToString();
                    lbltecnologia.Text = proyecto["tecnologia"].ToString();
                    lblcped.Text = proyecto["cped"].ToString();
                    lblempleado.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(proyecto["empleado"].ToString().ToLower());
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar información del proyecto. "+ex.Message,this);
            }
        }

        private void CargarRiesgos(int id_proyecto)
        {
            try
            {
                RiesgosCOM riesgos = new RiesgosCOM();
                DataTable dt = riesgos.proyectos_riesgos(id_proyecto);
                DataView dv = dt.DefaultView;
                dv.RowFilter = "id_riesgos_estatus = 1";
                DataTable dt_riesgos_abierto = dv.ToTable();
                lblnumriesgos.Text = dt_riesgos_abierto.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar información del proyecto(riesgos): " + ex.Message, this);
            }
        }


        private void CargarRecursos(int id_proyecto)
        {
            try
            {
                ProyectosEmpleadosCOM empleados = new ProyectosEmpleadosCOM();
                DataTable dt = empleados.empleados_proyecto(id_proyecto);                
                lblrecursos.Text = dt.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar información del proyecto(recursos): " + ex.Message, this);
            }
        }


        private void CargarMinutas(int id_proyecto)
        {
            try
            {
                ProyectosMinutasCOM proyectos = new ProyectosMinutasCOM();
                DataTable dt = proyectos.GetAll(id_proyecto);
                lblminutas.Text = dt.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar información del proyecto. " + ex.Message, this);
            }
        }
        protected void lnkgo_riesgos_Click(object sender, EventArgs e)
        {
            Response.Redirect("proyectos_evaluacion_riesgos.aspx?id_proyecto="+Request.QueryString["id_proyecto"]);
        }

        protected void lnkterminarproyecto_Click(object sender, EventArgs e)
        {
            ModalShow("#modal_terminacion");
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
                    documento.id_documento_tipo = 2;
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
                                DataRow usuario = row;
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

        protected void lnkrecursos_Click(object sender, EventArgs e)
        {
            Response.Redirect("proyectos_recursos.aspx?id_proyecto=" + Request.QueryString["id_proyecto"]);

        }

        protected void lnkminutas_Click(object sender, EventArgs e)
        {
            Response.Redirect("proyectos_minutas.aspx?id_proyecto=" + Request.QueryString["id_proyecto"]);
        }
    }
}