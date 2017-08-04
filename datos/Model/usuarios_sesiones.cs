namespace datos.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class usuarios_sesiones
    {
        [Key]
        public int id_usuario_sesion { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        [Required]
        [StringLength(250)]
        public string os { get; set; }

        [Required]
        [StringLength(250)]
        public string os_version { get; set; }

        [Required]
        [StringLength(250)]
        public string device { get; set; }

        [Required]
        [StringLength(250)]
        public string device_fingerprint { get; set; }

        [StringLength(250)]
        public string model { get; set; }

        [StringLength(50)]
        public string ip { get; set; }

        [StringLength(50)]
        public string latitud { get; set; }

        [StringLength(50)]
        public string longitud { get; set; }

        [StringLength(500)]
        public string region { get; set; }

        [StringLength(250)]
        public string proveedor { get; set; }

        [Required]
        [StringLength(250)]
        public string navegador { get; set; }

        public DateTime fecha_inicio_sesion { get; set; }

        public bool activo { get; set; }
    }
}
