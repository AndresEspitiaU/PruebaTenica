using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PruebaTecnica.Models;

public partial class PruebaTecnicaContext : DbContext
{
    public PruebaTecnicaContext()
    {
    }

    public PruebaTecnicaContext(DbContextOptions<PruebaTecnicaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=PruebaTecnica;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasKey(e => e.MovId).HasName("PK__Movimien__D1BE75C7011C0724");

            entity.ToTable("Movimiento");

            entity.Property(e => e.MovId).HasColumnName("mov_id");
            entity.Property(e => e.MovCantidad)
                .HasMaxLength(50)
                .HasColumnName("mov_cantidad");
            entity.Property(e => e.MovFecha).HasColumnName("mov_fecha");
            entity.Property(e => e.MovTipo)
                .HasMaxLength(50)
                .HasColumnName("mov_tipo");
            entity.Property(e => e.MovValor)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("mov_valor");
            entity.Property(e => e.ProId).HasColumnName("pro_id");

            entity.HasOne(d => d.Pro).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.ProId)
                .HasConstraintName("FK__Movimient__pro_i__47DBAE45");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProId).HasName("PK__Producto__335E4CA608FB0941");

            entity.ToTable("Producto");

            entity.Property(e => e.ProId).HasColumnName("pro_id");
            entity.Property(e => e.ProActivo).HasColumnName("pro_activo");
            entity.Property(e => e.ProCanmax)
                .HasMaxLength(50)
                .HasColumnName("pro_canmax");
            entity.Property(e => e.ProCanmini)
                .HasMaxLength(50)
                .HasColumnName("pro_canmini");
            entity.Property(e => e.ProCantidad)
                .HasMaxLength(50)
                .HasColumnName("pro_cantidad");
            entity.Property(e => e.ProCodigo)
                .HasMaxLength(50)
                .HasColumnName("pro_codigo");
            entity.Property(e => e.ProCostocompra)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("pro_costocompra");
            entity.Property(e => e.ProCostoventa1)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("pro_costoventa1");
            entity.Property(e => e.ProCostoventa2)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("pro_costoventa2");
            entity.Property(e => e.ProDescripcion)
                .HasMaxLength(1000)
                .HasColumnName("pro_descripcion");
            entity.Property(e => e.ProDetalle)
                .HasMaxLength(100)
                .HasColumnName("pro_detalle");
            entity.Property(e => e.ProMarca)
                .HasMaxLength(100)
                .HasColumnName("pro_marca");
            entity.Property(e => e.ProNombre)
                .HasMaxLength(50)
                .HasColumnName("pro_nombre");
            entity.Property(e => e.ProPresentacion)
                .HasMaxLength(100)
                .HasColumnName("pro_presentacion");
            entity.Property(e => e.ProProveedor)
                .HasMaxLength(100)
                .HasColumnName("pro_proveedor");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
