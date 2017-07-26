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
    public class DashboardBonosCOM
    {
        public DataSet Sps_DashBoardReport_Bonos(DateTime? fecha_ini, DateTime? fecha_fin, string pLstEmpleados,
            string lstCC, string Usr, int pidgrupo)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pfechainicial", SqlDbType = SqlDbType.Int, Value = fecha_ini });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfechafinal", SqlDbType = SqlDbType.Int, Value = fecha_fin });
            listparameters.Add(new SqlParameter() { ParameterName = "@pLstEmpleados", SqlDbType = SqlDbType.Int, Value = pLstEmpleados });
            listparameters.Add(new SqlParameter() { ParameterName = "@lstCC", SqlDbType = SqlDbType.Int, Value = lstCC });
            listparameters.Add(new SqlParameter() { ParameterName = "@Usr", SqlDbType = SqlDbType.Int, Value = Usr });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidgrupo", SqlDbType = SqlDbType.Int, Value = pidgrupo });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("Sps_DashBoardReport_Bonos_test", listparameters, false, 4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
