﻿using datos;
using datos.NAVISION;
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
        public string Eliminar(string login)
        {
            try
            {
                SICOEMEntities sicoem = new SICOEMEntities();
                permissions_users_bonds_types permiso = sicoem.permissions_users_bonds_types
                                 .First(i => i.login.ToString() == login.ToString());
                permiso.Enabled = false;
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
        public bool Exist(string login)
        {
            DataTable dt = new DataTable();
            try
            {
                SICOEMEntities sicoem = new SICOEMEntities();
                var query = sicoem.permissions_users_bonds_types
                                .Where(s => s.login.ToUpper() == login.ToUpper() && s.Enabled == true)
                                .Select(u => new
                                {
                                    u.login
                                })
                                .OrderBy(u => u.login);
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
        public permissions_users_bonds_types Permisos(string login)
        {
            try
            {
                SICOEMEntities sicoem = new SICOEMEntities();
                permissions_users_bonds_types Permiso = sicoem.permissions_users_bonds_types
                                .First(i => i.login == login);
                return Permiso;

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

                var query = sicoem.permissions_users_bonds_types
                                .Where(s => s.Enabled == true)
                                .Select(u => new
                                {
                                    u.login,
                                    u.permission_request_bond,
                                    u.permision_authorization_bond,
                                    u.FiltroCC,
                                }).ToArray();
                NAVISION dbnavision = new NAVISION();
                var results = from p in query
                              join up in dbnavision.Employee on p.login.ToUpper().Trim() equals up.Usuario_Red.ToUpper().Trim()
                              select new
                              {
                                  p.login,
                                  empleado = up.First_Name.Trim() + " " + up.Last_Name.Trim(),
                                  p.permission_request_bond,
                                  p.permision_authorization_bond,
                                  p.FiltroCC,
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
                return new DataTable();
            }
        }
    }
}
