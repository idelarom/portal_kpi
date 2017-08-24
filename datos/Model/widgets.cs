namespace datos.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class widgets
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public widgets()
        {
            usuarios_widgets = new HashSet<usuarios_widgets>();
            widgets_perfiles = new HashSet<widgets_perfiles>();
        }

        [Key]
        public int id_widget { get; set; }

        [Required]
        [StringLength(250)]
        public string widget { get; set; }

        [Required]
        [StringLength(250)]
        public string nombre_codigo { get; set; }

        [StringLength(8000)]
        public string texto_ayuda { get; set; }

        public bool? individual { get; set; }

        [StringLength(250)]
        public string icono { get; set; }

        [StringLength(8000)]
        public string ejemplo_html { get; set; }

        public DateTime fecha { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        public DateTime? fecha_edicion { get; set; }

        [StringLength(50)]
        public string usuario_edicion { get; set; }

        public DateTime? fecha_borrado { get; set; }

        [StringLength(50)]
        public string usuario_borrado { get; set; }

        [StringLength(250)]
        public string comentarios_borrado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<usuarios_widgets> usuarios_widgets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<widgets_perfiles> widgets_perfiles { get; set; }
    }
}
