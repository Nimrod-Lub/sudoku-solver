using Sudoku_Solver.sudoku_solver;
using System;
class Program
{
    public static void Main(String[] args)
    {
        
        Console.WriteLine("Enter sudoku board:");
        IInputHandler inputHandler = new CLIHandler();

        string input = inputHandler.GetInput();
        InputValidityChecker inputValidityChecker = new InputValidityChecker();
        inputValidityChecker.CheckValidity(input);
        
    }
}

