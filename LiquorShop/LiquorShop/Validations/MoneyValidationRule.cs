using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Rms.Validations
{
    public class MoneyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var money = decimal.Parse(value.ToString(), NumberStyles.Currency);
                if (money < 0)
                {
                    return new ValidationResult(false, "Not Valid Money");
                }
                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Not Valid Money");
            }
        }
    }
}
