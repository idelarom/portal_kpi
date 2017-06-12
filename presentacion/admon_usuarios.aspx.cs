using datos.Modelos;
using negocio.Componentes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace presentacion
{
    public partial class admon_usuarios : System.Web.UI.Page
    {
        private void ModalShow(string modalname)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                             "ModalShow('" + modalname + "');", true);
        }

        private void CargarUsuarios(bool solo_clientes, bool solo_empleados)
        {
            try
            {
                UsuariosCOM usuarios = new UsuariosCOM();
                DataTable dt_roles =
                    usuarios.sp_catalogo_administracion_usuarios(Convert.ToInt32(Session["id_cliente"]), Session["usuario"] as string, solo_clientes, solo_empleados).Tables[0];
                grid_usuarios.DataSource = dt_roles;
                grid_usuarios.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        private void CargarTiposUsuarios(int id_tipo)
        {
            try
            {
                UsuariosCOM usuarios = new UsuariosCOM();
                DataTable dt = usuarios.GetTiposUsuariosClientes();
                ddltipos_usuarios.DataValueField = "id_uperfil";
                ddltipos_usuarios.DataTextField = "perfil";
                ddltipos_usuarios.DataSource = dt;
                ddltipos_usuarios.DataBind();
                ddltipos_usuarios.Items.Insert(0, new ListItem("--Seleccione un Tipo de Usuario", "0"));
                ddltipos_usuarios.SelectedValue = id_tipo.ToString();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError("Erro al cargar lista de tipos de usuarios: " + ex.ToString(), this);
            }
        }

        private void CargarProyectos(int id_proyecto, string usuario, bool administrador)
        {
            try
            {
                ProyectosCOM proyectos = new ProyectosCOM();
                DataTable dt = proyectos.sp_get_proyects_info(id_proyecto, usuario, administrador, Convert.ToInt32(Session["id_cliente"]),"").Tables[0];
                rdl_proyectos.DataTextField = "proyecto";
                rdl_proyectos.DataValueField = "id_proyecto";
                rdl_proyectos.DataSource = dt;
                rdl_proyectos.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        private string AgregarUsuarioCliente()
        {
            try
            {
                string vmensaje = "";
                usuarios entidad = new usuarios();
                entidad.id_uperfil = Convert.ToInt32(ddltipos_usuarios.SelectedValue);
                entidad.usuario = rtxtusuario.Text.Trim();
                entidad.password = funciones.deTextoa64(rtxtcontra.Text.Trim());
                entidad.id_cliente = Convert.ToInt32(Session["id_cliente"]);
                UsuariosCOM usuarios = new UsuariosCOM();
                string[] return_array = usuarios.Agregar(entidad);
                vmensaje = return_array[0];
                int id_usuario = Convert.ToInt32(return_array[1]);
                if (id_usuario > 0)
                {
                    IList<RadListBoxItem> collection = rdl_proyectos.SelectedItems;
                    foreach (RadListBoxItem item in collection)
                    {
                        usuarios_proyectos entidad_p = new usuarios_proyectos();
                        entidad_p.id_usuario = id_usuario;
                        entidad_p.id_proyecto = Convert.ToInt32(item.Value);
                        vmensaje = usuarios.AgregarAProyecto(entidad_p);
                        if (vmensaje != "") { break; }
                    }
                }
                return vmensaje;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        private string EditarUsuarioCliente()
        {
            try
            {
                string vmensaje = "";
                usuarios entidad = new usuarios();
                int id_usuario = Convert.ToInt32(txtid_usuario.Text.Trim());
                entidad.id_usuario = id_usuario;
                entidad.id_uperfil = Convert.ToInt32(ddltipos_usuarios.SelectedValue);
                entidad.usuario = rtxtusuario.Text.Trim();
                entidad.password = funciones.deTextoa64(rtxtcontra.Text.Trim());
                entidad.id_cliente = Convert.ToInt32(Session["id_cliente"]);
                entidad.usuario_edicion = Session["usuario"] as string;
                UsuariosCOM usuarios = new UsuariosCOM();
                usuarios_proyectos entidad_p2= new usuarios_proyectos();
                entidad_p2.id_usuario = id_usuario;
                vmensaje = usuarios.Editar(entidad);
                vmensaje = usuarios.BorrardeProyecto(entidad_p2);
                if (id_usuario > 0)
                {
                    IList<RadListBoxItem> collection = rdl_proyectos.SelectedItems;
                    foreach (RadListBoxItem item in collection)
                    {
                        usuarios_proyectos entidad_p = new usuarios_proyectos();
                        entidad_p.id_usuario = id_usuario;
                        entidad_p.id_proyecto = Convert.ToInt32(item.Value);
                        vmensaje = usuarios.AgregarAProyecto(entidad_p);
                        if (vmensaje != "") { break; }
                    }
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
                bool cliente = Convert.ToBoolean(Session["cliente"]);
                bool admin = Convert.ToBoolean(Session["administrador"]);
                if (cliente)
                {
                    ddltipousuarios.Items.Insert(0, new ListItem("Clientes", "1"));
                    CargarUsuarios(true, false);
                }
                else
                {
                    div_tipos.Visible = true;
                    ddltipousuarios.Items.Insert(0, new ListItem("--Seleccione un Tipo de Usuario", "0"));
                    ddltipousuarios.Items.Insert(1, new ListItem("Clientes", "1"));
                    ddltipousuarios.Items.Insert(2, new ListItem("Empleados", "2"));
                    if (admin) {

                        ddltipousuarios.Items.Insert(3, new ListItem("Todos", "3"));
                    }
                }
            }
        }

        protected void lnkagregar_Click(object sender, EventArgs e)
        {
            txtid_usuario.Text = "";
            CargarTiposUsuarios(0);
            CargarProyectos(0, Session["usuario"] as string, Convert.ToBoolean(Session["administrador"]));
            rtxtcontra.Text = "";
            rtxtusuario.Text = "";
            ModalShow("#myModal");
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            string vmensaje = "";
            div_error.Visible = false;
            IList<RadListBoxItem> collection = rdl_proyectos.SelectedItems;
            if (rtxtusuario.Text == "")
            {
                vmensaje = "Ingrese el usuario";
            }
            else if (rtxtusuario.Text == "")
            {
                vmensaje = "Ingrese una contraseña";
            }
            else if (ddltipos_usuarios.SelectedValue == "0")
            {
                vmensaje = "Seleccione un tipo de usuario";
            }
            else if (collection.Count == 0)
            {
                vmensaje = "Seleccione uno o mas proyectos a relacionar";
            }
            else
            {
                vmensaje = txtid_usuario.Text == "" ? AgregarUsuarioCliente() : EditarUsuarioCliente() ;
            }

            if (vmensaje == "")
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                    "ModalClose();", true);
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                    "AlertGO('Usuario Guardado Correctamente', 'admon_usuarios.aspx');", true);
            }
            else
            {
                div_error.Visible = true;
                lblerror.Text = vmensaje;
            }
        }

        protected void lnkcommand_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = sender as LinkButton;
                string command = lnk.CommandName;
                int id_usuario = Convert.ToInt32(lnk.CommandArgument);
                usuarios entidad = new usuarios();
                entidad.id_usuario = Convert.ToInt32(id_usuario);
                entidad.comentarios_borrado = hdfmotivos.Value.Trim();
                entidad.usuario_borrado = Session["usuario"] as string;
                UsuariosCOM usuarios = new UsuariosCOM();
                usuarios_proyectos entidad_p2 = new usuarios_proyectos();
                if (command == "Editar")
                {
                    CargarTiposUsuarios(0);
                    CargarProyectos(0, Session["usuario"] as string, Convert.ToBoolean(Session["administrador"]));
                    rtxtcontra.Text = "";
                    rtxtusuario.Text = "";
                    DataTable dt_rol = usuarios.GetID(entidad);
                    txtid_usuario.Text = id_usuario.ToString().Trim();
                    rtxtcontra.Text = funciones.de64aTexto(dt_rol.Rows[0]["password"].ToString().Trim());
                    rtxtusuario.Text = dt_rol.Rows[0]["usuario"].ToString().Trim();
                    ddltipos_usuarios.SelectedValue = dt_rol.Rows[0]["id_uperfil"].ToString().Trim();
                    entidad_p2.id_usuario = id_usuario;
                    DataTable dt_proyectos = usuarios.GetUsersinProyects(entidad_p2);
                    foreach (DataRow row in dt_proyectos.Rows)
                    {
                        string id_proyecto = row["id_proyecto"].ToString().Trim();
                        IList<RadListBoxItem> collection = rdl_proyectos.Items;
                        foreach (RadListBoxItem item in collection)
                        {
                            item.Selected = id_proyecto == item.Value;
                        }
                    }
                    ModalShow("#myModal");
                }
                else
                {
                    string vmensaje = usuarios.Borrar(entidad);
                    entidad_p2.id_usuario = id_usuario;
                    vmensaje = usuarios.BorrardeProyecto(entidad_p2);
                    if (vmensaje == "")
                    {
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                            "ModalClose();", true);
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                            "AlertGO('Usuario Eliminado Correctamente', 'admon_usuarios.aspx');", true);
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

        protected void ddltipousuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tipo = Convert.ToInt32(ddltipousuarios.SelectedValue);
            switch (tipo)
            {
                //solo clientes
                case 1:
                    CargarUsuarios(true, false);
                    break;
                //solo empleados
                case 2:
                    CargarUsuarios(false, true);
                    break;
                //solo ambos
                case 3:
                    CargarUsuarios(false, false);
                    break;
            }
        }
    }
}