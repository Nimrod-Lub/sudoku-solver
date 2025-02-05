using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using src;
using src.Exceptions;

namespace src
{
    public static class SudokuSolverUtils
    {

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
            //SudokuConstants.obviousTuplesTime += stopwatch.Elapsed.TotalSeconds;
            return changed;
        }

        public static bool FindObviousTuplesGroup(Cell[,] board, List<(int, int)> group)
        {
            bool changed = false;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int combinationSize = 2
                ; combinationSize <= Math.Min(group.Count, SudokuConstants.MAXIMUM_GROUP_SIZE)
                ; combinationSize++)
            {
                // SudokuConstants.inObviousTuple++;
                // Get all combinations with size combinationSize in the group

                List<List<int>> combinationsList = new();

                if (SudokuConstants.storedCombinations.ContainsKey((group.Count, combinationSize)))
                {
                    combinationsList = SudokuConstants.storedCombinations[(group.Count, combinationSize)];
                }
                else
                {
                    NChooseK(combinationsList, group.Count, combinationSize, 0);
                    SudokuConstants.storedCombinations.Add((group.Count, combinationSize), combinationsList);
                }

                foreach (List<int> combination in combinationsList)
                {
                    List<(int, int)> combinationCells = new();

                    //SudokuConstants.inObviousTuple++;
                    foreach (int indexInGroup in combination)
                    {
                        combinationCells.Add(group.ElementAt(indexInGroup));
                    }

                    changed = changed || FindNakedTuples(board, group, combinationCells);
                }
            }

            stopwatch.Stop();
            SudokuConstants.nakedTuplesTime += stopwatch.Elapsed.TotalSeconds;

            stopwatch.Restart();

            for (int numExcludeFromGroup = 1
               ; numExcludeFromGroup <= Math.Min(group.Count / 2, SudokuConstants.MAXIMUM_GROUP_SIZE)
               ; numExcludeFromGroup++)
            {
                List<List<int>> combinationsList = new();

                if (SudokuConstants.storedCombinations.ContainsKey((group.Count, numExcludeFromGroup)))
                {
                    combinationsList = SudokuConstants.storedCombinations[(group.Count, numExcludeFromGroup)];
                }
                else
                {
                    NChooseK(combinationsList, group.Count, numExcludeFromGroup, 0);
                    SudokuConstants.storedCombinations.Add((group.Count, numExcludeFromGroup), combinationsList);
                }

                foreach (List<int> excludedNumsCombination in combinationsList)
                {
                    List<(int, int)> combinationCells = new List<(int, int)>(group);

                    //SudokuConstants.inObviousTuple++;

                    // Remove excluded numbers from the group

                   
                    foreach (int excludedNumIndex in excludedNumsCombination)
                    {
                        combinationCells.Remove(group.ElementAt(excludedNumIndex));

                    }
                    changed = changed || FindNakedTuples(board, group, combinationCells);
                }
            }

            stopwatch.Stop();
            SudokuConstants.hiddenTuplesTime += stopwatch.Elapsed.TotalSeconds;
            // return false INSTANTLY improves?
            //return changed;
            return false;
        }


        // find all subsets of size "size" in the group of size n (group.Count())
        // with each recursive call, we choose either to include the current index in the combination or not,
        // and adjust the size accordingly in the recursive call
        public static void NChooseK(List<List<int>> combinations, int groupSize, int size, int index = 0)
        {
            if (size == 0)
            {
                combinations.Add(new List<int>());
                return;
            }

            if (index >= groupSize)
            {
                return;
            }
            List<List<int>> includeCurrInComb = new();
            List<List<int>> excludeCurrInComb = new();

            NChooseK(includeCurrInComb, groupSize, size - 1, index + 1);
            NChooseK(excludeCurrInComb, groupSize, size, index + 1);

            // Combinations in includeCurrInComb contain size - 1 numbers, need to add group[index]
            // Add current number to every combination in includeCurrInComb and add to combinations list 
            foreach (var curr in includeCurrInComb)
            {
                curr.Add(index);
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

        public static bool FindNakedTuples(Cell[,] board
            , List<(int, int)> group, List<(int, int)> combination)
        {
            Stopwatch stopwatch = new Stopwatch();

            bool changed = false;
            HashSet<byte> candidatesInGroup = new HashSet<byte>();

            stopwatch.Restart();
            
            foreach ((int row, int col) in combination)
            {
                candidatesInGroup.UnionWith(board[row, col].GetPossibilities());
            }
            
            stopwatch.Stop();
            SudokuConstants.obviousTuplesTime += stopwatch.Elapsed.TotalSeconds;


            if (candidatesInGroup.Count < combination.Count)
                throw new UnsolvableBoardException();

            // if naked tuple
            if (candidatesInGroup.Count <= combination.Count)
            {
                //SudokuConstants.inObviousTuple++;

                

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

            }
            return changed;
        }
    }
}


