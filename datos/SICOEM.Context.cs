﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SICOEMEntities : DbContext
    {
        public SICOEMEntities()
            : base("name=SICOEMEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<bonds_types> bonds_types { get; set; }
        public virtual DbSet<bons_automatic_types> bons_automatic_types { get; set; }
        public virtual DbSet<closing_dates> closing_dates { get; set; }
        public virtual DbSet<comments_types_payments> comments_types_payments { get; set; }
        public virtual DbSet<employees_compensations> employees_compensations { get; set; }
        public virtual DbSet<files_requests_bonds> files_requests_bonds { get; set; }
        public virtual DbSet<payment_history> payment_history { get; set; }
        public virtual DbSet<periodicity> periodicity { get; set; }
        public virtual DbSet<requests_bonds> requests_bonds { get; set; }
        public virtual DbSet<requests_status> requests_status { get; set; }
        public virtual DbSet<tab_reporte_bonos> tab_reporte_bonos { get; set; }
        public virtual DbSet<bonds_log> bonds_log { get; set; }
        public virtual DbSet<group_leader> group_leader { get; set; }
        public virtual DbSet<periodicity_ranges> periodicity_ranges { get; set; }
        public virtual DbSet<permissions_users_bonds_types> permissions_users_bonds_types { get; set; }
        public virtual DbSet<requests_automatic_status> requests_automatic_status { get; set; }
        public virtual DbSet<requests_bonds_Automatic> requests_bonds_Automatic { get; set; }
        public virtual DbSet<tbl_ReplaceBoss> tbl_ReplaceBoss { get; set; }
    }
}