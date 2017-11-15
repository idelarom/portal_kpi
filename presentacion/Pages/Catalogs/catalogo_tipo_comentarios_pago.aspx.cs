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
    public partial class catalogo_tipo_comentarios_pago : System.Web.UI.Page
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

        private DataTable GetAlltipocomentario()
        {
            DataTable dt = new DataTable();
            try
            {
                TipoComentariosPagoCOM getall = new TipoComentariosPagoCOM();
                dt = getall.SelectAll();
            }
            catch (Exception ex)
            {
                dt = new DataTable();
            }
            return dt;
        }

        private comments_types_payments Gettipocomentario(int id_comment_type_payment)
        {
            comments_types_payments dt = new comments_types_payments();
            try
            {
                TipoComentariosPagoCOM getct = new TipoComentariosPagoCOM();
                dt = getct.Comentario(id_comment_type_payment);
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
                DataTable dt = GetAlltipocomentario();
                repeat_comment_type_payment.DataSource = dt;
                repeat_comment_type_payment.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar catalogo: " + ex.Message, this);
            }
        }

        private string Agregar(comments_types_payments id_comment_type_payment)
        {
            TipoComentariosPagoCOM bono = new TipoComentariosPagoCOM();
            string vmensaje = bono.Agregar(id_comment_type_payment);

            return vmensaje;
        }
        private string Editar(comments_types_payments id_comment_type_payment)
        {
            TipoComentariosPagoCOM bono = new TipoComentariosPagoCOM();
            string vmensaje = bono.Editar(id_comment_type_payment);

            return vmensaje;
        }

        private string Eliminar(int id_comment_type_payment)
        {
            TipoComentariosPagoCOM bono = new TipoComentariosPagoCOM();
            string vmensaje = bono.Eliminar(id_comment_type_payment);

            return vmensaje;
        }

        protected void lnknuevocomentario_Click(object sender, EventArgs e)
        {
            txtdescripcion.Text = "";
            chkpagoparcial.Checked = false;
            chkpagoexepcional.Checked = false;
            ModalShow("#ModalTipoComentarios");
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            try
            {
                string vmensaje = string.Empty;
                int id_comment_type_payment = Convert.ToInt32(hdfid_comment_type_payment.Value == "" ? "0" : hdfid_comment_type_payment.Value);
                comments_types_payments Comentario = new comments_types_payments();

                if (id_comment_type_payment > 0) { Comentario.id_comment_type_payment = id_comment_type_payment; }
                Comentario.description = txtdescripcion.Text;
                Comentario.partial = chkpagoparcial.Checked;
                Comentario.outstanding_paid = chkpagoexepcional.Checked;
                Comentario.created_by = Session["usuario"] as string;
                Comentario.created = DateTime.Now;
                Comentario.modified_by = Session["usuario"] as string;
                Comentario.modified = DateTime.Now;

                if (Comentario.description == "")
                {
                    ModalShow("#ModalTipoComentarios");
                    Toast.Error("Error al procesar tipo comentario de pago agregado : Ingrese una descripcion", this);
                }
                else
                {
                    vmensaje = id_comment_type_payment > 0 ? Editar(Comentario) : Agregar(Comentario);
                    if (vmensaje == "")
                    {
                        txtdescripcion.Text = "";
                        chkpagoparcial.Checked = false;
                        chkpagoexepcional.Checked = false;
                        hdfid_comment_type_payment.Value = "";
                        CargarCatalogo();
                        Toast.Success("tipo de comentario de pago agregado correctamente.", "Mensaje del sistema", this);
                    }
                    else
                    {
                        ModalShow("#ModalTipoComentarios");
                        Toast.Error("Error al procesar tipo comentario de pago agregado : " + vmensaje, this);
                    }
                }
            }
            catch (Exception ex)
            {
                ModalShow("#ModalTipoComentarios");
                Toast.Error("Error al procesar tipo comentario de pago : " + ex.Message, this);
            }
        }

        protected void btneventgrid_Click(object sender, EventArgs e)
        {
            try
            {
                int id_comment_type_payment = Convert.ToInt32(hdfid_comment_type_payment.Value == "" ? "0" : hdfid_comment_type_payment.Value);
                if (id_comment_type_payment > 0)
                {
                    comments_types_payments comentario = Gettipocomentario(id_comment_type_payment);
                    if (comentario != null)
                    {
                        txtdescripcion.Text = comentario.description;
                        chkpagoparcial.Checked = Convert.ToBoolean(comentario.partial);
                        chkpagoexepcional.Checked = Convert.ToBoolean(comentario.outstanding_paid);
                        ModalShow("#ModalTipoComentarios");
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al procesar tipo comentario de pago : " + ex.Message, this);
            }
        }

        protected void btneliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int id_comment_type_payment = Convert.ToInt32(hdfid_comment_type_payment.Value == "" ? "0" : hdfid_comment_type_payment.Value);
                comments_types_payments comentario = new comments_types_payments();
                comentario.id_comment_type_payment = id_comment_type_payment;
                string vmensaje = Eliminar(id_comment_type_payment);
                if (vmensaje == "")
                {
                    CargarCatalogo();
                    Toast.Success("tipo comentario de pago eliminado correctamente.", "Mensaje del sistema", this);
                }
                else
                {
                    Toast.Error("Error al eliminar tipo comentario de pago : " + vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al eliminar tipo comentario de pago : " + ex.Message, this);
            }
        }
    }
}