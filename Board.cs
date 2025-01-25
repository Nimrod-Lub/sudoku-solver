using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
    public class Board
    {
        private int[,] board;
        private bool[,] rows, cols, blocks;

        public Board(string input) 
        {
            int boardLength = SudokuConstants.boardLength;

            board = new int[boardLength, boardLength];
            rows = new bool[boardLength, boardLength];
            cols = new bool[boardLength, boardLength];
            blocks = new bool[boardLength, boardLength];

        }
    }
}
