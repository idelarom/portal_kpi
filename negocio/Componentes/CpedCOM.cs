using negocio.Entidades;
using datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace negocio.Componentes
{
    public class CpedCOM
    {
        /// <summary>
        /// Regresa una instancia de un CPED si existe, si no existe regresa null
        /// </summary>
        /// <param name="documento"></param>
        /// <returns></returns>
        public CPED cped(string documento)
        {
            DataTable dt = new DataTable();
            try
            {
                Proyectos_ConnextEntities db = new Proyectos_ConnextEntities();
                List<sp_get_cped_Result> resultado = db.sp_get_cped(documento).ToList();
                if (resultado.Count > 0)
                {
                    decimal costo_usd = 0;
                    decimal costo_mn = 0;
                    CPED cped = new CPED();
                    cped.documento = documento;
                    foreach (sp_get_cped_Result result in resultado)
                    {
                        costo_usd = Convert.ToDecimal(result.costo_usd);
                        costo_mn = Convert.ToDecimal(result.costo_mn);
                        cped.tipo_moneda = result.tipo_moneda;
                    }
                    cped.costo_usd = costo_usd;
                    cped.costo_mn = costo_usd;
                    return cped;
                }
                else {
                    return null;
                }
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                          .SelectMany(x => x.ValidationErrors)
                          .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                return null;
            }
        }
    }
}
