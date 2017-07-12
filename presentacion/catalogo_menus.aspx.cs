using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio.Componentes;
using System.Data;
using System.IO;

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
                CargarPaginas();
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

                DataSet ds1 = menu.sp_catalogo_menus(0);
                DataTable dt_pp = ds1.Tables[0];
                DataView dv = dt_pp.DefaultView;
                dv.RowFilter = "menu = ''";
                ddlmenupadre.DataSource = dv.ToTable();
                ddlmenupadre.DataTextField = "name";                            // FieldName of Table in DataBase
                ddlmenupadre.DataValueField= "id_menu";
                ddlmenupadre.DataBind();
                if (id_menu > 0)
                {
                    rtxtmenu.Text = dt.Rows[0]["name"].ToString();
                    rtxticono.Text = dt.Rows[0]["icon_ad"].ToString();
                    cbxmantenimiento.Checked = Convert.ToBoolean(dt.Rows[0]["en_mantenimiento"]);
                    if (!String.IsNullOrEmpty(dt.Rows[0]["id_menu_padre"].ToString()))
                    {
                        ddlmenupadre.SelectedValue = dt.Rows[0]["id_menu_padre"].ToString();
                        rtxtUrl.Text = dt.Rows[0]["menu"].ToString();
                        if (ddlpaginas.Items.FindByValue(rtxtUrl.Text) != null)
                        {
                            ddlpaginas.SelectedValue = rtxtUrl.Text;
                        }
                        Chkmenupadre.Checked = false;
                    }
                    else
                    {
                        Chkmenupadre.Checked = true;
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "showContentSubmenu();", true);

                    txtid_menu.Text = id_menu.ToString();
                }
            }
            catch (Exception ex)
            {

                Toast.Error("Error al cargar el catalogo de menu: " + ex.Message, this);
            }
        }

        private void Agregarmenu(string menu, string icono, int id_menu_padre, string url, bool en_mantenimiento)
        {
            div_error.Visible = false;
            try
            {

                MenusCOM menus = new MenusCOM();
                string usuario = Session["usuario"] as string;
                DataSet ds = menus.sp_agregar_menus(id_menu_padre, menu, url, icono, usuario,en_mantenimiento);
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
        private void EditarMenu(int id_menu , string menu, string icono, int id_menu_padre, string url, bool en_mantenimiento)
        {
            div_error.Visible = false;
            try
            {

                MenusCOM menus = new MenusCOM();
                string usuario = Session["usuario"] as string;
                DataSet ds = menus.sp_editar_menus(id_menu, id_menu_padre, menu, url, icono, usuario, en_mantenimiento);
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
        private void Eliminarmenu(int id_menu, string comenatrios)
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
            Chkmenupadre.Checked = false;
            cbxmantenimiento.Checked = false;
            CargarPaginas();
            ModalShow("#myModal");
        }

        protected void CargarPaginas()
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~"));
                String[] Files = Directory.GetFiles(dirInfo.ToString(), "*.aspx", SearchOption.AllDirectories);

                if (Files.Length > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("pagina");
                    foreach (string value in Files)
                    {
                        DataRow row = dt.NewRow();
                        row["pagina"] = Path.GetFileName(value).ToLower();
                        dt.Rows.Add(row);
                    }
                    ddlpaginas.DataTextField = "pagina";
                    ddlpaginas.DataValueField = "pagina";
                    ddlpaginas.DataSource = dt;
                    ddlpaginas.DataBind();
                    ddlpaginas.Items.Insert(0, new ListItem("--Seleccione una pagina del proyecto", "0"));
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar paginas: "+ex.Message,this);
            }
        }
        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            try
            {
                string id_menu = txtid_menu.Text.Trim();
                string menu = rtxtmenu.Text.Trim();               
                string icono = rtxticono.Text.Trim();
                string url = rtxtUrl.Text.Trim();
                bool en_mantenimiento = cbxmantenimiento.Checked;
                if (Chkmenupadre.Checked)
                {
                    if (rtxtmenu.Text != "" && rtxticono.Text != "")
                    {
                        if (id_menu == "")
                        {
                            Agregarmenu(menu, icono, 0, "", en_mantenimiento);
                        }
                        else
                        {
                            EditarMenu(Convert.ToInt32(id_menu), menu, icono, 0, "", en_mantenimiento);
                        }
                    }
                    else
                    {
                        Toast.Info("favor de llenar los campos", "DATOS REQUERIDOS", this);
                    }
                }
                else
                {
                    int id_menu_padre = Convert.ToInt32(ddlmenupadre.SelectedValue);
                    if (rtxtmenu.Text != "" && rtxticono.Text != "")
                    {

                        if (id_menu == "")
                        {
                            Agregarmenu(menu, icono, id_menu_padre, url, en_mantenimiento);
                        }
                        else
                        {
                            EditarMenu(Convert.ToInt32(id_menu), menu, icono, id_menu_padre, url, en_mantenimiento);
                        }
                    }
                    else
                    {
                        Toast.Info("favor de llenar los campos", "DATOS REQUERIDOS", this);
                    }
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
            int id_menu = Convert.ToInt32(lnk.CommandArgument);
            switch (lnk.CommandName.ToLower())
            {
                case "eliminar":
                    Eliminarmenu(id_menu, hdfmotivos.Value.Trim());
                    break;
            }
        }
        protected void btneventgrid_Click(object sender, EventArgs e)
        {
            int id_menu = Convert.ToInt32(hdfid_menu.Value);
            div_menu.Visible = false;
            switch (hdfcommand.Value.ToLower())
            {
                case "actualizar":
                    div_menu.Visible = true;
                    Cargar_catalogo_menus(id_menu);
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

        protected void ddlpaginas_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = ddlpaginas.SelectedValue;
            if (value == "0")
            {
                Toast.Error("Seleccione una opción valida.",this);
            }
            else {
                rtxtUrl.Text = value;
            }
        }
    }
}