using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace datos
{
    public class Datos
    {
        /*
        public DataSet datos_Clientes(List<SqlParameter> ListParameters)
        {
            string query = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                query = "sp_clientes";
                ds = datos.conexion.execute_sp(query, ListParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                query = null;
            }
            return ds;
        } */

        public DataSet enviar(string query, List<SqlParameter> ListParameters, bool transaccion, int TipoCadena)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = datos.conexion.execute_sp(query, ListParameters, TipoCadena);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                query = null;
            }
            return ds;
        }

        ////enviar funcion
        public DataTable enviar_funcion(string query, int TipoCadena)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datos.conexion.execute_funcion(query, TipoCadena);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                query = null;
            }
            return dt;
        }

        public DataSet enviar_ajax(string query, string[] parametros, object[] valores, bool transaccion, int TipoCadena)
        {
            DataSet ds = new DataSet();
            try
            {
                string vmensaje;
                int intentos, contador;
                bool vabortado;
                contador = 0;

                intentos = 10;

                while (contador++ < intentos)
                {
                    ds = datos.conexion.execute_sp_ajax(query, parametros, valores, TipoCadena);

                    if (transaccion == true)
                    {
                        vmensaje = Convert.ToString(ds.Tables[0].Rows[0]["mensaje"]);
                        vabortado = Convert.ToBoolean(ds.Tables[0].Rows[0]["abortado"]);

                        if (vabortado == true | vmensaje == "")
                        {
                            //intentos = 0;
                            break;
                        }
                    }
                    else
                    {
                        //intentos = 0;
                        break; // se salga a la primera ya que es de consulta, y no intente en 10
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                query = null;
            }
            return ds;
        }
    }
}