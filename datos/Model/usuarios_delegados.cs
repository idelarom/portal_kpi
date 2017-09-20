namespace datos.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class usuarios_delegados
    {
        [Key]
        public int id_usuariod { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_jefe { get; set; }

        public bool activo { get; set; }
    }
}
