namespace datos.Modelos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class proyectos_documentos
    {
        [Key]
        public int id_documento { get; set; }

        public int id_proyecto { get; set; }

        [Required]
        [StringLength(250)]
        public string nombre { get; set; }

        [Column(TypeName = "image")]
        [Required]
        public byte[] archivo { get; set; }

        [Required]
        [StringLength(250)]
        public string contentType { get; set; }

        [Required]
        [StringLength(50)]
        public string extension { get; set; }

        [Required]
        [StringLength(50)]
        public string tamaño { get; set; }

        public bool publico { get; set; }

        public bool archivo_proyecto { get; set; }

        public DateTime fecha_registro { get; set; }

        public bool encuesta { get; set; }

        public bool documento_cierre { get; set; }

        public bool kit_cliente { get; set; }

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
