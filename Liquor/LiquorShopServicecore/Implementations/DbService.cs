using Rms.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TescoWineShopSql;

namespace LiquorShopService.Implementations
{
    public class DbService
    {
        public static WineContext Context { get; set; }
       
        public static WineContext GetDbContext()
        {
            return Context;
        }
        public DbService()
        {

            try
            {
                var v = new StackFrame(1, true).GetMethod();
                Context = new WineContext();
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
    }

}
