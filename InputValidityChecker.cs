using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
    public static class InputValidityChecker
    {
        public static void CheckValidity(String input) 
        {
            if (input == null || input.Length == 0)
            {
                Environment.Exit(1); // will throw an exception
            }

            double boardLength = Math.Sqrt(input.Length);
            if ( boardLength != Math.Floor(boardLength))
            {
                Environment.Exit(1); // will throw an exception
            }

            double blockLength = Math.Sqrt(boardLength);
            if (blockLength != Math.Floor(blockLength)) 
            { 
                Environment.Exit(1); 
            }

            int max = 0;

            foreach (char c in input)
            {
                int curr = c - '0';
                if (curr < 0)
                {
                    Environment.Exit(1); // will throw an exception
                }
                if (curr > max)
                    max = curr;
            }

            if (max > boardLength)
            {
                Environment.Exit(1); // will throw an exception
            }

            SudokuConstants.boardLength = (int)boardLength;
            SudokuConstants.blockLength = (int)blockLength;
            
        }
    }
}
