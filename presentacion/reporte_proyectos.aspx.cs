using datos.Modelos;
using datos.NAVISION;
using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace presentacion
{
    public partial class reporte_proyectos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string usuario = Session["usuario"] as string;
                Boolean cliente = Convert.ToBoolean(Session["cliente"]);
                CargarComboPM();
                CargarComboCliente();
                rfinicio.Text = DateTime.Today.AddDays(-30).ToString("yyyy-MM-dd");
                rfin.Text = DateTime.Today.AddDays(30).ToString("yyyy-MM-dd");
            }
        }
        protected void lnkfiltro_Click(object sender, EventArgs e)
        {
            if (rfinicio.Text == "" || rfin.Text == "")
            {
                Alert.ShowAlertError("Ingrese la Fecha de inicio y la fecha de fin.",this);
            }
            else {

                IList<RadComboBoxItem> collection = ddlpm_x_proyecto.CheckedItems;
                string usuario_filtro = "";
                string usuario = Session["usuario"] as string;
                if (collection.Count > 0)
                {

                    foreach (RadComboBoxItem item in collection)
                    {
                        usuario_filtro = usuario_filtro + "'" + item.Value.ToString().Trim().ToUpper() + "',";
                    }
                    usuario_filtro = usuario_filtro.TrimEnd(',');
                    usuario_filtro = usuario_filtro == "0" ? "" : usuario_filtro;
                }
                IList<RadComboBoxItem> collection2 = ddlcliente_x_proyecto.CheckedItems;
                string cliente_filtro = "";
                if (collection2.Count > 0)
                {

                    foreach (RadComboBoxItem item in collection2)
                    {
                        cliente_filtro = cliente_filtro + "'" + item.Value.ToString().Trim().ToUpper() + "',";
                    }
                    cliente_filtro = cliente_filtro.TrimEnd(',');
                    cliente_filtro = cliente_filtro == "0" ? "" : cliente_filtro;
                }
                int id_cliente = Convert.ToInt32(Session["id_cliente"]);
                DateTime f_inicio = Convert.ToDateTime(rfinicio.Text);
                DateTime f_fin = Convert.ToDateTime(rfin.Text);
                CargarProyectos(usuario, Convert.ToBoolean(Session["administrador"]), usuario_filtro, id_cliente, f_inicio, f_fin, cliente_filtro);
            }
        }

        private void CargarComboPM()
        {
            try
            {
                ProyectosCOM proyectos = new ProyectosCOM();
                DataTable dt_pm = proyectos.sp_combo_pm_x_proyecto().Tables[0];
                ddlpm_x_proyecto.DataTextField = "pm";
                ddlpm_x_proyecto.DataValueField = "usuario";
                ddlpm_x_proyecto.DataSource = dt_pm;
                ddlpm_x_proyecto.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        private void CargarComboCliente()
        {
            try
            {
                ProyectosCOM proyectos = new ProyectosCOM();
                DataTable dt_pm = proyectos.sp_COMBO_CLIENTES_PROYECTOS().Tables[0];
                ddlcliente_x_proyecto.DataTextField = "razon_social";
                ddlcliente_x_proyecto.DataValueField = "idcliente";
                ddlcliente_x_proyecto.DataSource = dt_pm;
                ddlcliente_x_proyecto.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }
        private void CargarProyectos(string usuario, bool administrador, string usuario_filtro, int id_cliente, DateTime f_inicio, DateTime f_fin, 
            string cliente_filtro)
        {
            try
            {
                ReportesCOM reportes = new ReportesCOM();
                DataTable dt = reportes.sp_reporte_portal_proyectos(0, usuario, administrador, 
                    id_cliente, usuario_filtro,f_inicio, f_fin, cliente_filtro).Tables[0];
                th_pm.Visible = (administrador && Convert.ToInt32(Session["id_cliente"]) == 0);
                div_combo_pm_x_proyecto.Visible = (administrador && Convert.ToInt32(Session["id_cliente"]) == 0);
                if (dt.Rows.Count > 0)
                {
                    repeat_mis_proyectos.DataSource = dt.AsEnumerable().Take(10).CopyToDataTable();
                    repeat_mis_proyectos.DataBind();
                }
                else
                {
                    repeat_mis_proyectos.DataSource = null;
                    repeat_mis_proyectos.DataBind();
                }
                if (usuario_filtro == "")
                {
                    CargarComboPM();
                    CargarComboCliente();
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }
    }
}