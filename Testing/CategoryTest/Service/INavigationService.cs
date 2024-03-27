using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryTest.Service
{
    public interface INavigationService
    {
        Task NavigateAsync(string pageName);
    }
}
