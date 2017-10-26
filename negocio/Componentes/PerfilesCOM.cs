using datos;
using datos.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;

namespace negocio.Componentes
{
    public class PerfilesCOM
    {
        /// <summary>
        /// Agrega un usuario a un perfil
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="id_menu"></param>
        /// <returns></returns>
        public string Agregar(string usuario, int id_perfil, string creador)
        {
            try
            {
                usuarios_perfiles uperfil = new usuarios_perfiles
                {
                    id_perfil= id_perfil,
                    usuario = usuario,
                    fecha = DateTime.Now,
                    usuario_creador = creador
                };
                Model context = new Model();
                context.usuarios_perfiles.Add(uperfil);
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
        public bool ExistUsuarioPerfil(string usuario)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.usuarios_perfiles
                                .Where(s => s.usuario.ToUpper() == usuario.ToUpper() && s.usuario_borrado == null)
                                .Select(u => new
                                {
                                    u.id_usuariop
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
        public string Eliminar(string usuario)
        {
            try
            {
                Model context = new Model();
                IEnumerable<usuarios_perfiles> usuarios_perfiles = context.usuarios_perfiles
                               .Where(s => s.usuario.Trim().ToUpper() == usuario.Trim().ToUpper()); 
                
                foreach (usuarios_perfiles usuario_perfil in usuarios_perfiles)
                {
                    usuario_perfil.usuario_borrado =usuario;
                    usuario_perfil.fecha_borrado = DateTime.Now;
                    usuario_perfil.comentarios_borrado ="BORRADO POR ACTUALIZACION";
                }
                IEnumerable<usuarios_widgets> usuarios_widgets = context.usuarios_widgets
                            .Where(s => s.usuario.Trim().ToUpper() == usuario.Trim().ToUpper());
                foreach (usuarios_widgets usuario_widget in usuarios_widgets)
                {
                    usuario_widget.usuario_borrado = usuario;
                    usuario_widget.fecha_borrado = DateTime.Now;
                    usuario_widget.comentarios_borrado = "BORRADO POR ACTUALIZACION";
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
        public DataSet sp_editar_perfiles(int id_perfil, string perfil, string usuario, string cadena_usuarios,
            int total_cadena_usuarios, string cadena_widgets, int total_cadena_widgets,
            string cadena_menus, int total_cadena_menus, bool ver_todos_empleados)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_perfil", SqlDbType = SqlDbType.Int, Value = id_perfil });
            listparameters.Add(new SqlParameter() { ParameterName = "@pperfil", SqlDbType = SqlDbType.Int, Value = perfil });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario", SqlDbType = SqlDbType.Int, Value = usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_usuarios", SqlDbType = SqlDbType.Int, Value = cadena_usuarios });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena_usuarios", SqlDbType = SqlDbType.Int, Value = total_cadena_usuarios });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_widgets", SqlDbType = SqlDbType.Int, Value = cadena_widgets });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena_widgets", SqlDbType = SqlDbType.Int, Value = total_cadena_widgets });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_menus", SqlDbType = SqlDbType.Int, Value = cadena_menus });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena_menus", SqlDbType = SqlDbType.Int, Value = total_cadena_menus });
            listparameters.Add(new SqlParameter() { ParameterName = "@pver_todos_los_empleados", SqlDbType = SqlDbType.Int, Value = ver_todos_empleados });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_editar_perfiles", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_agregar_perfiles(string perfil, string usuario,
            string cadena_usuarios, int total_cadena_usuarios,
            string cadena_widgets, int total_cadena_widgets,
            string cadena_menus, int total_cadena_menus, bool ver_todos_empleados)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pperfil", SqlDbType = SqlDbType.Int, Value = perfil });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario", SqlDbType = SqlDbType.Int, Value = usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_usuarios", SqlDbType = SqlDbType.Int, Value = cadena_usuarios });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena_usuarios", SqlDbType = SqlDbType.Int, Value = total_cadena_usuarios });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_widgets", SqlDbType = SqlDbType.Int, Value = cadena_widgets });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena_widgets", SqlDbType = SqlDbType.Int, Value = total_cadena_widgets });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_menus", SqlDbType = SqlDbType.Int, Value = cadena_menus });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena_menus", SqlDbType = SqlDbType.Int, Value = total_cadena_menus });
            listparameters.Add(new SqlParameter() { ParameterName = "@pver_todos_los_empleados", SqlDbType = SqlDbType.Int, Value = ver_todos_empleados });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_agregar_perfiles", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_borrar_perfiles(int pid_perfil, string usuario, string comentarios)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_perfil", SqlDbType = SqlDbType.Int, Value = pid_perfil });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario", SqlDbType = SqlDbType.Int, Value = usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcomentarios", SqlDbType = SqlDbType.Int, Value = comentarios });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_borrar_perfiles", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_catalogo_perfiles(int pid_perfil)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_perfil", SqlDbType = SqlDbType.Int, Value = pid_perfil });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_catalogo_perfiles", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_widgets_perfiles(int pid_perfil)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_perfil", SqlDbType = SqlDbType.Int, Value = pid_perfil });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_widgets_perfiles", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_usuarios_perfiles(int pid_perfil)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_perfil", SqlDbType = SqlDbType.Int, Value = pid_perfil });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_usuarios_perfiles", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_menus_perfiles(int pid_perfil)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_perfil", SqlDbType = SqlDbType.Int, Value = pid_perfil });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_menus_perfiles", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        
    }
}