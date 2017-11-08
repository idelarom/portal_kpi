using datos.OPTRACKER;
using datos.NAVISION;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;

namespace negocio.Componentes
{
    public class OportunidadesCOM
    {
        /// <summary>
        /// Regresa un mensaje vacio si existe una oportunidad con la clave indicada
        /// </summary>
        /// <param name="cveoport"></param>
        /// <returns></returns>
        public String ExistFolioOport(string cveoport)
        {
            DataTable dt = new DataTable();
            try
            {
                OPTrackerEntities context = new OPTrackerEntities();
                var query = context.Oportunidad
                                .Where(s => s.Folio_Op.Trim().ToUpper() == cveoport.Trim().ToUpper())
                                .Select(u => new
                                {
                                    u.CveOport
                                })
                                .OrderBy(u => u.CveOport);
                dt = To.DataTable(query.ToList());
                return dt.Rows.Count > 0 ? "":"No existe ninguna oportunidad con folio: "+cveoport.ToString();
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

        /// <summary>
        /// Regresa un mensaje vacio si existe un folio
        /// </summary>
        /// <param name="cveoport"></param>
        /// <returns></returns>
        public String ExistFoliopm(String Folio_Pm)
        {
            DataTable dt = new DataTable();
            try
            {
                OPTrackerEntities context = new OPTrackerEntities();
                var query = context.Oportunidad
                                .Where(s => s.Folio_Op.Trim() == Folio_Pm.Trim())
                                .Select(u => new
                                {
                                    u.CveOport
                                })
                                .OrderBy(u => u.CveOport);
                dt = To.DataTable(query.ToList());
                return dt.Rows.Count > 0 ? "" : "No existe ningun folio: " + Folio_Pm.ToString();
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
    }
}
