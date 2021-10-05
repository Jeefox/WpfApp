namespace WpfApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValuteDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Valutes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ValuteId = c.String(),
                        NumCode = c.String(),
                        CharCode = c.String(),
                        Nominal = c.Int(nullable: false),
                        Name = c.String(),
                        Value = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Valutes");
        }
    }
}