//public static void NChooseK(List<List<(int, int)>> combinations, List<(int, int)> group, int size, int index = 0)
//{
//    if (size == 0)
//    {
//        combinations.Add(new List<(int, int)>());
//        return;
//    }

//    if (index >= group.Count)
//    {
//        //combinations.Add(new List<(int, int)>());
//        return;
//    }
//    List<List<(int, int)>> includeCurrInComb = new();
//    List<List<(int, int)>> excludeCurrInComb = new();

//    NChooseK(includeCurrInComb, group, size - 1, index + 1);
//    NChooseK(excludeCurrInComb, group, size, index + 1);

//    // Combinations in includeCurrInComb contain size - 1 numbers, need to add group[index]
//    // Add current number to every combination in includeCurrInComb and add to combinations list 
//    foreach (var curr in includeCurrInComb)
//    {
//        curr.Add(group.ElementAt(index));
//        combinations.Add(curr);
//    }

//    foreach (var curr in excludeCurrInComb)
//    {
//        combinations.Add(curr);
//    }
//}


//public static bool FindObviousTuplesGroup(Cell[,] board, List<(int, int)> group)
//{
//    bool changed = false;

//    for (int combinationSize = 2; combinationSize <= MAXIMUM_GROUP_LENGTH; combinationSize++)
//    {
//        // SudokuConstants.inObviousTuple++;
//        // get all combinations with size combinationSize in the group, starting from index 0

//        Stopwatch stopwatch = new();
//        stopwatch.Start();

//        //List<List<(int, int)>> combinationsList = NChooseK(group, combinationSize, 0);
//        List<List<(int, int)>> combinationsList = new();
//        NChooseK(combinationsList, group, combinationSize, 0);

//        stopwatch.Stop();
//        SudokuConstants.chooseTime += stopwatch.Elapsed.TotalSeconds;

//        foreach (List<(int, int)> combination in combinationsList)
//        {
//            //SudokuConstants.inObviousTuple++;
//            HashSet<byte> candidatesInGroup = new HashSet<byte>();
//            foreach ((int row, int col) in combination)
//            {
//                candidatesInGroup.UnionWith(board[row, col].GetPossibilities());
//            }

//            // if naked tuple
//            if (candidatesInGroup.Count <= combinationSize)
//            {
//                //SudokuConstants.inObviousTuple++;

//                Stopwatch stop2 = new Stopwatch();
//                stop2.Start();

//                foreach ((int row, int col) in group)
//                {
//                    // for every cell not in the current combination, remove naked tuples from candidates
//                    if (!combination.Contains((row, col)))
//                    {
//                        HashSet<byte> currPossibilities = board[row, col].GetPossibilities();
//                        HashSet<byte> possibilitiesBefore = new HashSet<byte>(currPossibilities);
//                        currPossibilities.ExceptWith(candidatesInGroup);

//                        // if removal changed something, changed is true
//                        changed = changed || !currPossibilities.Equals(possibilitiesBefore);
//                        SudokuConstants.inObviousTuple += changed ? 1 : 0;
//                    }
//                }
//                stop2.Stop();
//                SudokuConstants.nakedTuplesTime += stopwatch.Elapsed.TotalSeconds;
//            }
//            // not naked tuple, might be hidden tuple
//            else
//            {
//                Stopwatch stop3 = new Stopwatch();
//                stop3.Start();

