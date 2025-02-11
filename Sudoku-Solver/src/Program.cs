using src.Constants;
using src.Exceptions;
using src.IO;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;

namespace src
{
    public class Program
    {
        /// <summary>
        /// Main function of the program.
        /// </summary>
        public void Run()
        {
            CLIHandler cliHandler;
            FileHandler fileHandler;
            IInputHandler boardInput = null;
            IOutputHandler boardOutput = null;
            string input = null;
            Console.WriteLine("Welcome to my sudoku!\n\n");
            
            // While user hasn't given valid input
            while (true)
            {
                Console.WriteLine($"Enter {IOConstants.FILE_INPUT} to input from file, " +
                    $"{IOConstants.CONSOLE_INPUT} to input from console, " +
                    $"or {IOConstants.EXIT_STRING} to exit");
                input = Console.ReadLine();
                // Reached EOF
                if (input == null)
                {
                    Console.WriteLine("Reached EOF");
                    Environment.Exit(0);
                }
                // User wants to exit the program
                else if (input.Equals(IOConstants.EXIT_STRING))
                {
                    Console.WriteLine("Exitting program");
                    Environment.Exit(0);
                }
                // User wants to input boards using files
                else if (input.Equals(IOConstants.FILE_INPUT))
                {
                    Console.WriteLine($"The solutions of the boards in the file will be displayed at {IOConstants.OUTPUT_FILENAME}");
                    Console.WriteLine("Enter the input file path");
                    input = Console.ReadLine();
                    if (input == null)
                    {
                        Console.WriteLine("Reached EOF");
                        Environment.Exit(0);
                    }
                    try
                    {
                        // Set the board input and output to files
                        fileHandler = new FileHandler(input);
                        boardInput = fileHandler;
                        boardOutput = fileHandler;
                        break;
                    }
                    catch (IOException ioe) // Something went wrong with the file interaction
                    {
                        Console.WriteLine(ioe.Message);
                    }

                }
                // User wants to input boards using console
                else if (input.Equals(IOConstants.CONSOLE_INPUT))
                {
                    // Set the board input and output to console
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

            // While user hasn't exited/ not EOF
            while (true)
            {
                try
                {
                    // Get one board
                    input = boardInput.GetInput();
                    // Check validity of the board - throws exceptions if invalid
                    InputValidityChecker.CheckValidity(input);
                    // Build a Cell matrix using user input
                    Cell[,] board = Board.BuildBoard(input);
                    Console.WriteLine("\nThe inputted board is:\n\n");
                    Board.OutputBoard(board);

                    Stopwatch solveTimer = new Stopwatch();
                    solveTimer.Start();
                    // Solve the board - the amount of time taken to solve is measured with the Stopwatch object
                    Cell[,] result = SudokuHeuristicsSolver.Solve(board);
                    solveTimer.Stop();
                    
                    double solveLenSeconds = solveTimer.Elapsed.TotalSeconds;
                    Console.WriteLine(string.Format("\n\nTime taken to solve: {0} seconds", solveLenSeconds));
                    
                    if (result == null) // If the board is unsolvable
                    {
                        boardOutput.Output("The board you provided is not solvable");
                    }
                    else // Board was solved
                    {
                        Console.WriteLine("The solution of the board is:\n\n");
                        Board.OutputBoard(result);
                        boardOutput.Output(Board.BoardToString(result));
                    }
                }
                catch (InvalidInputException iie) // Input is invalid
                {
                    boardOutput.Output(iie.Message);
                }
                catch (EndOfStreamException eose) // Reached EOF
                {
                    Console.WriteLine(eose.Message);
                    Environment.Exit(0);
                }
            }
        }
    }
}


