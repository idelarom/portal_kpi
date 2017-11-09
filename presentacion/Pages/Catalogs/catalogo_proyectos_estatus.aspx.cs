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

namespace presentacion.Pages.Catalogs
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

                ProyectosEstatusCOM PE = new ProyectosEstatusCOM();
                dt = PE.SelectAll();
            }
            catch (Exception)
            {
                dt = new DataTable();
            }
            return dt;
        }

        private proyectos_estatus GetProyectoEstatus(int id_proyecto_estatus)
        {
            proyectos_estatus dt = new proyectos_estatus();
            try
            {
                ProyectosEstatusCOM PE = new ProyectosEstatusCOM();
                dt = PE.estatus(id_proyecto_estatus);
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

        private string Agregar(proyectos_estatus id_proyecto_estatus)
        {
            ProyectosEstatusCOM PE = new ProyectosEstatusCOM();
            string vmensaje = PE.Agregar(id_proyecto_estatus);
            if (vmensaje == "")
            {
                return "";
            }
            else
            {
                return vmensaje;
            }
        }
        private string Editar(proyectos_estatus id_proyecto_estatus)
        {
            ProyectosEstatusCOM PE = new ProyectosEstatusCOM();
            string vmensaje = PE.Editar(id_proyecto_estatus);
            if (vmensaje == "")
            {
                return "";
            }
            else
            {
                return vmensaje;
            }
        }

        private string Eliminar(int id_proyecto_estatus)
        {
            ProyectosEstatusCOM PE = new ProyectosEstatusCOM();
            string vmensaje = PE.Eliminar(id_proyecto_estatus);
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
            txtestatus.Text = "";
            chkactivo.Checked = true;
            hdfid_proyecto_estatus.Value = "";
            ModalShow("#ModalProyectoestatus");
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            try
            {
                string vmensaje = string.Empty;
                int id_proyecto_estatus = Convert.ToInt32(hdfid_proyecto_estatus.Value == "" ? "0" : hdfid_proyecto_estatus.Value);
                proyectos_estatus PE = new proyectos_estatus();
                PE.estatus = txtestatus.Text;
                
                if (id_proyecto_estatus > 0) { PE.id_proyecto_estatus = id_proyecto_estatus; }
                PE.activo = chkactivo.Checked;
                PE.usuario = Session["usuario"] as string;
                if (PE.estatus == "")
                {
                    ModalShow("#ModalProyectoestatus");
                    Toast.Error("Error al procesar estatus : Ingrese un titulo", this);
                }               
                else
                {
                    vmensaje = id_proyecto_estatus > 0 ? Editar(PE) : Agregar(PE);
                    if (vmensaje == "")
                    {
                        txtestatus.Text = "";
                        hdfid_proyecto_estatus.Value = "";
                        chkactivo.Checked = false;
                        CargarCatalogo();
                        Toast.Success("Estatus agregado correctamente.", "Mensaje del sistema", this);
                    }
                    else
                    {
                        ModalShow("#ModalProyectoestatus");
                        Toast.Error("Error al procesar estatus : " + vmensaje, this);
                    }
                }
            }
            catch (Exception ex)
            {
                ModalShow("#ModalProyectoestatus");
                Toast.Error("Error al procesar estatus : " + ex.Message, this);
            }
        }

        protected void btneventgrid_Click(object sender, EventArgs e)
        {
            try
            {

                int id_proyecto_estatus = Convert.ToInt32(hdfid_proyecto_estatus.Value == "" ? "0" : hdfid_proyecto_estatus.Value);
                if (id_proyecto_estatus > 0)
                {
                    proyectos_estatus PE = GetProyectoEstatus(id_proyecto_estatus);
                    if (PE != null)
                    {
                        txtestatus.Text = PE.estatus;
                        chkactivo.Checked = PE.activo;                      
                        ModalShow("#ModalProyectoestatus");
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar estatus : " + ex.Message, this);
            }
        }

        protected void btneliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int id_proyecto_estatus = Convert.ToInt32(hdfid_proyecto_estatus.Value == "" ? "0" : hdfid_proyecto_estatus.Value);
                proyectos_estatus PE = new proyectos_estatus();
                PE.id_proyecto_estatus = id_proyecto_estatus;
                string vmensaje = Eliminar(id_proyecto_estatus);
                if (vmensaje == "")
                {
                    CargarCatalogo();
                    Toast.Success("proyecto estus eliminado correctamente.", "Mensaje del sistema", this);
                }
                else
                {
                    Toast.Error("Error al eliminar proyecto estatus: " + vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al eliminar proyecto estus : " + ex.Message, this);
            }
        }
    }
}