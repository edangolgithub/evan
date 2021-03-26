using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TescoWineShopSql
{
   public class DatabaseInitializer : CreateDatabaseIfNotExists<WineContext>
    {
        protected override void Seed(WineContext context)
        {
            DatabaseSeeder.Seed(context);
            base.Seed(context);
        }
    }
}
  
