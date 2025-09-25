namespace API_RESTful.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aggCorreoAClientes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cliente", "Correo", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cliente", "Correo");
        }
    }
}
