using datos.Model;
using negocio.Componentes;
using System;
using System.Data;
using System.Linq;

namespace presentacion.Pages.Catalogs
{
    public partial class catalogo_permisos : System.Web.UI.Page
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
                CargarCatalogo("", 0);
            }
        }
    
        private DataTable GetPermisos(string permiso, int id_permiso)
        {
            DataTable dt = new DataTable();
            try
            {
                PermisosCOM permisos = new PermisosCOM();
                if (permiso == "" && id_permiso == 0)
                {
                    dt = permisos.SelectAll();
                }
                            
            }
            catch (Exception)
            {
                dt = new DataTable();
            }
            return dt;
        }

        private permisos GetPermiso(string permiso, int id_permiso)
        {
            permisos permiso_ = new permisos();
            try
            {
                PermisosCOM permisos = new PermisosCOM();
                if (permiso != "" && id_permiso == 0)
                {
                    permiso_ = permisos.SelectName(permiso);
                }
                else if (permiso == "" && id_permiso > 0)
                {
                    permiso_ = permisos.SelectId(id_permiso);
                }
            }
            catch (Exception)
            {
                permiso_ = null;
            }
            return permiso_;
        }

        private void CargarCatalogo(string permiso, int id_permiso)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = GetPermisos(permiso, id_permiso);
                repeat_permisos.DataSource = dt;
                repeat_permisos.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar catalogo de permisos: " + ex.Message, this);
            }
        }

        private void AgregarPermiso(string permiso_nombre)
        {
            permisos permiso = new permisos();
            permiso.permiso = permiso_nombre;
            permiso.usuario_creacion = Session["usuario"] as string;
            PermisosCOM permisos = new PermisosCOM();
            string vmensaje = permisos.Agregar(permiso);
            if (vmensaje == "")
            {
                ModalClose("#ModalPermisos");
                CargarCatalogo("", 0);
                txtpermiso.Text = "";
                txtid_permiso.Value = "";
                Toast.Success("Permiso agregado de manera correcta", "Mensaje del sistema", this);
            }
            else
            {
                ModalShow("#ModalPermisos");
                Toast.Error("Error al agregar permiso: " + vmensaje, this);
            }
        }

        private void EditarPermiso(int id_permiso,string permiso_nombre)
        {
            permisos permiso = new permisos();
            permiso.permiso = permiso_nombre;
            permiso.id_permiso = id_permiso;
            permiso.usuario_edicion = Session["usuario"] as string;
            PermisosCOM permisos = new PermisosCOM();
            string vmensaje = permisos.Editar(permiso);
            if (vmensaje == "")
            {
                ModalClose("#ModalPermisos");
                CargarCatalogo("", 0);
                txtpermiso.Text = "";
                txtid_permiso.Value = "";
                Toast.Success("Permiso editado de manera correcta", "Mensaje del sistema", this);
            }
            else
            {
                ModalShow("#ModalPermisos");
                Toast.Error("Error al editar permiso: " + vmensaje, this);
            }
        }
        private void EliminarPermiso(int id_permiso)
        {
            permisos permiso = new permisos();
            permiso.id_permiso = id_permiso;
            permiso.usuario_edicion = Session["usuario"] as string;
            PermisosCOM permisos = new PermisosCOM();
            string vmensaje = permisos.Eliminar(permiso);
            if (vmensaje == "")
            {
                ModalClose("#ModalPermisos");
                CargarCatalogo("", 0);
                txtpermiso.Text = "";
                txtid_permiso.Value = "";
                Toast.Success("Permiso eliminado de manera correcta", "Mensaje del sistema", this);
            }
            else
            {
                ModalShow("#ModalPermisos");
                Toast.Error("Error al eliminar permiso: " + vmensaje, this);
            }
        }
        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            try
            {
                int id_permiso = txtid_permiso.Value == "" ? 0 : Convert.ToInt32(txtid_permiso.Value);
                string permiso = txtpermiso.Text;
                if (id_permiso == 0)
                {
                    AgregarPermiso(permiso);
                }
                else {
                    EditarPermiso(id_permiso,permiso);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al procesar permiso: " + ex.Message, this);
            }
        }

        protected void lnknuevomenu_Click(object sender, EventArgs e)
        {
            ModalShow("#ModalPermisos");
        }

        protected void btneventgrid_Click(object sender, EventArgs e)
        {
            try
            {
                int id_permiso = txtid_permiso.Value == "" ? 0 : Convert.ToInt32(txtid_permiso.Value);
                string permiso = txtpermiso.Text;
                if (id_permiso > 0)
                {
                    permisos permiso_ = GetPermiso("",id_permiso);
                    if (permiso_ != null)
                    {
                        txtpermiso.Text = permiso_.permiso;
                        ModalShow("#ModalPermisos");
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar permiso: " + ex.Message, this);
            }
        }

        protected void btneliminarpermiso_Click(object sender, EventArgs e)
        {
            try
            {
                int id_permiso = txtid_permiso.Value == "" ? 0 : Convert.ToInt32(txtid_permiso.Value);
                string permiso = txtpermiso.Text;
                if (id_permiso > 0)
                {
                    EliminarPermiso(id_permiso);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al eliminar permiso: " + ex.Message, this);
            }
        }
    }
}