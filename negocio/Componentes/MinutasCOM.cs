using datos.Modelos;
using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;

namespace negocio.Componentes
{
    public class MinutasCOM
    {
        /// <summary>
        /// Agrega una nueva Minuta
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string[] Agregar(proyectos_minutas entidad, DataTable dt_participantes, DataTable dt_pendientes)
        {
            string[] return_array = new string[2];
            try
            {
                proyectos_minutas minuta = new proyectos_minutas
                {
                    id_proyecto = entidad.id_proyecto,
                    asunto = entidad.asunto,
                    fecha = entidad.fecha,
                    resultados = entidad.resultados,
                    acuerdos = entidad.acuerdos,
                    propósito = entidad.propósito,
                    lugar = entidad.lugar,
                    fecha_registro = DateTime.Now,
                    usuario = entidad.usuario,
                    estatus = "BORRADOR"
                };
                Model context = new Model();
                context.proyectos_minutas.Add(minuta);
                context.SaveChanges();
                int id_entity = minuta.id_minuta;
                return_array[0] = "";
                return_array[1] = id_entity.ToString();

                int id_miniuta = Convert.ToInt32(return_array[1]);
                if (dt_participantes.Rows.Count > 0)
                {
                    proyectos_minutas_participantes entidad_p = new proyectos_minutas_participantes();
                    entidad_p.id_minuta = id_miniuta;
                    entidad_p.usuario = entidad.usuario;
                    entidad_p.usuario_borrado = entidad.usuario_edicion;
                    BorrarAllParticipantes(entidad_p);
                    foreach (DataRow row in dt_participantes.Rows)
                    {
                        if (Convert.ToInt32(row["id_pinvolucrado"]) > 0) { entidad_p.id_pinvolucrado = Convert.ToInt32(row["id_pinvolucrado"]); }
                        else { entidad_p.id_pinvolucrado = null; }
                        entidad_p.nombre = row["nombre"].ToString();
                        entidad_p.rol = row["rol"].ToString();
                        entidad_p.organización = row["organizacion"].ToString();
                        return_array[0] = ExisParticipante(entidad_p) ? "" : AgregarParticipante(entidad_p);
                        if (return_array[0] != "")
                        {
                            break;
                        }
                    }
                }
                if (dt_pendientes.Rows.Count > 0)
                {
                    proyectos_minutas_pendientes entidad_pend = new proyectos_minutas_pendientes();
                    entidad_pend.id_minuta = id_miniuta;
                    entidad_pend.usuario = entidad.usuario;
                    entidad_pend.usuario_borrado = entidad.usuario_edicion;
                    BorrarAllPendientes(entidad_pend);
                    foreach (DataRow row in dt_pendientes.Rows)
                    {
                        if (Convert.ToInt32(row["id_pinvolucrado"]) > 0) { entidad_pend.id_pinvolucrado = Convert.ToInt32(row["id_pinvolucrado"]); }
                        else { entidad_pend.id_pinvolucrado = null; }
                        entidad_pend.descripcion = row["descripcion"].ToString();
                        entidad_pend.responsable = row["responsable"].ToString();
                        entidad_pend.fecha_planeada = Convert.ToDateTime(row["fecha"]);
                        return_array[0] = ExistPendiente(entidad_pend) ? "" : AgregarPendiente(entidad_pend);
                        if (return_array[0] != "")
                        {
                            break;
                        }
                    }
                }
                return return_array;
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                return_array[0] = fullErrorMessage.ToString();
                return_array[1] = 0.ToString();
                return return_array;
            }
        }

