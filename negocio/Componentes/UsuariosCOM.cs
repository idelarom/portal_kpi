using datos;
using datos.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;

namespace negocio.Componentes
{
    public class UsuariosCOM
    {
        public string[] Agregar(usuarios entidad)
        {
                string[] return_array = new string[2];
            try
            {
                return_array[0] = "";
                if (GetExists(entidad).Rows.Count == 0)
                {

                    //id_uperfil 1 = administrador 2 = Cliente 3 = PM
                    Model context = new Model();
                    usuarios usuario = new usuarios
                    {
                        id_cliente = entidad.id_cliente,
                        id_uperfil = entidad.id_uperfil,
                        password = entidad.password,
                        usuario = entidad.usuario,
                        fecha_registro = DateTime.Now
                    };
                    context.usuarios.Add(usuario);
                    context.SaveChanges();
                    int id_entity = usuario.id_usuario;
                    return_array[0] = "";
                    return_array[1] = id_entity.ToString();
                }
                return return_array;
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);
                return_array[0] = fullErrorMessage.ToString();
                return_array[1] = 0.ToString();
                return return_array;
            }
        }

        public string Editar(usuarios entidad)
        {
            try
            {
               
                    Model context = new Model();
                    usuarios usuario = context.usuarios
                                    .First(i => i.id_usuario == entidad.id_usuario);
                    usuario.usuario = entidad.usuario;
                    usuario.password = entidad.password;
                    usuario.id_uperfil = entidad.id_uperfil;
                    usuario.fecha_edicion = DateTime.Now;
                    usuario.usuario_edicion = entidad.usuario_edicion;
                    context.SaveChanges();                
                return "";
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                return fullErrorMessage.ToString();
            }
        }
        public string EditarImagen(usuarios entidad)
        {
            try
            {

                Model context = new Model();
                usuarios usuario = context.usuarios
                                .First(i => i.id_usuario == entidad.id_usuario);
                usuario.img_profile = entidad.img_profile;
                usuario.fecha_edicion = DateTime.Now;
                usuario.usuario_edicion = entidad.usuario_edicion;
                context.SaveChanges();
                return "";
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                return fullErrorMessage.ToString();
            }
        }
        public string Borrar(usuarios entidad)
        {
            try
            {
                Model context = new Model();
                usuarios usuario = context.usuarios
                                .First(i => i.id_usuario == entidad.id_usuario);

                usuario.fecha_borrado = DateTime.Now;
                usuario.usuario_borrado = entidad.usuario_borrado;
                usuario.comentarios_borrado = entidad.comentarios_borrado;
                context.SaveChanges();
                return "";
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                return fullErrorMessage.ToString();
            }
        }


        public string AgregarAProyecto(usuarios_proyectos entidad)
        {
            try
            {
                Model context = new Model();
                usuarios_proyectos usuario = new usuarios_proyectos
                {
                    id_proyecto= entidad.id_proyecto,
                    id_usuario = entidad.id_usuario
                };
                context.usuarios_proyectos.Add(usuario);
                context.SaveChanges();
                return "";
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                return fullErrorMessage.ToString();
            }
        }

        public string BorrardeProyecto(usuarios_proyectos entidad)
        {
            try
            {
                Model context = new Model();
                var query = context.usuarios_proyectos
                                .Where(s => s.id_usuario == entidad.id_usuario)
                                .Select(u => new
                                {
                                    u.id_usuario
                                });
                DataTable dt = To.DataTable(query.ToList());
                foreach (DataRow row in dt.Rows)
                {
                    int id_usuario = Convert.ToInt32(row["id_usuario"]);
                    usuarios_proyectos usuario = context.usuarios_proyectos
                                    .First(i => i.id_usuario == id_usuario);

                    context.usuarios_proyectos.Remove(usuario);
                }
                context.SaveChanges();
                return "";
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                return fullErrorMessage.ToString();
            }
        }

