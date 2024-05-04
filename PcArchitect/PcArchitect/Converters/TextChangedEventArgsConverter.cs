using System.Globalization;

// ELKE KEER DAT DE TEKST IN DE SEARCHBAR VERANDERT, WORDT DE TEXTCHANGED EVENT GETRIGGERD
// DE TOOLKIT:EVENTTOCOMMANDBEHAVIOR VANGT DEZE EVENT OP EN GEBRUIKT DEZE CONVERTER OM DE NIEUWE TEKST TE KRIJGEN
// DEZE NIEUWE TEKST WORDT DOORGEGEVEN AAN DE SEARCHCOMMAND IN DE VIEWMODEL

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
