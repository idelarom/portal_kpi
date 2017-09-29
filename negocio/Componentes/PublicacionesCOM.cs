using datos.Model;
using datos.NAVISION;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;

namespace negocio.Componentes
{
    public class PublicacionesCOM
    {
        /// <summary>
        /// Agrega una nueva publicacion
        /// </summary>
        /// <param name="entidad"></param>
        /// <param name="entidad_imagenes"></param>
        /// <returns></returns>
        public string Agregar(publicaciones entidad, List<publicaciones_imagenes> entidad_imagenes)
        {
            try
            {
                string mess = "";
                if (Exist(entidad.titulo))
                {
                    mess = "Ya existe una publicación con el titulo: " + entidad.titulo;
                }
                else
                {
                    publicaciones publicacion = new publicaciones
                    {
                        titulo = entidad.titulo,
                        descripcion = entidad.descripcion,
                        activo = true,
                        usuario = entidad.usuario.ToUpper(),
                        fecha = DateTime.Now
                    };
                    Model context = new Model();
                    context.publicaciones.Add(publicacion);
                    int id_entity = publicacion.id_publicacion;
                    foreach (publicaciones_imagenes publicaciones_imagen in entidad_imagenes)
                    {
                        publicaciones_imagenes imagen = new publicaciones_imagenes
                        {
                            id_publicacion = id_entity,
                            path = publicaciones_imagen.path,
                            extension = publicaciones_imagen.extension,
                            activo = true,
                            usuario = entidad.usuario.ToUpper(),
                            fecha = DateTime.Now
                        };
                        context.publicaciones_imagenes.Add(imagen);
                    }
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
        /// Elimina una publicacion
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Eliminar(publicaciones entidad)
        {
            try
            {
                Model context = new Model();
                publicaciones publicacion = context.publicaciones
                                .First(i => i.id_publicacion == entidad.id_publicacion);
                publicacion.activo = false;
                ICollection<publicaciones_imagenes> lstimagenes = publicacion.publicaciones_imagenes;
                foreach (publicaciones_imagenes imagen in lstimagenes)
                {
                    imagen.activo = false;
                }
                ICollection<publicaciones_comentarios> lstcomentarios = publicacion.publicaciones_comentarios;
                foreach (publicaciones_comentarios comentario in lstcomentarios)
                {
                    comentario.activo = false;
                }
                ICollection<publicaciones_likes> lstlikes = publicacion.publicaciones_likes;
                foreach (publicaciones_likes like in lstlikes)
                {
                    like.activo = false;
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
        /// Agrega un comentario a una publicacion
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string AgregarComentario(publicaciones_comentarios entidad)
        {
            try
            {
                string mess = "";
                publicaciones_comentarios comentario = new publicaciones_comentarios
                {
                    id_publicacion = entidad.id_publicacion,
                    comentario = entidad.comentario,
                    activo = true,
                    usuario = entidad.usuario.ToUpper(),
                    fecha = DateTime.Now
                };
                Model context = new Model();
                context.publicaciones_comentarios.Add(comentario);
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

        public string EliminarComentario(publicaciones_comentarios entidad)
        {
            try
            {
                Model context = new Model();
                publicaciones_comentarios publicacion = context.publicaciones_comentarios
                                .First(i => i.id_publicacionc == entidad.id_publicacionc);
                publicacion.activo = false;
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
        /// Agrega un like a una publicacion
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string AgregarLike(publicaciones_likes entidad)
        {
            try
            {
                string mess = "";
                publicaciones_likes like = new publicaciones_likes
                {
                    id_publicacion= entidad.id_publicacion,
                    activo = true,
                    usuario = entidad.usuario.ToUpper(),
                    fecha = DateTime.Now
                };
                Model context = new Model();
                context.publicaciones_likes.Add(like);
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

        public string EliminarLike(publicaciones_likes entidad)
        {
            try
            {
                Model context = new Model();
                publicaciones_likes publicacion = context.publicaciones_likes
                                .First(i => i.id_publicacionlike == entidad.id_publicacionlike);
                publicacion.activo = false;
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
        /// Devuelve un valor booleano si existe una publicacion con el titulo especificado
        /// </summary>
        /// <param name="titulo"></param>
        /// <returns></returns>
        public bool Exist(string titulo)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.publicaciones
                                .Where(s => s.titulo.ToUpper() == titulo.ToUpper() && s.activo)
                                .Select(u => new
                                {
                                    u.id_publicacion
                                })
                                .OrderBy(u => u.id_publicacion);
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

        public DataSet SelectAll()
        {
            DataSet ds = new DataSet();
            try
            {
                Model db = new Model();

                var publicaciones = from p in db.publicaciones
                              where (p.activo == true)
                              select new { p.id_publicacion, p.titulo, p.descripcion, p.fecha, fecha_str = p.fecha.ToString(), p.usuario };
                NAVISION dbnavision = new NAVISION();
                var results = from p in publicaciones
                              join up in dbnavision.Employee on p.usuario.ToUpper().Trim() equals up.Usuario_Red.ToUpper().Trim()
                              select new {
                                  p.id_publicacion,
                                  p.titulo,
                                  p.descripcion,
                                  p.fecha,
                                  p.fecha_str,
                                  descripcion_corta= p.descripcion.Substring(0,100),
                                  usuario = p.usuario,
                                  nombre = up.First_Name.Trim() + " " + up.Last_Name.Trim() };
                DataTable dt_publicaciones = new DataTable();
                dt_publicaciones = To.DataTable(results.ToList());
                ds.Tables.Add(dt_publicaciones);

                var comentarios = from p in db.publicaciones_comentarios
                                  join pub in results on p.id_publicacion equals pub.id_publicacion
                                  where (p.activo == true)
                                    select new { p.id_publicacionc, p.comentario, p.fecha, fecha_str = p.fecha.ToString(), p.usuario };
          
                var results2 = from p in comentarios
                               join up in dbnavision.Employee on p.usuario.ToUpper().Trim() equals up.Usuario_Red.ToUpper().Trim()
                              select new
                              {
                                  p.id_publicacionc,
                                  p.comentario,
                                  p.fecha,
                                  p.fecha_str,
                                  descripcion_corta = p.comentario.Substring(0, 100),
                                  usuario = p.usuario,
                                  nombre = up.First_Name.Trim() + " " + up.Last_Name.Trim()
                              };
                DataTable dt_comentarios = new DataTable();
                dt_comentarios = To.DataTable(results2.ToList());
                ds.Tables.Add(dt_comentarios);

                var publicaciones_likes = from p in db.publicaciones_likes
                                          join pub in results on p.id_publicacion equals pub.id_publicacion
                                          where (p.activo == true)
                                  select new { p.id_publicacionlike, p.fecha, fecha_str = p.fecha.ToString(), p.usuario };

                var results3 = from p in publicaciones_likes
                               join up in dbnavision.Employee on p.usuario.ToUpper().Trim() equals up.Usuario_Red.ToUpper().Trim()
                               select new
                               {
                                   p.id_publicacionlike,
                                   p.fecha,
                                   p.fecha_str,
                                   usuario = p.usuario,
                                   nombre = up.First_Name.Trim() + " " + up.Last_Name.Trim()
                               };
                DataTable dt_likes = new DataTable();
                dt_comentarios = To.DataTable(results3.ToList());
                ds.Tables.Add(dt_likes);

                return ds;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                          .SelectMany(x => x.ValidationErrors)
                          .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                return new DataSet();
            }
        }

        public DataSet SelectForUser(string usuario)
        {
            DataSet ds = new DataSet();
            try
            {
                Model db = new Model();

                var publicaciones = (from p in db.publicaciones
                                    where (p.activo == true & p.usuario.ToUpper() == usuario.ToUpper())
                                    orderby(p.fecha)
                                    select new { p.id_publicacion, p.titulo, p.descripcion, p.fecha, fecha_str = p.fecha.ToString(), p.usuario }).ToArray();

                NAVISION dbnavision = new NAVISION();
                var results = from p in publicaciones
                              join up in dbnavision.Employee on p.usuario.ToUpper().Trim() equals up.Usuario_Red.ToUpper().Trim()
                              select new
                              {
                                  p.id_publicacion,
                                  p.titulo,
                                  p.descripcion,
                                  p.fecha,
                                  p.fecha_str,
                                  descripcion_corta = p.descripcion.Substring(0, p.descripcion.Length >200 ? 100:p.descripcion.Length),
                                  usuario = p.usuario,
                                  imagen_usuario = p.usuario+".png",
                                  nombre = up.First_Name.Trim() + " " + up.Last_Name.Trim()
                              };
                DataTable dt_publicaciones = new DataTable();
                dt_publicaciones = To.DataTable(results.ToList());
                ds.Tables.Add(dt_publicaciones);

                //var comentarios = (from p in db.publicaciones_comentarios
                //                  join pub in results on p.id_publicacion equals pub.id_publicacion
                //                  where (p.activo == true)
                //                  select new { p.id_publicacionc, p.comentario, p.fecha, fecha_str = p.fecha.ToString(), p.usuario }).ToArray(); 

                //var results2 = from p in comentarios
                //               join up in dbnavision.Employee on p.usuario.ToUpper().Trim() equals up.Usuario_Red.ToUpper().Trim()
                //               select new
                //               {
                //                   p.id_publicacionc,
                //                   p.comentario,
                //                   p.fecha,
                //                   p.fecha_str,
                //                   descripcion_corta = p.comentario.Substring(0, 100),
                //                   usuario = p.usuario,
                //                   nombre = up.First_Name.Trim() + " " + up.Last_Name.Trim()
                //               };
                //DataTable dt_comentarios = new DataTable();
                //dt_comentarios = To.DataTable(results2.ToList());
                //ds.Tables.Add(dt_comentarios);

                //var publicaciones_likes = (from p in db.publicaciones_likes
                //                          join pub in results on p.id_publicacion equals pub.id_publicacion
                //                          where (p.activo == true)
                //                          select new { p.id_publicacionlike, p.fecha, fecha_str = p.fecha.ToString(), p.usuario }).ToArray(); 

                //var results3 = from p in publicaciones_likes
                //               join up in dbnavision.Employee on p.usuario.ToUpper().Trim() equals up.Usuario_Red.ToUpper().Trim()
                //               select new
                //               {
                //                   p.id_publicacionlike,
                //                   p.fecha,
                //                   p.fecha_str,
                //                   usuario = p.usuario,
                //                   nombre = up.First_Name.Trim() + " " + up.Last_Name.Trim()
                //               };
                //DataTable dt_likes = new DataTable();
                //dt_comentarios = To.DataTable(results3.ToList());
                //ds.Tables.Add(dt_likes);

                return ds;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                          .SelectMany(x => x.ValidationErrors)
                          .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                return new DataSet();
            }
        }

        public DataSet SelectAllToday()
        {
            DataSet ds = new DataSet();
            try
            {
                Model db = new Model();

                var publicaciones = from p in db.publicaciones
                                    where (p.activo == true & p.fecha > DateTime.Today.AddDays(-1) & p.fecha < DateTime.Today.AddDays(1))
                                    select new { p.id_publicacion, p.titulo, p.descripcion, p.fecha, fecha_str = p.fecha.ToString(), p.usuario };
                NAVISION dbnavision = new NAVISION();
                var results = from p in publicaciones
                              join up in dbnavision.Employee on p.usuario.ToUpper().Trim() equals up.Usuario_Red.ToUpper().Trim()
                              select new
                              {
                                  p.id_publicacion,
                                  p.titulo,
                                  p.descripcion,
                                  p.fecha,
                                  p.fecha_str,
                                  descripcion_corta = p.descripcion.Substring(0, 100),
                                  usuario = p.usuario,
                                  nombre = up.First_Name.Trim() + " " + up.Last_Name.Trim()
                              };
                DataTable dt_publicaciones = new DataTable();
                dt_publicaciones = To.DataTable(results.ToList());
                ds.Tables.Add(dt_publicaciones);

                var comentarios = from p in db.publicaciones_comentarios
                                  join pub in results on p.id_publicacion equals pub.id_publicacion
                                  where (p.activo == true)
                                  select new { p.id_publicacionc, p.comentario, p.fecha, fecha_str = p.fecha.ToString(), p.usuario };

                var results2 = from p in comentarios
                               join up in dbnavision.Employee on p.usuario.ToUpper().Trim() equals up.Usuario_Red.ToUpper().Trim()
                               select new
                               {
                                   p.id_publicacionc,
                                   p.comentario,
                                   p.fecha,
                                   p.fecha_str,
                                   descripcion_corta = p.comentario.Substring(0, 100),
                                   usuario = p.usuario,
                                   nombre = up.First_Name.Trim() + " " + up.Last_Name.Trim()
                               };
                DataTable dt_comentarios = new DataTable();
                dt_comentarios = To.DataTable(results2.ToList());
                ds.Tables.Add(dt_comentarios);

                var publicaciones_likes = from p in db.publicaciones_likes
                                          join pub in results on p.id_publicacion equals pub.id_publicacion
                                          where (p.activo == true)
                                          select new { p.id_publicacionlike, p.fecha, fecha_str = p.fecha.ToString(), p.usuario };

                var results3 = from p in publicaciones_likes
                               join up in dbnavision.Employee on p.usuario.ToUpper().Trim() equals up.Usuario_Red.ToUpper().Trim()
                               select new
                               {
                                   p.id_publicacionlike,
                                   p.fecha,
                                   p.fecha_str,
                                   usuario = p.usuario,
                                   nombre = up.First_Name.Trim() + " " + up.Last_Name.Trim()
                               };
                DataTable dt_likes = new DataTable();
                dt_comentarios = To.DataTable(results3.ToList());
                ds.Tables.Add(dt_likes);

                return ds;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                          .SelectMany(x => x.ValidationErrors)
                          .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                return new DataSet();
            }
        }
    }
}
