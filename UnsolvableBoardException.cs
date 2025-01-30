using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Sudoku_Solver
{
    public class UnsolvableBoardException : Exception
    {
        public UnsolvableBoardException() : base("\n\nThe board you provided is not solvable")
        {
        }
    }
}
