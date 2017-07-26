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
    public class MenusCOM
    {
        public DataSet sp_menus_breadcumbs(string menu)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pmenu", SqlDbType = SqlDbType.Int, Value = menu });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_menus_breadcumbs", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        
        public DataSet sp_editar_menus(int id_menu, int id_menu_padre, string menu, string url, string icono, string usuario, bool en_mantenimiento)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_menu", SqlDbType = SqlDbType.Int, Value = id_menu });
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_menu_padre", SqlDbType = SqlDbType.Int, Value = id_menu_padre });
            listparameters.Add(new SqlParameter() { ParameterName = "@pname", SqlDbType = SqlDbType.Int, Value = menu });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmenu", SqlDbType = SqlDbType.Int, Value = url });
            listparameters.Add(new SqlParameter() { ParameterName = "@picon_ad", SqlDbType = SqlDbType.Int, Value = icono });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario", SqlDbType = SqlDbType.Int, Value = usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@en_mantenimiento", SqlDbType = SqlDbType.Int, Value = en_mantenimiento });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_editar_menu", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_agregar_menus(int id_menu_padre, string menu, string url, string icono, string usuario, bool en_mantenimiento)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_menu_padre", SqlDbType = SqlDbType.Int, Value = id_menu_padre });
            listparameters.Add(new SqlParameter() { ParameterName = "@pname", SqlDbType = SqlDbType.Int, Value = menu });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmenu", SqlDbType = SqlDbType.Int, Value = url });
            listparameters.Add(new SqlParameter() { ParameterName = "@picon_ad", SqlDbType = SqlDbType.Int, Value = icono });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario", SqlDbType = SqlDbType.Int, Value = usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@en_mantenimiento", SqlDbType = SqlDbType.Int, Value = en_mantenimiento });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_agregar_menu", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_borrar_menus(int id_menu, string usuario, string comentarios)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_menu", SqlDbType = SqlDbType.Int, Value = id_menu });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario", SqlDbType = SqlDbType.Int, Value = usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcomentarios", SqlDbType = SqlDbType.Int, Value = comentarios });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_borrar_menu", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_catalogo_menus(int id_menu)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pid_menu", SqlDbType = SqlDbType.Int, Value = id_menu });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_catalogo_menus", listparameters, false, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
