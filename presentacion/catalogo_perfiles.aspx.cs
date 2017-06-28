using datos.NAVISION;
using negocio.Componentes;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class catalogo_perfiles : System.Web.UI.Page
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

        private void CargarListadoEmpleados(string filtro)
        {
            try
            {
                Employee entidad = new Employee();
                EmpleadosCOM empleados = new EmpleadosCOM();
                DataTable dt_original = empleados.GetUsers(entidad);

                DataTable dt = new DataTable();
                if (filtro == "")
                {
                    dt = dt_original;
                }
                else
                {
                    if (dt_original.Select("nombre_usuario like '%" + filtro + "%'").Length > 0)
                    {
                        dt = filtro == "" ? dt_original : dt_original.Select("nombre_usuario like '%" + filtro + "%'").CopyToDataTable();
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    rdllista_empleados.DataSource = dt;
                    rdllista_empleados.DataBind();
                    CheckValuesListUsuarios();
                }
                else
                {
                    Toast.Info("No se encontro ninguna coincidencia. Intentelo nuevamente.", "Mensaje del Sistema", this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar lista de empleados. " + ex.Message, this);
            }
        }

        private void CargarListadoWidgets(string filtro)
        {
            try
            {
                WidgetsCOM widgets = new WidgetsCOM();
                DataTable dt_original = widgets.sp_catalogo_widgets(0).Tables[0];

                DataTable dt = new DataTable();
                if (filtro == "")
                {
                    dt = dt_original;
                }
                else
                {
                    if (dt_original.Select("widget like '%" + filtro + "%'").Length > 0)
                    {
                        dt = filtro == "" ? dt_original : dt_original.Select("widget like '%" + filtro + "%'").CopyToDataTable();
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    repeater_widgets.DataSource = dt;
                    repeater_widgets.DataBind();
                    CheckValuesListWidgets();
                }
                else
                {
                    Toast.Info("No se encontro ninguna coincidencia. Intentelo nuevamente.", "Mensaje del Sistema", this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar lista de widgets. " + ex.Message, this);
            }
        }

        private void CargarListadoMenus(string filtro)
        {
            try
            {
                MenusCOM widgets = new MenusCOM();
                DataTable dt_original = widgets.sp_catalogo_menus(0).Tables[0];

                DataTable dt = new DataTable();
                if (filtro == "")
                {
                    dt = dt_original;
                }
                else
                {
                    if (dt_original.Select("name like '%" + filtro + "%'").Length > 0)
                    {
                        dt = filtro == "" ? dt_original : dt_original.Select("name like '%" + filtro + "%'").CopyToDataTable();
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    repeater_menus.DataSource = dt;
                    repeater_menus.DataBind();
                    CheckValuesListMenus();
                }
                else
                {
                    Toast.Info("No se encontro ninguna coincidencia. Intentelo nuevamente.", "Mensaje del Sistema", this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar lista de menus. " + ex.Message, this);
            }
        }

        private String UsuarioTienePerfil(string usuario, int id_perfil)
        {
            try
            {
                Employee entidad = new Employee();
                EmpleadosCOM empleados = new EmpleadosCOM();
                DataTable dt_original = empleados.GetUsers(entidad);

                DataTable dt = new DataTable();
                if (dt_original.Select("usuario_red = '" + usuario.Trim().ToUpper() + "'").Length > 0)
                {
                    dt = dt_original.Select("usuario_red = '" + usuario.Trim().ToUpper() + "'").CopyToDataTable();
                }
                int vid_perfil = Convert.ToInt32(dt.Rows[0]["id_perfil"]);
                string perfil = dt.Rows[0]["perfil"].ToString().Trim();
                perfil = id_perfil == vid_perfil ? "" : perfil;
                return perfil;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private void AgregarUsuarioPerfiles(string usuario)
        {
            try
            {
                if (ViewState["dt_usuarios"] == null)
                {
                    DataTable dt_new = new DataTable();
                    dt_new.Columns.Add("usuario");
                    ViewState["dt_usuarios"] = dt_new;
                }
                DataTable dt = ViewState["dt_usuarios"] as DataTable;
                DataRow row = dt.NewRow();
                row["usuario"] = usuario;
                dt.Rows.Add(row);
                ViewState["dt_usuarios"] = dt;
            }
            catch (Exception ex)
            {
                Toast.Error("Error al relacionar usuario: " + ex.Message, this);
            }
        }

        private void EliminarUsuarioPerfiles(string usuario)
        {
            try
            {
                if (ViewState["dt_usuarios"] == null)
                {
                    DataTable dt_new = new DataTable();
                    dt_new.Columns.Add("usuario");
                    ViewState["dt_usuarios"] = dt_new;
                }
                DataTable dt = ViewState["dt_usuarios"] as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    if (usuario.Trim().ToUpper() == row["usuario"].ToString().Trim().ToUpper())
                    {
                        row.Delete();
                        break;
                    }
                }
                ViewState["dt_usuarios"] = dt;
            }
            catch (Exception ex)
            {
                Toast.Error("Error al eliminar usuario: " + ex.Message, this);
            }
        }

        private void AgregarWidgetsoPerfiles(int id_widget)
        {
            try
            {
                if (ViewState["dt_widgets"] == null)
                {
                    DataTable dt_new = new DataTable();
                    dt_new.Columns.Add("id_widget");
                    ViewState["dt_widgets"] = dt_new;
                }
                DataTable dt = ViewState["dt_widgets"] as DataTable;
                DataRow row = dt.NewRow();
                row["id_widget"] = id_widget;
                dt.Rows.Add(row);
                ViewState["dt_widgets"] = dt;
            }
            catch (Exception ex)
            {
                Toast.Error("Error al relacionar widget: " + ex.Message, this);
            }
        }

        private void EliminarWidgetsPerfiles(int id_widget)
        {
            try
            {
                if (ViewState["dt_widgets"] == null)
                {
                    DataTable dt_new = new DataTable();
                    dt_new.Columns.Add("id_widget");
                    ViewState["dt_widgets"] = dt_new;
                }
                DataTable dt = ViewState["dt_widgets"] as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    if (id_widget == Convert.ToInt32(row["id_widget"]))
                    {
                        row.Delete();
                        break;
                    }
                }
                ViewState["dt_widgets"] = dt;
            }
            catch (Exception ex)
            {
                Toast.Error("Error al eliminar widget: " + ex.Message, this);
            }
        }

        private void AgregarMenusoPerfiles(int id_menu)
        {
            try
            {
                if (ViewState["dt_menus"] == null)
                {
                    DataTable dt_new = new DataTable();
                    dt_new.Columns.Add("id_menu");
                    ViewState["dt_menus"] = dt_new;
                }
                DataTable dt = ViewState["dt_menus"] as DataTable;
                DataRow row = dt.NewRow();
                row["id_menu"] = id_menu;
                dt.Rows.Add(row);
                ViewState["dt_menus"] = dt;
            }
            catch (Exception ex)
            {
                Toast.Error("Error al relacionar menu: " + ex.Message, this);
            }
        }

        private void EliminarMenusPerfiles(int id_menu)
        {
            try
            {
                if (ViewState["dt_menus"] == null)
                {
                    DataTable dt_new = new DataTable();
                    dt_new.Columns.Add("id_menu");
                    ViewState["dt_menus"] = dt_new;
                }
                DataTable dt = ViewState["dt_menus"] as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    if (id_menu == Convert.ToInt32(row["id_menu"]))
                    {
                        row.Delete();
                        break;
                    }
                }
                ViewState["dt_menus"] = dt;
            }
            catch (Exception ex)
            {
                Toast.Error("Error al eliminar menu: " + ex.Message, this);
            }
        }

        private Boolean ExistUsuarioPerfiles(string usuario)
        {
            try
            {
                bool exist = false;
                DataTable dt = ViewState["dt_usuarios"] as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    if (usuario.Trim().ToUpper() == row["usuario"].ToString().Trim().ToUpper())
                    {
                        exist = true;
                        break;
                    }
                }
                return exist;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private Boolean ExistWidgetsPerfiles(string id_widget)
        {
            try
            {
                bool exist = false;
                DataTable dt = ViewState["dt_widgets"] as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    if (id_widget.Trim().ToUpper() == row["id_widget"].ToString().Trim().ToUpper())
                    {
                        exist = true;
                        break;
                    }
                }
                return exist;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private Boolean ExistMenusPerfiles(string id_menu)
        {
            try
            {
                bool exist = false;
                DataTable dt = ViewState["dt_menus"] as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    if (id_menu.Trim().ToUpper() == row["id_menu"].ToString().Trim().ToUpper())
                    {
                        exist = true;
                        break;
                    }
                }
                return exist;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void CheckValuesListUsuarios()
        {
            try
            {
                foreach (RepeaterItem item in rdllista_empleados.Items)
                {
                    CheckBox check = item.FindControl("mycheck") as CheckBox;
                    check.Checked = ExistUsuarioPerfiles(check.ToolTip.Trim().ToUpper());
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar lista de usuario: " + ex.Message, this);
            }
        }

        private void CheckValuesListWidgets()
        {
            try
            {
                foreach (RepeaterItem item in repeater_widgets.Items)
                {
                    CheckBox check = item.FindControl("mycheck_widgets") as CheckBox;
                    check.Checked = ExistWidgetsPerfiles(check.ToolTip.Trim().ToUpper());
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar lista de widgets: " + ex.Message, this);
            }
        }

        private void CheckValuesListMenus()
        {
            try
            {
                foreach (RepeaterItem item in repeater_menus.Items)
                {
                    CheckBox check = item.FindControl("mycheck_menus") as CheckBox;
                    check.Checked = ExistMenusPerfiles(check.ToolTip.Trim().ToUpper());
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar lista de menus: " + ex.Message, this);
            }
        }

        private String CadenaUsuarios()
        {
            try
            {
                string cadena = "";
                DataTable dt = ViewState["dt_usuarios"] as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    cadena = cadena + row["usuario"].ToString().Trim().ToUpper() + ";";
                }
                return cadena;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private int TotalCadenaUsuarios()
        {
            try
            {
                DataTable dt = ViewState["dt_usuarios"] as DataTable;
                return dt.Rows.Count;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private String CadenaWidgets()
        {
            try
            {
                string cadena = "";
                DataTable dt = ViewState["dt_widgets"] as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    cadena = cadena + row["id_widget"].ToString().Trim().ToUpper() + ";";
                }
                return cadena;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private int TotalCadenaWidgets()
        {
            try
            {
                DataTable dt = ViewState["dt_widgets"] as DataTable;
                return dt.Rows.Count;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private String CadenaMenus()
        {
            try
            {
                string cadena = "";
                DataTable dt = ViewState["dt_menus"] as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    cadena = cadena + row["id_menu"].ToString().Trim().ToUpper() + ";";
                }
                return cadena;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private int TotalCadenaMenus()
        {
            try
            {
                DataTable dt = ViewState["dt_menus"] as DataTable;
                return dt.Rows.Count;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private void CargarCatalogo(int id_perfil)
        {
            try
            {
                PerfilesCOM perfiles = new PerfilesCOM();
                DataSet ds = perfiles.sp_catalogo_perfiles(id_perfil);
                DataTable dt = ds.Tables[0];
                grid_perfiles.DataSource = dt;
                grid_perfiles.DataBind();
                if (id_perfil > 0)
                {
                    rtxtperfil.Text = dt.Rows[0]["perfil"].ToString();
                    txtid_perfil.Text = id_perfil.ToString();
                    DataTable dt_usuarios_original = perfiles.sp_usuarios_perfiles(id_perfil).Tables[0];
                    if (dt_usuarios_original.Rows.Count > 0)
                    {
                        System.Data.DataView view = new System.Data.DataView(dt_usuarios_original);
                        System.Data.DataTable selected = view.ToTable("Selected", false, "usuario");
                        ViewState["dt_usuarios"] = selected;
                        rdllista_empleados.DataSource = dt_usuarios_original;
                        rdllista_empleados.DataBind();
                        CheckValuesListUsuarios();
                    }
                    DataTable dt_widgets_original = perfiles.sp_widgets_perfiles(id_perfil).Tables[0];
                    if (dt_widgets_original.Rows.Count > 0)
                    {
                        System.Data.DataView view = new System.Data.DataView(dt_widgets_original);
                        System.Data.DataTable selected = view.ToTable("Selected", false, "id_widget");
                        ViewState["dt_widgets"] = selected;
                        repeater_widgets.DataSource = dt_widgets_original;
                        repeater_widgets.DataBind();
                        CheckValuesListWidgets();
                    }

                    DataTable dt_menus_original = perfiles.sp_menus_perfiles(id_perfil).Tables[0];
                    if (dt_menus_original.Rows.Count > 0)
                    {
                        System.Data.DataView view = new System.Data.DataView(dt_menus_original);
                        System.Data.DataTable selected = view.ToTable("Selected", false, "id_menu");
                        ViewState["dt_menus"] = selected;
                        repeater_menus.DataSource = dt_menus_original;
                        repeater_menus.DataBind();
                        CheckValuesListMenus();
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar catalogo principal: " + ex.Message, this);
            }
        }

        private void AgregarPerfil(string perfil, string cadena_usuarios,
            int total_cadena_usuarios, string cadena_widgets,
            int total_cadena_widgets, string cadena_menus, int total_cadena_menus)
        {
            div_error.Visible = false;
            try
            {
                PerfilesCOM perfiles = new PerfilesCOM();
                string usuario = Session["usuario"] as string;
                DataSet ds = perfiles.sp_agregar_perfiles(perfil, usuario, cadena_usuarios, total_cadena_usuarios,
                    cadena_widgets, total_cadena_widgets, cadena_menus, total_cadena_menus);
                DataTable dt = ds.Tables[0];
                string vmensaje = (dt.Rows.Count == 0 || !dt.Columns.Contains("mensaje")) ? "Error al guardar perfil. Intentelo Nuevamente." : dt.Rows[0]["mensaje"].ToString().Trim();
                if (vmensaje == "")
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                  "AlertGO('Perfil Guardado Correctamente', 'catalogo_perfiles.aspx');", true);
                }
                else
                {
                    div_error.Visible = true;
                    lblerror.Text = vmensaje;
                }
            }
            catch (Exception ex)
            {
                lblerror.Text = "Error al guardar perfil: " + ex.Message;
            }
        }

        private void EditarPerfil(int idperfil, string perfil, string cadena_usuarios,
            int total_cadena_usuarios, string cadena_widgets, int total_cadena_widgets, string cadena_menus, int total_cadena_menus)
        {
            div_error.Visible = false;
            try
            {
                PerfilesCOM perfiles = new PerfilesCOM();
                string usuario = Session["usuario"] as string;
                DataSet ds = perfiles.sp_editar_perfiles(idperfil, perfil, usuario, cadena_usuarios, total_cadena_usuarios,
                    cadena_widgets, total_cadena_widgets, cadena_menus, total_cadena_menus);
                DataTable dt = ds.Tables[0];
                string vmensaje = (dt.Rows.Count == 0 || !dt.Columns.Contains("mensaje")) ? "Error al editar perfil. Intentelo Nuevamente." : dt.Rows[0]["mensaje"].ToString().Trim();
                if (vmensaje == "")
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                  "AlertGO('Perfil Guardado Correctamente', 'catalogo_perfiles.aspx');", true);
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
                lblerror.Text = "Error al editar perfil: " + ex.Message;
            }
        }

        private void EliminarPerfil(int id_perfil, string comenatrios)
        {
            try
            {
                PerfilesCOM perfiles = new PerfilesCOM();
                string usuario = Session["usuario"] as string;
                DataSet ds = perfiles.sp_borrar_perfiles(id_perfil, usuario, comenatrios);
                DataTable dt = ds.Tables[0];
                string vmensaje = (dt.Rows.Count == 0 || !dt.Columns.Contains("mensaje")) ? "Error al eliminar perfil. Intentelo Nuevamente." : dt.Rows[0]["mensaje"].ToString().Trim();
                if (vmensaje == "")
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                  "AlertGO('Perfil Guardado Correctamente', 'catalogo_perfiles.aspx');", true);
                }
                else
                {
                    Toast.Error("Error al eliminar perfil: " + vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al eliminar perfil: " + ex.Message, this);
            }
        }

        private void Tabs(string caso)
        {
            tusu.Attributes["class"] = "";
            twid.Attributes["class"] = "";
            tmen.Attributes["class"] = "";
            div_empleados.Attributes["class"] = "tab-pane";
            div_widgets.Attributes["class"] = "tab-pane";
            div_menus.Attributes["class"] = "tab-pane";
            switch (caso)
            {
                case "usuarios":
                default:
                    tusu.Attributes["class"] = "active";
                    div_empleados.Attributes["class"] = "tab-pane active";
                    break;

                case "menus":
                    tmen.Attributes["class"] = "active";
                    div_menus.Attributes["class"] = "tab-pane active";
                    break;

                case "widgets":
                    twid.Attributes["class"] = "active";
                    div_widgets.Attributes["class"] = "tab-pane active";
                    break;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["dt_usuarios"] = null;
                ViewState["dt_widgets"] = null;
                ViewState["dt_menus"] = null;
                CargarCatalogo(0);
            }
        }

        protected void lnknuevoperfil_Click(object sender, EventArgs e)
        {
            div_empleados.Visible = true;
            div_perfil.Visible = true;
            txtid_perfil.Text = "";
            rtxtperfil.Text = "";
            txtbuscarempleado.Text = "";
            rdllista_empleados.DataSource = null;
            rdllista_empleados.DataBind();
            Tabs("usuarios");
            ModalShow("#myModal");
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            string cadena_perfiles = CadenaUsuarios();
            int total_cadena_perfiles = TotalCadenaUsuarios();
            string cadena_widgets = CadenaWidgets();
            int total_cadena_widgets = TotalCadenaWidgets();
            string perfil = rtxtperfil.Text.Trim();
            string cadena_menus = CadenaMenus();
            int total_cadena_menus = TotalCadenaMenus();
            div_error.Visible = false;
            if (rtxtperfil.Text == "")
            {
                div_error.Visible = true;
                lblerror.Text = "Ingrese el nombre del perfil";
            }
            else
            {
                if (txtid_perfil.Text == "")
                {
                    AgregarPerfil(perfil, cadena_perfiles, total_cadena_perfiles,
                        cadena_widgets, total_cadena_widgets, cadena_menus, total_cadena_menus);
                }
                else
                {
                    EditarPerfil(Convert.ToInt32(txtid_perfil.Text), perfil,
                        cadena_perfiles, total_cadena_perfiles, cadena_widgets, total_cadena_widgets, cadena_menus, total_cadena_menus);
                }
            }
        }

        protected void btnbuscarempleado2_Click(object sender, EventArgs e)
        {
            if (txtbuscarempleado.Text.Trim().Length > 2 || txtbuscarempleado.Text.Trim().Length == 0)
            {
                CargarListadoEmpleados(txtbuscarempleado.Text.Trim());
                imgloadempleado_.Style["display"] = "none";
                lblbe2.Style["display"] = "none";
            }
            else
            {
                Toast.Info("Ingrese un minimo de 3 caracteres para realizar la busqueda.", "Mensaje del Sistema", this);
            }
        }

        protected void mycheck_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox item = sender as CheckBox;
            string usuario = item.ToolTip.ToString().Trim().ToUpper();
            string perfil = UsuarioTienePerfil(usuario, Convert.ToInt32(txtid_perfil.Text == "" ? "0" : txtid_perfil.Text));
            EliminarUsuarioPerfiles(usuario);
            if (item.Checked)
            {
                if (perfil == "")
                {
                    AgregarUsuarioPerfiles(usuario);
                }
                else
                {
                    item.Checked = false;
                    Toast.Error("El usuario " + usuario.ToUpper() + " ya esta relacionado al perfil: " + perfil.ToUpper(), this);
                }
            }
        }

        protected void lnkcommand2_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            int id_perfil = Convert.ToInt32(lnk.CommandArgument);
            switch (lnk.CommandName.ToLower())
            {
                case "eliminar":
                    EliminarPerfil(id_perfil, hdfmotivos.Value.Trim());
                    break;
            }
        }

        protected void btneventgrid_Click(object sender, EventArgs e)
        {
            int id_perfil = Convert.ToInt32(hdfid_perfil.Value);
            switch (hdfcommand.Value.ToLower())
            {
                case "actualizar":
                    CargarCatalogo(id_perfil);
                    Tabs("usuarios");
                    ModalShow("#myModal");
                    break;

                case "usuarios":
                    CargarCatalogo(id_perfil);
                    Tabs("usuarios");
                    ModalShow("#myModal");
                    break;

                case "widgets":
                    CargarCatalogo(id_perfil);
                    Tabs("widgets");
                    ModalShow("#myModal");
                    break;

                case "menus":
                    CargarCatalogo(id_perfil);
                    Tabs("menus");
                    ModalShow("#myModal");
                    break;
            }
        }

        protected void lnkbuscarwidget_Click(object sender, EventArgs e)
        {
            if (txtbuscarwidget.Text.Trim().Length > 2 || txtbuscarwidget.Text.Trim().Length == 0)
            {
                CargarListadoWidgets(txtbuscarwidget.Text.Trim());
                img_widget.Style["display"] = "none";
                lblwidget.Style["display"] = "none";
            }
            else
            {
                Toast.Info("Ingrese un minimo de 3 caracteres para realizar la busqueda.", "Mensaje del Sistema", this);
            }
        }

        protected void mycheck_widgets_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox item = sender as CheckBox;
            int id_widget = Convert.ToInt32(item.ToolTip.Trim());
            EliminarWidgetsPerfiles(id_widget);
            if (item.Checked)
            {
                AgregarWidgetsoPerfiles(id_widget);
            }
        }

        protected void lnkusuarios_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            Tabs(lnk.CommandName.ToLower());
        }

        protected void lnkbuscarmenu_Click(object sender, EventArgs e)
        {
            if (txtbuscarmenu.Text.Trim().Length > 2 || txtbuscarmenu.Text.Trim().Length == 0)
            {
                CargarListadoMenus(txtbuscarwidget.Text.Trim());
                imgmenu.Style["display"] = "none";
                lblmenu.Style["display"] = "none";
            }
            else
            {
                Toast.Info("Ingrese un minimo de 3 caracteres para realizar la busqueda.", "Mensaje del Sistema", this);
            }
        }

        protected void mycheck_menus_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox item = sender as CheckBox;
            int id_menu = Convert.ToInt32(item.ToolTip.Trim());
            EliminarMenusPerfiles(id_menu);
            if (item.Checked)
            {
                AgregarMenusoPerfiles(id_menu);
            }
        }
    }
}