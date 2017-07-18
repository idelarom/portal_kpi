namespace datos.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class usuarios_widgets
    {
        [Key]
        public int id_usuariow { get; set; }

        public int id_widget { get; set; }

        public int orden { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_creador { get; set; }

        public DateTime fecha { get; set; }

        [StringLength(50)]
        public string usuario_edicion { get; set; }

        public DateTime? fecha_edicion { get; set; }

        [StringLength(50)]
        public string usuario_borrado { get; set; }

        public DateTime? fecha_borrado { get; set; }

        [StringLength(250)]
        public string comentarios_borrado { get; set; }

        public virtual widgets widgets { get; set; }
    }
}
