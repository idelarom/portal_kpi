using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace datos
{
    internal class conexion
    {
        public static SqlConnection conectar(int TipoCadena)
        {
            SqlConnection connection = new SqlConnection();
            string cadena = obten_cadena_con(TipoCadena);
            connection.ConnectionString = cadena;
            return connection;
        }

        public static DataSet execute_sp(string query, List<SqlParameter> ListParameters, int TipoCadena)
        {
            SqlCommand command;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    string cadena = obten_cadena_con(TipoCadena);
                    connection.ConnectionString = cadena;
                    connection.Open();
                    command = new SqlCommand(query, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 120000;
                    foreach (SqlParameter item in ListParameters)
                    {
                        command.Parameters.AddWithValue(item.ParameterName, item.SqlDbType).Value = item.Value;
                    }
                    adapter = new SqlDataAdapter(command);
                    adapter.Fill(ds);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command = null;
                adapter = null;
            }
            return ds;
        }

        //ejecuta una funcion y lo regresa en un datatable
        public static DataTable execute_funcion(string query, int TipoCadena)
        {
            SqlCommand command;
            SqlDataAdapter adapter;
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    string cadena = obten_cadena_con(TipoCadena);
                    connection.ConnectionString = cadena;
                    connection.Open();
                    command = new SqlCommand(query, connection);

                    adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command = null;
                adapter = null;
            }
            return dt;
        }

        //obtener cadena de conexion segun el parametro de  System.Configuration.ConfigurationManager.AppSettings["cs"];
        public static string obten_cadena_con(int ValueCadCon)
        {
            string cadena = "";
            try
            {
                switch (ValueCadCon)
                {
                    case 1:
                        cadena = System.Configuration.ConfigurationManager.ConnectionStrings["DB_PORTAL"] ==  null ? "Data Source=mts-appslab01;Initial Catalog=portal_connext;User ID=sa;Password=asytlab01;MultipleActiveResultSets=True;App=EntityFramework":
                            System.Configuration.ConfigurationManager.ConnectionStrings["DB_PORTAL"].ToString();
                        break;
                    case 2:
                        cadena = System.Configuration.ConfigurationManager.ConnectionStrings["NAVISION"].ToString();
                        break;

                    case 3:
                        cadena = System.Configuration.ConfigurationManager.ConnectionStrings["ModelOPT"].ToString();
                        break;
                    case 4:
                        cadena = System.Configuration.ConfigurationManager.ConnectionStrings["ModelSICOEM"].ToString();
                        break;
                    case 5:
                        cadena = System.Configuration.ConfigurationManager.ConnectionStrings["Sailine"].ToString();
                        break;
                    case 6:
                        cadena = System.Configuration.ConfigurationManager.ConnectionStrings["PMTracker"].ToString();
                        break;
                    case 7:
                        cadena = System.Configuration.ConfigurationManager.ConnectionStrings["Bonos"].ToString();
                        break;
                    case 8:
                        cadena = System.Configuration.ConfigurationManager.ConnectionStrings["NAVINFO"].ToString();
                        break;
                    case 9:
                        cadena = System.Configuration.ConfigurationManager.ConnectionStrings["Proyectos_Connext"].ToString();
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cadena;
        }

        //ejecuta sp enviado desde ajax
        public static DataSet execute_sp_ajax(string sp, string[] parametros, object[] valores, int TipoCadena)
        {
            SqlCommand command;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    string cadena = obten_cadena_con(TipoCadena);
                    connection.ConnectionString = cadena;
                    connection.Open();
                    command = new SqlCommand(sp, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 60000;
                    if (parametros != null)
                    {
                        for (int i = 0; i <= (parametros.Length - 1); i++)
                        {
                            command.Parameters.AddWithValue(parametros[i], valores[i]);
                        }
                    }
                    adapter = new SqlDataAdapter(command);
                    adapter.Fill(ds);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command = null;
                adapter = null;
            }
            return ds;
        }
    }
}