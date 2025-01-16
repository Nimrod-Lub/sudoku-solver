using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
    public interface IOutputHandler
    {
        public abstract void Output(string str);

        public abstract void OutputBoard(int[,] board);
    }
}
