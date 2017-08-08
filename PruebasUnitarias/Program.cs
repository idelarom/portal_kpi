using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebasUnitarias
{
    class Program
    {
        static void Main(string[] args)
        {
            EWSHelper calendar = new EWSHelper();
            calendar.Initialize("isaac_delarosa@migesa.com.mx","IH1706RM");
        }
    }
}
