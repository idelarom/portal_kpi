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
    
    public partial class tbl_VentaCruzada
    {
        public int Id_CNSC_VentaCruzada { get; set; }
        public Nullable<int> Id_CNSC_ProgramaVentas { get; set; }
        public Nullable<int> CveOport { get; set; }
        public Nullable<int> Fuente { get; set; }
        public Nullable<int> Empresa_Facturacion { get; set; }
        public string Referencia { get; set; }
        public string Vendedor_A { get; set; }
        public Nullable<int> Porcentaje_A { get; set; }
        public string Vendedor_B { get; set; }
        public Nullable<int> Porcentaje_B { get; set; }
        public Nullable<bool> Bit_Activo { get; set; }
        public string UsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
    }
}
