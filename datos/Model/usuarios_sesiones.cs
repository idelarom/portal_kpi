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
        [Column(Order = 0)]
        [StringLength(50)]
        public string usuario { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(250)]
        public string os { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(250)]
        public string os_version { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(250)]
        public string device { get; set; }

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

        [Key]
        [Column(Order = 4)]
        [StringLength(250)]
        public string navegador { get; set; }

        [Key]
        [Column(Order = 5)]
        public DateTime fecha_inicio_sesion { get; set; }
    }
}
