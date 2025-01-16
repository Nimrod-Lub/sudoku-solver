using System;
using System.Drawing;

namespace Sudoku_Solver
{
    public class Program
    {
        public static void Main(String[] args)
        {

            Console.WriteLine("Enter sudoku board:");
            CLIHandler cliHandler = new CLIHandler();
            IInputHandler inputHandler = cliHandler;
            IOutputHandler outputHandler = cliHandler;

            string input = inputHandler.GetInput();
            InputValidityChecker.CheckValidity(input);

            int[,] sudokuBoard = BoardBuilder.BuildBoard(input);
            outputHandler.OutputBoard(sudokuBoard);

            int[,] result = SudokuSolver.Solve(sudokuBoard, 0, 0);
            outputHandler.OutputBoard(result);

        }
    }
}


