using datos.NAVISION;
using negocio.Componentes;
using negocio.Entidades;
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
            }
           // div_usuarios.Visible = Convert.ToBoolean(Session["administrador"]);
        }


        #region FUNCIONES

        //private void Menu(string filtro)
        //{
        //    try
        //    {
        //        int id_menu_padre = Request.QueryString["m"] == null ? 0 : Convert.ToInt32(funciones.de64aTexto(Request.QueryString["m"]));
        //        bool admnistrador = Convert.ToBoolean(Session["administrador"]);
        //        bool cliente = Convert.ToBoolean(Session["cliente"]);
        //        MenuCOM menus = new MenuCOM();
        //        DataSet ds = menus.ListadoMenus(id_menu_padre,admnistrador, filtro, cliente);
        //        DataTable dt_menus = ds.Tables[0];
        //        if (dt_menus.Rows.Count > 0)
        //        {
        //            repeat_menu.DataSource = dt_menus;
        //            repeat_menu.DataBind();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Alert.ShowAlertError(ex.ToString(), this);
        //    }
        //}


        private void ModalShow(string modalname)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                             "ModalShow('" + modalname + "');", true);
        }



        #endregion
    }
}