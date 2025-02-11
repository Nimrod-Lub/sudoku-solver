using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.IO
{
    public interface IOutputHandler // Interface for outputting boards
    {
        public abstract void Output(string str); // Output board str
    }
}
