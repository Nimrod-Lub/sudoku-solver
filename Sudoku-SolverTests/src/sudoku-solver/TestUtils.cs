using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Tests
{
    public class TestUtils
    {
        // Checks if the solved board was solved correctly
        public static bool SolvedCorrectly(string input, Cell[,] result)
        {
            if (!Board.IsSolvable(result)) // If the result board is not a valid solution, return false
                return false;

            string resultString = Board.BoardToString(result);

            // Checks that every non-empty cell in the original board is included in the solved board
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != '0')
                {
                    if (input[i] != resultString[i])
                        return false;
                }
            }
            return true;
        }
    }
}
