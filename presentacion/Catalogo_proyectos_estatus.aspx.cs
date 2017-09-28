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
    public partial class catalogo_proyectos_estatus : System.Web.UI.Page
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
                CargarCatalogo();
            }
        }

        private DataTable GetProyectosEstatus()
        {
            DataTable dt = new DataTable();
            try
            {
                
                AyudaCOM ayudas = new AyudaCOM();
                dt = ayudas.SelectAll();
            }
            catch (Exception)
            {
                dt = new DataTable();
            }
            return dt;
        }

        private proyectos_estatus GetProyectoEstatus(int id_ayuda)
        {
            proyectos_estatus dt = new proyectos_estatus();
            try
            {
                proyec ayudas = new AyudaCOM();
                dt = ayudas.Ayuda(id_ayuda);
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }

        private void CargarCatalogo()
        {
            try
            {
                DataTable dt = GetProyectosEstatus();
                repeat_proyectoestatus.DataSource = dt;
                repeat_proyectoestatus.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar catalogo: " + ex.Message, this);
            }
        }

        private string Agregar(ayudas ayuda)
        {
            AyudaCOM ayudas = new AyudaCOM();
            string vmensaje = ayudas.Agregar(ayuda);
            if (vmensaje == "")
            {
                return "";
            }
            else
            {
                return vmensaje;
            }
        }
        private string Editar(ayudas ayuda)
        {
            AyudaCOM ayudas = new AyudaCOM();
            string vmensaje = ayudas.Editar(ayuda);
            if (vmensaje == "")
            {
                return "";
            }
            else
            {
                return vmensaje;
            }
        }

        private string Eliminar(ayudas ayuda)
        {
            AyudaCOM ayudas = new AyudaCOM();
            string vmensaje = ayudas.Eliminar(ayuda);
            if (vmensaje == "")
            {
                return "";
            }
            else
            {
                return vmensaje;
            }
        }

        protected void lnknuevoproyectoestatus_Click(object sender, EventArgs e)
        {

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