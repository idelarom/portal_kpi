namespace datos.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class grupos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public grupos()
        {
            grupos_permisos = new HashSet<grupos_permisos>();
            usuarios_grupos = new HashSet<usuarios_grupos>();
        }

        [Key]
        public int id_grupo { get; set; }

        [Required]
        [StringLength(1000)]
        public string grupo { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_creacion { get; set; }

        public DateTime fecha_creacion { get; set; }

        public bool activo { get; set; }

        public DateTime? fecha_edicion { get; set; }

        [StringLength(50)]
        public string usuario_edicion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<grupos_permisos> grupos_permisos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<usuarios_grupos> usuarios_grupos { get; set; }
    }
}
