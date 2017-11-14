using datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;

namespace negocio.Componentes
{
    public class TipoBonosCOM
    {
        /// <summary>
        /// Agrega una instancia de bonds_types
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Agregar(bonds_types entidad)
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
                    bonds_types bono = new bonds_types
                    {
                        name = entidad.name,
                        created = DateTime.Now,
                        created_by = entidad.created_by.ToUpper(),
                        enabled = true,
                    };
                    SICOEMEntities sicoem = new SICOEMEntities();
                    sicoem.bonds_types.Add(bono);
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
        public string Editar(bonds_types entidad)
        {
            try
            {
                SICOEMEntities sicoem = new SICOEMEntities();
                bonds_types bono = sicoem.bonds_types
                                .First(i => i.id_bond_type == entidad.id_bond_type);
                bono.name = entidad.name;
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
        public string Eliminar(int id_bond_type)
        {
            try
            {
                SICOEMEntities sicoem = new SICOEMEntities();
                bonds_types bono = sicoem.bonds_types
                                .First(i => i.id_bond_type == id_bond_type);
                bono.enabled = false;
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
        public bool Exist(string bono)
        {
            DataTable dt = new DataTable();
            try
            {
                SICOEMEntities sicoem = new SICOEMEntities();
                var query = sicoem.bonds_types
                                .Where(s => s.name.ToUpper() == bono.ToUpper() && s.enabled==true)
                                .Select(u => new
                                {
                                    u.id_bond_type
                                })
                                .OrderBy(u => u.id_bond_type);
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
        public bonds_types bono(int id_bond_type)
        {
            try
            {
                SICOEMEntities sicoem = new SICOEMEntities();
                bonds_types bono = sicoem.bonds_types
                                .First(i => i.id_bond_type == id_bond_type);
                return bono;
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

                var query = sicoem.bonds_types
                                .Where(s => s.enabled==true)
                                .Select(u => new
                                {
                                    u.id_bond_type,
                                    u.name,
                                    u.description
                                })
                                .OrderBy(u => u.id_bond_type);
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
