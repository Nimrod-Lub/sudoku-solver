using System;
using System.Diagnostics;
using System.Drawing;

namespace Sudoku_Solver
{
    public class Program
    {
        public static void Main(String[] args)
        {
            
            CLIHandler cliHandler = new CLIHandler();
            IInputHandler inputHandler = cliHandler;
            IOutputHandler outputHandler = cliHandler;

            while (true)
            {
                Console.WriteLine("Enter sudoku board:");
                string input = inputHandler.GetInput();

                if (input != null && input.Equals("exit"))
                    Environment.Exit(0);

                InputValidityChecker.CheckValidity(input);

                int[,] sudokuBoard = BoardBuilder.BuildBoard(input);
                outputHandler.Output("The inputted board is:\n\n");
                outputHandler.OutputBoard(sudokuBoard);


                Stopwatch solveTimer = new Stopwatch();
                solveTimer.Start();
                bool result = SudokuSolver.Solve(sudokuBoard, 0, 0);
                solveTimer.Stop();
                long solveLenMillis = solveTimer.ElapsedMilliseconds;
                outputHandler.Output(String.Format("Time taken to solve: {0} milliseconds", solveLenMillis));

                if (result == false)
                {
                    outputHandler.Output("\n\nThe board you provided is not solvable");
                }
                else
                {
                    outputHandler.Output("\n\nThe solution of the board is:\n\n");
                    outputHandler.OutputBoard(sudokuBoard);
                }
            }
        }
    }
}


