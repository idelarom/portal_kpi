namespace datos.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ayudas
    {
        [Key]
        public int id_ayuda { get; set; }

        public int? id_ayuda_padre { get; set; }

        [StringLength(50)]
        public string icono { get; set; }

        [Required]
        [StringLength(250)]
        public string titulo { get; set; }

        [StringLength(8000)]
        public string descripcion { get; set; }

        [StringLength(8000)]
        public string codigo_html { get; set; }

        [StringLength(8000)]
        public string src { get; set; }

        public bool video { get; set; }

        public bool activo { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_creacion { get; set; }

        public DateTime fecha_creacion { get; set; }

        [StringLength(50)]
        public string usuario_edicion { get; set; }

        public DateTime? fecha_edicion { get; set; }
    }
}
