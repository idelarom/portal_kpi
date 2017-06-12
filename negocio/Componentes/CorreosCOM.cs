using datos;
using datos.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace negocio.Componentes
{
    public class CorreosCOM
    {
        /// <summary>
        /// Devuelve un DatatTable con los correos
        /// </summary>
        /// <param name="id_proyect"></param>
        /// <returns></returns>
        public DataTable GetAll(int id_proyect)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.proyectos_correos_historial
                                .Where(s => s.id_proyecto == id_proyect)
                                .Select(u => new
                                {
                                    u.id_pcorreo,
                                    u.id_proyecto,
                                    u.subject,
                                    u.mail_to,
                                    u.body,
                                    u.fecha_envio,
                                    u.usuario
                                });
                dt = To.DataTable(query.ToList());
                dt.Columns.Add("subject_corta");
                foreach (DataRow row in dt.Rows)
                {
                    row["subject_corta"] = row["subject"].ToString().Length > 35 ? row["subject"].ToString().Substring(0, 35) + "..." : row["subject"].ToString();
                }
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }


        public DataTable GetAllxUser(int id_proyect, string user)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.proyectos_correos_historial
                                .Where(s => s.id_proyecto == id_proyect && s.usuario.Trim().ToUpper() == user.Trim().ToUpper() )
                                .Select(u => new
                                {
                                    u.id_pcorreo,
                                    u.id_proyecto,
                                    u.subject,
                                    u.mail_to,
                                    u.body,
                                    u.fecha_envio,
                                    u.usuario
                                });
                dt = To.DataTable(query.ToList());
                dt.Columns.Add("subject_corta");
                foreach (DataRow row in dt.Rows)
                {
                    row["subject_corta"] = row["subject"].ToString().Length > 35 ? row["subject"].ToString().Substring(0, 35) + "..." : row["subject"].ToString();
                }
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        public DataTable GetAllxTrash(int id_proyect)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.proyectos_correos_historial
                                .Where(s => s.id_proyecto == id_proyect && s.papelera == true)
                                .Select(u => new
                                {
                                    u.id_pcorreo,
                                    u.id_proyecto,
                                    u.subject,
                                    u.mail_to,
                                    u.body,
                                    u.fecha_envio,
                                    u.usuario
                                });
                dt = To.DataTable(query.ToList());
                dt.Columns.Add("subject_corta");
                foreach (DataRow row in dt.Rows)
                {
                    row["subject_corta"] = row["subject"].ToString().Length > 35 ? row["subject"].ToString().Substring(0, 35) + "..." : row["subject"].ToString();
                }
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        /// <summary>
        /// Devuelve un DatatTable con  las tareas filtradas por id_correo
        /// </summary>
        /// <param name="id_proyect"></param>
        /// <returns></returns>
        public DataTable Get(proyectos_correos_historial entidad)
        {
            DataTable dt = new DataTable();
            try
            {
                Model context = new Model();
                var query = context.proyectos_correos_historial
                                .Where(s => s.id_pcorreo == entidad.id_pcorreo)
                                .Select(u => new
                                {
                                    u.id_pcorreo,
                                    u.id_proyecto,
                                    u.subject,
                                    u.mail_to,
                                    u.body,
                                    u.fecha_envio,
                                    u.usuario
                                });
                dt = To.DataTable(query.ToList());
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        public DataSet Enviar(proyectos_correos_historial entidad, 
            string nombre_cliente, string informacion, string proyecto,string nombre_remitente, int caso)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@id_proyecto", SqlDbType = SqlDbType.Int, Value = entidad.id_proyecto });
            listparameters.Add(new SqlParameter() { ParameterName = "@usuario", SqlDbType = SqlDbType.Int, Value = entidad.usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@mail_To", SqlDbType = SqlDbType.Int, Value = entidad.mail_to });
            listparameters.Add(new SqlParameter() { ParameterName = "@mail", SqlDbType = SqlDbType.Int, Value = entidad.body });
            listparameters.Add(new SqlParameter() { ParameterName = "@subject", SqlDbType = SqlDbType.Int, Value = entidad.subject });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombre_cliente", SqlDbType = SqlDbType.Int, Value = nombre_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@informacion_adicional", SqlDbType = SqlDbType.Int, Value = informacion });
            listparameters.Add(new SqlParameter() { ParameterName = "@pproyecto", SqlDbType = SqlDbType.Int, Value = proyecto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombre_remitent", SqlDbType = SqlDbType.Int, Value = nombre_remitente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcaso_envio", SqlDbType = SqlDbType.Int, Value = caso });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_envia_correo_portalclientes", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_historial_correos_chat(int id_proyecto, string usuario, string mail_to)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_proyecto", SqlDbType = SqlDbType.Int, Value = id_proyecto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario", SqlDbType = SqlDbType.Int, Value = usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmail_To", SqlDbType = SqlDbType.Int, Value = mail_to });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_historial_correos_chat", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}