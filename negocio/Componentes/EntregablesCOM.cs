using datos.Modelos;
using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;

namespace negocio.Componentes
{
    public class EntregablesCOM
    {
        /// <summary>
        /// Agrega una nuevo Entregable
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string[] Agregar(proyectos_entregables entidad)
        {
            string[] return_array = new string[2];
            try
            {
                proyectos_entregables entregable = new proyectos_entregables
                {
                    id_proyecto = entidad.id_proyecto,
                    entregable = entidad.entregable,
                    fecha = entidad.fecha,
                    avance = Convert.ToByte(entidad.avance),
                    fecha_registro = DateTime.Now,
                    usuario = entidad.usuario
                };
                Model context = new Model();
                context.proyectos_entregables.Add(entregable);
                context.SaveChanges();
                int id_entity = entregable.id_entregable;
                return_array[0] = "";
                return_array[1] = id_entity.ToString();
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
        /// Edita un entregable
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Editar(proyectos_entregables entidad)
        {
            try
            {
                Model context = new Model();
                proyectos_entregables entregable = context.proyectos_entregables
                                .First(i => i.id_entregable == entidad.id_entregable);
                entregable.entregable = entidad.entregable;
                entregable.fecha = entidad.fecha;
                entregable.avance = Convert.ToByte(entidad.avance);
                entregable.fecha_edicion = DateTime.Now;
                entregable.usuario_edicion = entidad.usuario_edicion;
                entregable.usuario_borrado = null;
                entregable.fecha_borrado = null;
                entregable.comentarios_borrado = null;
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
        /// Borra un entregable
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Borrar(proyectos_entregables entidad)
        {
            try
            {
                Model context = new Model();
                proyectos_entregables entregable = context.proyectos_entregables
                                .First(i => i.id_entregable == entidad.id_entregable);
                entregable.fecha_borrado = DateTime.Now;
                entregable.usuario_borrado = entidad.usuario_borrado;
                entregable.comentarios_borrado = entidad.comentarios_borrado;
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
        /// Borra todas las minutas
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public Boolean BorrarAll(proyectos_entregables entidad)
        {
            try
            {
                DataTable dt_tareas = GetAll(entidad.id_proyecto);
                foreach (DataRow tarea in dt_tareas.Rows)
                {
                    entidad.id_entregable = Convert.ToInt32(tarea["id_entregable"]);
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
        /// Devuelve un DatatTable con los entregables
        /// </summary>
        /// <param name="id_proyect"></param>
        /// <returns></returns>
        public DataTable GetAll(int id_proyect)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.proyectos_entregables
                                .Where(s => s.id_proyecto == id_proyect && s.usuario_borrado == null)
                                .Select(u => new
                                {
                                    u.id_entregable,
                                    u.id_proyecto,
                                    entregable_name = u.id_entregable.ToString() + "-" + u.entregable,
                                    u.entregable,
                                    u.fecha,
                                    u.avance,
                                    u.fecha_registro,
                                    u.usuario,
                                    u.usuario_edicion,
                                    u.fecha_edicion,
                                    u.usuario_borrado,
                                    u.comentarios_borrado,
                                    u.fecha_borrado
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
        /// Devuelve un DatatTable con un entregable
        /// </summary>
        /// <param name="id_proyect"></param>
        /// <returns></returns>
        public DataTable Get(int id_entregable)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.proyectos_entregables
                                .Where(s => s.id_entregable == id_entregable && s.usuario_borrado == null)
                                .Select(u => new
                                {
                                    u.id_entregable,
                                    u.id_proyecto,
                                    entregable_name = u.id_entregable.ToString() + "-" + u.entregable,
                                    u.entregable,
                                    u.fecha,
                                    u.avance,
                                    u.fecha_registro,
                                    u.usuario,
                                    u.usuario_edicion,
                                    u.fecha_edicion,
                                    u.usuario_borrado,
                                    u.comentarios_borrado,
                                    u.fecha_borrado
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
        /// Devuelve un valor booleano si existe una tarea activa por id interno(del proyecto)
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public bool Exist(proyectos_entregables entidad)
        {
            try
            {
                Model context = new Model();
                bool exist = context.proyectos_entregables
                                .Any(i => i.id_proyecto == entidad.id_proyecto
                                 && i.usuario_borrado == null
                                 && i.entregable == entidad.entregable);
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
    }
}