using datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
namespace negocio.Componentes.Compensaciones
{
    public class PeriodicidadCOM
    {
        /// <summary>
        /// Agrega una instancia de bonds_types
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Agregar(periodicity entidad)
        {
            try
            {
                string mess = "";
                if (Exist(entidad.name))
                {
                    mess = "Ya existe un estatus llamado: " + entidad.name;
                }
                else
                {
                    periodicity periodicidad = new periodicity
                    {
                        name = entidad.name,
                        created = DateTime.Now,
                        created_by = entidad.created_by.ToUpper(),
                        enabled = true,
                    };
                    SICOEMEntities sicoem = new SICOEMEntities();
                    sicoem.periodicity.Add(periodicidad);
                    sicoem.SaveChanges();
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
        public string Editar(periodicity entidad)
        {
            try
            {
                SICOEMEntities sicoem = new SICOEMEntities();
                periodicity Periodicidad = sicoem.periodicity
                                .First(i => i.id_periodicity == entidad.id_periodicity);
                Periodicidad.name = entidad.name;
                sicoem.SaveChanges();
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
        public string Eliminar(int id_periodicity)
        {
            try
            {
                SICOEMEntities sicoem = new SICOEMEntities();
                periodicity Periodicidad = sicoem.periodicity
                                .First(i => i.id_periodicity == id_periodicity);
                Periodicidad.enabled = false;
                sicoem.SaveChanges();
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
        public bool Exist(string Periodicidad)
        {
            DataTable dt = new DataTable();
            try
            {
                SICOEMEntities sicoem = new SICOEMEntities();
                var query = sicoem.periodicity
                                .Where(s => s.name.ToUpper() == Periodicidad.ToUpper() && s.enabled == true)
                                .Select(u => new
                                {
                                    u.id_periodicity
                                })
                                .OrderBy(u => u.id_periodicity);
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
        public periodicity Periodicidad(int id_periodicity)
        {
            try
            {
                SICOEMEntities sicoem = new SICOEMEntities();
                periodicity Periodicidad = sicoem.periodicity
                                .First(i => i.id_periodicity == id_periodicity);
                return Periodicidad;
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
                SICOEMEntities sicoem = new SICOEMEntities();

                var query = sicoem.periodicity
                                .Where(s => s.enabled == true)
                                .Select(u => new
                                {
                                    u.id_periodicity,
                                    u.name,
                                    u.description
                                })
                                .OrderBy(u => u.id_periodicity);
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
