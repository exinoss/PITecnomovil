namespace API_RESTful.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NuevaColumnaEstadoPagado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VentaReparacion", "EstadoPago", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.VentaReparacion", "FechaPago", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.VentaReparacion", "FechaPago");
            DropColumn("dbo.VentaReparacion", "EstadoPago");
        }
    }
}
