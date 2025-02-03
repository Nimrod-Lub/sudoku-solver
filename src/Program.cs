using System;
using System.Diagnostics;
using System.Drawing;

namespace src
{
    public class Program
    {
        public static void Main(string[] args)
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

                //int[,] sudokuBoard = BoardBuilder.BuildBoard(input);
                Cell[,] board = Board.BuildBoard(input);
                outputHandler.Output("The inputted board is:\n\n");
                outputHandler.OutputBoard(board);


                Stopwatch solveTimer = new Stopwatch();
                solveTimer.Start();
                //bool result = SudokuBacktrackingSolver.Solve(sudokuBoard, 0, 0);
                Cell[,] result = SudokuHeuristicsSolver.Solve(board);
                solveTimer.Stop();

                Console.WriteLine($"Choose took {SudokuConstants.chooseTime} seconds");
                Console.WriteLine($"Board copy took {SudokuConstants.boardCopyTime} seconds");
                Console.WriteLine($"Entered obvious tuple {SudokuConstants.inObviousTuple} times");
                Console.WriteLine($"Obvious tuple took {SudokuConstants.obviousTuplesTime - SudokuConstants.chooseTime} seconds");
                Console.WriteLine($"Naked tuple took {SudokuConstants.nakedTuplesTime} seconds");
                Console.WriteLine($"Hidden tuple took {SudokuConstants.hiddenTuplesTime} seconds");

                long solveLenMillis = solveTimer.ElapsedMilliseconds;
                outputHandler.Output(string.Format("Time taken to solve: {0} milliseconds", solveLenMillis));

                if (result == null)
                {
                    outputHandler.Output("\n\nThe board you provided is not solvable");
                }
                else
                {
                    outputHandler.Output("\n\nThe solution of the board is:\n\n");
                    outputHandler.OutputBoard(result);
                    outputHandler.Output("\n\nSolution of the board in string format:\n\n");
                    outputHandler.Output(Board.boardToString(result));
                }
            }
        }
    }
}


