using src.Constants;
using src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Tests
{
    [TestClass()]
    public class SolveUnsolvableBoards
    {
        [TestMethod()]
        public void UnsolvableBoard1()
        {
            string boardString = "000005080000601043000000000010500000000106000300000005530000061000000004000000000";
            SolverConstants.boardLength = (int)(Math.Sqrt(boardString.Length));
            SolverConstants.blockLength = (int)(Math.Sqrt(SolverConstants.boardLength));

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void UnsolvableBoard2()
        {
            string boardString = "000030000060000400007050800000406000000900000050010300400000020000300000000000000";
            SolverConstants.boardLength = (int)(Math.Sqrt(boardString.Length));
            SolverConstants.blockLength = (int)(Math.Sqrt(SolverConstants.boardLength));

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void UnsolvableBoard3()
        {
            string boardString = "009028700806004005003000004600000000020713450000000002300000500900400807001250300";
            SolverConstants.boardLength = (int)(Math.Sqrt(boardString.Length));
            SolverConstants.blockLength = (int)(Math.Sqrt(SolverConstants.boardLength));

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void UnsolvableBoard4()
        {
            string boardString = "090300001000080046000000800405060030003275600060010904001000000580020000200007060";
            SolverConstants.boardLength = (int)(Math.Sqrt(boardString.Length));
            SolverConstants.blockLength = (int)(Math.Sqrt(SolverConstants.boardLength));

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void UnsolvableBoard5()
        {
            string boardString = "000041000060000020002000000320600000000050041700000002000000230048000000501002000";
            SolverConstants.boardLength = (int)(Math.Sqrt(boardString.Length));
            SolverConstants.blockLength = (int)(Math.Sqrt(SolverConstants.boardLength));

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void UnsolvableBoard6()
        {
            string boardString = "900100004014030800003000090000708001800003000000000030021000070009040500500016003";
            SolverConstants.boardLength = (int)(Math.Sqrt(boardString.Length));
            SolverConstants.blockLength = (int)(Math.Sqrt(SolverConstants.boardLength));

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void UnsolvableBoard7()
        {
            string boardString = ">000?000;00=20000@000090700:0300000;080000090?0060000=;0000000<90>40000000300000820004<007000005;000@0020>40000:700005=080009000070?00500820<000090000000=00080000209>40000000;00;00200000000700@002<0000?0000=05000000040900000000600350182000>0000:700000;0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000180";
            SolverConstants.boardLength = (int)(Math.Sqrt(boardString.Length));
            SolverConstants.blockLength = (int)(Math.Sqrt(SolverConstants.boardLength));

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsNull(result);
        }

    }
}
