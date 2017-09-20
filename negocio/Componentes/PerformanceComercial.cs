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
    }
}
