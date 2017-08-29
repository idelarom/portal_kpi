using datos.NAVISION;
using datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using datos.Model;
using System.Globalization;

namespace negocio.Componentes
{
    public class UsuariosSesionesCOM
    {
        public int Agregar(usuarios_sesiones e)
        {
            try
            {
                usuarios_sesiones sesion = new usuarios_sesiones
                {
                    usuario = e.usuario,
                    os = e.os,
                    os_version = e.os_version,
                    navegador = e.navegador,
                    fecha_inicio_sesion = e.fecha_inicio_sesion,
                    ip=e.ip,
                    device = e.device,
                    latitud = e.latitud,
                    longitud = e.longitud,
                    region = e.region,
                    proveedor = e.proveedor,
                    model = e.model,
                    activo = true,
                    device_fingerprint = e.device_fingerprint
                };
                Model context = new Model();
                context.usuarios_sesiones.Add(sesion);
                context.SaveChanges();
                int id_entity = sesion.id_usuario_sesion;
                return id_entity;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                return 0;
            }
        }

        public int Editar(usuarios_sesiones e)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                usuarios_sesiones sesion = context.usuarios_sesiones
                                .First(s => s.usuario.ToUpper() == e.usuario.ToUpper() && s.device_fingerprint.ToUpper() == e.device_fingerprint.ToUpper());
                sesion.usuario = e.usuario;
                sesion.os = e.os;
                sesion.os_version = e.os_version;
                sesion.navegador = e.navegador;
                sesion.fecha_inicio_sesion = e.fecha_inicio_sesion;
                sesion.ip = e.ip;
                sesion.device = e.device;
                sesion.latitud = e.latitud;
                sesion.longitud = e.longitud;
                sesion.region = e.region;
                sesion.proveedor = e.proveedor;
                sesion.model = e.model;
                sesion.activo = true;
                sesion.device_fingerprint = e.device_fingerprint;
                context.SaveChanges();
                return sesion.id_usuario_sesion;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                          .SelectMany(x => x.ValidationErrors)
                          .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                return 0;
            }
        }

        public bool Exist(string usuario, string finger_print)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.usuarios_sesiones
                                .Where(s => s.usuario.ToUpper() == usuario.ToUpper() && s.device_fingerprint.ToUpper() == finger_print.ToUpper())
                                .Select(u => new
                                {
                                    u.usuario
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

    }
}
