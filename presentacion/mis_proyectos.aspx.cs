using datos;
using datos.Model;
using negocio.Componentes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
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
                CargarProyectos();
            }
        }

        private DataTable GetProyectos()
        {
            DataTable dt = new DataTable();
            try
            {
                int num_empleado = Convert.ToInt32(Session["num_empleado"]);
                Boolean ver_Todos_los_empleados = Convert.ToBoolean(Session["ver_Todos_los_empleados"]);
                ProyectosCOM Proyectos = new ProyectosCOM();
                dt = Proyectos.SelectAll(num_empleado, ver_Todos_los_empleados, 1);
            }
            catch (Exception)
            {
                dt = new DataTable();
            }
            return dt;
        }

        private proyectos GetProyectoEstatus(int id_proyecto)
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

        private void CargarProyectos()
        {
            try
            {
                DataTable dt = GetProyectos();
                repeat_proyectos.DataSource = dt;
                repeat_proyectos.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar catalogo: " + ex.Message, this);
            }
        }

        private string Agregar(proyectos id_proyecto)
        {
            ProyectosCOM Proyecto = new ProyectosCOM();
            string vmensaje = Proyecto.Agregar(id_proyecto);

            return vmensaje;
        }
        private string Editar(proyectos id_proyecto)
        {
            ProyectosCOM Proyecto = new ProyectosCOM();
            string vmensaje = Proyecto.Editar(id_proyecto);

            return vmensaje;
        }

        private string Eliminar(proyectos id_proyecto)
        {
            ProyectosCOM Proyecto = new ProyectosCOM();
            string vmensaje = Proyecto.Elimina(id_proyecto);

            return vmensaje;
        }

        protected void lnknuevoproyecto_Click(object sender, EventArgs e)
        {
            txtnombreproyecto.Text = "";
            txtdescripcion.Text = "";
            ddlperiodo.SelectedIndex = 0;
            ddlestatus.SelectedIndex = 0;
            txtcveop.Text = "";
            txtfolopmt.Text = "";
            ModalShow("#ModalCapturaProyectos");
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {

        }

        protected void btneventgrid_Click(object sender, EventArgs e)
        {

        }

        protected void btneliminar_Click(object sender, EventArgs e)
        {

        }
    }
}