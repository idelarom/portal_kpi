using datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
namespace negocio.Componentes.Compensaciones
{
    public class PermisosUsuariosCOM
    {
        /// <summary>
        /// Agrega una instancia de bonds_types
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Agregar(permissions_users_bonds_types entidad)
        {
            try
            {
                string mess = "";
                if (Exist(entidad.login))
                {
                    mess = "Ya existe un estatus llamado: " + entidad.login;
                }
                else
                {
                    permissions_users_bonds_types permiso = new permissions_users_bonds_types
                    {
                        login = entidad.login,
                        permission_request_bond = entidad.permission_request_bond,
                        permision_authorization_bond = entidad.permision_authorization_bond,
                        FiltroCC = entidad.FiltroCC
                    };
                    SICOEMEntities sicoem = new SICOEMEntities();
                    sicoem.permissions_users_bonds_types.Add(permiso);
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
        public string Editar(permissions_users_bonds_types entidad)
        {
            try
            {
                SICOEMEntities sicoem = new SICOEMEntities();
                permissions_users_bonds_types permiso = sicoem.permissions_users_bonds_types
                                .First(i => i.login == entidad.login);
                permiso.login = entidad.login;
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
        public string Eliminar(int login)
        {
            try
            {
                SICOEMEntities sicoem = new SICOEMEntities();
                permissions_users_bonds_types permiso = sicoem.permissions_users_bonds_types
                                 .First(i => i.login == login);
                permiso.enabled = false;
                sicoem.SaveChanges();



                Emp e = (from permissions_users_bonds_types in sicoem.permissions_users_bonds_types

                         where permissions_users_bonds_types.Enabled == false
             select e1).First();
                //Change the Employee Name in memory
                e.Name = “Changed Name”;
                //Save to database
                ctx.SaveChanges();



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
        public bool Exist(string nameestatus)
        {
            DataTable dt = new DataTable();
            try
            {
                SICOEMEntities sicoem = new SICOEMEntities();
                var query = sicoem.requests_status
                                .Where(s => s.name.ToUpper() == nameestatus.ToUpper() && s.enabled == true)
                                .Select(u => new
                                {
                                    u.id_request_status
                                })
                                .OrderBy(u => u.id_request_status);
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
        public requests_status estatus(int id_request_status)
        {
            try
            {
                SICOEMEntities sicoem = new SICOEMEntities();
                requests_status estatus = sicoem.requests_status
                                .First(i => i.id_request_status == id_request_status);
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
                SICOEMEntities sicoem = new SICOEMEntities();

                var query = sicoem.requests_status
                                .Where(s => s.enabled == true)
                                .Select(u => new
                                {
                                    u.id_request_status,
                                    u.name,
                                    u.description
                                })
                                .OrderBy(u => u.id_request_status);
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
