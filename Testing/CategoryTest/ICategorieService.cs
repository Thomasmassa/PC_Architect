using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CategoryTest.Model;

namespace CategoryTest
{
    public interface ICategorieService
    {
        Task<List<Category>?> GetAll();
    }
}
