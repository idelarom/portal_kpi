using datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;

namespace negocio.Componentes.Compensaciones
{
    public class TipoBonosAutomaticosCOM
    {
        /// <summary>
        /// Agrega una instancia de bonds_types
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Agregar(bons_automatic_types entidad)
        { 
            try
            {
                string mess = "";
                if (Exist(entidad.NameBonds))
                {
                    mess = "Ya existe un estatus llamado: " + entidad.NameBonds;
                }
                else
                {
                    bons_automatic_types bono = new bons_automatic_types
                    {
                        NameBonds = entidad.NameBonds,
                        Created = DateTime.Now,
                        Create_by = entidad.Create_by.ToUpper(),
                        Enabled = true,
                    };
                    SICOEMEntities sicoem = new SICOEMEntities();
                    sicoem.bons_automatic_types.Add(bono);
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
        public string Editar(bons_automatic_types entidad)
        {
            try
            {
                SICOEMEntities sicoem = new SICOEMEntities();
                bons_automatic_types bono = sicoem.bons_automatic_types
                                .First(i => i.IdBonds == entidad.IdBonds);
                bono.NameBonds = entidad.NameBonds;
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
                bons_automatic_types bono = sicoem.bons_automatic_types
                                .First(i => i.IdBonds == id_bond_type);
                bono.Enabled = false;
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
                var query = sicoem.bons_automatic_types
                                .Where(s => s.NameBonds.ToUpper() == bono.ToUpper() && s.Enabled == true)
                                .Select(u => new
                                {
                                    u.IdBonds
                                })
                                .OrderBy(u => u.IdBonds);
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
        /// <param name="idbonds"></param>
        /// <returns></returns>
        public bons_automatic_types bono(int id_bond_type)
        {
            try
            {
                SICOEMEntities sicoem = new SICOEMEntities();
                bons_automatic_types bono = sicoem.bons_automatic_types
                                .First(i => i.IdBonds == id_bond_type);
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

                var query = sicoem.bons_automatic_types
                                .Where(s => s.Enabled == true)
                                .Select(u => new
                                {
                                    u.IdBonds,
                                    u.NameBonds
                                })
                                .OrderBy(u => u.IdBonds);
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
