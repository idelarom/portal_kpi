using datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;

namespace negocio.Componentes.Compensaciones
{
    public class EstatusSolicitudBonosCOM
    {
        /// <summary>
        /// Agrega una instancia de bonds_types
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Agregar(requests_status entidad)
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
                    requests_status estatus = new requests_status
                    {
                        description = entidad.description,
                        created = DateTime.Now,
                        created_by = entidad.created_by.ToUpper(),
                        enabled = true,
                    };
                    SICOEMEntities sicoem = new SICOEMEntities();
                    sicoem.requests_status.Add(estatus);
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
        public string Editar(requests_status entidad)
        {
            try
            {
                SICOEMEntities sicoem = new SICOEMEntities();
                requests_status bono = sicoem.requests_status
                                .First(i => i.id_request_status == entidad.id_request_status);
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
        public string Eliminar(int id_request_status)
        {
            try
            {
                SICOEMEntities sicoem = new SICOEMEntities();
                requests_status bono = sicoem.requests_status
                                .First(i => i.id_request_status == id_request_status);
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
