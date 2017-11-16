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
    public partial class catalogo_periodicidad_bonos : System.Web.UI.Page
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
        private DataTable GetAllPeriodicidad()
        {
            DataTable dt = new DataTable();
            try
            {
                PeriodicidadCOM getall = new PeriodicidadCOM();
                dt = getall.SelectAll();
            }
            catch (Exception ex)
            {
                dt = new DataTable();
            }
            return dt;
        }

        private periodicity GetPeriodicidad(int id_periodicity)
        {
            periodicity dt = new periodicity();
            try
            {
                PeriodicidadCOM getbt = new PeriodicidadCOM();
                dt = getbt.Periodicidad(id_periodicity);
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
                DataTable dt = GetAllPeriodicidad();
                repeat_periodicity.DataSource = dt;
                repeat_periodicity.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar catalogo: " + ex.Message, this);
            }
        }

        private string Agregar(periodicity id_periodicity)
        {
            PeriodicidadCOM Periodicidad = new PeriodicidadCOM();
            string vmensaje = Periodicidad.Agregar(id_periodicity);

            return vmensaje;
        }
        private string Editar(periodicity id_periodicity)
        {
            PeriodicidadCOM Periodicidad = new PeriodicidadCOM();
            string vmensaje = Periodicidad.Editar(id_periodicity);

            return vmensaje;
        }

        private string Eliminar(int id_periodicity)
        {
            PeriodicidadCOM Periodicidad = new PeriodicidadCOM();
            string vmensaje = Periodicidad.Eliminar(id_periodicity);

            return vmensaje;
        }

        protected void lnknuevaperiodicidad_Click(object sender, EventArgs e)
        {
            txtperiodicidad.Text = "";
            txtdescripcion.Text = "";
            ModalShow("#Modalperiodicidad");
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {

            try
            {
                string vmensaje = string.Empty;
                int id_periodicity = Convert.ToInt32(hdfid_periodicity.Value == "" ? "0" : hdfid_periodicity.Value);
                periodicity Periodicidad = new periodicity();

                if (id_periodicity > 0) { Periodicidad.id_periodicity = id_periodicity; }
                Periodicidad.name = txtperiodicidad.Text;
                Periodicidad.description = txtdescripcion.Text;
                Periodicidad.created_by = Session["usuario"] as string;
                Periodicidad.created = DateTime.Now;
                Periodicidad.modified_by = Session["usuario"] as string;
                Periodicidad.modified = DateTime.Now;
                if (Periodicidad.name == "")
                {
                    ModalShow("#Modalperiodicidad");
                    Toast.Error("Error al procesar periodicidad : Ingrese el nombre de la periodicidad", this);
                }
                else if (Periodicidad.description == "")
                {
                    ModalShow("#Modalperiodicidad");
                    Toast.Error("Error al procesar periodicidad : Ingrese la descripcion", this);
                }
                else
                {
                    vmensaje = id_periodicity > 0 ? Editar(Periodicidad) : Agregar(Periodicidad);
                    if (vmensaje == "")
                    {
                        txtperiodicidad.Text = "";
                        txtdescripcion.Text = "";
                        hdfid_periodicity.Value = "";
                        CargarCatalogo();
                        Toast.Success("Estatus agregado correctamente.", "Mensaje del sistema", this);
                    }
                    else
                    {
                        ModalShow("#ModalTipoBonos");
                        Toast.Error("Error al procesar estatus : " + vmensaje, this);
                    }
                }
            }
            catch (Exception ex)
            {
                ModalShow("#ModalTipoBonos");
                Toast.Error("Error al procesar estatus : " + ex.Message, this);
            }
        }

        protected void btneventgrid_Click(object sender, EventArgs e)
        {
            try
            {

                int id_periodicity = Convert.ToInt32(hdfid_periodicity.Value == "" ? "0" : hdfid_periodicity.Value);
                if (id_periodicity > 0)
                {
                    periodicity Periodicidad = GetPeriodicidad(id_periodicity);
                    if (Periodicidad != null)
                    {
                        txtperiodicidad.Text = Periodicidad.name;
                        txtdescripcion.Text = Periodicidad.description;
                        ModalShow("#Modalperiodicidad");
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar periodicidad : " + ex.Message, this);
            }
        }

        protected void btneliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int id_periodicity = Convert.ToInt32(hdfid_periodicity.Value == "" ? "0" : hdfid_periodicity.Value);
                periodicity Periodicidad = new periodicity();
                Periodicidad.id_periodicity = id_periodicity;
                string vmensaje = Eliminar(id_periodicity);
                if (vmensaje == "")
                {
                    CargarCatalogo();
                    Toast.Success("periodicidad eliminado correctamente.", "Mensaje del sistema", this);
                }
                else
                {
                    Toast.Error("Error al eliminar periodicidad: " + vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al eliminar periodicidad: " + ex.Message, this);
            }
        }
    }
}