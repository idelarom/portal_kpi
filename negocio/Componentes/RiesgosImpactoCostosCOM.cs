using datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;

namespace negocio.Componentes
{
    public class RiesgosImpactoCostosCOM
    {
        /// <summary>
        /// Agrega una instancia de riesgos_impactos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Agregar(riesgos_impactos entidad)
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
                    riesgos_impactos impacto = new riesgos_impactos
                    {
                        nombre = entidad.nombre,
                        valor = entidad.valor,
                        activo = true,
                        usuario = entidad.usuario.ToUpper(),
                        fecha = DateTime.Now
                    };
                    Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                    context.riesgos_impactos.Add(impacto);
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
        /// Edita una instancia de riesgos_impactos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Editar(riesgos_impactos entidad)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                riesgos_impactos impacto = context.riesgos_impactos
                                .First(i => i.id_riesgo_impacto == entidad.id_riesgo_impacto);
                impacto.nombre = entidad.nombre;
                impacto.valor = entidad.valor;
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
        /// Elimina una instancia de riesgos_impactos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Eliminar(int id_riesgo_impacto)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                riesgos_impactos periodo = context.riesgos_impactos
                                .First(i => i.id_riesgo_impacto == id_riesgo_impacto);
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
                var query = context.riesgos_impactos
                                .Where(s => s.nombre.ToUpper() == nombre.ToUpper() && s.activo)
                                .Select(u => new
                                {
                                    u.id_riesgo_impacto
                                })
                                .OrderBy(u => u.id_riesgo_impacto);
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
        /// Devuelve una instancia de la clase riesgos_impactos
        /// </summary>
        /// <param name="id_proyecto_perido"></param>
        /// <returns></returns>
        public riesgos_impactos impacto(int id_riesgo_impacto)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                riesgos_impactos impacto = context.riesgos_impactos
                                .First(i => i.id_riesgo_impacto == id_riesgo_impacto);
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
        /// Devuelve una tabla con los riesgos_impactos
        /// </summary>
        /// <returns></returns>
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();

                var query = context.riesgos_impactos
                                .Where(s => s.activo)
                                .Select(u => new
                                {
                                    u.id_riesgo_impacto,
                                    u.nombre,                                    
                                    u.valor,
                                    u.activo,
                                    u.fecha,
                                    u.usuario
                                })
                                .OrderBy(u => u.valor);
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
