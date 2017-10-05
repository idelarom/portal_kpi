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
        public string Agregar(List<actividades> lstactividades, List<documentos> lstdocumento)
        {
            try
            {
                string mess = "";
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                foreach (actividades entidad in lstactividades)
                {
                    actividades actividad = new actividades
                    {
                        id_proyecto = entidad.id_proyecto,
                        id_riesgo = entidad.id_riesgo,
                        nombre = entidad.nombre,
                        empleado_resp= entidad.empleado_resp,
                        usuario_resp = entidad.usuario_resp,
                        fecha_ejecucion = entidad.fecha_ejecucion,
                        fecha_asignacion = entidad.fecha_asignacion,
                        usuario = entidad.usuario,
                        fecha_registro = DateTime.Now
                    };
                    context.actividades.Add(actividad);
                    int id_actividad = actividad.id_actividad;
                    foreach (documentos documento in lstdocumento)
                    {
                        if (documento.id_actividad == entidad.id_actividad)
                        {
                            documento.id_actividad = id_actividad;
                            context.documentos.Add(documento);
                        }
                    }
                }
                context.SaveChanges();
                return mess;
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
                                       a.empleado_resp
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

       
    }
}
