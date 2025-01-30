using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src
{
    public class Board
    {
        private Cell[,] board;

        public Board(string input)
        {
            int boardLength = SudokuConstants.boardLength;
            board = new Cell[boardLength, boardLength];
            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    board[i, j] = new Cell();
                    board[i, j].SetValue((byte)(input.ElementAt(i * boardLength + j) - '0'));
                }
            }

            InitializePossibilities();

        }

        private void InitializePossibilities()
        {
            int boardLength = SudokuConstants.boardLength;
            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    byte currValue = board[i, j].GetValue();
                    if (currValue != 0)
                    {
                        RemovePossibilities(currValue, i, j);
                    }
                }
            }
        }

        private void RemovePossibilities(byte num, int row, int col)
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

        public Cell[,] GetBoard()
        {
            return board;
        }

    }
}

//int[,] board
//List(<tupple(int,int)>) [,] changes
//List<Tuple(int,int), int value > possibilitychange;

//board[0, 0] = 4;
//for all neighbors (x,y) with possibilitty 4:
//    remove pos 4 from x,y
//    add x,y to changes(i,j)






// option 1 - 3 boolean matrixes.
// to find min
// for each tile check in every matrix - 3n time *  n^2 = theta(n^3)
// to update after placing number - theta(1)
// memory - 3n^2 bits
// false - not found, true - found

// option 2 - 81 boolean arrays -- keeping valid possibilities for a specific entry
// to find min
// for each tile check array - n time * n^2 = theta(n^3)
// to update after placing number - 3n = theta(n)
// memory - n^2 * n = n^3 hashsets