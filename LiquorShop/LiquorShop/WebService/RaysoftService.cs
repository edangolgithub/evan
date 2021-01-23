//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Runtime.Serialization;
//using System.ServiceModel;
//using System.Text;
//using LiquorShop.ViewModels.Sales;

//using TescoWineShopSql;

//namespace Rms.WebService
//{
//    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RaysoftService" in both code and config file together.
//    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
   
//    public class RaysoftService : IRaysoftService
//    {
//        SalesViewModel vm;
//        WineContext context = new TescoWineShopSql.WineContext();
//        public void a()
//        {
//            File.WriteAllText("d:\\ax.txt","hello wcf");
//        }

//        public List<Item> GetItems()
//        {
//             return context.Items.ToList(); 
            
//        }
//    }
//}
