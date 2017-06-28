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
    public class ConfiguracionDashboardCOM
    {
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

        public DataSet sp_guardar_usuarios_config(string usuario, string cadena_widgets, int total_cadena_widgets)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario", SqlDbType = SqlDbType.Int, Value = usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_widgets", SqlDbType = SqlDbType.Int, Value = cadena_widgets });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena_widgets", SqlDbType = SqlDbType.Int, Value = total_cadena_widgets });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_guardar_usuarios_config", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
