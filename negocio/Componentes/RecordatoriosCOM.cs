using datos.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Globalization;
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
        public string Agregar(recordatorios entidad, List<recordatorios_usuarios_adicionales> usuarios_adi)
        {
            try
            {
                recordatorios recordatorio = new recordatorios
                {
                    location = entidad.location,
                    key_appointment_exchanged = entidad.key_appointment_exchanged,
                    organizer = entidad.organizer,
                    organizer_address = entidad.organizer_address,
                    fecha_end = entidad.fecha_end,
                    titulo = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(entidad.titulo.ToLower()),
                    descripcion = entidad.descripcion,
                    fecha = entidad.fecha,
                    activo = true,
                    usuario = entidad.usuario.ToUpper(),
                    usuario_creacion = entidad.usuario_creacion.ToUpper(),
                    fecha_creacion = DateTime.Now
                };
                Model context = new Model();
                context.recordatorios.Add(recordatorio);
                int id_entity = recordatorio.id_recordatorio;
                foreach (recordatorios_usuarios_adicionales usu_ad in usuarios_adi)
                {
                    usu_ad.id_recordatorio = id_entity;
                    usu_ad.activo = true;
                    context.recordatorios_usuarios_adicionales.Add(usu_ad);
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
        public string Editar(recordatorios entidad, List<recordatorios_usuarios_adicionales> usuarios_adi)
        {
            try
            {
                Model context = new Model();
                recordatorios recordatorio = context.recordatorios
                                .First(i => i.id_recordatorio == entidad.id_recordatorio);
                recordatorio.titulo = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(entidad.titulo.ToLower());
                recordatorio.descripcion = entidad.descripcion;
                recordatorio.fecha = entidad.fecha;
                recordatorio.location = entidad.location;
                recordatorio.organizer = entidad.organizer;
                recordatorio.organizer_address = entidad.organizer_address;
                recordatorio.fecha_end = entidad.fecha_end;                
                //ICollection<recordatorios_usuarios_adicionales> lstusuarios_ad = recordatorio.recordatorios_usuarios_adicionales;
                //foreach (recordatorios_usuarios_adicionales usuario_adicional in lstusuarios_ad)
                //{
                //    context.recordatorios_usuarios_adicionales.Remove(usuario_adicional);
                //}
                //foreach (recordatorios_usuarios_adicionales usu_ad in usuarios_adi)
                //{
                //    usu_ad.id_recordatorio = entidad.id_recordatorio;
                //    usu_ad.activo = true;
                //    context.recordatorios_usuarios_adicionales.Add(usu_ad);
                //}
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
        public string Descartar(int id_recordatorio,string usuario)
        {
            try
            {
                Model context = new Model();
                recordatorios recordatorio = context.recordatorios
                                .First(i => i.id_recordatorio == id_recordatorio);
                recordatorio.activo = false;
                recordatorio.usuario_borrado = usuario;
                recordatorio.fecha_borrado = DateTime.Now;
                recordatorio.comentarios_borrado = "Descartado por el usuario: "+usuario;
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
        public string Posponer(int id_recordatorio, int minutes)
        {
            try
            {
                Model context = new Model();
                recordatorios recordatorio = context.recordatorios
                                .First(i => i.id_recordatorio == id_recordatorio);
                recordatorio.fecha = recordatorio.fecha.AddMinutes(minutes);
                recordatorio.fecha_end =Convert.ToDateTime(recordatorio.fecha_end).AddMinutes(minutes);
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
                                .First(i => i.nombre == entidad.nombre && i.id_recordatorio == entidad.id_recordatorio);
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

        public DataTable Select(string usuario, DateTime fecha)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var ruleDate = fecha.Date;
                var query = context.recordatorios
                                .Where(s => s.usuario == usuario && s.activo &&
                                s.fecha.Year == ruleDate.Year
                               && s.fecha.Month == ruleDate.Month
                               && s.fecha.Day == ruleDate.Day)
                                .Select(u => new
                                {
                                    key = (u.key_appointment_exchanged == null ?"": u.key_appointment_exchanged),
                                    u.fecha_end,
                                    u.organizer,
                                    u.location,
                                    u.organizer_address,
                                    appointment = u.key_appointment_exchanged != null,
                                    u.id_recordatorio,
                                    u.titulo,
                                    u.descripcion,
                                    descripcion_corta = (u.descripcion.Length > 30 ? u.descripcion.Substring(0, 30) + "..." : u.descripcion),
                                    titulo_corta = (u.titulo.Length > 65 ? u.titulo.Substring(0, 65) + "..." : u.titulo),
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

        public bool ExistAppointment(string usuario, string key, string organizer, string subjetc, DateTime start, DateTime end)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.recordatorios
                                .Where(s => s.usuario == usuario && s.activo &&
                                s.key_appointment_exchanged == key && s.organizer == organizer && s.titulo == subjetc
                                && s.fecha == start && s.fecha_end == end)
                                .Select(u => new
                                {
                                    u.id_recordatorio,
                                    u.titulo,
                                    u.descripcion,
                                    descripcion_corta = (u.descripcion.Length > 30 ? u.descripcion.Substring(0, 30) + "..." : u.descripcion),
                                    titulo_corta = (u.titulo.Length > 65 ? u.titulo.Substring(0, 65) + "..." : u.titulo),
                                    u.fecha
                                })
                                .OrderBy(u => u.fecha);
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

        public DataTable SelectToday(string usuario)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                DateTime fecha_inicio = DateTime.Now.AddMinutes(-15);
                DateTime fecha_fin = DateTime.Now.AddMinutes(15);
                var query = context.recordatorios
                                .Where(s => s.usuario == usuario && s.activo &&
                                s.fecha >= fecha_inicio && s.fecha <= fecha_fin)
                                .Select(u => new
                                {
                                    key = (u.key_appointment_exchanged == null ? "" : u.key_appointment_exchanged),
                                    u.fecha_end,
                                    u.organizer,
                                    u.organizer_address,
                                    u.location,
                                    appointment = u.key_appointment_exchanged != null,
                                    u.id_recordatorio,
                                    u.titulo,
                                    u.descripcion,
                                    descripcion_corta =  (u.descripcion.Length > 30 ? u.descripcion.Substring(0, 30) + "..." : u.descripcion),
                                    titulo_corta = (u.titulo.Length > 65 ? u.titulo.Substring(0, 65) + "..." : u.titulo),
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

        public DataTable Get(string usuario)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.recordatorios
                                .Where(s => s.usuario.ToUpper() == usuario.ToUpper() && s.activo)
                                .Select(u => new
                                {
                                    key = (u.key_appointment_exchanged == null ? "" : u.key_appointment_exchanged),
                                    u.fecha_end,
                                    u.organizer,
                                    u.location,
                                    u.organizer_address,
                                    appointment = u.key_appointment_exchanged != null,
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

        public int GetRecords(string usuario)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.recordatorios
                                .Where(s => s.usuario.ToUpper() == usuario.ToUpper() && s.activo)
                                .Select(u => new
                                {
                                    u.id_recordatorio
                                });
                dt = To.DataTable(query.ToList());
                return dt.Rows.Count;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                          .SelectMany(x => x.ValidationErrors)
                          .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                return 0;
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