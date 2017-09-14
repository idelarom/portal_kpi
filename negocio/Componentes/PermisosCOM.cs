using datos.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;

namespace negocio.Componentes
{
    public class PermisosCOM
    {
        /// <summary>
        /// Agrega un nuevo Permiso
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Agregar(permisos entidad)
        {
            try
            {
                string mess = "";
                if (Exist(entidad.permiso))
                {
                    mess = "Ya existe un permiso con el nombre " + entidad.permiso;
                }
                else
                {
                    permisos permiso = new permisos
                    {
                        permiso = entidad.permiso,
                        activo = true,
                        usuario_creacion = entidad.usuario_creacion.ToUpper(),
                        fecha_creacion = DateTime.Now
                    };
                    Model context = new Model();
                    context.permisos.Add(permiso);
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
        /// Edita un permiso
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Editar(permisos entidad)
        {
            try
            {
                Model context = new Model();
                permisos permiso = context.permisos
                                .First(i => i.id_permiso == entidad.id_permiso);
                permiso.permiso = entidad.permiso;
                permiso.fecha_edicion = DateTime.Now;
                permiso.usuario_edicion = permiso.usuario_edicion;
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
        /// Elimina un permiso, tambien elimina las relaciones a usuarios y grupos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Eliminar(permisos entidad)
        {
            try
            {
                Model context = new Model();
                permisos permiso = context.permisos
                                .First(i => i.id_permiso == entidad.id_permiso);
                permiso.activo = false;
                permiso.fecha_edicion = DateTime.Now;
                permiso.usuario_edicion = entidad.usuario_edicion;
                ICollection<usuarios_permisos> list_usuarios_permisos = permiso.usuarios_permisos;
                foreach (usuarios_permisos usuario_permiso in list_usuarios_permisos)
                {
                    usuario_permiso.activo = false;
                }
                ICollection<grupos_permisos> list_grupos_permisos = permiso.grupos_permisos;
                foreach (grupos_permisos grupos_permiso in list_grupos_permisos)
                {
                    grupos_permiso.activo = false;
                }
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
        /// Devuelve si existe un permiso con el mismo nombre
        /// </summary>
        /// <param name="permiso"></param>
        /// <returns></returns>
        public bool Exist(string permiso)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.permisos
                                .Where(s => s.permiso.ToUpper() == permiso.ToUpper() && s.activo)
                                .Select(u => new
                                {
                                    u.id_permiso
                                })
                                .OrderBy(u => u.id_permiso);
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
        /// Devuelve una tabla con todos los permisos activos
        /// </summary>
        /// <param name="permiso"></param>
        /// <returns></returns>
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();

                var query = context.permisos
                                .Where(s => s.activo)
                                .Select(u => new
                                {
                                    u.id_permiso,
                                    u.permiso,
                                    u.activo,
                                    u.usuario_creacion,
                                    usuarios_permisos = u.usuarios_permisos.Count,
                                    grupos_permisos = u.grupos_permisos.Count,
                                    perfiles_permisos = u.perfiles_permisos.Count
                                })
                                .OrderBy(u => u.permiso);
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

        /// <summary>
        /// Devuelve una entidad permisos,filtrada por el id
        /// </summary>
        /// <param name="id_permiso"></param>
        /// <returns></returns>
        public permisos SelectId(int id_permiso)
        {
            try
            {
                Model context = new Model();
                permisos permiso = context.permisos
                                .First(i => i.id_permiso == id_permiso);
                //ICollection<usuarios_permisos> list_usuarios_permisos = permiso.usuarios_permisos;
                //foreach (usuarios_permisos usuario_permiso in list_usuarios_permisos)
                //{
                //    usuario_permiso.activo = false;
                //}
                //ICollection<grupos_permisos> list_grupos_permisos = permiso.grupos_permisos;
                //foreach (grupos_permisos grupos_permiso in list_grupos_permisos)
                //{
                //    grupos_permiso.activo = false;
                //}
                return permiso;
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
        /// Devuelve una entidad permisos,filtrada por el nombre
        /// </summary>
        /// <param name="permiso_"></param>
        /// <returns></returns>
        public permisos SelectName(string permiso_)
        {
            try
            {
                Model context = new Model();
                permisos permiso = context.permisos
                                .First(i => i.permiso.ToUpper() == permiso_.ToUpper());
                return permiso;
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
        /// Devuelve una coleccion de los usuarios que tienen el permiso especificados
        /// </summary>
        /// <param name="id_permiso"></param>
        /// <returns></returns>
        public ICollection<usuarios_permisos> usuarios_permisos(int id_permiso)
        {
            try
            {
                Model context = new Model();
                permisos permiso = context.permisos
                                .First(i => i.id_permiso == id_permiso);
                ICollection<usuarios_permisos> list_usuarios_permisos = permiso.usuarios_permisos;

                return list_usuarios_permisos;
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
        /// Devuelve una coleccion de los grupos que tienen el permiso especificados
        /// </summary>
        /// <param name="id_permiso"></param>
        /// <returns></returns>
        public ICollection<grupos_permisos> grupos_permisos(int id_permiso)
        {
            try
            {
                Model context = new Model();
                permisos permiso = context.permisos
                                .First(i => i.id_permiso == id_permiso);
                ICollection<grupos_permisos> list_usuarios_permisos = permiso.grupos_permisos;

                return list_usuarios_permisos;
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
        /// Devuelve una coleccion de los perfiles que tienen el permiso especificados
        /// </summary>
        /// <param name="id_permiso"></param>
        /// <returns></returns>
        public ICollection<perfiles_permisos> perfiles_permisos(int id_permiso)
        {
            try
            {
                Model context = new Model();
                permisos permiso = context.permisos
                                .First(i => i.id_permiso == id_permiso);
                ICollection<perfiles_permisos> list_usuarios_permisos = permiso.perfiles_permisos;

                return list_usuarios_permisos;
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
    }
}