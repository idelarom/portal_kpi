using negocio.Componentes;
using Newtonsoft.Json;
using datos.Model;
using datos.NAVISION;
using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace presentacion.Pages.MP
{
    public partial class Global : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["usuario"] == null && Request.QueryString["u"] == null)
            {
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("../../Pages/Common/login.aspx");
            }
        }

        private Boolean Login(string usuario, string password)
        {
            try
            {
                bool retur = true;
                if (usuario == "")
                {
                    Toast.Info("Ingrese Usuario", "Mensaje del Sistema", this.Page);
                    retur = false;
                }
                else if (password == "")
                {
                    Toast.Info("Ingrese Contraseña", "Mensaje del Sistema", this.Page);
                    retur = false;
                }
                else if (!LoginActive(usuario, password, "migesa.net"))
                {
                    retur = false;
                }
                return retur;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertInfo(ex.Message, "Mensaje del Sistema", this.Page);
                return false;
            }
        }

        private Boolean LoginActive(string username, string password, string dominio)
        {
            try
            {
                // create a "principal context" - e.g. your domain (could be machine, too)
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, dominio))
                {
                    Boolean isValid = false;
                    // validate the credentials
                    isValid = pc.ValidateCredentials(username, password);

                    Employee entidad = new Employee();
                    entidad.Usuario_Red = username.Trim();
                    EmpleadosCOM empleados = new EmpleadosCOM();
                    string finger_print = "e";
                    DataTable dt = empleados.GetLogin(username, finger_print);
                    string vmensaje = "";
                    if ((isValid || dt.Rows.Count > 0) && finger_print != "")
                    {
                        isValid = true;
                        DataRow row = dt.Rows[0];
                        String os = "VENTANA EMERGENTE";
                        String os_vers = "VENTANA EMERGENTE";
                        String browser = "VENTANA EMERGENTE";
                        String device = "VENTANA EMERGENTE";
                        String ip = "";
                        String lat = "";
                        String lon = "";
                        String region = "";
                        String proveedor = "";
                        String modelo = "";
                        bool bloqueado = Convert.ToBoolean(row["dispositivo_bloqueado"]);
                        if (bloqueado)
                        {
                            vmensaje = "Este dispostivo(" + device + " " + modelo + " " + os + " " + os_vers + ") fue bloqueado para el inicio de sesión. Si usted no realizo esta configuración, comuniquese al departamento de sistemas.";
                            isValid = false;
                        }
                        else
                        {
                            string adress = "";
                            if (Convert.ToInt32(row["num_empleado"]) > 0)
                            {
                                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/img/users/"));//path local
                                DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://" + dominio, username, password);
                                //Create a searcher on your DirectoryEntry
                                DirectorySearcher adSearch = new DirectorySearcher(directoryEntry);
                                adSearch.SearchScope = SearchScope.Subtree;    //Look into all subtree during the search
                                adSearch.Filter = "(&(ObjectClass=user)(sAMAccountName=" + username + "))";    //Filter information, here i'm looking at a user with given username
                                SearchResult sResult = adSearch.FindOne();       //username is unique, so I want to find only one
                                string name = dirInfo.ToString() + username + ".png";
                                string imagen = "";
                                if (!File.Exists(name))
                                {
                                    if (sResult.Properties["thumbnailPhoto"].Count > 0)
                                    {
                                        byte[] array_img = sResult.Properties["thumbnailPhoto"][0] as byte[];    //Get the property info
                                        imagen = GuardarImagenUsuario(array_img, username + ".png");
                                    }
                                }

                                adress = sResult.Properties["mail"][0].ToString();
                            }
                            string nombre = Convert.ToInt32(row["num_empleado"]) > 0 ? (funciones.SplitLastIndex(row["First_Name"].ToString().Trim(), ' ') + " " +
                                        funciones.SplitLastIndex(row["Last_Name"].ToString().Trim(), ' ')) :
                            row["First_Name"].ToString().Trim() + " " + row["Last_Name"].ToString().Trim();
                            string puesto = (row["puesto"].ToString().Trim());
                            string perfil = row["perfil"].ToString().Trim().ToLower();
                            //pasamos aminusculas
                            nombre = nombre.ToLower();
                            puesto = puesto.ToLower();
                            nombre = nombre.Replace("  ", " ");
                            //pasamos a estilos title
                            Session["mail"] = adress;
                            Session["imagen"] = username + ".png";
                            Session["usuario"] = username;
                            Session["password"] = password;
                            Session["contraseña"] = password;
                            string nombre_pro = row["nombre_provicional"].ToString();
                            Session["nombre"] = nombre_pro != "" ? nombre_pro : CultureInfo.InvariantCulture.TextInfo.ToTitleCase(nombre);
                            Session["correo"] = row["Company_E_Mail"].ToString().Trim().ToLower();
                            Session["puesto"] = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(puesto);
                            Session["perfil"] = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(perfil);
                            Session["id_perfil"] = Convert.ToInt32(row["id_perfil"]);
                            Session["NumJefe"] = Convert.ToInt32(row["NumJefe"]);
                            Session["num_empleado"] = Convert.ToInt32(row["num_empleado"]);
                            Session["mostrar_recordatorios"] = Convert.ToBoolean(row["mostrar_recordatorios"]);
                            Session["alerta_inicio_sesion"] = Convert.ToBoolean(row["alerta_inicio_sesion"]);
                            bool ver_Todos = Convert.ToBoolean(row["ver_todos_empleados"]);
                            Session["ver_Todos_los_empleados"] = ver_Todos;
                            DateTime fecha_inicio_sesion = DateTime.Now;
                            Session["os"] = os;
                            Session["os_vers"] = os_vers;
                            Session["browser"] = browser;
                            Session["device"] = device;
                            Session["ip"] = ip;
                            Session["fecha_inicio_sesion"] = fecha_inicio_sesion;
                            usuarios_sesiones e = new usuarios_sesiones();
                            UsuariosSesionesCOM sesion = new UsuariosSesionesCOM();
                            e.usuario = username.Trim().ToUpper();
                            e.os = os;
                            e.os_version = os_vers;
                            e.navegador = browser;
                            e.fecha_inicio_sesion = DateTime.Now;
                            e.ip = ip;
                            e.device = device;
                            e.latitud = lat;
                            e.longitud = lon;
                            e.region = region;
                            e.proveedor = proveedor;
                            e.model = modelo;
                            e.activo = true;
                            e.device_fingerprint = finger_print;
                            int id_usuario_sesion = sesion.Exist(e.usuario, e.device_fingerprint) ? sesion.Editar(e) : sesion.Agregar(e);
                            UsuariosCOM usuarios_ = new UsuariosCOM();
                            usuarios usuario = new usuarios
                            {
                                usuario = username.ToUpper().Trim(),
                                temporal = false,
                                fecha_vencimiento = null,
                                contraseña = funciones.deTextoa64(password),
                                puesto = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(puesto),
                                nombres = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(row["First_Name"].ToString()),
                                a_paterno = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(row["Last_Name"].ToString()),
                                correo = row["Company_E_Mail"].ToString().Trim().ToLower(),
                                usuario_alta = username.ToUpper().Trim(),
                                No_ = Convert.ToInt32(row["num_empleado"]).ToString(),
                                path_imagen = username + ".png"

                            };
                            if (!usuarios_.Exist(username))
                            {
                                usuarios_.Agregar(usuario);
                            }
                            else
                            {
                                usuarios_.Editar(usuario);
                            }
                            if (id_usuario_sesion > 0)
                            {
                                Session["devices_conectados"] = UpdateDevices(username);
                                Session["id_usuario_sesion"] = id_usuario_sesion;
                            }
                            else
                            {
                                vmensaje = "No se pudo registrar el inicio de sesión en el servidor. Intentelo nuevamente.";
                                isValid = false;
                            }
                        }
                    }
                    else
                    {
                        vmensaje = "Credenciales invalidas";
                    }

                    if (vmensaje != "")
                    {
                        Toast.Error(vmensaje, this.Page);
                        isValid = false;
                    }
                    return isValid;
                }
            }
            catch (Exception ex)
            {
                Toast.Error(ex.Message, this.Page);
                return false;
            }
        }

        protected int UpdateDevices(string usuario)
        {
            try
            {
                EmpleadosCOM empleados = new EmpleadosCOM();
                DataTable dt = empleados.sp_usuario_sesiones(usuario, false).Tables[0];
                return dt.Rows.Count;
            }
            catch (Exception ex)
            {
                Toast.Error("Error al actualizar la lista de dispositivos conectados: " + ex.Message, this.Page);
                return 0;
            }
        }

        /// <summary>
        /// Guarda un byte[] como imagen en una ruta especificada
        /// </summary>
        /// <param name="array"></param>
        /// <param name="name_image"></param>
        /// <returns></returns>
        protected String GuardarImagenUsuario(byte[] array, string name_image)
        {
            System.Drawing.Image imagen = byteArrayToImage(array);
            DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/img/users/"));//path local
            string name = dirInfo.ToString() + name_image;
            imagen.Save(name);
            return name_image;
        }

        /// <summary>
        /// IDELAROM: Convierte un byte en imagen
        /// </summary>
        /// <param name="byteArrayIn"></param>
        /// <returns></returns>
        public System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
            return returnImage;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.MaintainScrollPositionOnPostBack = true;
            string usuario = (Session["usuario"] as string) == null ? "" : (Session["usuario"] as string);
            String puesto = (Session["puesto"] as string) == null ? "" : (Session["puesto"] as string);
            if (Request.QueryString["u"] != null)
            {
                if (!Login(funciones.de64aTexto(Request.QueryString["u"]), funciones.de64aTexto(Request.QueryString["c"])))
                {
                    Response.Redirect("../../Pages/Common/login.aspx");
                }
            }
            usuario = (Session["usuario"] as string) == null ? "" : (Session["usuario"] as string);
            puesto = (Session["puesto"] as string) == null ? "" : (Session["puesto"] as string);
            if (usuario == "" || puesto == "")
            {
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("../../Pages/Common/login.aspx");
            }
            if (!IsPostBack)
            {
                if (!ExistInSession())
                {
                    lnkcerrarsession_Click(null, null);
                }
                CargarImagen();
                CargarMenu();
                string nombre = Session["nombre"] == null ? "" : Session["nombre"] as string;
                lblname.Text = nombre;
                lblname2.Text = nombre;
                lblname3.Text = nombre;
                lblpuesto.Text = puesto;
                lblperfil.Text = Session["perfil"] as string;
                CargarImagen();
                UpdateDevices();
                GetRecordsToday();
                hdf_mp_usuario.Value = Convert.ToString(Session["usuario"]).ToUpper();
                ScriptManager.RegisterStartupScript(this.Page,GetType(),Guid.NewGuid().ToString(),
                    "CargarNotificaciones('" + Convert.ToString(Session["usuario"]).ToUpper() + "');", true);
                if (Convert.ToInt32(Session["id_perfil"]) == 0)
                {
                    Toast.Info("El usuario en uso: " + Convert.ToString(Session["usuario"]).ToUpper() + ", no cuenta con un perfil asignado. Por favor, comuniquese con su administrador.", "Mensaje del sistema", this.Page);
                }
            }
        }

        private void CargarImagen()
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/img/users/"));
                string imagen = Session["imagen"] as string;
                if (imagen != "" && File.Exists(dirInfo.ToString().Trim() + imagen))
                {
                    DateTime localDate = DateTime.Now;
                    string date = localDate.ToString();
                    date = date.Replace("/", "_");
                    date = date.Replace(":", "_");
                    date = date.Replace(" ", "");
                    imguser.ImageUrl = "~/img/users/" + imagen + "?date=" + date;
                    imguser2.ImageUrl = "~/img/users/" + imagen + "?date=" + date;
                    imguser3.ImageUrl = "~/img/users/" + imagen + "?date=" + date;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }

        private DataTable TableMenu(int id_menu_padre)
        {
            try
            {
                EmpleadosCOM componente = new EmpleadosCOM();
                DataSet ds = componente.sp_menu(id_menu_padre, Convert.ToString(Session["usuario"]));
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar el menu: " + ex.Message, this.Page);
                return null;
            }
        }

        protected void repeat_menu_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            Repeater repeater_menu_multi = (Repeater)e.Item.FindControl("repeater_menu_multi");
            HtmlGenericControl ml = (HtmlGenericControl)e.Item.FindControl("ml");
            HtmlGenericControl mml = (HtmlGenericControl)e.Item.FindControl("mml");
            int id_menu = Convert.ToInt32(DataBinder.Eval(dbr, "id_menu"));
            int total_menu = TableMenu(id_menu).Rows.Count;
            ml.Visible = total_menu == 0;
            mml.Visible = total_menu > 0;
            if (total_menu > 0)
            {
                try
                {
                    DataTable dt_menu = TableMenu(id_menu);
                    repeater_menu_multi.DataSource = dt_menu;
                    repeater_menu_multi.DataBind();
                }
                catch (Exception ex)
                {
                    Toast.Error("Error al cargar menus. " + ex.Message, this.Page);
                }
            }
        }

        private void CargarMenu()
        {
            try
            {
                DataTable dt_menu = TableMenu(0);
                repeat_menu.DataSource = dt_menu;
                repeat_menu.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar menus. " + ex.Message, this.Page);
            }
        }

        protected void lnkcerrarsession_Click(object sender, EventArgs e)
        {
            string url = "../../Pages/Common/login.aspx";
            EmpleadosCOM empleados = new EmpleadosCOM();
            DataSet ds = empleados.sp_eliminar_usuario_sesiones(Convert.ToInt32(Session["id_usuario_sesion"]), false);
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect(url);
        }

        protected bool ExistInSession()
        {
            try
            {
                EmpleadosCOM empleados = new EmpleadosCOM();
                DataTable dt = empleados.sp_existe_usuario_sesiones(
                Session["usuario"] as string, Session["os"] as string, Session["os_vers"] as string,
                Session["browser"] as string, Session["device"] as string).Tables[0];
                return Convert.ToBoolean(dt.Rows[0]["existe"]);
            }
            catch (Exception ex)
            {
                Toast.Error("Error al verificar sesion: " + ex.Message, this.Page);
                return true;
            }
        }

        public String DevicesConecteds()
        {
            try
            {
                string value = "";
                EmpleadosCOM empleados = new EmpleadosCOM();
                DataTable dt = empleados.sp_usuario_sesiones(Session["usuario"] as string, false).Tables[0];
                lbldispo.Text = dt.Rows.Count.ToString();
                repeat_devices.DataSource = dt;
                repeat_devices.DataBind();
                repeat_devices2.DataSource = dt;
                repeat_devices2.DataBind();
                int devices_count = Convert.ToInt32(Session["devices_conectados"]);
                bool mas_dispositivos = dt.Rows.Count > devices_count;
                Session["devices_conectados"] = dt.Rows.Count;
                if (mas_dispositivos)
                {
                    value = "Se detecto un nuevo dispositivo conectado. Da clic sobre este mensaje para ver mas opciones.";
                }
                return value;
            }
            catch (Exception ex)
            {
                return "Error al actualizar la lista de dispositivo(s) conectado(s): " + ex.Message;
            }
        }

        protected void UpdateDevices()
        {
            try
            {
                EmpleadosCOM empleados = new EmpleadosCOM();
                DataTable dt = empleados.sp_usuario_sesiones(Session["usuario"] as string, false).Tables[0];
                lbldispo.Text = dt.Rows.Count.ToString();
                repeat_devices.DataSource = dt;
                repeat_devices.DataBind();
                repeat_devices2.DataSource = dt;
                repeat_devices2.DataBind();
                int devices_count = Convert.ToInt32(Session["devices_conectados"]);
                bool mas_dispositivos = dt.Rows.Count > devices_count;
                Session["devices_conectados"] = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                Toast.Error("Error al actualizar la lista de dispositivo(s) conectado(s): " + ex.Message, this.Page);
            }
        }

        protected void lnkactualizar_Click(object sender, EventArgs e)
        {
            UpdateDevices();
        }

        protected void lnkdesconectar_Click(object sender, EventArgs e)
        {
            try
            {
                int total = 0;
                foreach (RepeaterItem item in repeat_devices.Items)
                {
                    CheckBox cbx = item.FindControl("cbxcheck") as CheckBox;
                    Label dispositivo = item.FindControl("dispositivo") as Label;
                    Label os = item.FindControl("os") as Label;
                    Label os_version = item.FindControl("os_version") as Label;
                    Label browser = item.FindControl("browser") as Label;
                    Label ip = item.FindControl("ip") as Label;
                    Label fecha = item.FindControl("fecha") as Label;
                    Label id_usuario_sesion = item.FindControl("id_usuario_sesion") as Label;
                    if (cbx.Checked)
                    {
                        total++;
                        EmpleadosCOM empleados = new EmpleadosCOM();
                        DataSet ds = empleados.sp_eliminar_usuario_sesiones(Convert.ToInt32(id_usuario_sesion.Text), false);
                    }
                }
                if (total == 0)
                {
                    Toast.Info("Seleccione un dispositivo para desconectar.", "Mensaje del sistema", this.Page);
                }
                else
                {
                    UpdateDevices();
                    Toast.Success(total.ToString() + " dispositivo(s) desconectado(s) correctamente.", "Mensaje del sistema", this.Page);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cerrar sesiones: " + ex.Message, this.Page);
            }
        }

        protected void btncerrarsesion_Click(object sender, EventArgs e)
        {
            try
            {
                string command = hdfcommand.Value;
                int idc_usuario_sesion = Convert.ToInt32(hdfid_usuario_sesion.Value);
                bool bloquear = command.Trim() == "bloquear";
                if (idc_usuario_sesion > 0)
                {
                    EmpleadosCOM empleados = new EmpleadosCOM();
                    DataSet ds = empleados.sp_eliminar_usuario_sesiones(idc_usuario_sesion, bloquear);
                    UpdateDevices();
                    Toast.Success("Dispositivo desconectado correctamente.", "Mensaje del sistema", this.Page);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cerrar sesiones: " + ex.Message, this.Page);
            }
        }

        /// <summary>
        /// Obtiene los recordatorios de hiy, si existe alguno lanza el modal con recordatorios
        /// </summary>
        protected void GetRecordsToday()
        {
            try
            {

                if (Convert.ToBoolean(Session["mostrar_recordatorios"]))
                {
                    DataTable dt = GetRecordsToday(Session["usuario"] as string);
                    if (dt.Rows.Count > 0)
                    {
                        ViewState["dt_records_today"] = dt;
                        lnkcommand.OnClientClick = "return confirm('¿Desea descartar los " + dt.Rows.Count.ToString() + " recordatorios?');";
                        repeater_tab_recs.DataSource = dt;
                        repeater_tab_recs.DataBind();
                        repeater_recs.DataSource = dt;
                        repeater_recs.DataBind();
                        ModalShow("#modal_recordatorios_mp");
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al comprobar pendientes del calendario: "+ex.Message,this.Page);
            }
        }

        private DataTable GetRecordsToday(string user)
        {
            RecordatoriosCOM recordatorios = new RecordatoriosCOM();
            DataTable dt = recordatorios.SelectToday(user);
            return dt;
        }
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

        protected void lnkcommand_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = sender as LinkButton;
                string command = lnk.CommandName.ToLower();
                int id_recordatorio = Convert.ToInt32(lnk.CommandArgument);
                RecordatoriosCOM recordatorios = new RecordatoriosCOM();
                string usuario = Session["usuario"] as string;
                string mensaje = "";
                switch (command)
                {
                    case "todo":
                        DataTable dt_all_records =ViewState["dt_records_today"] as DataTable;
                        foreach (DataRow row in dt_all_records.Rows)
                        {
                            id_recordatorio  = Convert.ToInt32(row["id_recordatorio"]);
                            recordatorios.Descartar(id_recordatorio, usuario);
                        }
                        break;
                    case "descartar":
                        recordatorios.Descartar(id_recordatorio, usuario);
                        break;
                    case "posponer":
                        int minutos = 15;
                        recordatorios.Posponer(id_recordatorio, minutos);
                        break;
                }
                string url = Request.Url.AbsoluteUri;
                if (mensaje == "")
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                                     "AlertGO('Solicitud realizada correctamente','"+ url + "');", true);
                }
                else
                {
                    Toast.Error("Error al procesar recordatorio con id("+id_recordatorio+"),: " + mensaje, this.Page);
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al procesar recodatorio: " + ex.Message, this.Page);
            }
        }

        protected void lnksincronizar_Click(object sender, EventArgs e)
        {
            try
            {
                string username = Session["usuario"] as string;
                string password = Session["contraseña"] as string;
                string mail = Session["mail"] as string;
                string mail_user = username + mail.Replace(mail.Split('@')[0], "");
                EWSHelper calendar = new EWSHelper();
                calendar.GetAllCalendar(mail_user, password);
                Toast.Success("Sincronización realizada correctamente.", "Mensaje del sistema", this.Page);
            }
            catch (Exception ex)
            {
                Toast.Error("Error al sincornizar con el servidor: " + ex.Message, this.Page);
            }
            finally {

            }
        }

    }
}