        public DataSet Login(string usuario, string password, bool cliente)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario", SqlDbType = SqlDbType.Int, Value = usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcontra", SqlDbType = SqlDbType.Int, Value = password });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcliente", SqlDbType = SqlDbType.Int, Value = cliente });
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_login_clientes", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataTable GetUsersinProyects(usuarios_proyectos entidad)
        {
            DataTable dt = new DataTable();
            try
            {

                Model context = new Model();
                var query = context.usuarios_proyectos
                                .Where(s => s.id_usuario == entidad.id_usuario)
                                .Select(u => new
                                {
                                    u.id_usuario,
                                    u.id_proyecto
                                });
                dt = To.DataTable(query.ToList());
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }
        public DataTable GetID(usuarios entidad)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.usuarios
                                .Where(s => s.id_usuario == entidad.id_usuario)
                                .Select(u => new
                                {
                                    u.id_usuario,
                                    u.id_uperfil,
                                    u.id_cliente,
                                    u.usuario,
                                    u.password,
                                    u.fecha_registro,
                                    u.usuario_borrado,
                                    u.fecha_borrado,
                                    u.comentarios_borrado,
                                    u.fecha_edicion,
                                    u.usuario_edicion
                                });
                dt = To.DataTable(query.ToList());
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }
        
        public DataTable Get(usuarios entidad)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.usuarios
                                .Where(s => s.usuario.ToUpper() == entidad.usuario.ToUpper() && s.password.ToUpper() == entidad.password.ToUpper()
                                        && s.usuario_borrado == null)
                                .Select(u => new
                                {
                                    u.id_usuario,
                                    u.id_uperfil,
                                    u.id_cliente,
                                    u.usuario,
                                    u.password,
                                    u.fecha_registro,
                                    u.usuario_borrado,
                                    u.fecha_borrado,
                                    u.comentarios_borrado,
                                    u.fecha_edicion,
                                    u.usuario_edicion
                                });
                dt = To.DataTable(query.ToList());
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }
        public DataTable GetExists(usuarios entidad)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.usuarios
                                .Where(s => s.usuario.ToUpper() == entidad.usuario.ToUpper()
                                        && s.usuario_borrado == null)
                                .Select(u => new
                                {
                                    u.id_usuario,
                                    u.id_uperfil,
                                    u.id_cliente,
                                    u.usuario,
                                    u.password,
                                    u.fecha_registro,
                                    u.usuario_borrado,
                                    u.fecha_borrado,
                                    u.comentarios_borrado,
                                    u.fecha_edicion,
                                    u.usuario_edicion
                                });
                dt = To.DataTable(query.ToList());
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        public DataTable GetTiposUsuariosClientes()
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.usuarios_perfiles
                                .Where(s => s.id_uperfil == s.id_uperfil && s.solo_clientes == true)
                                .Select(u => new
                                {
                                    u.id_uperfil,
                                    u.perfil
                                })
                                .OrderBy(u => u.perfil);
                dt = To.DataTable(query.ToList());
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        public Boolean ExistUserCliente(int id_cliente)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.usuarios
                                .Where(s => s.id_cliente == id_cliente)
                                .Select(u => new
                                {
                                    u.id_cliente
                                });
                dt = To.DataTable(query.ToList());
                return dt.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataSet sp_catalogo_administracion_usuarios(int id_cliente,string usuario, bool solo_clientes, bool solo_empleados)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_cliente", SqlDbType = SqlDbType.Int, Value = id_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario", SqlDbType = SqlDbType.Int, Value = usuario }); 
            listparameters.Add(new SqlParameter() { ParameterName = "@psolo_clientes", SqlDbType = SqlDbType.Int, Value = solo_clientes });
            listparameters.Add(new SqlParameter() { ParameterName = "@psolo_empleados", SqlDbType = SqlDbType.Int, Value = solo_empleados });
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_catalogo_administracion_usuarios", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

    }
}