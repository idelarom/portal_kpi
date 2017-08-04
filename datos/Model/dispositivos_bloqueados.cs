namespace datos.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class dispositivos_bloqueados
    {
        [Key]
        public int id_dispositivo_bloq { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        [Required]
        [StringLength(250)]
        public string device_fingerprint { get; set; }

        [StringLength(50)]
        public string ip { get; set; }

        public bool activo { get; set; }
    }
}
