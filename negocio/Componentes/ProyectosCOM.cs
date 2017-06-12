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
    public class ProyectosCOM
    {
        public string[] Agregar(proyectos entidad)
        {
            string[] return_array = new string[2];
            try
            {
                if (Exist(entidad))
                {
                    return_array[0] = "Ya existe un Proyecto Llamado: " + entidad.proyecto;
                    return_array[1] = "0";
                }
                else
                {
                    proyectos proyecto = new proyectos
                    {
                        terminado = false,
                        codigo_proyecto = entidad.codigo_proyecto,
                        proyecto = entidad.proyecto,
                        duración = entidad.duración,
                        descripcion = entidad.descripcion,
                        avance = 0,
                        fecha_fin = entidad.fecha_fin,
                        fecha_inicio = entidad.fecha_inicio,
                        fecha_inicio_str = entidad.fecha_inicio_str,
                        fecha_fin_str = entidad.fecha_fin_str,
                        fecha_registro = DateTime.Now,

                        usuario = entidad.usuario
                    };
                    Model context = new Model();
                    context.proyectos.Add(proyecto);
                    context.SaveChanges();
                    int id_entity = proyecto.id_proyecto;
                    return_array[0] = "";
                    return_array[1] = id_entity.ToString();
                }
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

        public string Editar(proyectos entidad)
        {
            try
            {
                if (entidad.proyecto == "")
                {
                    return "Ingrese un nombre de proyecto";
                }
                else
                {
                    Model context = new Model();
                    proyectos proyecto = context.proyectos
                                    .First(i => i.id_proyecto == entidad.id_proyecto);
                    proyecto.codigo_proyecto = proyecto.codigo_proyecto;
                    proyecto.terminado = false;
                    proyecto.proyecto = entidad.proyecto;
                    proyecto.duración = entidad.duración;
                    proyecto.descripcion = entidad.descripcion;
                    proyecto.avance = Convert.ToByte(entidad.avance);
                    proyecto.fecha_inicio_str = entidad.fecha_inicio_str;
                    proyecto.fecha_fin_str = entidad.fecha_fin_str;
                    proyecto.fecha_edicion = DateTime.Now;
                    proyecto.fecha_inicio = entidad.fecha_inicio;
                    proyecto.fecha_fin = entidad.fecha_fin;
                    proyecto.usuario_edicion = entidad.usuario_edicion;
                    proyecto.correo_bienvenida = false;
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

        public string Terminar(proyectos entidad)
        {
            try
            {
                Model context = new Model();
                proyectos proyecto = context.proyectos
                                .First(i => i.id_proyecto == entidad.id_proyecto);               
                proyecto.terminado = true;
                proyecto.costo_real = entidad.costo_real;
                proyecto.valor_ganado = entidad.valor_ganado;
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
        public string CorreoBienvenida(int id_proyecto)
        {
            try
            {
                Model context = new Model();
                proyectos proyecto = context.proyectos
                                .First(i => i.id_proyecto == id_proyecto);
                proyecto.correo_bienvenida = true;
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
        public string EditarCharter(proyectos entidad)
        {
            try
            {
                Model context = new Model();
                proyectos proyecto = context.proyectos
                                .First(i => i.id_proyecto == entidad.id_proyecto);
                if (entidad.objetivos != null)
                {
                    proyecto.objetivos = entidad.objetivos;
                }
                else if (entidad.descripcion_solucion != null)
                {
                    proyecto.descripcion_solucion = entidad.descripcion_solucion;
                }
                else if (entidad.supuestos != null)
                {
                    proyecto.supuestos = entidad.supuestos;
                }
                else if (entidad.fuera_alcance != null)
                {
                    proyecto.fuera_alcance = entidad.fuera_alcance;
                }
                else if (entidad.riesgos_alto_nivel != null)
                {
                    proyecto.riesgos_alto_nivel = entidad.riesgos_alto_nivel;
                }
                proyecto.usuario_edicion = entidad.usuario_edicion;
                proyecto.fecha_edicion = DateTime.Now;
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

        public DataSet actualizar_avances()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_actualiza_avance", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_combo_pm_x_proyecto()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_combo_pm_x_proyecto", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_COMBO_CLIENTES_PROYECTOS()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_COMBO_CLIENTES_PROYECTOS", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet sp_get_proyects_info(int id_proyecto, string usuario, bool administrador, int id_cliente, string usuario_filtro)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_proyecto", SqlDbType = SqlDbType.Int, Value = id_proyecto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario", SqlDbType = SqlDbType.Int, Value = usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@padministrador", SqlDbType = SqlDbType.Bit, Value = administrador });
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_cliente", SqlDbType = SqlDbType.Bit, Value = id_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario_filtro", SqlDbType = SqlDbType.VarChar, Value = usuario_filtro });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_get_proyects_info", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet ListadoEmpleadoProyecto(int id_proyecto)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_proyecto", SqlDbType = SqlDbType.Int, Value = id_proyecto });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_listado_empleados_proyecto", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Devuelve un valor booleano si existe una tarea activa por id interno(del proyecto)
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public bool Exist(proyectos entidad)
        {
            try
            {
                Model context = new Model();
                bool exist = context.proyectos
                                .Any(i => i.usuario_borrado == null
                                 && i.proyecto == entidad.proyecto);
                return exist;
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                return false;
            }
        }
    }
}