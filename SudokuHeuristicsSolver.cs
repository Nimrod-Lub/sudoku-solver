using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
    public class SudokuHeuristicsSolver //TODO fix solver
    {
        public static Cell[,] Solve(Board board)
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
                updated = SudokuSolverUtils.FindNakedSingles(board) && SudokuSolverUtils.FindObviousTuples(board);
            }



            (int, int) indexes = SudokuSolverUtils.FindMinPossibilityCell(board);

            int row = indexes.Item1;
            int col = indexes.Item2;

            if (row == -1) // all cells have a value
                return board;

            Cell minPosibilitiesCell = board[row, col];
            

            foreach (byte b in minPosibilitiesCell.GetPossibilities())
            {
                minPosibilitiesCell.SetValue(b);
                SudokuSolverUtils.RemovePossibilities(board, b, row, col);
                Cell[,] result = SolveSudokuHeuristics(SudokuSolverUtils.CopyBoard(board));

                if (result != null)
                    return result;

                minPosibilitiesCell.RemovePossibility(b);
            }

            return null;
        }

       

    }
}
