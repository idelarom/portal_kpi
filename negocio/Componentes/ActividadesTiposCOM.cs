using datos;
using datos.NAVISION;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;

namespace negocio.Componentes
{
    public class ActividadesTiposCOM
    {
        public DataTable SelectAll()
        {
            try
            {
                DataTable dt = new DataTable();
                Proyectos_ConnextEntities db = new Proyectos_ConnextEntities();
                var riesgos = (from ta in db.actividades_tipos
                               where (ta.activo)
                               orderby (ta.tipo)
                               select new
                               {
                                   ta.id_actividad_tipo,
                                   ta.tipo
                               });
                dt = To.DataTable(riesgos.ToList());
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
