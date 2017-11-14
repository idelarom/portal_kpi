using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace presentacion
{
    public class enumerators
    {
        public enum bonds_status
        {
            request = 1,
            authorization = 2,
            not_approved = 3,
            canceled = 4
        }

        public enum systems
        {
            cheques = 1,
            compensaciones = 2,
            viajes = 3,
            cobranza = 4
        }

        public enum profiles_compensations
        {
            administrador = 201,
            solicitante = 202,
            autorizador = 203,
            usuario = 204
        }

        public enum Periodicity
        {
            monthly = 1,
            quarterly = 2,
            semiannual = 3,
            annual = 4,
            bimestral = 5
        }

        public enum Months
        {
            January = 1,
            February = 2,
            March = 3,
            April = 4,
            May = 5,
            June = 6,
            July = 7,
            August = 8,
            September = 9,
            October = 10,
            November = 11,
            December = 12
        }
    }
}