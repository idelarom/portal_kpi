using datos.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;

namespace negocio.Componentes
{
    public class RecordatoriosCOM
    {
        /// <summary>
        /// Agrega un nuevo recordatorio
        /// </summary>
        /// <param name="entidad"></param>
        /// <param name="usuarios_adicionales"></param>
        /// <returns></returns>
        public string Agregar(recordatorios entidad, DataTable usuarios_adicionales)
        {
            try
            {
                recordatorios recordatorio = new recordatorios
                {
                    titulo = entidad.titulo,
                    descripcion = entidad.descripcion,
                    fecha = entidad.fecha,
                    activo = true,
                    usuario = entidad.usuario,
                    usuario_creacion = entidad.usuario_creacion,
                    fecha_creacion = DateTime.Now
                };
                Model context = new Model();
                context.recordatorios.Add(recordatorio);
                int id_entity = recordatorio.id_recordatorio;
                foreach (DataRow row in usuarios_adicionales.Rows)
                {
                    recordatorios_usuarios_adicionales usuarios_ad = new recordatorios_usuarios_adicionales
                    {
                        id_recordatorio = id_entity,
                        usuario = row["usuario"].ToString(),
                        activo = true
                    };
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

        /// <summary>
        /// Edita un recordatorio
        /// </summary>
        /// <param name="entidad"></param>
        /// <param name="usuarios_adicionales"></param>
        /// <returns></returns>
        public string Editar(recordatorios entidad, DataTable usuarios_adicionales)
        {
            try
            {
                Model context = new Model();
                recordatorios recordatorio = context.recordatorios
                                .First(i => i.id_recordatorio == entidad.id_recordatorio);
                recordatorio.titulo = entidad.titulo;
                recordatorio.descripcion = entidad.descripcion;
                recordatorio.fecha = entidad.fecha;
                ICollection<recordatorios_usuarios_adicionales> lstusuarios_ad = recordatorio.recordatorios_usuarios_adicionales;
                foreach (recordatorios_usuarios_adicionales usuario_adicional in lstusuarios_ad)
                {
                    context.recordatorios_usuarios_adicionales.Remove(usuario_adicional);
                }
                foreach (DataRow row in usuarios_adicionales.Rows)
                {
                    recordatorios_usuarios_adicionales usuarios_ad = new recordatorios_usuarios_adicionales
                    {
                        id_recordatorio = entidad.id_recordatorio,
                        usuario = row["usuario"].ToString(),
                        activo = true
                    };
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

        /// <summary>
        /// Elimina un recordatorio
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Eliminar(recordatorios entidad)
        {
            try
            {
                Model context = new Model();
                recordatorios recordatorio = context.recordatorios
                                .First(i => i.id_recordatorio == entidad.id_recordatorio);
                recordatorio.activo = false;
                recordatorio.fecha_borrado = DateTime.Now;
                recordatorio.comentarios_borrado = entidad.comentarios_borrado;
                ICollection<recordatorios_usuarios_adicionales> lstusuarios_ad = recordatorio.recordatorios_usuarios_adicionales;
                foreach (recordatorios_usuarios_adicionales usuario_adicional in lstusuarios_ad)
                {
                    usuario_adicional.activo = false;
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

        /// <summary>
        /// Elimina un usuario ligado a un recordatorio
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string EliminarUsuarioAdicional(recordatorios_usuarios_adicionales entidad)
        {
            try
            {
                Model context = new Model();
                recordatorios_usuarios_adicionales recordatorio = context.recordatorios_usuarios_adicionales
                                .First(i => i.usuario == entidad.usuario && i.id_recordatorio == entidad.id_recordatorio);
                recordatorio.activo = false;
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

        public DataTable Get(string usuario)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.recordatorios
                                .Where(s => s.usuario == usuario && s.activo)
                                .Select(u => new
                                {
                                    u.id_recordatorio,
                                    u.titulo,
                                    u.descripcion,
                                    u.fecha
                                })
                                .OrderBy(u => u.fecha);
                dt = To.DataTable(query.ToList());
                return dt;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                          .SelectMany(x => x.ValidationErrors)
                          .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                return dt;
            }
        }

        public DataTable GetUsuariosAdicionales(int id_recordatorio)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                recordatorios recordatorio = context.recordatorios
                                .First(i => i.id_recordatorio == id_recordatorio);
                ICollection<recordatorios_usuarios_adicionales> lstusuarios_ad = recordatorio.recordatorios_usuarios_adicionales;
                dt = To.DataTable(lstusuarios_ad.ToList());
                return dt;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                          .SelectMany(x => x.ValidationErrors)
                          .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                return dt;
            }
        }

    }
}