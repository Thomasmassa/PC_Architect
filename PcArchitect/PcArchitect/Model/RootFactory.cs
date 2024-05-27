/*

De RootFactory klasse bevat twee objecten van de Root klasse.
Het doel van deze klasse is om ervoor te zorgen dat we meerdere Root objecten kunnen aanmaken en gebruiken in de applicatie.
De RootFactory zal de waarden van iedere Root object bijhouden en ervoor zorgen dat er geen dubbele objecten worden aangemaakt.

De GetRoot1 en GetRoot2 methoden controleren of de root1 en root2 objecten al zijn aangemaakt.
Als dat niet het geval is, wordt er een nieuw object aangemaakt en geretourneerd.
Als dat wel het geval is, wordt het bestaande object geretourneerd.

GetRoot1 gaat alle componenten gaan ophalen en in de root1 object steken.
GetRoot2 gaat specifieke componenten gaan ophalen en in de root2 object steken.

*/

namespace PcArchitect.Model
{
    public class RootFactory
    {
        private Root? root1;
        private Root? root2;

        public Root GetRoot1()
        {
            root1 ??= new(); //zelfde als een if staterment if(root1 == null) root1 = new Root();
            return root1;
        }

        public Root GetRoot2()
        {
            root2 ??= new(); //zelfde als een if staterment if(root2 == null) root2 = new Root();
            return root2;
        }
    }
}
