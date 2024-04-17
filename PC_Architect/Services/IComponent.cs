using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Architect.Services
{
    public interface IComponent
    {
        string Name { get; }
        string Image { get; }
        double? Price { get; }
    }
}
