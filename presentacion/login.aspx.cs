using datos.Modelos;
using datos.NAVISION;
using negocio.Componentes;
using System;
using System.Data;
using System.DirectoryServices.AccountManagement;
using System.Globalization;

namespace presentacion
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["c"] == null)
                {
                    div_cambiodomiinio.Visible = true;
                    div_portalclientes.Visible = false;
                }
                else
                {
                    div_cambiodomiinio.Visible = false;
                    div_portalclientes.Visible = false;
                }
            }
        }

        protected void btniniciar_Click(object sender, EventArgs e)
        {
            if (Login(rtxtusuario.Text, rtxtcontra.Text))
            {
                ChecarContraseña();
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
                    Alert.ShowAlertInfo("Ingrese Usuario", "Mensaje del Sistema", this);
                    retur = false;
                }
                if (password == "")
                {
                    Alert.ShowAlertInfo("Ingrese Contraseña", "Mensaje del Sistema", this);
                    retur = false;
                }
                if (Request.QueryString["c"] != null && !LoginActiveCliente(usuario, password, rtxtdominio.Text.Trim()))
                {
                    Alert.ShowAlertInfo("Credenciales Invalidas", "Mensaje del Sistema", this);
                    retur = false;
                }
                if (Request.QueryString["c"] == null && !LoginActive(usuario, password, rtxtdominio.Text.Trim()))
                {
                    Alert.ShowAlertInfo("Credenciales Invalidas", "Mensaje del Sistema", this);
                    retur = false;
                }
                return retur;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private Boolean LoginActiveCliente(string username, string password, string dominio)
        {
            try
            {
                Boolean isValid = true;
                UsuariosCOM usuarios = new UsuariosCOM();
                ClientesCOM clientes = new ClientesCOM();
                DataTable dt_usuario = usuarios.Login(username.Trim(), funciones.deTextoa64(password.Trim()),true).Tables[0];
                if (dt_usuario.Rows.Count > 0)
                {
                    DataRow row = dt_usuario.Rows[0];
                    int id_cliente = dt_usuario.Rows[0]["id_cliente"].ToString().Trim() == "" ? 0 : Convert.ToInt32(dt_usuario.Rows[0]["id_cliente"].ToString().Trim());
                    Session["usuario"] = username;
                    Session["password"] = password;

                    bool admin = dt_usuario.Rows.Count > 0 ? Convert.ToBoolean(dt_usuario.Rows[0]["administrador"]) : false;
                    DataTable dt_info = clientes.ListadoClientes(id_cliente).Tables[0];
                    Session["nombre"] = dt_info.Rows.Count > 0 ? dt_info.Rows[0]["Razon_social"].ToString() : "";
                    Session["puesto"] = username;
                    Session["id_usuario"] = Convert.ToInt32(row["id_usuario"]);
                    Session["id_uperfil"] = Convert.ToInt32(row["id_uperfil"]);
                    Session["imagen"] = row["imagen"].ToString().Trim();
                    Session["id_cliente"] = id_cliente;
                    Session["administrador"] = admin;
                    Session["cliente"] = true;
                }
                else
                {
                    isValid = false;
                }
                return isValid;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private void ChecarContraseña()
        {
            try
            {
                datos.Modelos.usuarios entidad = new datos.Modelos.usuarios();
                UsuariosCOM usuarios = new UsuariosCOM();
                entidad.id_usuario = Convert.ToInt32(Session["id_usuario"]);
                DataTable dt = usuarios.GetID(entidad);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["password"].ToString() == "")
                    {
                        EditarUsuario(Convert.ToInt32(Session["id_usuario"]), funciones.deTextoa64((Session["password"] as string).Trim()), (Session["usuario"] as string).Trim(), Convert.ToInt32(Session["id_uperfil"]));

                    }
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError("Error al actualizar los datos del usuario. " + ex.ToString(), this.Page);
            }
        }

        private string EditarUsuario(int id_usuario, string usuario, string contraseña, int id_uperfil)
        {
            try
            {
                datos.Modelos.usuarios entidad = new datos.Modelos.usuarios();
                UsuariosCOM usuarios = new UsuariosCOM();
                entidad.id_usuario = id_usuario;
                entidad.password = usuario;
                entidad.usuario = contraseña;
                entidad.id_uperfil = id_uperfil;
                entidad.usuario_edicion = "Sistema";
                return usuarios.Editar(entidad);
            }
            catch (Exception ex)
            {
                return ex.ToString();
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
                    DataTable dt = empleados.GetLogin(entidad);
                    if (isValid && dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        string nombre = "";
                        string puesto = "";
                        //recuperamos datos
                        nombre = (funciones.SplitLastIndex(row["First_Name"].ToString().Trim(), ' ') + " " +
                                    funciones.SplitLastIndex(row["Last_Name"].ToString().Trim(), ' '));
                        puesto = (row["puesto"].ToString().Trim());
                        //pasamos aminusculas
                        nombre = nombre.ToLower();
                        puesto = puesto.ToLower();
                        //pasamos a estilos title
                        String nombre_user = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(nombre);
                        String puesto_user = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(puesto);

                        UsuariosCOM pusuarios = new UsuariosCOM();
                        DataTable dt_usuario = pusuarios.Login(username.Trim(), funciones.deTextoa64(password.Trim()), false).Tables[0];
                        if (dt_usuario.Rows.Count == 0)//no existe, lo agregamos a usuarios internos
                        {
                            usuarios entidadus = new usuarios();
                            entidadus.id_cliente = null;
                            entidadus.id_uperfil = 3;
                            entidadus.usuario = username.Trim();
                            entidadus.password = funciones.deTextoa64(password.Trim());
                            pusuarios.Agregar(entidadus);
                            dt_usuario = pusuarios.Login(username.Trim(), funciones.deTextoa64(password.Trim()), false).Tables[0];
                        }
                        bool admin = dt_usuario.Rows.Count > 0 ? Convert.ToBoolean(dt_usuario.Rows[0]["administrador"]) : false; 
                        Session["usuario"] = username;
                        Session["password"] = password;
                        Session["contraseña"] = password;
                        Session["nombre"] = nombre_user;
                        Session["correo_pm"] = row["Company_E_Mail"].ToString().Trim().ToLower();
                        Session["id_usuario"] = Convert.ToInt32(dt_usuario.Rows[0]["id_usuario"]);
                        Session["id_uperfil"] = Convert.ToInt32(dt_usuario.Rows[0]["id_uperfil"]);
                        Session["imagen"] = dt_usuario.Rows[0]["imagen"].ToString().Trim();
                        Session["puesto"] = puesto_user;
                        Session["administrador"] = admin;
                        Session["cliente"] = false;
                        Session["id_cliente"] = 0;
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
                return false;
            }
        }

        #endregion METODOS

        protected void lnkcambiardominio_Click(object sender, EventArgs e)
        {
            div_dominio.Visible = div_dominio.Visible ? false : true;
        }
    }
}