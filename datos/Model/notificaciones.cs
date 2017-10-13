namespace datos.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class notificaciones
    {
        [Key]
        public int id_notificacion { get; set; }

        [StringLength(50)]
        public string icono { get; set; }

        [Required]
        [StringLength(250)]
        public string notificacion { get; set; }

        [Required]
        [StringLength(8000)]
        public string url { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        public bool leido { get; set; }

        public DateTime? fecha_leido { get; set; }

        public DateTime fecha_registro { get; set; }
    }
}