        /// <summary>
        /// Edita una minuta
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Editar(proyectos_minutas entidad, DataTable dt_participantes, DataTable dt_pendientes)
        {
            try
            {
                string vmensaje = "";
                Model context = new Model();
                proyectos_minutas minuta = context.proyectos_minutas
                                .First(i => i.id_minuta == entidad.id_minuta);
                minuta.asunto = entidad.asunto;
                minuta.fecha = entidad.fecha;
                minuta.lugar = entidad.lugar;
                minuta.propósito = entidad.propósito;
                minuta.acuerdos = entidad.acuerdos;
                minuta.resultados = entidad.resultados;
                minuta.estatus = entidad.estatus;
                minuta.fecha_edicion = DateTime.Now;
                minuta.usuario_edicion = entidad.usuario_edicion;
                minuta.usuario_borrado = null;
                minuta.fecha_borrado = null;
                minuta.comentarios_borrado = null;
                context.SaveChanges();
                int id_entity = minuta.id_minuta;
                proyectos_minutas_participantes entidad_p = new proyectos_minutas_participantes();
                entidad_p.id_minuta = id_entity;
                entidad_p.usuario = entidad.usuario_edicion;
                entidad_p.usuario_borrado = entidad.usuario_edicion;
                BorrarAllParticipantes(entidad_p);
                foreach (DataRow row in dt_participantes.Rows)
                {
                    if (Convert.ToInt32(row["id_pinvolucrado"]) > 0) { entidad_p.id_pinvolucrado = Convert.ToInt32(row["id_pinvolucrado"]); }
                    else { entidad_p.id_pinvolucrado = null; }
                    entidad_p.nombre = row["nombre"].ToString();
                    entidad_p.rol = row["rol"].ToString();
                    entidad_p.organización = row["organizacion"].ToString();
                    vmensaje = ExisParticipante(entidad_p) ? "" : AgregarParticipante(entidad_p);
                    if (vmensaje != "")
                    {
                        break;
                    }
                }
                proyectos_minutas_pendientes entidad_pend = new proyectos_minutas_pendientes();
                entidad_pend.id_minuta = id_entity;
                entidad_pend.usuario = entidad.usuario_edicion;
                entidad_pend.usuario_borrado = entidad.usuario_edicion;
                BorrarAllPendientes(entidad_pend);
                foreach (DataRow row in dt_pendientes.Rows)
                {
                    if (Convert.ToInt32(row["id_pinvolucrado"]) > 0) { entidad_pend.id_pinvolucrado = Convert.ToInt32(row["id_pinvolucrado"]); }
                    else { entidad_pend.id_pinvolucrado = null; }
                    entidad_pend.descripcion = row["descripcion"].ToString();
                    entidad_pend.responsable = row["responsable"].ToString();
                    entidad_pend.fecha_planeada = Convert.ToDateTime(row["fecha"]);
                    vmensaje = ExistPendiente(entidad_pend) ? "" : AgregarPendiente(entidad_pend);
                    if (vmensaje != "")
                    {
                        break;
                    }
                }
                return vmensaje;
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                return fullErrorMessage.ToString();
            }
        }


        public string EditarEstatus(proyectos_minutas entidad)
        {
            try
            {
                string vmensaje = "";
                Model context = new Model();
                proyectos_minutas minuta = context.proyectos_minutas
                                .First(i => i.id_minuta == entidad.id_minuta);
                minuta.estatus = entidad.estatus;
                minuta.fecha_edicion = DateTime.Now;
                minuta.usuario_edicion = entidad.usuario_edicion;
                minuta.usuario_borrado = null;
                minuta.fecha_borrado = null;
                minuta.comentarios_borrado = null;
                context.SaveChanges();             
                return vmensaje;
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                return fullErrorMessage.ToString();
            }
        }


        /// <summary>
        /// Borra una minuta
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Borrar(proyectos_minutas entidad)
        {
            try
            {
                Model context = new Model();
                proyectos_minutas minuta = context.proyectos_minutas
                                .First(i => i.id_minuta == entidad.id_minuta);
                minuta.fecha_borrado = DateTime.Now;
                minuta.usuario_borrado = entidad.usuario_borrado;
                minuta.comentarios_borrado = entidad.comentarios_borrado;
                context.SaveChanges();
                return "";
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                return fullErrorMessage.ToString();
            }
        }

