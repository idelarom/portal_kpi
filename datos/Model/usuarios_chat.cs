namespace datos.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class usuarios_chat
    {
        [Key]
        public int id_usuario_chat { get; set; }

        [Required]
        [StringLength(8000)]
        public string mensaje { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_envia { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_recibe { get; set; }

        public bool activo { get; set; }

        public DateTime fecha_enviado { get; set; }

        public DateTime? fecha_recibido { get; set; }
    }
}
