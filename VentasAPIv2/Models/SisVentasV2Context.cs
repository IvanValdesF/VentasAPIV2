using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VentasAPIv2.Models
{
    public partial class SisVentasV2Context : DbContext
    {
        public SisVentasV2Context()
        {
        }

        public SisVentasV2Context(DbContextOptions<SisVentasV2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Compra> Compras { get; set; } = null!;
        public virtual DbSet<ConceptosCompra> ConceptosCompras { get; set; } = null!;
        public virtual DbSet<ConceptosVentum> ConceptosVenta { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Sesionesactiva> Sesionesactivas { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<Ventum> Venta { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=SisVentasV2.mssql.somee.com;initial catalog=SisVentasV2;user id=IvanValdes01_SQLLogin_1;pwd=ldk7pepal8");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("cliente");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Correo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.ToTable("compra");

                entity.Property(e => e.Id)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Fecha)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("fecha");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("total");
            });

            modelBuilder.Entity<ConceptosCompra>(entity =>
            {
                entity.ToTable("conceptosCompra");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.IdCompra)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("id_compra");

                entity.Property(e => e.PrecioUnitario)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("precioUnitario");

                entity.Property(e => e.PrecioVenta)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("precioVenta");

                entity.Property(e => e.Producto)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("producto");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("total");

                entity.HasOne(d => d.IdCompraNavigation)
                    .WithMany(p => p.ConceptosCompras)
                    .HasForeignKey(d => d.IdCompra)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_conceptosCompra_compra");
            });

            modelBuilder.Entity<ConceptosVentum>(entity =>
            {
                entity.ToTable("conceptosVenta");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.IdVenta)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("id_venta");

                entity.Property(e => e.PrecioUnitario)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("precioUnitario");

                entity.Property(e => e.PrecioVenta)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("precioVenta");

                entity.Property(e => e.Producto)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("producto");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("total");

                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.ConceptosVenta)
                    .HasForeignKey(d => d.IdVenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_conceptosVenta_venta");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.Idproducto);

                entity.ToTable("producto");

                entity.Property(e => e.Idproducto)
                    .ValueGeneratedNever()
                    .HasColumnName("IDProducto");

                entity.Property(e => e.ImagenProducto)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.PrecioCompra).HasColumnType("decimal(16, 2)");

                entity.Property(e => e.PrecioVenta).HasColumnType("decimal(16, 2)");
            });

            modelBuilder.Entity<Sesionesactiva>(entity =>
            {
                entity.HasKey(e => e.Idusuario);

                entity.ToTable("sesionesactivas");

                entity.Property(e => e.Idusuario)
                    .ValueGeneratedNever()
                    .HasColumnName("IDUsuario");

                entity.Property(e => e.Contraseña)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NombreUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuario");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Contraseña)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ventum>(entity =>
            {
                entity.ToTable("venta");

                entity.Property(e => e.Id)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Fecha)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("fecha");

                entity.Property(e => e.Idcliente)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("idcliente");

                entity.Property(e => e.Montocambio)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("montocambio");

                entity.Property(e => e.Montopago)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("montopago");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("total");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
