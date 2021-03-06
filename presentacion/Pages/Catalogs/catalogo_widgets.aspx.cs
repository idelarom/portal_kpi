﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio.Componentes;
using System.Data;

namespace presentacion.Pages.Catalogs
{
    public partial class catalogo_widgets : System.Web.UI.Page
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
                Cargar_catalogo_widgets(0);
            }
        }
        private void Cargar_catalogo_widgets(int id_widget)
        {
            try
            {
                WidgetsCOM widcom = new WidgetsCOM();
                DataSet ds = widcom.sp_catalogo_widgets(id_widget);
                DataTable dt = ds.Tables[0];
                grid_widgets.DataSource = ds.Tables[0];
                grid_widgets.DataBind();
                if (id_widget > 0)
                {
                    rtxtwidget.Text = dt.Rows[0]["widget"].ToString();
                    rtxtayuda.Text = dt.Rows[0]["texto_ayuda"].ToString();
                    rtxticono.Text = dt.Rows[0]["icono"].ToString();
                    rtxtejemplo_html.Text = dt.Rows[0]["ejemplo_html"].ToString();
                    rtxtnombrecodigo.Text = dt.Rows[0]["nombre_codigo"].ToString();
                    txtid_widget.Text = id_widget.ToString();
                    if (Convert.ToBoolean(dt.Rows[0]["individual"].ToString())==true)
                    {
                        cbxwindividual.Checked = true;
                    }
                    else
                    {
                        cbxwindividual.Checked = false;
                    }
                }
                }
            catch (Exception ex)
            {

                Toast.Error("Error al cargar el catalogo de widgets: " + ex.Message,this);
            }
        }
        private void Agregarwidget(string widget,string nombre_codigo, string icono, string ejemplo_html, string individual, string ayuda)
        {
            div_error.Visible = false;
            try
            {

                WidgetsCOM Widget = new WidgetsCOM();
                string usuario = Session["usuario"] as string;
                DataSet ds = Widget.sp_agregar_widgets(widget, icono, ejemplo_html, usuario, nombre_codigo , individual, ayuda);
                DataTable dt = ds.Tables[0];
                string vmensaje = (dt.Rows.Count == 0 || !dt.Columns.Contains("mensaje")) ? "Error al guardar Widget. Intentelo Nuevamente." : dt.Rows[0]["mensaje"].ToString().Trim();
                if (vmensaje == "")
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                  "AlertGO('Widget Guardado Correctamente', 'catalogo_widgets.aspx');", true);
                }
                else
                {
                    div_error.Visible = true;
                    lblerror.Text = vmensaje;
                }
            }
            catch (Exception ex)
            {
                div_error.Visible = true;
                lblerror.Text = "Error al guardar widget: " + ex.Message;
            }
        }

        private void Editarwidget(int id_widget, string nombre_codigo, string widget, string icono, string ejemplo_html, string individual, string ayuda)
        {
            div_error.Visible = false;
            try
            {

                WidgetsCOM Widget = new WidgetsCOM();
                string usuario = Session["usuario"] as string;
                DataSet ds = Widget.sp_editar_widgets(id_widget,widget, icono, ejemplo_html, usuario, nombre_codigo, individual, ayuda);
                DataTable dt = ds.Tables[0];
                string vmensaje = (dt.Rows.Count == 0 || !dt.Columns.Contains("mensaje")) ? "Error al editar perfil. Intentelo Nuevamente." : dt.Rows[0]["mensaje"].ToString().Trim();
                if (vmensaje == "")
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                  "AlertGO('Widget Guardado Correctamente', 'catalogo_widgets.aspx');", true);
                }
                else
                {
                    div_error.Visible = true;
                    lblerror.Text = vmensaje;
                }
            }
            catch (Exception ex)
            {
                div_error.Visible = true;
                lblerror.Text = "Error al editar widget: " + ex.Message;
            }
        }
        private void Eliminarwidget(int id_widget, string comenatrios)
        {
            try
            {
                WidgetsCOM Widget = new WidgetsCOM();
                string usuario = Session["usuario"] as string;
                DataSet ds = Widget.sp_borrar_widgets(id_widget, usuario, comenatrios);
                DataTable dt = ds.Tables[0];
                string vmensaje = (dt.Rows.Count == 0 || !dt.Columns.Contains("mensaje")) ? "Error al eliminar widget. Intentelo Nuevamente." : dt.Rows[0]["mensaje"].ToString().Trim();
                if (vmensaje == "")
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                  "AlertGO('Widget eliminado Correctamente', 'catalogo_widgets.aspx');", true);
                }
                else
                {
                    div_error.Visible = true;
                    Toast.Error("Error al eliminar widget: " + vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                div_error.Visible = true;
                Toast.Error("Error al eliminar widget: " + ex.Message, this);
            }
        }
        protected void lnknuevowidget_Click(object sender, EventArgs e)
        {
            div_widget.Visible = true;
            txtid_widget.Text = "";
            rtxtwidget.Text = "";
            rtxticono.Text = "";
            rtxtejemplo_html.Text = "";
            rtxtnombrecodigo.Text = "";
            cbxwindividual.Checked = false;
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "CaracteresRestantes();", true);
            ModalShow("#myModal");
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (rtxtwidget.Text != "" && rtxticono.Text != "")
                {
                    string widget = rtxtwidget.Text.Trim();
                    string icono = rtxticono.Text.Trim();
                    string ejemplo_html = rtxtejemplo_html.Text.Trim();
                    string nombre_codigo = rtxtnombrecodigo.Text.Trim();
                    string individual = "";
                    string ayuda = rtxtayuda.Text.Trim();
                    if (cbxwindividual.Checked== true)
                    {
                        individual = "true";
                    }
                    else
                    { 
                        individual = "false";
                    }
                    if (txtid_widget.Text == "")
                    {
                        Agregarwidget(widget, nombre_codigo, icono, ejemplo_html, individual, ayuda);
                    }
                    else
                    {
                        Editarwidget(Convert.ToInt32(txtid_widget.Text), nombre_codigo, widget, icono, ejemplo_html, individual, ayuda);
                    }
                }
                else
                {
                    Toast.Info("favor de llenar los campos","DATOS REQUERIDOS", this);
                }
            }
            catch (Exception ex)
            {

                Toast.Error("Error al relacionar usuario: " + ex.Message, this);
            }
           
        }
        protected void lnkcommand2_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            int id_widget = Convert.ToInt32(lnk.CommandArgument);
            switch (lnk.CommandName.ToLower())
            {
                case "eliminar":
                    Eliminarwidget(id_widget, hdfmotivos.Value.Trim());
                    break;
            }
        }
        protected void btneventgrid_Click(object sender, EventArgs e)
        {
            int id_widget = Convert.ToInt32(hdfid_widget.Value);
            //div_empleados.Visible = false;
            div_widget.Visible = false;
            switch (hdfcommand.Value.ToLower())
            {
                case "actualizar":
                    //div_empleados.Visible = true;
                    div_widget.Visible = true;
                    Cargar_catalogo_widgets(id_widget);
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "CaracteresRestantes();", true);
                    ModalShow("#myModal");
                    break;
                //case "usuarios":
                //    div_empleados.Visible = true;
                //    div_perfil.Visible = false;
                //    CargarCatalogo(id_perfil);
                //    ModalShow("#myModal");
                //    break;
            }
        }
    }
}