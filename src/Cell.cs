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
        private HashSet<byte> possibilites;
        //private List<Tuple<byte, byte, byte>> possiblityChanges;

        public Cell()
        {
            value = 0;
            possibilites = new HashSet<byte>
                (Enumerable.Range(1, SudokuConstants.boardLength).Select(x => (byte)x));
            // possiblityChanges = new();
        }

        public Cell(Cell cell)
        {
            value = cell.value;
            possibilites = new HashSet<byte>(cell.possibilites);
        }

        public byte GetValue()
        {
            return value;
        }

        public void SetValue(byte value)
        {
            this.value = value;
            //if (value != 0)
            //    possibilites.Clear();
        }
        public void RemovePossibility(byte value)
        {
            possibilites.Remove(value);
        }

        public HashSet<byte> GetPossibilities()
        {
            return possibilites;
        }

    }
}
