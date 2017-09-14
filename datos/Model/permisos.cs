namespace datos.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class permisos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public permisos()
        {
            grupos_permisos = new HashSet<grupos_permisos>();
            perfiles_permisos = new HashSet<perfiles_permisos>();
            usuarios_permisos = new HashSet<usuarios_permisos>();
        }

        [Key]
        public int id_permiso { get; set; }

        [Required]
        [StringLength(8000)]
        public string permiso { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_creacion { get; set; }

        public DateTime fecha_creacion { get; set; }

        public bool activo { get; set; }

        [StringLength(50)]
        public string usuario_edicion { get; set; }

        public DateTime? fecha_edicion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<grupos_permisos> grupos_permisos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<perfiles_permisos> perfiles_permisos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<usuarios_permisos> usuarios_permisos { get; set; }
    }
}
