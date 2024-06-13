/*

De BufferService klasse dient als opslagplaats voor builds die met behulp van een Dictionary worden opgeslagen.

De sleutel is de naam van de build en de waarde is de opgeslagen build zelf. 
Deze klasse wordt gebruikt om builds op te slaan voor gebruik op de MyBuild pagina, maar ook om op te vragen op de StartBuilding pagina.

De BuffComponent methode voegt een nieuw build toe aan de buffer als het nog niet bestaat. 
Als de build al bestaat in de buffer, doet de methode niets en retourneert gewoon de huidige buffer.

De GetBufferedComponent methode haalt een build op uit de buffer op basis van de build naam, 
en maakt vervolgens de buffer leeg. Als de build niet bestaat in de buffer, retourneert de methode null.

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
