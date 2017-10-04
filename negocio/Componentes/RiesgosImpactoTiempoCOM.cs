using datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;

namespace negocio.Componentes
{
    public class RiesgosImpactoTiempoCOM
    {

        /// <summary>
        /// Agrega una instancia de riesgos_impacto_tiempo
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Agregar(riesgos_impacto_tiempo entidad)
        {
            try
            {
                string mess = "";
                if (Exist(entidad.nombre))
                {
                    mess = "Ya existe un impacto llamado: " + entidad.nombre;
                }
                else
                {
                    riesgos_impacto_tiempo impacto = new riesgos_impacto_tiempo
                    {
                        nombre = entidad.nombre,
                        porcentaje = entidad.porcentaje,
                        activo = true,
                        usuario = entidad.usuario.ToUpper(),
                        fecha = DateTime.Now
                    };
                    Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                    context.riesgos_impacto_tiempo.Add(impacto);
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
        /// Edita una instancia de riesgos_impacto_tiempo
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Editar(riesgos_impacto_tiempo entidad)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                riesgos_impacto_tiempo impacto = context.riesgos_impacto_tiempo
                                .First(i => i.id_riesgo_impacto_tiempo == entidad.id_riesgo_impacto_tiempo);
                impacto.nombre = entidad.nombre;
                impacto.porcentaje = entidad.porcentaje;
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
        /// Elimina una instancia de riesgos_impacto_tiempo
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Eliminar(int id_riesgo_impacto_tiempo)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                riesgos_impacto_tiempo periodo = context.riesgos_impacto_tiempo
                                .First(i => i.id_riesgo_impacto_tiempo == id_riesgo_impacto_tiempo);
                periodo.activo = false;
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
        /// Devuelve un valor booleano si existe un impacto con el mismo nombre
        /// </summary>
        /// <param name="titulo"></param>
        /// <returns></returns>
        public bool Exist(string nombre)
        {
            DataTable dt = new DataTable();
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                var query = context.riesgos_impacto_tiempo
                                .Where(s => s.nombre.ToUpper() == nombre.ToUpper() && s.activo)
                                .Select(u => new
                                {
                                    u.id_riesgo_impacto_tiempo
                                })
                                .OrderBy(u => u.id_riesgo_impacto_tiempo);
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
        /// Devuelve una instancia de la clase riesgos_impacto_tiempo
        /// </summary>
        /// <param name="id_proyecto_perido"></param>
        /// <returns></returns>
        public riesgos_impacto_tiempo impacto(int id_riesgo_impacto_tiempo)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                riesgos_impacto_tiempo impacto = context.riesgos_impacto_tiempo
                                .First(i => i.id_riesgo_impacto_tiempo == id_riesgo_impacto_tiempo);
                return impacto;
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
        /// Devuelve una tabla con los riesgos_impacto_tiempo
        /// </summary>
        /// <returns></returns>
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();

                var query = context.riesgos_impacto_tiempo
                                .Where(s => s.activo)
                                .Select(u => new
                                {
                                    u.id_riesgo_impacto_tiempo,
                                    u.nombre,
                                    u.porcentaje,
                                    u.activo,
                                    u.fecha,
                                    u.usuario
                                })
                                .OrderBy(u => u.porcentaje);
                dt = To.DataTable(query.ToList());
                return dt;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                          .SelectMany(x => x.ValidationErrors)
                          .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                return new DataTable();
            }
        }
    }
}
