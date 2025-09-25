using API_RESTful.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace API_RESTful.Data
{
    public class TecnomovilContext : DbContext
    {
        public TecnomovilContext() : base("name=TecnomovilContext")
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Reparacion> Reparaciones { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<VentaProducto> VentaProductos { get; set; }
        public DbSet<VentaReparacion> VentaReparaciones { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Pago> Pagos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>().ToTable("Productos");
            modelBuilder.Entity<Cliente>().ToTable("Cliente");
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Reparacion>().ToTable("Reparacion");
            modelBuilder.Entity<Venta>().ToTable("Venta");
            modelBuilder.Entity<VentaProducto>().ToTable("VentaProducto");
            modelBuilder.Entity<VentaReparacion>().ToTable("VentaReparacion");
            modelBuilder.Entity<Factura>().ToTable("Factura");
            modelBuilder.Entity<Pago>().ToTable("Pago");

            // Configurar relaciones
            modelBuilder.Entity<Reparacion>()
                .HasRequired(r => r.Cliente)
                .WithMany()
                .HasForeignKey(r => r.IdCliente);

            modelBuilder.Entity<Reparacion>()
                .HasRequired(r => r.Usuario)
                .WithMany()
                .HasForeignKey(r => r.IdUsuario);

            modelBuilder.Entity<Venta>()
                .HasRequired(v => v.Cliente)
                .WithMany()
                .HasForeignKey(v => v.IdCliente);

            modelBuilder.Entity<Venta>()
                .HasRequired(v => v.Usuario)
                .WithMany()
                .HasForeignKey(v => v.IdUsuario);

            modelBuilder.Entity<VentaProducto>()
                .HasKey(vp => new { vp.IdVenta, vp.IdProducto });

            modelBuilder.Entity<VentaReparacion>()
                .HasKey(vr => new { vr.IdVenta, vr.IdReparacion });


            modelBuilder.Entity<VentaReparacion>()
                .HasRequired(vr => vr.Venta)               
                .WithMany(v => v.VentaReparaciones)       
                .HasForeignKey(vr => vr.IdVenta)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<VentaReparacion>()
                .HasRequired(vr => vr.Reparacion)
                .WithMany(r => r.VentaReparaciones)     
                .HasForeignKey(vr => vr.IdReparacion)
                .WillCascadeOnDelete(false);

            // Configurar precisión decimal para Factura
            modelBuilder.Entity<Factura>()
                .Property(f => f.Subtotal)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Factura>()
                .Property(f => f.IVA)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Factura>()
                .Property(f => f.Total)
                .HasPrecision(18, 2);

            // Configurar precisión decimal para Pago
            modelBuilder.Entity<Pago>()
                .Property(p => p.Monto)
                .HasPrecision(18, 2);

            // Configurar relaciones para Factura
            modelBuilder.Entity<Factura>()
                .HasRequired(f => f.Venta)
                .WithMany()
                .HasForeignKey(f => f.IdVenta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Factura>()
                .HasRequired(f => f.Cliente)
                .WithMany()
                .HasForeignKey(f => f.IdCliente)
                .WillCascadeOnDelete(false);

            // Configurar relaciones para Pago
            modelBuilder.Entity<Pago>()
                .HasRequired(p => p.Factura)
                .WithMany(f => f.Pagos)
                .HasForeignKey(p => p.IdFactura)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}