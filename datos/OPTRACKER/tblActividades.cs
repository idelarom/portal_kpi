//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace datos.OPTRACKER
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblActividades
    {
        public int ActividadId { get; set; }
        public Nullable<int> IdTipoActividad { get; set; }
        public string DescripcionLarga { get; set; }
        public string Usuario { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public Nullable<System.DateTime> FechaInicio { get; set; }
        public Nullable<int> ReferenciaId { get; set; }
        public Nullable<bool> Prospecto { get; set; }
    }
}
