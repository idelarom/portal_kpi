using datos;
using datos.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;


namespace negocio.Componentes
{
    public class DocumentosCOM
    {
        /// <summary>
        /// Agrega una nuevo documento
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string[] Agregar(proyectos_documentos entidad)
        {
            string[] return_array = new string[2];
            try
            {
                proyectos_documentos documento = new proyectos_documentos
                {
                    id_proyecto = entidad.id_proyecto,
                    nombre = entidad.nombre,
                    tamaño = entidad.tamaño,
                    archivo_proyecto = entidad.archivo_proyecto,
                    extension = entidad.extension,
                    contentType = entidad.contentType,
                    archivo = entidad.archivo,
                    publico = entidad.publico,
                    fecha_registro = DateTime.Now,
                    usuario = entidad.usuario,
                    encuesta = entidad.encuesta,
                    documento_cierre = entidad.documento_cierre,
                    kit_cliente = entidad.kit_cliente,
                };
                Model context = new Model();
                context.proyectos_documentos.Add(documento);
                context.SaveChanges();
                int id_entity = documento.id_documento;
                return_array[0] = "";
                return_array[1] = id_entity.ToString();
                return return_array;
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                return_array[0] = fullErrorMessage.ToString();
                return_array[1] = 0.ToString();
                return return_array;
            }
        }

        /// <summary>
        /// Edita un documento
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Editar(proyectos_documentos entidad)
        {
            try
            {
                Model context = new Model();
                proyectos_documentos documento = context.proyectos_documentos
                                .First(i => i.id_documento == entidad.id_documento);

                documento.id_proyecto = entidad.id_proyecto;
                documento.nombre = entidad.nombre;
                documento.tamaño = entidad.tamaño;
                documento.extension = entidad.extension;
                documento.contentType = entidad.contentType;
                documento.archivo = entidad.archivo;
                documento.publico = entidad.publico;
                documento.fecha_edicion = DateTime.Now;
                documento.usuario_edicion = entidad.usuario_edicion;
                documento.usuario_borrado = null;
                documento.fecha_borrado = null;
                documento.comentarios_borrado = null;
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

        /// <summary>
        /// Borra un documento
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string Borrar(proyectos_documentos entidad)
        {
            try
            {
                Model context = new Model();
                proyectos_documentos entregable = context.proyectos_documentos
                                .First(i => i.id_documento == entidad.id_documento);
                entregable.fecha_borrado = DateTime.Now;
                entregable.usuario_borrado = entidad.usuario_borrado;
                entregable.comentarios_borrado = entidad.comentarios_borrado;
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

        /// <summary>
        /// Devuelve un DatatTable con los documentos
        /// </summary>
        /// <param name="id_proyect"></param>
        /// <returns></returns>
        public DataTable GetAll(int id_proyect)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.proyectos_documentos
                                .Where(s => s.id_proyecto == id_proyect && s.usuario_borrado == null)
                                .Select(u => new
                                {
                                    u.id_documento,
                                    u.id_proyecto,
                                    u.nombre,
                                    u.tamaño,
                                    u.extension,
                                    u.publico,
                                    u.fecha_registro,
                                    u.usuario
                                });
                dt = To.DataTable(query.ToList());
                dt.Columns.Add("icono");
                foreach (DataRow row in dt.Rows)
                {
                    string extension = row["extension"].ToString().Trim();
                    switch (extension.ToLower())
                    {
                        case ".xls":
                        case ".xlsx":
                            row["icono"] = "fa fa-file-excel-o";
                            break;

                        case ".doc":
                        case ".docx":
                            row["icono"] = "fa fa-file-word-o";
                            break;

                        case ".ppt":
                        case ".pptx":
                            row["icono"] = "fa fa-file-powerpoint-o";
                            break;

                        case ".pdf":
                            row["icono"] = "fa fa-file-pdf-o";
                            break;

                        case ".gif":
                        case ".jpg":
                        case ".jpeg":
                        case ".png":
                        case ".bmp":
                            row["icono"] = "fa fa-file-pdf-o";
                            break;

                        case ".mp3":
                        case ".wma":
                            row["icono"] = "fa fa-file-audio-o";
                            break;

                        case ".wmv":
                        case ".3gp":
                        case ".mp4":
                            row["icono"] = "fa fa-file-movie-o";
                            break;

                        case ".zip":
                        case ".rar":
                            row["icono"] = "fa fa-file-zip-o";
                            break;

                        default:
                            row["icono"] = "fa fa-file-archive-o";
                            break;
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        /// <summary>
        /// Devuelve un DatatTable con un documentos
        /// </summary>
        /// <param name="id_proyect"></param>
        /// <returns></returns>
        public DataTable Get(int id_documento)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.proyectos_documentos
                                .Where(s => s.id_documento == id_documento && s.usuario_borrado == null)
                                .Select(u => new
                                {
                                    u.id_documento,
                                    u.id_proyecto,
                                    u.nombre,
                                    u.archivo,
                                    u.contentType,
                                    u.tamaño,
                                    u.publico,
                                    u.fecha_registro,
                                    u.extension,
                                    u.usuario,
                                    u.usuario_edicion,
                                    u.fecha_edicion,
                                    u.usuario_borrado,
                                    u.fecha_borrado,
                                    u.comentarios_borrado
                                });
                dt = To.DataTable(query.ToList());
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }
        
        public DataSet sp_existe_documentacion_cierre(int id_proyecto)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_proyecto", SqlDbType = SqlDbType.Int, Value = id_proyecto });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_existe_documentacion_cierre", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}