//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace datos
{
    using System;
    using System.Collections.Generic;
    
    public partial class proyectos_empleados
    {
        public decimal id_proyectoe { get; set; }
        public int id_proyecto { get; set; }
        public bool administrador_proyecto { get; set; }
        public string usuario { get; set; }
        public bool activo { get; set; }
        public string usuario_registro { get; set; }
        public System.DateTime fecha_registro { get; set; }
    
        public virtual proyectos proyectos { get; set; }
    }
}