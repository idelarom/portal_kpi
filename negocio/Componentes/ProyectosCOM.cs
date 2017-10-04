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
    public class ProyectosCOM
    {
        /// <summary>
        /// Agrega una instancia de proyectos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Agregar(proyectos entidad)
        {
            try
            {
                string mess = "";
                if (Exist(entidad.proyecto))
                {
                    mess = "Ya existe un proyecto llamado: " + entidad.proyecto;
                }
                else
                {
                    proyectos proyecto = new proyectos
                    {
                        id_proyecto_periodo = entidad.id_proyecto_periodo,
                        id_proyecto_estatus = entidad.id_proyecto_estatus,
                        proyecto = entidad.proyecto,
                        descripcion = entidad.descripcion,
                        cveoport = entidad.cveoport,
                        folio_pmt = entidad.folio_pmt,
                        id_cliente = entidad.id_cliente,
                        duración = entidad.duración,
                        avance = entidad.avance,
                        terminado = false,
                        correo_bienvenida = false,
                        fecha_inicio = entidad.fecha_inicio,
                        fecha_fin = entidad.fecha_fin,
                        usuario = entidad.usuario.ToUpper(),
                        fecha_registro = DateTime.Now
                    };
                    Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                    context.proyectos.Add(proyecto);
                    context.SaveChanges();
                }
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
        /// Edita una instancia de proyectos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Editar(proyectos entidad)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                proyectos proyecto = context.proyectos
                                .First(i => i.id_proyecto == entidad.id_proyecto);
                proyecto.proyecto = entidad.proyecto;            
                proyecto.id_proyecto_periodo = entidad.id_proyecto_periodo;
                proyecto.id_proyecto_estatus = entidad.id_proyecto_estatus;
                proyecto.proyecto = entidad.proyecto;
                proyecto.descripcion = entidad.descripcion;
                proyecto.cveoport = entidad.cveoport;
                proyecto.folio_pmt = entidad.folio_pmt;
                proyecto.id_cliente = entidad.id_cliente;
                proyecto.duración = entidad.duración;
                proyecto.avance = entidad.avance;
                proyecto.terminado = false;
                proyecto.correo_bienvenida = false;
                proyecto.fecha_inicio = entidad.fecha_inicio;
                proyecto.fecha_fin = entidad.fecha_fin;
                proyecto.usuario_edicion = entidad.usuario_edicion.ToUpper();
                proyecto.fecha_edicion = DateTime.Now;
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
        /// Elimina una instancia de proyectos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Elimina(proyectos entidad)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                proyectos proyecto = context.proyectos
                                .First(i => i.id_proyecto == entidad.id_proyecto);
                proyecto.usuario_borrado = entidad.usuario_borrado.ToUpper();
                proyecto.fecha_borrado = DateTime.Now;
                proyecto.comentarios_borrado = entidad.comentarios_borrado;
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
        /// Devuelve un valor booleano si existe un proyecto con el mismo nombre
        /// </summary>
        /// <param name="titulo"></param>
        /// <returns></returns>
        public bool Exist(string proyecto)
        {
            DataTable dt = new DataTable();
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                var query = context.proyectos
                                .Where(s => s.proyecto.ToUpper() == proyecto.ToUpper() && s.usuario_borrado == null)
                                .Select(u => new
                                {
                                    u.id_proyecto
                                })
                                .OrderBy(u => u.id_proyecto);
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
        /// Devuelve una instancia de la clase proyectos
        /// </summary>
        /// <param name="id_proyecto_perido"></param>
        /// <returns></returns>
        public proyectos proyecto(int id_proyecto)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                proyectos proyecto = context.proyectos
                                .First(i => i.id_proyecto == id_proyecto);
                return proyecto;
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
        /// Devuelve un listado de instancias de la clase evaluaciones por proyectos
        /// </summary>
        /// <param name="id_proyecto_perido"></param>
        /// <returns></returns>
        public DataTable evaluaciones(int id_proyecto)
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
                dt.Columns.Add("fecha_evaluacion_str");
                foreach (DataRow row in dt.Rows)
                {
                    row["fecha_evaluacion_str"] = Convert.ToDateTime(row["fecha_evaluacion"]).ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX")).ToUpper();
                }
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
        /// Obtiene una tabla con la informacion de un proyecto
        /// </summary>
        /// <param name="id_proyecto"></param>
        /// <returns></returns>
        public DataTable Select(int id_proyecto)
        {
            DataTable dt = new DataTable();
            try
            {
                EmpleadosCOM empleados = new EmpleadosCOM();
                Proyectos_ConnextEntities db = new Proyectos_ConnextEntities();
                var proyectos = (from p in db.proyectos
                                 join est in db.proyectos_estatus on p.id_proyecto_estatus equals est.id_proyecto_estatus
                                 join period in db.proyectos_periodos on p.id_proyecto_periodo equals period.id_proyecto_periodo
                                 join t in db.proyectos_tecnologias on p.id_proyecto_tecnologia equals t.id_proyecto_tecnologia
                                 where (p.usuario_borrado == null && p.id_proyecto == id_proyecto)
                                 select new
                                 {
                                     p.id_proyecto_tecnologia,
                                     tecnologia = t.nombre,
                                     p.usuario,
                                     p.id_proyecto,
                                     p.id_proyecto_estatus,
                                     est.estatus,
                                     p.id_proyecto_periodo,
                                     periodo = period.nombre,
                                     p.cveoport,
                                     p.folio_pmt,
                                     p.proyecto,
                                     p.descripcion,
                                     p.fecha_registro,
                                     p.fecha_inicio,
                                     p.fecha_fin
                                 }).ToArray();
                NAVISION dbnavision = new NAVISION();
                var results = from p in proyectos
                              join up in dbnavision.Employee on p.usuario.ToUpper().Trim() equals up.Usuario_Red.ToUpper().Trim()
                              select new
                              {
                                  usuario = p.usuario,
                                  empleado = up.First_Name.Trim() + " " + up.Last_Name.Trim(),
                                  p.id_proyecto,
                                  p.id_proyecto_estatus,
                                  p.estatus,
                                  p.id_proyecto_periodo,
                                  p.periodo,
                                  p.cveoport,
                                  p.folio_pmt,
                                  p.proyecto,
                                  p.descripcion,
                                  p.fecha_registro,
                                  p.fecha_inicio,
                                  p.fecha_fin,
                                  p.id_proyecto_tecnologia,
                                  p.tecnologia
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
                return dt;
            }
        }

        /// <summary>
        /// Regresa una tabla con todos los proyectos del empleado y sus subordinados, se manda el id del estatus que quiera filtrarse 1= abierto
        /// </summary>
        /// <param name="num_empleado"></param>
        /// <param name="ver_Todos_los_empleados"></param>
        /// <param name="id_proyecto_estatus"></param>
        /// <returns></returns>
        public DataTable SelectAll(int num_empleado, bool ver_Todos_los_empleados, int id_proyecto_estatus)
        {
            DataTable dt = new DataTable();
            try
            {
                EmpleadosCOM empleados = new EmpleadosCOM();
                bool no_activos = false;
                DataSet ds = empleados.sp_listado_empleados(num_empleado, ver_Todos_los_empleados, no_activos);
                DataTable dt_empleados_subordinados = ds.Tables[0];
                Proyectos_ConnextEntities db = new Proyectos_ConnextEntities();
                var proyectos = (from p in db.proyectos
                                 join emp in dt_empleados_subordinados.AsEnumerable() on p.usuario.ToUpper() equals emp.Field<string>("usuario").ToUpper()
                                 join est in db.proyectos_estatus on p.id_proyecto_estatus equals est.id_proyecto_estatus
                                 join period in db.proyectos_periodos on p.id_proyecto_periodo equals period.id_proyecto_periodo
                                 join t in db.proyectos_tecnologias on p.id_proyecto_tecnologia equals t.id_proyecto_tecnologia
                                 where (p.usuario_borrado == null && p.id_proyecto_estatus == id_proyecto_estatus) 
                                 select new
                                 {
                                     p.id_proyecto_tecnologia,
                                     tecnologia = t.nombre,
                                     p.usuario,
                                     p.id_proyecto,
                                     p.id_proyecto_estatus,
                                     est.estatus,
                                     p.id_proyecto_periodo,
                                     periodo = period.nombre,
                                     p.cveoport,
                                     p.folio_pmt,
                                     p.proyecto,
                                     p.descripcion,
                                     p.fecha_registro,
                                     p.fecha_inicio,
                                     p.fecha_fin
                                 }).ToArray();
                NAVISION dbnavision = new NAVISION();
                var results = from p in proyectos
                              join up in dbnavision.Employee on p.usuario.ToUpper().Trim() equals up.Usuario_Red.ToUpper().Trim()
                              select new
                              {
                                  usuario = p.usuario,
                                  empleado = up.First_Name.Trim() + " " + up.Last_Name.Trim(),
                                  p.id_proyecto,
                                  p.id_proyecto_estatus,
                                  p.estatus,
                                  p.id_proyecto_periodo,
                                  p.periodo,
                                  p.cveoport,
                                  p.folio_pmt,
                                  p.proyecto,
                                  p.descripcion,
                                  p.fecha_registro,
                                  p.fecha_inicio,
                                  p.fecha_fin,
                                  p.id_proyecto_tecnologia,
                                  p.tecnologia
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
                return dt;
            }
        }
    }
}
