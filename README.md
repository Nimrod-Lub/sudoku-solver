Sudoku Solver
-

A sudoku board is a x*x grid filled with numbers and divided into sqrt(x) blocks. The number 0 represents an empty cell.
A sudoku board is solved when there are no empty cells in the board and in each row, column and block in the board, each number only appears once.

My algorithm receives sudoku boards from the console or from files, and attempts to solve each one.
If a board has a solution, the solution is displayed to the user in either the console or the file output.txt.
If a board does not have a solution, the user is informed that the board is unsolvable.
The time taken to solve the board is also displayed at the console.

My Sudoku solver works for 1x1, 4x4, 9x9, 16x16 and 25x25 boards

An input will be valid if:
- The 4th root of the input length is an integer
- The only characters in the input are '0' and all the characters that follow it in the ASCII table up to the length of the board
  (e.g. for a 16x16 board the allowed characters are 0 1 2 3 4 5 6 7 8 9 : ; < = > ? @).
- The board length must be at most 25x25

The value of each symbol is the distance from '0' in the ASCII table (e.g. the value of '1' is 1 and the value of '@' is 16)

Example for a board input:
Input: 
005890603060403000030050010500038100003009000190570000000042008850010409070905020
Expected solution in string format:
425891673761423985938756214547238196283169547196574832319642758852317469674985321

My solver is based on heuristics and backtracking: 
The solver will attempt to prune the possibilities using heuristics - in my project I implemented naked singles and obvious tuples.
When it no longer makes any progress, the algorithm will find the cell with the least amount of possible values, guess a value, and return to using human tactics.
If the board was unsolvable, the cell will guess a different possiblity.
When the cell runs out of possibilities, the board is unsolvable.

The project was implemented in C#, and is built to run on .NET 8.0.
Unit tests were implemented using Visual Studio's testing project.
