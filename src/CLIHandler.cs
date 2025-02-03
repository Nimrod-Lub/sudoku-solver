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
            return Console.ReadLine();
        }

        public void Output(string str)
        {
            Console.WriteLine(str);
        }

        public void OutputBoard(Cell[,] board)
        {
            int boardLength = SudokuConstants.boardLength;
            int blockLength = SudokuConstants.blockLength;

            for (int i = 0; i < boardLength; i++) // Iterates over rows
            {
                // Prints the upper edge of the board
                // Every number has a space, every block seperator except the first one has a space, and the first seperator
                // Therefore, the board length will be 2 * boardLength + 2 * blockNum (or blockLength) + 1
                if (i % blockLength == 0)
                    Console.WriteLine(new string('-', boardLength * 2 + blockLength * 2 + 1));
                string row = "| ";
                for (int j = 0; j < boardLength; j++) // Iterates over one row
                {
                    // converts from int to ascii value and displays as char e.g. converts 1 to 49/'1'
                    row += (char)(board[i, j].GetValue() + '0') + " ";
                    if ((j + 1) % blockLength == 0)
                        row += "| ";
                }
                Console.WriteLine(row);
            }
            // Prints the bottom edge of the board
            Console.WriteLine(new string('-', boardLength * 2 + blockLength * 2 + 1));
        }
    }
}