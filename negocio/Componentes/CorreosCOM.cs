using datos;
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
    public class CorreosCOM
    {
        public Boolean SendMail(string mail, string subject, string mail_to)
        {
            try
            {
                string mess = "";
                Proyectos_ConnextEntities db = new Proyectos_ConnextEntities();
                int ret = db.sp_send_mail(mail,subject,mail_to);
                return true;
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
    }
}
