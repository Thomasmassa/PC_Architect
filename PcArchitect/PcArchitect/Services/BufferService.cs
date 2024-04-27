using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcArchitect.Services
{
    public class BufferService
    {
        private Dictionary<string, object> buffer = [];

        public Dictionary<string, object> BuffComponentForDetailPage(string componentName, object component)
        {
            if (!buffer.ContainsKey(componentName))
            {
                buffer.Add(componentName, component);
            }
            return buffer;
        }

        public object GetBufferedComponent(string componentName)
        {
            object component = null;
            if (buffer.ContainsKey(componentName))
            {
                component = buffer[componentName];
            }
            return component;
        }
    }
}
