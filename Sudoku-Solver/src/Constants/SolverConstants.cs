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

        // Maximum combination size for obvious tuples
        public static readonly int MAXIMUM_COMBINATION_SIZE = 2; 

        // Stores combinations after making a combination list
        public static Dictionary<(int, int), List<List<int>>> storedCombinations = new(); 
    }
}
