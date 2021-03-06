﻿using datos;
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
    public class ProyectosEmpleadosCOM
    {
        public string Agregar(int id_proyecto, int id_proyecto_rol, string usuario, string usuario_registro)
        {
            try
            {
                if (Exists(id_proyecto, usuario))
                {
                    return "El proyecto ya tiene como integrante al usuario: "+usuario;
                }
                else
                {
                    Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                    string mess = "";
                    proyectos_empleados empleado = new proyectos_empleados
                    {
                        id_proyecto = id_proyecto,
                        usuario = usuario,
                        id_proyecto_rol = id_proyecto_rol,
                        usuario_registro = usuario_registro,
                        fecha_registro = DateTime.Now,
                        activo = true

                    };
                    context.proyectos_empleados.Add(empleado);
                    context.SaveChanges();
                    return mess;
                }
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

        public string Eliminar(int id_proyectoe, string usuario)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                proyectos_empleados proyecto = context.proyectos_empleados
                                .First(i => i.id_proyectoe == id_proyectoe);
                proyecto.activo = false;
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
        public bool Exists(int id_proyecto, string usuario)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                var query  = context.proyectos_empleados
                                .Where(i => i.id_proyecto == id_proyecto && i.usuario.ToUpper().Trim()== usuario.ToUpper().Trim() &&
                                i.activo);
                DataTable dt = To.DataTable(query.ToList());
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

        public List<proyectos_roles> list_proyectos_roles()
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                List<proyectos_roles> list= context.proyectos_roles
                                .Where(i => i.activo).ToList();
                return list;
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


        public DataTable empleados_proyecto(int id_proyecto)
        {
            try
            {
                Proyectos_ConnextEntities context = new Proyectos_ConnextEntities();
                EmpleadosCOM empleados = new EmpleadosCOM();
                bool no_activos = false;
                DataSet ds = empleados.sp_listado_empleados(0, true, no_activos);
                DataTable dt_empleados_subordinados = ds.Tables[0];
                List<EmpleadoSubordinados> list_emp = new List<EmpleadoSubordinados>();
                foreach (DataRow row in dt_empleados_subordinados.Rows)
                {
                    EmpleadoSubordinados empleado = new EmpleadoSubordinados();
                    empleado.Usuario = row["usuario"].ToString().ToUpper();
                    empleado.Nombre = row["nombre"].ToString().ToUpper();
                    empleado.Puesto = row["puesto"].ToString().ToUpper();
                    empleado.Correo = row["correo"].ToString().ToUpper();
                    list_emp.Add(empleado);
                }
                var tblempleados = context.proyectos_empleados
                                .Where(s => s.activo && s.id_proyecto == id_proyecto)
                                .Select(u => new
                                {
                                    u.id_proyectoe,
                                    u.id_proyecto_rol,
                                    u.usuario,
                                    u.id_proyecto,
                                    u.usuario_registro,
                                    u.fecha_registro                                 
                                })
                                .OrderBy(u => u.usuario).ToArray();

                var result = (from a in tblempleados
                              join p in context.proyectos on a.id_proyecto equals p.id_proyecto
                              join b in list_emp on a.usuario.ToUpper().Trim() equals b.Usuario.Trim().ToUpper()
                              join c in context.proyectos_roles on a.id_proyecto_rol equals c.id_proyecto_rol
                              select new {
                                  a.id_proyectoe,
                                  a.usuario,
                                  a.usuario_registro,
                                  a.fecha_registro,
                                  c.id_proyecto_rol,
                                  c.administrador_proyecto,
                                  c.rol,
                                  b.Nombre,
                                  b.Correo,
                                  b.Puesto,
                                  p.usuario_resp
                              });
                DataTable dt = To.DataTable(result.ToList());
                return dt;
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

        public DataTable participantes_proyectos(int id_proyecto)
        {
            try
            {
                ProyectosMinutasCOM minutas = new ProyectosMinutasCOM();
                DataTable dt = minutas.GetAllParticipante(id_proyecto);
                return dt;
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
