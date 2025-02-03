using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Exceptions;

namespace src
{
    public static class SudokuSolverUtils
    {
        private static readonly int MAXIMUM_GROUP_LENGTH = 2;

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
                    if (possibilities.Count == 1 && currCell.GetValue() == 0)
                    {
                        //Console.WriteLine("Entered");
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
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();


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
                    if (currCellCol.GetValue() == 0)
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
            stopwatch.Stop();
            SudokuConstants.obviousTuplesTime += stopwatch.Elapsed.TotalSeconds;
            return changed;
        }

        public static bool FindObviousTuplesGroup(Cell[,] board, List<(int, int)> group)
        {
            bool changed = false;

            for (int combinationSize = 2; combinationSize <= MAXIMUM_GROUP_LENGTH; combinationSize++)
            {
                // SudokuConstants.inObviousTuple++;
                // get all combinations with size combinationSize in the group, starting from index 0

                Stopwatch stopwatch = new();
                stopwatch.Start();

                //List<List<(int, int)>> combinationsList = NChooseK(group, combinationSize, 0);
                List<List<(int, int)>> combinationsList = new();
                NChooseK(combinationsList, group, combinationSize, 0);

                stopwatch.Stop();
                SudokuConstants.chooseTime += stopwatch.Elapsed.TotalSeconds;

                foreach (List<(int, int)> combination in combinationsList)
                {
                    //SudokuConstants.inObviousTuple++;
                    HashSet<byte> candidatesInGroup = new HashSet<byte>();
                    foreach ((int row, int col) in combination)
                    {
                        candidatesInGroup.UnionWith(board[row, col].GetPossibilities());
                    }

                    // if naked tuple
                    if (candidatesInGroup.Count <= combinationSize)
                    {
                        //SudokuConstants.inObviousTuple++;
                        
                        Stopwatch stop2 = new Stopwatch();
                        stop2.Start();

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
                                SudokuConstants.inObviousTuple += changed ? 1 : 0;
                            }
                        }
                        stop2.Stop();
                        SudokuConstants.nakedTuplesTime += stopwatch.Elapsed.TotalSeconds;
                    }
                    // not naked tuple, might be hidden tuple
                    else
                    {
                        Stopwatch stop3 = new Stopwatch();
                        stop3.Start();

                        HashSet<byte> candidatesOutGroup = new HashSet<byte>();
                        foreach ((int row, int col) in group)
                        {
                            // for every cell not in the current combination, add to a list of possibilities of 
                            // numbers not in the combination
                            if (!combination.Contains((row, col)))
                            {
                                candidatesOutGroup.UnionWith(board[row, col].GetPossibilities());
                            }
                        }
                        candidatesInGroup.ExceptWith(candidatesOutGroup);

                        if (candidatesInGroup.Count > combinationSize)
                            throw new UnsolvableBoardException();

                        if (candidatesInGroup.Count != 0 && candidatesInGroup.Count == combinationSize) //IS THIS GOOD
                        {
                            foreach ((int row, int col) in combination)
                            {
                                // for every cell not in the current combination, add to a list of possibilities of 
                                // numbers not in the combination
                                board[row, col].GetPossibilities().IntersectWith(candidatesInGroup);

                            }
                        }
                        stop3.Stop();
                        SudokuConstants.hiddenTuplesTime += stop3.Elapsed.TotalSeconds;
                    }
                }
            }
            return changed;
        }

        //outdated version
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


        // find all subsets of size "size" in the group of size n (group.Count())
        // with each recursive call, we choose either to include the current index in the combination or not,
        // and adjust the size accordingly in the recursive call
        public static void NChooseK(List<List<(int, int)>> combinations, List<(int, int)> group, int size, int index = 0)
        {
            if (size == 0)
            {
                combinations.Add(new List<(int, int)>());
                return;
            }

            if (index >= group.Count)
            {
                //combinations.Add(new List<(int, int)>());
                return;
            }
            List<List<(int, int)>> includeCurrInComb = new();
            List<List<(int, int)>> excludeCurrInComb = new();

            NChooseK(includeCurrInComb, group, size - 1, index + 1);
            NChooseK(excludeCurrInComb, group, size, index + 1);

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
