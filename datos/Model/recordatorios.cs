namespace datos.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class recordatorios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public recordatorios()
        {
            recordatorios_usuarios_adicionales = new HashSet<recordatorios_usuarios_adicionales>();
        }

        [Key]
        public int id_recordatorio { get; set; }

        [StringLength(8000)]
        public string key_appointment_exchanged { get; set; }

        [StringLength(250)]
        public string organizer { get; set; }

        [StringLength(250)]
        public string organizer_address { get; set; }

        [Required]
        [StringLength(250)]
        public string titulo { get; set; }

        [StringLength(8000)]
        public string descripcion { get; set; }

        public DateTime fecha { get; set; }

        public DateTime? fecha_end { get; set; }

        [StringLength(500)]
        public string location { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        public bool activo { get; set; }

        public DateTime fecha_creacion { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_creacion { get; set; }

        [StringLength(50)]
        public string usuario_borrado { get; set; }

        public DateTime? fecha_borrado { get; set; }

        [StringLength(250)]
        public string comentarios_borrado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<recordatorios_usuarios_adicionales> recordatorios_usuarios_adicionales { get; set; }
    }
}
