using System.Globalization;

namespace PcArchitect.Converters
{
    public class TextChangedEventArgsConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var eventArgs = value as TextChangedEventArgs;
            return eventArgs?.NewTextValue;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
