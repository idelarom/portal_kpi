namespace datos.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class usuarios_configuraciones
    {
        [Key]
        public int id_usuarioc { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        [StringLength(500)]
        public string nombre { get; set; }

        public bool? sincronizacion_automatica { get; set; }

        public bool? mostrar_recordatorios { get; set; }

        public bool? alerta_inicio_sesion { get; set; }

        public bool activo { get; set; }
    }
}
