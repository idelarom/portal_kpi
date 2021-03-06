﻿using datos.NAVISION;
using datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using datos.Model;

namespace negocio.Componentes
{
    public class UsuariosCOM
    {
        public string perfil(string usuario_)
        {
            try
            {
                Model db = new Model();
                var results = (from a in db.usuarios_perfiles
                            join b in db.perfiles on a.id_perfil equals b.id_perfil
                            where(a.usuario_borrado == null && a.usuario.ToUpper() == usuario_.ToUpper())
                            select new {
                                b.perfil
                            });
                DataTable dt = To.DataTable(results.ToList());
                string usuario = "--Sin perfil relacionado";
                if (dt.Rows.Count > 0)
                {
                    usuario = dt.Rows[0]["perfil"].ToString();
                }
                return usuario;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                return "--Sin perfil relacionado";
            }

        }
        public usuarios usuario(string usuario_)
        {
            try
            {
                Model context = new Model();
                usuarios usuario = context.usuarios
                                .First(i => i.usuario.ToUpper().Trim() == usuario_.ToUpper().Trim());
                return usuario;
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

        public usuarios usuario_mail(string mail)
        {
            try
            {
                Model context = new Model();
                List<usuarios>  list = context.usuarios
                                .Where(i => i.correo.ToUpper().Trim() == mail.ToUpper().Trim()).ToList();
                return list.Count > 0 ? list[0]:null;
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


        public DataSet sp_prueba_files()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_prueba_files", listparameters, false, 7);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet sp_usuario_widgets(string usuario)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario", SqlDbType = SqlDbType.Int, Value = usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_usuario_widgets", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataTable GetUsuariosPermisos(string usuario)
        {
            DataTable dt = new DataTable();
            try
            {
                Model db = new Model();

                var results = from p in db.permisos
                              join up in db.usuarios_permisos on p.id_permiso equals up.id_permiso                             
                              where (up.usuario.ToUpper() == usuario.ToUpper())
                              select new {  p.id_permiso, p.permiso };
                dt = To.DataTable(results.ToList());
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
        public string Editar(usuarios entidad)
        {
            try
            {
                EmpleadosCOM empleados = new EmpleadosCOM();
                string mess = "";
                if (entidad.No_ != "" && !empleados.Exists(entidad.No_))
                {

                    mess = "No existe el empleado con el numero: " + entidad.No_;
                }
                else
                {
                    Model context = new Model();
                    usuarios usuario = context.usuarios
                                    .First(i => i.usuario.ToUpper().Trim() == entidad.usuario.ToUpper().Trim());
                    usuario.contraseña = entidad.contraseña;
                    usuario.correo = entidad.correo;
                    usuario.puesto = entidad.puesto;
                    usuario.nombres = entidad.nombres;
                    usuario.a_paterno = entidad.a_paterno;
                    usuario.a_materno = entidad.a_materno;
                    usuario.No_ = entidad.No_;
                    usuario.temporal = entidad.temporal;
                    usuario.fecha_vencimiento = entidad.fecha_vencimiento;
                    usuario.path_imagen = entidad.path_imagen;
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

        public string Agregar(usuarios entidad)
        {
            try
            {
                EmpleadosCOM empleados = new EmpleadosCOM();
                string mess = "";
                if (Exist(entidad.usuario))
                {
                    mess = "Ya existe un usuario llamado: " + entidad.usuario;
                }
                else if (entidad.No_ != "" && !empleados.Exists(entidad.No_)) {

                    mess = "No existe el empleado con el numero: "+ entidad.No_ ;
                }
                else
                {
                    usuarios usuario = new usuarios
                    {
                        usuario = entidad.usuario.ToUpper().Trim(),
                        No_ = entidad.No_,

                        contraseña = entidad.contraseña,
                        nombres = entidad.nombres.ToUpper().Trim(),
                        puesto = entidad.puesto,
                        a_paterno = entidad.a_paterno.ToUpper().Trim(),
                        a_materno = entidad.a_materno,
                        temporal = entidad.temporal,
                        fecha_vencimiento = entidad.fecha_vencimiento,
                        correo = entidad.correo,
                        path_imagen = entidad.path_imagen,
                        activo = true,
                        usuario_alta = entidad.usuario_alta.ToUpper(),
                        fecha = DateTime.Now
                    };
                    Model context = new Model();
                    context.usuarios.Add(usuario);
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
        /// Agrega un permiso a un usuario
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public string AgregarPermiso(usuarios_permisos entidad)
        {
            try
            {
                string mess = "";
                if (ExistPermission(entidad.usuario, entidad.id_permiso))
                {
                    mess = "El usuario ya tiene este permiso.";
                }
                else
                {
                    usuarios_permisos permiso = new usuarios_permisos
                    {
                        id_permiso = entidad.id_permiso,
                        activo = true,
                        usuario = entidad.usuario.ToUpper()
                    };
                    Model context = new Model();
                    context.usuarios_permisos.Add(permiso);
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
        /// Devuelve un valor booleano si el usuario tiene un permiso especificado
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="id_permiso"></param>
        /// <returns></returns>
        public bool ExistPermission(string usuario, int id_permiso)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.usuarios_permisos
                                .Where(s => s.usuario.ToUpper() == usuario.ToUpper() && s.activo && s.id_permiso == id_permiso)
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

        public bool Exist(string usuario)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.usuarios
                                .Where(s => s.usuario.ToUpper() == usuario.ToUpper() && s.activo )
                                .Select(u => new
                                {
                                    u.id_usuario
                                })
                                .OrderBy(u => u.id_usuario);
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
