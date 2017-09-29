using datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;

namespace negocio.Componentes
{
    public class ProyectosPeriodosCOM
    {
        /// <summary>
        /// Agrega una instancia de proyectos_periodos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Agregar(proyectos_periodos entidad)
        {
            try
            {
                string mess = "";
                if (Exist(entidad.nombre))
                {
                    mess = "Ya existe un periodo llamado: " + entidad.nombre;
                }
                else
                {
                    proyectos_periodos periodo = new proyectos_periodos
                    {
                        nombre = entidad.nombre,
                        dias = entidad.dias,
                        activo = true,
                        usuario = entidad.usuario.ToUpper(),
                        fecha = DateTime.Now
                    };
                    Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                    context.proyectos_periodos.Add(periodo);
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
        /// Edita una instancia de proyectos periodos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Editar(proyectos_periodos entidad)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                proyectos_periodos periodo = context.proyectos_periodos
                                .First(i => i.id_proyecto_perido == entidad.id_proyecto_perido);
                periodo.nombre = entidad.nombre;
                periodo.dias = entidad.dias;
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
        /// Elimina una instancia de proyectos periodos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Eliminar(int id_proyecto_perido)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                proyectos_periodos periodo = context.proyectos_periodos
                                .First(i => i.id_proyecto_perido == id_proyecto_perido);
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
        /// Devuelve un valor booleano si existe un periodo con el mismo nombre
        /// </summary>
        /// <param name="titulo"></param>
        /// <returns></returns>
        public bool Exist(string nombre)
        {
            DataTable dt = new DataTable();
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                var query = context.proyectos_periodos
                                .Where(s => s.nombre.ToUpper() == nombre.ToUpper() && s.activo)
                                .Select(u => new
                                {
                                    u.id_proyecto_perido
                                })
                                .OrderBy(u => u.id_proyecto_perido);
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
        /// Devuelve una instancia de la clase proyectos_periodos
        /// </summary>
        /// <param name="id_proyecto_perido"></param>
        /// <returns></returns>
        public proyectos_periodos proyectos_periodo(int id_proyecto_perido)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                proyectos_periodos proyectos_periodo = context.proyectos_periodos
                                .First(i => i.id_proyecto_perido == id_proyecto_perido);
                return proyectos_periodo;
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
        /// Devuelve una tabla con los periodos de los proyectos activos
        /// </summary>
        /// <returns></returns>
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();

                var query = context.proyectos_periodos
                                .Where(s => s.activo)
                                .Select(u => new
                                {
                                    u.id_proyecto_perido,
                                    u.nombre,
                                    u.dias
                                })
                                .OrderBy(u => u.dias);
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
