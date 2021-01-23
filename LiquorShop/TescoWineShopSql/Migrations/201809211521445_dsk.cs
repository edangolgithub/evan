namespace TescoWineShopSql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dsk : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.FiscalYears", new[] { "StartDate", "EndDate" }, unique: true, name: "IX_StartAndEnd");
        }
        
        public override void Down()
        {
            DropIndex("dbo.FiscalYears", "IX_StartAndEnd");
        }
    }
}
