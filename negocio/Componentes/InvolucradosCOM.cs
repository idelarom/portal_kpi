using datos;
using datos.Modelos;
using datos.NAVISION;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;

namespace negocio.Componentes
{
    public class InvolucradosCOM
    {
        public string AgregarPM(proyectos_empleados entidad)
        {
            try
            {
                string vmensaje = "";
                Model context = new Model();
                if (!ExistPM(entidad))
                {
                    proyectos_empleados empleado = new proyectos_empleados
                    {
                        id_proyecto = entidad.id_proyecto,
                        no_ = entidad.no_,
                        creador = entidad.creador,
                        usuario = entidad.usuario,
                        fecha = DateTime.Now
                    };
                    context.proyectos_empleados.Add(empleado);
                    usuarios entidadus = new usuarios();
                    entidadus.id_cliente = null;
                    entidadus.id_uperfil = 3;
                    EmpleadosCOM empleados_com = new EmpleadosCOM();
                    Employee enti = new Employee();
                    DataTable dt_empleados = empleados_com.Get(enti);
                    int no_ = entidad.no_;
                    if (dt_empleados.Select("no_ = " + no_.ToString().Trim() + "").Length > 0)
                    {

                        DataTable dt_filter = dt_empleados.Select("no_ = " + no_.ToString().Trim() + "").CopyToDataTable();
                        if (dt_filter.Rows.Count > 0)
                        {
                            entidadus.usuario = dt_filter.Rows[0]["Usuario_Red"].ToString().ToUpper();
                            entidadus.password = "";
                            UsuariosCOM pusuarios = new UsuariosCOM();
                            vmensaje = entidadus.usuario == "" ? "El empleado no cuenta con usuario de red." : pusuarios.Agregar(entidadus)[0];
                        }
                    }
                    else
                    {
                        vmensaje = "No se encontraron los datos completos del empleado. Favor de verificar con el departamento correspondiente, que la información del empleado este completa.";
                    }


                    if (vmensaje == "")
                    {
                        context.SaveChanges();
                    }
                }
                return vmensaje;
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

        public string Agregar(proyectos_involucrados entidad)
        {
            try
            {
                Model context = new Model();
                proyectos_involucrados involucrado = new proyectos_involucrados
                {
                    id_proyecto = entidad.id_proyecto,
                    id_rol = entidad.id_rol,
                    nombre = entidad.nombre,
                    telefono = entidad.telefono,
                    celular=entidad.celular,
                    no_empleado = entidad.no_empleado,
                    correo = entidad.correo,
                    usuario = entidad.usuario,
                    fecha_registro = DateTime.Now
                };
                context.proyectos_involucrados.Add(involucrado);
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

        public string Editar(proyectos_involucrados entidad)
        {
            try
            {
                Model context = new Model();
                proyectos_involucrados involucrado = context.proyectos_involucrados
                                .First(i => i.id_pinvolucrado == entidad.id_pinvolucrado);
                involucrado.nombre = entidad.nombre;
                involucrado.id_rol = entidad.id_rol;
                involucrado.telefono = entidad.telefono;
                involucrado.no_empleado = entidad.no_empleado;
                involucrado.correo = entidad.correo;
                involucrado.celular = entidad.celular;
                involucrado.usuario_edicion = entidad.usuario_edicion;
                involucrado.fecha_edicion = DateTime.Now;
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

        public string Borrar(proyectos_involucrados entidad)
        {
            try
            {
                Model context = new Model();
                proyectos_involucrados involucrado = context.proyectos_involucrados
                                .First(i => i.id_pinvolucrado == entidad.id_pinvolucrado);
                involucrado.id_pinvolucrado = entidad.id_pinvolucrado;
                involucrado.usuario_borrado = entidad.usuario_borrado;
                involucrado.fecha_borrado = DateTime.Now;
                involucrado.comentarios_borrado = entidad.comentarios_borrado;
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

        public string BorrarPM(proyectos_empleados entidad)
        {
            try
            {
                Model context = new Model();
                proyectos_empleados involucrado = context.proyectos_empleados
                                .First(i => i.id_pempleado == entidad.id_pempleado);
                involucrado.usuario_borrado = entidad.usuario_borrado;
                involucrado.fecha_borrado = DateTime.Now;
                involucrado.comentarios_borrado = entidad.comentarios_borrado;
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

        public Boolean BorrarPMAll(proyectos_empleados entidad)
        {
            try
            {
                DataTable dt_tareas = GetPMAll(entidad.id_proyecto);
                foreach (DataRow tarea in dt_tareas.Rows)
                {
                    entidad.id_pempleado = Convert.ToInt32(tarea["id_pempleado"]);
                    entidad.id_proyecto = Convert.ToInt32(tarea["id_proyecto"]);
                    entidad.comentarios_borrado = "borrado por actualizacion";
                    BorrarPM(entidad);
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

        public DataTable GetPMAll(int id_proyect)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.proyectos_empleados
                                .Where(s => s.id_proyecto == id_proyect && s.usuario_borrado == null)
                                .Select(u => new
                                {
                                    u.id_pempleado,
                                    u.id_proyecto,
                                    u.no_,
                                    u.creador,
                                    u.usuario,
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

        public DataTable GetPM(proyectos_empleados entidad)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.proyectos_empleados
                                .Where(s => s.id_proyecto == entidad.id_proyecto && s.no_ == entidad.no_)
                                .Select(u => new
                                {
                                    u.id_pempleado,
                                    u.id_proyecto,
                                    u.no_,
                                    u.creador,
                                    u.usuario,
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

        public Boolean ExistPM(proyectos_empleados entidad)
        {
            try
            {
                Model context = new Model();
                DataTable dt_tareas = GetPM(entidad);
                bool exists = false;
                ProyectosCOM proyectos = new ProyectosCOM();
                DataTable ListadoEmpleadoProyecto = proyectos.ListadoEmpleadoProyecto(entidad.id_proyecto).Tables[0];
                DataTable dt_filter = ListadoEmpleadoProyecto.Select("no_ = "+entidad.no_+"").CopyToDataTable().Rows.Count > 0? 
                                                                                ListadoEmpleadoProyecto.Select("no_ = " + entidad.no_ + "").CopyToDataTable()
                                                                                : new DataTable();
                if (dt_tareas.Rows.Count > 0)
                {
                    proyectos_empleados contacto = context.proyectos_empleados
                                .First(i => i.id_proyecto == entidad.id_proyecto
                                 && i.no_ == entidad.no_);
                    contacto.fecha_borrado = null;
                    contacto.comentarios_borrado = null;
                    contacto.usuario_borrado = null;
                    context.SaveChanges();
                    exists = true;
                }
                else if (dt_filter.Rows.Count > 0)
                {
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

        public DataTable Get(proyectos_involucrados entidad)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.proyectos_involucrados
                                .Where(s => s.id_pinvolucrado == entidad.id_pinvolucrado & s.usuario_borrado == null)
                                .Select(u => new
                                {
                                    u.id_pinvolucrado,
                                    u.id_proyecto,
                                    u.celular,
                                    u.id_rol,
                                    u.nombre,
                                    u.no_empleado,
                                    u.telefono,
                                    u.correo,
                                    u.usuario,
                                    u.fecha_registro,
                                    u.usuario_edicion,
                                    u.fecha_edicion,
                                    u.usuario_borrado,
                                    u.fecha_borrado,
                                    u.comentarios_borrado
                                });
                dt = To.DataTable(query.ToList());
                dt.Columns.Add("rol");
                foreach (DataRow row in dt.Rows)
                {
                    roles_proyecto entidadrol = new roles_proyecto();
                    entidadrol.id_rol = Convert.ToInt32(row["id_rol"]);
                    RolesCOM roles = new RolesCOM();
                    DataTable dt_roles = roles.Get(entidadrol);
                    if (dt_roles.Rows.Count > 0)
                    {
                        row["rol"] = dt_roles.Rows[0]["rol"].ToString();
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        public DataSet sp_arbol_involucrados(int id_proyecto, bool empleados)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_proyecto", SqlDbType = SqlDbType.Int, Value = id_proyecto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pempleados", SqlDbType = SqlDbType.Bit, Value = empleados });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_arbol_involucrados", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_arbol_involucradosall(int id_proyecto, bool empleados)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_proyecto", SqlDbType = SqlDbType.Int, Value = id_proyecto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pempleados", SqlDbType = SqlDbType.Bit, Value = empleados });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptodo", SqlDbType = SqlDbType.Bit, Value = true });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_arbol_involucrados", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}