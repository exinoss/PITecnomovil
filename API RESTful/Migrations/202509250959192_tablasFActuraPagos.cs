namespace API_RESTful.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tablasFActuraPagos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Factura",
                c => new
                    {
                        IdFactura = c.Int(nullable: false, identity: true),
                        Numero = c.String(nullable: false, maxLength: 30),
                        FechaEmision = c.DateTime(nullable: false),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IVA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Estado = c.String(nullable: false, maxLength: 20),
                        IdVenta = c.Int(nullable: false),
                        IdCliente = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdFactura)
                .ForeignKey("dbo.Cliente", t => t.IdCliente)
                .ForeignKey("dbo.Venta", t => t.IdVenta)
                .Index(t => t.IdVenta)
                .Index(t => t.IdCliente);
            
            CreateTable(
                "dbo.Pago",
                c => new
                    {
                        IdPago = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Metodo = c.String(nullable: false, maxLength: 20),
                        IdFactura = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdPago)
                .ForeignKey("dbo.Factura", t => t.IdFactura)
                .Index(t => t.IdFactura);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Factura", "IdVenta", "dbo.Venta");
            DropForeignKey("dbo.Pago", "IdFactura", "dbo.Factura");
            DropForeignKey("dbo.Factura", "IdCliente", "dbo.Cliente");
            DropIndex("dbo.Pago", new[] { "IdFactura" });
            DropIndex("dbo.Factura", new[] { "IdCliente" });
            DropIndex("dbo.Factura", new[] { "IdVenta" });
            DropTable("dbo.Pago");
            DropTable("dbo.Factura");
        }
    }
}
