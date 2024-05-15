using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
