using datos.NAVISION;
using datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using datos.Model;

namespace negocio.Componentes
{
    public class WidgetsCOM
    {
        public string AgregarConfiguracion(usuarios_widgets entidad)
        {
            try
            {
                usuarios_widgets widget = new usuarios_widgets
                {
                    usuario  = entidad.usuario,
                    id_widget = entidad.id_widget,
                    fecha = DateTime.Now,
                    usuario_creador = entidad.usuario
                };
                Model context = new Model();
                context.usuarios_widgets.Add(widget);
                int id_entity = widget.id_usuariow;
                context.SaveChanges();
                return "";
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                return fullErrorMessage.ToString();
            }
        }
        public string EditarOrden(usuarios_widgets entidad)
        {
            try
            {
                Model context = new Model();
                usuarios_widgets widget = context.usuarios_widgets
                                .First(i => i.usuario == entidad.usuario && i.id_widget == entidad.id_widget);
                widget.orden = entidad.orden;
                widget.usuario_edicion = entidad.usuario_edicion;
                widget.fecha_edicion = DateTime.Now;
                context.SaveChanges();
                return "";
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                return fullErrorMessage.ToString();
            }
        }
        /// <summary>
        /// Edita la configuracion de un widget
        /// </summary>
        /// <param name="entidad"></param>
        /// <param name="usuarios_adicionales"></param>
        /// <returns></returns>
        public string EditarConfiguracion(usuarios_widgets entidad)
        {
            try
            {
                Model context = new Model();
                usuarios_widgets widget = context.usuarios_widgets
                                .First(i => i.usuario == entidad.usuario && i.id_widget == entidad.id_widget);
                widget.orden = entidad.orden;
                widget.usuario_edicion = entidad.usuario_edicion;
                widget.fecha_edicion = DateTime.Now;
                context.SaveChanges();
                return "";
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                return fullErrorMessage.ToString();
            }
        }
        /// <summary>
        /// Edita la configuracion de un widget
        /// </summary>
        /// <param name="entidad"></param>
        /// <param name="usuarios_adicionales"></param>
        /// <returns></returns>
        public string EditarVisibilidadConfiguracion(usuarios_widgets entidad, bool visible)
        {
            try
            {
                Model context = new Model();
                usuarios_widgets widget = context.usuarios_widgets
                                .First(i => i.usuario == entidad.usuario && i.id_widget == entidad.id_widget);              
                widget.usuario_edicion = entidad.usuario_edicion;
                widget.fecha_edicion = DateTime.Now;
                widget.usuario_borrado = entidad.usuario_edicion;
                widget.comentarios_borrado = "BORRADO POR EL USUARIO";
                widget.fecha_borrado = DateTime.Now;
                if (visible)
                {
                    widget.fecha_edicion = DateTime.Now;
                    widget.usuario_borrado = null;
                    widget.comentarios_borrado = null;
                    widget.fecha_borrado = null;
                }
                context.SaveChanges();
                return "";
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                return fullErrorMessage.ToString();
            }
        }
        public bool ExistAnyConfig(string usuario)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.usuarios_widgets
                                .Where(s => s.usuario.ToUpper() == usuario.ToUpper())
                                .Select(u => new
                                {
                                    u.id_widget
                                });
                dt = To.DataTable(query.ToList());
                return dt.Rows.Count > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                          .SelectMany(x => x.ValidationErrors)
                          .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                return false;
            }
        }
        public bool ExistConfig(string usuario, int id_widget)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.usuarios_widgets
                                .Where(s => s.usuario.ToUpper() == usuario.ToUpper() && s.id_widget == id_widget && s.usuario_borrado == null)
                                .Select(u => new
                                {
                                    u.id_widget
                                });
                dt = To.DataTable(query.ToList());
                return dt.Rows.Count > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                          .SelectMany(x => x.ValidationErrors)
                          .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                return false;
            }
        }
        public DataSet sp_guardar_usuarios_config_orden(string usuario)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario", SqlDbType = SqlDbType.Int, Value = usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_guardar_usuarios_config_orden", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet sp_editar_widgets(int id_widget, string widget, string icono, string ejemplo_html, string usuario,string nombre_codigo, string individual,string ayuda)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_widget", SqlDbType = SqlDbType.Int, Value = id_widget });
            listparameters.Add(new SqlParameter() { ParameterName = "@pwidget", SqlDbType = SqlDbType.Int, Value = widget });
            listparameters.Add(new SqlParameter() { ParameterName = "@picono", SqlDbType = SqlDbType.Int, Value = icono });
            listparameters.Add(new SqlParameter() { ParameterName = "@pejemplo_html", SqlDbType = SqlDbType.Int, Value = ejemplo_html });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario", SqlDbType = SqlDbType.Int, Value = usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombre_codigo", SqlDbType = SqlDbType.Int, Value = nombre_codigo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pindividual", SqlDbType = SqlDbType.Int, Value = individual });
            listparameters.Add(new SqlParameter() { ParameterName = "@payuda", SqlDbType = SqlDbType.Int, Value = ayuda });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_editar_widget", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_agregar_widgets(string widget, string icono, string ejemplo_html, string usuario, string nombre_codigo ,string individual,string ayuda)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pwidget", SqlDbType = SqlDbType.Int, Value = widget });
            listparameters.Add(new SqlParameter() { ParameterName = "@picono", SqlDbType = SqlDbType.Int, Value = icono });
            listparameters.Add(new SqlParameter() { ParameterName = "@pejemplo_html", SqlDbType = SqlDbType.Int, Value = ejemplo_html });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario", SqlDbType = SqlDbType.Int, Value = usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombre_codigo", SqlDbType = SqlDbType.Int, Value = nombre_codigo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pindividual", SqlDbType = SqlDbType.Int, Value = individual });
            listparameters.Add(new SqlParameter() { ParameterName = "@payuda", SqlDbType = SqlDbType.Int, Value = ayuda });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_agregar_widgets", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_borrar_widgets(int id_widget, string usuario, string comentarios)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_widget", SqlDbType = SqlDbType.Int, Value = id_widget });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario", SqlDbType = SqlDbType.Int, Value = usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcomentarios", SqlDbType = SqlDbType.Int, Value = comentarios });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_borrar_widget", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_catalogo_widgets(int id_widget)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_widget", SqlDbType = SqlDbType.Int, Value = id_widget });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_catalogo_widgets", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        
    }
}
