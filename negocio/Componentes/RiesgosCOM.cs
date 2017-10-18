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
    public class RiesgosCOM
    {
        /// <summary>
        /// Agrega una instancia de riesgos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Agregar(riesgos entidad, List<actividades> lst_actividades, 
            List<documentos> lstdocumentos, string nombre_proyecto, string nombre_session)
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
                    id_riesgo_impacto =entidad.id_riesgo_impacto,
                    id_riesgo_estrategia = entidad.id_riesgo_estrategia,
                    estrategia=entidad.estrategia,
                    valor=entidad.valor,
                    usuario_resp =entidad.usuario_resp,
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
                        id_actividad_tipo= entidad2.id_actividad_tipo,
                        id_proyecto = entidad2.id_proyecto,
                        id_riesgo = entidad2.id_riesgo,
                        nombre = entidad2.nombre,
                        usuario_resp = entidad2.usuario_resp,
                        fecha_ejecucion = entidad2.fecha_ejecucion,
                        fecha_asignacion = entidad2.fecha_asignacion,
                        usuario = entidad2.usuario,
                        empleado_resp = entidad2.empleado_resp,
                        fecha_registro = DateTime.Now,
                        recomendada = entidad2.recomendada,
                        resultado ="",
                        terminada=false
                    };
                    context.actividades.Add(actividad);
                    context.SaveChanges();
                    int id_actividad = actividad.id_actividad;
                    //ENVIAMOS NOTIFICACION
                    string usuario_resp = actividad.usuario_resp;
                    EmpleadosCOM usuarios = new EmpleadosCOM();
                    DataTable dt_usuario = usuarios.GetUsers();
                    DataView dv = dt_usuario.DefaultView;
                    dv.RowFilter = "usuario_red = '" + usuario_resp.Trim().ToUpper() + "'";
                    DataTable dt_result = dv.ToTable();
                    if (dt_result.Rows.Count > 0)
                    {

                        string saludo = DateTime.Now.Hour > 13 ? "Buenas tardes" : "Buenos dias";
                        DataRow usuario = dt_result.Rows[0];
                        string mail_to = usuario["mail"].ToString() == "" ? "" : (usuario["mail"].ToString() + ";");
                        string subject = "Módulo de proyectos - Actividad relacionada";
                        string mail = "<div>" + saludo + " <strong>" +
                            System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(usuario["empleado"].ToString().ToLower())
                            + "</strong> <div>" +
                            "<br>" +
                            "<p>Le fue asignada una actividad, dentro de una evaluación en el proyecto <strong>" + nombre_proyecto + "</strong>" +
                            "</p>" +
                            "<p>A continuación, se muestra la información completa:</p>" +                          
                               "<p><strong>Riesgo</strong><br/> " +
                                 riesgo.riesgo + "</p> " +
                               "<p><strong>Actividad/Acción</strong><br/> " +
                                actividad.nombre + "</p> " +
                            "<br/><p>Este movimiento fue realizado por <strong>" +
                            System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(nombre_session.ToLower())
                            + "</strong> el dia <strong>" +
                            DateTime.Now.ToString("dddd dd MMMM, yyyy hh:mm:ss tt", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX")) + "</strong>" +
                            "</p>";
                        CorreosCOM correos = new CorreosCOM();
                        bool correct = correos.SendMail(mail, subject, mail_to);
                    }
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
                riesgo.id_riesgo_impacto = entidad.id_riesgo_impacto;               
                riesgo.id_riesgo_estrategia = entidad.id_riesgo_estrategia;
                riesgo.estrategia = entidad.estrategia;
                riesgo.usuario_resp = entidad.usuario_resp;
                riesgo.valor = entidad.valor;
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
                        actividad.recomendada = entidad2.recomendada;
                        actividad.id_actividad_tipo = entidad2.id_actividad_tipo;
                        actividad.resultado = entidad2.resultado;
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
                            id_actividad_tipo = entidad2.id_actividad_tipo,
                            usuario_resp = entidad2.usuario_resp,
                            fecha_ejecucion = entidad2.fecha_ejecucion,
                            fecha_asignacion = entidad2.fecha_asignacion,
                            usuario = entidad2.usuario,
                            empleado_resp = entidad2.empleado_resp,
                            fecha_registro = DateTime.Now,
                            recomendada =entidad2.recomendada,
                            resultado = "",
                            terminada=false
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
        /// Elimina una instancia de riesgos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string EditarProbabilidad (int id_riesgo, int id_probabilidad, string usuario)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                riesgos riesgo = context.riesgos
                                .First(i => i.id_riesgo == id_riesgo);
                int id_impacto = Convert.ToInt32(riesgo.id_riesgo_impacto);
                if (id_impacto > 0 && id_probabilidad > 0)
                {
                    RiesgosProbabilidadCOM probabilidades = new RiesgosProbabilidadCOM();
                    riesgos_probabilidad probabilidad = probabilidades.impacto(id_probabilidad);
                    RiesgosImpactoCostosCOM impactos = new RiesgosImpactoCostosCOM();
                    riesgos_impactos impacto = impactos.impacto(id_impacto);
                    int valor = impacto.valor * probabilidad.valor;
                    RiesgosEstrategiaCOM estrategias = new RiesgosEstrategiaCOM();
                    DataTable dt = estrategias.SelectAll();
                    foreach (DataRow estrategia in dt.Rows)
                    {
                        int value_min = Convert.ToInt16(estrategia["valor_min"]);
                        int value_max = Convert.ToInt16(estrategia["valor_max"]);
                        if (valor >= value_min && valor <= value_max)
                        {
                            riesgo.usuario_edicion = usuario;
                            riesgo.fecha_edicion = DateTime.Now;
                            riesgo.valor = Convert.ToByte(valor);
                            riesgo.id_riesgo_estrategia = Convert.ToInt32(estrategia["id_riesgo_estrategia"]);
                            riesgo.id_riesgo_probabilidad = id_probabilidad;
                            break;
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

        public string EditarImpacto(int id_riesgo, int id_impacto, string usuario)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                riesgos riesgo = context.riesgos
                                .First(i => i.id_riesgo == id_riesgo);
                int id_probabilidad = Convert.ToInt32(riesgo.id_riesgo_probabilidad);
                if (id_impacto > 0 && id_probabilidad > 0)
                {
                    RiesgosProbabilidadCOM probabilidades = new RiesgosProbabilidadCOM();
                    riesgos_probabilidad probabilidad = probabilidades.impacto(id_probabilidad);
                    RiesgosImpactoCostosCOM impactos = new RiesgosImpactoCostosCOM();
                    riesgos_impactos impacto = impactos.impacto(id_impacto);
                    int valor = impacto.valor * probabilidad.valor;
                    RiesgosEstrategiaCOM estrategias = new RiesgosEstrategiaCOM();
                    DataTable dt = estrategias.SelectAll();
                    foreach (DataRow estrategia in dt.Rows)
                    {
                        int value_min = Convert.ToInt16(estrategia["valor_min"]);
                        int value_max = Convert.ToInt16(estrategia["valor_max"]);
                        if (valor >= value_min && valor <= value_max)
                        {
                            riesgo.usuario_edicion = usuario;
                            riesgo.fecha_edicion = DateTime.Now;
                            riesgo.valor = Convert.ToByte(valor);
                            riesgo.id_riesgo_estrategia = Convert.ToInt32(estrategia["id_riesgo_estrategia"]);
                            riesgo.id_riesgo_impacto = id_impacto;
                            break;
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
                              join ric in db.riesgos_impactos on r.id_riesgo_impacto equals ric.id_riesgo_impacto
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
                                  estrategia_detalle=r.estrategia,                                  
                                  impacto_costo = ric.nombre,
                                  r.id_riesgo_impacto,
                                  r.id_riesgo_estrategia,
                                  estrategia = rs.nombre,
                                  fecha_evaluacion = pe.fecha_evaluacion,
                                  proyecto = p.proyecto
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
                               join ric in db.riesgos_impactos on r.id_riesgo_impacto equals ric.id_riesgo_impacto
                               join rs in db.riesgos_estrategia on r.id_riesgo_estrategia equals rs.id_riesgo_estrategia
                               join pe in db.proyectos_evaluaciones on r.id_proyecto_evaluacion equals pe.id_proyecto_evaluacion
                               join p in db.proyectos on pe.id_proyecto equals p.id_proyecto
                               join pt in db.proyectos_tecnologias on p.id_proyecto_tecnologia equals pt.id_proyecto_tecnologia
                               where (p.id_proyecto == id_proyecto && r.usuario_borrado == null)
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
                                   estrategia_detalle = r.estrategia,
                                   impacto_costo = ric.nombre,
                                   r.id_riesgo_impacto,
                                   r.id_riesgo_estrategia,
                                   estrategia = rs.nombre,
                                   fecha_evaluacion = pe.fecha_evaluacion,
                                   proyecto = p.proyecto
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
        public DataTable proyectos_riesgos_reporte(int num_empleado, bool ver_Todos_los_empleados,  DateTime fecha_inicio, DateTime fecha_fin)
        {
            DataTable dt = new DataTable();
            try
            {
                EmpleadosCOM empleados = new EmpleadosCOM();
                bool no_activos = false;
                DataSet ds = empleados.sp_listado_empleados(num_empleado, ver_Todos_los_empleados, no_activos);
                DataTable dt_empleados_subordinados = ds.Tables[0];
                List<EmpleadoSubordinados> list_emp = new List<EmpleadoSubordinados>();
                foreach (DataRow row in dt_empleados_subordinados.Rows)
                {
                    EmpleadoSubordinados empleado = new EmpleadoSubordinados();
                    empleado.Usuario = row["usuario"].ToString().ToUpper();
                    list_emp.Add(empleado);
                }
               
                Proyectos_ConnextEntities db = new Proyectos_ConnextEntities();
                var riesgos = (from r in db.riesgos
                               join re in db.riesgos_estatus on r.id_riesgos_estatus equals re.id_riesgos_estatus
                               join rp in db.riesgos_probabilidad on r.id_riesgo_probabilidad equals rp.id_riesgo_probabilidad
                               join rs in db.riesgos_estrategia on r.id_riesgo_estrategia equals rs.id_riesgo_estrategia
                               join pe in db.proyectos_evaluaciones on r.id_proyecto_evaluacion equals pe.id_proyecto_evaluacion
                               join p in db.proyectos on pe.id_proyecto equals p.id_proyecto
                               join pt in db.proyectos_tecnologias on p.id_proyecto_tecnologia equals pt.id_proyecto_tecnologia
                               where (p.usuario_borrado == null
                                 && (r.usuario_borrado==null)
                                  && (r.fecha_registro >= fecha_inicio && r.fecha_registro <= fecha_fin))
                               orderby r.id_riesgo ascending
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
                                   r.id_riesgo_estrategia,
                                   estrategia = rs.nombre,
                                   fecha_evaluacion = pe.fecha_evaluacion,
                                   proyecto = p.proyecto,
                                   r.usuario
                               }).ToArray();
                 var triesgos = (from r in riesgos
                                 join emp in list_emp on r.usuario.ToUpper() equals emp.Usuario
                                 select new
                                 {
                                     r.usuario,
                                     r.id_proyecto_tecnologia,
                                     r.tecnologia,
                                     r.id_riesgo,
                                     r.riesgo,
                                     r.id_riesgos_estatus,
                                     r.estatus,
                                     r.id_riesgo_probabilidad,
                                     r.probabilidad,
                                     r.id_riesgo_estrategia,
                                     r.estrategia,
                                     r.fecha_evaluacion,
                                     r.proyecto
                                   });
                NAVISION dbnavision = new NAVISION();
                var results = from r in riesgos
                              join up in dbnavision.Employee on r.usuario.ToUpper().Trim() equals up.Usuario_Red.ToUpper().Trim()
                              select new
                              {
                                  usuario = r.usuario,
                                  empleado = up.First_Name.Trim() + " " + up.Last_Name.Trim(),
                                  r.id_proyecto_tecnologia,
                                  r.tecnologia,
                                  r.id_riesgo,
                                  r.riesgo,
                                  r.id_riesgos_estatus,
                                  r.estatus,
                                  r.id_riesgo_probabilidad,
                                  r.probabilidad,
                                  r.id_riesgo_estrategia,
                                  r.estrategia,
                                  r.fecha_evaluacion,
                                  r.proyecto
                              };
                dt = To.DataTable(results.ToList());
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
                               join ric in db.riesgos_impactos on r.id_riesgo_impacto equals ric.id_riesgo_impacto
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
                                   estrategia_detalle = r.estrategia,
                                   impacto_costo = ric.nombre,
                                   r.id_riesgo_impacto,
                                   r.id_riesgo_estrategia,
                                   estrategia = rs.nombre,
                                   fecha_evaluacion = pe.fecha_evaluacion,
                                   proyecto = p.proyecto
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
                               join ric in db.riesgos_impactos on r.id_riesgo_impacto equals ric.id_riesgo_impacto
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
