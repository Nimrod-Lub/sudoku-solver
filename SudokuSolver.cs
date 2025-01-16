using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

public class SudokuSolver
{
    // General note - since the board is a square - the amount of rows and cols are the same,
    // so board.GetLength(0) and board.GetLength(1) are the same thing
    public static int[,] Solve(int[,] board, int row, int col)
    {
        if (row == board.GetLength(0) - 1 && col == board.GetLength(0)) // every tile has been checked
            return board;

        if (col == board.GetLength(0))
        {
            row++;
            col = 0;
        }

        if (board[row, col] != 0)
        {
            return Solve(board, row, col + 1);
        }

        for (int num = 1; num < board.GetLength(0); num++)
        {
            if (IsValid(board, row, col, num))
            {
                board[row, col] = num;

                int[,] temp = Solve(board, row, col);
                if (temp != null)
                {
                    return temp;
                }
            }
        }
        return null;
    }

    private static bool IsValid(int[,] board,int row, int col, int num)
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
            for (int j = 0; j < blockStartRow; j++)
            {
                if (board[i + blockStartRow, j + blockStartCol] == num)
                    return false;
            }
        }

        return true;
    }
}