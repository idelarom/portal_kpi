using datos;
using datos.Model;
using datos.NAVISION;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace negocio.Componentes
{
    public class RutasArchivosCOM
    {
        /// <summary>
        /// Regres ala ruta en el proyecto.
        /// tipo: 1 = Archivos de bonos
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public string path(int tipo)
        {
            try
            {
                Model db = new Model();
                string path = "";
                var result = (from a in db.rutas_archivos
                              where(a.activo && a.id_ruta_archivo == tipo)
                              select new {
                                  a.path
                              });
                DataTable dt = To.DataTable(result.ToList());
                if (dt.Rows.Count > 0)
                {
                    path = dt.Rows[0]["path"].ToString().Trim();
                }
                return path;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                return "";
            }
        }
    }
}
