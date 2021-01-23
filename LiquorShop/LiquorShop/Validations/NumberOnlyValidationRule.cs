using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Rms.Validations
{
    class NumberOnlyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                //var integer = int.Parse(value.ToString(),NumberStyles.Currency);
                foreach (char c in (string)value)
                {
                    if (!Char.IsDigit(c))
                        return new ValidationResult(false, "error happened");
                }
                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "error happened");
            }
        }
    }
}
