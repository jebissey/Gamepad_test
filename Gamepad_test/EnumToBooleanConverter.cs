﻿using System.Globalization;
using System.Windows.Data;

namespace Gamepad_test;

internal class EnumToBooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || parameter == null) return false;

        string? checkValue = value.ToString();
        string? targetValue = parameter.ToString();
        return checkValue?.Equals(targetValue, StringComparison.InvariantCultureIgnoreCase) ?? false;
    }

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || parameter == null) return null;

        bool useValue = (bool)value;
        string? targetValue = parameter.ToString();
        if (useValue && targetValue != null) return Enum.Parse(targetType, targetValue);

        return null;
    }
}
