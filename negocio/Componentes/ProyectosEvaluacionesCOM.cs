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
                int id_proyecto_evaluacion_anterior = get_id_proyecto_evaluacion(entidad.id_proyecto);
                proyectos_evaluaciones evaluacion = new proyectos_evaluaciones
                {
                    id_proyecto = entidad.id_proyecto,
                    fecha_evaluacion = entidad.fecha_evaluacion,
                    usuario = entidad.usuario,
                    fecha = DateTime.Now,
                    riesgo_costo=0,
                    riesgo_tiempo=0,

                    p_riesgo_costo=0,
                    p_riesgo_tiempo= 0
                };
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                context.proyectos_evaluaciones.Add(evaluacion);
                context.SaveChanges();

                if (id_proyecto_evaluacion_anterior > 0)
                {
                    proyectos_evaluaciones pevaluacion = context.proyectos_evaluaciones
                              .First(i => i.id_proyecto_evaluacion == id_proyecto_evaluacion_anterior);
                    List<riesgos> riesgos = pevaluacion.riesgos.ToList();
                    foreach (riesgos riesgo in riesgos)
                    {
                        if (riesgo.id_riesgos_estatus == 1 && riesgo.usuario_borrado == null)
                        {
                            riesgos new_riesgo = new riesgos
                            {
                                id_proyecto_evaluacion = evaluacion.id_proyecto_evaluacion,
                                riesgo = riesgo.riesgo,
                                usuario = entidad.usuario,
                                fecha_registro = DateTime.Now,
                                id_riesgos_estatus = 1,
                                id_riesgo_probabilidad = riesgo.id_riesgo_probabilidad,
                                id_riesgo_impacto = riesgo.id_riesgo_impacto,
                                id_riesgo_estrategia = riesgo.id_riesgo_estrategia,
                                usuario_resp = riesgo.usuario_resp
                            };
                            context.riesgos.Add(new_riesgo);
                            context.SaveChanges();
                        }
                    }

                }
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
        /// Eliminar una instancia de evaluaciones
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Eliminar(proyectos_evaluaciones entidad)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                proyectos_evaluaciones evaluacion = context.proyectos_evaluaciones
                                .First(i => i.id_proyecto_evaluacion == entidad.id_proyecto_evaluacion);
                evaluacion.usuario_borrado = entidad.usuario_borrado.ToUpper();
                evaluacion.fecha_borrado = DateTime.Now;
                evaluacion.comentarios_borrado = entidad.comentarios_borrado;
                ICollection<riesgos> riesgos = evaluacion.riesgos;

                //borramos riesgos
                foreach (riesgos riesgo in riesgos)
                {
                    riesgo.usuario_borrado = entidad.usuario_borrado.ToUpper();
                    riesgo.fecha_borrado = DateTime.Now;
                    riesgo.comentarios_borrado = entidad.comentarios_borrado;

                    //borramos actividades
                    ICollection<actividades> actividades = riesgo.actividades;
                    foreach (actividades actividad in actividades)
                    {
                        actividad.usuario_borrado = entidad.usuario_borrado.ToUpper();
                        actividad.fecha_borrado = DateTime.Now;
                        actividad.comentarios_borrado = entidad.comentarios_borrado;

                        //borramos documentos
                        ICollection<documentos> documentos = actividad.documentos;
                        foreach (documentos documento in documentos)
                        {
                            documento.usuario_borrado = entidad.usuario_borrado.ToUpper();
                            documento.fecha_borrado = DateTime.Now;
                            documento.comentarios_borrado = entidad.comentarios_borrado;
                        }
                    }
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


        /// <summary>
        /// Regresa el id de la evaluacion mas reciente
        /// </summary>
        /// <param name="id_proyecto"></param>
        /// <param name="dias"></param>
        /// <returns></returns>
        public int get_id_proyecto_evaluacion(int id_proyecto)
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
                                .OrderBy(u => u.id_proyecto_evaluacion);
                DataTable dt = To.DataTable(query.ToList());
                return dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[dt.Rows.Count -1]["id_proyecto_evaluacion"]):0;
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
    }
}
