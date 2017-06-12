namespace datos.Modelos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class proyectos_minutas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public proyectos_minutas()
        {
            proyectos_minutas_participantes = new HashSet<proyectos_minutas_participantes>();
            proyectos_minutas_pendientes = new HashSet<proyectos_minutas_pendientes>();
        }

        [Key]
        public int id_minuta { get; set; }

        public int id_proyecto { get; set; }

        [Required]
        [StringLength(1000)]
        public string asunto { get; set; }

        public DateTime fecha { get; set; }

        [Required]
        [StringLength(8000)]
        public string propósito { get; set; }

        [StringLength(8000)]
        public string resultados { get; set; }

        [StringLength(8000)]
        public string acuerdos { get; set; }

        [Required]
        [StringLength(250)]
        public string lugar { get; set; }

        [Required]
        [StringLength(10)]
        public string estatus { get; set; }

        public DateTime fecha_registro { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        [StringLength(50)]
        public string usuario_edicion { get; set; }

        public DateTime? fecha_edicion { get; set; }

        [StringLength(50)]
        public string usuario_borrado { get; set; }

        [StringLength(250)]
        public string comentarios_borrado { get; set; }

        public DateTime? fecha_borrado { get; set; }

        public virtual proyectos proyectos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<proyectos_minutas_participantes> proyectos_minutas_participantes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<proyectos_minutas_pendientes> proyectos_minutas_pendientes { get; set; }
    }
}
