using datos.NAVISION;
using negocio.Componentes;
using System;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Globalization;
using System.IO;

namespace presentacion
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
                    Toast.Error("Credenciales Invalidas", this);
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
                    DataTable dt = empleados.GetLogin(username);
                    if (isValid && dt.Rows.Count > 0)
                    {
                        DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://" + dominio, username, password);
                        //Create a searcher on your DirectoryEntry
                        DirectorySearcher adSearch = new DirectorySearcher(directoryEntry);
                        adSearch.SearchScope = SearchScope.Subtree;    //Look into all subtree during the search
                        adSearch.Filter = "(&(ObjectClass=user)(sAMAccountName=" + username + "))";    //Filter information, here i'm looking at a user with given username
                        SearchResult sResult = adSearch.FindOne();       //username is unique, so I want to find only one
                        string imagen = "";
                        if (sResult.Properties["thumbnailPhoto"].Count > 0)
                        {
                            byte[] array_img = sResult.Properties["thumbnailPhoto"][0] as byte[];    //Get the property info
                            imagen = GuardarImagenUsuario(array_img, username + ".png");
                        }
                     
                        DataRow row = dt.Rows[0];
                        //recuperamos datos
                        string nombre = (funciones.SplitLastIndex(row["First_Name"].ToString().Trim(), ' ') + " " +
                                    funciones.SplitLastIndex(row["Last_Name"].ToString().Trim(), ' '));
                        string puesto = (row["puesto"].ToString().Trim());
                        string perfil = row["perfil"].ToString().Trim().ToLower();
                        //pasamos aminusculas
                        nombre = nombre.ToLower();
                        puesto = puesto.ToLower();
                        //pasamos a estilos title
                        Session["imagen"] = imagen;
                        Session["usuario"] = username;
                        Session["password"] = password;
                        Session["contraseña"] = password;
                        Session["nombre"] = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(nombre);
                        Session["correo"] = row["Company_E_Mail"].ToString().Trim().ToLower();
                        Session["puesto"] = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(puesto);
                        Session["perfil"] = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(perfil); 
                        Session["id_perfil"] = Convert.ToInt32(row["id_perfil"]);
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
                        DateTime fecha_inicio_sesion = DateTime.Now;
                        Session["os"] =os;
                        Session["os_vers"] = os_vers;
                        Session["browser"] = browser;
                        Session["device"] = device;
                        Session["ip"] = ip;
                        Session["fecha_inicio_sesion"] = fecha_inicio_sesion;
                        DataSet ds = empleados.sp_agregar_usuario_sesiones(username.Trim().ToUpper(), os, os_vers, browser, device,
                            ip,lat,lon,region,proveedor,modelo, fecha_inicio_sesion);
                        int id_usuario_sesion = ds.Tables[0].Columns.Contains("id_usuario_sesion") ? 
                            Convert.ToInt32(ds.Tables[0].Rows[0]["id_usuario_sesion"]) : 0;
                        Session["devices_conectados"] = UpdateDevices(username);
                        if (id_usuario_sesion > 0)
                        {
                            Session["id_usuario_sesion"] = id_usuario_sesion;
                        }
                        else
                        {
                            isValid = false;
                        }
                    }
                    else
                    {
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
                DataTable dt = empleados.sp_usuario_sesiones(usuario).Tables[0];
               return  dt.Rows.Count;
               

            }
            catch (Exception ex)
            {
                Toast.Error("Error al actualizar la lista de dispositivos conectados: " + ex.Message, this.Page);
                return 0;
            }
        }

    }
}