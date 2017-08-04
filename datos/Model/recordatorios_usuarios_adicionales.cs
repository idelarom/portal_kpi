namespace datos.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class recordatorios_usuarios_adicionales
    {
        [Key]
        public int id_recordatorioua { get; set; }

        public int id_recordatorio { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        public bool activo { get; set; }

        public virtual recordatorios recordatorios { get; set; }
    }
}
