namespace WpfApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBlogCreatedTimestamp : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Valutes", "Nominal", c => c.String());
            AlterColumn("dbo.Valutes", "Value", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Valutes", "Value", c => c.Double(nullable: false));
            AlterColumn("dbo.Valutes", "Nominal", c => c.Int(nullable: false));
        }
    }
}
