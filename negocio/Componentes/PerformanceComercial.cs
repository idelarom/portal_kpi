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
    public class PerformanceComercial
    {
        public DataSet PerformanceComercial_NuevasOP_NUEVO(string vendedor)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@RespCom", SqlDbType = SqlDbType.Int, Value = vendedor });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("PerformanceComercial_NuevasOP_NUEVO", listparameters, false, 3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet PerformanceComercial_InventarioOP_NUEVO(string vendedor)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@RespCom", SqlDbType = SqlDbType.Int, Value = vendedor });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("PerformanceComercial_InventarioOP_NUEVO", listparameters, false, 3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet PerformanceComercial_CierreOP_NUEVO(string vendedor)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@RespCom", SqlDbType = SqlDbType.Int, Value = vendedor });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("PerformanceComercial_CierreOP_NUEVO", listparameters, false, 3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet NAVISION_VtaCtoMar_VenDLLS_nuevo(string vendedor, int año_anterior)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@CVEN", SqlDbType = SqlDbType.Int, Value = vendedor });
            listparameters.Add(new SqlParameter() { ParameterName = "@CAÑO", SqlDbType = SqlDbType.Int, Value = año_anterior });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("NAVISION_VtaCtoMar_VenDLLS_nuevo", listparameters, false, 8);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet PerformanceComercial_Visitas(string vendedor, int año)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@RespCom", SqlDbType = SqlDbType.Int, Value = vendedor });
            listparameters.Add(new SqlParameter() { ParameterName = "@ano", SqlDbType = SqlDbType.Int, Value = año });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("PerformanceComercial_Visitas_nuevo", listparameters, false, 3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet PerformanceComercial_SuficienciaOP_nuevo(string vendedor)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@RespCom", SqlDbType = SqlDbType.Int, Value = vendedor });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("PerformanceComercial_SuficienciaOP_nuevo", listparameters, false, 3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet PerformanceComercial_DetalleInventarioOP(string vendedor)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@RespCom", SqlDbType = SqlDbType.Int, Value = vendedor });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("PerformanceComercial_DetalleInventarioOP", listparameters, false, 3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
