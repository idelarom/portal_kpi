//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace datos.NAVINFO
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cobranza_tblEscalaciones
    {
        public decimal EscalacionId { get; set; }
        public Nullable<int> AreaId { get; set; }
        public Nullable<int> EmpresaId { get; set; }
        public Nullable<decimal> ResponsableId { get; set; }
        public Nullable<decimal> VendedorId { get; set; }
        public string Gerencia { get; set; }
        public Nullable<decimal> CompradorId { get; set; }
        public Nullable<int> RolId { get; set; }
        public Nullable<int> Dias { get; set; }
        public Nullable<bool> Activo { get; set; }
    
        public virtual Cobranza_tblAreas Cobranza_tblAreas { get; set; }
        public virtual Cobranza_tblRoles Cobranza_tblRoles { get; set; }
        public virtual Cobranza_tblUsuarios Cobranza_tblUsuarios { get; set; }
        public virtual Cobranza_tblUsuarios Cobranza_tblUsuarios1 { get; set; }
        public virtual Cobranza_tblUsuarios Cobranza_tblUsuarios2 { get; set; }
    }
}
