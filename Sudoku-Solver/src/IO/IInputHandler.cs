using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace src.IO
{
    public interface IInputHandler // Interface for inputting boards
    {
        public abstract string GetInput(); // Get a board from user
    }
}