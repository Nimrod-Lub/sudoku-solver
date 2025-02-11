using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Constants
{
    public class SolverConstants
    {
        public static int blockLength; // Length of each block in the board
        public static int boardLength; // Length of the board
        public static readonly int MAXIMUM_COMBINATION_SIZE = 2; // Maximum combination size for obvious tuples
        public static Dictionary<(int, int), List<List<int>>> storedCombinations = new(); // Stores combinations after making a combination list

        public static double boardCopyTime = 0; // Debugging
        public static double obviousTuplesTime = 0;
        public static double nakedTuplesTime = 0;
        public static double hiddenTuplesTime = 0;
        public static int inObviousTuple = 0;

    }
}
