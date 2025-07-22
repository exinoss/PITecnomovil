namespace API_RESTful.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescripcionAndStockToProducto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Productos", "Descripcion", c => c.String(nullable: false, maxLength: 500));
            AddColumn("dbo.Productos", "Stock", c => c.Int(nullable: false));
            AlterColumn("dbo.Productos", "Nombre", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Productos", "Nombre", c => c.String());
            DropColumn("dbo.Productos", "Stock");
            DropColumn("dbo.Productos", "Descripcion");
        }
    }
}
