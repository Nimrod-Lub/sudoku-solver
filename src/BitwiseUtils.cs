using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src
{
    public class BitwiseUtils
    {
        public static int GetPossibilitiesCount(int possibilities)
        {
            int possibilitiesCount = 0;
            while (possibilities != 0)
            {
                possibilitiesCount += possibilities & 1;
                possibilities = possibilities >> 1;
            }
            return possibilitiesCount;
        }

        public static byte GetLastNum (int possibilities)
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
