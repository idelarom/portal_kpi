using negocio.Componentes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace presentacion
{
    public partial class proyectos_recursos : System.Web.UI.Page
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
                    CargarRecursos(id_proyecto);
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

        private void CargarRecursos(int id_proyecto)
        {
            try
            {
                ProyectosEmpleadosCOM empleados = new ProyectosEmpleadosCOM();
                DataTable dt = empleados.empleados_proyecto(id_proyecto);
                repeat_proyectos_empleados.DataSource = dt;
                repeat_proyectos_empleados.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar información del proyecto(recursos): " + ex.Message, this);
            }
        }

        private void CargarListadoEmpleados(string filtro)
        {
            try
            {
                int NumJefe = Convert.ToInt32(Session["NumJefe"]);
                int num_empleado = Convert.ToInt32(Session["num_empleado"]);
                Boolean ver_Todos_los_empleados = Convert.ToBoolean(Session["ver_Todos_los_empleados"]);
                EmpleadosCOM empleados = new EmpleadosCOM();
                bool no_activos = false;
                DataSet ds = empleados.sp_listado_empleados(num_empleado, ver_Todos_los_empleados, no_activos);
                DataTable dt_original = new DataTable();
                dt_original = ds.Tables[0];
                DataTable dt = new DataTable();
                if (filtro == "")
                {
                    dt = dt_original;
                }
                else
                {
                    if (dt_original.Select("nombre like '%" + filtro + "%'").Length > 0)
                    {
                        dt = filtro == "" ? dt_original : dt_original.Select("nombre like '%" + filtro + "%'").CopyToDataTable();
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    ViewState["dt_empleados"] = dt;
                    rdlempleadosproyecto.DataTextField = "nombre";
                    rdlempleadosproyecto.DataValueField = "usuario";
                    rdlempleadosproyecto.DataSource = dt;
                    rdlempleadosproyecto.DataBind();
                }
                else
                {
                    Toast.Info("No se encontro ninguna coincidencia. Intentelo nuevamente.", "Mensaje del Sistema", this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar lista de empleados. " + ex.Message, this);
            }
        }

        protected void lnkdashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("proyectos_dashboard.aspx?id_proyecto=" + Request.QueryString["id_proyecto"]);
        }

        protected void lnkagregarempleadoaproyecto_Click(object sender, EventArgs e)
        {
            rdlempleadosproyecto.Items.Clear();
            txtbuscarempleadoproyecto.Text = "";

            ModalShow("#myModalEmpleados");
        }

        protected void btnbuscarempleado_Click(object sender, EventArgs e)
        {
            if (txtbuscarempleadoproyecto.Text.Trim().Length > 2 || txtbuscarempleadoproyecto.Text.Trim().Length == 0)
            {
                CargarListadoEmpleados(txtbuscarempleadoproyecto.Text.Trim());
                imgloadempleados.Style["display"] = "none";
                lblbe.Style["display"] = "none";
            }
            else
            {
                Toast.Info("Ingrese un minimo de 3 caracteres para realizar la busqueda.", "Mensaje del Sistema", this);
            }
        }

        protected void lnkguardarempleado_Click(object sender, EventArgs e)
        {
            IList<RadListBoxItem> collection = rdlempleadosproyecto.SelectedItems;
            string vmensaje = "";
            try
            {
                if (collection.Count == 0)
                {
                    vmensaje = "Seleccione minimo 1 empleado.";
                }
                string correos_pm = "";
                if (vmensaje == "")
                {
                    ProyectosEmpleadosCOM proyectos = new ProyectosEmpleadosCOM();
                    foreach (RadListBoxItem item in collection)
                    {
                        string usuario = item.Value;
                        int id_proyecto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"]));
                        string usuario_registro = Session["usuario"] as string;
                        vmensaje = proyectos.Agregar(id_proyecto, usuario, true, usuario_registro);
                        if (vmensaje != "") { break; }
                    }
                }

                if (vmensaje == "")
                {
                    ProyectosCOM proyectos = new ProyectosCOM();
                    datos.proyectos proyecto = proyectos.proyecto(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id_proyecto"])));
                    if (proyecto != null)
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
                            string subject = "Módulo de proyectos - Proyecto relacionado";
                            string mail = "<div>" + saludo + " <strong>" +
                                System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(usuario["empleado"].ToString().ToLower())
                                + "</strong> <div>" +
                                "<br>" +
                                "<p>Le fue asignado el proyecto <strong>" + proyecto.proyecto + "</strong>" +
                                "</p>" +
                                   "<p><strong>Descripción</strong> <br/> " +
                                  (proyecto.descripcion == "" || proyecto.descripcion == null ? proyecto.proyecto : proyecto.descripcion) + "</dd> " +
                                   "<p><strong>CPED</strong> <br/> " +
                                   proyecto.cped + "</p> " +
                                   "<p><strong>Costo</strong><br/> " +
                                   proyecto.costo_usd.ToString("C2") + " USD / " + proyecto.costo_mn.ToString("C2") + " MN</p> " +
                                   "<p><strong>Duración</strong><br/> " +
                                  proyecto.duración + " dia(s)</p> " +
                                "<br/><p>Este movimiento fue realizado por <strong>" +
                                System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Session["nombre"].ToString().ToLower())
                                + "</strong> el dia <strong>" +
                                DateTime.Now.ToString("dddd dd MMMM, yyyy hh:mm:ss tt", CultureInfo.CreateSpecificCulture("es-MX")) + "</strong>" +
                                "</p>";
                            CorreosCOM correos = new CorreosCOM();
                            bool correct = correos.SendMail(mail, subject, mail_to);
                        }
                    }
                    string url = "proyectos_recursos.aspx?id_proyecto=" + Request.QueryString["id_proyecto"];
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                        "AlertGO('Configuración Guardada Correctamente', '" + url + "');", true);
                }
                else
                {
                    Toast.Error("Error al guardar lista de empleados. " + vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al guardar lista de empleados. " + ex.Message, this);
            }
        }

        protected void lnkeliminarempleadoproyecto_Click(object sender, EventArgs e)
        {
            string vmensaje = "";
            try
            {
               
                LinkButton lnk = sender as LinkButton;
                int id_pempleado = Convert.ToInt32(lnk.CommandArgument);
                ProyectosEmpleadosCOM proyectos = new ProyectosEmpleadosCOM();
                vmensaje = proyectos.Eliminar(id_pempleado,Session["usuario"] as string);
                if (vmensaje == "")
                {
                    string url = "proyectos_recursos.aspx?id_proyecto=" + Request.QueryString["id_proyecto"];
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                        "AlertGO('Empleado eliminado correctamente del proyecto.', '" + url + "');", true);
                }
                else
                {
                    Toast.Error(vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al eliminar empleado. " + ex.Message, this);
            }
        }
    }
}