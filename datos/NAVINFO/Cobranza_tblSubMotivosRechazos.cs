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
    
    public partial class Cobranza_tblSubMotivosRechazos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cobranza_tblSubMotivosRechazos()
        {
            this.Cobranza_tblRechazos = new HashSet<Cobranza_tblRechazos>();
        }
    
        public int SubMotivoRechazoId { get; set; }
        public Nullable<int> MotivoRechazoId { get; set; }
        public string Nombre { get; set; }
        public Nullable<bool> Activo { get; set; }
    
        public virtual Cobranza_tblMotivosRechazos Cobranza_tblMotivosRechazos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cobranza_tblRechazos> Cobranza_tblRechazos { get; set; }
    }
}
