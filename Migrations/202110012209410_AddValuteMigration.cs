namespace WpfApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValuteMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Valutes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        NumCode = c.String(),
                        CharCode = c.String(),
                        Nominal = c.Int(nullable: false),
                        Name = c.String(),
                        Value = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Valutes");
        }
    }
}
