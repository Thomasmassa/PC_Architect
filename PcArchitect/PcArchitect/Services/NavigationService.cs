/*

De NavigationService klasse wordt gebruikt om de huidige en vorige pagina bij te houden.

Deze service gaat nodig zijn om specifieke delen van de UI te tonen aan de gebruiker, afhankelijk van de huidige en vorige pagina.

*/

namespace PcArchitect.Services
{
    public class NavigationService
    {
        private string? currentPage, previousPage;

        public void CurrentPage(string page)
        {
            previousPage = currentPage;
            currentPage = page;
        }

        public string PreviousPage()
        {
            if (previousPage == null)
                return "MainPage";
            else
                return previousPage;
        }
    }
}
