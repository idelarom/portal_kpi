using datos.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;

namespace negocio.Componentes
{
    public class AyudaCOM
    {
        /// <summary>
        /// Agrega un nuevo apartado de ayuda
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Agregar(ayudas entidad)
        {
            try
            {
                string mess = "";
                if (Exist(entidad.titulo))
                {
                    mess = "Ya existe un apartado de ayuda con el nombre " + entidad.titulo;
                }
                else
                {
                    ayudas ayuda = new ayudas
                    {
                        titulo = entidad.titulo,
                        id_ayuda_padre = entidad.id_ayuda_padre,
                        icono = entidad.icono,
                        descripcion = entidad.descripcion,
                        codigo_html = entidad.codigo_html,
                        src = entidad.src,
                        video = entidad .video,
                        activo = true,
                        usuario_creacion = entidad.usuario_creacion.ToUpper(),
                        fecha_creacion = DateTime.Now
                    };
                    Model context = new Model();
                    context.ayudas.Add(ayuda);
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
        /// Edita una ayuda
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Editar(ayudas entidad)
        {
            try
            {
                Model context = new Model();
                ayudas ayuda = context.ayudas
                                .First(i => i.id_ayuda == entidad.id_ayuda);
                ayuda.titulo = entidad.titulo;
                ayuda.id_ayuda_padre = entidad.id_ayuda_padre;
                ayuda.icono = entidad.icono;
                ayuda.descripcion = entidad.descripcion;
                ayuda.codigo_html = entidad.codigo_html;
                ayuda.src = entidad.src;
                ayuda.video = entidad.video;
                ayuda.fecha_edicion = DateTime.Now;
                ayuda.usuario_edicion = entidad.usuario_edicion;
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
        /// Elimina una ayuda
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Eliminar(ayudas entidad)
        {
            try
            {
                Model context = new Model();
                ayudas ayuda = context.ayudas
                                .First(i => i.id_ayuda == entidad.id_ayuda);
                ayuda.activo = false;
                ayuda.fecha_edicion = DateTime.Now;
                ayuda.usuario_edicion = entidad.usuario_edicion;
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
        /// Devuelve un valor booleano si existe una ayuda con el titulo especificado
        /// </summary>
        /// <param name="titulo"></param>
        /// <returns></returns>
        public bool Exist(string titulo)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.ayudas
                                .Where(s => s.titulo.ToUpper() == titulo.ToUpper() && s.activo)
                                .Select(u => new
                                {
                                    u.id_ayuda
                                })
                                .OrderBy(u => u.id_ayuda);
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
        /// Devuelve una instancia de Ayuda
        /// </summary>
        /// <param name="id_ayuda"></param>
        /// <returns></returns>
        public ayudas Ayuda(int id_ayuda)
        {
            try
            {
                Model context = new Model();
                ayudas ayuda = context.ayudas
                                .First(i => i.id_ayuda == id_ayuda);
                return ayuda;
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
        /// Devuelve una tabla con las ayudas activas
        /// </summary>
        /// <returns></returns>
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();

                var query = context.ayudas
                                .Where(s => s.activo)
                                .Select(u => new
                                {
                                    u.id_ayuda,
                                    u.id_ayuda_padre,
                                    u.titulo,
                                    u.icono,
                                    u.descripcion,
                                    u.src,
                                    u.codigo_html,
                                    u.video,
                                })
                                .OrderBy(u => u.titulo);
                dt = To.DataTable(query.ToList());
                foreach (DataRow row in dt.Rows)
                {
                    row["id_ayuda_padre"]= row["id_ayuda_padre"] == DBNull.Value ? 0 : row["id_ayuda_padre"];
                }
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
