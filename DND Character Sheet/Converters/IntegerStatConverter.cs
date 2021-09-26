using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

namespace DND_Character_Sheet.Converters
{
    [ValueConversion(typeof(int), typeof(int))]
    class IntegerStatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            //if (targetType != typeof(int))
            //    throw new InvalidOperationException("The target must be an integer");
            var x = value;

            return value;
            //return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            //if (targetType != typeof(int))
            //    throw new InvalidOperationException("The target must be an integer");

            if (int.TryParse((string)value, out var intValue))
            {
                return value;
            }

            return new ValidationResult(false, "Not an integer");
        }
        
    }
}
