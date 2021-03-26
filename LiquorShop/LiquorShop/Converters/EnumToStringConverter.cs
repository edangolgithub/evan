using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using  TescoWineShopSql;

namespace TescoWine.Converters
{
    class EnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value==null)
            {
                return "";
            }
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
             return (Drinktype)Enum.Parse(typeof(Drinktype), value.ToString(), true);
            
        }
    }
}
