using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TiendaOrg.Models;

public partial class TiendaOrgContext : DbContext
{
    public TiendaOrgContext()
    {
    }

    public TiendaOrgContext(DbContextOptions<TiendaOrgContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Vendedor> Vendedors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-CTL24QM\\SQLEXPRESS01; Database=TiendaOrg; Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria);

            entity.Property(e => e.NombreCategoria).HasMaxLength(50);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto);

            entity.ToTable("Producto");

            entity.Property(e => e.NombreProducto).HasMaxLength(50);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_Categorias");

            entity.HasOne(d => d.IdVendedorNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdVendedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_Vendedor");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol);

            entity.ToTable("Rol");

            entity.Property(e => e.TipoRol).HasMaxLength(50);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.ToTable("Usuario");

            entity.Property(e => e.Apellidos).HasMaxLength(50);
            entity.Property(e => e.Contrasena).HasMaxLength(50);
            entity.Property(e => e.CorreoE).HasMaxLength(50);
            entity.Property(e => e.Nombres).HasMaxLength(50);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Rol");
        });

        modelBuilder.Entity<Vendedor>(entity =>
        {
            entity.HasKey(e => e.IdVendedor);

            entity.ToTable("Vendedor");

            entity.Property(e => e.Logo).IsUnicode(false);
            entity.Property(e => e.NombreVendedor).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
