using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver.sudoku_solver
{
    public class CLIHandler : IInputHandler
    {
        public string GetInput()
        {
            return Console.ReadLine();
        }
    }
}