        /// <summary>
        /// Devuelve un DatatTable con las minutas
        /// </summary>
        /// <param name="id_proyect"></param>
        /// <returns></returns>
        public DataTable GetAll(int id_proyect)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.proyectos_minutas
                                .Where(s => s.id_proyecto == id_proyect && s.usuario_borrado == null)
                                .Select(u => new
                                {
                                    u.id_minuta,
                                    u.id_proyecto,
                                    u.asunto,
                                    u.fecha,
                                    u.propósito,
                                    u.estatus,
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

        /// <summary>
        /// Devuelve un DatatTable con las minutas filtradas por id_minuta
        /// </summary>
        /// <param name="id_proyect"></param>
        /// <returns></returns>
        public DataTable Get(proyectos_minutas entidad)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.proyectos_minutas
                                .Where(s => s.id_minuta == entidad.id_minuta && s.usuario_borrado == null)
                                .Select(u => new
                                {
                                    u.id_minuta,
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
                                    u.estatus,
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

        /// <summary>
        /// Agrega un participante
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string AgregarParticipante(proyectos_minutas_participantes entidad)
        {
            try
            {
                proyectos_minutas_participantes participante = new proyectos_minutas_participantes
                {
                    id_minuta = entidad.id_minuta,
                    id_pinvolucrado = entidad.id_pinvolucrado,
                    nombre = entidad.nombre,
                    organización = entidad.organización,
                    rol = entidad.rol,
                    fecha_registro = DateTime.Now,
                    usuario = entidad.usuario
                };
                Model context = new Model();
                context.proyectos_minutas_participantes.Add(participante);
                context.SaveChanges();
                int id_entity = participante.id_minparticipante;
                return "";
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);
                return fullErrorMessage.ToString();
            }
        }

        /// <summary>
        /// Agrega un participante
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string AgregarPendiente(proyectos_minutas_pendientes entidad)
        {
            try
            {
                proyectos_minutas_pendientes pendiente = new proyectos_minutas_pendientes
                {
                    id_minuta = entidad.id_minuta,
                    id_pinvolucrado = entidad.id_pinvolucrado,
                    descripcion = entidad.descripcion,
                    fecha_planeada = entidad.fecha_planeada,
                    responsable = entidad.responsable,
                    fecha_registro = DateTime.Now,
                    usuario = entidad.usuario
                };
                Model context = new Model();
                context.proyectos_minutas_pendientes.Add(pendiente);
                context.SaveChanges();
                return "";
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                return fullErrorMessage.ToString();
            }
        }

        /// <summary>
        /// Devuelve un DatatTable con los participantes
        /// </summary>
        /// <param name="id_proyect"></param>
        /// <returns></returns>
        public DataTable GetAllParticipante(int id_minuta)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.proyectos_minutas_participantes
                                .Where(s => s.id_minuta == id_minuta && s.usuario_borrado == null)
                                .Select(u => new
                                {
                                    u.id_minuta,
                                    u.id_pinvolucrado,
                                    u.id_minparticipante,
                                    u.nombre,
                                    u.organización,
                                    u.rol,
                                    u.fecha_registro,
                                    u.usuario,
                                    u.usuario_edicion,
                                    u.fecha_edicion,
                                    u.usuario_borrado,
                                    u.comentarios_borrado,
                                    u.fecha_borrado
                                })
                                .OrderBy(u => u.nombre);
                dt = To.DataTable(query.ToList());
                foreach (DataRow row in dt.Rows)
                {
                    if (row["id_pinvolucrado"].ToString().Trim() == "")
                    {
                        row["id_pinvolucrado"] = "0";
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        public DataTable GetAllPendientes(int id_minuta)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.proyectos_minutas_pendientes
                                .Where(s => s.id_minuta == id_minuta && s.usuario_borrado == null)
                                .Select(u => new
                                {
                                    u.id_minpendiente,
                                    u.id_minuta,
                                    u.id_pinvolucrado,
                                    u.descripcion,
                                    u.responsable,
                                    u.fecha_planeada,
                                    u.fecha_registro,
                                    u.usuario,
                                    u.usuario_edicion,
                                    u.fecha_edicion,
                                    u.usuario_borrado,
                                    u.fecha_borrado,
                                    u.comentarios_borrado
                                })
                                .OrderBy(u => u.responsable);
                dt = To.DataTable(query.ToList());
                foreach (DataRow row in dt.Rows)
                {
                    if (row["id_pinvolucrado"].ToString().Trim() == "")
                    {
                        row["id_pinvolucrado"] = "0";
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        /// <summary>
        /// Borra un participante
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string BorrarParticipante(proyectos_minutas_participantes entidad)
        {
            try
            {
                Model context = new Model();
                proyectos_minutas_participantes participantes = context.proyectos_minutas_participantes
                                .First(i => i.id_minparticipante == entidad.id_minparticipante);
                participantes.fecha_borrado = DateTime.Now;
                participantes.usuario_borrado = entidad.usuario_borrado;
                participantes.comentarios_borrado = entidad.comentarios_borrado;
                context.SaveChanges();
                return "";
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                return fullErrorMessage.ToString();
            }
        }

        public string BorrarPendientes(proyectos_minutas_pendientes entidad)
        {
            try
            {
                Model context = new Model();
                proyectos_minutas_pendientes participantes = context.proyectos_minutas_pendientes
                                .First(i => i.id_minpendiente == entidad.id_minpendiente);
                participantes.fecha_borrado = DateTime.Now;
                participantes.usuario_borrado = entidad.usuario_borrado;
                participantes.comentarios_borrado = entidad.comentarios_borrado;
                context.SaveChanges();
                return "";
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                return fullErrorMessage.ToString();
            }
        }

        public Boolean BorrarAllParticipantes(proyectos_minutas_participantes entidad)
        {
            try
            {
                DataTable dt_tareas = GetAllParticipante(entidad.id_minuta);
                foreach (DataRow participante in dt_tareas.Rows)
                {
                    entidad.id_minuta = Convert.ToInt32(participante["id_minuta"]);
                    entidad.id_minparticipante = Convert.ToInt32(participante["id_minparticipante"]);
                    entidad.comentarios_borrado = "borrado por actualizacion";
                    BorrarParticipante(entidad);
                }
                return true;
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                return false;
            }
        }

        public Boolean BorrarAllPendientes(proyectos_minutas_pendientes entidad)
        {
            try
            {
                DataTable dt_tareas = GetAllPendientes(entidad.id_minuta);
                foreach (DataRow participante in dt_tareas.Rows)
                {
                    entidad.id_minuta = Convert.ToInt32(participante["id_minuta"]);
                    entidad.id_minpendiente = Convert.ToInt32(participante["id_minpendiente"]);
                    entidad.comentarios_borrado = "borrado por actualizacion";
                    BorrarPendientes(entidad);
                }
                return true;
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                return false;
            }
        }

        public bool ExisParticipante(proyectos_minutas_participantes entidad)
        {
            try
            {
                bool exist = false;
                Model context = new Model();
                var query = context.proyectos_minutas_participantes
                                .Where(s => s.id_minuta == entidad.id_minuta)
                                .Select(u => new
                                {
                                    u.id_minuta,
                                    u.id_pinvolucrado,
                                    u.id_minparticipante,
                                    u.nombre,
                                    u.organización,
                                    u.rol,
                                    u.fecha_registro,
                                    u.usuario,
                                    u.usuario_edicion,
                                    u.fecha_edicion,
                                    u.usuario_borrado,
                                    u.comentarios_borrado,
                                    u.fecha_borrado
                                });
                DataTable dt_participantes = To.DataTable(query.ToList());
                DataRow[] row = dt_participantes.
                    Select("nombre = '" + entidad.nombre.Trim() + "' and organización = '" + entidad.organización.Trim() + "' and rol ='" + entidad.rol.Trim() + "'");
                if (row.CopyToDataTable().Rows.Count > 0)
                {
                    int id_minparticipante = Convert.ToInt32(row[0]["id_minparticipante"]);
                    proyectos_minutas_participantes participantes = context.proyectos_minutas_participantes
                                    .First(i => i.id_minparticipante == id_minparticipante);
                    participantes.fecha_borrado = null;
                    participantes.usuario_borrado = null;
                    participantes.comentarios_borrado = null;
                    context.SaveChanges();
                    exist = true;
                }
                return exist;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ExistPendiente(proyectos_minutas_pendientes entidad)
        {
            try
            {
                Model context = new Model();
                bool exist = false;
                var query = context.proyectos_minutas_pendientes
                           .Where(s => s.id_minuta == entidad.id_minuta)
                           .Select(u => new
                           {
                               u.id_minpendiente,
                               u.id_minuta,
                               u.id_pinvolucrado,
                               u.descripcion,
                               u.responsable,
                               u.fecha_planeada,
                               u.fecha_registro,
                               u.usuario,
                               u.usuario_edicion,
                               u.fecha_edicion,
                               u.usuario_borrado,
                               u.fecha_borrado,
                               u.comentarios_borrado
                           });
                DataTable dt_participantes = To.DataTable(query.ToList());
                DataRow[] row = dt_participantes.
                    Select("responsable = '" + entidad.responsable + "' and descripcion ='" + entidad.descripcion + "'");
                if (row.CopyToDataTable().Rows.Count > 0)
                {
                    int id_minpendiente = Convert.ToInt32(row[0]["id_minpendiente"]);
                    proyectos_minutas_pendientes participantes = context.proyectos_minutas_pendientes
                                    .First(i => i.id_minpendiente == id_minpendiente);
                    participantes.fecha_borrado = null;
                    participantes.usuario_borrado = null;
                    participantes.comentarios_borrado = null;
                    context.SaveChanges();
                    exist = true;
                }
                return exist;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}