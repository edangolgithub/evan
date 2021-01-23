using Rms.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TescoWineShopSql;

namespace Rms.Converters
{
    class AccountClassEnableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            AccountClass ac = value as AccountClass;
            if (ac == null)
            {
                return false;
            }
            else
            {
                int[] ids = { 1, 2, 3, 4, 5 };
                if (ids.Contains(ac.AccountClassId))
                {
                    return true;
                }
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