//                HashSet<byte> candidatesOutGroup = new HashSet<byte>();
//                foreach ((int row, int col) in group)
//                {
//                    // for every cell not in the current combination, add to a list of possibilities of 
//                    // numbers not in the combination
//                    if (!combination.Contains((row, col)))
//                    {
//                        candidatesOutGroup.UnionWith(board[row, col].GetPossibilities());
//                    }
//                }
//                candidatesInGroup.ExceptWith(candidatesOutGroup);

//                if (candidatesInGroup.Count > combinationSize)
//                    throw new UnsolvableBoardException();

//                if (candidatesInGroup.Count != 0 && candidatesInGroup.Count == combinationSize) //IS THIS GOOD
//                {
//                    foreach ((int row, int col) in combination)
//                    {
//                        // for every cell not in the current combination, add to a list of possibilities of 
//                        // numbers not in the combination
//                        board[row, col].GetPossibilities().IntersectWith(candidatesInGroup);

//                    }
//                }
//                stop3.Stop();
//                SudokuConstants.hiddenTuplesTime += stop3.Elapsed.TotalSeconds;
//            }
//        }
//    }
//    return changed;
//}






//public static bool FindNakedTuples(Cell[,] board, List<(int, int)> group)
//{
//    bool changed = false;

//    for (int combinationSize = 2; combinationSize <= Math.Min(group.Count, SudokuConstants.MAXIMUM_GROUP_SIZE); combinationSize++)
//    {
//        // SudokuConstants.inObviousTuple++;
//        // Get all combinations with size combinationSize in the group

//        Stopwatch stopwatch = new();
//        stopwatch.Start();

//        List<List<int>> combinationsList = new();

//        if (SudokuConstants.storedCombinations.ContainsKey((group.Count, combinationSize)))
//        {
//            combinationsList = SudokuConstants.storedCombinations[(group.Count, combinationSize)];
//        }
//        else
//        {
//            NChooseK(combinationsList, group.Count, combinationSize, 0);
//            SudokuConstants.storedCombinations.Add((group.Count, combinationSize), combinationsList);
//        }



//        stopwatch.Stop();
//        SudokuConstants.chooseTime += stopwatch.Elapsed.TotalSeconds;

//        foreach (List<int> combination in combinationsList)
//        {
//            List<(int, int)> combinationCells = new();

//            //SudokuConstants.inObviousTuple++;
//            foreach (int indexInGroup in combination)
//            {
//                combinationCells.Add(group.ElementAt(indexInGroup));
//            }

//            HashSet<byte> candidatesInGroup = new HashSet<byte>();
//            foreach ((int row, int col) in combinationCells)
//            {
//                candidatesInGroup.UnionWith(board[row, col].GetPossibilities());
//            }

//            // if naked tuple
//            if (candidatesInGroup.Count <= combinationSize)
//            {
//                //SudokuConstants.inObviousTuple++;

//                Stopwatch stop2 = new Stopwatch();
//                stop2.Start();

//                foreach ((int row, int col) in group)
//                {
//                    // for every cell not in the current combination, remove naked tuples from candidates
//                    if (!combinationCells.Contains((row, col)))
//                    {
//                        HashSet<byte> currPossibilities = board[row, col].GetPossibilities();
//                        HashSet<byte> possibilitiesBefore = new HashSet<byte>(currPossibilities);
//                        currPossibilities.ExceptWith(candidatesInGroup);

//                        // if removal changed something, changed is true
//                        changed = changed || !currPossibilities.Equals(possibilitiesBefore);

//                        SudokuConstants.inObviousTuple += changed ? 1 : 0;
//                    }
//                }
//                stop2.Stop();
//                SudokuConstants.nakedTuplesTime += stopwatch.Elapsed.TotalSeconds;
//            }

//        }
//    }
//    return changed;
//}