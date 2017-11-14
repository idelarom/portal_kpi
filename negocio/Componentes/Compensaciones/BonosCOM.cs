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
    public class BonosCOM
    {
        public DataSet tipos_bonos(bool is_request, string usuario)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@is_request", SqlDbType = SqlDbType.Int, Value = is_request });
            listparameters.Add(new SqlParameter() { ParameterName = "@login", SqlDbType = SqlDbType.Int, Value = usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_GetBondsTypesPermission", listparameters, false, 4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_GetBondsTypesEnabledAndID(int id_bond_type)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@id_bond_type", SqlDbType = SqlDbType.Int, Value = id_bond_type });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_GetBondsTypesEnabledAndID", listparameters, false, 4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_GetConnextProjectsForLogin(string usuario, string nombre_proyecto)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@login_password", SqlDbType = SqlDbType.Int, Value = usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@nombre_proyecto", SqlDbType = SqlDbType.Int, Value = nombre_proyecto });
            try
            {
                ds = data.enviar("sp_GetConnextProjectsForLogin", listparameters, false, 6);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }


        public DataSet sp_GetEmployeesCompensationsForEmployeeNumberAndBondType(int employee_number, int id_bond_type,DateTime period_date_of)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@employee_number", SqlDbType = SqlDbType.Int, Value = employee_number });
            listparameters.Add(new SqlParameter() { ParameterName = "@id_bond_type", SqlDbType = SqlDbType.Int, Value = id_bond_type });
            listparameters.Add(new SqlParameter() { ParameterName = "@period_date_of", SqlDbType = SqlDbType.Int, Value = period_date_of });
            try
            {
                ds = data.enviar("sp_GetEmployeesCompensationsForEmployeeNumberAndBondType", listparameters, false, 4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        
    }
}
