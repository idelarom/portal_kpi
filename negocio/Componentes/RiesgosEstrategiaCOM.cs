using datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;

namespace negocio.Componentes
{
    public class RiesgosEstrategiaCOM
    {
        /// <summary>
        /// Agrega una instancia de riesgos_estrategia
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Agregar(riesgos_estrategia entidad)
        {
            try
            {
                string mess = "";
                if (Exist(entidad.nombre))
                {
                    mess = "Ya existe una estrategia llamado: " + entidad.nombre;
                }
                else
                {
                    riesgos_estrategia impacto = new riesgos_estrategia
                    {
                        nombre = entidad.nombre,
                        activo = true,
                        usuario = entidad.usuario.ToUpper(),
                        fecha = DateTime.Now
                    };
                    Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                    context.riesgos_estrategia.Add(impacto);
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
        /// Edita una instancia de riesgos_estrategia
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Editar(riesgos_estrategia entidad)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                riesgos_estrategia impacto = context.riesgos_estrategia
                                .First(i => i.id_riesgo_estrategia == entidad.id_riesgo_estrategia);
                impacto.nombre = entidad.nombre;
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
        /// Elimina una instancia de riesgos_estrategia
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Eliminar(int id_riesgo_estrategia)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                riesgos_estrategia periodo = context.riesgos_estrategia
                                .First(i => i.id_riesgo_estrategia == id_riesgo_estrategia);
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
                var query = context.riesgos_estrategia
                                .Where(s => s.nombre.ToUpper() == nombre.ToUpper() && s.activo)
                                .Select(u => new
                                {
                                    u.id_riesgo_estrategia
                                })
                                .OrderBy(u => u.id_riesgo_estrategia);
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
        /// Devuelve una instancia de la clase riesgos_estrategia
        /// </summary>
        /// <param name="id_proyecto_perido"></param>
        /// <returns></returns>
        public riesgos_estrategia estrategia(int id_riesgo_estrategia)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                riesgos_estrategia impacto = context.riesgos_estrategia
                                .First(i => i.id_riesgo_estrategia == id_riesgo_estrategia);
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
        /// Devuelve una tabla con los riesgos_estrategia
        /// </summary>
        /// <returns></returns>
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();

                var query = context.riesgos_estrategia
                                .Where(s => s.activo)
                                .Select(u => new
                                {
                                    u.id_riesgo_estrategia,
                                    u.nombre,
                                    u.activo,
                                    u.fecha,
                                    u.usuario
                                })
                                .OrderBy(u => u.nombre);
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
