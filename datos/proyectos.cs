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
    
    public partial class proyectos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public proyectos()
        {
            this.actividades = new HashSet<actividades>();
            this.documentos = new HashSet<documentos>();
            this.proyectos_evaluaciones = new HashSet<proyectos_evaluaciones>();
        }
    
        public int id_proyecto { get; set; }
        public int id_proyecto_periodo { get; set; }
        public Nullable<int> id_proyecto_estatus { get; set; }
        public Nullable<int> cveoport { get; set; }
        public string folio_pmt { get; set; }
        public Nullable<int> id_cliente { get; set; }
        public string proyecto { get; set; }
        public string descripcion { get; set; }
        public Nullable<short> duración { get; set; }
        public Nullable<decimal> avance { get; set; }
        public Nullable<decimal> costo_real { get; set; }
        public Nullable<decimal> valor_ganado { get; set; }
        public Nullable<System.DateTime> fecha_inicio { get; set; }
        public Nullable<System.DateTime> fecha_fin { get; set; }
        public string objetivos { get; set; }
        public string descripcion_solucion { get; set; }
        public string supuestos { get; set; }
        public string fuera_alcance { get; set; }
        public string riesgos_alto_nivel { get; set; }
        public bool terminado { get; set; }
        public bool correo_bienvenida { get; set; }
        public System.DateTime fecha_registro { get; set; }
        public string usuario { get; set; }
        public string usuario_edicion { get; set; }
        public Nullable<System.DateTime> fecha_edicion { get; set; }
        public string usuario_borrado { get; set; }
        public string comentarios_borrado { get; set; }
        public Nullable<System.DateTime> fecha_borrado { get; set; }
    
        public virtual proyectos_estatus proyectos_estatus { get; set; }
        public virtual proyectos_periodos proyectos_periodos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<actividades> actividades { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<documentos> documentos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<proyectos_evaluaciones> proyectos_evaluaciones { get; set; }
    }
}
