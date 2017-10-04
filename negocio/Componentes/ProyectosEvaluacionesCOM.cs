using datos;
using datos.NAVISION;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;

namespace negocio.Componentes
{
    public class ProyectosEvaluacionesCOM
    {
        /// <summary>
        /// Agrega una instancia de proyectos_evaluaciones
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public int Agregar(proyectos_evaluaciones entidad)
        {
            try
            {
                proyectos_evaluaciones evaluacion = new proyectos_evaluaciones
                {
                    id_proyecto = entidad.id_proyecto,
                    fecha_evaluacion = entidad.fecha_evaluacion,
                    usuario = entidad.usuario,
                    fecha = DateTime.Now
                };
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                context.proyectos_evaluaciones.Add(evaluacion);
                context.SaveChanges();
                return evaluacion.id_proyecto_evaluacion;
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


        /// <summary>
        /// Regresa la fecha de la siguiente evaluacion
        /// </summary>
        /// <param name="id_proyecto"></param>
        /// <param name="dias"></param>
        /// <returns></returns>
        public DateTime fecha_siguiente_Evaluacion(int id_proyecto, int dias)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                var query = context.proyectos_evaluaciones
                                .Where(s => s.usuario_borrado == null && s.id_proyecto == id_proyecto)
                                .Select(u => new
                                {
                                    u.id_proyecto_evaluacion,
                                    u.id_proyecto,
                                    u.fecha_evaluacion
                                })
                                .OrderBy(u => u.fecha_evaluacion);
                DataTable dt = To.DataTable(query.ToList());
                DateTime fecha_ultima_evaluacion = dt.Rows.Count > 0 ? Convert.ToDateTime(dt.Rows[dt.Rows.Count - 1]["fecha_evaluacion"]).AddDays(dias) :
                    DateTime.Now;
                return fecha_ultima_evaluacion;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                return DateTime.Now;
            }
        }
    }
}
