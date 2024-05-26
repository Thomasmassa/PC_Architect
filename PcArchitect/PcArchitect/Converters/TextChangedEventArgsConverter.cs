using System.Globalization;

/*

Deze klasse definieert een converter die wordt gebruikt om de TextChangedEventArgs van een SearchBar naar een string te converteren. 

Wanneer de tekst in de SearchBar verandert, wordt de TextChanged event getriggerd. 
Deze event geeft een TextChangedEventArgs object door, dat de oude en nieuwe tekstwaarden bevat.

De EventToCommandBehavior van de Toolkit vangt deze event op en gebruikt deze converter om de nieuwe tekst te krijgen. 
De Convert methode van deze converter wordt aangeroepen met de TextChangedEventArgs als de value parameter. 
Deze methode retourneert de NewTextValue van de TextChangedEventArgs, dat is de nieuwe tekstwaarde.

Deze nieuwe tekstwaarde wordt vervolgens doorgegeven aan de TextChangedCommand in de ViewModel. 
Dit betekent dat elke keer dat de tekst in de SearchBar verandert, de TextChangedCommand in de ViewModel wordt getriggerd met de nieuwe tekst als parameter.

*/

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
