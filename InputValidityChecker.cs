using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver.sudoku_solver
{
    public class InputValidityChecker
    {
        public void CheckValidity(String input)
        {
            if (input == null || input.Length != SudokuConstants.ROWS * SudokuConstants.COLS)
            {
                Environment.Exit(1); // will throw an exception
            }

            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                {
                    Environment.Exit(1); // will throw an exception
                }
            }

        }
    }
}
