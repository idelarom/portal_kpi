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
    public class ActividadesCOM
    {
        public int Agregar(actividades entidad, List<documentos> lstdocumento)
        {
            try
            {
                string mess = "";
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                actividades actividad = new actividades
                {
                    id_proyecto = entidad.id_proyecto,
                    id_riesgo = entidad.id_riesgo,
                    nombre = entidad.nombre,
                    id_actividad_tipo = entidad.id_actividad_tipo,
                    empleado_resp = entidad.empleado_resp,
                    usuario_resp = entidad.usuario_resp,
                    fecha_ejecucion = entidad.fecha_ejecucion,
                    fecha_asignacion = entidad.fecha_asignacion,
                    usuario = entidad.usuario,
                    fecha_registro = DateTime.Now,
                    resultado = entidad.resultado,
                    recomendada = entidad.recomendada,
                    terminada=false
                };
                context.actividades.Add(actividad);
                context.SaveChanges();
                int id_actividad = actividad.id_actividad;
                foreach (documentos documento in lstdocumento)
                {
                    if (documento.id_actividad == entidad.id_actividad)
                    {
                        documento.id_documento_tipo = 1;
                        documento.id_actividad = id_actividad;
                        context.documentos.Add(documento);
                    }
                }
                context.SaveChanges();
                return id_actividad;
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

        public string GuardarResultado(actividades entidad, List<documentos> lstdocumento)
        {
            try
            {
                string mess = "";
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                actividades actividad = context.actividades
                              .First(i => i.id_actividad == entidad.id_actividad);
                actividad.fecha_ejecucion = DateTime.Now;
                actividad.fecha_edicion = DateTime.Now;
                actividad.usuario_edicion = entidad.usuario_edicion;
                actividad.resultado = entidad.resultado;
                actividad.recomendada = entidad.recomendada;
                actividad.terminada = true;
                int id_actividad = actividad.id_actividad;
                foreach (documentos documento in lstdocumento)
                {
                    if (documento.id_actividad == entidad.id_actividad)
                    {
                        documento.id_documento_tipo = 1;
                        documento.id_actividad = id_actividad;
                        context.documentos.Add(documento);
                    }
                }
                context.SaveChanges();
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
        public DataSet actividades_riesgo(int id_riesgo)
        {
            try
            {
                DataSet ds = new DataSet();
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                DataTable dt = new DataTable();
                Proyectos_ConnextEntities db = new Proyectos_ConnextEntities();
                var actividades = (from a in db.actividades
                                   where(a.usuario_borrado == null && a.id_riesgo == id_riesgo)
                                   select new {
                                       a.id_actividad,
                                       a.id_proyecto,
                                       a.id_riesgo,
                                       a.nombre,
                                       a.usuario_resp,
                                       a.fecha_ejecucion,
                                       a.fecha_asignacion,
                                       a.empleado_resp,
                                       a.recomendada,
                                       a.resultado,
                                       a.id_actividad_tipo,
                                       a.terminada
                                   }).ToArray();
                dt = To.DataTable(actividades.ToList());
                if (dt.Rows.Count > 0)
                {
                    ds.Tables.Add(dt);

                    var documentos = (from d in db.documentos
                                      join a in db.actividades on d.id_actividad equals a.id_actividad
                                      where (d.usuario_borrado == null && a.id_riesgo== id_riesgo)
                                      select new
                                      {
                                          d.id_documento,
                                          d.nombre,
                                          d.extension,
                                          d.path,
                                          d.publico,
                                          d.tamaño,
                                          d.contentType,
                                          d.id_actividad,
                                          d.id_proyecto
                                      }).ToArray();
                    DataTable dt_doc = To.DataTable(documentos.ToList());
                    ds.Tables.Add(dt_doc);
                    return ds;
                }
                else {
                    return null;
                }
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

        public DataTable actividades_tecnologia(int id_tecnologia)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                DataTable dt = new DataTable();
                Proyectos_ConnextEntities db = new Proyectos_ConnextEntities();
                var actividades = (from a in db.actividades
                                   join p in db.proyectos on a.id_proyecto equals p.id_proyecto
                                   join ta in db.actividades_tipos on a.id_actividad_tipo equals ta.id_actividad_tipo
                                   where (a.usuario_borrado == null && p.id_proyecto_tecnologia == id_tecnologia
                                   && p.usuario_borrado == null)
                                   select new
                                   {
                                       a.id_actividad,
                                       a.id_proyecto,
                                       a.id_riesgo,
                                       a.nombre,
                                       a.usuario_resp,
                                       a.fecha_ejecucion,
                                       a.fecha_asignacion,
                                       a.empleado_resp,
                                       a.recomendada,
                                       a.resultado,
                                       a.id_actividad_tipo,
                                       ta.tipo,
                                       a.terminada
                                   });
                dt = To.DataTable(actividades.ToList());
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

        public bool Exist(int id_actividad, int id_riesgo)
        {
            DataTable dt = new DataTable();
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                var query = context.actividades
                                .Where(s => s.id_actividad== id_actividad && s.id_riesgo == id_riesgo)
                                .Select(u => new
                                {
                                    u.id_actividad
                                })
                                .OrderBy(u => u.id_actividad);
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


        /// <summary>
        /// Elimina una instancia de actividades
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Eliminar(int id_actividad, string usuario)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                actividades actividad = context.actividades
                                .First(i => i.id_actividad == id_actividad);
                actividad.usuario_borrado = usuario;
                actividad.fecha_borrado = DateTime.Now;
                actividad.comentarios_borrado = "Borrado por usuario:" + usuario;
                ICollection<documentos> documentos_por_borrar = actividad.documentos;
                foreach (documentos documento in documentos_por_borrar)
                {
                    documento.usuario_borrado = usuario;
                    documento.fecha_borrado = DateTime.Now;
                    documento.comentarios_borrado = "Borrado por usuario:" + usuario;
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

        public actividades actividad(int id_actividad)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                actividades actividad = context.actividades
                                .First(i => i.id_actividad == id_actividad);
                return actividad;
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
