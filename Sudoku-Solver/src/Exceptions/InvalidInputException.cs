using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Exceptions
{
    public class InvalidInputException : Exception // Exception that is thrown when user input is invalid
    {
        public InvalidInputException(string message) : base(message) 
        {

        }
    }
}
