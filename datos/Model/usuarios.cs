namespace datos.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class usuarios
    {
        [Key]
        public int id_usuario { get; set; }

        [StringLength(20)]
        public string No_ { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        [Required]
        [StringLength(8000)]
        public string contraseña { get; set; }

        [StringLength(250)]
        public string puesto { get; set; }

        [StringLength(250)]
        public string nombres { get; set; }

        [StringLength(100)]
        public string a_paterno { get; set; }

        [StringLength(100)]
        public string a_materno { get; set; }

        [StringLength(250)]
        public string correo { get; set; }

        [StringLength(8000)]
        public string path_imagen { get; set; }

        public bool activo { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_alta { get; set; }

        public DateTime fecha { get; set; }

        [StringLength(50)]
        public string usuario_borrado { get; set; }

        public DateTime? fecha_borrado { get; set; }

        [StringLength(250)]
        public string comentarios_borrado { get; set; }
    }
}
