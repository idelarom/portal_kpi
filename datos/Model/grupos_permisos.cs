namespace datos.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class grupos_permisos
    {
        [Key]
        public int id_grupop { get; set; }

        public int id_grupo { get; set; }

        public int id_permiso { get; set; }

        public bool activo { get; set; }

        public virtual grupos grupos { get; set; }

        public virtual permisos permisos { get; set; }
    }
}
