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
    public class TareasCOM
    {
        /// <summary>
        /// Agrega una Tarea
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string[] Agregar(proyectos_tareas entidad)
        {
            string[] return_array = new string[2];
            try
            {
                if (entidad.tarea == "")
                {
                    return_array[0] = "Ingrese un nombre de proyecto";
                }
                else
                {
                    proyectos_tareas tarea = new proyectos_tareas
                    {
                        codigo_tarea = entidad.codigo_tarea,
                        tarea = entidad.tarea,
                        id_proyecto = entidad.id_proyecto,
                        nivel_esquema = Convert.ToByte(entidad.nivel_esquema),
                        duración = entidad.duración,
                        actividades_predecesoras = entidad.actividades_predecesoras,
                        recursos = entidad.recursos,
                        avance = Convert.ToByte(entidad.avance),
                        fecha_inicio_str = entidad.fecha_inicio_str,
                        fecha_fin_str = entidad.fecha_fin_str,
                        fecha_fin = entidad.fecha_fin,
                        fecha_inicio = entidad.fecha_inicio,
                        fecha_registro = DateTime.Now,
                        usuario = entidad.usuario
                    };
                    Model context = new Model();
                    context.proyectos_tareas.Add(tarea);
                    context.SaveChanges();
                    int id_entity = tarea.id_tarea;
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

        /// <summary>
        /// Edita una Tarea mediante el codigo de tarea interno(del proyecto)
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Editar(proyectos_tareas entidad)
        {
            try
            {
                Model context = new Model();
                proyectos_tareas tarea = context.proyectos_tareas
                                .First(i => i.id_proyecto == entidad.id_proyecto
                                 && i.id_tarea == entidad.id_tarea);
                tarea.tarea = entidad.tarea;
                tarea.id_proyecto = entidad.id_proyecto;
                tarea.nivel_esquema = Convert.ToByte(entidad.nivel_esquema);
                tarea.duración = entidad.duración;
                tarea.actividades_predecesoras = entidad.actividades_predecesoras;
                tarea.recursos = entidad.recursos;
                tarea.avance = Convert.ToByte(entidad.avance);
                tarea.fecha_inicio_str = entidad.fecha_inicio_str;
                tarea.fecha_fin_str = entidad.fecha_fin_str;
                tarea.fecha_edicion = DateTime.Now;
                tarea.fecha_fin = entidad.fecha_fin;
                tarea.fecha_inicio = entidad.fecha_inicio;
                tarea.fecha_borrado = null;
                tarea.usuario_borrado = null;
                tarea.comentarios_borrado = null;
                tarea.usuario_edicion = entidad.usuario_edicion;
                context.SaveChanges();
                int id_entity = tarea.id_tarea;
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


        public string EditarCodigo(proyectos_tareas entidad)
        {
            try
            {
                Model context = new Model();
                proyectos_tareas tarea = context.proyectos_tareas
                                .First(i => i.id_proyecto == entidad.id_proyecto
                                 && i.codigo_tarea.Trim().ToUpper() == entidad.codigo_tarea.Trim().ToUpper());
                tarea.tarea = entidad.tarea;
                tarea.id_proyecto = entidad.id_proyecto;
                tarea.nivel_esquema = Convert.ToByte(entidad.nivel_esquema);
                tarea.duración = entidad.duración;
                tarea.actividades_predecesoras = entidad.actividades_predecesoras;
                tarea.recursos = entidad.recursos;
                tarea.avance = Convert.ToByte(entidad.avance);
                tarea.fecha_inicio_str = entidad.fecha_inicio_str;
                tarea.fecha_fin_str = entidad.fecha_fin_str;
                tarea.fecha_edicion = DateTime.Now;
                tarea.fecha_fin = entidad.fecha_fin;
                tarea.fecha_inicio = entidad.fecha_inicio;
                tarea.fecha_borrado = null;
                tarea.usuario_borrado = null;
                tarea.comentarios_borrado = null;
                tarea.usuario_edicion = entidad.usuario_edicion;
                context.SaveChanges();
                int id_entity = tarea.id_tarea;
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

        /// <summary>
        /// Devuelve un valor booleano si existe una tarea borrado con id interno(del proyecto)
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public bool ExistDelete(proyectos_tareas entidad)
        {
            try
            {
                Model context = new Model();
                bool exist = context.proyectos_tareas
                                .Any(i => i.id_proyecto == entidad.id_proyecto
                                 && i.usuario_borrado != null
                                 && i.codigo_tarea == entidad.codigo_tarea);
                return exist;
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

        /// <summary>
        /// Devuelve un valor booleano si existe una tarea activa por id interno(del proyecto)
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public bool Exist(proyectos_tareas entidad)
        {
            try
            {
                Model context = new Model();
                bool exist = context.proyectos_tareas
                                .Any(i => i.id_proyecto == entidad.id_proyecto
                                 && i.usuario_borrado == null
                                 && i.codigo_tarea == entidad.codigo_tarea);
                return exist;
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

        /// <summary>
        /// Borra una tarea por id
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Borrar(proyectos_tareas entidad)
        {
            try
            {
                Model context = new Model();
                proyectos_tareas tarea = context.proyectos_tareas
                                .First(i => i.id_proyecto == entidad.id_proyecto
                                 && i.id_tarea == entidad.id_tarea);
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

        /// <summary>
        /// Borra todas las tareas
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public Boolean BorrarAll(proyectos_tareas entidad)
        {
            try
            {
                DataTable dt_tareas = GetAll(entidad.id_proyecto);
                foreach (DataRow tarea in dt_tareas.Rows)
                {
                    entidad.id_tarea = Convert.ToInt32(tarea["id_tarea"]);
                    entidad.id_proyecto = Convert.ToInt32(tarea["id_proyecto"]);
                    entidad.comentarios_borrado = "borrado por actualizacion";
                    Borrar(entidad);
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

        /// <summary>
        /// Devuelve un DatatTable con las tareas
        /// </summary>
        /// <param name="id_proyect"></param>
        /// <returns></returns>
        public DataTable GetAll(int id_proyect)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.proyectos_tareas
                                .Where(s => s.id_proyecto == id_proyect && s.usuario_borrado == null)
                                .Select(u => new
                                {
                                    u.id_tarea,
                                    u.codigo_tarea,
                                    u.id_proyecto,
                                    u.id_tarea_padre,
                                    u.tarea,
                                    u.duración,
                                    u.avance,
                                    u.fecha_inicio_str,
                                    u.fecha_fin_str,
                                    u.fecha_inicio,
                                    u.fecha_fin,
                                    u.recursos,
                                    u.actividades_predecesoras,
                                    u.nivel_esquema,
                                    u.fecha_registro,
                                    u.usuario,
                                    u.usuario_edicion,
                                    u.fecha_edicion,
                                    u.usuario_borrado,
                                    u.fecha_borrado,
                                    u.comentarios_borrado
                                });
                dt = To.DataTable(query.ToList());
                dt.Columns.Add("tarea_corta");
                foreach (DataRow row in dt.Rows)
                {
                    row["tarea_corta"] = row["tarea"].ToString().Length > 35 ? row["tarea"].ToString().Substring(0, 35) + "..." : row["tarea"].ToString();
                }
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        /// <summary>
        /// Devuelve un DatatTable con  las tareas filtradas por id_tarea
        /// </summary>
        /// <param name="id_proyect"></param>
        /// <returns></returns>
        public DataTable Get(proyectos_tareas entidad)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.proyectos_tareas
                                .Where(s => s.id_proyecto == entidad.id_proyecto && s.id_tarea == entidad.id_tarea && s.usuario_borrado == null)
                                .Select(u => new
                                {
                                    u.id_tarea,
                                    u.codigo_tarea,
                                    u.id_proyecto,
                                    u.id_tarea_padre,
                                    u.tarea,
                                    u.duración,
                                    u.avance,
                                    u.fecha_inicio_str,
                                    u.fecha_fin_str,
                                    u.fecha_inicio,
                                    u.fecha_fin,
                                    u.recursos,
                                    u.actividades_predecesoras,
                                    u.nivel_esquema,
                                    u.fecha_registro,
                                    u.usuario,
                                    u.usuario_edicion,
                                    u.fecha_edicion,
                                    u.usuario_borrado,
                                    u.fecha_borrado,
                                    u.comentarios_borrado,
                                });
                dt = To.DataTable(query.ToList());
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        /// <summary>
        /// Devuelve una DataTable con las tareas principales de 2do nivel
        /// </summary>
        /// <param name="id_proyecto"></param>
        /// <returns></returns>
        public DataTable GetTaresPrincipales(int id_proyect)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.proyectos_tareas
                                .Where(s => s.id_proyecto == id_proyect && s.nivel_esquema == 2)
                                .Select(u => new { u.id_tarea, u.id_tarea_padre, u.tarea, inicio = u.fecha_inicio_str, fin = u.fecha_fin_str, u.avance });
                dt = To.DataTable(query.ToList());
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        /// <summary>
        /// Actualiza el campo tareapadre de la tabla tareas
        /// </summary>
        /// <param name="id_proyecto"></param>
        /// <returns></returns>
        public DataSet sp_genera_tareas_padres(int id_proyecto)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_proyecto", SqlDbType = SqlDbType.Int, Value = id_proyecto });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_genera_tareas_padres", listparameters, true, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}