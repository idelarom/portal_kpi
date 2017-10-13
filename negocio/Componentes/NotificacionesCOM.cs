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
