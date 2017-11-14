using datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;

namespace negocio.Componentes.Compensaciones
{
    public class TipoComentariosPagoCOM
    {
        /// <summary>
        /// Agrega una instancia de bonds_types
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Agregar(comments_types_payments entidad)
        {
            try
            {
                string mess = "";
                if (Exist(entidad.description))
                {
                    mess = "Ya existe un estatus llamado: " + entidad.description;
                }
                else
                {
                    comments_types_payments comentario = new comments_types_payments
                    {
                        description = entidad.description,
                        created = DateTime.Now,
                        created_by = entidad.created_by.ToUpper(),
                        enabled = true,
                    };
                    SICOEMEntities sicoem = new SICOEMEntities();
                    sicoem.comments_types_payments.Add(comentario);
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
        public string Editar(comments_types_payments entidad)
        {
            try
            {
                SICOEMEntities sicoem = new SICOEMEntities();
                comments_types_payments bono = sicoem.comments_types_payments
                                .First(i => i.id_comment_type_payment == entidad.id_comment_type_payment);
                bono.description = entidad.description;
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
        public string Eliminar(int id_comment_type_payment)
        {
            try
            {
                SICOEMEntities sicoem = new SICOEMEntities();
                comments_types_payments bono = sicoem.comments_types_payments
                                .First(i => i.id_comment_type_payment == id_comment_type_payment);
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
        public bool Exist(string Comentario)
        {
            DataTable dt = new DataTable();
            try
            {
                SICOEMEntities sicoem = new SICOEMEntities();
                var query = sicoem.comments_types_payments
                                .Where(s => s.description.ToUpper() == Comentario.ToUpper() && s.enabled == true)
                                .Select(u => new
                                {
                                    u.id_comment_type_payment
                                })
                                .OrderBy(u => u.id_comment_type_payment);
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
        public comments_types_payments Comentario(int id_comment_type_payment)
        {
            try
            {
                SICOEMEntities sicoem = new SICOEMEntities();
                comments_types_payments Comentario = sicoem.comments_types_payments
                                .First(i => i.id_comment_type_payment == id_comment_type_payment);
                return Comentario;
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

                var query = sicoem.comments_types_payments
                                .Where(s => s.enabled == true)
                                .Select(u => new
                                {
                                    u.id_comment_type_payment,
                                    u.description
                                })
                                .OrderBy(u => u.id_comment_type_payment);
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
