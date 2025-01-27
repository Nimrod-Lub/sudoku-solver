using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;


namespace Sudoku_Solver
{
    public class SudokuBacktrackingSolver
    {

        //public static bool SolveSudokuBacktrackingMinPossibilites(Cell[,] board)
        //{
        //    Tuple<int, int> indexes = FindMinPossibilityCell(board);
        //    int row = indexes.Item1;
        //    int col = indexes.Item2;

        //    if (row == -1) // all cells have a value
        //        return true;

        //    Cell currentCell = board[row, col];

        //    foreach (byte num in currentCell.GetPossibilities())
        //    {
        //        currentCell.SetValue(num);
        //        RemovePossibilities(board, row, col);
        //        bool result = SolveSudokuBacktrackingMinPossibilites(board);
        //        if (result) return true;
        //        currentCell.SetValue(0);
        //        UpdatePossibilities(board, row, col);

        //    }


        //}


        public static bool SolveSudokuBacktracking(int[,] board, int row, int col)
        {
            int boardLength = SudokuConstants.boardLength;
            if (row == boardLength - 1 && col == boardLength) // every tile has been checked
                return true;

            if (col == boardLength)
            {
                row++;
                col = 0;
            }

            if (board[row, col] != 0)
            {
                return SolveSudokuBacktracking(board, row, col + 1);
            }

            for (int num = 1; num <= boardLength; num++)
            {
                if (IsValid(board, row, col, num))
                {
                    board[row, col] = num;

                    bool solvedWithCurrNum = SolveSudokuBacktracking(board, row, col + 1);
                    if (solvedWithCurrNum == true)
                    {
                        return true;
                    }
                    board[row, col] = 0;
                }
            }
            return false;
        }

        public static bool Solve(int[,] board, int row, int col)
        {
            if (!IsSolvable(board))
                return false;

            return SolveSudokuBacktracking(board, row, col);
        }

        private static bool IsValid(int[,] board, int row, int col, int num)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                if (board[i, col] == num || board[row, i] == num) // checks row and columns
                    return false;
            }

            int blockLength = (int)Math.Sqrt(board.GetLength(0));
            int blockStartRow = row - row % blockLength;
            int blockStartCol = col - col % blockLength;
            for (int i = 0; i < blockLength; i++)
            {
                for (int j = 0; j < blockLength; j++)
                {
                    if (board[i + blockStartRow, j + blockStartCol] == num)
                        return false;
                }
            }

            return true;
        }

        public static bool IsSolvable(int[,] sudokuBoard)
        {

            if (HasDuplicatesInRow(sudokuBoard)
                || HasDuplicatesInColumn(sudokuBoard)
                || HasDuplicatesInBlock(sudokuBoard))
                return false;
            return true;
        }

        public static bool HasDuplicatesInRow(int[,] sudokuBoard)
        {
            int boardLength = SudokuConstants.boardLength;
            int[] counterArr = new int[boardLength];

            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    int currNum = sudokuBoard[i, j];
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

        public static bool HasDuplicatesInColumn(int[,] sudokuBoard)
        {
            int boardLength = SudokuConstants.boardLength;
            int[] counterArr = new int[boardLength];

            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    int currNum = sudokuBoard[j, i];
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

        public static bool HasDuplicatesInBlock(int[,] sudokuBoard)
        {
            int boardLength = SudokuConstants.boardLength;
            int blockLength = SudokuConstants.blockLength;
            int[] counterArr = new int[boardLength];

            // Assuming block length = num of blocks in each row/column
            for (int blockRow = 0; blockRow < blockLength; blockRow++)
            {
                for (int blockCol = 0; blockCol < blockLength; blockCol++)
                {
                    for (int i = 0;i < blockLength; i++)
                    {
                        for (int j = 0; j < blockLength ; j++)
                        {
                            int currRow = blockRow * blockLength + i;
                            int currCol = blockCol * blockLength + j;
                            int currNum = sudokuBoard[currRow, currCol];
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
    }
}