﻿using datos.NAVISION;
using datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;


namespace negocio.Componentes
{
    public class DashboardCompromisosCOM
    {
        public DataSet Sps_DashBoardCompromisos(DateTime? fecha_ini, DateTime? fecha_fin, string pLstEmpleados)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pFechaInicial", SqlDbType = SqlDbType.Int, Value = fecha_ini });
            listparameters.Add(new SqlParameter() { ParameterName = "@pFechaFinal", SqlDbType = SqlDbType.Int, Value = fecha_fin });
            listparameters.Add(new SqlParameter() { ParameterName = "@pLstEmpleados", SqlDbType = SqlDbType.Int, Value = pLstEmpleados });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("Sps_DashBoardCompromisos", listparameters, false, 3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet Sps_CumplimientoCompromisos(DateTime? fecha_ini, DateTime? fecha_fin, string pLstEmpleados)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pFechaInicial", SqlDbType = SqlDbType.Int, Value = fecha_ini });
            listparameters.Add(new SqlParameter() { ParameterName = "@pFechaFinal", SqlDbType = SqlDbType.Int, Value = fecha_fin });
            listparameters.Add(new SqlParameter() { ParameterName = "@Responsable", SqlDbType = SqlDbType.Int, Value = pLstEmpleados });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("Sps_CumplimientoCompromisos", listparameters, false, 3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
