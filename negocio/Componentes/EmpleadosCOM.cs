using datos.NAVISION;
using datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using datos.Model;

namespace negocio.Componentes
{


    public class EmpleadosCOM
    {
        /// <summary>
        /// Agrega un listado de usuarios para que un usuario pueda visualizarlos como si fuera su jefe directo.
        /// </summary>
        /// <param name="usuario_jefe"></param>
        /// <param name="usuarios"></param>
        /// <returns></returns>
        public string AgregarDelegados(string usuario_jefe, List<string> usuarios)
        {
            try
            {
                string vmensaje = EliminarDelegados(usuario_jefe);
                if (vmensaje != "")
                {
                    return vmensaje;
                }
                else {
                    Model context = new Model();
                    foreach (string usuario in usuarios)
                    {
                        if (!ExisteDelegado(usuario_jefe, usuario))
                        {
                            usuarios_delegados usuarios_delegado = new usuarios_delegados
                            {
                                usuario = usuario,
                                usuario_jefe = usuario_jefe,
                                activo = true
                            };
                            context.usuarios_delegados.Add(usuarios_delegado);
                        }
                    }
                    context.SaveChanges();
                    return "";
                }                
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                return fullErrorMessage.ToString();
            }
        }

        public string EliminarDelegados(string usuario_jefe)
        {
            try
            {
                Model context = new Model();
                var query = context.usuarios_delegados
                                .Where(i => i.usuario_jefe.ToUpper() == usuario_jefe.ToUpper())
                                .Select(u => new
                                {
                                    u.id_usuariod
                                });
               DataTable dt = To.DataTable(query.ToList());
                foreach (DataRow row in dt.Rows)
                {
                    int id = Convert.ToInt32(row["id_usuariod"]);
                    usuarios_delegados usuario_delegado = context.usuarios_delegados
                               .First(i => i.id_usuariod ==id);
                    usuario_delegado.activo = false;
                }
                context.SaveChanges();
                return "";
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                return fullErrorMessage.ToString();
            }
        }

        public bool ExisteDelegado(string usuario_jefe, string usuario)
        {
            try
            {
                Model context = new Model();
                var query = context.usuarios_delegados
                                .Where(i => i.usuario_jefe.ToUpper() == usuario_jefe.ToUpper()
                                && i.usuario.ToUpper() == usuario.ToUpper())
                                .Select(u => new
                                {
                                    u.id_usuariod
                                });
                DataTable dt = To.DataTable(query.ToList());
                foreach (DataRow row in dt.Rows)
                {
                    int id = Convert.ToInt32(row["id_usuariod"]);
                    usuarios_delegados usuario_delegado = context.usuarios_delegados
                               .First(i => i.id_usuariod == id);
                    usuario_delegado.activo = true;
                }
                context.SaveChanges();
                return dt.Rows.Count > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                return false;
            }
        }
        public bool Exists(string No_)
        {
            try
            {
                NAVISION context = new NAVISION();
                var query = context.Employee
                                .Where(i => i.No_.Trim().ToUpper() == No_.Trim().ToUpper()
                                & i.Status == 1)
                                .Select(u => new
                                {
                                    u.No_
                                });
                DataTable dt = To.DataTable(query.ToList());
                return dt.Rows.Count > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                return false;
            }
        }

