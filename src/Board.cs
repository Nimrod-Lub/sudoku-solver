using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Constants;

namespace src
{
    public static class Board // Class for functions involving the board
    {
        // Builds the sudoku board fitting to the string input
        public static Cell[,] BuildBoard(string input)
        {
            int boardLength = SolverConstants.boardLength;
            Cell[,] board = new Cell[boardLength, boardLength];
            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    board[i, j] = new Cell();
                    board[i, j].SetValue((byte)(input.ElementAt(i * boardLength + j) - '0')); // turns char into byte
                }
            }
            InitializePossibilities(board);
            return board;
        }

        // Updates possibilities of each cell in the board after building the board
        public static void InitializePossibilities(Cell[,] board)
        {
            int boardLength = SolverConstants.boardLength;
            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    byte currValue = board[i, j].GetValue();
                    if (currValue != 0)
                    {
                        RemovePossibilities(board, currValue, i, j);
                    }
                }
            }
        }

        // Remove the number num from the possiblities of all cells in the same row, column, and block as the cell in [row,col]
        public static void RemovePossibilities(Cell[,] board, byte num, int row, int col)
        {
            int boardLength = SolverConstants.boardLength;
            int blockLength = SolverConstants.blockLength;
            int blockStartRow = row - row % blockLength;
            int blockStartCol = col - col % blockLength;

            for (int i = 0; i < boardLength; i++) // Remove from row and col
            {
                board[i, col].RemovePossibility(num);
                board[row, i].RemovePossibility(num);
            }

            for (int i = 0; i < blockLength; i++) // Remove from block
            {
                for (int j = 0; j < blockLength; j++)
                {
                    board[i + blockStartRow, j + blockStartCol].RemovePossibility(num);
                }
            }
        }

        // Checks if the initial board is solvable - returns true if solvable else false
        public static bool IsSolvable(Cell[,] sudokuBoard)
        {

            if (HasDuplicatesInRow(sudokuBoard)
                || HasDuplicatesInColumn(sudokuBoard)
                || HasDuplicatesInBlock(sudokuBoard))
                return false;
            return true;
        }

        // Returns true if there is a number that appears twice in one row, else false
        public static bool HasDuplicatesInRow(Cell[,] sudokuBoard)
        {
            int boardLength = SolverConstants.boardLength;
            int[] counterArr = new int[boardLength];

            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    int currNum = sudokuBoard[i, j].GetValue();
                    if (currNum != 0)
                        counterArr[currNum - 1]++;
                }

                if (HasDuplicates(counterArr))
                {
                    return true;
                }
                Array.Clear(counterArr, 0, counterArr.Length);
            }
            return false;
        }

        // Returns true if there is a number that appears twice in one column, else false
        public static bool HasDuplicatesInColumn(Cell[,] sudokuBoard)
        {
            int boardLength = SolverConstants.boardLength;
            int[] counterArr = new int[boardLength];

            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    int currNum = sudokuBoard[j, i].GetValue();
                    if (currNum != 0)
                        counterArr[currNum - 1]++;
                }

                if (HasDuplicates(counterArr))
                {
                    return true;
                }
                Array.Clear(counterArr, 0, counterArr.Length);
            }
            return false;
        }

        // Returns true if there is a number that appears twice in one block, else false
        public static bool HasDuplicatesInBlock(Cell[,] sudokuBoard)
        {
            int boardLength = SolverConstants.boardLength;
            int blockLength = SolverConstants.blockLength;
            int[] counterArr = new int[boardLength];

            // Assuming block length = num of blocks in each row/column
            for (int blockRow = 0; blockRow < blockLength; blockRow++)
            {
                for (int blockCol = 0; blockCol < blockLength; blockCol++)
                {
                    for (int i = 0; i < blockLength; i++)
                    {
                        for (int j = 0; j < blockLength; j++)
                        {
                            int currRow = blockRow * blockLength + i;
                            int currCol = blockCol * blockLength + j;
                            int currNum = sudokuBoard[currRow, currCol].GetValue();
                            if (currNum != 0)
                                counterArr[currNum - 1]++;
                        }
                    }

                    if (HasDuplicates(counterArr))
                    {
                        return true;
                    }
                    Array.Clear(counterArr, 0, counterArr.Length);

                }

            }
            return false;
        }

        // Linq that checks if there's a number who appeared more than once in the current row/column/block
        public static bool HasDuplicates(int[] counterArr) { return !counterArr.All(number => number <= 1); }

        // Returns a deep copy of the board board
        public static Cell[,] CopyBoard(Cell[,] board)
        {
            int boardLength = SolverConstants.boardLength;
            Cell[,] boardCopy = new Cell[boardLength, boardLength];
            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    boardCopy[i, j] = new Cell(board[i, j]); // Deep copy of each cell
                }
            }
            return boardCopy;
        }

        // Returns the string format of a board
        public static string BoardToString(Cell[,] board)
        {
            string result = "";
            foreach (Cell cell in board)
            {
                result += (char)(cell.GetValue() + '0'); // converts the number (byte) to its ascii value (char)
            }
            return result;
        }

        // Prints the board in board format
        public static void OutputBoard(Cell[,] board)
        {
            int boardLength = SolverConstants.boardLength;
            int blockLength = SolverConstants.blockLength;

            for (int i = 0; i < boardLength; i++) // Iterates over rows
            {
                // Prints the upper edge of the board
                // Every number has a space, every block seperator except the first one has a space, and the first seperator exists
                // Therefore, the printed board length will be 2 * boardLength + 2 * blockNum (or blockLength) + 1
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
            // Prints the bottom edge of the board/ bottom seperator of blocks in the board
            Console.WriteLine(new string('-', boardLength * 2 + blockLength * 2 + 1));
        }
    }
}