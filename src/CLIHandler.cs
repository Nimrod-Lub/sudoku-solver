using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src
{
    public class CLIHandler : IInputHandler, IOutputHandler
    {
        public string GetInput()
        {
            Console.WriteLine($"\nEnter sudoku board, or {SudokuConstants.EXIT_STRING} to exit:");
            string input = Console.ReadLine();
            if (input != null && input.Equals($"{SudokuConstants.EXIT_STRING}"))
            {
                input = null; 
            }
            return input;
        }

        public void Output(string str)
        {
            Console.WriteLine(str);
        }
    }
}