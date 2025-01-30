using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src
{
    public interface IOutputHandler
    {
        public abstract void Output(string str);

        public abstract void OutputBoard(Cell[,] board);
    }
}
