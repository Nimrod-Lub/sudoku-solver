using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
    public class SudokuHeuristicsSolver
    {
        public static Board Solve(Board board)
        {
            Cell[,] sudokuBoard = board.GetBoard();
            if (!SudokuSolverUtils.IsSolvable(sudokuBoard))
                return null;

            return SolveSudokuHeuristics(sudokuBoard);
        }
       
        private static Cell[,] SolveSudokuHeuristics(Cell[,] board)
        {
            bool updated = true;
            while (updated)
            {
                updated = SudokuSolverUtils.FindHiddenSingles(board);
            }


            Tuple<int, int> indexes = SudokuSolverUtils.FindMinPossibilityCell(board);

            int row = indexes.Item1;
            int col = indexes.Item2;
            Cell minPosibilitiesCell = board[row, col];

            if (row == -1) // all cells have a value
                return board;


            foreach (byte b in minPosibilitiesCell.GetPossibilities())
            {
                minPosibilitiesCell.SetValue(b);
                Cell[,] result = SolveSudokuHeuristics(SudokuSolverUtils.CopyBoard(board));

                if (result != null)
                    return result;

                minPosibilitiesCell.RemovePossibility(b);
            }

            return null;
        }

       

    }
}
