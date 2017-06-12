using datos.Modelos;
using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;

namespace negocio.Componentes
{
    public class RolesCOM
    {
        public string Agregar(roles_proyecto entidad)
        {
            try
            {
                if (Exist(entidad))
                {
                    return "Ya existe un rol con el nombre: "+entidad.rol;
                }
                else
                {
                    roles_proyecto rol = new roles_proyecto
                    {
                        rol = entidad.rol,
                        responsabilidades = entidad.responsabilidades,
                        nivel = entidad.nivel,
                        fecha_registro = DateTime.Now,
                        usuario = entidad.usuario
                    };
                    Model context = new Model();
                    context.roles_proyecto.Add(rol);
                    context.SaveChanges();
                    return "";
                }
                
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                return fullErrorMessage.ToString();
            }
        }

        public string Editar(roles_proyecto entidad)
        {
            try
            {
                Model context = new Model();
                roles_proyecto rol = context.roles_proyecto
                                .First(i => i.id_rol == entidad.id_rol);
                rol.rol = entidad.rol;
                rol.nivel = entidad.nivel;
                rol.responsabilidades = entidad.responsabilidades;
                rol.fecha_borrado = null;
                rol.usuario_borrado = null;
                rol.comentarios_borrado = null;
                rol.usuario_edicion = entidad.usuario_edicion;
                context.SaveChanges();
                return "";
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                return fullErrorMessage.ToString();
            }
        }

        public string Borrar(roles_proyecto entidad)
        {
            try
            {
                if (ExistRolenUso(entidad))
                {
                    return "Este Rol esta siendo utilizado en un proyecto, por lo que NO PODRA SER ELIMINADO.";
                }
                else {

                    Model context = new Model();
                    roles_proyecto rol = context.roles_proyecto
                                    .First(i => i.id_rol == entidad.id_rol);
                    rol.usuario_borrado = entidad.usuario_borrado;
                    rol.comentarios_borrado = entidad.comentarios_borrado;
                    rol.fecha_borrado = DateTime.Now;
                    context.SaveChanges();
                    return "";
                }
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                return fullErrorMessage.ToString();
            }
        }

        /// <summary>
        /// Obtiene los roles
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public DataTable Get(roles_proyecto entidad)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.roles_proyecto
                                .Where(s => s.id_rol == (entidad.id_rol == 0 ? s.id_rol : entidad.id_rol) && s.usuario_borrado == null)
                                .Select(u => new
                                {
                                    u.id_rol,
                                    u.rol,
                                    u.nivel,
                                    u.usuario,
                                    u.responsabilidades,
                                    u.fecha_registro,
                                    u.usuario_borrado,
                                    u.fecha_borrado,
                                    u.usuario_edicion,
                                    u.fecha_edicion
                                })
                                .OrderBy(s => s.rol);
                dt = To.DataTable(query.ToList());
                dt.Columns.Add("en_uso");
                foreach (DataRow row in dt.Rows)
                {
                    roles_proyecto entidad_rol = new roles_proyecto();
                    entidad_rol.id_rol = Convert.ToInt32(row["id_rol"]);
                    row["en_uso"] = ExistRolenUso(entidad_rol);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        public Boolean ExistRolenUso(roles_proyecto entidad)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.proyectos_involucrados
                                .Where(s => s.id_rol == entidad.id_rol && s.usuario_borrado == null)
                                .Select(u => new
                                {
                                    u.id_rol
                                });
                dt = To.DataTable(query.ToList());
                return dt.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Boolean Exist(roles_proyecto entidad)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.roles_proyecto
                                .Where(s => s.rol.Trim().ToUpper()== entidad.rol.Trim().ToUpper() && s.usuario_borrado == null)
                                .Select(u => new
                                {
                                    u.id_rol
                                });
                dt = To.DataTable(query.ToList());
                return dt.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}