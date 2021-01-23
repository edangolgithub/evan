using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Rms.Validations
{
    public class IntegerValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var integer = int.Parse(value.ToString(),NumberStyles.Currency);
                if (integer <= 0)
                {
                    return new ValidationResult(false, "Not a Valid Number");
                }
                else
                {
                    return new ValidationResult(true, null);
                }
            }
            catch
            {
                return new ValidationResult(false, "Not a Valid Number");
            }
        }
    }
}
