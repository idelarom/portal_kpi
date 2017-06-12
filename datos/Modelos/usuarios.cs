namespace datos.Modelos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class usuarios
    {
        [Key]
        public int id_usuario { get; set; }

        public int id_uperfil { get; set; }

        public int? id_cliente { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        [StringLength(250)]
        public string password { get; set; }

        [StringLength(8000)]
        public string img_profile { get; set; }

        public DateTime fecha_registro { get; set; }

        [StringLength(50)]
        public string usuario_borrado { get; set; }

        public DateTime? fecha_borrado { get; set; }

        [StringLength(250)]
        public string comentarios_borrado { get; set; }

        public DateTime? fecha_edicion { get; set; }

        [StringLength(50)]
        public string usuario_edicion { get; set; }

        public virtual usuarios_perfiles usuarios_perfiles { get; set; }
    }
}
