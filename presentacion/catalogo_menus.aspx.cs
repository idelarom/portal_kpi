using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio.Componentes;
using System.Data;

namespace presentacion
{
    public partial class catalogo_menus : System.Web.UI.Page
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
                Cargar_catalogo_menus(0);
            }
        }
        private void Cargar_catalogo_menus(int id_menu)
        {
            try
            {
                MenusCOM menu = new MenusCOM();
                DataSet ds = menu.sp_catalogo_menus(id_menu);
                DataTable dt = ds.Tables[0];
                grid_menus.DataSource = ds.Tables[0];
                grid_menus.DataBind();
                if (id_menu > 0)
                {
                    rtxtmenu.Text = dt.Rows[0]["name"].ToString();
                    rtxticono.Text = dt.Rows[0]["icon_ad"].ToString();
                    
                    if (!String.IsNullOrEmpty(dt.Rows[0]["id_menu_padre"].ToString()))
                    {
                        ddlmenupadre.SelectedIndex = Convert.ToInt32(dt.Rows[0]["id_menu_padre"].ToString());
                        rtxtUrl.Text = dt.Rows[0]["menu"].ToString();
                        Chkmenupadre.Checked = false;
                    }
                    else
                    {
                        Chkmenupadre.Checked = true;
                    }
                   
                    txtid_menu.Text = id_menu.ToString();
                }
            }
            catch (Exception ex)
            {

                Toast.Error("Error al cargar el catalogo de widgets: " + ex.Message, this);
            }
        }

        private void Agregarmenu(string menu, string icono, Int32 id_menu_padre, string url)
        {
            div_error.Visible = false;
            try
            {

                MenusCOM menus = new MenusCOM();
                string usuario = Session["usuario"] as string;
                DataSet ds = menus.sp_agregar_menus(id_menu_padre, menu, url, icono, usuario);
                DataTable dt = ds.Tables[0];
                string vmensaje = (dt.Rows.Count == 0 || !dt.Columns.Contains("mensaje")) ? "Error al guardar menú. Intentelo Nuevamente." : dt.Rows[0]["mensaje"].ToString().Trim();
                if (vmensaje == "")
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                  "AlertGO('Menú Guardado Correctamente', 'catalogo_menus.aspx');", true);
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
                lblerror.Text = "Error al guardar menu: " + ex.Message;
            }
        }
        private void EditarMenu(int id_menu , string menu, string icono, int id_menu_padre, string url)
        {
            div_error.Visible = false;
            try
            {

                MenusCOM menus = new MenusCOM();
                string usuario = Session["usuario"] as string;
                DataSet ds = menus.sp_editar_menus(id_menu, id_menu_padre, menu, url, icono, usuario);
                DataTable dt = ds.Tables[0];
                string vmensaje = (dt.Rows.Count == 0 || !dt.Columns.Contains("mensaje")) ? "Error al editar menú . Intentelo Nuevamente." : dt.Rows[0]["mensaje"].ToString().Trim();
                if (vmensaje == "")
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                  "AlertGO('Menú Guardado Correctamente', 'catalogo_menus.aspx');", true);
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
                lblerror.Text = "Error al editar menú: " + ex.Message;
            }
        }
        private void Eliminarwidget(int id_menu, string comenatrios)
        {
            try
            {
                MenusCOM menus = new MenusCOM();
                string usuario = Session["usuario"] as string;
                DataSet ds = menus.sp_borrar_menus(id_menu, usuario, comenatrios);
                DataTable dt = ds.Tables[0];
                string vmensaje = (dt.Rows.Count == 0 || !dt.Columns.Contains("mensaje")) ? "Error al eliminar menú. Intentelo Nuevamente." : dt.Rows[0]["mensaje"].ToString().Trim();
                if (vmensaje == "")
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                  "AlertGO('Menú eliminado Correctamente', 'catalogo_menus.aspx');", true);
                }
                else
                {
                    div_error.Visible = true;
                    Toast.Error("Error al eliminar menú: " + vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                div_error.Visible = true;
                Toast.Error("Error al eliminar menú: " + ex.Message, this);
            }
        }

        protected void lnknuevomenu_Click(object sender, EventArgs e)
        {
            div_menu.Visible = true;
            txtid_menu.Text = "";
            rtxtmenu.Text = "";
            rtxticono.Text = "";
            rtxtUrl.Text = "";            
            ModalShow("#myModal");
        }
        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Chkmenupadre.Checked)
                {

                }
                else
                {

                }
                
                if (rtxtmenu.Text != "" && rtxticono.Text != "")
                {
                    string menu = rtxtwidget.Text.Trim();
                    string icono = rtxticono.Text.Trim();
                    if (txtid_widget.Text == "")
                    {
                        Agregarwidget(menu, icono);
                    }
                    else
                    {
                        Editarwidget(Convert.ToInt32(txtid_widget.Text), widget, icono);
                    }
                }
                else
                {
                    Toast.Info("favor de llenar los campos", "DATOS REQUERIDOS", this);
                }
            }
            catch (Exception ex)
            {

                Toast.Error("Error al relacionar usuario: " + ex.Message, this);
            }

        }
        protected void lnkcommand2_Click(object sender, EventArgs e)
        {
            //LinkButton lnk = sender as LinkButton;
            //int id_widget = Convert.ToInt32(lnk.CommandArgument);
            //switch (lnk.CommandName.ToLower())
            //{
            //    case "eliminar":
            //        Eliminarwidget(id_widget, hdfmotivos.Value.Trim());
            //        break;
            //}
        }
        protected void btneventgrid_Click(object sender, EventArgs e)
        {
            //int id_widget = Convert.ToInt32(hdfid_widget.Value);
            ////div_empleados.Visible = false;
            //div_widget.Visible = false;
            //switch (hdfcommand.Value.ToLower())
            //{
            //    case "actualizar":
            //        //div_empleados.Visible = true;
            //        div_widget.Visible = true;
            //        Cargar_catalogo_widgets(id_widget);
            //        ModalShow("#myModal");
            //        break;
            //        //case "usuarios":
            //        //    div_empleados.Visible = true;
            //        //    div_perfil.Visible = false;
            //        //    CargarCatalogo(id_perfil);
            //        //    ModalShow("#myModal");
            //        //    break;
            //}
        }
    }
}