using src.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src
{
    public static class InputValidityChecker
    {
        public static void CheckValidity(string input)
        {
            if (input == null)
            {
                throw new EndOfStreamException("Reached EOF");
            }
            if (input.Length == 0)
            {
                throw new InvalidInputException("No input was provided");
            }

            double boardLength = Math.Sqrt(input.Length);
            if (boardLength != Math.Floor(boardLength))
            {
                throw new InvalidInputException("Square root of input length must be an integer. " +
                    $"Current input length is {input.Length}");
            }

            double blockLength = Math.Sqrt(boardLength);
            if (blockLength != Math.Floor(blockLength))
            {
                throw new InvalidInputException("4th root of input length must be an integer. " +
                    $"Current input length is {input.Length}");
            }

            int max = 0;

            foreach (char c in input)
            {
                int curr = c - '0';
                if (curr < 0)
                {
                    throw new InvalidInputException($"For a {boardLength}x{boardLength} board, " +
                        $"you must input values between 0 and {'0' + boardLength} in the ascii table. " +
                    $"The char {c} is out of that range (lower than '0')");
                }
                if (curr > max)
                    max = curr;
            }
            

            if (max > boardLength)
            {
                throw new InvalidInputException($"For a {boardLength}x{boardLength} board, " +
                        $"you must input values between 0 and {(char)(boardLength + '0')} in the ascii table. " +
                    $"The char {(char)(max + '0')} is out of that range (higher than {(char)(boardLength + '0')}");
            }

            SudokuConstants.boardLength = (int)boardLength;
            SudokuConstants.blockLength = (int)blockLength;

        }
    }
}
