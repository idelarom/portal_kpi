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
    public class PerformanceIngenieriaCOM
    {
        public DataSet spq_Ingenieros_Performance(DateTime? fecha_ini, DateTime? fecha_fin, string pLstEmpleados, string Usr, int Tipo)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@fecha_inicial", SqlDbType = SqlDbType.Int, Value = fecha_ini });
            listparameters.Add(new SqlParameter() { ParameterName = "@fecha_final", SqlDbType = SqlDbType.Int, Value = fecha_fin });
            listparameters.Add(new SqlParameter() { ParameterName = "@ingenieros", SqlDbType = SqlDbType.Int, Value = pLstEmpleados });
            listparameters.Add(new SqlParameter() { ParameterName = "@login", SqlDbType = SqlDbType.Int, Value = Usr });
            listparameters.Add(new SqlParameter() { ParameterName = "@Tipo", SqlDbType = SqlDbType.Int, Value = Tipo });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("spq_Ingenieros_Performance_test", listparameters, false, 3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet spq_Dashboard_Preventa_Ingenieria(DateTime? fecha_ini, DateTime? fecha_fin, string Usr)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@fecha_inicial", SqlDbType = SqlDbType.Int, Value = fecha_ini });
            listparameters.Add(new SqlParameter() { ParameterName = "@fecha_final", SqlDbType = SqlDbType.Int, Value = fecha_fin });
            listparameters.Add(new SqlParameter() { ParameterName = "@Login", SqlDbType = SqlDbType.Int, Value = Usr });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("spq_Dashboard_Preventa_Ingenieria", listparameters, false, 3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet spq_Performance_Ingenieria(DateTime? fecha_ini, DateTime? fecha_fin, string Usr)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@fecha_inicial", SqlDbType = SqlDbType.Int, Value = fecha_ini });
            listparameters.Add(new SqlParameter() { ParameterName = "@fecha_final", SqlDbType = SqlDbType.Int, Value = fecha_fin });
            listparameters.Add(new SqlParameter() { ParameterName = "@Login", SqlDbType = SqlDbType.Int, Value = Usr });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("spq_Performance_Ingenieria", listparameters, false, 6);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet spq_Performance_Ingenieria_sailine(DateTime? fecha_ini, DateTime? fecha_fin, string Usr)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@fecha_inicial", SqlDbType = SqlDbType.Int, Value = fecha_ini });
            listparameters.Add(new SqlParameter() { ParameterName = "@fecha_final", SqlDbType = SqlDbType.Int, Value = fecha_fin });
            listparameters.Add(new SqlParameter() { ParameterName = "@Login", SqlDbType = SqlDbType.Int, Value = Usr });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("spq_Performance_Ingenieria", listparameters, false, 5);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
