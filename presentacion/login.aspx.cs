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
            //jajajaa
            try
            {
                bool retur = true;
                if (usuario == "")
                {
                    Toast.Info("Ingrese Usuario", "Mensaje del Sistema", this);
                    retur = false;
                }
                if (password == "")
                {
                    Toast.Info("Ingrese Contraseña", "Mensaje del Sistema", this);
                    retur = false;
                }
                if (!LoginActive(usuario, password, rtxtdominio.Text.Trim()))
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
                        Boolean admin = false;
                        Session["usuario"] = username;
                        Session["password"] = password;
                        Session["contraseña"] = password;
                        Session["nombre"] = nombre_user;
                        Session["correo_pm"] = row["Company_E_Mail"].ToString().Trim().ToLower();
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