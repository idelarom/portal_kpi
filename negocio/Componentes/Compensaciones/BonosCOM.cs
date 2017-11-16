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
        public int AgregarArchivo(files_requests_bonds file)
        {
            try
            {
                string mess = "";                
                SICOEMEntities context = new SICOEMEntities();
                file.date_attach = DateTime.Now;
                context.files_requests_bonds.Add(file);
                context.SaveChanges();
                return file.id_file_request_bond;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                return 0;
            }
        }

        public DataSet Agregar(requests_bonds bono)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@employee_number", SqlDbType = SqlDbType.Int, Value = bono.employee_number });
            listparameters.Add(new SqlParameter() { ParameterName = "@authorization_amount", SqlDbType = SqlDbType.Int, Value = bono.authorization_amount });
            listparameters.Add(new SqlParameter() { ParameterName = "@period_date_of", SqlDbType = SqlDbType.Int, Value = bono.period_date_of });
            listparameters.Add(new SqlParameter() { ParameterName = "@period_date_to", SqlDbType = SqlDbType.Int, Value = bono.period_date_to });
            listparameters.Add(new SqlParameter() { ParameterName = "@id_bond_type", SqlDbType = SqlDbType.Int, Value = bono.id_bond_type });
            listparameters.Add(new SqlParameter() { ParameterName = "@id_request_status", SqlDbType = SqlDbType.Int, Value = bono.id_request_status });
            listparameters.Add(new SqlParameter() { ParameterName = "@number_hours", SqlDbType = SqlDbType.Int, Value = bono.number_hours });
            listparameters.Add(new SqlParameter() { ParameterName = "@folio_pmtracker", SqlDbType = SqlDbType.Int, Value = bono.folio_pmtracker });
            listparameters.Add(new SqlParameter() { ParameterName = "@week", SqlDbType = SqlDbType.Int, Value = bono.week });
            listparameters.Add(new SqlParameter() { ParameterName = "@month", SqlDbType = SqlDbType.Int, Value = bono.month });
            listparameters.Add(new SqlParameter() { ParameterName = "@year", SqlDbType = SqlDbType.Int, Value = bono.year });
            listparameters.Add(new SqlParameter() { ParameterName = "@selected_date", SqlDbType = SqlDbType.Int, Value = bono.selected_date });
            listparameters.Add(new SqlParameter() { ParameterName = "@requisition_comments", SqlDbType = SqlDbType.Int, Value = bono.requisition_comments });
            listparameters.Add(new SqlParameter() { ParameterName = "@modified_by", SqlDbType = SqlDbType.Int, Value = bono.created_by });
            listparameters.Add(new SqlParameter() { ParameterName = "@created_by", SqlDbType = SqlDbType.Int, Value = bono.created_by });
            listparameters.Add(new SqlParameter() { ParameterName = "@CC_Emp", SqlDbType = SqlDbType.Int, Value = bono.CC_Empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@CC_Cob", SqlDbType = SqlDbType.Int, Value = bono.CC_Cargo });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_Insert_Requests_Bonds", listparameters, false, 4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_GetRequests_Bonds(int? id_bond_type = null, 
            int? id_request_status = null, int? id_request_bond = null, int? employee_number = null, 
            int? id_immediate_boss = null, DateTime? period_date_of = null, DateTime? period_date_to = null)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@id_bond_type", SqlDbType = SqlDbType.Int, Value = id_bond_type });
            listparameters.Add(new SqlParameter() { ParameterName = "@id_request_status", SqlDbType = SqlDbType.Int, Value = id_request_status });
            listparameters.Add(new SqlParameter() { ParameterName = "@employee_number", SqlDbType = SqlDbType.Int, Value = id_request_bond });
            listparameters.Add(new SqlParameter() { ParameterName = "@id_request_bond", SqlDbType = SqlDbType.Int, Value = employee_number });
            listparameters.Add(new SqlParameter() { ParameterName = "@id_immediate_boss", SqlDbType = SqlDbType.Int, Value = id_immediate_boss });
            listparameters.Add(new SqlParameter() { ParameterName = "@period_date_of", SqlDbType = SqlDbType.Int, Value = period_date_of });
            listparameters.Add(new SqlParameter() { ParameterName = "@period_date_to", SqlDbType = SqlDbType.Int, Value = period_date_to });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_GetRequests_Bonds", listparameters, false, 4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

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


        public DataSet sp_Get_Tbl_Estructura_CC(string cc, string descripcion)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@CC", SqlDbType = SqlDbType.Int, Value = cc });
            listparameters.Add(new SqlParameter() { ParameterName = "@Desc_CC", SqlDbType = SqlDbType.Int, Value = descripcion });
            try
            {
                ds = data.enviar("sp_Get_Tbl_Estructura_CC", listparameters, false, 4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet sp_GetEmployeeForBondType(int id_bond_type)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@id_bond_type", SqlDbType = SqlDbType.Int, Value = id_bond_type });
            try
            {
                ds = data.enviar("sp_GetEmployeeForBondType", listparameters, false, 4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        

    }
}
