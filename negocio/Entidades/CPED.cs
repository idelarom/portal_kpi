using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace negocio.Entidades
{
    public class CPED
    {
        public string documento { get; set; }
        public decimal costo_usd { get; set; }
        public decimal costo_mn { get; set; }
        public string tipo_moneda { get; set; }
    }
}
