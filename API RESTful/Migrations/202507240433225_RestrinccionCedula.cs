namespace API_RESTful.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RestrinccionCedula : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Cliente", new[] { "Cedula" });
            AlterColumn("dbo.Cliente", "Cedula", c => c.String(nullable: false, maxLength: 10));
            CreateIndex("dbo.Cliente", "Cedula", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Cliente", new[] { "Cedula" });
            AlterColumn("dbo.Cliente", "Cedula", c => c.String(nullable: false, maxLength: 20));
            CreateIndex("dbo.Cliente", "Cedula", unique: true);
        }
    }
}
