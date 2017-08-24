namespace datos.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class menus_usuarios
    {
        [Key]
        public int id_menuu { get; set; }

        public int id_menu { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        public bool activo { get; set; }

        public virtual menus menus { get; set; }
    }
}
