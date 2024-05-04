// ROOTFACTORY KLASSE BEVAT TWEE OBJECTEN VAN DE ROOT KLASSE
// OM ERVOOR TE ZORGEN DAT ER MAAR 1 OBJECT VAN DE ROOT KLASSE WORDT AANGEMAAKT (SINGLETON)
// DIT VOOR GEHEUGENGEBRUIK TE VERMINDEREN EN TOEGANG TOT DEZELFDE DATA TE GARANDEREN

namespace PcArchitect.Model
{
    public class RootFactory
    {
        private Root? root1;
        private Root? root2;

        public Root GetRoot1()
        {
            root1 ??= new Root(); //zelfde als een if staterment if(root1 == null) root1 = new Root();
            return root1;
        }

        public Root GetRoot2()
        {
            root2 ??= new Root(); //zelfde als een if staterment if(root2 == null) root2 = new Root();
            return root2;
        }
    }
}
