namespace datos.Modelos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class menus
    {
        [Key]
        public int id_menu { get; set; }

        public int? id_menu_padre { get; set; }

        [Required]
        [StringLength(250)]
        public string name { get; set; }

        [Required]
        [StringLength(1000)]
        public string menu { get; set; }

        [Required]
        [StringLength(250)]
        public string color_menu { get; set; }

        [Required]
        [StringLength(250)]
        public string icon { get; set; }

        [StringLength(250)]
        public string icon_ad { get; set; }

        public bool only_admin { get; set; }

        public bool? view_client { get; set; }

        public bool? borrado { get; set; }
    }
}
