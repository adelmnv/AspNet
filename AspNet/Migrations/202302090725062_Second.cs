namespace AspNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accounts", "FirstName", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Accounts", "LastName", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Accounts", "Password", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Accounts", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Accounts", "Gender", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accounts", "Gender", c => c.String());
            AlterColumn("dbo.Accounts", "Email", c => c.String());
            AlterColumn("dbo.Accounts", "Password", c => c.String());
            AlterColumn("dbo.Accounts", "LastName", c => c.String());
            AlterColumn("dbo.Accounts", "FirstName", c => c.String());
        }
    }
}
