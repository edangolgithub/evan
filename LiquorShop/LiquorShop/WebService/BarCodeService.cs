using LiquorShop.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Windows;
using TescoWineShopSql;
using WpfApp1;

namespace Rms.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BarCodeService" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class BarCodeService : IBarCodeService
    {
        SalesViewModel vm;
     WineContext context = new TescoWineShopSql.WineContext();
        private void InitPrintBill(string OrderId)
        {
            try
            {
                vm = ((App)Application.Current).SalesViewModel;
                int id;
                if (!int.TryParse(OrderId, out id))
                {
                    throw new Exception("Barcode doesn't belong to our software");
                }
                ((App)Application.Current).SalesViewModel.UpdateSelectedBeverages(id);
            }
            catch(Exception ex)
            {
                throw ex;
            }
              
        }
        public BarCodeService()
        {
          
        }
        public void DoWork(string Data)
        {
            try
            {
                string[] strings = Data.Split(' ');
                InitPrintBill(strings[0]);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
