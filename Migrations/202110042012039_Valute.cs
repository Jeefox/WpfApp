namespace WpfApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Valute : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Valutes", "Value", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Valutes", "Value", c => c.Double(nullable: false));
        }
    }
}
