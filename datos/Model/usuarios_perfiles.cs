namespace datos.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class usuarios_perfiles
    {
        [Key]
        public int id_usuariop { get; set; }

        public int id_perfil { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        public DateTime fecha { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_creador { get; set; }

        public DateTime? fecha_edicion { get; set; }

        [StringLength(50)]
        public string usuario_edicion { get; set; }

        public DateTime? fecha_borrado { get; set; }

        [StringLength(50)]
        public string usuario_borrado { get; set; }

        [StringLength(250)]
        public string comentarios_borrado { get; set; }

        public virtual perfiles perfiles { get; set; }
    }
}
