using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src
{
    public class Cell
    {
        private byte value;
        //private HashSet<byte> possibilities;
        private int possibilities;

        public Cell()
        {
            value = 0;
            //possibilities = new HashSet<byte>
            //    (Enumerable.Range(1, SudokuConstants.boardLength).Select(x => (byte)x));

            // the cell can hold every possible value in the beginning
            for (int i = 0; i < SudokuConstants.boardLength; i++)
                possibilities = possibilities | (1 << i);
        }

        public Cell(Cell cell)
        {
            value = cell.value;
            //possibilities = new HashSet<byte>(cell.possibilities);
            possibilities = cell.GetPossibilities();
        }

        public byte GetValue()
        {
            return value;
        }

        public void SetValue(byte value)
        {
            this.value = value;
            if (value != 0)
                //possibilites.Clear();
                possibilities = 0;
        }
        public void RemovePossibility(byte value) // 1,3,5,9 ->       100010101
        {
            // bitwise expression - and operation with all bits set to 1 except for the bit we want to remove
            possibilities = possibilities & (~(1 << (value - 1)));
        }

        public int GetPossibilities()
        {
            return possibilities;
        }

        public void SetPossibilities(int possibilities)
        {
            this.possibilities = possibilities;
        }

        public int GetPossibilitiesCount()
        {
            return BitwiseUtils.GetPossibilitiesCount(possibilities);
        }
    }
}
