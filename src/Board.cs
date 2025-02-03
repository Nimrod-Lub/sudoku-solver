using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src
{
    public static class Board
    {
        public static Cell[,] BuildBoard(string input)
        {
            int boardLength = SudokuConstants.boardLength;
            Cell[,] board = new Cell[boardLength, boardLength];
            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    board[i, j] = new Cell();
                    board[i, j].SetValue((byte)(input.ElementAt(i * boardLength + j) - '0'));
                }
            }
            InitializePossibilities(board);
            return board;
        }

        public static void InitializePossibilities(Cell[,] board)
        {
            int boardLength = SudokuConstants.boardLength;
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

        public static void RemovePossibilities(Cell[,] board, byte num, int row, int col)
        {
            int boardLength = SudokuConstants.boardLength;
            int blockLength = SudokuConstants.blockLength;
            int blockStartRow = row - row % blockLength;
            int blockStartCol = col - col % blockLength;

            for (int i = 0; i < boardLength; i++)
            {
                board[i, col].RemovePossibility(num);
                board[row, i].RemovePossibility(num);
            }

            for (int i = 0; i < blockLength; i++)
            {
                for (int j = 0; j < blockLength; j++)
                {
                    board[i + blockStartRow, j + blockStartCol].RemovePossibility(num);
                }
            }
        }

        public static bool IsSolvable(Cell[,] sudokuBoard)
        {

            if (HasDuplicatesInRow(sudokuBoard)
                || HasDuplicatesInColumn(sudokuBoard)
                || HasDuplicatesInBlock(sudokuBoard))
                return false;
            return true;
        }

        public static bool HasDuplicatesInRow(Cell[,] sudokuBoard)
        {
            int boardLength = SudokuConstants.boardLength;
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

        public static bool HasDuplicatesInColumn(Cell[,] sudokuBoard)
        {
            int boardLength = SudokuConstants.boardLength;
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

        public static bool HasDuplicatesInBlock(Cell[,] sudokuBoard)
        {
            int boardLength = SudokuConstants.boardLength;
            int blockLength = SudokuConstants.blockLength;
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

        public static Cell[,] CopyBoard(Cell[,] board)
        {
            int boardLength = SudokuConstants.boardLength;
            Cell[,] boardCopy = new Cell[boardLength, boardLength];
            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    boardCopy[i, j] = new Cell(board[i, j]);
                }
            }
            return boardCopy;
        }

        public static string BoardToString(Cell[,] board)
        {
            string result = "";
            foreach (Cell cell in board)
            {
                result += (char)(cell.GetValue() + '0');
            }
            return result;
        }
    }
}