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
    public class MenuCOM
    {
        public DataSet ListadoMenus(int id_menu_padre, bool administrador, string filtro, bool cliente)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_menu_padre", SqlDbType = SqlDbType.Int, Value = id_menu_padre }); 
            listparameters.Add(new SqlParameter() { ParameterName = "@administrador", SqlDbType = SqlDbType.Int, Value = administrador });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcliente", SqlDbType = SqlDbType.Int, Value = cliente });
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_menus", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
