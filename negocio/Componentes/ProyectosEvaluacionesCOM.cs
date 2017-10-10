﻿using datos;
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
                    fecha = DateTime.Now,
                    riesgo_costo=0,
                    riesgo_tiempo=0,

                    p_riesgo_costo=0,
                    p_riesgo_tiempo= 0
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
    }
}
