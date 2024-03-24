using PC_Architect.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Architect.Services
{
    public interface IPartsService
    {
        Task<List<CPU>?> GetCPUs();
    }
}
