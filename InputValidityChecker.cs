using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class InputValidityChecker
{
    public void CheckValidity(String input)
    {
        if (input == null || 
            input.Length == 0 || 
            Math.Sqrt(input.Length) != Math.Floor(Math.Sqrt(input.Length)))
        {
            Environment.Exit(1); // will throw an exception
        }

        foreach (char c in input)
        {
            if (!char.IsDigit(c))
            {
                Environment.Exit(1); // will throw an exception
            }
        }
    }
}
