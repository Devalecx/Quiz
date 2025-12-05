using System.Globalization;

namespace SoftwareEngineeringQuizApp.ViewModels;

/// <summary>
/// Convertidor que invierte un valor booleano
/// Útil para bindings donde necesitas el valor opuesto
/// </summary>
public class InvertedBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
            return !boolValue;

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
            return !boolValue;

        return false;
    }
}