namespace datos.Modelos
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model : DbContext
    {
        public Model()
            : base("name=Modeldb")
        {
        }

        public virtual DbSet<menus> menus { get; set; }
        public virtual DbSet<proyectos> proyectos { get; set; }
        public virtual DbSet<proyectos_clientes_contactos> proyectos_clientes_contactos { get; set; }
        public virtual DbSet<proyectos_correos_historial> proyectos_correos_historial { get; set; }
        public virtual DbSet<proyectos_documentos> proyectos_documentos { get; set; }
        public virtual DbSet<proyectos_empleados> proyectos_empleados { get; set; }
        public virtual DbSet<proyectos_entregables> proyectos_entregables { get; set; }
        public virtual DbSet<proyectos_involucrados> proyectos_involucrados { get; set; }
        public virtual DbSet<proyectos_minutas> proyectos_minutas { get; set; }
        public virtual DbSet<proyectos_minutas_participantes> proyectos_minutas_participantes { get; set; }
        public virtual DbSet<proyectos_minutas_pendientes> proyectos_minutas_pendientes { get; set; }
        public virtual DbSet<proyectos_tareas> proyectos_tareas { get; set; }
        public virtual DbSet<roles_proyecto> roles_proyecto { get; set; }
        public virtual DbSet<usuarios> usuarios { get; set; }
        public virtual DbSet<usuarios_perfiles> usuarios_perfiles { get; set; }
        public virtual DbSet<usuarios_proyectos> usuarios_proyectos { get; set; }

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
                .Property(e => e.icon)
                .IsUnicode(false);

            modelBuilder.Entity<menus>()
                .Property(e => e.icon_ad)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos>()
                .Property(e => e.codigo_proyecto)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<proyectos>()
                .Property(e => e.proyecto)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos>()
                .Property(e => e.duración)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos>()
                .Property(e => e.costo_real)
                .HasPrecision(19, 4);

            modelBuilder.Entity<proyectos>()
                .Property(e => e.valor_ganado)
                .HasPrecision(19, 4);

            modelBuilder.Entity<proyectos>()
                .Property(e => e.fecha_inicio_str)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos>()
                .Property(e => e.fecha_fin_str)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos>()
                .Property(e => e.objetivos)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos>()
                .Property(e => e.descripcion_solucion)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos>()
                .Property(e => e.supuestos)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos>()
                .Property(e => e.fuera_alcance)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos>()
                .Property(e => e.riesgos_alto_nivel)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos>()
                .Property(e => e.usuario_edicion)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos>()
                .Property(e => e.usuario_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos>()
                .Property(e => e.comentarios_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos>()
                .HasMany(e => e.proyectos_clientes_contactos)
                .WithRequired(e => e.proyectos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<proyectos>()
                .HasMany(e => e.proyectos_correos_historial)
                .WithRequired(e => e.proyectos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<proyectos>()
                .HasMany(e => e.proyectos_documentos)
                .WithRequired(e => e.proyectos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<proyectos>()
                .HasMany(e => e.proyectos_empleados)
                .WithRequired(e => e.proyectos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<proyectos>()
                .HasMany(e => e.proyectos_entregables)
                .WithRequired(e => e.proyectos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<proyectos>()
                .HasMany(e => e.proyectos_involucrados)
                .WithRequired(e => e.proyectos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<proyectos>()
                .HasMany(e => e.proyectos_minutas)
                .WithRequired(e => e.proyectos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<proyectos>()
                .HasMany(e => e.proyectos_tareas)
                .WithRequired(e => e.proyectos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<proyectos_clientes_contactos>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_clientes_contactos>()
                .Property(e => e.correo)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_clientes_contactos>()
                .Property(e => e.telefono)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_clientes_contactos>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_clientes_contactos>()
                .Property(e => e.usuario_edicion)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_clientes_contactos>()
                .Property(e => e.usuario_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_clientes_contactos>()
                .Property(e => e.comentarios_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_correos_historial>()
                .Property(e => e.subject)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_correos_historial>()
                .Property(e => e.mail_to)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_correos_historial>()
                .Property(e => e.body)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_correos_historial>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_documentos>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_documentos>()
                .Property(e => e.contentType)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_documentos>()
                .Property(e => e.extension)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_documentos>()
                .Property(e => e.tamaño)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_documentos>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_documentos>()
                .Property(e => e.usuario_edicion)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_documentos>()
                .Property(e => e.usuario_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_documentos>()
                .Property(e => e.comentarios_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_empleados>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_empleados>()
                .Property(e => e.usuario_edicion)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_empleados>()
                .Property(e => e.usuario_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_empleados>()
                .Property(e => e.comentarios_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_entregables>()
                .Property(e => e.entregable)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_entregables>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_entregables>()
                .Property(e => e.usuario_edicion)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_entregables>()
                .Property(e => e.usuario_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_entregables>()
                .Property(e => e.comentarios_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_involucrados>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_involucrados>()
                .Property(e => e.telefono)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_involucrados>()
                .Property(e => e.celular)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_involucrados>()
                .Property(e => e.correo)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_involucrados>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_involucrados>()
                .Property(e => e.usuario_edicion)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_involucrados>()
                .Property(e => e.usuario_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_involucrados>()
                .Property(e => e.comentarios_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_minutas>()
                .Property(e => e.asunto)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_minutas>()
                .Property(e => e.propósito)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_minutas>()
                .Property(e => e.resultados)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_minutas>()
                .Property(e => e.acuerdos)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_minutas>()
                .Property(e => e.lugar)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_minutas>()
                .Property(e => e.estatus)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_minutas>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_minutas>()
                .Property(e => e.usuario_edicion)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_minutas>()
                .Property(e => e.usuario_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_minutas>()
                .Property(e => e.comentarios_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_minutas>()
                .HasMany(e => e.proyectos_minutas_participantes)
                .WithRequired(e => e.proyectos_minutas)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<proyectos_minutas>()
                .HasMany(e => e.proyectos_minutas_pendientes)
                .WithRequired(e => e.proyectos_minutas)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<proyectos_minutas_participantes>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_minutas_participantes>()
                .Property(e => e.organización)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_minutas_participantes>()
                .Property(e => e.rol)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_minutas_participantes>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_minutas_participantes>()
                .Property(e => e.usuario_edicion)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_minutas_participantes>()
                .Property(e => e.usuario_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_minutas_participantes>()
                .Property(e => e.comentarios_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_minutas_pendientes>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_minutas_pendientes>()
                .Property(e => e.responsable)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_minutas_pendientes>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_minutas_pendientes>()
                .Property(e => e.usuario_edicion)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_minutas_pendientes>()
                .Property(e => e.usuario_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_minutas_pendientes>()
                .Property(e => e.comentarios_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_tareas>()
                .Property(e => e.codigo_tarea)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_tareas>()
                .Property(e => e.tarea)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_tareas>()
                .Property(e => e.duración)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_tareas>()
                .Property(e => e.fecha_inicio_str)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_tareas>()
                .Property(e => e.fecha_fin_str)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_tareas>()
                .Property(e => e.recursos)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_tareas>()
                .Property(e => e.actividades_predecesoras)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_tareas>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_tareas>()
                .Property(e => e.usuario_edicion)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_tareas>()
                .Property(e => e.usuario_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<proyectos_tareas>()
                .Property(e => e.comentarios_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<roles_proyecto>()
                .Property(e => e.rol)
                .IsUnicode(false);

            modelBuilder.Entity<roles_proyecto>()
                .Property(e => e.responsabilidades)
                .IsUnicode(false);

            modelBuilder.Entity<roles_proyecto>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<roles_proyecto>()
                .Property(e => e.usuario_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<roles_proyecto>()
                .Property(e => e.comentarios_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<roles_proyecto>()
                .Property(e => e.usuario_edicion)
                .IsUnicode(false);

            modelBuilder.Entity<roles_proyecto>()
                .HasMany(e => e.proyectos_involucrados)
                .WithRequired(e => e.roles_proyecto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<usuarios>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios>()
                .Property(e => e.img_profile)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios>()
                .Property(e => e.usuario_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios>()
                .Property(e => e.comentarios_borrado)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios>()
                .Property(e => e.usuario_edicion)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios_perfiles>()
                .Property(e => e.perfil)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios_perfiles>()
                .HasMany(e => e.usuarios)
                .WithRequired(e => e.usuarios_perfiles)
                .WillCascadeOnDelete(false);
        }
    }
}
