﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using src.Constants;
using src.Exceptions;

namespace src.utils
{
    public static class SudokuSolverUtils
    {
        // For each cell in the board with 1 possible value, sets their value to that value
        // Returns true if progress was made, else false
        public static bool FindNakedSingles(Cell[,] board)
        {
            bool updated = false;
            int boardLength = SolverConstants.boardLength;
            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    Cell currCell = board[i, j];
                    int possibilities = currCell.GetPossibilities();
                    // Checks whether current cell has 1 possible value and doesn't currently have a value
                    if (BitwiseUtils.GetPossibilitiesCount(possibilities) == 1 && currCell.GetValue() == 0)
                    {
                        byte onlyNum = BitwiseUtils.GetLastNum(possibilities); // Gets the value
                        currCell.SetValue(onlyNum);
                        Board.RemovePossibilities(board, onlyNum, i, j); // Remove possibilities from neighbors
                        updated = true;
                    }
                }
            }
            return updated; // Returns true if progress was made, else false
        }        

        // Attempts to find obvious tuples on the board
        // Obvious tuple - a "tuple" (group) of cells that are naked tuples or hidden tuples
        public static void FindObviousTuples(Cell[,] board)
        {
            int boardLength = SolverConstants.boardLength;
            int blockLength = SolverConstants.blockLength;
            List<(int, int)> currentCheckedGroupRows = new List<(int, int)>();
            List<(int, int)> currentCheckedGroupCols = new List<(int, int)>();
            List<(int, int)> currentCheckedGroupBlocks = new List<(int, int)>();

            // Makes a list of all cells in each row and column that don't have a set value
            for (int row = 0; row < boardLength; row++) 
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
                // If the group size is larger than the max combination size
                if (currentCheckedGroupRows.Count > SolverConstants.MAXIMUM_COMBINATION_SIZE)
                    FindObviousTuplesGroup(board, currentCheckedGroupRows); // Applies obvious tuples on each group
                // If the group size is larger than the max combination size
                if (currentCheckedGroupCols.Count > SolverConstants.MAXIMUM_COMBINATION_SIZE) 
                    FindObviousTuplesGroup(board, currentCheckedGroupCols); // Applies obvious tuples on each group
            }

            // Makes a list of all cells in each block that don't have a set value
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
                    // If the group size is larger than the max combination size
                    if (currentCheckedGroupBlocks.Count > SolverConstants.MAXIMUM_COMBINATION_SIZE) 
                        FindObviousTuplesGroup(board, currentCheckedGroupBlocks); // Applies obvious tuples on each group
                }
            }
        }

        // Attempts to find obvious tuples in a given group
        public static void FindObviousTuplesGroup(Cell[,] board, List<(int, int)> group)
        {
            // Finds naked tuples of size combinationSize
            for (int combinationSize = 2
                ; combinationSize <= Math.Min(group.Count, SolverConstants.MAXIMUM_COMBINATION_SIZE)
                ; combinationSize++)
            {
                List<List<int>> combinationsList = new();
                // Get all combinations with size combinationSize in the group

                // If combinations have been calculated before
                if (SolverConstants.storedCombinations.ContainsKey((group.Count, combinationSize))) 
                {
                    // Every possible index combination of combinationSize cells in a group of size group.Count
                    combinationsList = SolverConstants.storedCombinations[(group.Count, combinationSize)];
                }
                else
                {
                    // Adds every possible index combination of combinationSize cells in a group of size group.Count
                    NChooseK(combinationsList, group.Count, combinationSize, 0);
                    // Add to stored combinations in case the result is needed in the future
                    SolverConstants.storedCombinations.Add((group.Count, combinationSize), combinationsList);
                }

                // For each possible combination
                foreach (List<int> combination in combinationsList)
                {
                    List<(int, int)> combinationCells = new();

                    // The cells in the current combination are determined by their index within the group
                    // The cells whose index in the group is inside combination, are added to combinationCells
                    foreach (int indexInGroup in combination)
                    {
                        combinationCells.Add(group.ElementAt(indexInGroup));
                    }

                    // Apply naked tuples in current group with current combination
                    FindNakedTuples(board, group, combinationCells);
                }
            }

            // Finds hidden tuples of size numExcludeFromGroup
            // Naked tuples of the entire group except for numExcludeFromGroup cells
            // is similar to hidden tuples for numExcludeFromGroup cells
            for (int numExcludeFromGroup = 1
               ; numExcludeFromGroup <= Math.Min(group.Count / 2, SolverConstants.MAXIMUM_COMBINATION_SIZE)
               ; numExcludeFromGroup++)
            {
                List<List<int>> combinationsList = new();

                // If combinations have been calculated before
                if (SolverConstants.storedCombinations.ContainsKey((group.Count, numExcludeFromGroup)))
                {
                    // Every possible index combination of numExcludeFromGroup cells in a group of size group.Count
                    combinationsList = SolverConstants.storedCombinations[(group.Count, numExcludeFromGroup)];
                }
                else
                {
                    // Adds every possible index combination of combinationSize cells in a group of size group.Count to combinationsList
                    NChooseK(combinationsList, group.Count, numExcludeFromGroup, 0);
                    // Add to stored combinations in case the result is needed in the future
                    SolverConstants.storedCombinations.Add((group.Count, numExcludeFromGroup), combinationsList);
                }
                // For each possible combination of cells who aren't in the excludedNumsCombination
                foreach (List<int> excludedNumsCombination in combinationsList)
                {
                    List<(int, int)> combinationCells = new List<(int, int)>(group);

                    // Remove excluded numbers from the group
                    foreach (int excludedNumIndex in excludedNumsCombination)
                    {
                        combinationCells.Remove(group.ElementAt(excludedNumIndex));

                    }
                    // Apply naked tuples in current group with current combination:
                    // Similar to hidden tuples in current group with all cells not in combinationCells being the combination
                    FindNakedTuples(board, group, combinationCells);
                }
            }
        }


        // Find all subsets of size "size" in the group of size n (group.Count()).
        // With each recursive call, we choose either to include the current index in the combination or not,
        // and adjust the size accordingly in the recursive call
        public static void NChooseK(List<List<int>> combinations, int groupSize, int size, int index = 0)
        {
            if (size == 0) // Combination includes enough members
            {
                combinations.Add(new List<int>());
                return;
            }

            if (index >= groupSize) // Reached the end of the group without enough members in the combination, invalid combination
            {
                return;
            }
            List<List<int>> includeCurrInComb = new();
            List<List<int>> excludeCurrInComb = new();

            NChooseK(includeCurrInComb, groupSize, size - 1, index + 1); // Add current number to combination
            NChooseK(excludeCurrInComb, groupSize, size, index + 1); // Don't add current number to combination

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


        // Given a board, returns the index of the cell with the smallest amount of possibilities.
        // Returns (-1,-1) if all cells have a value (board is solved)
        public static (int, int) FindMinPossibilityCell(Cell[,] board)
        {
            int boardLength = SolverConstants.boardLength;

            (int, int) minPossibilityIndex = (-1, -1);
            int minPossibilityCnt = boardLength + 1;

            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    Cell currCell = board[i, j];
                    int currPossibilityCnt = currCell.GetPossibilitiesCount();
                    if (currCell.GetValue() == 0 && currPossibilityCnt < minPossibilityCnt)
                    {
                        minPossibilityIndex = (i, j);
                        minPossibilityCnt = currPossibilityCnt;
                    }
                }
            }
            return minPossibilityIndex;
        }

        // Applies naked tuples to the group group with the current combination
        public static void FindNakedTuples(Cell[,] board
            , List<(int, int)> group, List<(int, int)> combination)
        {
            int candidatesInGroup = 0;

            // Adds all possibilities of all cells in the combination to one place
            foreach ((int row, int col) in combination)
            {
                candidatesInGroup |= board[row, col].GetPossibilities();
            }

            // If there are more cells in the combination than possibilities, the board is unsolvable
            if (BitwiseUtils.GetPossibilitiesCount(candidatesInGroup) < combination.Count)
                throw new UnsolvableBoardException();


            // If the amount of cells in the combination is equal to the amount of possibilities, it's a naked tuple
            if (BitwiseUtils.GetPossibilitiesCount(candidatesInGroup) == combination.Count)
            {

                // Remove the possibilities of the combination from every cell in the group that is not in the combination
                foreach ((int row, int col) in group)
                {
                    // for every cell not in the current combination, remove possibilities of the combination from possibilities
                    if (!combination.Contains((row, col)))
                    {
                        int currPossibilities = board[row, col].GetPossibilities();

                        // Bitwise expression - Set every bit that isn't a possibility of the combination to 1
                        // and perform the "and" operation with the cell's current possibilities
                        currPossibilities = currPossibilities & ~candidatesInGroup;
                        board[row, col].SetPossibilities(currPossibilities);
                    }
                }
            }
        }
    }
}