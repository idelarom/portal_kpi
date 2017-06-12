namespace datos.Modelos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class proyectos_entregables
    {
        [Key]
        public int id_entregable { get; set; }

        public int id_proyecto { get; set; }

        [Required]
        [StringLength(1000)]
        public string entregable { get; set; }

        public byte avance { get; set; }

        public DateTime fecha { get; set; }

        public DateTime fecha_registro { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        [StringLength(50)]
        public string usuario_edicion { get; set; }

        public DateTime? fecha_edicion { get; set; }

        [StringLength(50)]
        public string usuario_borrado { get; set; }

        public DateTime? fecha_borrado { get; set; }

        [StringLength(250)]
        public string comentarios_borrado { get; set; }

        public virtual proyectos proyectos { get; set; }
    }
}
