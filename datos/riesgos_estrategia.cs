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
    
    public partial class riesgos_estrategia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public riesgos_estrategia()
        {
            this.riesgos = new HashSet<riesgos>();
        }
    
        public int id_riesgo_estrategia { get; set; }
        public string usuario { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public Nullable<byte> valor_min { get; set; }
        public Nullable<byte> valor_max { get; set; }
        public bool activo { get; set; }
        public System.DateTime fecha { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<riesgos> riesgos { get; set; }
    }
}
