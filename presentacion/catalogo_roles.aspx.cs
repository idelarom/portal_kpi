using datos.Modelos;
using datos.NAVISION;
using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
namespace presentacion
{
    public partial class catalogo_roles : System.Web.UI.Page
    {
        private void ModalShow(string modalname)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                             "ModalShow('" + modalname + "');", true);
        }

        private void CargarRoles()
        {
            try
            {
                roles_proyecto entidad = new roles_proyecto();
                RolesCOM roles = new RolesCOM();
                DataTable dt_roles = roles.Get(entidad);
                grid_roles.DataSource = dt_roles;
                grid_roles.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        private String AgregarRol()
        {
            try
            {
                
                string vmensaje = "";
                if (rtxtrol.Text == "")
                {
                    vmensaje = "Escriba el Nombre del Rol";
                }
                else if (rtxtresponsabilidades.Text == "")
                {
                    vmensaje = "Escriba las Responsabilidades";
                }
                else if (Convert.ToInt32(rtxtnivel.Text == "" ? "0" : rtxtnivel.Text) == 0)
                {
                    vmensaje = "Indique un nivel mayor a 0(cero)";
                }
                else {
                    roles_proyecto entidad = new roles_proyecto();
                    entidad.rol = rtxtrol.Text.Trim();
                    entidad.responsabilidades = rtxtresponsabilidades.Text.Trim();
                    entidad.nivel = Convert.ToByte(rtxtnivel.Text.Trim());
                    entidad.usuario = Session["usuario"] as string;
                    RolesCOM roles = new RolesCOM();
                    vmensaje = roles.Agregar(entidad);
                }


                return vmensaje;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        private String EditarRol()
        {
            try
            {

                string vmensaje = "";
                if (rtxtrol.Text == "")
                {
                    vmensaje = "Escriba el Nombre del Rol";
                }
                else if (rtxtresponsabilidades.Text == "")
                {
                    vmensaje = "Escriba las Responsabilidades";
                }
                else if (Convert.ToInt32(rtxtnivel.Text == "" ? "0" : rtxtnivel.Text) == 0)
                {
                    vmensaje = "Indique un nivel mayor a 0(cero)";
                }
                else
                {
                    roles_proyecto entidad = new roles_proyecto();
                    entidad.id_rol = Convert.ToInt32(txtid_rol.Text);
                    entidad.rol = rtxtrol.Text.Trim();
                    entidad.responsabilidades = rtxtresponsabilidades.Text.Trim();
                    entidad.nivel = Convert.ToByte(rtxtnivel.Text.Trim());
                    entidad.usuario_edicion = Session["usuario"] as string;
                    RolesCOM roles = new RolesCOM();
                    vmensaje = roles.Editar(entidad);
                }


                return vmensaje;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRoles();
            }
        }

  
        protected void lnkcommand_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = sender as LinkButton;
                string command = lnk.CommandName;
                int id_rol = Convert.ToInt32(lnk.CommandArgument);
                roles_proyecto entidad = new roles_proyecto();
                entidad.id_rol = Convert.ToInt32(id_rol);
                entidad.comentarios_borrado = hdfmotivos.Value.Trim();
                entidad.usuario_borrado = Session["usuario"] as string;
                RolesCOM roles = new RolesCOM();
                rtxtnivel.ReadOnly = false;
                if (command == "Editar")
                {
                    DataTable dt_rol = roles.Get(entidad);
                    txtid_rol.Text = id_rol.ToString().Trim();
                    rtxtnivel.Text = dt_rol.Rows[0]["nivel"].ToString().Trim();
                    rtxtresponsabilidades.Text = dt_rol.Rows[0]["responsabilidades"].ToString().Trim();
                    rtxtrol.Text = dt_rol.Rows[0]["rol"].ToString().Trim();
                    rtxtnivel.ReadOnly = Convert.ToBoolean(dt_rol.Rows[0]["en_uso"]);
                    ModalShow("#myModalEmpleados");
                }
                else
                {
                    string vmensaje = roles.Borrar(entidad);
                    if (vmensaje == "")
                    {
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                            "ModalClose();", true);
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                            "AlertGO('Rol Eliminado Correctamente', 'catalogo_roles.aspx');", true);
                    }
                    else
                    {
                        Alert.ShowAlertError(vmensaje, this);
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            div_error.Visible = false;
            lblerror.Text = "";
            string vmensaje = txtid_rol.Text == "" ? AgregarRol() : EditarRol();
            if (vmensaje == "")
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                    "ModalClose();", true);
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                    "AlertGO('Rol Guardado Correctamente', 'catalogo_roles.aspx');", true);
            }
            else
            {
                div_error.Visible = true;
                lblerror.Text =vmensaje;
                ModalShow("#myModalEmpleados");
            }
        }

        protected void lnkagregar_Click(object sender, EventArgs e)
        {
            rtxtnivel.ReadOnly = false;
            txtid_rol.Text = "";
            ModalShow("#myModalEmpleados");
        }
    }
}