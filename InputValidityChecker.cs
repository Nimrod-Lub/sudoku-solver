using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
    public static class InputValidityChecker
    {
        //TODO add check if sqrt of sqrt is an integer (block length)
        public static void CheckValidity(String input) 
        {
            if (input == null ||
                input.Length == 0 ||
                Math.Sqrt(input.Length) != Math.Floor(Math.Sqrt(input.Length)))
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
