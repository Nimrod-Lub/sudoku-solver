using src.Exceptions;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;

namespace src
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CLIHandler cliHandler;
            FileHandler fileHandler;
            IInputHandler boardInput = null;
            IOutputHandler boardOutput = null;
            string input = null;

            while (true)
            {
                Console.WriteLine($"Enter {SudokuConstants.FILE_INPUT} to input from file, " +
                    $"{SudokuConstants.CONSOLE_INPUT} to input from console, " +
                    $"or {SudokuConstants.EXIT_STRING} to exit");
                input = Console.ReadLine();
                if (input == null)
                {
                    Console.WriteLine("Reached EOF");
                    Environment.Exit(0);
                }
                else if (input.Equals(SudokuConstants.EXIT_STRING))
                {
                    Console.WriteLine("Exitting program");
                    Environment.Exit(0);
                }    
                else if (input.Equals(SudokuConstants.FILE_INPUT))
                {
                    // TODO get file name and not path
                    Console.WriteLine("Enter the input file path");
                    input = Console.ReadLine();
                    if (input == null)
                    {
                        Console.WriteLine("Reached EOF");
                        Environment.Exit(0);
                    }
                    try
                    {
                        fileHandler = new FileHandler(input);
                        boardInput = fileHandler;
                        boardOutput = fileHandler;
                        break;
                    }
                    catch(IOException ioe)
                    {
                        Console.WriteLine(ioe.Message);
                    }

                }
                else if (input.Equals(SudokuConstants.CONSOLE_INPUT))
                {
                    cliHandler = new CLIHandler();
                    boardInput = cliHandler;
                    boardOutput = cliHandler;
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a valid input");
                }
            }
        
            while (true)
            {
                try
                {
                    input = boardInput.GetInput();
                    InputValidityChecker.CheckValidity(input);

                    Cell[,] board = Board.BuildBoard(input);
                    Console.WriteLine("The inputted board is:\n\n");
                    Board.OutputBoard(board);

                    Stopwatch solveTimer = new Stopwatch();
                    solveTimer.Start();
                    Cell[,] result = SudokuHeuristicsSolver.Solve(board);
                    solveTimer.Stop();

                    //Console.WriteLine($"Board copy took {SudokuConstants.boardCopyTime} seconds");
                    //Console.WriteLine($"Entered obvious tuple {SudokuConstants.inObviousTuple} times");
                    //Console.WriteLine($"Obvious tuple took {SudokuConstants.obviousTuplesTime} seconds");
                    //Console.WriteLine($"Naked tuple took {SudokuConstants.nakedTuplesTime} seconds");
                    //Console.WriteLine($"Hidden tuple took {SudokuConstants.hiddenTuplesTime} seconds");

                    long solveLenMillis = solveTimer.ElapsedMilliseconds;
                    Console.WriteLine(string.Format("\n\nTime taken to solve: {0} milliseconds", solveLenMillis));

                    if (result == null)
                    {
                        boardOutput.Output("The board you provided is not solvable");
                    }
                    else
                    {
                        Console.WriteLine("The solution of the board is:\n\n");
                        Board.OutputBoard(result);
                        boardOutput.Output(Board.BoardToString(result));
                    }
                }
                catch (InvalidInputException iie)
                {
                    // Console.WriteLine(iie.Message);
                    boardOutput.Output(iie.Message);
                }
                catch (EndOfStreamException eose)
                {
                    Console.WriteLine(eose.Message);
                    Environment.Exit(0);
                }
            }
        }
    }
}


