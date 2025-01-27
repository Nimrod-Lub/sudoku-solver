using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
    public static class SudokuSolverUtils
    {
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

        public static bool FindNakedSingles(Cell[,] board)
        {
            bool updated = false;
            int boardLength = SudokuConstants.boardLength;
            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    Cell currCell = board[i, j];
                    HashSet<byte> possibilities = currCell.GetPossibilities();
                    if (possibilities.Count == 1)
                    {
                        byte onlyNum = possibilities.ElementAt(0);
                        currCell.SetValue(onlyNum);
                        RemovePossibilities(board, onlyNum, i, j);
                        updated = true;
                    }
                }
            }
            return updated;
        }

        public static void RemovePossibilities(Cell[,] board, byte num, int row, int col)
        {
            for (int i = 0; i < SudokuConstants.boardLength; i++)
            {
                board[i, col].RemovePossibility(num);
                board[row, i].RemovePossibility(num);
            }

            int blockLength = SudokuConstants.blockLength;
            int blockStartRow = row - row % blockLength;
            int blockStartCol = col - col % blockLength;

            for (int i = 0; i < blockLength; i++)
            {
                for (int j = 0; j < blockLength; j++)
                {
                    board[i + blockStartRow, j + blockStartCol].RemovePossibility(num);
                }
            }
        }

        public static bool FindObviousTuples(Cell[,] board)
        {
            int boardLength = SudokuConstants.boardLength;
            int blockLength = SudokuConstants.blockLength;
            List<(int, int)> currentCheckedGroupRows = new List<(int, int)>();
            List<(int, int)> currentCheckedGroupCols = new List<(int, int)>();
            List<(int, int)> currentCheckedGroupBlocks = new List<(int, int)>();
            bool changed = false;

            for (int row = 0; row < boardLength; row++) // rows and cols
            {
                currentCheckedGroupRows.Clear();
                currentCheckedGroupCols.Clear();
                for (int col = 0; col < boardLength; col++)
                {
                    Cell currCellRow = board[row, col];
                    if (currCellRow.GetValue() == 0)
                    {
                        currentCheckedGroupRows.Add((row, col));
                    }

                    Cell currCellCol = board[col, row];
                    {
                        currentCheckedGroupCols.Add((col, row));
                    }
                }
                changed = changed
                    || FindObviousTuplesGroup(board, currentCheckedGroupRows)
                    || FindObviousTuplesGroup(board, currentCheckedGroupCols);
            }

            for (int blockRow = 0; blockRow < blockLength; blockRow++)
            {
                for (int blockCol = 0; blockCol < blockLength; blockCol++)
                {
                    currentCheckedGroupBlocks.Clear();

                    for (int i = 0; i < blockLength; i++) // row within the current block
                    {
                        for (int j = 0; j < blockLength; j++) // column within the current block
                        {
                            int currRow = blockRow * blockLength + i;
                            int currCol = blockCol * blockLength + j;

                            Cell currCellBlock = board[currRow, currCol];
                            if (currCellBlock.GetValue() == 0)
                            {
                                currentCheckedGroupBlocks.Add((currRow, currCol));
                            }

                        }
                    }

                    changed = changed
                        || FindObviousTuplesGroup(board, currentCheckedGroupBlocks);
                }
            }
            return changed;
        }

        public static bool FindObviousTuplesGroup(Cell[,] board, List<(int, int)> group)
        {
            bool changed = false;
            //int groupSize = (int) Math.Floor(Math.Sqrt(group.Count));
            int groupSize = group.Count;
            for (int combinationSize = 2; combinationSize < groupSize; combinationSize++)
            {
                // get all combinations with size combinationSize in the group, starting from index 0
                List<List<(int, int)>> combinationsList = NChooseK(group, combinationSize, 0);
                foreach (List<(int, int)> combination in combinationsList)
                {
                    HashSet<byte> candidatesInGroup = new HashSet<byte>();
                    foreach ((int row, int col) in combination)
                    {
                        candidatesInGroup.UnionWith(board[row, col].GetPossibilities());
                    }

                    // if naked tuple
                    if (candidatesInGroup.Count <= combinationSize)
                    {
                        foreach ((int row, int col) in group)
                        {
                            // for every cell not in the current combination, remove naked tuples from candidates
                            if (!combination.Contains((row, col)))
                            {
                                HashSet<byte> currPossibilities = board[row, col].GetPossibilities();
                                HashSet<byte> possibilitiesBefore = new HashSet<byte>(currPossibilities);
                                currPossibilities.ExceptWith(candidatesInGroup);

                                // if removal changed something, changed is true
                                changed = changed || !currPossibilities.Equals(possibilitiesBefore);
                            }
                        }
                    }
                }

            }
            return changed;
        }

        // find all subsets of size "size" in the group of size n (group.Count())
        // with each recursive call, we choose either to include the current index in the combination or not,
        // and adjust the size accordingly in the recursive call
        public static List<List<(int, int)>> NChooseK(List<(int, int)> group, int size, int index)
        {
            if (size == 0)
                return new List<List<(int, int)>>();

            if (index >= group.Count)
            {
                return new List<List<(int, int)>>();
            }
            List<List<(int, int)>> combinations = new();
            List<List<(int, int)>> includeCurrInComb;
            List<List<(int, int)>> excludeCurrInComb;

            includeCurrInComb = NChooseK(group, size - 1, index + 1);
            excludeCurrInComb = NChooseK(group, size, index + 1);

            // Combinations in includeCurrInComb contain size - 1 numbers, need to add group[index]
            // Add current number to every combination in includeCurrInComb and add to combinations list 
            foreach (var curr in includeCurrInComb)
            {
                curr.Add(group.ElementAt(index));
                combinations.Add(curr);
            }

            foreach (var curr in excludeCurrInComb)
            {
                combinations.Add(curr);
            }

            return combinations;
        }

        public static (int, int) FindMinPossibilityCell(Cell[,] board)
        {
            int boardLength = SudokuConstants.boardLength;

            (int, int) minPossibilityIndex = (-1, -1);
            int minPossibilityCnt = boardLength + 1;

            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    Cell currCell = board[i, j];
                    int currPossibilityCnt = currCell.GetPossibilities().Count;
                    if (currCell.GetValue() == 0 && currPossibilityCnt < minPossibilityCnt)
                    {
                        minPossibilityIndex = (i, j);
                        minPossibilityCnt = currPossibilityCnt;
                    }
                }
            }
            return minPossibilityIndex;
        }
    }
}
