using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Sudoku_Solver
{
    public static class BoardBuilder
    {
        public static int[,] BuildBoard(string input)
        {
            int boardLength = SudokuConstants.boardLength;
            int[,] sudokuBoard = new int[boardLength, boardLength];

            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    sudokuBoard[i, j] = input.ElementAt(i * boardLength + j) - '0';
                }
            }

            return sudokuBoard;
            //if (IsSolvable(sudokuBoard))
              //  return sudokuBoard;
            //return null;
        }


        //public Board(string input) 
        //{
        //    int boardLength = (int)Math.Sqrt(input.Length); // Sqrt is an integer if passed validity check
        //    board = new int[boardLength,boardLength];

        //    for (int i = 0; i < boardLength; i++)
        //    {
        //        for (int j = 0; j < boardLength; j++)
        //        {
        //            board[i, j] = input.ElementAt(i * boardLength + j);
        //        }
        //    }
        //} 


    }

}