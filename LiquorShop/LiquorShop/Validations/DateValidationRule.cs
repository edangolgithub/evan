using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Rms.Validations
{
    public class DateValidationRule : ValidationRule
    {
       public DateTime StartDate { get; set; }
       public DateTime EndDate { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            
            try
            {
                DateTime date = (DateTime)value;
                if (StartDate.Year != 0001 && EndDate.Year != 0001)
                {
                    if (StartDate > EndDate)
                    {
                        return new ValidationResult(false, "Not a valid date.");
                    }
                    else
                    {
                        return ValidationResult.ValidResult;
                    }

                }
                else
                {
                    if (date>DateTime.Now.Date)
                    {
                        return new ValidationResult(false, "Please enter a date less than or equals to today.");
                    }
                    else
                    {
                        return ValidationResult.ValidResult;
                    }
                }
            }
            catch (FormatException)
            {
                return new ValidationResult(false, "Value is not a valid date.");
            }
           
        }
    }
}
