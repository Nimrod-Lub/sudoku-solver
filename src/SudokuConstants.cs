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
        public static readonly string EXIT_STRING = "exit";


        public static double chooseTime = 0;
        public static double boardCopyTime = 0;
        public static double obviousTuplesTime = 0;
        public static double nakedTuplesTime = 0;
        public static double hiddenTuplesTime = 0;
        public static int inObviousTuple = 0;
    }
}
