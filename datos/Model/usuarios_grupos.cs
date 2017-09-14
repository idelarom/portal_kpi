namespace datos.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class usuarios_grupos
    {
        [Key]
        public int id_usuariog { get; set; }

        public int id_grupo { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        public bool activo { get; set; }

        public virtual grupos grupos { get; set; }
    }
}
