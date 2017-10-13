using datos.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace negocio.Componentes
{
    public class NotificacionesCOM
    {
        public string MarcarLeido(string usuario)
        {
            try
            {
                Model context = new Model();
                //notificaciones notificacion = context.notificaciones
                //                .First(i => i.id_notificacion == id_notificacion);
                //notificacion.leido = true;
                //notificacion.fecha_leido = DateTime.Now;
                IQueryable<notificaciones> notificaciones= context.notificaciones
                                .Where(s => s.usuario.ToUpper() == usuario.ToUpper());
                foreach (notificaciones notificacion in notificaciones)
                {
                    notificacion.leido = true;
                    notificacion.fecha_leido = DateTime.Now;
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
        public DataTable notificaciones(string usuario)
        {
            try
            {
                Model db = new Model();
                var notificaciones = (from a in db.notificaciones
                                      where (a.usuario.ToUpper() == usuario.ToUpper() && !a.leido)
                                      orderby (a.fecha_registro)
                                      select new
                                      {
                                          a.id_notificacion,
                                          a.notificacion,
                                          a.fecha_registro,
                                          a.icono,
                                          a.url
                                      });

                DataTable dt = To.DataTable(notificaciones.ToList());
                return dt;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                return null;
            }
        }
    }
}
