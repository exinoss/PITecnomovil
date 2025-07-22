namespace API_RESTful.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Productos",
                c => new
                    {
                        IdProducto = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.IdProducto);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Productos");
        }
    }
}
