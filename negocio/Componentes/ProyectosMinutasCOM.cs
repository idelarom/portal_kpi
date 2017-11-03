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
    public class ProyectosMinutasCOM
    {
        public string Agregar(proyectos_minutas entidad, List<proyectos_minutas_participantes> participantes, List<proyectos_minutas_pendientes> pendientes)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                string mess = "";
                proyectos_minutas minuta = new proyectos_minutas
                {
                   id_proyecto = entidad.id_proyecto
                   ,usuario = entidad.usuario
                   ,asunto = entidad.asunto
                   ,fecha= entidad.fecha
                   ,propósito = entidad.propósito
                   ,resultados = entidad.resultados
                   ,acuerdos = entidad.acuerdos
                   ,lugar = entidad.lugar
                   ,enviada = entidad.enviada
                   ,fecha_registro = DateTime.Now
                };
                context.proyectos_minutas.Add(minuta);
                decimal id_minuta = minuta.id_proyectomin;
                foreach (proyectos_minutas_participantes participante in participantes)
                {
                    participante.id_proyectomin = id_minuta;
                    context.proyectos_minutas_participantes.Add(participante);
                }
                foreach (proyectos_minutas_pendientes pendiente in pendientes)
                {
                    pendiente.id_proyectomin = id_minuta;
                    context.proyectos_minutas_pendientes.Add(pendiente);
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

        public string Editar(proyectos_minutas entidad, List<proyectos_minutas_participantes> participantes, List<proyectos_minutas_pendientes> pendientes)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                proyectos_minutas minuta = context.proyectos_minutas
                                .First(i => i.id_proyectomin == entidad.id_proyectomin);
                minuta.usuario_edicion = entidad.usuario_edicion;
                minuta.fecha_edicion = DateTime.Now;
                minuta.asunto = entidad.asunto;
                minuta.fecha = entidad.fecha;
                minuta.propósito = entidad.propósito;
                minuta.resultados = entidad.resultados;
                minuta.acuerdos = entidad.acuerdos;
                minuta.lugar = entidad.lugar;
                minuta.enviada = entidad.enviada;
                List<proyectos_minutas_participantes> participantes_historial = minuta.proyectos_minutas_participantes.ToList();
                List<proyectos_minutas_pendientes> pendientes_historial = minuta.proyectos_minutas_pendientes.ToList();
                foreach (proyectos_minutas_participantes participante in participantes_historial)
                {
                    participante.usuario_borrado = entidad.usuario_edicion;
                    participante.comentarios_borrado = "Borrado por actualización";
                    participante.fecha_borrado = DateTime.Now;
                }
                foreach (proyectos_minutas_pendientes pendiente in pendientes_historial)
                {
                    pendiente.usuario_borrado = entidad.usuario_edicion;
                    pendiente.comentarios_borrado = "Borrado por actualización";
                    pendiente.fecha_borrado = DateTime.Now;
                }


                foreach (proyectos_minutas_participantes participante in participantes)
                {
                    var query = context.proyectos_minutas_participantes
                               .Where(i => i.id_proyectominpart == participante.id_proyectominpart
                                                                && i.id_proyectomin == participante.id_proyectomin);
                    DataTable dt = To.DataTable(query.ToList());
                    
                    //si es null agregamos, si no es null editamos
                    if (dt.Rows.Count == 0)
                    {
                        participante.fecha_registro = DateTime.Now;
                        context.proyectos_minutas_participantes.Add(participante);
                    }
                    else
                    {
                        proyectos_minutas_participantes vparticipante = context.proyectos_minutas_participantes
                                                                .First(i => i.id_proyectominpart == participante.id_proyectominpart
                                                                && i.id_proyectomin == participante.id_proyectomin);
                        vparticipante.usuario_borrado = null;
                        vparticipante.fecha_borrado = null;
                        vparticipante.comentarios_borrado = null;
                        vparticipante.nombre = participante.nombre;
                        vparticipante.usuario = participante.usuario;
                        vparticipante.rol = participante.rol;
                        vparticipante.organización = participante.organización;
                        vparticipante.fecha_edicion = DateTime.Now;
                        vparticipante.usuario_edicion = entidad.usuario_edicion;
                    }
                }

                foreach (proyectos_minutas_pendientes pendiente in pendientes)
                {
                    var query = context.proyectos_minutas_pendientes
                              .Where(i => i.id_proyectominpen == pendiente.id_proyectominpen
                                                                && i.id_proyectomin == pendiente.id_proyectomin);
                    DataTable dt = To.DataTable(query.ToList());
                    //si es null agregamos, si no es null editamos
                    if (dt.Rows.Count == 0)
                    {
                        pendiente.fecha_registro = DateTime.Now;
                        context.proyectos_minutas_pendientes.Add(pendiente);
                    }
                    else
                    {
                        proyectos_minutas_pendientes vpendiente = context.proyectos_minutas_pendientes
                                                                    .First(i => i.id_proyectominpen == pendiente.id_proyectominpen
                                                                    && i.id_proyectomin == pendiente.id_proyectomin);
                        vpendiente.usuario_borrado = null;
                        vpendiente.fecha_borrado = null;
                        vpendiente.comentarios_borrado = null;
                        vpendiente.nombre = pendiente.nombre;
                        vpendiente.usuario_resp = pendiente.usuario_resp;
                        vpendiente.avance = pendiente.avance;
                        vpendiente.fecha_planeada = pendiente.fecha_planeada;
                        vpendiente.descripcion = pendiente.descripcion;
                        vpendiente.fecha_edicion = DateTime.Now;
                        vpendiente.usuario_edicion = entidad.usuario_edicion;
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

        public string Enviar(int id_proyectomin, string usuario)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                proyectos_minutas minuta = context.proyectos_minutas
                                .First(i => i.id_proyectomin == id_proyectomin);
                minuta.usuario_edicion = usuario;
                minuta.fecha_edicion = DateTime.Now;
                minuta.enviada = true;
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

        public string Eliminar(int id_proyectomin, string usuario, string comentarios)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                proyectos_minutas minuta = context.proyectos_minutas
                                .First(i => i.id_proyectomin == id_proyectomin);
                minuta.usuario_borrado = usuario;
                minuta.fecha_borrado = DateTime.Now;
                minuta.comentarios_borrado = comentarios;
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

        public DataTable GetAllPendientes(int id_proyectomin)
        {
            DataTable dt = new DataTable();
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                EmpleadosCOM empleados = new EmpleadosCOM();
                bool no_activos = false;
                DataSet ds = empleados.sp_listado_empleados(0, true, no_activos);
                DataTable dt_empleados_subordinados = ds.Tables[0];
                List<EmpleadoSubordinados> list_emp = new List<EmpleadoSubordinados>();
                foreach (DataRow row in dt_empleados_subordinados.Rows)
                {
                    EmpleadoSubordinados empleado = new EmpleadoSubordinados();
                    empleado.Usuario = row["usuario"].ToString().ToUpper();
                    empleado.Nombre = row["nombre"].ToString();
                    empleado.Puesto = row["puesto"].ToString();
                    empleado.Correo = row["correo"].ToString();
                    list_emp.Add(empleado);
                }
                var query = context.proyectos_minutas_pendientes
                                .Where(s => s.id_proyectomin == id_proyectomin && s.usuario_borrado == null)
                                .Select(u => new
                                {
                                    u.id_proyectominpen,
                                    u.id_proyectomin,
                                    u.descripcion,
                                    u.fecha_planeada,
                                    u.fecha_registro,
                                    u.usuario_resp,
                                    u.usuario_edicion,
                                    u.fecha_edicion,
                                    u.usuario_borrado,
                                    u.fecha_borrado,
                                    u.comentarios_borrado,
                                    u.avance,
                                    u.nombre
                                }).ToArray();

                var result = (from u in query
                              join em in list_emp on u.usuario_resp.Trim().ToUpper() equals em.Usuario.ToUpper().Trim()
                              orderby(em.Nombre)
                              select new {
                                  u.id_proyectominpen,
                                  responsable = em.Nombre,
                                  em.Puesto,
                                  em.Correo,
                                  u.id_proyectomin,
                                  u.descripcion,
                                  u.fecha_planeada,
                                  u.fecha_registro,
                                  u.usuario_resp,
                                  u.usuario_edicion,
                                  u.fecha_edicion,
                                  u.usuario_borrado,
                                  u.fecha_borrado,
                                  u.comentarios_borrado,
                                  u.avance,
                                  u.nombre
                              });
                dt = To.DataTable(result.ToList());
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        /// <summary>
        /// Devuelve un DatatTable con los participantes
        /// </summary>
        /// <param name="id_proyect"></param>
        /// <returns></returns>
        public DataTable GetAllParticipante(int id_proyectomin)
        {
            DataTable dt = new DataTable();
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                EmpleadosCOM empleados = new EmpleadosCOM();
                bool no_activos = false;
                DataSet ds = empleados.sp_listado_empleados(0, true, no_activos);
                DataTable dt_empleados_subordinados = ds.Tables[0];
                List<EmpleadoSubordinados> list_emp = new List<EmpleadoSubordinados>();
                foreach (DataRow row in dt_empleados_subordinados.Rows)
                {
                    EmpleadoSubordinados empleado = new EmpleadoSubordinados();
                    empleado.Usuario = row["usuario"].ToString().ToUpper();
                    empleado.Nombre = row["nombre"].ToString();
                    empleado.Puesto = row["puesto"].ToString();
                    empleado.Correo = row["correo"].ToString();
                    list_emp.Add(empleado);
                }
                //empleados
                var query = context.proyectos_minutas_participantes
                                .Where(s => s.id_proyectomin == id_proyectomin && s.usuario_borrado == null
                                && s.usuario != "")
                                .Select(u => new
                                {
                                    u.id_proyectomin,
                                    u.id_proyectominpart,
                                    u.organización,
                                    u.rol,
                                    u.correos,
                                    u.fecha_registro,
                                    u.usuario,
                                    u.usuario_edicion,
                                    u.fecha_edicion,
                                    u.usuario_borrado,
                                    u.comentarios_borrado,
                                    u.fecha_borrado,
                                    u.nombre
                                }).ToArray();

                var result = (from u in query
                              join em in list_emp on u.usuario.Trim().ToUpper() equals em.Usuario.ToUpper().Trim()
                              orderby (em.Nombre)
                              select new
                              {
                                  em.Nombre,
                                  em.Puesto,
                                  em.Correo,
                                  u.id_proyectomin,
                                  u.id_proyectominpart,
                                  u.organización,
                                  u.rol,
                                  u.fecha_registro,
                                  u.usuario,
                                  u.usuario_edicion,
                                  u.fecha_edicion,
                                  u.usuario_borrado,
                                  u.comentarios_borrado,
                                  u.fecha_borrado
                              }).ToArray();
                //NO EMPLEADOS
                var query2 = context.proyectos_minutas_participantes
                               .Where(s => s.id_proyectomin == id_proyectomin && s.usuario_borrado == null
                               && s.usuario == "")
                               .Select(u => new
                               {
                                   Nombre = u.nombre,
                                   Puesto = u.rol,
                                   Correo = u.correos,
                                   u.id_proyectomin,
                                   u.id_proyectominpart,
                                   u.organización,
                                   u.rol,
                                   u.fecha_registro,
                                   u.usuario,
                                   u.usuario_edicion,
                                   u.fecha_edicion,
                                   u.usuario_borrado,
                                   u.comentarios_borrado,
                                   u.fecha_borrado
                               }).ToArray();

                var resultUnion = Enumerable.Union(result, query2);
                dt = To.DataTable(resultUnion.ToList());
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        public DataTable GetAll(int id_proyect)
        {
            DataTable dt = new DataTable();
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                var query = context.proyectos_minutas
                                .Where(s => s.id_proyecto == id_proyect && s.usuario_borrado == null)
                                .Select(u => new
                                {
                                    u.id_proyectomin,
                                    u.id_proyecto,
                                    u.asunto,
                                    u.fecha,
                                    u.propósito,
                                    u.enviada,
                                    u.resultados,
                                    u.acuerdos,
                                    u.lugar,
                                    u.fecha_registro,
                                    u.usuario,
                                    u.usuario_edicion,
                                    u.fecha_edicion,
                                    u.usuario_borrado,
                                    u.comentarios_borrado,
                                    u.fecha_borrado
                                });
                dt = To.DataTable(query.ToList());
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }
        public DataTable Get(proyectos_minutas entidad)
        {
            DataTable dt = new DataTable();
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                var query = context.proyectos_minutas
                                .Where(s => s.id_proyectomin == entidad.id_proyectomin && s.usuario_borrado == null)
                                .Select(u => new
                                {
                                    u.id_proyectomin,
                                    u.id_proyecto,
                                    u.asunto,
                                    u.fecha,
                                    u.propósito,
                                    u.resultados,
                                    u.acuerdos,
                                    u.lugar,
                                    u.fecha_registro,
                                    u.usuario,
                                    u.usuario_edicion,
                                    u.fecha_edicion,
                                    u.usuario_borrado,
                                    u.enviada,
                                    u.comentarios_borrado,
                                    u.fecha_borrado
                                });
                dt = To.DataTable(query.ToList());
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }
    }
}
