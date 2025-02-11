using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Constants;
using src.Exceptions;
using src.utils;

namespace src
{
    public class SudokuHeuristicsSolver  // The solver algorithm - solves sudoku given a sudoku board
    {


        // Solves sudoku given a sudoku board or returns null if the board is unsolvable
        public static Cell[,] Solve(Cell[,] board)
        {
            if (!Board.IsSolvable(board)) // Checks whether the initial board is solvable
                return null;

            return SolveSudokuHeuristics(board);
        }

        // Recursive algorithm that slowly solves the board.
        // The algorithm attempts to make progress using human tactics, and when no progress is made,
        // the algorithm places a value at the cell with the least possibilities and continues recursively assuming his guess was correct.
        // If the board was unsolvable, the algorithm places the next possible value, until there are no possibilities left,
        // which results in the algorithm returning null (unsolvable board).
        private static Cell[,] SolveSudokuHeuristics(Cell[,] board)
        {
            bool updated = true;
            try
            {
                while (updated) // Attempts to make progress using human tactics
                {
                    while (updated)
                        updated = SudokuSolverUtils.FindNakedSingles(board);
                    
                    updated = SudokuSolverUtils.FindObviousTuples(board);
                }
            }
            catch (UnsolvableBoardException ube) // If board is unsolvable
            {
                return null;
            }


            (int, int) indexes = SudokuSolverUtils.FindMinPossibilityCell(board); // Cell with the least possible values

            int row = indexes.Item1;
            int col = indexes.Item2;

            if (row == -1) // All cells have a value - board is solved
                return board;

            Cell minPosibilitiesCell = board[row, col];
            int possibilities = minPosibilitiesCell.GetPossibilities();

            // For each possibility in the cell
            for (byte currBit = 1; possibilities != 0; currBit++, possibilities = possibilities >> 1) 
            {
                if ((possibilities & 1) == 0) // Skip if current bit is not a valid possibility
                    continue;

                Cell[,] boardCopy = Board.CopyBoard(board); // Make a deep copy of the board
                boardCopy[row, col].SetValue(currBit); // Set the current possibility to be the value of the cell
                
                Board.RemovePossibilities(boardCopy, currBit, row, col); // Remove possibilities from neighbors of the cell
                Cell[,] result = SolveSudokuHeuristics(boardCopy); // Attempt to solve the current board

                if (result != null) // Board was solved
                    return result;

                minPosibilitiesCell.RemovePossibility(currBit); 
            }           
            return null; // Board is unsolvable
        }
    }
}
