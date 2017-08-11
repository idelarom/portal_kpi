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
    public partial class configuracion_portal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkactualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string username = Session["usuario"] as string;
                string password = Session["contraseña"] as string;
                DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://" + "MIGESA.NET", username, password);
                int NumJefe = Convert.ToInt32(Session["NumJefe"]);
                int num_empleado = Convert.ToInt32(Session["num_empleado"]);
                Boolean ver_Todos_los_empleados = Convert.ToBoolean(Session["ver_Todos_los_empleados"]);
                EmpleadosCOM empleados = new EmpleadosCOM();
                bool no_activos = false;
                DataSet ds = empleados.sp_listado_empleados(num_empleado, ver_Todos_los_empleados, no_activos);
                DataTable dt_empleados = ds.Tables[0];
                foreach (DataRow row in dt_empleados.Rows)
                {
                    string usuario = row["usuario"].ToString().Trim().ToUpper();
                    //Create a searcher on your DirectoryEntry
                    DirectorySearcher adSearch = new DirectorySearcher(directoryEntry);
                    adSearch.SearchScope = SearchScope.Subtree;    //Look into all subtree during the search
                    adSearch.Filter = "(&(ObjectClass=user)(sAMAccountName=" + usuario + "))";    //Filter information, here i'm looking at a user with given username
                    if (adSearch != null)
                    {
                        SearchResult sResult = adSearch.FindOne();       //username is unique, so I want to find only one
                        string imagen = "";
                        if (sResult != null)
                        {
                            if (sResult.Properties["thumbnailPhoto"].Count > 0)
                            {
                                byte[] array_img = sResult.Properties["thumbnailPhoto"][0] as byte[];    //Get the property info
                                imagen = GuardarImagenUsuario(array_img, usuario + ".png");
                            }
                        }
                    }
                }
                Toast.Success("Todas las fotografias de los usuarios fueron actualizadas de manera correcta.", "Porceso terminado correctamente", this);

            }
            catch (Exception ex)
            {
                Toast.Error("Error al actualizar imagenes: " + ex.Message, this);
            }
            finally {
                lnkactualizar.Visible = true;
                lnkactualizaractualizar.Style["display"] = "none";
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
    }
}