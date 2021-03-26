using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TescoWine.Converters
{
    class ImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var path= Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Images\\" +(string)value;
                return path;
            }
         
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Images\\jack.jpg";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class AnonIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            System.Type type = value.GetType();
            return (int)type.GetProperty("InvoiceNumber").GetValue(value, null);
        }
    }
}
