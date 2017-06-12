namespace datos.Modelos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class proyectos_involucrados
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public proyectos_involucrados()
        {
            proyectos_minutas_participantes = new HashSet<proyectos_minutas_participantes>();
            proyectos_minutas_pendientes = new HashSet<proyectos_minutas_pendientes>();
        }

        [Key]
        public int id_pinvolucrado { get; set; }

        public int id_proyecto { get; set; }

        public int id_rol { get; set; }

        public int? no_empleado { get; set; }

        [Required]
        [StringLength(1000)]
        public string nombre { get; set; }

        [StringLength(250)]
        public string telefono { get; set; }

        [StringLength(250)]
        public string celular { get; set; }

        [StringLength(250)]
        public string correo { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        public DateTime fecha_registro { get; set; }

        [StringLength(50)]
        public string usuario_edicion { get; set; }

        public DateTime? fecha_edicion { get; set; }

        [StringLength(50)]
        public string usuario_borrado { get; set; }

        public DateTime? fecha_borrado { get; set; }

        [StringLength(250)]
        public string comentarios_borrado { get; set; }

        public virtual proyectos proyectos { get; set; }

        public virtual roles_proyecto roles_proyecto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<proyectos_minutas_participantes> proyectos_minutas_participantes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<proyectos_minutas_pendientes> proyectos_minutas_pendientes { get; set; }
    }
}
