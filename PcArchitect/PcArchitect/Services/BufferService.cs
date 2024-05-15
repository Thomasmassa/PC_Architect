/*
De BufferService klasse dient als opslagplaats voor componenten die met behulp van een Dictionary worden opgeslagen.

De sleutel is de naam van het component en de waarde is het component zelf. 
Deze klasse wordt gebruikt om componenten op te slaan voor gebruik op de detailpagina.

De BuffComponent methode voegt een nieuw component toe aan de buffer als het nog niet bestaat.

De GetBufferedComponent methode haalt een component op uit de buffer op basis van de naam, 
en maakt vervolgens de buffer leeg.
*/

namespace PcArchitect.Services
{
    public class BufferService
    {
        private Dictionary<string, object> buffer = [];

        public Dictionary<string, object> BuffComponent(string componentName, object component)
        {
            if (!buffer.ContainsKey(componentName))
            {
                buffer.Add(componentName, component);
            }
            return buffer;
        }

        public object GetBufferedComponent(string componentName)
        {
            object? component = null;
            if (buffer.ContainsKey(componentName))
            {
                component = buffer[componentName];
            }
            buffer.Clear();
            return component;
        }
    }
}
