using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InventarioBiblioteca.Modelos;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autore> Autores { get; set; }

    public virtual DbSet<Estadoconservacion> Estadoconservacions { get; set; }

    public virtual DbSet<Inventariolibro> Inventariolibros { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<Librosautore> Librosautores { get; set; }

    public virtual DbSet<Tipoautor> Tipoautors { get; set; }

    public virtual DbSet<Tipolibro> Tipolibros { get; set; }

    public virtual DbSet<Tipousuario> Tipousuarios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<VInventario> VInventarios { get; set; }

    public virtual DbSet<VLibro> VLibros { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseNpgsql("Host=localhost;Database=inventariobiblioteca;Username=inventario;Password=desarrollo");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autore>(entity =>
        {
            entity.HasKey(e => e.Autorid).HasName("autores_pkey");

            entity.ToTable("autores");

            entity.Property(e => e.Autorid).HasColumnName("autorid");
            entity.Property(e => e.Nombreautor)
                .HasMaxLength(80)
                .HasColumnName("nombreautor");
            entity.Property(e => e.Tipoautorid).HasColumnName("tipoautorid");

            entity.HasOne(d => d.Tipoautor).WithMany(p => p.Autores)
                .HasForeignKey(d => d.Tipoautorid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("autores_tipoautorid_fkey");
        });

        modelBuilder.Entity<Estadoconservacion>(entity =>
        {
            entity.HasKey(e => e.Estadoid).HasName("estadoconservacion_pkey");

            entity.ToTable("estadoconservacion");

            entity.Property(e => e.Estadoid).HasColumnName("estadoid");
            entity.Property(e => e.Color)
                .HasMaxLength(7)
                .HasColumnName("color");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .HasColumnName("descripcion");
            entity.Property(e => e.Valor).HasColumnName("valor");
        });

        modelBuilder.Entity<Inventariolibro>(entity =>
        {
            entity.HasKey(e => e.Inventarioid).HasName("inventariolibro_pkey");

            entity.ToTable("inventariolibro");

            entity.Property(e => e.Inventarioid).HasColumnName("inventarioid");
            entity.Property(e => e.Codigo)
                .HasMaxLength(40)
                .HasColumnName("codigo");
            entity.Property(e => e.Estadoid).HasColumnName("estadoid");
            entity.Property(e => e.Libroid).HasColumnName("libroid");

            entity.HasOne(d => d.Estado).WithMany(p => p.Inventariolibros)
                .HasForeignKey(d => d.Estadoid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("inventariolibro_estadoid_fkey");

            entity.HasOne(d => d.Libro).WithMany(p => p.Inventariolibros)
                .HasForeignKey(d => d.Libroid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("inventariolibro_libroid_fkey");
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.Libroid).HasName("libros_pkey");

            entity.ToTable("libros");

            entity.Property(e => e.Libroid).HasColumnName("libroid");
            entity.Property(e => e.Año)
                .HasMaxLength(4)
                .HasColumnName("año");
            entity.Property(e => e.Edicion).HasColumnName("edicion");
            entity.Property(e => e.Editorial)
                .HasMaxLength(80)
                .HasColumnName("editorial");
            entity.Property(e => e.Nombrelib)
                .HasMaxLength(80)
                .HasColumnName("nombrelib");
            entity.Property(e => e.Tipoid).HasColumnName("tipoid");

            entity.HasOne(d => d.Tipo).WithMany(p => p.Libros)
                .HasForeignKey(d => d.Tipoid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("libros_tipoid_fkey");
        });

        modelBuilder.Entity<Librosautore>(entity =>
        {
            entity.HasKey(e => e.Libroautorid).HasName("librosautores_pk");

            entity.ToTable("librosautores");

            entity.Property(e => e.Libroautorid).HasColumnName("libroautorid");
            entity.Property(e => e.Autorid).HasColumnName("autorid");
            entity.Property(e => e.Libroid).HasColumnName("libroid");

            entity.HasOne(d => d.Autor).WithMany(p => p.Librosautores)
                .HasForeignKey(d => d.Autorid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("librosautores_autorid_fkey");

            entity.HasOne(d => d.Libro).WithMany(p => p.Librosautores)
                .HasForeignKey(d => d.Libroid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("librosautores_libroid_fkey");
        });

        modelBuilder.Entity<Tipoautor>(entity =>
        {
            entity.HasKey(e => e.Tipoautorid).HasName("tipoautor_pkey");

            entity.ToTable("tipoautor");

            entity.Property(e => e.Tipoautorid).HasColumnName("tipoautorid");
            entity.Property(e => e.Tipoautor1)
                .HasMaxLength(25)
                .HasColumnName("tipoautor");
        });

        modelBuilder.Entity<Tipolibro>(entity =>
        {
            entity.HasKey(e => e.Tipolibroid).HasName("tipolibro_pkey");

            entity.ToTable("tipolibro");

            entity.Property(e => e.Tipolibroid).HasColumnName("tipolibroid");
            entity.Property(e => e.Tipolibro1)
                .HasMaxLength(50)
                .HasColumnName("tipolibro");
        });

        modelBuilder.Entity<Tipousuario>(entity =>
        {
            entity.HasKey(e => e.Tipousuarioid).HasName("tipousuario_pkey");

            entity.ToTable("tipousuario");

            entity.Property(e => e.Tipousuarioid).HasColumnName("tipousuarioid");
            entity.Property(e => e.Tipousuario1)
                .HasMaxLength(50)
                .HasColumnName("tipousuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Usuarioid).HasName("usuario_pkey");

            entity.ToTable("usuario");

            entity.Property(e => e.Usuarioid).HasColumnName("usuarioid");
            entity.Property(e => e.Pwsd).HasColumnName("pwsd");
            entity.Property(e => e.Tipousuarioid).HasColumnName("tipousuarioid");
            entity.Property(e => e.Usu)
                .HasMaxLength(50)
                .HasColumnName("usu");

            entity.HasOne(d => d.Tipousuario).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Tipousuarioid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuario_tipousuarioid_fkey");
        });

        modelBuilder.Entity<VInventario>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_inventario");

            entity.Property(e => e.Codigo)
                .HasMaxLength(40)
                .HasColumnName("codigo");
            entity.Property(e => e.Color)
                .HasMaxLength(7)
                .HasColumnName("color");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estadoid).HasColumnName("estadoid");
            entity.Property(e => e.Inventarioid).HasColumnName("inventarioid");
            entity.Property(e => e.Libroid).HasColumnName("libroid");
            entity.Property(e => e.Valor).HasColumnName("valor");
        });

        modelBuilder.Entity<VLibro>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_libro");

            entity.Property(e => e.Autorid).HasColumnName("autorid");
            entity.Property(e => e.Año)
                .HasMaxLength(4)
                .HasColumnName("año");
            entity.Property(e => e.Edicion).HasColumnName("edicion");
            entity.Property(e => e.Editorial)
                .HasMaxLength(80)
                .HasColumnName("editorial");
            entity.Property(e => e.Libroid).HasColumnName("libroid");
            entity.Property(e => e.Nombreautor)
                .HasMaxLength(80)
                .HasColumnName("nombreautor");
            entity.Property(e => e.Nombrelib)
                .HasMaxLength(80)
                .HasColumnName("nombrelib");
            entity.Property(e => e.Tipolibro)
                .HasMaxLength(50)
                .HasColumnName("tipolibro");
            entity.Property(e => e.Tipolibroid).HasColumnName("tipolibroid");
        });
        modelBuilder.HasSequence("autores_autorid_seq");
        modelBuilder.HasSequence("estadoconservacion_estadoid_seq");
        modelBuilder.HasSequence("inventariolibro_inventarioid_seq");
        modelBuilder.HasSequence("libros_libroid_seq");
        modelBuilder.HasSequence("librosautores_libroautorid_seq");
        modelBuilder.HasSequence("tipoautor_tipoautorid_seq");
        modelBuilder.HasSequence("tipolibro_tipolibroid_seq");
        modelBuilder.HasSequence("tipousuario_tipousuarioid_seq");
        modelBuilder.HasSequence("usuario_usuarioid_seq");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
