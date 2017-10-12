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
    public class RiesgosCOM
    {
        /// <summary>
        /// Agrega una instancia de riesgos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Agregar(riesgos entidad, List<actividades> lst_actividades, List<documentos> lstdocumentos)
        {
            try
            {
                string mess = "";
                riesgos riesgo = new riesgos
                {
                    id_proyecto_evaluacion = entidad.id_proyecto_evaluacion,
                    riesgo = entidad.riesgo,
                    id_riesgos_estatus = entidad.id_riesgos_estatus,
                    id_riesgo_probabilidad = entidad.id_riesgo_probabilidad,
                    porc_probabilidad = entidad.porc_probabilidad,
                    id_riesgo_impacto_costo = entidad.id_riesgo_impacto_costo,
                    porc_impcosto = entidad.porc_impcosto,
                    id_riesgo_impacto_tiempo = entidad.id_riesgo_impacto_tiempo,
                    porc_imptiempo = entidad.porc_imptiempo,
                    riesgo_costo = entidad.riesgo_costo,
                    riesgo_tiempo = entidad.riesgo_tiempo,
                    id_riesgo_estrategia = entidad.id_riesgo_estrategia,
                    usuario= entidad.usuario,
                    fecha_registro= DateTime.Now
                };
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                context.riesgos.Add(riesgo);
                context.SaveChanges();
                int id_riesgo = riesgo.id_riesgo;
                foreach (actividades actividad in lst_actividades)
                {
                    actividad.id_riesgo = id_riesgo;
                }
                foreach (actividades entidad2 in lst_actividades)
                {
                    actividades actividad = new actividades
                    {
                        id_proyecto = entidad2.id_proyecto,
                        id_riesgo = entidad2.id_riesgo,
                        nombre = entidad2.nombre,
                        usuario_resp = entidad2.usuario_resp,
                        fecha_ejecucion = entidad2.fecha_ejecucion,
                        fecha_asignacion = entidad2.fecha_asignacion,
                        usuario = entidad2.usuario,
                        empleado_resp = entidad2.empleado_resp,
                        fecha_registro = DateTime.Now
                    };
                    context.actividades.Add(actividad);
                    context.SaveChanges();
                    int id_actividad = actividad.id_actividad;
                    foreach (documentos documento in lstdocumentos)
                    {
                        if (documento.id_actividad == entidad2.id_actividad)
                        {
                            documento.fecha = DateTime.Now;
                            documento.id_documento_tipo = 1;
                            documento.usuario_edicion = null;
                            documento.id_actividad = id_actividad;
                            context.documentos.Add(documento);
                        }
                    }

                }

                context.SaveChanges();

                EditarRiesgosEvaluacion(riesgo.id_proyecto_evaluacion);
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

        /// <summary>
        /// Edita una instancia de riesgos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Editar(riesgos entidad, List<actividades> lst_actividades, List<documentos> lstdocumentos)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                riesgos riesgo = context.riesgos
                                .First(i => i.id_riesgo == entidad.id_riesgo);
                riesgo.id_proyecto_evaluacion = entidad.id_proyecto_evaluacion;
                riesgo.riesgo = entidad.riesgo;
                riesgo.id_riesgos_estatus = entidad.id_riesgos_estatus;
                riesgo.id_riesgo_probabilidad = entidad.id_riesgo_probabilidad;
                riesgo.porc_probabilidad = entidad.porc_probabilidad;
                riesgo.id_riesgo_impacto_costo = entidad.id_riesgo_impacto_costo;
                riesgo.porc_impcosto = entidad.porc_impcosto;
                riesgo.id_riesgo_impacto_tiempo = entidad.id_riesgo_impacto_tiempo;
                riesgo.porc_imptiempo = entidad.porc_imptiempo;
                riesgo.riesgo_costo = entidad.riesgo_costo;
                riesgo.riesgo_tiempo = entidad.riesgo_tiempo;
                riesgo.id_riesgo_estrategia = entidad.id_riesgo_estrategia;
                riesgo.usuario_edicion = entidad.usuario_edicion;
                riesgo.fecha_edicion = DateTime.Now;

                //borramos todas las actividades y documentos
                ICollection<actividades> actividades_por_borrar = riesgo.actividades;
                foreach (actividades actividad in actividades_por_borrar)
                {
                    actividad.usuario_borrado= entidad.usuario_edicion;
                    actividad.fecha_borrado = DateTime.Now;
                    actividad.comentarios_borrado = "Borrado por actualizacion";

                    ICollection<documentos> documentos_por_borrar = actividad.documentos;
                    foreach (documentos documento in documentos_por_borrar)
                    {
                        documento.usuario_borrado = entidad.usuario_edicion;
                        documento.fecha_borrado = DateTime.Now;
                        documento.comentarios_borrado = "Borrado por actualizacion";
                    }
                }


                //actualizamos lo que venga en la lista
                foreach (actividades entidad2 in lst_actividades)
                {
                    ActividadesCOM actividades = new ActividadesCOM();
                    //si existe actualizamos, si no existe agregamos
                    if (actividades.Exist(entidad2.id_actividad, Convert.ToInt32(entidad2.id_riesgo)))
                    {
                        actividades actividad = context.actividades
                                  .First(i => i.id_actividad == entidad2.id_actividad);
                        actividad.id_proyecto = entidad2.id_proyecto;
                        actividad.id_riesgo = entidad2.id_riesgo;
                        actividad.nombre = entidad2.nombre;
                        actividad.usuario_resp = entidad2.usuario_resp;
                        actividad.fecha_ejecucion = entidad2.fecha_ejecucion;
                        actividad.fecha_asignacion = entidad2.fecha_asignacion;
                        actividad.usuario = entidad2.usuario;
                        actividad.empleado_resp = entidad2.empleado_resp;
                        actividad.fecha_registro = DateTime.Now;
                        actividad.usuario_borrado = null;
                        actividad.fecha_borrado = null;
                        actividad.comentarios_borrado = null;
                        actividad.fecha_edicion = DateTime.Now;
                        actividad.usuario_edicion = entidad.usuario_edicion;
                        foreach (documentos entidad3 in lstdocumentos)
                        {
                            if (entidad3.id_actividad == entidad2.id_actividad)
                            {
                                documentos documento = context.documentos
                                     .First(i => i.id_documento == entidad3.id_documento);
                                documento.fecha = DateTime.Now;
                                documento.usuario_edicion = null;
                                documento.fecha_borrado = null;
                                documento.usuario_borrado = null;
                                documento.comentarios_borrado = null;
                                documento.path = entidad3.path;
                                documento.nombre = entidad3.nombre;
                                documento.contentType = entidad3.contentType;
                                documento.tamaño = entidad3.tamaño;
                                documento.publico = entidad3.publico;
                                documento.extension = entidad3.extension;
                                documento.fecha_edicion = DateTime.Now;
                                documento.usuario_edicion = entidad.usuario_edicion;
                            }
                        }
                    }
                    else {
                        actividades actividad = new actividades
                        {
                            id_proyecto = entidad2.id_proyecto,
                            id_riesgo = entidad2.id_riesgo,
                            nombre = entidad2.nombre,
                            usuario_resp = entidad2.usuario_resp,
                            fecha_ejecucion = entidad2.fecha_ejecucion,
                            fecha_asignacion = entidad2.fecha_asignacion,
                            usuario = entidad2.usuario,
                            empleado_resp = entidad2.empleado_resp,
                            fecha_registro = DateTime.Now
                        };
                        context.actividades.Add(actividad);
                        context.SaveChanges();
                        int id_actividad = actividad.id_actividad;
                        foreach (documentos documento in lstdocumentos)
                        {
                            if (documento.id_actividad == entidad2.id_actividad)
                            {
                                documento.id_documento_tipo = 1;
                                documento.fecha = DateTime.Now;
                                documento.usuario_edicion = null;
                                documento.id_actividad = id_actividad;
                                context.documentos.Add(documento);
                            }
                        }
                    }
                }


                context.SaveChanges();
                EditarRiesgosEvaluacion(riesgo.id_proyecto_evaluacion);
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
        /// Edita la porbabilidad de una instancia de riesgos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string EditarProbabilidad(int id_riesgo, int id_probabilidad, decimal probabilidad, string usuario)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                riesgos riesgo = context.riesgos
                                .First(i => i.id_riesgo == id_riesgo);
                riesgo.id_riesgo_probabilidad = id_probabilidad;
                riesgo.porc_probabilidad = probabilidad;
                riesgo.usuario_edicion = usuario;
                riesgo.fecha_edicion = DateTime.Now;
                riesgo.riesgo_costo = Convert.ToDecimal(((riesgo.porc_probabilidad * riesgo.porc_impcosto) / 100) );
                riesgo.riesgo_tiempo = Convert.ToDecimal(((riesgo.porc_probabilidad * riesgo.porc_imptiempo) / 100) );
                context.SaveChanges();
                EditarRiesgosEvaluacion(riesgo.id_proyecto_evaluacion);
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
        /// Edita el impacto costo de una instancia de riesgos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string EditarImpactoCosto(int id_riesgo, int id_impc, decimal probabilidad, string usuario)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                riesgos riesgo = context.riesgos
                                .First(i => i.id_riesgo == id_riesgo);
                riesgo.id_riesgo_impacto_costo = id_impc;
                riesgo.porc_impcosto = probabilidad;
                riesgo.usuario_edicion = usuario;
                riesgo.fecha_edicion = DateTime.Now;
                riesgo.riesgo_costo = Convert.ToDecimal(((riesgo.porc_probabilidad * riesgo.porc_impcosto) / 100) );
                context.SaveChanges();
                EditarRiesgosEvaluacion(riesgo.id_proyecto_evaluacion);
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
        /// Edita el impacto tiempo de una instancia de riesgos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string EditarImpactoTiempo(int id_riesgo, int id_impc, decimal probabilidad, string usuario)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                riesgos riesgo = context.riesgos
                                .First(i => i.id_riesgo == id_riesgo);
                riesgo.id_riesgo_impacto_tiempo = id_impc;
                riesgo.porc_imptiempo = probabilidad;
                riesgo.usuario_edicion = usuario;
                riesgo.fecha_edicion = DateTime.Now;
                riesgo.riesgo_tiempo = Convert.ToDecimal(((riesgo.porc_probabilidad * riesgo.porc_imptiempo) / 100) );
                context.SaveChanges();
                EditarRiesgosEvaluacion(riesgo.id_proyecto_evaluacion);
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
        /// Edita la estrategia de una instancia de riesgos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string EditarImpactoEstrategia(int id_riesgo, int id_impc,string usuario)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                riesgos riesgo = context.riesgos
                                .First(i => i.id_riesgo == id_riesgo);
                riesgo.id_riesgo_estrategia = id_impc;
                riesgo.usuario_edicion = usuario;
                riesgo.fecha_edicion = DateTime.Now;
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

        public string EditarRiesgosEvaluacion(int id_proyecto_evaluacion)
        {
            try
            {
                Proyectos_ConnextEntities db = new Proyectos_ConnextEntities();
                var riesgos = (from r in db.riesgos
                               where(r.id_proyecto_evaluacion == id_proyecto_evaluacion
                               && r.usuario_borrado== null)
                               select new {
                                   r.riesgo_costo,
                                   r.riesgo_tiempo
                               });
                DataTable dt_values = To.DataTable(riesgos.ToList());
                int total = dt_values.Rows.Count;

                decimal riesgo_costo = 0;
                decimal riesgo_tiempo = 0;
                foreach (DataRow row in dt_values.Rows)
                {
                    riesgo_costo = riesgo_costo + Convert.ToDecimal(row["riesgo_costo"]);
                    riesgo_tiempo = riesgo_tiempo + Convert.ToDecimal(row["riesgo_tiempo"]);
                }

                proyectos_evaluaciones evaluacion = db.proyectos_evaluaciones
                               .First(i => i.id_proyecto_evaluacion == id_proyecto_evaluacion);

                proyectos proyecto = db.proyectos
                               .First(i => i.id_proyecto == evaluacion.id_proyecto);

                evaluacion.riesgo_costo = total == 0?0:
                    Convert.ToDecimal(proyecto.costo_usd * Convert.ToDecimal(riesgo_costo/100));
                evaluacion.riesgo_costo_mn = total == 0 ? 0 :
                    Convert.ToDecimal(proyecto.costo_mn * Convert.ToDecimal(riesgo_costo / 100));

                double diff_days = (Convert.ToDateTime(proyecto.fecha_fin) - Convert.ToDateTime(proyecto.fecha_inicio)).TotalDays;


                evaluacion.riesgo_tiempo = total == 0 ? Convert.ToInt16(0) :
                   Convert.ToInt16(Math.Round(Convert.ToDecimal(Convert.ToDecimal(diff_days) * Convert.ToDecimal(riesgo_tiempo/100)), 0, MidpointRounding.AwayFromZero));

                evaluacion.p_riesgo_costo = Convert.ToDecimal(riesgo_costo / 100);
                evaluacion.p_riesgo_tiempo = Convert.ToDecimal(riesgo_tiempo / 100);

                evaluacion.riesgo_costo = evaluacion.p_riesgo_costo > 0 ? evaluacion.riesgo_costo : 0;
                evaluacion.riesgo_costo_mn = evaluacion.p_riesgo_costo > 0 ? evaluacion.riesgo_costo_mn : 0;
                evaluacion.riesgo_tiempo = evaluacion.p_riesgo_tiempo > 0 ? evaluacion.riesgo_tiempo : 0;
                evaluacion.fecha_edicion = DateTime.Now;

                db.SaveChanges();
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
        /// Elimina una instancia de riesgos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Eliminar(riesgos entidad)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                riesgos riesgo = context.riesgos
                                .First(i => i.id_riesgo == entidad.id_riesgo);
                riesgo.usuario_borrado = entidad.usuario_borrado;
                riesgo.fecha_borrado = DateTime.Now;
                riesgo.comentarios_borrado = "Borrado por usuario:" + entidad.usuario_borrado;

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
        /// Devuelve una instancia de la clase riesgos
        /// </summary>
        /// <param name="id_proyecto_perido"></param>
        /// <returns></returns>
        public DataTable riesgo(int id_riesgo)
        {
            try
            {
                DataTable dt = new DataTable();
                Proyectos_ConnextEntities db = new Proyectos_ConnextEntities();
                var riesgos = (from r in db.riesgos
                              join re in db.riesgos_estatus on r.id_riesgos_estatus equals re.id_riesgos_estatus
                              join rp in db.riesgos_probabilidad on r.id_riesgo_probabilidad equals rp.id_riesgo_probabilidad
                              join ric in db.riesgos_impacto_costo on r.id_riesgo_impacto_costo equals ric.id_riesgo_impacto_costo
                              join rit in db.riesgos_impacto_tiempo on r.id_riesgo_impacto_tiempo equals rit.id_riesgo_impacto_tiempo
                              join rs in db.riesgos_estrategia on r.id_riesgo_estrategia equals rs.id_riesgo_estrategia
                              join pe in db.proyectos_evaluaciones on r.id_proyecto_evaluacion equals pe.id_proyecto_evaluacion
                              join p in db.proyectos on pe.id_proyecto equals p.id_proyecto
                               join pt in db.proyectos_tecnologias on p.id_proyecto_tecnologia equals pt.id_proyecto_tecnologia
                               where (r.id_riesgo == id_riesgo)
                              select new
                              {
                                  pt.id_proyecto_tecnologia,
                                  tecnologia = pt.nombre,
                                  r.id_riesgo,
                                  r.riesgo,
                                  r.id_riesgos_estatus,
                                  re.estatus,
                                  r.id_riesgo_probabilidad,
                                  probabilidad = rp.nombre,
                                  p_probabilidad= rp.porcentaje,
                                  r.id_riesgo_impacto_costo,
                                  impacto_costo = ric.nombre,
                                  p_impacto_costo=ric.porcentaje,
                                  r.id_riesgo_impacto_tiempo,
                                  impacto_tiempo = rit.nombre,
                                  p_impacto_tiempo = rit.porcentaje,
                                  r.id_riesgo_estrategia,
                                  estrategia = rs.nombre,
                                  fecha_evaluacion = pe.fecha_evaluacion,
                                  proyecto = p.proyecto,
                                  r.riesgo_costo,
                                  r.riesgo_tiempo
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

        public Boolean Exists(string riesgo, int id_proyecto_evaluacion)
        {
            try
            {
                DataTable dt = new DataTable();
                Proyectos_ConnextEntities db = new Proyectos_ConnextEntities();
                var riesgos = (from r in db.riesgos
                               where (r.riesgo.ToUpper() == riesgo && r.id_proyecto_evaluacion == id_proyecto_evaluacion
                               && r.usuario_borrado ==  null)
                               select new
                               {
                                   r.id_riesgo
                               });

                dt = To.DataTable(riesgos.ToList());
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
        /// Devuelve un cursor con los riesgos por proyectos
        /// </summary>
        /// <param name="id_proyecto_perido"></param>
        /// <returns></returns>
        public DataTable proyectos_riesgos(int id_proyecto)
        {
            try
            {
                DataTable dt = new DataTable();
                Proyectos_ConnextEntities db = new Proyectos_ConnextEntities();
                var riesgos = (from r in db.riesgos
                               join re in db.riesgos_estatus on r.id_riesgos_estatus equals re.id_riesgos_estatus
                               join rp in db.riesgos_probabilidad on r.id_riesgo_probabilidad equals rp.id_riesgo_probabilidad
                               join ric in db.riesgos_impacto_costo on r.id_riesgo_impacto_costo equals ric.id_riesgo_impacto_costo
                               join rit in db.riesgos_impacto_tiempo on r.id_riesgo_impacto_tiempo equals rit.id_riesgo_impacto_tiempo
                               join rs in db.riesgos_estrategia on r.id_riesgo_estrategia equals rs.id_riesgo_estrategia
                               join pe in db.proyectos_evaluaciones on r.id_proyecto_evaluacion equals pe.id_proyecto_evaluacion
                               join p in db.proyectos on pe.id_proyecto equals p.id_proyecto
                               join pt in db.proyectos_tecnologias on p.id_proyecto_tecnologia equals pt.id_proyecto_tecnologia
                               where (p.id_proyecto == id_proyecto && r.usuario_borrado == null)
                               select new
                               {
                                   pt.id_proyecto_tecnologia,
                                   tecnologia = pt.nombre,
                                   r.id_riesgo,
                                   r.riesgo,
                                   r.id_riesgos_estatus,
                                   re.estatus,
                                   r.id_riesgo_probabilidad,
                                   probabilidad = rp.nombre,
                                   p_probabilidad = rp.porcentaje,
                                   r.id_riesgo_impacto_costo,
                                   impacto_costo = ric.nombre,
                                   p_impacto_costo = ric.porcentaje,
                                   r.id_riesgo_impacto_tiempo,
                                   impacto_tiempo = rit.nombre,
                                   p_impacto_tiempo = rit.porcentaje,
                                   r.id_riesgo_estrategia,
                                   estrategia = rs.nombre,
                                   fecha_evaluacion = pe.fecha_evaluacion,
                                   proyecto = p.proyecto,
                                   r.riesgo_costo,
                                   r.riesgo_tiempo
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

        /// <summary>
        /// Devuelve un cursor con los riesgos por evaluacion
        /// </summary>
        /// <param name="id_proyecto_perido"></param>
        /// <returns></returns>
        public DataTable evaluacion_riesgos(int id_proyecto_Evaluacion)
        {
            try
            {
                DataTable dt = new DataTable();
                Proyectos_ConnextEntities db = new Proyectos_ConnextEntities();
                var riesgos = (from r in db.riesgos
                               join re in db.riesgos_estatus on r.id_riesgos_estatus equals re.id_riesgos_estatus
                               join rp in db.riesgos_probabilidad on r.id_riesgo_probabilidad equals rp.id_riesgo_probabilidad
                               join ric in db.riesgos_impacto_costo on r.id_riesgo_impacto_costo equals ric.id_riesgo_impacto_costo
                               join rit in db.riesgos_impacto_tiempo on r.id_riesgo_impacto_tiempo equals rit.id_riesgo_impacto_tiempo
                               join rs in db.riesgos_estrategia on r.id_riesgo_estrategia equals rs.id_riesgo_estrategia
                               join pe in db.proyectos_evaluaciones on r.id_proyecto_evaluacion equals pe.id_proyecto_evaluacion
                               join p in db.proyectos on pe.id_proyecto equals p.id_proyecto
                               join pt in db.proyectos_tecnologias on p.id_proyecto_tecnologia equals pt.id_proyecto_tecnologia
                               where (r.id_proyecto_evaluacion == id_proyecto_Evaluacion && r.usuario_borrado == null)
                               orderby(r.riesgo)
                               select new
                               {
                                   pt.id_proyecto_tecnologia,
                                   tecnologia = pt.nombre,
                                   r.id_riesgo,
                                   r.riesgo,
                                   r.id_riesgos_estatus,
                                   re.estatus,
                                   r.id_riesgo_probabilidad,
                                   probabilidad = rp.nombre,
                                   p_probabilidad = rp.porcentaje,
                                   r.id_riesgo_impacto_costo,
                                   impacto_costo = ric.nombre,
                                   p_impacto_costo = ric.porcentaje,
                                   r.id_riesgo_impacto_tiempo,
                                   impacto_tiempo = rit.nombre,
                                   p_impacto_tiempo = rit.porcentaje,
                                   r.id_riesgo_estrategia,
                                   estrategia = rs.nombre,
                                   fecha_evaluacion = pe.fecha_evaluacion,
                                   proyecto = p.proyecto,
                                   r.riesgo_costo,
                                   r.riesgo_tiempo
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

        /// <summary>
        /// Devuelve un cursor con los riesgos por proyectos
        /// </summary>
        /// <param name="id_proyecto_perido"></param>
        /// <returns></returns>
        public DataTable riesgos_historial(int id_proyecto_tecnologia)
        {
            try
            {
                DataTable dt = new DataTable();
                Proyectos_ConnextEntities db = new Proyectos_ConnextEntities();
                var riesgos = (from r in db.riesgos
                               join re in db.riesgos_estatus on r.id_riesgos_estatus equals re.id_riesgos_estatus
                               join rp in db.riesgos_probabilidad on r.id_riesgo_probabilidad equals rp.id_riesgo_probabilidad
                               join ric in db.riesgos_impacto_costo on r.id_riesgo_impacto_costo equals ric.id_riesgo_impacto_costo
                               join rit in db.riesgos_impacto_tiempo on r.id_riesgo_impacto_tiempo equals rit.id_riesgo_impacto_tiempo
                               join rs in db.riesgos_estrategia on r.id_riesgo_estrategia equals rs.id_riesgo_estrategia
                               join pe in db.proyectos_evaluaciones on r.id_proyecto_evaluacion equals pe.id_proyecto_evaluacion
                               join p in db.proyectos on pe.id_proyecto equals p.id_proyecto
                               join pt in db.proyectos_tecnologias on p.id_proyecto_tecnologia equals pt.id_proyecto_tecnologia
                               where (r.usuario_borrado == null && p.id_proyecto_tecnologia== id_proyecto_tecnologia)
                               select new
                               {
                                   tecnologia = pt.nombre,
                                   r.riesgo                                  
                               }).Distinct();

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
