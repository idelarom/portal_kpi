using datos.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;

namespace negocio.Componentes
{
    public class UsuariosConfiguracionesCOM
    {
        /// <summary>
        /// Agrega un usuario a la tabla de configuraciones
        /// </summary>
        /// <param name="entidad"></param>
        /// <param name="usuarios_adicionales"></param>
        /// <returns></returns>
        public string Agregar(usuarios_configuraciones entidad)
        {
            try
            {
                usuarios_configuraciones usuario = new usuarios_configuraciones
                {
                    usuario = entidad.usuario,
                    nombre = entidad.nombre,
                    alerta_inicio_sesion=true,
                    mostrar_recordatorios =true,
                    sincronizacion_automatica = true,
                    activo = true
                };
                Model context = new Model();
                context.usuarios_configuraciones.Add(usuario);
                int id_entity = usuario.id_usuarioc;
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
        /// Edita un el nombre a visualizar de un usuario
        /// </summary>
        /// <param name="entidad"></param>
        /// <param name="usuarios_adicionales"></param>
        /// <returns></returns>
        public string EditarNombre(usuarios_configuraciones entidad)
        {
            try
            {
                Model context = new Model();
                usuarios_configuraciones usuario = context.usuarios_configuraciones
                                .First(i => i.usuario == entidad.usuario);
                usuario.nombre = entidad.nombre;
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
        /// Edita si el usuario visualizara las alertas de inicio de sesión
        /// </summary>
        /// <param name="entidad"></param>
        /// <param name="usuarios_adicionales"></param>
        /// <returns></returns>
        public string EditarAlertaInicioSesion(usuarios_configuraciones entidad)
        {
            try
            {
                Model context = new Model();
                usuarios_configuraciones usuario = context.usuarios_configuraciones
                                .First(i => i.usuario.ToUpper() == entidad.usuario.ToUpper());
                usuario.alerta_inicio_sesion = entidad.alerta_inicio_sesion;
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
        /// Edita si el usuario visualizara los recordatorios
        /// </summary>
        /// <param name="entidad"></param>
        /// <param name="usuarios_adicionales"></param>
        /// <returns></returns>
        public string EditarMostrarRecordatorios(usuarios_configuraciones entidad)
        {
            try
            {
                Model context = new Model();
                usuarios_configuraciones usuario = context.usuarios_configuraciones
                                .First(i => i.usuario == entidad.usuario);
                usuario.mostrar_recordatorios = entidad.mostrar_recordatorios;
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
        /// Permite sincronizar el servidor de correo en cada inicio de sesión(puede alentar al mismo)
        /// </summary>
        /// <param name="entidad"></param>
        /// <param name="usuarios_adicionales"></param>
        /// <returns></returns>
        public string EditarSincronizacionAutomatica(usuarios_configuraciones entidad)
        {
            try
            {
                Model context = new Model();
                usuarios_configuraciones usuario = context.usuarios_configuraciones
                                .First(i => i.usuario == entidad.usuario);
                usuario.sincronizacion_automatica = entidad.sincronizacion_automatica;
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
        /// Verifica si el usuario existe en la tabla de configuracion
        /// </summary>
        /// <returns></returns>
        public bool Exist(string usuario)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.usuarios_configuraciones
                                .Where(s => s.usuario.ToUpper() == usuario.ToUpper() && s.activo )
                                .Select(u => new
                                {
                                    u.id_usuarioc
                                });
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

        public DataTable Select(string usuario)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.usuarios_configuraciones
                                .Where(s => s.usuario.ToUpper() == usuario.ToUpper() && s.activo)
                                .Select(u => new
                                {
                                    u.id_usuarioc,
                                    u.nombre,
                                    u.alerta_inicio_sesion,
                                    u.mostrar_recordatorios,
                                    u.sincronizacion_automatica
                                });
                dt = To.DataTable(query.ToList());
                return dt;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                          .SelectMany(x => x.ValidationErrors)
                          .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                return dt;
            }
        }
    }
}
