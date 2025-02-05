using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src
{
    public class SudokuConstants
    {
        public static int blockLength;
        public static int boardLength;
        public static readonly int MAXIMUM_GROUP_SIZE = 2;
        public static readonly string EXIT_STRING = "exit";
        public static Dictionary<(int, int),List<List<int>>> storedCombinations = new();
        
        public static double boardCopyTime = 0;
        public static double obviousTuplesTime = 0;
        public static double nakedTuplesTime = 0;
        public static double hiddenTuplesTime = 0;
        public static int inObviousTuple = 0;

    }
}
