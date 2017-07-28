namespace datos.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model : DbContext
    {
        public Model()
            : base("name=Model")
        {
        }

        public virtual DbSet<menus> menus { get; set; }
        public virtual DbSet<menus_perfiles> menus_perfiles { get; set; }
        public virtual DbSet<perfiles> perfiles { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<usuarios_perfiles> usuarios_perfiles { get; set; }
        public virtual DbSet<usuarios_widgets> usuarios_widgets { get; set; }
        public virtual DbSet<widgets> widgets { get; set; }
        public virtual DbSet<widgets_perfiles> widgets_perfiles { get; set; }
        public virtual DbSet<usuarios_sesiones> usuarios_sesiones { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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
        }
    }
}