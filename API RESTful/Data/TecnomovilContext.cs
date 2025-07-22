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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>().ToTable("Productos");
            modelBuilder.Entity<Cliente>().ToTable("Cliente");
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Reparacion>().ToTable("Reparacion");
            modelBuilder.Entity<Venta>().ToTable("Venta");
            modelBuilder.Entity<VentaProducto>().ToTable("VentaProducto");
            modelBuilder.Entity<VentaReparacion>().ToTable("VentaReparacion");

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

            base.OnModelCreating(modelBuilder);
        }
    }
}