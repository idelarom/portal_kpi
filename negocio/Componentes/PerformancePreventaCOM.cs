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
    public class PerformancePreventaCOM
    {
        public DataSet sp_Preventa_Ingenieria_reportecompromisos_detalle_test(DateTime? fecha_ini, DateTime? fecha_fin, string ingeniero, string tipo, 
            int tipo_consulta, int tipo_tiempo, int tiempo, int mes)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@fechainicial", SqlDbType = SqlDbType.Int, Value = fecha_ini });
            listparameters.Add(new SqlParameter() { ParameterName = "@fechafinal", SqlDbType = SqlDbType.Int, Value = fecha_fin });
            listparameters.Add(new SqlParameter() { ParameterName = "@TipoConsulta", SqlDbType = SqlDbType.Int, Value = tipo_consulta });
            listparameters.Add(new SqlParameter() { ParameterName = "@IngPreventa", SqlDbType = SqlDbType.Int, Value = ingeniero });
            listparameters.Add(new SqlParameter() { ParameterName = "@TipoCompromiso", SqlDbType = SqlDbType.Int, Value = tipo });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipo_tiempo", SqlDbType = SqlDbType.Int, Value = tipo_tiempo });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptiempo", SqlDbType = SqlDbType.Int, Value = tiempo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmes", SqlDbType = SqlDbType.Int, Value = mes });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_Preventa_Ingenieria_reportecompromisos_detalle_test", listparameters, false, 3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sps_backlogCompromisos(DateTime? fecha_ini)
        {
            
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@Fecha", SqlDbType = SqlDbType.Int, Value = fecha_ini });
            listparameters.Add(new SqlParameter() { ParameterName = "@pMonth", SqlDbType = SqlDbType.Int, Value = -1 });
            listparameters.Add(new SqlParameter() { ParameterName = "@pYear", SqlDbType = SqlDbType.Int, Value = -1 });
             try {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sps_backlogCompromisos", listparameters, false, 3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
