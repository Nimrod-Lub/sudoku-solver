using src.Constants;
using src.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src
{
    public static class InputValidityChecker // Validates input - throws exceptions if there are problems with the input
    {
        public static void CheckValidity(string input)
        {
            if (input == null) // If EOF
            {
                throw new EndOfStreamException("Reached EOF");
            }
            if (input.Length == 0) // If input is empty
            {
                throw new InvalidInputException("No input was provided");
            }

            double boardLength = Math.Sqrt(input.Length);
            if (boardLength != Math.Floor(boardLength)) // Checks whether input length is valid for a sudoku board
            {
                throw new InvalidInputException("Square root of input length must be an integer. " +
                    $"Current input length is {input.Length}");
            }

            double blockLength = Math.Sqrt(boardLength);
            if (blockLength != Math.Floor(blockLength)) // Further checks whether input length is valid for a sudoku board
            {
                throw new InvalidInputException("4th root of input length must be an integer. " +
                    $"Current input length is {input.Length}");
            }

            int max = 0;

            foreach (char c in input) // checks if every single char in the input is valid for board size boardLength x boardLength
            {
                int curr = c - '0';
                if (curr < 0) // Current char is too low in the ascii table to be valid input
                {
                    throw new InvalidInputException($"For a {boardLength}x{boardLength} board, " +
                        $"you must input values between 0 and {'0' + boardLength} in the ascii table. " +
                    $"The char {c} is out of that range (lower than '0')");
                }
                if (curr > max)
                    max = curr;
            }
            

            if (max > boardLength) // There is a char whose value in the ascii table is too high to be valid input
            {
                throw new InvalidInputException($"For a {boardLength}x{boardLength} board, " +
                        $"you must input values between 0 and {(char)(boardLength + '0')} in the ascii table. " +
                    $"The char {(char)(max + '0')} is out of that range (higher than {(char)(boardLength + '0')}");
            }

            SolverConstants.boardLength = (int)boardLength; // Initializes board length
            SolverConstants.blockLength = (int)blockLength; // Initializes block Length

        }
    }
}
