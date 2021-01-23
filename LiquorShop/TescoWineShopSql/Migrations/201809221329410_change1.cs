namespace TescoWineShopSql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "CompanyPassword", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "CompanyPassword");
        }
    }
}
