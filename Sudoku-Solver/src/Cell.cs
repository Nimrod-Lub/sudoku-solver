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
        private byte value; // The value of the tile (is 0 if a value is not yet assigned)
        private int possibilities; // Possiblities - represented using bits e.g. 1st bit is on if 1 is a possible value of the cell

        public Cell()
        {
            value = 0;

            // The cell can hold every possible value in the beginning
            for (int i = 0; i < SolverConstants.boardLength; i++)
                possibilities = possibilities | (1 << i);
        }
        
        // Copy constructor used for deep cloning the board
        public Cell(Cell cell)
        {
            value = cell.value;
            possibilities = cell.possibilities;
        }

        public byte GetValue()
        {
            return value;
        }

        public void SetValue(byte value)
        {
            this.value = value;
            if (value != 0)
                possibilities = 0;
        }

        // Removes the possiblity num from possibilities
        public void RemovePossibility(byte value)
        {
            // Bitwise expression - and operation with all bits set to 1 except for the bit we want to remove
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
