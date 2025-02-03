using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src
{
    public class SudokuHeuristicsSolver
    {
        public static Cell[,] Solve(Cell[,] board)
        {
            if (!Board.IsSolvable(board))
                return null;

            return SolveSudokuHeuristics(board);
        }

        private static Cell[,] SolveSudokuHeuristics(Cell[,] board)
        {
            bool updated = true;
            try
            {
                while (updated)
                {
                    updated = SudokuSolverUtils.FindNakedSingles(board) && SudokuSolverUtils.FindObviousTuples(board);
                    //updated = SudokuSolverUtils.FindObviousTuples(board);
                }
            }
            catch (UnsolvableBoardException ube)
            {
                return null;
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
                
                Stopwatch stopwatch = new();
                stopwatch.Start();
                Cell[,] boardCopy = Board.CopyBoard(board);
                stopwatch.Stop();
                SudokuConstants.boardCopyTime += stopwatch.Elapsed.TotalSeconds;
                SudokuSolverUtils.RemovePossibilities(boardCopy, b, row, col);
                Cell[,] result = SolveSudokuHeuristics(boardCopy);

                if (result != null)
                    return result;

                minPosibilitiesCell.RemovePossibility(b);
            }

            return null;
        }



    }
}
