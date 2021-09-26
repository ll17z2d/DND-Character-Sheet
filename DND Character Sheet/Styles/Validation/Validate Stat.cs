using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

namespace DND_Character_Sheet.Styles.Validation
{
    public class Validate_Stat : ValidationRule
    {
        private const string ErrorMessage = "This isn't a valid modifier, please {Relevent error}";

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            BindingGroup group = (BindingGroup)value;
            var item = group.Items[0];
            string inputString = (group.GetValue(item, "Name") ?? string.Empty).ToString();
            if (int.TryParse(inputString, out var _))
            {
                return new ValidationResult(false, ErrorMessage);
            }

            return ValidationResult.ValidResult;
        }
    }
}
