namespace datos.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class publicaciones_comentarios
    {
        [Key]
        public int id_publicacionc { get; set; }

        public int id_publicacion { get; set; }

        [Required]
        [StringLength(1000)]
        public string comentario { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        public DateTime fecha { get; set; }

        public bool activo { get; set; }

        public virtual publicaciones publicaciones { get; set; }
    }
}
