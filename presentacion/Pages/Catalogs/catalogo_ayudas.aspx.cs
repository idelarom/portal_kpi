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
    public partial class catalogo_ayudas : System.Web.UI.Page
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
                CargarListadoAyudasPadre();
            }
        }

        private DataTable GetAyudas()
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

        private ayudas GetAyuda(int id_ayuda)
        {
            ayudas dt = new ayudas();
            try
            {
                AyudaCOM ayudas = new AyudaCOM();
                dt = ayudas.Ayuda(id_ayuda);
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }

        private void CargarListadoAyudasPadre()
        {
            try
            {
                AyudaCOM ayudas = new AyudaCOM();
                DataTable dt = ayudas.SelectAll();
              
                ddlpadre.DataTextField = "titulo";
                ddlpadre.DataValueField = "id_ayuda";
                ddlpadre.DataSource = dt;
                ddlpadre.DataBind();
                ddlpadre.Items.Insert(0, new ListItem("--Sin módulo padre", "0"));
            }
            catch (Exception ex)
            {
                Toast.Error("Error al llenar listado de categorias padre: " + ex.Message, this);
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

        private void CargarCatalogo()
        {
            try
            {
                DataTable dt = GetAyudas();
                repeat_ayudas.DataSource = dt;
                repeat_ayudas.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar catalogo: "+ex.Message,this);
            }
        }

        protected void lnknuevaayuda_Click(object sender, EventArgs e)
        {
            txttitulo.Text = "";
            txtdescripcion.Text = "";
            txtcodigohtml.Text = "";
            txticono.Text = "";
            hdfid_ayuda.Value = "";
            ddlpadre.SelectedValue = "0";
            ModalShow("#ModalAyudas");
        }

        protected void btneventgrid_Click(object sender, EventArgs e)
        {
            try
            {

                int id_ayuda = Convert.ToInt32(hdfid_ayuda.Value == "" ? "0" : hdfid_ayuda.Value);
                if (id_ayuda > 0)
                {
                    ayudas ayuda = GetAyuda(id_ayuda);
                    if (ayuda != null)
                    {
                        txttitulo.Text = ayuda.titulo;
                        txtdescripcion.Text = ayuda.descripcion;
                        txtcodigohtml.Text = ayuda.codigo_html;
                        ddlpadre.SelectedValue = ayuda.id_ayuda_padre == null ? "0" : ayuda.id_ayuda_padre.ToString();
                        ddltipoarchivo.SelectedValue = ayuda.video ? "1" : "0";
                        txticono.Text = ayuda.icono;
                        ModalShow("#ModalAyudas");
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar ayudas : " + ex.Message, this);
            }
        }

        protected void btneliminarpermiso_Click(object sender, EventArgs e)
        {

            try
            {
                int id_ayuda = Convert.ToInt32(hdfid_ayuda.Value == "" ? "0" : hdfid_ayuda.Value);
                ayudas ayuda = new ayudas();
                ayuda.id_ayuda = id_ayuda;
                ayuda.usuario_edicion = Session["usuario"] as string;
                string vmensaje = Eliminar(ayuda);
                if (vmensaje == "")
                {
                    CargarCatalogo();
                    Toast.Success("Ayuda eliminada correctamente.", "Mensaje del sistema", this);
                }
                else
                {
                    Toast.Error("Error al eliminar ayudas: " + vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al eliminar ayuda : " + ex.Message, this);
            }
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            try
            {
                string vmensaje = string.Empty;
                int id_ayuda = Convert.ToInt32(hdfid_ayuda.Value == "" ? "0" : hdfid_ayuda.Value);
                ayudas ayuda = new ayudas();
                ayuda.titulo = txttitulo.Text;
                ayuda.descripcion = txtdescripcion.Text;
                ayuda.codigo_html = txtcodigohtml.Text;
                ayuda.id_ayuda_padre = null;
                if (ddlpadre.SelectedValue != "0")
                {
                    ayuda.id_ayuda_padre = Convert.ToInt32(ddlpadre.SelectedValue);
                }
                if (id_ayuda > 0) { ayuda.id_ayuda = id_ayuda; }
                bool video = ddltipoarchivo.SelectedValue == "1";
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/"));
                string src = fuparchivo.HasFile ? "img/helps/" +Guid.NewGuid().ToString()+ Path.GetExtension(fuparchivo.FileName): "";
                ayuda.src = src;
                ayuda.video = video;
                ayuda.icono = txticono.Text;
                ayuda.usuario_creacion = Session["usuario"] as string;
                ayuda.usuario_edicion = Session["usuario"] as string;
                string extension = Path.GetExtension(fuparchivo.FileName);
                if (ayuda.titulo == "")
                {
                    ModalShow("#ModalAyudas");
                    Toast.Error("Error al procesar ayudas : Ingrese un titulo", this);
                }
                else if (!fuparchivo.HasFile && ayuda.descripcion == "")
                {
                    ModalShow("#ModalAyudas");
                    Toast.Error("Debe incluir un archivo.", this);
                }
                else if (fuparchivo.HasFile && extension != ".mp4" && extension != ".jpg"
                        && extension != ".png"
                        && extension != ".jpeg")
                {
                    ModalShow("#ModalAyudas");
                    Toast.Error("Formato de archivo no permitido", this);
                }
                else
                {
                    vmensaje = id_ayuda > 0 ? Editar(ayuda) : Agregar(ayuda);
                    if (vmensaje == "")
                    {
                        if (src != "")
                        {
                            funciones.UploadFile(fuparchivo, dirInfo + src.Trim(), this.Page);
                        }
                        txttitulo.Text = "";
                        txtdescripcion.Text = "";
                        txtcodigohtml.Text = "";
                        txticono.Text = "";
                        hdfid_ayuda.Value = "";
                        ddlpadre.SelectedValue = "0";
                        CargarCatalogo();
                        Toast.Success("Ayuda agregada correctamente.", "Mensaje del sistema", this);
                    }
                    else
                    {
                        ModalShow("#ModalAyudas");
                        Toast.Error("Error al procesar ayudas : " + vmensaje, this);
                    }
                }
            }
            catch (Exception ex)
            {
                ModalShow("#ModalAyudas");
                Toast.Error("Error al procesar ayudas : " + ex.Message, this);
            }
        }
    }
}