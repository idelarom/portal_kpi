//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace datos
{
    using System;
    using System.Collections.Generic;
    
    public partial class bonds_log
    {
        public int id_log { get; set; }
        public string employee_number { get; set; }
        public Nullable<int> id_bond_type { get; set; }
        public Nullable<int> id_bond_type_automatic { get; set; }
        public Nullable<decimal> amount_paid { get; set; }
        public Nullable<int> Periodicity { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<bool> Enable { get; set; }
    }
}