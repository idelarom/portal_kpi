namespace datos.Modelos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class proyectos_tareas
    {
        [Key]
        public int id_tarea { get; set; }

        [Required]
        [StringLength(20)]
        public string codigo_tarea { get; set; }

        public int id_proyecto { get; set; }

        public int? id_tarea_padre { get; set; }

        [Required]
        [StringLength(8000)]
        public string tarea { get; set; }

        [StringLength(250)]
        public string duración { get; set; }

        public byte? avance { get; set; }

        [StringLength(250)]
        public string fecha_inicio_str { get; set; }

        [StringLength(250)]
        public string fecha_fin_str { get; set; }

        public DateTime? fecha_inicio { get; set; }

        public DateTime? fecha_fin { get; set; }

        [StringLength(8000)]
        public string recursos { get; set; }

        [StringLength(250)]
        public string actividades_predecesoras { get; set; }

        public byte nivel_esquema { get; set; }

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
