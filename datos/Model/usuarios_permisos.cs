namespace datos.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class usuarios_permisos
    {
        [Key]
        public int id_usuariop { get; set; }

        public int id_permiso { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        public bool activo { get; set; }

        public virtual permisos permisos { get; set; }
    }
}
