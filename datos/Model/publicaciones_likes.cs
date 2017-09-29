namespace datos.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class publicaciones_likes
    {
        [Key]
        public int id_publicacionlike { get; set; }

        public int id_publicacion { get; set; }

        public bool activo { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        public DateTime fecha { get; set; }

        public virtual publicaciones publicaciones { get; set; }
    }
}
