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

                ProyectosEstatusCOM PE = new ProyectosEstatusCOM();
                dt = PE.SelectAll();
            }
            catch (Exception)
            {
                dt = new DataTable();
            }
            return dt;
        }

        private proyectos_estatus GetProyectoEstatus(int id_proyectos_estatus)
        {
            proyectos_estatus dt = new proyectos_estatus();
            try
            {
                ProyectosEstatusCOM PE = new ProyectosEstatusCOM();
                dt = PE.estatus(id_proyectos_estatus);
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

        private string Agregar(proyectos_estatus id_proyectos_estatus)
        {
            ProyectosEstatusCOM PE = new ProyectosEstatusCOM();
            string vmensaje = PE.Agregar(id_proyectos_estatus);
            if (vmensaje == "")
            {
                return "";
            }
            else
            {
                return vmensaje;
            }
        }
        private string Editar(proyectos_estatus id_proyectos_estatus)
        {
            ProyectosEstatusCOM PE = new ProyectosEstatusCOM();
            string vmensaje = PE.Editar(id_proyectos_estatus);
            if (vmensaje == "")
            {
                return "";
            }
            else
            {
                return vmensaje;
            }
        }

        private string Eliminar(int id_proyectos_estatus)
        {
            ProyectosEstatusCOM PE = new ProyectosEstatusCOM();
            string vmensaje = PE.Eliminar(id_proyectos_estatus);
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
            chkactivo.Checked = false;
            ModalShow("#ModalProyectoestatus");
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            try
            {
                string vmensaje = string.Empty;
                int id_proyectos_estatus = Convert.ToInt32(hdfid_proyectos_estatus.Value == "" ? "0" : hdfid_proyectos_estatus.Value);
                proyectos_estatus PE = new proyectos_estatus();
                PE.estatus = txtestatus.Text;
                
                if (id_proyectos_estatus > 0) { PE.id_proyectos_estatus = id_proyectos_estatus; }
                PE.activo = chkactivo.Checked;
                PE.usuario = Session["usuario"] as string;
                if (PE.estatus == "")
                {
                    ModalShow("#ModalProyectoestatus");
                    Toast.Error("Error al procesar el proyecto estus : Ingrese un titulo", this);
                }               
                else
                {
                    vmensaje = id_proyectos_estatus > 0 ? Editar(PE) : Agregar(PE);
                    if (vmensaje == "")
                    {
                        txtestatus.Text = "";
                        chkactivo.Checked = false;
                        CargarCatalogo();
                        Toast.Success("proyecto estus agregado correctamente.", "Mensaje del sistema", this);
                    }
                    else
                    {
                        ModalShow("#ModalProyectoestatus");
                        Toast.Error("Error al procesar proyecto estus : " + vmensaje, this);
                    }
                }
            }
            catch (Exception ex)
            {
                ModalShow("#ModalProyectoestatus");
                Toast.Error("Error al procesar proyecto estus : " + ex.Message, this);
            }
        }

        protected void btneventgrid_Click(object sender, EventArgs e)
        {
            try
            {

                int id_proyectos_estatus = Convert.ToInt32(hdfid_proyectos_estatus.Value == "" ? "0" : hdfid_proyectos_estatus.Value);
                if (id_proyectos_estatus > 0)
                {
                    proyectos_estatus PE = GetProyectoEstatus(id_proyectos_estatus);
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
                Toast.Error("Error al cargar proyecto estus : " + ex.Message, this);
            }
        }

        protected void btneliminar_Click(object sender, EventArgs e)
        {

            try
            {
                int id_proyectos_estatus = Convert.ToInt32(hdfid_proyectos_estatus.Value == "" ? "0" : hdfid_proyectos_estatus.Value);
                proyectos_estatus PE = new proyectos_estatus();
                PE.id_proyectos_estatus = id_proyectos_estatus;
                string vmensaje = Eliminar(id_proyectos_estatus);
                if (vmensaje == "")
                {
                    CargarCatalogo();
                    Toast.Success("proyecto estus eliminad correctamente.", "Mensaje del sistema", this);
                }
                else
                {
                    Toast.Error("Error al eliminar proyecto estus: " + vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al eliminar proyecto estus : " + ex.Message, this);
            }
        }
    }
}