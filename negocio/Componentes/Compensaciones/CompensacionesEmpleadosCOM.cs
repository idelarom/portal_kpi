using datos;
using datos.NAVISION;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace negocio.Componentes.Compensaciones
{
    public class CompensacionesEmpleadosCOM
    {
        public DataSet Sps_ConsultaEmpleadosNAVISION(int id_bond_type)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pNumEmpleado", SqlDbType = SqlDbType.Int, Value = id_bond_type });
            listparameters.Add(new SqlParameter() { ParameterName = "@pNomEmpleado", SqlDbType = SqlDbType.Int, Value = id_bond_type });
            listparameters.Add(new SqlParameter() { ParameterName = "@pTipoConsulta", SqlDbType = SqlDbType.Int, Value = id_bond_type });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("Sps_ConsultaEmpleadosNAVISION", listparameters, false, 4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
