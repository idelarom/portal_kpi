namespace datos.Modelos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class proyectos_correos_historial
    {
        [Key]
        public int id_pcorreo { get; set; }

        public int id_proyecto { get; set; }

        [Required]
        [StringLength(100)]
        public string subject { get; set; }

        [Required]
        public string mail_to { get; set; }

        [Required]
        public string body { get; set; }

        public DateTime fecha_envio { get; set; }

        public bool papelera { get; set; }

        public bool borrado { get; set; }

        public bool enviado_por_sistema { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        public virtual proyectos proyectos { get; set; }
    }
}
