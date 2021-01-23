using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TescoWineShopSql;

namespace TescoWine.Converters
{
    public class CustomerIdtobool : IValueConverter
    {
        TescoWineShopSql.WineContext context = new TescoWineShopSql.WineContext();
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var v = context.Sales.Where(a => a.CustomerId == (int)value).Where(a => a.Due>0).FirstOrDefault();
            if(v==null)
            {
                return true;
            }
            return false;
          
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
