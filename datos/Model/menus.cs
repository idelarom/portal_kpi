namespace datos.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class menus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public menus()
        {
            menus_perfiles = new HashSet<menus_perfiles>();
            menus_usuarios = new HashSet<menus_usuarios>();
        }

        [Key]
        public int id_menu { get; set; }

        public int? id_menu_padre { get; set; }

        [Required]
        [StringLength(250)]
        public string name { get; set; }

        [StringLength(1000)]
        public string menu { get; set; }

        [StringLength(250)]
        public string color_menu { get; set; }

        [Required]
        [StringLength(250)]
        public string icon_ad { get; set; }

        public bool en_mantenimiento { get; set; }

        public DateTime? fecha_inicio_mtto { get; set; }

        public DateTime? fecha_fin_mtto { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        public DateTime fecha { get; set; }

        [StringLength(50)]
        public string usuario_edicion { get; set; }

        public DateTime? fecha_edicion { get; set; }

        [StringLength(50)]
        public string usuario_borrado { get; set; }

        public DateTime? fecha_borrado { get; set; }

        [StringLength(250)]
        public string comentarios_borrado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<menus_perfiles> menus_perfiles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<menus_usuarios> menus_usuarios { get; set; }
    }
}
