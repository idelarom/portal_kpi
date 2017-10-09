using datos;
using datos.NAVISION;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
namespace negocio.Componentes
{
    public class DocumentosCOM
    {
        public string Agregar(documentos entidad)
        {
            try
            {
                string mess = "";
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                documentos documento = new documentos();
                documento.id_actividad = entidad.id_actividad;
                documento.id_proyecto = entidad.id_proyecto;
                documento.path = entidad.path;
                documento.nombre = entidad.nombre;
                documento.tamaño = entidad.tamaño;
                documento.publico = true;
                documento.extension = entidad.extension;
                documento.contentType = entidad.contentType;
                documento.fecha = DateTime.Now;
                documento.usuario =entidad.usuario;
                documento.id_documento_tipo = entidad.id_documento_tipo;
                context.documentos.Add(documento);
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
        /// Devuelve una instancia de la clase documentos
        /// </summary>
        /// <param name="id_proyecto_perido"></param>
        /// <returns></returns>
        public documentos documento(int id_documento)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                documentos documento = context.documentos
                                .First(i => i.id_documento == id_documento);
                return documento;
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

        public documentos documento_actividad(int id_actividad)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                documentos documento = context.documentos
                                .First(i => i.id_actividad == id_actividad);
                return documento;
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
