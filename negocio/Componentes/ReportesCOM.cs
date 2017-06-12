using datos;
using datos.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;

namespace negocio.Componentes
{
    public class ReportesCOM
    {
        public DataSet sp_reporte_portal_proyectos(int id_proyecto, string usuario, bool administrador, int id_cliente,
            string usuario_filtro, DateTime fecha_inicio, DateTime fecha_fin,string cliente_filtro)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha_inicio", SqlDbType = SqlDbType.Int, Value = fecha_inicio });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha_fin", SqlDbType = SqlDbType.Int, Value = fecha_fin });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario", SqlDbType = SqlDbType.Int, Value = usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@padministrador", SqlDbType = SqlDbType.Bit, Value = administrador });
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_cliente", SqlDbType = SqlDbType.Bit, Value = id_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario_filtro", SqlDbType = SqlDbType.VarChar, Value = usuario_filtro });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcliente_filtro", SqlDbType = SqlDbType.VarChar, Value = cliente_filtro }); 
             Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_reporte_portal_proyectos", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
