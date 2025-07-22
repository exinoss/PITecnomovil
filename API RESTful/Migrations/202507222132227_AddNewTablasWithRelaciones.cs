namespace API_RESTful.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewTablasWithRelaciones : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        IdCliente = c.Int(nullable: false, identity: true),
                        Nombres = c.String(nullable: false, maxLength: 100),
                        Cedula = c.String(nullable: false, maxLength: 20),
                        Contacto = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.IdCliente)
                .Index(t => t.Cedula, unique: true);
            
            CreateTable(
                "dbo.Reparacion",
                c => new
                    {
                        IdReparacion = c.Int(nullable: false, identity: true),
                        IdCliente = c.Int(nullable: false),
                        IdUsuario = c.Int(nullable: false),
                        FechaIngreso = c.DateTime(nullable: false),
                        FechaEntrega = c.DateTime(),
                        Estado = c.String(nullable: false, maxLength: 20),
                        Diagnostico = c.String(),
                        Observaciones = c.String(),
                        Dispositivo = c.String(nullable: false, maxLength: 50),
                        PrecioServicio = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.IdReparacion)
                .ForeignKey("dbo.Cliente", t => t.IdCliente, cascadeDelete: true)
                .ForeignKey("dbo.Usuario", t => t.IdUsuario, cascadeDelete: true)
                .Index(t => t.IdCliente)
                .Index(t => t.IdUsuario);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        IdUsuario = c.Int(nullable: false, identity: true),
                        NombreUsuario = c.String(nullable: false, maxLength: 50),
                        Clave = c.String(nullable: false, maxLength: 100),
                        Rol = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.IdUsuario)
                .Index(t => t.NombreUsuario, unique: true);
            
            CreateTable(
                "dbo.VentaReparacion",
                c => new
                    {
                        IdVenta = c.Int(nullable: false),
                        IdReparacion = c.Int(nullable: false),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.IdVenta, t.IdReparacion })
                .ForeignKey("dbo.Reparacion", t => t.IdReparacion)
                .ForeignKey("dbo.Venta", t => t.IdVenta)
                .Index(t => t.IdVenta)
                .Index(t => t.IdReparacion);
            
            CreateTable(
                "dbo.Venta",
                c => new
                    {
                        IdVenta = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IdCliente = c.Int(nullable: false),
                        IdUsuario = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdVenta)
                .ForeignKey("dbo.Cliente", t => t.IdCliente, cascadeDelete: true)
                .ForeignKey("dbo.Usuario", t => t.IdUsuario, cascadeDelete: true)
                .Index(t => t.IdCliente)
                .Index(t => t.IdUsuario);
            
            CreateTable(
                "dbo.VentaProducto",
                c => new
                    {
                        IdVenta = c.Int(nullable: false),
                        IdProducto = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.IdVenta, t.IdProducto })
                .ForeignKey("dbo.Productos", t => t.IdProducto, cascadeDelete: true)
                .ForeignKey("dbo.Venta", t => t.IdVenta, cascadeDelete: true)
                .Index(t => t.IdVenta)
                .Index(t => t.IdProducto);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VentaProducto", "IdVenta", "dbo.Venta");
            DropForeignKey("dbo.VentaProducto", "IdProducto", "dbo.Productos");
            DropForeignKey("dbo.VentaReparacion", "IdVenta", "dbo.Venta");
            DropForeignKey("dbo.Venta", "IdUsuario", "dbo.Usuario");
            DropForeignKey("dbo.Venta", "IdCliente", "dbo.Cliente");
            DropForeignKey("dbo.VentaReparacion", "IdReparacion", "dbo.Reparacion");
            DropForeignKey("dbo.Reparacion", "IdUsuario", "dbo.Usuario");
            DropForeignKey("dbo.Reparacion", "IdCliente", "dbo.Cliente");
            DropIndex("dbo.VentaProducto", new[] { "IdProducto" });
            DropIndex("dbo.VentaProducto", new[] { "IdVenta" });
            DropIndex("dbo.Venta", new[] { "IdUsuario" });
            DropIndex("dbo.Venta", new[] { "IdCliente" });
            DropIndex("dbo.VentaReparacion", new[] { "IdReparacion" });
            DropIndex("dbo.VentaReparacion", new[] { "IdVenta" });
            DropIndex("dbo.Usuario", new[] { "NombreUsuario" });
            DropIndex("dbo.Reparacion", new[] { "IdUsuario" });
            DropIndex("dbo.Reparacion", new[] { "IdCliente" });
            DropIndex("dbo.Cliente", new[] { "Cedula" });
            DropTable("dbo.VentaProducto");
            DropTable("dbo.Venta");
            DropTable("dbo.VentaReparacion");
            DropTable("dbo.Usuario");
            DropTable("dbo.Reparacion");
            DropTable("dbo.Cliente");
        }
    }
}