        public DataTable GetDelegados(string usuario)
        {
            DataTable dt = new DataTable();
            try
            {
                Model db = new Model();
                var usuarios = (from p in db.usuarios_delegados
                           where (p.usuario_jefe.ToUpper() == usuario.ToUpper()
                           && p.activo == true)
                           select new { p.usuario }).ToArray();
                NAVISION dbnavision = new NAVISION();
                var results = from p in usuarios
                              join up in dbnavision.Employee on p.usuario.ToUpper().Trim() equals up.Usuario_Red.ToUpper().Trim()
                              select new { Usuario_red = p.usuario, nombre_usuario = up.First_Name.Trim() +" "+up.Last_Name.Trim()+" | "+p.usuario.Trim().ToUpper() };
                dt = To.DataTable(results.ToList());
                return dt;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                          .SelectMany(x => x.ValidationErrors)
                          .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                return dt;
            }
        }
        public DataSet sp_listado_empleados(int num_empleado, bool ver_Todos_Empleados, bool no_activos)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pno_activos", SqlDbType = SqlDbType.Int, Value = no_activos });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnum_jefe", SqlDbType = SqlDbType.Int, Value = num_empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@pver_todos_empleados", SqlDbType = SqlDbType.Int, Value = ver_Todos_Empleados });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_listado_empleados", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_desbloquear_dispositivo(int id_usuario_sesion)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_usuario_sesion", SqlDbType = SqlDbType.Int, Value = id_usuario_sesion });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_desbloquear_dispositivo", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet sp_usuario_sesiones(string usuario, bool ver_todos)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario", SqlDbType = SqlDbType.Int, Value = usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@ver_Todos", SqlDbType = SqlDbType.Int, Value = ver_todos });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_usuario_sesiones", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet sp_existe_usuario_sesiones(string usuario, string os, string os_version, string browser, string device)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario", SqlDbType = SqlDbType.Int, Value = usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pos", SqlDbType = SqlDbType.Int, Value = os });
            listparameters.Add(new SqlParameter() { ParameterName = "@pos_version", SqlDbType = SqlDbType.Int, Value = os_version });
            listparameters.Add(new SqlParameter() { ParameterName = "@pbrowser", SqlDbType = SqlDbType.Int, Value = browser });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdevice", SqlDbType = SqlDbType.Int, Value = device });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_existe_usuario_sesiones", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet sp_agregar_usuario_sesiones(string usuario, string os, string os_version, string browser, string device,
            string ip, string lat, string lon, string region, string proveedor, string modelo, DateTime fecha_inicio_sesion,
            string device_fingerprint)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario", SqlDbType = SqlDbType.Int, Value = usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pos", SqlDbType = SqlDbType.Int, Value = os });
            listparameters.Add(new SqlParameter() { ParameterName = "@pos_version", SqlDbType = SqlDbType.Int, Value = os_version });
            listparameters.Add(new SqlParameter() { ParameterName = "@pbrowser", SqlDbType = SqlDbType.Int, Value = browser });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdevice", SqlDbType = SqlDbType.Int, Value = device });
            listparameters.Add(new SqlParameter() { ParameterName = "@pip", SqlDbType = SqlDbType.Int, Value = ip });
            listparameters.Add(new SqlParameter() { ParameterName = "@platitud", SqlDbType = SqlDbType.Int, Value = lat });
            listparameters.Add(new SqlParameter() { ParameterName = "@plongitud", SqlDbType = SqlDbType.Int, Value = lon });
            listparameters.Add(new SqlParameter() { ParameterName = "@pregion", SqlDbType = SqlDbType.Int, Value = region });
            listparameters.Add(new SqlParameter() { ParameterName = "@pproveedor", SqlDbType = SqlDbType.Int, Value = proveedor });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmodel", SqlDbType = SqlDbType.Int, Value = modelo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdevice_fingerprint", SqlDbType = SqlDbType.Int, Value = device_fingerprint });
            
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha_inicio_sesion", SqlDbType = SqlDbType.Int, Value = fecha_inicio_sesion });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_agregar_usuario_sesiones", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_eliminar_usuario_sesiones(int id_usuario_sesion, Boolean bloquear)
        {
            //string usuario, string os, string os_version, string browser, string device,
            //string ip, DateTime fecha_inicio_sesion
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pid_usuario_sesion", SqlDbType = SqlDbType.Int, Value = id_usuario_sesion });
            listparameters.Add(new SqlParameter() { ParameterName = "@pbloquear", SqlDbType = SqlDbType.Int, Value = bloquear });
            //listparameters.Add(new SqlParameter() { ParameterName = "@pos", SqlDbType = SqlDbType.Int, Value = os });
            //listparameters.Add(new SqlParameter() { ParameterName = "@pos_version", SqlDbType = SqlDbType.Int, Value = os_version });
            //listparameters.Add(new SqlParameter() { ParameterName = "@pbrowser", SqlDbType = SqlDbType.Int, Value = browser });
            //listparameters.Add(new SqlParameter() { ParameterName = "@pdevice", SqlDbType = SqlDbType.Int, Value = device });
            //listparameters.Add(new SqlParameter() { ParameterName = "@pip", SqlDbType = SqlDbType.Int, Value = ip });
            //listparameters.Add(new SqlParameter() { ParameterName = "@pfecha_inicio_sesion", SqlDbType = SqlDbType.Int, Value = fecha_inicio_sesion });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_eliminar_usuario_sesiones", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

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

        public DataSet sp_order_widgets(string usuario)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario", SqlDbType = SqlDbType.Int, Value = usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_order_widgets", listparameters, false, 1);
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
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        public DataTable GetLogin(string usuario, string fingerprint)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario", SqlDbType = SqlDbType.Int, Value = usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfinger_print", SqlDbType = SqlDbType.Int, Value = fingerprint });
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