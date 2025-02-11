using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Constants;
using src.utils;

namespace src
{
    public class Cell // Represents one tile in a sudoku board
    {
        private byte value; // the value of the tile (is 0 if a value is not yet assigned)
        //private HashSet<byte> possibilities;
        private int possibilities; // possiblities - represented using bits e.g. 1st bit is on if 1 is a possible value of the cell

        public Cell()
        {
            value = 0;
            //possibilities = new HashSet<byte>
            //    (Enumerable.Range(1, SudokuConstants.boardLength).Select(x => (byte)x));

            // the cell can hold every possible value in the beginning
            for (int i = 0; i < SolverConstants.boardLength; i++)
                possibilities = possibilities | (1 << i);
        }
        
        // Copy constructor used for deep cloning the board
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

        // Removes the possiblity num from possibilities
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
