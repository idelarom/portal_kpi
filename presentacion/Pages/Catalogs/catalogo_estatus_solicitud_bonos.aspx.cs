using datos;
using datos.Model;
using negocio.Componentes.Compensaciones;
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
    public partial class catalogo_estatus_solicitud_bonos : System.Web.UI.Page
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

        private DataTable GetAllestatus()
        {
            DataTable dt = new DataTable();
            try
            {
                EstatusSolicitudBonosCOM getall = new EstatusSolicitudBonosCOM();
                dt = getall.SelectAll();
            }
            catch (Exception ex)
            {
                dt = new DataTable();
            }
            return dt;
        }

        private requests_status Getestatus(int id_request_status)
        {
            requests_status dt = new requests_status();
            try
            {
                EstatusSolicitudBonosCOM getct = new EstatusSolicitudBonosCOM();
                dt = getct.estatus(id_request_status);
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
                DataTable dt = GetAllestatus();
                repeat_requests_status.DataSource = dt;
                repeat_requests_status.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar catalogo: " + ex.Message, this);
            }
        }

        private string Agregar(requests_status id_request_status)
        {
            EstatusSolicitudBonosCOM bono = new EstatusSolicitudBonosCOM();
            string vmensaje = bono.Agregar(id_request_status);

            return vmensaje;
        }
        private string Editar(requests_status id_request_status)
        {
            EstatusSolicitudBonosCOM bono = new EstatusSolicitudBonosCOM();
            string vmensaje = bono.Editar(id_request_status);

            return vmensaje;
        }

        private string Eliminar(int id_request_status)
        {
            EstatusSolicitudBonosCOM bono = new EstatusSolicitudBonosCOM();
            string vmensaje = bono.Eliminar(id_request_status);

            return vmensaje;
        }

        protected void lnknuevoestatus_Click(object sender, EventArgs e)
        {
            txtestatus.Text = "";
            txtdescripcion.Text = "";
            ModalShow("#ModalEstatusSolicitud");
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            try
            {
                string vmensaje = string.Empty;
                int id_request_status = Convert.ToInt32(hdfid_request_status.Value == "" ? "0" : hdfid_request_status.Value);
                requests_status estatus = new requests_status();

                if (id_request_status > 0) { estatus.id_request_status = id_request_status; }
                estatus.name = txtestatus.Text;
                estatus.description = txtdescripcion.Text;
                estatus.modified_by = Session["usuario"] as string;
                estatus.modified = DateTime.Now;

                if (estatus.name == "")
                {
                    ModalShow("#ModalEstatusSolicitud");
                    Toast.Error("Error al procesar estatus, favor de agregar el nombre del estatus : Ingrese una descripcion", this);
                }
                else if (estatus.name == "")
                {
                    ModalShow("#ModalEstatusSolicitud");
                    Toast.Error("Error al procesar estatus, favor de agregar la descripcion : Ingrese una descripcion", this);
                }
                else
                {
                    vmensaje = id_request_status > 0 ? Editar(estatus) : Agregar(estatus);
                    if (vmensaje == "")
                    {

                        txtestatus.Text = "";
                        txtdescripcion.Text = "";
                        hdfid_request_status.Value = "";
                        CargarCatalogo();
                        Toast.Success("Estatus pago agregado correctamente.", "Mensaje del sistema", this);
                    }
                    else
                    {
                        ModalShow("#ModalEstatusSolicitud");
                        Toast.Error("Error al procesar Estatus : " + vmensaje, this);
                    }
                }
            }
            catch (Exception ex)
            {
                ModalShow("#ModalEstatusSolicitud");
                Toast.Error("Error al procesar Estatus : " + ex.Message, this);
            }
        }

        protected void btneventgrid_Click(object sender, EventArgs e)
        {
            try
            {
                int id_request_status = Convert.ToInt32(hdfid_request_status.Value == "" ? "0" : hdfid_request_status.Value);
                if (id_request_status > 0)
                {
                    requests_status estatus = Getestatus(id_request_status);
                    if (estatus != null)
                    {
                        txtestatus.Text = estatus.name;
                        txtdescripcion.Text = estatus.description;
                        ModalShow("#ModalEstatusSolicitud");
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al procesar Estatus : " + ex.Message, this);
            }
        }

        protected void btneliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int id_request_status = Convert.ToInt32(hdfid_request_status.Value == "" ? "0" : hdfid_request_status.Value);
                requests_status estatus = new requests_status();
                estatus.id_request_status = id_request_status;
                string vmensaje = Eliminar(id_request_status);
                if (vmensaje == "")
                {
                    CargarCatalogo();
                    Toast.Success("Estatus eliminado correctamente.", "Mensaje del sistema", this);
                }
                else
                {
                    Toast.Error("Error al eliminar Estatus : " + vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al eliminar Estatus : " + ex.Message, this);
            }
        }
    }
}