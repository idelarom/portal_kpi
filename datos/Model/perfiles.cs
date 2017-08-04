namespace datos.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class perfiles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public perfiles()
        {
            menus_perfiles = new HashSet<menus_perfiles>();
            usuarios_perfiles = new HashSet<usuarios_perfiles>();
            widgets_perfiles = new HashSet<widgets_perfiles>();
        }

        [Key]
        public int id_perfil { get; set; }

        [Required]
        [StringLength(250)]
        public string perfil { get; set; }

        public bool? ver_todos_empleados { get; set; }

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
        public virtual ICollection<menus_perfiles> menus_perfiles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<usuarios_perfiles> usuarios_perfiles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<widgets_perfiles> widgets_perfiles { get; set; }
    }
}
