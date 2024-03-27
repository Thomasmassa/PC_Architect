using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Architect.Services
{
    internal interface IBindable
    {
        string Name { get; }
        string ImageSource { get; }
    }
}
