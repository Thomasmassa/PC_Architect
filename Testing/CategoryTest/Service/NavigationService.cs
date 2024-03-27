using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryTest.Service
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<string, Type> _pages = new Dictionary<string, Type>
        { 
            { "MainPage", typeof(MainPage) }
        };

        public async Task NavigateAsync(string pageName)
        {
            if (_pages.TryGetValue(pageName, out var type))
            {
                Page page = (Page)Activator.CreateInstance(type);
                await Application.Current.MainPage.Navigation.PushAsync(page);
            }
        }
    }
}
