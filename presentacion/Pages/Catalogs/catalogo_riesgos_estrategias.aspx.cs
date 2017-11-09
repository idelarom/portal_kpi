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
    public partial class catalogo_riesgos_estrategias : System.Web.UI.Page
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

                RiesgosEstrategiaCOM PE = new RiesgosEstrategiaCOM();
                dt = PE.SelectAll();
            }
            catch (Exception)
            {
                dt = new DataTable();
            }
            return dt;
        }

        private riesgos_estrategia GetProyectoEstatus(int id_riesgo_estrategia)
        {
            riesgos_estrategia dt = new riesgos_estrategia();
            try
            {
                RiesgosEstrategiaCOM PE = new RiesgosEstrategiaCOM();
                dt = PE.estrategia(id_riesgo_estrategia);
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

        private string Agregar(riesgos_estrategia id_riesgo_estrategia)
        {
            RiesgosEstrategiaCOM PE = new RiesgosEstrategiaCOM();
            string vmensaje = PE.Agregar(id_riesgo_estrategia);

            return vmensaje;
        }
        private string Editar(riesgos_estrategia id_riesgo_estrategia)
        {
            RiesgosEstrategiaCOM PE = new RiesgosEstrategiaCOM();
            string vmensaje = PE.Editar(id_riesgo_estrategia);

            return vmensaje;
        }

        private string Eliminar(int id_riesgo_estrategia)
        {
            RiesgosEstrategiaCOM PE = new RiesgosEstrategiaCOM();
            string vmensaje = PE.Eliminar(id_riesgo_estrategia);

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
                int id_riesgo_estrategia = Convert.ToInt32(hdfid_riesgo_estrategia.Value == "" ? "0" : hdfid_riesgo_estrategia.Value);
                riesgos_estrategia PE = new riesgos_estrategia();
                PE.nombre = txtestatus.Text;

                if (id_riesgo_estrategia > 0) { PE.id_riesgo_estrategia = id_riesgo_estrategia; }
                PE.activo = chkactivo.Checked;
                PE.usuario = Session["usuario"] as string;
                PE.valor_min = txtmin.Text==""? Convert.ToByte(0) : Convert.ToByte(txtmin.Text);
                PE.valor_max = txtmax.Text == "" ? Convert.ToByte(0) : Convert.ToByte(txtmax.Text);
                if (PE.nombre == "")
                {
                    ModalShow("#ModalProyectoestatus");
                    Toast.Error("Error al procesar Estrategia : Ingrese un titulo", this);
                }
                else if (PE.valor_min == 0)
                {
                    ModalShow("#ModalProyectoestatus");
                    Toast.Error("Error al procesar Estrategia : Ingrese un valor minimo", this);
                }
                else if (PE.valor_max == 0)
                {
                    ModalShow("#ModalProyectoestatus");
                    Toast.Error("Error al procesar Estrategia : Ingrese un valor maximo", this);
                }
                else if (PE.valor_max < PE.valor_min)
                {
                    ModalShow("#ModalProyectoestatus");
                    Toast.Error("Error al procesar Estrategia : El valor maximo debe ser mayor o igual al minimo.", this);
                }
                else
                {
                    vmensaje = id_riesgo_estrategia > 0 ? Editar(PE) : Agregar(PE);
                    if (vmensaje == "")
                    {
                        txtestatus.Text = "";
                        chkactivo.Checked = false;
                        hdfid_riesgo_estrategia.Value = "";
                        CargarCatalogo();
                        Toast.Success("Estrategia agregada correctamente.", "Mensaje del sistema", this);
                    }
                    else
                    {
                        ModalShow("#ModalProyectoestatus");
                        Toast.Error("Error al procesar Estrategia : " + vmensaje, this);
                    }
                }
            }
            catch (Exception ex)
            {
                ModalShow("#ModalProyectoestatus");
                Toast.Error("Error al procesar Estrategia : " + ex.Message, this);
            }
        }

        protected void btneventgrid_Click(object sender, EventArgs e)
        {
            try
            {

                int id_riesgo_estrategia = Convert.ToInt32(hdfid_riesgo_estrategia.Value == "" ? "0" : hdfid_riesgo_estrategia.Value);
                if (id_riesgo_estrategia > 0)
                {
                    riesgos_estrategia PE = GetProyectoEstatus(id_riesgo_estrategia);
                    if (PE != null)
                    {
                        txtestatus.Text = PE.nombre;
                        txtmax.Text = PE.valor_max.ToString();
                        txtmin.Text = PE.valor_min.ToString();
                        chkactivo.Checked = PE.activo;
                        ModalShow("#ModalProyectoestatus");
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar Estrategia : " + ex.Message, this);
            }
        }

        protected void btneliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int id_riesgo_estrategia = Convert.ToInt32(hdfid_riesgo_estrategia.Value == "" ? "0" : hdfid_riesgo_estrategia.Value);
                riesgos_estrategia PE = new riesgos_estrategia();
                PE.id_riesgo_estrategia = id_riesgo_estrategia;
                string vmensaje = Eliminar(id_riesgo_estrategia);
                if (vmensaje == "")
                {
                    CargarCatalogo();
                    Toast.Success("Estrategia eliminada correctamente.", "Mensaje del sistema", this);
                }
                else
                {
                    Toast.Error("Error al eliminar estrategia: " + vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al eliminar estrategia: " + ex.Message, this);
            }
        }
    }
}