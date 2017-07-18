using datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class PerfilesCOM
    {
        public DataSet sp_editar_perfiles(int id_perfil, string perfil, string usuario, string cadena_usuarios,
            int total_cadena_usuarios, string cadena_widgets, int total_cadena_widgets,
            string cadena_menus, int total_cadena_menus)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_perfil", SqlDbType = SqlDbType.Int, Value = id_perfil });
            listparameters.Add(new SqlParameter() { ParameterName = "@pperfil", SqlDbType = SqlDbType.Int, Value = perfil });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario", SqlDbType = SqlDbType.Int, Value = usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_usuarios", SqlDbType = SqlDbType.Int, Value = cadena_usuarios });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena_usuarios", SqlDbType = SqlDbType.Int, Value = total_cadena_usuarios });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_widgets", SqlDbType = SqlDbType.Int, Value = cadena_widgets });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena_widgets", SqlDbType = SqlDbType.Int, Value = total_cadena_widgets });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_menus", SqlDbType = SqlDbType.Int, Value = cadena_menus });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena_menus", SqlDbType = SqlDbType.Int, Value = total_cadena_menus });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_editar_perfiles", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_agregar_perfiles(string perfil, string usuario,
            string cadena_usuarios, int total_cadena_usuarios,
            string cadena_widgets, int total_cadena_widgets,
            string cadena_menus, int total_cadena_menus)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pperfil", SqlDbType = SqlDbType.Int, Value = perfil });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario", SqlDbType = SqlDbType.Int, Value = usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_usuarios", SqlDbType = SqlDbType.Int, Value = cadena_usuarios });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena_usuarios", SqlDbType = SqlDbType.Int, Value = total_cadena_usuarios });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_widgets", SqlDbType = SqlDbType.Int, Value = cadena_widgets });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena_widgets", SqlDbType = SqlDbType.Int, Value = total_cadena_widgets });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_menus", SqlDbType = SqlDbType.Int, Value = cadena_menus });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena_menus", SqlDbType = SqlDbType.Int, Value = total_cadena_menus });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_agregar_perfiles", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_borrar_perfiles(int pid_perfil, string usuario, string comentarios)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_perfil", SqlDbType = SqlDbType.Int, Value = pid_perfil });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario", SqlDbType = SqlDbType.Int, Value = usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcomentarios", SqlDbType = SqlDbType.Int, Value = comentarios });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_borrar_perfiles", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_catalogo_perfiles(int pid_perfil)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_perfil", SqlDbType = SqlDbType.Int, Value = pid_perfil });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_catalogo_perfiles", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_widgets_perfiles(int pid_perfil)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_perfil", SqlDbType = SqlDbType.Int, Value = pid_perfil });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_widgets_perfiles", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_usuarios_perfiles(int pid_perfil)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_perfil", SqlDbType = SqlDbType.Int, Value = pid_perfil });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_usuarios_perfiles", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_menus_perfiles(int pid_perfil)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_perfil", SqlDbType = SqlDbType.Int, Value = pid_perfil });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_menus_perfiles", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        
    }
}