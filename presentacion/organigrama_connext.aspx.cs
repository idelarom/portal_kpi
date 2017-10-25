using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class organigrama_connext : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                "CargarDiagramaEmpleados();", true);
        }


        [System.Web.Services.WebMethod]
        public static List<ArbolInvolucrados> GetEmpleadosInvolucrados()
        {
            List<ArbolInvolucrados> tree_involucrados = new List<ArbolInvolucrados>();
            try
            {
                EmpleadosCOM involucraods = new EmpleadosCOM();
                DataSet ds = involucraods.sp_listado_empleados(5264, false,false);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    tree_involucrados.Add(new ArbolInvolucrados
                    {
                        idpinvolucrado = Convert.ToInt32(row["num_empleado"]),
                        nombre = row["nombre"].ToString(),
                        correo = row["correo"].ToString(),
                        telefono = "0",
                        rol = row["puesto"].ToString(),
                        id_parent = Convert.ToInt32(row["numjefe"])
                    });
                }
                return tree_involucrados;
            }
            catch (Exception ex)
            {
                return tree_involucrados;
            }
        }
    }
}