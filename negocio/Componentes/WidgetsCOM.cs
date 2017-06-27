using datos.NAVISION;
using datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;


namespace negocio.Componentes
{
    public class WidgetsCOM
    {
        public DataSet sp_editar_widgets(int id_widget, string widget, string icono, string usuario)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_widget", SqlDbType = SqlDbType.Int, Value = widget });
            listparameters.Add(new SqlParameter() { ParameterName = "@pwidget", SqlDbType = SqlDbType.Int, Value = widget });
            listparameters.Add(new SqlParameter() { ParameterName = "@picono", SqlDbType = SqlDbType.Int, Value = icono });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario", SqlDbType = SqlDbType.Int, Value = usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_editar_widget", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_agregar_widgets(string widget, string icono, string usuario)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pwidget", SqlDbType = SqlDbType.Int, Value = widget });
            listparameters.Add(new SqlParameter() { ParameterName = "@picono", SqlDbType = SqlDbType.Int, Value = icono });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario", SqlDbType = SqlDbType.Int, Value = usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_agregar_widgets", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_borrar_widgets(int pid_perfil, string usuario, string comentarios)
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

        public DataSet sp_catalogo_widgets(int pid_perfil)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_widget", SqlDbType = SqlDbType.Int, Value = pid_perfil });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_catalogo_widgets", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_usuarios_widgets(int pid_perfil)
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
    }
}
