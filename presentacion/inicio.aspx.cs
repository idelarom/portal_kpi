using datos.NAVISION;
using negocio.Componentes;
using negocio.Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace presentacion
{
    public partial class inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string usuario = Session["usuario"] as string;
                hdf_usuario.Value = usuario.ToUpper().ToString();
                hdf_numempleado.Value = Convert.ToInt32(Session["num_empleado"]).ToString();
                hdf_ver_Todos_empleados.Value = Convert.ToBoolean(Session["ver_Todos_los_empleados"]).ToString();
                CargarOrdenDivs();
            }
        }

       

        [System.Web.Services.WebMethod]
        public static String getDivs(string usuario)
        {
            try
            {
                UsuariosCOM usuarios = new UsuariosCOM();
                DataTable dt = usuarios.sp_usuario_widgets(usuario).Tables[0];
                string value = JsonConvert.SerializeObject(dt);
                return value;
            }
            catch (Exception)
            {
                return "";
            }
        }


        private void CargarOrdenDivs()
        {
            try
            {
                EmpleadosCOM componente = new EmpleadosCOM();
                //UsuariosCOM usuarios = new UsuariosCOM();
                //DataTable dt = usuarios.sp_usuario_widgets(Convert.ToString(Session["usuario"])).Tables[0];
                //foreach (DataRow row in dt.Rows)
                //{
                //    string div = row["nombre_codigo"].ToString().Trim().ToLower();
                //    if (div == "dashboard_kpi_ind")
                //    {
                //        dashboard_kpi_ind.Visible = true;
                //    }else if (div == "dashboard_kpi")
                //    {
                //        dashboard_kpi.Visible = true;
                //    }
                //    else if (div == "div1")
                //    {
                //        Div1.Visible = true;
                //    }
                //    else if (div == "div2")
                //    {
                //        Div2.Visible = true;
                //    }

                //}
                DataSet ds = componente.sp_order_widgets(Convert.ToString(Session["usuario"]));
                DataTable dt = ds.Tables[0];
                string value = dt.Rows[0]["order"].ToString().Trim();
                if (value != "")
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<script type='text/javascript'>");
                    sb.Append("$(document).ready(function () {");
                    sb.Append(value);
                    sb.Append("});");
                    sb.Append("</script>");
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), sb.ToString(), false);
                }
            }
            catch (Exception ex)
            {
                Toast.Error(ex.Message,this);
            }
        }
        private void ModalShow(string modalname)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                             "ModalShow('" + modalname + "');", true);
        }

        
    }
}