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
    public partial class catalogo_riesgos_estatus : System.Web.UI.Page
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

                RiesgosEstatusCOM PE = new RiesgosEstatusCOM();
                dt = PE.SelectAll();
            }
            catch (Exception)
            {
                dt = new DataTable();
            }
            return dt;
        }

        private riesgos_estatus GetProyectoEstatus(int id_riesgos_estatus)
        {
            riesgos_estatus dt = new riesgos_estatus();
            try
            {
                RiesgosEstatusCOM PE = new RiesgosEstatusCOM();
                dt = PE.estatus(id_riesgos_estatus);
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
                repeat_riesgosestatus.DataSource = dt;
                repeat_riesgosestatus.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar catalogo: " + ex.Message, this);
            }
        }

        private string Agregar(riesgos_estatus id_riesgos_estatus)
        {
            RiesgosEstatusCOM PE = new RiesgosEstatusCOM();
            string vmensaje = PE.Agregar(id_riesgos_estatus);
            if (vmensaje == "")
            {
                return "";
            }
            else
            {
                return vmensaje;
            }
        }
        private string Editar(riesgos_estatus id_riesgos_estatus)
        {
            RiesgosEstatusCOM PE = new RiesgosEstatusCOM();
            string vmensaje = PE.Editar(id_riesgos_estatus);
            if (vmensaje == "")
            {
                return "";
            }
            else
            {
                return vmensaje;
            }
        }

        private string Eliminar(int id_riesgos_estatus)
        {
            RiesgosEstatusCOM PE = new RiesgosEstatusCOM();
            string vmensaje = PE.Eliminar(id_riesgos_estatus);
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
                int id_riesgos_estatus = Convert.ToInt32(hdfid_riesgos_estatus.Value == "" ? "0" : hdfid_riesgos_estatus.Value);
                riesgos_estatus PE = new riesgos_estatus();
                PE.estatus = txtestatus.Text;

                if (id_riesgos_estatus > 0) { PE.id_riesgos_estatus = id_riesgos_estatus; }
                PE.activo = chkactivo.Checked;
                PE.usuario = Session["usuario"] as string;
                if (PE.estatus == "")
                {
                    ModalShow("#ModalProyectoestatus");
                    Toast.Error("Error al procesar estatus : Ingrese un titulo", this);
                }
                else
                {
                    vmensaje = id_riesgos_estatus > 0 ? Editar(PE) : Agregar(PE);
                    if (vmensaje == "")
                    {
                        txtestatus.Text = "";
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

                int id_riesgos_estatus = Convert.ToInt32(hdfid_riesgos_estatus.Value == "" ? "0" : hdfid_riesgos_estatus.Value);
                if (id_riesgos_estatus > 0)
                {
                    riesgos_estatus PE = GetProyectoEstatus(id_riesgos_estatus);
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
                int id_riesgos_estatus = Convert.ToInt32(hdfid_riesgos_estatus.Value == "" ? "0" : hdfid_riesgos_estatus.Value);
                riesgos_estatus PE = new riesgos_estatus();
                PE.id_riesgos_estatus = id_riesgos_estatus;
                string vmensaje = Eliminar(id_riesgos_estatus);
                if (vmensaje == "")
                {
                    CargarCatalogo();
                    Toast.Success("Estatus eliminado correctamente.", "Mensaje del sistema", this);
                }
                else
                {
                    Toast.Error("Error al eliminar estatus: " + vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al eliminar estatus: " + ex.Message, this);
            }
        }
    }
}