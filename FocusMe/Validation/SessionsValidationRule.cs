using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FocusMe.Validation
{
    class SessionsValidationRule:ValidationRule
    {
        //This class is used to validate the textboxes bound to the Status.cs in Cofiguration.xaml and display the error messages 
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            int result;
            if (int.TryParse((string)value,out result))
            {
                if(result<=0)
             return new ValidationResult(false, "Number must be positive");
            }
            else if(string.IsNullOrEmpty((string) value))
            {
                return new ValidationResult(false, "Field can't be empty");
            }
            else
            {
                return new ValidationResult(false, "Not a valid value");
            }
            return new ValidationResult(true, null);
        }
    }
}
