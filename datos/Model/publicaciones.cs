namespace datos.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class publicaciones
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public publicaciones()
        {
            publicaciones_comentarios = new HashSet<publicaciones_comentarios>();
            publicaciones_imagenes = new HashSet<publicaciones_imagenes>();
            publicaciones_likes = new HashSet<publicaciones_likes>();
        }

        [Key]
        public int id_publicacion { get; set; }

        [Required]
        [StringLength(250)]
        public string titulo { get; set; }

        [Required]
        [StringLength(8000)]
        public string descripcion { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        public DateTime fecha { get; set; }

        public bool activo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<publicaciones_comentarios> publicaciones_comentarios { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<publicaciones_imagenes> publicaciones_imagenes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<publicaciones_likes> publicaciones_likes { get; set; }
    }
}
