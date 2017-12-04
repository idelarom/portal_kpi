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
        public DataSet Sps_ConsultaEmpleadosNAVISION(int? NumEmpleado, string NomEmpleado, int TipoConsulta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pNumEmpleado", SqlDbType = SqlDbType.Int, Value = NumEmpleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@pNomEmpleado", SqlDbType = SqlDbType.Int, Value = NomEmpleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@pTipoConsulta", SqlDbType = SqlDbType.Int, Value = TipoConsulta });
            try
            {
                ds = data.enviar("Sps_ConsultaEmpleadosNAVISION", listparameters, false, 4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_GetEmployees_CompensationsForEmployeeNumber(int? NumEmpleado)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@employee_number", SqlDbType = SqlDbType.Int, Value = NumEmpleado });
            try
            {
                ds = data.enviar("sp_GetEmployees_CompensationsForEmployeeNumber", listparameters, false, 4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_GetBondsTypesEnabled()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                ds = data.enviar("sp_GetBondsTypesEnabled", listparameters, false, 4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_GetAllBondsAtomaticForEmployee(int? NumEmpleado)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@EmployeeNumber", SqlDbType = SqlDbType.Int, Value = NumEmpleado });
            try
            {
                ds = data.enviar("sp_GetAllBondsAtomaticForEmployee", listparameters, false, 4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_Delete_Employee_Compensations(int? NumEmpleado)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@employee_number", SqlDbType = SqlDbType.Int, Value = NumEmpleado });
            try
            {
                ds = data.enviar("sp_Delete_Employee_Compensations", listparameters, false, 4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_Insert_Employee_Compensations(employees_compensations employee_compensations)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@employee_number", SqlDbType = SqlDbType.Int, Value = employee_compensations.employee_number });
            listparameters.Add(new SqlParameter() { ParameterName = "@id_bond_type", SqlDbType = SqlDbType.Int, Value = employee_compensations.id_bond_type });
            listparameters.Add(new SqlParameter() { ParameterName = "@amount", SqlDbType = SqlDbType.Int, Value = employee_compensations.amount });
            listparameters.Add(new SqlParameter() { ParameterName = "@id_periodicity", SqlDbType = SqlDbType.Int, Value = employee_compensations.id_periodicity });
            listparameters.Add(new SqlParameter() { ParameterName = "@created_by", SqlDbType = SqlDbType.Int, Value = employee_compensations.created_by });
            listparameters.Add(new SqlParameter() { ParameterName = "@modified_by", SqlDbType = SqlDbType.Int, Value = employee_compensations.modified_by });
            listparameters.Add(new SqlParameter() { ParameterName = "@Porcentaje", SqlDbType = SqlDbType.Int, Value = employee_compensations.percentage_extra });
            try
            {
                ds = data.enviar("sp_Insert_Employee_Compensations", listparameters, false, 4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_actualizar_log_inactivo(employees_compensations employee_compensations)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@employee_number", SqlDbType = SqlDbType.Int, Value = employee_compensations.employee_number });
            listparameters.Add(new SqlParameter() { ParameterName = "@id_bond_type", SqlDbType = SqlDbType.Int, Value = employee_compensations.id_bond_type });
            listparameters.Add(new SqlParameter() { ParameterName = "@id_bond_type_automatic", SqlDbType = SqlDbType.Int, Value = employee_compensations.id_bond_type });
            listparameters.Add(new SqlParameter() { ParameterName = "@UpdateBy", SqlDbType = SqlDbType.Int, Value = employee_compensations.modified_by});
            try
            {
                ds = data.enviar("sp_actualizar_log_inactivo", listparameters, false, 4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_GetAllBondsAutomaticType()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                ds = data.enviar("sp_GetAllBondsAutomaticType", listparameters, false, 4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_GetAllPeriodicityEnabled()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                ds = data.enviar("sp_GetAllPeriodicityEnabled", listparameters, false, 4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_OptenerDatostoRequestsBondsAutomatic(int? NumEmpleado, int? Centro_costos)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@EmployeeNumber", SqlDbType = SqlDbType.Int, Value = NumEmpleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@CC", SqlDbType = SqlDbType.Int, Value = Centro_costos });
            try
            {
                ds = data.enviar("sp_OptenerDatostoRequestsBondsAutomatic", listparameters, false, 4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_GetRequests_Bonds_Automatic_Type(int? NumEmpleado)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@Employee_number", SqlDbType = SqlDbType.Int, Value = NumEmpleado });
            try
            {
                ds = data.enviar("sp_GetRequests_Bonds_Automatic_Type", listparameters, false, 4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_GetRequests_Bonds_Automatic_Type_Detalle(int? Id_request_bond_automatic)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@Id_request_bond_automatic", SqlDbType = SqlDbType.Int, Value = Id_request_bond_automatic });
            try
            {
                ds = data.enviar("sp_GetRequests_Bonds_Automatic_Type_Detalle", listparameters, false, 4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_getAll_cost_center()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                ds = data.enviar("sp_getAll_cost_center", listparameters, false, 4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_InsertResquestBondsAutomatic(requests_bonds_Automatic requests_bonds_Automatic)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@Employee_number", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.Employee_number });
            listparameters.Add(new SqlParameter() { ParameterName = "@Employee", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.Employee });
            listparameters.Add(new SqlParameter() { ParameterName = "@CC_Empleado", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.CC_Empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@IdBonds", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.IdBonds });
            listparameters.Add(new SqlParameter() { ParameterName = "@NameBonds", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.NameBonds });
            listparameters.Add(new SqlParameter() { ParameterName = "@Id_Periodicity", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.Id_Periodicity });
            listparameters.Add(new SqlParameter() { ParameterName = "@Occurrences", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.Occurrences });
            listparameters.Add(new SqlParameter() { ParameterName = "@CC_Cargo", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.CC_Cargo });
            listparameters.Add(new SqlParameter() { ParameterName = "@InitialDate", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.InitialDate });
            listparameters.Add(new SqlParameter() { ParameterName = "@FinalDate", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.FinalDate });
            listparameters.Add(new SqlParameter() { ParameterName = "@observations", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.observations });
            listparameters.Add(new SqlParameter() { ParameterName = "@PendingOccurrences", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.PendingOccurrences });
            listparameters.Add(new SqlParameter() { ParameterName = "@status", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.status });
            listparameters.Add(new SqlParameter() { ParameterName = "@Amount", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.Amount });
            listparameters.Add(new SqlParameter() { ParameterName = "@Created_by", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.Created_by });
            try
            {
                ds = data.enviar("sp_InsertResquestBondsAutomatic", listparameters, false, 4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_UpdateResquestBondsAutomatic(requests_bonds_Automatic requests_bonds_Automatic)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@Id_request_bond_automatic", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.Id_request_bond_automatic });
            listparameters.Add(new SqlParameter() { ParameterName = "@IdBonds", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.IdBonds });
            listparameters.Add(new SqlParameter() { ParameterName = "@NameBonds", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.NameBonds });
            listparameters.Add(new SqlParameter() { ParameterName = "@Id_Periodicity", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.Id_Periodicity });
            listparameters.Add(new SqlParameter() { ParameterName = "@Occurrences", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.Occurrences });
            listparameters.Add(new SqlParameter() { ParameterName = "@CC_Cargo", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.CC_Cargo });
            listparameters.Add(new SqlParameter() { ParameterName = "@InitialDate", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.InitialDate });
            listparameters.Add(new SqlParameter() { ParameterName = "@FinalDate", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.FinalDate });
            listparameters.Add(new SqlParameter() { ParameterName = "@observations", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.observations });
            listparameters.Add(new SqlParameter() { ParameterName = "@PendingOccurrences", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.PendingOccurrences });
            listparameters.Add(new SqlParameter() { ParameterName = "@Amount", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.Amount });
            listparameters.Add(new SqlParameter() { ParameterName = "@Update_by", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.Update_by });
            try
            {
                ds = data.enviar("sp_UpdateResquestBondsAutomatic", listparameters, false, 4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_UpdateDeleteRequestBondsAutomatic(requests_bonds_Automatic requests_bonds_Automatic)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@IDBONO", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.Id_request_bond_automatic });
            listparameters.Add(new SqlParameter() { ParameterName = "@pDelete_comments", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.Delete_comments });
            listparameters.Add(new SqlParameter() { ParameterName = "@Employee_number", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.Employee_number });
            listparameters.Add(new SqlParameter() { ParameterName = "@DeleteBy", SqlDbType = SqlDbType.Int, Value = requests_bonds_Automatic.UpdateDelete_By });
            try
            {
                ds = data.enviar("sp_UpdateDeleteRequestBondsAutomatic", listparameters, false, 4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_NotificacionBondAutomatic(string Body, int Action, string User, string id_empleado)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@Body", SqlDbType = SqlDbType.Int, Value = Body });
            listparameters.Add(new SqlParameter() { ParameterName = "@Acion", SqlDbType = SqlDbType.Int, Value = Action });
            listparameters.Add(new SqlParameter() { ParameterName = "@Usuario", SqlDbType = SqlDbType.Int, Value = User });
            listparameters.Add(new SqlParameter() { ParameterName = "@Pnum_empleado", SqlDbType = SqlDbType.Int, Value = id_empleado });
            try
            {
                ds = data.enviar("sp_NotificacionBondAutomatic", listparameters, false, 4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
