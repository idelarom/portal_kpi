//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace datos.OPTRACKER
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Compromisos
    {
        public int IdCompromiso { get; set; }
        public Nullable<int> CveOport { get; set; }
        public string Folio_Op { get; set; }
        public Nullable<int> Tipo_Compromiso { get; set; }
        public string Nombre_Compromiso { get; set; }
        public Nullable<System.DateTime> FechaComp_Ini { get; set; }
        public Nullable<System.DateTime> FechaCompromiso { get; set; }
        public string UsuRespComp { get; set; }
        public Nullable<System.DateTime> FechaAsignacionComp { get; set; }
        public string UsuRegAsignaComp { get; set; }
        public Nullable<int> Prioridad { get; set; }
        public Nullable<int> Tecnologia { get; set; }
        public Nullable<int> Clasificador { get; set; }
        public string DescCompromiso { get; set; }
        public Nullable<int> Estatus { get; set; }
        public Nullable<System.DateTime> FechaCierreComp { get; set; }
        public string UsuRegComp { get; set; }
        public Nullable<System.DateTime> FechaRegComp { get; set; }
        public string UsuActComp { get; set; }
        public Nullable<System.DateTime> FechaActComp { get; set; }
        public Nullable<bool> Incluye_Archivo { get; set; }
        public Nullable<bool> Cerrado { get; set; }
        public string UsuCierreComp { get; set; }
        public Nullable<double> TotalHoras { get; set; }
        public Nullable<double> TotalCostoAdic { get; set; }
        public Nullable<int> IdCategoria { get; set; }
        public Nullable<int> IdCliente { get; set; }
        public string IdRespComercial { get; set; }
        public Nullable<int> Id_CNSC_CatValoracion { get; set; }
        public string MotivoCierre { get; set; }
        public Nullable<byte> ReApertura { get; set; }
    }
}
