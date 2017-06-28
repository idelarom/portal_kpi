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


    public class EmpleadosCOM
    {
        
        public DataSet sp_menu(int pid_menu_padre, string usuario)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos(); 
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_menu_padre", SqlDbType = SqlDbType.Int, Value = pid_menu_padre });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario", SqlDbType = SqlDbType.Int, Value = usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_menu", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_order_divs_prueba()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_order_divs_prueba", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataTable GetUsers(Employee entidad)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_listado_usuarios", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
    }

        /// <summary>
        /// Devuelve un DatatTable con la informacion de un empleado
        /// </summary>
        /// <param name="id_proyect"></param>
        /// <returns></returns>
        public DataTable Get(Employee entidad)
        {
            DataTable dt = new DataTable();
            try
            {
                NAVISION context = new NAVISION();
                var query = context.Employee
                                .Where(s => s.Status == 1)
                                .Select(u => new
                                {
                                    u.No_,
                                    u.First_Name,
                                    u.Middle_Name,
                                    u.Last_Name,
                                    u.Initials,
                                    u.Job_Title,
                                    u.Search_Name,
                                    u.Address,
                                    u.Address_2,
                                    u.City,
                                    u.Post_Code,
                                    u.County,
                                    u.Phone_No_,
                                    u.Mobile_Phone_No_,
                                    u.E_Mail,
                                    u.Alt__Address_Code,
                                    u.Alt__Address_Start_Date,
                                    u.Alt__Address_End_Date,
                                    u.Picture,
                                    u.Birth_Date,
                                    u.Social_Security_No_,
                                    u.Union_Code,
                                    u.Union_Membership_No_,
                                    u.Sex,
                                    u.Country_Region_Code,
                                    u.Manager_No_,
                                    u.Emplymt__Contract_Code,
                                    u.Statistics_Group_Code,
                                    u.Employment_Date,
                                    u.Status,
                                    u.Inactive_Date,
                                    u.Cause_of_Inactivity_Code,
                                    u.Termination_Date,
                                    u.Grounds_for_Term__Code,
                                    u.Global_Dimension_1_Code,
                                    u.Global_Dimension_2_Code,
                                    u.Resource_No_,
                                    u.Last_Date_Modified,
                                    u.Extension,
                                    u.Pager,
                                    u.Fax_No_,
                                    u.Company_E_Mail,
                                    u.Title,
                                    u.Salespers__Purch__Code,
                                    u.No__Series,
                                    u.RFC,
                                    u.Lugar_de_Nacimiento,
                                    u.CURP,
                                    u.Fecha_Ultimo_Aumento,
                                    u.CC_Direccion,
                                    u.Cliente,
                                    u.Nombre_Cliente,
                                    u.Resp_Area_Gerencia,
                                    u.Puesto,
                                    u.Resp_Area,
                                    Usuario_Red = (u.Usuario_Red.Trim()),
                                    u.Resp_Gerencia,
                                    u.Centro_de_Costos,
                                    u.No_Celular_Oficina,
                                    u.PuestoId,
                                    u.FechaInicioContrato,
                                    u.FechaFinContrato,
                                    u.FM3Numero,
                                    u.OnSite,
                                    u.MotivoBaja,
                                    u.ComBaja,
                                    u.ComInactividad,
                                    u.Banco1,
                                    u.Cuenta1,
                                    u.Clabe1,
                                    u.Banco2,
                                    u.Cuenta2,
                                    u.Clabe2,
                                    u.BMonto,
                                    u.NumJefe,
                                    u.Tipo_Empleado,
                                    u.Ubicacion_Empleado,
                                    u.Funcion_Empleado,
                                    u.Area,
                                    u.Responsable_Dir,
                                    u.Responsable_Ger,
                                    u.Estado_Civil,
                                    u.Duración_Contrato,
                                    u.FM3,
                                    u.CompañiaTel,
                                    u.Plan_Celular,
                                    u.Subordinados,
                                    u.Bono,
                                    u.Comisiones,
                                    u.BPeriodo,
                                    u.CPeriodo,
                                    u.BTipo,
                                    u.UsuarioMod,
                                    u.AreaAdministrativa,
                                    u.UsuarioRegistro,
                                    u.FechaRegistro,
                                    u.Empresa,
                                    u.Fecha_Alta_IMSS,
                                    u.Hijos,
                                    u.UsuarioAutoriza,
                                    u.FechaAutoriza,
                                    u.MotivoModificacion,
                                    u.TipoBaja,
                                    u.ActivoFijo,
                                    nombre_completo =(u.First_Name.Trim()  +" "+ u.Last_Name.Trim()),
                                    nombre_usuario= (u.First_Name.Trim() + " " + u.Last_Name.Trim()+" | "+u.Usuario_Red.Trim())
                                })
                                .OrderBy(u => u.First_Name);
                dt = To.DataTable(query.ToList());
                //dt.Columns.Add("nombre_completo");
                //foreach (DataRow row in dt.Rows)
                //{
                //    row["nombre_completo"] = row["First_Name"].ToString().Trim() + " " + row["Last_Name"].ToString().Trim();
                //}
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        public DataTable GetLogin(string usuario)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario", SqlDbType = SqlDbType.Int, Value = usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_login", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }
    }
}