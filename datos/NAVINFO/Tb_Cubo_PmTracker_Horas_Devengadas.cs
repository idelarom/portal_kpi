//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace datos.NAVINFO
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tb_Cubo_PmTracker_Horas_Devengadas
    {
        public string Folio { get; set; }
        public string Nombre_Proyecto { get; set; }
        public string Nombre_Cliente { get; set; }
        public string Estatus_Proyecto { get; set; }
        public System.DateTime Fecha_Estimada_Inicio_Proyecto { get; set; }
        public System.DateTime Fecha_Estimada_Fin_Proyecto { get; set; }
        public System.DateTime Fecha_Real_Inicio_Proyecto { get; set; }
        public System.DateTime Fecha_Real_Fin_Proyecto { get; set; }
        public string Usuario_Actividad { get; set; }
        public string Lider_Proyecto { get; set; }
        public string Descripcion_Corta_Actividad { get; set; }
        public string Descripcion_Detallada_Actividad { get; set; }
        public System.DateTime Fecha_Terminacion_Actividad { get; set; }
        public System.DateTime Fecha_Inicio_Actividad { get; set; }
        public int Id_Actividad { get; set; }
        public decimal Presupuesto_En_Horas { get; set; }
        public decimal Monto_Presupuestado { get; set; }
        public decimal Horas_Devengadas { get; set; }
        public Nullable<decimal> Costo_Hora { get; set; }
        public decimal Monto_Real { get; set; }
    }
}