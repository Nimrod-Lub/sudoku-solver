using src.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Tests
{

    [TestClass()]
    public class SolveEmptyBoards
    {
        [TestMethod()]
        public void EmptyBoardLength1()
        {
            string boardString = "0";
            SolverConstants.boardLength = 1;
            SolverConstants.blockLength = 1;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsTrue(TestUtils.SolvedCorrectly(boardString, result));
        }

        [TestMethod()]
        public void EmptyBoardLength4()
        {
            string boardString = "0000000000000000";
            SolverConstants.boardLength = 4;
            SolverConstants.blockLength = 2;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsTrue(TestUtils.SolvedCorrectly(boardString, result));
        }

        [TestMethod()]
        public void EmptyBoardLength9()
        {
            string boardString = "000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            SolverConstants.boardLength = 9;
            SolverConstants.blockLength = 3;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsTrue(TestUtils.SolvedCorrectly(boardString, result));
        }

        [TestMethod()]
        public void EmptyBoardLength16()
        {
            string boardString = "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            SolverConstants.boardLength = 16;
            SolverConstants.blockLength = 4;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsTrue(TestUtils.SolvedCorrectly(boardString, result));
        }

        [TestMethod()]
        public void EmptyBoardLength25()
        {
            string boardString = "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            SolverConstants.boardLength = 25;
            SolverConstants.blockLength = 5;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsTrue(TestUtils.SolvedCorrectly(boardString, result));
        }
    }
}
