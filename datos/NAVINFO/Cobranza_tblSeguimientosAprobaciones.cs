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
    
    public partial class Cobranza_tblSeguimientosAprobaciones
    {
        public decimal SeguimientoAprobacionId { get; set; }
        public Nullable<decimal> CobranzaId { get; set; }
        public string DocumentoNo { get; set; }
        public Nullable<int> Cve_Empresa { get; set; }
        public Nullable<int> TipoActividadId { get; set; }
        public string Actividad { get; set; }
        public Nullable<System.DateTime> FechaActividad { get; set; }
        public Nullable<bool> AgendarLlamada { get; set; }
        public Nullable<System.DateTime> FechaLlamada { get; set; }
        public Nullable<int> EstatusCobranzaId { get; set; }
        public string OtroEstatusCobranza { get; set; }
        public Nullable<System.DateTime> FechaEstimadaCobro { get; set; }
        public Nullable<System.DateTime> FechaCreado { get; set; }
        public string CreadoPor { get; set; }
        public Nullable<System.DateTime> FechaModificado { get; set; }
        public string ModificadoPor { get; set; }
        public Nullable<bool> Reasignar { get; set; }
        public Nullable<System.DateTime> FechaAsignado { get; set; }
        public string AsignadoA { get; set; }
        public Nullable<int> SubEstatusCobranza1Id { get; set; }
        public Nullable<int> SubEstatusCobranza2Id { get; set; }
        public string Destinos { get; set; }
    
        public virtual Cobranza_tblCobranzas Cobranza_tblCobranzas { get; set; }
        public virtual Cobranza_tblEstatusCobranzas Cobranza_tblEstatusCobranzas { get; set; }
        public virtual Cobranza_tblSubEstatusCobranza1 Cobranza_tblSubEstatusCobranza1 { get; set; }
        public virtual Cobranza_tblSubEstatusCobranza2 Cobranza_tblSubEstatusCobranza2 { get; set; }
        public virtual Cobranza_tblTiposActividades Cobranza_tblTiposActividades { get; set; }
    }
}
