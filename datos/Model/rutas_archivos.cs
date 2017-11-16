namespace datos.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class rutas_archivos
    {
        [Key]
        public int id_ruta_archivo { get; set; }

        [Required]
        [StringLength(250)]
        public string descripcion { get; set; }

        [Required]
        [StringLength(8000)]
        public string path { get; set; }

        public bool activo { get; set; }
    }
}
