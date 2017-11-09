using datos.Model;
using datos.NAVISION;
using negocio.Componentes;
using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Globalization;
using System.IO;

namespace presentacion.Pages.Common
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btniniciar_Click(object sender, EventArgs e)
        {
            if (Login(rtxtusuario.Text, rtxtcontra.Text))
            {
                Response.Redirect("inicio.aspx");
            }
        }

        #region METODOS

        private Boolean Login(string usuario, string password)
        {
            try
            {
                bool retur = true;
                if (usuario == "")
                {
                    Toast.Info("Ingrese Usuario", "Mensaje del Sistema", this);
                    retur = false;
                }
                else if (password == "")
                {
                    Toast.Info("Ingrese Contraseña", "Mensaje del Sistema", this);
                    retur = false;
                }
                else if (!LoginActive(usuario, password, rtxtdominio.Text.Trim()))
                {
                    retur = false;
                }
                return retur;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertInfo(ex.Message, "Mensaje del Sistema", this);
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
                    string finger_print = hdffinger.Value.Trim();
                    DataTable dt = empleados.GetLogin(username, finger_print);
                    string vmensaje = "";
                    if ((isValid || dt.Rows.Count > 0) && finger_print != "")
                    {
                        isValid = true;
                        DataRow row = dt.Rows[0];
                        String os = hdfos.Value.Trim();
                        String os_vers = hdfosversion.Value.Trim();
                        String browser = hdfbrowser.Value.Trim();
                        String device = hdfdevice.Value.Trim();
                        String ip = hdfip.Value.Trim();
                        String lat = hdflatitud.Value.Trim();
                        String lon = hdflongitud.Value.Trim();
                        String region = hdfregion.Value.Trim();
                        String proveedor = hdfproveedor.Value.Trim();
                        String modelo = hdfmodel.Value.Trim();
                        bool bloqueado = Convert.ToBoolean(row["dispositivo_bloqueado"]);
                        if (bloqueado)
                        {
                            vmensaje = "Este dispostivo(" + device + " " + modelo + " " + os + " " + os_vers + ") fue bloqueado para el inicio de sesión. Si usted no realizo esta configuración, comuniquese al departamento de sistemas.";
                            isValid = false;
                        }
                        else
                        {
                            string adress = "";
                            if (Convert.ToInt32(row["num_empleado"])>0) {
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
                            string nombre = Convert.ToInt32(row["num_empleado"]) > 0?(funciones.SplitLastIndex(row["First_Name"].ToString().Trim(), ' ') + " " +
                                        funciones.SplitLastIndex(row["Last_Name"].ToString().Trim(), ' ')):
                            row["First_Name"].ToString().Trim() + " "+ row["Last_Name"].ToString().Trim();
                            string puesto = (row["puesto"].ToString().Trim());
                            string perfil = row["perfil"].ToString().Trim().ToLower();
                            //pasamos aminusculas
                            nombre = nombre.ToLower();
                            puesto = puesto.ToLower();
                            nombre = nombre.Replace("  "," ");
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
                            e.os =os;
                            e.os_version = os_vers;
                            e.navegador = browser;
                            e.fecha_inicio_sesion = DateTime.Now;
                            e.ip = ip;
                            e.device = device;
                            e.latitud = lat;
                            e.longitud =lon;
                            e.region =region;
                            e.proveedor = proveedor;
                            e.model =modelo;
                            e.activo = true;
                            e.device_fingerprint = finger_print;
                            int id_usuario_sesion = sesion.Exist(e.usuario, e.device_fingerprint) ? sesion.Editar(e):sesion.Agregar(e);
                            UsuariosCOM usuarios_ = new UsuariosCOM();
                            usuarios usuario = new usuarios {
                                usuario = username.ToUpper().Trim(),
                                contraseña = funciones.deTextoa64(password),
                                puesto = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(puesto),
                                nombres =CultureInfo.InvariantCulture.TextInfo.ToTitleCase(row["First_Name"].ToString()),
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
                            else {
                                usuarios_.Editar(usuario);
                            }
                            if (id_usuario_sesion > 0)
                            {
                                //if (Convert.ToBoolean(row["sincronizacion_automatica"]))
                                //{
                                //    string mail = Session["mail"] as string;
                                //    string mail_user = username + mail.Replace(mail.Split('@')[0], "");
                                //    EWSHelper calendar = new EWSHelper();
                                //    calendar.GetAllCalendar(mail_user, password);
                                //}
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
                        Toast.Error(vmensaje, this);
                        isValid = false;
                    }
                    return isValid;
                }
            }
            catch (Exception ex)
            {
                Toast.Error(ex.Message, this);
                return false;
            }
        }

        #endregion METODOS

        protected void lnkcambiardominio_Click(object sender, EventArgs e)
        {
            div_dominio.Visible = div_dominio.Visible ? false : true;
        }

        /// <summary>
        /// Guarda un byte[] como imagen en una ruta especificada
        /// </summary>
        /// <param name="array"></param>
        /// <param name="name_image"></param>
        /// <returns></returns>
        protected String GuardarImagenUsuario(byte[] array, string name_image)
        {
            Image imagen = byteArrayToImage(array);
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
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        protected int UpdateDevices(string usuario)
        {
            try
            {
                EmpleadosCOM empleados = new EmpleadosCOM();
                DataTable dt = empleados.sp_usuario_sesiones(usuario,false).Tables[0];
                return dt.Rows.Count;
            }
            catch (Exception ex)
            {
                Toast.Error("Error al actualizar la lista de dispositivos conectados: " + ex.Message, this.Page);
                return 0;
            }
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


        protected void lnkrecuperarcontraseña_Click(object sender, EventArgs e)
        {
            txtcorreo.Text = "";
            ModalShow("#modal_recuperar_contraseña");
            txtcorreo.Focus();
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtcorreo.Text == "")
                {
                    Toast.Error("Ingrese el correo electrónico relacionado a su cuenta.", this.Page);
                }
                else if (!funciones.emailIsValid(txtcorreo.Text))
                {
                    Toast.Error("Ingrese un correo electrónico valido.", this.Page);
                }
                else {
                    UsuariosCOM usuarios = new UsuariosCOM();
                    usuarios usuario = usuarios.usuario_mail(txtcorreo.Text);
                    if (usuario == null)
                    {
                        Toast.Error("No existe ningun usuario con el correo electrónico proporcionado.", this.Page);
                    }
                    else {
                        string saludo = DateTime.Now.Hour > 13 ? "Buenas tardes" : "Buenos dias";
                       
                        string mail_to = usuario.correo;
                        string subject = "Portal Connext | Recuperación de contraseña";
                        string mail = "<div>" + saludo + " <strong>" +
                            System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(usuario.nombres.ToLower() + " "+usuario.a_paterno.ToLower().Trim() + " "+(usuario.a_materno==null?"": usuario.a_materno.ToLower().Trim()))
                            + "</strong> <div>" +
                            "<br>" +
                            "<p>Se anexa la información para el inicio de sesión en el Portal Connext" +
                            "</p>" +
                            "<p>A continuación, se muestra la información completa:</p>" +
                                 "<p><strong>Usuario</strong><br/> " +
                             usuario.usuario + "</p> " +
                               "<p><strong>Contraseña</strong><br/> " +
                              funciones.de64aTexto(usuario.contraseña) + "</p> " +
                               "<p><strong>Nombre</strong><br/> " +
                                System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(usuario.nombres.ToLower() + " " + usuario.a_paterno.ToLower().Trim() + " " + (usuario.a_materno == null ? "" : usuario.a_materno.ToLower().Trim())) + "</p> " +
                               "<p><strong>Correo electrónico</strong><br/> " +
                              usuario.correo + "</p> " +
                                "<p><strong>Puesto</strong><br/> " +
                              (usuario.puesto==""?"--Información no proporcionada--":usuario.puesto) + "</p> " +
                            "<br/><p>Este movimiento fue solicitado el dia <strong>" +
                            DateTime.Now.ToString("dddd dd MMMM, yyyy hh:mm:ss tt", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX")) + "</strong>" +
                            "</p>";
                        CorreosCOM correos = new CorreosCOM();
                        bool correct = correos.SendMail(mail, subject, mail_to);
                        Toast.Success("Se envio la información al correo proporcionado.","Mensaje del sistema",this);
                        ModalClose("#modal_recuperar_contraseña");
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Error("Error al enviar correo: " + ex.Message, this.Page);
            }
            finally {
                lnkguardar.Visible = true;
                lnkcargando.Style["display"] = "none";
            }
        }
    }
}