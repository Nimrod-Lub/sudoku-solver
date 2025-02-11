using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Constants;

namespace src.IO
{
    public class CLIHandler : IInputHandler, IOutputHandler // Input/output boards to console
    {
        public string GetInput() // Get a sudoku board from console
        {
            Console.WriteLine($"\nEnter sudoku board, or {IOConstants.EXIT_STRING} to exit:");
            string input = Console.ReadLine();
            if (input != null && input.Equals($"{IOConstants.EXIT_STRING}"))
            {
                input = null;
            }
            return input;
        }

        public void Output(string str) // Write a sudoku board to console
        {
            Console.WriteLine(str);
        }
    }
}