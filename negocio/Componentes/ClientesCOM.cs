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
    public class ClientesCOM
    {
        public string RelacionarAProyecto(proyectos entidad)
        {
            try
            {
                Model context = new Model();
                proyectos proyecto = context.proyectos
                                .First(i => i.id_proyecto == entidad.id_proyecto);
                proyecto.id_cliente = entidad.id_cliente;
                proyecto.fecha_edicion = DateTime.Now;
                proyecto.usuario_edicion = entidad.usuario_edicion;
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

        public string AgregarContacto(proyectos_clientes_contactos entidad)
        {
            try
            {
                Model context = new Model();
                proyectos_clientes_contactos contacto = new proyectos_clientes_contactos
                {
                    CveContacto = entidad.CveContacto,
                    nombre = entidad.nombre,
                    id_proyecto = entidad.id_proyecto,
                    telefono = entidad.telefono,
                    correo = entidad.correo,
                    usuario = entidad.usuario,
                    fecha = DateTime.Now
                };
                context.proyectos_clientes_contactos.Add(contacto);
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

        public DataTable Get(proyectos_clientes_contactos entidad)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.proyectos_clientes_contactos
                                .Where(s => s.id_proyecto == entidad.id_proyecto && s.CveContacto == (entidad.CveContacto > 0 ? entidad.CveContacto : s.CveContacto)
                                        && s.usuario_borrado == null)
                                .Select(u => new
                                {
                                    u.id_pcontacto,
                                    u.id_proyecto,
                                    u.CveContacto,
                                    u.nombre,
                                    u.correo,
                                    u.telefono,
                                    u.usuario,
                                    u.fecha,
                                    u.usuario_edicion,
                                    u.fecha_edicion,
                                    u.usuario_borrado,
                                    u.fecha_borrado,
                                    u.comentarios_borrado
                                });
                dt = To.DataTable(query.ToList());
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        public DataTable GetAll(proyectos_clientes_contactos entidad)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.proyectos_clientes_contactos
                                .Where(s => s.id_proyecto == entidad.id_proyecto && s.CveContacto == entidad.CveContacto)
                                .Select(u => new
                                {
                                    u.id_pcontacto,
                                    u.id_proyecto,
                                    u.CveContacto,
                                    u.nombre,
                                    u.correo,
                                    u.telefono,
                                    u.usuario,
                                    u.fecha,
                                    u.usuario_edicion,
                                    u.fecha_edicion,
                                    u.usuario_borrado,
                                    u.fecha_borrado,
                                    u.comentarios_borrado
                                });
                dt = To.DataTable(query.ToList());
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        public DataSet ListadoClientes(int idcliente)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            listparameters.Add(new SqlParameter() { ParameterName = "@idcliente", SqlDbType = SqlDbType.Int, Value = idcliente });
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_lista_clientes", listparameters, false, 3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet ListadoContactos(int idcliente, int cvecontacto)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            listparameters.Add(new SqlParameter() { ParameterName = "@idcliente", SqlDbType = SqlDbType.Int, Value = idcliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@cvecontacto", SqlDbType = SqlDbType.Int, Value = cvecontacto });
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_contactos_clientes", listparameters, false, 3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public string Borrar(proyectos_clientes_contactos entidad)
        {
            try
            {
                Model context = new Model();
                proyectos_clientes_contactos tarea = context.proyectos_clientes_contactos
                                .First(i => i.id_proyecto == entidad.id_proyecto
                                 && i.id_pcontacto == entidad.id_pcontacto);
                tarea.fecha_borrado = DateTime.Now;
                tarea.comentarios_borrado = entidad.comentarios_borrado;
                tarea.usuario_borrado = entidad.usuario_borrado;
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

        public Boolean BorrarAll(proyectos_clientes_contactos entidad)
        {
            try
            {
                DataTable dt_tareas = Get(entidad);
                if (dt_tareas.Rows.Count > 0)
                {
                    DataTable dt = dt_tareas.Select("not cvecontacto is null").CopyToDataTable();
                    foreach (DataRow tarea in dt.Rows)
                    {
                        entidad.id_pcontacto = Convert.ToInt32(tarea["id_pcontacto"]);
                        entidad.id_proyecto = Convert.ToInt32(tarea["id_proyecto"]);
                        entidad.comentarios_borrado = "borrado por actualizacion";
                        Borrar(entidad);
                    }
                }
                return true;
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                return false;
            }
        }

        public Boolean Exist(proyectos_clientes_contactos entidad)
        {
            try
            {
                Model context = new Model();
                DataTable dt_tareas = GetAll(entidad);
                bool exists = false;
                if (dt_tareas.Rows.Count > 0)
                {
                    proyectos_clientes_contactos contacto = context.proyectos_clientes_contactos
                                .First(i => i.id_proyecto == entidad.id_proyecto
                                 && i.CveContacto == entidad.CveContacto);
                    contacto.fecha_borrado = null;
                    contacto.comentarios_borrado = null;
                    contacto.usuario_borrado = null;
                    context.SaveChanges();
                    exists = true;
                }
                return exists;
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                return false;
            }
        }
    }
}