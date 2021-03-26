namespace TescoWineShopSql.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.IO;

    internal sealed class Configuration : DbMigrationsConfiguration<TescoWineShopSql.WineContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TescoWineShopSql.WineContext context)
        {
            DatabaseSeeder.Seed(context);

        }
    }
}