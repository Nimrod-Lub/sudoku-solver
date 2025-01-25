using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
    public class CLIHandler : IInputHandler, IOutputHandler
    {
        public string GetInput()
        {
            return Console.ReadLine();
        }

        public void Output(string str)
        {
            Console.WriteLine(str);
        }
       
        public void OutputBoard(int[,] board)
        {
            if (board == null) //TODO test if this is needed
            {
                Console.WriteLine("No solution found");
                return;
            }

            for (int i = 0; i < board.GetLength(0); i++) // Iterates over rows
            {
                // Prints the upper edge of the board
                // Every number has a space, every block seperator except the first one has a space, and the first seperator
                // Therefore, the board length will be 2 * boardLength + 2 * blockNum (or blockLength) + 1
                if (i % SudokuConstants.blockLength == 0)
                    Console.WriteLine(new string('-', board.GetLength(0) * 2 + SudokuConstants.blockLength * 2 + 1));
                string row = "| ";
                for (int j = 0; j < board.GetLength(0); j++) // Iterates over one row
                {
                    // converts from int to ascii value and displays as char e.g. converts 1 to 49/'1'
                    row += ((char)(board[i, j] + '0')) + " ";
                    if ((j + 1) % SudokuConstants.blockLength == 0)
                    row += "| ";
                }
                Console.WriteLine(row);
            }
            // Prints the bottom edge of the board
            Console.WriteLine(new string('-', board.GetLength(0) * 2 + SudokuConstants.blockLength * 2 + 1));
        }
    }
}