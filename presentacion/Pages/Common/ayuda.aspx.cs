using datos.Model;
using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace presentacion.Pages.Common
{
    public partial class ayuda : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarAyudas();
            }

        }
        private void CargarAyudas()
        {
            try
            {
                AyudaCOM ayudas = new AyudaCOM();
                DataTable dt = ayudas.SelectAll();
                DataView dv = dt.DefaultView;
                dv.Sort = "id_ayuda_padre asc,id_ayuda asc";
                DataTable sortedDT = dv.ToTable();
                List<SiteDataItem> siteData = new List<SiteDataItem>();
                foreach (DataRow row in sortedDT.Rows)
                {
                    siteData.Add(new SiteDataItem(
                        Convert.ToInt32(row["id_ayuda"]),
                        Convert.ToInt32(row["id_ayuda_padre"]),
                        row["titulo"].ToString(),
                        row["descripcion"].ToString()
                        ));
                }

                rtrvProyectWorks.DataTextField = "Text";
                rtrvProyectWorks.DataValueField = "ID";
                rtrvProyectWorks.DataFieldID = "ID";
                rtrvProyectWorks.DataFieldParentID = "ParentID";
                rtrvProyectWorks.DataSource = siteData;
                rtrvProyectWorks.DataBind();
            }
            catch (Exception ex)
            {
                Toast.Error("Error al cargar tareas. " + ex.Message, this);
            }
        }

        private ayudas GetAyuda(int id_ayuda)
        {
            ayudas dt = new ayudas();
            try
            {
                AyudaCOM ayudas = new AyudaCOM();
                dt = ayudas.Ayuda(id_ayuda);

            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }

        protected void rtrvProyectWorks_NodeClick(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
        {
            int id = Convert.ToInt32(e.Node.Value);
            RadTreeNode node1 = rtrvProyectWorks.FindNodeByValue(id.ToString()) as RadTreeNode;
            if (node1 != null)
            {
                int nodes_children = node1.Nodes.Count;
                if (nodes_children > 0)
                {
                    node1.Expanded = node1.Expanded == true ? false : true;
                }
                ayudas ayuda = GetAyuda(id);
                if (ayuda != null && ayuda.id_ayuda_padre != null)
                {
                    lbldesc.Text = ayuda.descripcion == "" ? "No hay descripción disponible." : ayuda.descripcion;
                    lbltituloayuda.Text = ayuda.titulo;
                    string codigo = "";
                    image.Visible = false;
                    PlaceHolder1.Visible = false;
                    if (ayuda.video)
                    {
                        string url = HttpContext.Current.Request.Url.AbsoluteUri;
                        url = url.Replace("ayuda.aspx","");
                        codigo = "<div align='center' style='max-height:400px' class='embed-responsive embed-responsive-16by9'>" +    
                                    "<video class='embed-responsive-item' controls>"+
                                     "   <source src='"+ url + ayuda.src + "'  type='video/mp4'>" +
                                    "</video>"+
                                   " </div>";
                        PlaceHolder1.Visible = true;
                        PlaceHolder1.Controls.Add(new Literal {  Text = codigo });
                    }
                    else {
                        image.Visible = true;
                        image.ImageUrl = ayuda.src;
                    }
                    
                    ModalShow("#ModalAyudas");
                }
            }

        }
    }
}