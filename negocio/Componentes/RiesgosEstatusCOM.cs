using datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;

namespace negocio.Componentes
{
    public class RiesgosEstatusCOM
    {
        /// <summary>
        /// Agrega una instancia de riesgos_estatus
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Agregar(riesgos_estatus entidad)
        {
            try
            {
                string mess = "";
                if (Exist(entidad.estatus))
                {
                    mess = "Ya existe un estatus llamado: " + entidad.estatus;
                }
                else
                {
                    riesgos_estatus estatus = new riesgos_estatus
                    {
                        estatus = entidad.estatus,
                        activo = true,
                        usuario = entidad.usuario.ToUpper(),
                        fecha = DateTime.Now
                    };
                    Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                    context.riesgos_estatus.Add(estatus);
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
        /// Edita una instancia de proyectos estatus
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Editar(riesgos_estatus entidad)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                riesgos_estatus estatus = context.riesgos_estatus
                                .First(i => i.id_riesgos_estatus == entidad.id_riesgos_estatus);
                estatus.estatus = entidad.estatus;
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
        /// Elimina una instancia de riesgos_estatus
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Eliminar(int id_riesgos_estatus)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                riesgos_estatus periodo = context.riesgos_estatus
                                .First(i => i.id_riesgos_estatus == id_riesgos_estatus);
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
        /// Devuelve un valor booleano si existe un estatus con el mismo nombre
        /// </summary>
        /// <param name="titulo"></param>
        /// <returns></returns>
        public bool Exist(string estatus)
        {
            DataTable dt = new DataTable();
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                var query = context.riesgos_estatus
                                .Where(s => s.estatus.ToUpper() == estatus.ToUpper() && s.activo)
                                .Select(u => new
                                {
                                    u.id_riesgos_estatus
                                })
                                .OrderBy(u => u.id_riesgos_estatus);
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
        /// Devuelve una instancia de la clase riesgos_estatus
        /// </summary>
        /// <param name="id_proyecto_perido"></param>
        /// <returns></returns>
        public riesgos_estatus estatus(int id_riesgos_estatus)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                riesgos_estatus estatus = context.riesgos_estatus
                                .First(i => i.id_riesgos_estatus == id_riesgos_estatus);
                return estatus;
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
        /// Devuelve una tabla con los estatus de los proyectos activos
        /// </summary>
        /// <returns></returns>
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();

                var query = context.riesgos_estatus
                                .Where(s => s.activo)
                                .Select(u => new
                                {
                                    u.id_riesgos_estatus,
                                    u.estatus
                                })
                                .OrderBy(u => u.estatus);
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
