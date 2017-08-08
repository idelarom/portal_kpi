namespace datos.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model : DbContext
    {
        public Model()
            : base("name=DB_PORTAL")
        {
        }

        public virtual DbSet<dispositivos_bloqueados> dispositivos_bloqueados { get; set; }
        public virtual DbSet<menus> menus { get; set; }
        public virtual DbSet<menus_perfiles> menus_perfiles { get; set; }
        public virtual DbSet<perfiles> perfiles { get; set; }
        public virtual DbSet<recordatorios> recordatorios { get; set; }
        public virtual DbSet<recordatorios_usuarios_adicionales> recordatorios_usuarios_adicionales { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<usuarios_perfiles> usuarios_perfiles { get; set; }
        public virtual DbSet<usuarios_sesiones> usuarios_sesiones { get; set; }
        public virtual DbSet<usuarios_widgets> usuarios_widgets { get; set; }
        public virtual DbSet<widgets> widgets { get; set; }
        public virtual DbSet<widgets_perfiles> widgets_perfiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<dispositivos_bloqueados>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<dispositivos_bloqueados>()
                .Property(e => e.device_fingerprint)
                .IsUnicode(false);

            modelBuilder.Entity<dispositivos_bloqueados>()
                .Property(e => e.ip)
                .IsUnicode(false);

            modelBuilder.Entity<menus>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<menus>()
                .Property(e => e.menu)
                .IsUnicode(false);

            modelBuilder.Entity<menus>()
                .Property(e => e.color_menu)
                .IsUnicode(false);

            modelBuilder.Entity<menus>()
                .Property(e => e.icon_ad)
                .IsUnicode(false);

            modelBuilder.Entity<menus>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<menus>()
                .Property(e => e.usuario_edicion)
                .IsUnicode(false);

            modelBuilder.Entity<menus>()
                .Property(e => e.usuario_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<menus>()
                .Property(e => e.comentarios_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<menus>()
                .HasMany(e => e.menus_perfiles)
                .WithRequired(e => e.menus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<menus_perfiles>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<menus_perfiles>()
                .Property(e => e.usuario_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<menus_perfiles>()
                .Property(e => e.comentarios_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<perfiles>()
                .Property(e => e.perfil)
                .IsUnicode(false);

            modelBuilder.Entity<perfiles>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<perfiles>()
                .Property(e => e.usuario_edicion)
                .IsUnicode(false);

            modelBuilder.Entity<perfiles>()
                .Property(e => e.usuario_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<perfiles>()
                .Property(e => e.comentarios_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<perfiles>()
                .HasMany(e => e.menus_perfiles)
                .WithRequired(e => e.perfiles)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<perfiles>()
                .HasMany(e => e.usuarios_perfiles)
                .WithRequired(e => e.perfiles)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<perfiles>()
                .HasMany(e => e.widgets_perfiles)
                .WithRequired(e => e.perfiles)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<recordatorios>()
                .Property(e => e.key_appointment_exchanged)
                .IsUnicode(false);

            modelBuilder.Entity<recordatorios>()
                .Property(e => e.organizer)
                .IsUnicode(false);

            modelBuilder.Entity<recordatorios>()
                .Property(e => e.organizer_address)
                .IsUnicode(false);

            modelBuilder.Entity<recordatorios>()
                .Property(e => e.titulo)
                .IsUnicode(false);

            modelBuilder.Entity<recordatorios>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<recordatorios>()
                .Property(e => e.location)
                .IsUnicode(false);

            modelBuilder.Entity<recordatorios>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<recordatorios>()
                .Property(e => e.usuario_creacion)
                .IsUnicode(false);

            modelBuilder.Entity<recordatorios>()
                .Property(e => e.usuario_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<recordatorios>()
                .Property(e => e.comentarios_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<recordatorios>()
                .HasMany(e => e.recordatorios_usuarios_adicionales)
                .WithRequired(e => e.recordatorios)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<recordatorios_usuarios_adicionales>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios_perfiles>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios_perfiles>()
                .Property(e => e.usuario_creador)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios_perfiles>()
                .Property(e => e.usuario_edicion)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios_perfiles>()
                .Property(e => e.usuario_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios_perfiles>()
                .Property(e => e.comentarios_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios_sesiones>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios_sesiones>()
                .Property(e => e.os)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios_sesiones>()
                .Property(e => e.os_version)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios_sesiones>()
                .Property(e => e.device)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios_sesiones>()
                .Property(e => e.device_fingerprint)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios_sesiones>()
                .Property(e => e.model)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios_sesiones>()
                .Property(e => e.ip)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios_sesiones>()
                .Property(e => e.latitud)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios_sesiones>()
                .Property(e => e.longitud)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios_sesiones>()
                .Property(e => e.region)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios_sesiones>()
                .Property(e => e.proveedor)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios_sesiones>()
                .Property(e => e.navegador)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios_widgets>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios_widgets>()
                .Property(e => e.usuario_creador)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios_widgets>()
                .Property(e => e.usuario_edicion)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios_widgets>()
                .Property(e => e.usuario_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios_widgets>()
                .Property(e => e.comentarios_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<widgets>()
                .Property(e => e.widget)
                .IsUnicode(false);

            modelBuilder.Entity<widgets>()
                .Property(e => e.nombre_codigo)
                .IsUnicode(false);

            modelBuilder.Entity<widgets>()
                .Property(e => e.icono)
                .IsUnicode(false);

            modelBuilder.Entity<widgets>()
                .Property(e => e.ejemplo_html)
                .IsUnicode(false);

            modelBuilder.Entity<widgets>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<widgets>()
                .Property(e => e.usuario_edicion)
                .IsUnicode(false);

            modelBuilder.Entity<widgets>()
                .Property(e => e.usuario_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<widgets>()
                .Property(e => e.comentarios_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<widgets>()
                .HasMany(e => e.usuarios_widgets)
                .WithRequired(e => e.widgets)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<widgets>()
                .HasMany(e => e.widgets_perfiles)
                .WithRequired(e => e.widgets)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<widgets_perfiles>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<widgets_perfiles>()
                .Property(e => e.usuario_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<widgets_perfiles>()
                .Property(e => e.comentarios_borrado)
                .IsUnicode(false);
        }
    }
}
