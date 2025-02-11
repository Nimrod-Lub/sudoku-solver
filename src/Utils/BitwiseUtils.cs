using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.utils
{
    public class BitwiseUtils // Functions that help with bitwise operations
    {
        // Returns the amount of possibilities in possibilities
        public static int GetPossibilitiesCount(int possibilities)
        {
            int possibilitiesCount = 0;
            while (possibilities != 0)
            {
                possibilitiesCount += possibilities & 1; // if a bit is activated, it's a posibility
                possibilities = possibilities >> 1;
            }
            return possibilitiesCount;
        }

        // Returns the last number in possibilities (is called when there is only one possibility remaining)
        public static byte GetLastNum(int possibilities)
        {
            byte lastNum = 0;
            while (possibilities != 0)
            {
                possibilities = possibilities >> 1;
                lastNum++;
            }
            return lastNum;
        }
    }
}
