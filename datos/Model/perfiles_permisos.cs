namespace datos.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class perfiles_permisos
    {
        [Key]
        public int id_perfilp { get; set; }

        public int id_perfil { get; set; }

        public int id_permiso { get; set; }

        public bool activo { get; set; }

        public virtual perfiles perfiles { get; set; }

        public virtual permisos permisos { get; set; }
    }
}
