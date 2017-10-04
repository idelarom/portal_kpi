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
    public partial class catalogo_proyectos_tecnologias : System.Web.UI.Page
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

        private DataTable GetRiesgosEstatus()
        {
            DataTable dt = new DataTable();
            try
            {

                ProyectosTecnologiasCOM PE = new ProyectosTecnologiasCOM();
                dt = PE.SelectAll();
            }
            catch (Exception)
            {
                dt = new DataTable();
            }
            return dt;
        }

        private proyectos_tecnologias GetProyectoEstatus(int id_proyecto_tecnologia)
        {
            proyectos_tecnologias dt = new proyectos_tecnologias();
            try
            {
                ProyectosTecnologiasCOM PE = new ProyectosTecnologiasCOM();
                dt = PE.tecnologia(id_proyecto_tecnologia);
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
                DataTable dt = GetRiesgosEstatus();
                repeat_riesgosestrategias.DataSource = dt;
                repeat_riesgosestrategias.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar catalogo: " + ex.Message, this);
            }
        }

        private string Agregar(proyectos_tecnologias id_proyecto_tecnologia)
        {
            ProyectosTecnologiasCOM PE = new ProyectosTecnologiasCOM();
            string vmensaje = PE.Agregar(id_proyecto_tecnologia);

            return vmensaje;
        }
        private string Editar(proyectos_tecnologias id_proyecto_tecnologia)
        {
            ProyectosTecnologiasCOM PE = new ProyectosTecnologiasCOM();
            string vmensaje = PE.Editar(id_proyecto_tecnologia);

            return vmensaje;
        }

        private string Eliminar(int id_proyecto_tecnologia)
        {
            ProyectosTecnologiasCOM PE = new ProyectosTecnologiasCOM();
            string vmensaje = PE.Eliminar(id_proyecto_tecnologia);

            return vmensaje;
        }

        protected void lnknuevoproyectoestatus_Click(object sender, EventArgs e)
        {
            txtestatus.Text = "";
            chkactivo.Checked = true;
            ModalShow("#ModalProyectoestatus");
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            try
            {
                string vmensaje = string.Empty;
                int id_proyecto_tecnologia = Convert.ToInt32(hdfid_proyecto_tecnologia.Value == "" ? "0" : hdfid_proyecto_tecnologia.Value);
                proyectos_tecnologias PE = new proyectos_tecnologias();
                PE.nombre = txtestatus.Text;

                if (id_proyecto_tecnologia > 0) { PE.id_proyecto_tecnologia = id_proyecto_tecnologia; }
                PE.activo = chkactivo.Checked;
                PE.usuario = Session["usuario"] as string;
                if (PE.nombre == "")
                {
                    ModalShow("#ModalProyectoestatus");
                    Toast.Error("Error al procesar Tecnologia : Ingrese un titulo", this);
                }
                else
                {
                    vmensaje = id_proyecto_tecnologia > 0 ? Editar(PE) : Agregar(PE);
                    if (vmensaje == "")
                    {
                        txtestatus.Text = "";
                        chkactivo.Checked = true;
                        hdfid_proyecto_tecnologia.Value = "";
                        CargarCatalogo();
                        Toast.Success("Tecnologia agregada correctamente.", "Mensaje del sistema", this);
                    }
                    else
                    {
                        ModalShow("#ModalProyectoestatus");
                        Toast.Error("Error al procesar Tecnologia : " + vmensaje, this);
                    }
                }
            }
            catch (Exception ex)
            {
                ModalShow("#ModalProyectoestatus");
                Toast.Error("Error al procesar Tecnologia : " + ex.Message, this);
            }
        }

        protected void btneventgrid_Click(object sender, EventArgs e)
        {
            try
            {

                int id_proyecto_tecnologia = Convert.ToInt32(hdfid_proyecto_tecnologia.Value == "" ? "0" : hdfid_proyecto_tecnologia.Value);
                if (id_proyecto_tecnologia > 0)
                {
                    proyectos_tecnologias PE = GetProyectoEstatus(id_proyecto_tecnologia);
                    if (PE != null)
                    {
                        txtestatus.Text = PE.nombre;
                        chkactivo.Checked = PE.activo;
                        ModalShow("#ModalProyectoestatus");
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar Tecnologia : " + ex.Message, this);
            }
        }

        protected void btneliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int id_proyecto_tecnologia = Convert.ToInt32(hdfid_proyecto_tecnologia.Value == "" ? "0" : hdfid_proyecto_tecnologia.Value);
                proyectos_tecnologias PE = new proyectos_tecnologias();
                PE.id_proyecto_tecnologia = id_proyecto_tecnologia;
                string vmensaje = Eliminar(id_proyecto_tecnologia);
                if (vmensaje == "")
                {
                    CargarCatalogo();
                    Toast.Success("Tecnologia eliminada correctamente.", "Mensaje del sistema", this);
                }
                else
                {
                    Toast.Error("Error al eliminar tecnologia: " + vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al eliminar tecnologia: " + ex.Message, this);
            }
        }
    }
}