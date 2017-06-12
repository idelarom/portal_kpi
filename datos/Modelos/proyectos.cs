namespace datos.Modelos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class proyectos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public proyectos()
        {
            proyectos_clientes_contactos = new HashSet<proyectos_clientes_contactos>();
            proyectos_correos_historial = new HashSet<proyectos_correos_historial>();
            proyectos_documentos = new HashSet<proyectos_documentos>();
            proyectos_empleados = new HashSet<proyectos_empleados>();
            proyectos_entregables = new HashSet<proyectos_entregables>();
            proyectos_involucrados = new HashSet<proyectos_involucrados>();
            proyectos_minutas = new HashSet<proyectos_minutas>();
            proyectos_tareas = new HashSet<proyectos_tareas>();
        }

        [Key]
        public int id_proyecto { get; set; }

        [Required]
        [StringLength(20)]
        public string codigo_proyecto { get; set; }

        public int? id_cliente { get; set; }

        [Required]
        [StringLength(1000)]
        public string proyecto { get; set; }

        [StringLength(8000)]
        public string descripcion { get; set; }

        [StringLength(250)]
        public string duración { get; set; }

        public byte avance { get; set; }

        [Column(TypeName = "money")]
        public decimal? costo_real { get; set; }

        [Column(TypeName = "money")]
        public decimal? valor_ganado { get; set; }

        public DateTime? fecha_inicio { get; set; }

        public DateTime? fecha_fin { get; set; }

        [StringLength(250)]
        public string fecha_inicio_str { get; set; }

        [StringLength(250)]
        public string fecha_fin_str { get; set; }

        [StringLength(8000)]
        public string objetivos { get; set; }

        [StringLength(8000)]
        public string descripcion_solucion { get; set; }

        [StringLength(8000)]
        public string supuestos { get; set; }

        [StringLength(8000)]
        public string fuera_alcance { get; set; }

        [StringLength(8000)]
        public string riesgos_alto_nivel { get; set; }

        public bool terminado { get; set; }

        public bool correo_bienvenida { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<proyectos_clientes_contactos> proyectos_clientes_contactos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<proyectos_correos_historial> proyectos_correos_historial { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<proyectos_documentos> proyectos_documentos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<proyectos_empleados> proyectos_empleados { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<proyectos_entregables> proyectos_entregables { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<proyectos_involucrados> proyectos_involucrados { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<proyectos_minutas> proyectos_minutas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<proyectos_tareas> proyectos_tareas { get; set; }
    }
}
