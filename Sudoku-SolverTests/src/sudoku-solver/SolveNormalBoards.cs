using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Constants;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Tests
{
    [TestClass()]
    public class SolveNormalBoards
    {

        [TestMethod()]
        public void NormalBoard1()
        {
            string boardString = "800000070006010053040600000000080400003000700020005038000000800004050061900002000";
            SolverConstants.boardLength = (int)(Math.Sqrt(boardString.Length));
            SolverConstants.blockLength = (int)(Math.Sqrt(SolverConstants.boardLength));

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsTrue(TestUtils.SolvedCorrectly(boardString, result));
        }

        [TestMethod()]
        public void NormalBoard2()
        {
            string boardString = "003000002080050000700800049000000100006003000900500078009060014000400200100000500";
            SolverConstants.boardLength = (int)(Math.Sqrt(boardString.Length));
            SolverConstants.blockLength = (int)(Math.Sqrt(SolverConstants.boardLength));

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
        }

        [TestMethod()]
        public void NormalBoard3()
        {
            string boardString = "509600000030807920000300800000016080050000010000000032104030000006709000000000003";
            SolverConstants.boardLength = (int)(Math.Sqrt(boardString.Length));
            SolverConstants.blockLength = (int)(Math.Sqrt(SolverConstants.boardLength));

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsTrue(TestUtils.SolvedCorrectly(boardString, result));
        }

        [TestMethod()]
        public void NormalBoard4()
        {
            string boardString = "600040010010000003002008040020000004007382600500000020090500100400000070050090002";
            SolverConstants.boardLength = (int)(Math.Sqrt(boardString.Length));
            SolverConstants.blockLength = (int)(Math.Sqrt(SolverConstants.boardLength));

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsTrue(TestUtils.SolvedCorrectly(boardString, result));
        }

        [TestMethod()]
        public void NormalBoard5()
        {
            string boardString = "100042700200003008057001000000000060090000100000715200000000002018009003900054000";
            SolverConstants.boardLength = (int)(Math.Sqrt(boardString.Length));
            SolverConstants.blockLength = (int)(Math.Sqrt(SolverConstants.boardLength));

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsTrue(TestUtils.SolvedCorrectly(boardString, result));
        }

        [TestMethod()]
        public void NormalBoard6()
        {
            string boardString = "003080600820000000006075080000000750900050806017006000000007010000500000000064937";
            SolverConstants.boardLength = (int)(Math.Sqrt(boardString.Length));
            SolverConstants.blockLength = (int)(Math.Sqrt(SolverConstants.boardLength));

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsTrue(TestUtils.SolvedCorrectly(boardString, result));
        }

        [TestMethod()]
        public void NormalBoard7()
        {
            string boardString = "040287000000000000309000002507034600400000008200870009000020090060700400000041006";
            SolverConstants.boardLength = (int)(Math.Sqrt(boardString.Length));
            SolverConstants.blockLength = (int)(Math.Sqrt(SolverConstants.boardLength));

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsTrue(TestUtils.SolvedCorrectly(boardString, result));
        }

        [TestMethod()]
        public void NormalBoard8()
        {
            string boardString = "507000030000061000108000000620040000000700080000000000010000604300500000000000200";
            SolverConstants.boardLength = (int)(Math.Sqrt(boardString.Length));
            SolverConstants.blockLength = (int)(Math.Sqrt(SolverConstants.boardLength));

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsTrue(TestUtils.SolvedCorrectly(boardString, result));
        }

        [TestMethod()]
        public void NormalBoard9()
        {
            string boardString = "003080000000350000070000600005000000020009407000000001000000080060000030100004000";
            SolverConstants.boardLength = (int)(Math.Sqrt(boardString.Length));
            SolverConstants.blockLength = (int)(Math.Sqrt(SolverConstants.boardLength));

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsTrue(TestUtils.SolvedCorrectly(boardString, result));
        }

        [TestMethod()]
        public void NormalBoard10()
        {
            string boardString = "000000000000003085001020000000507000004000100090000000500000073002010000000040009";
            SolverConstants.boardLength = (int)(Math.Sqrt(boardString.Length));
            SolverConstants.blockLength = (int)(Math.Sqrt(SolverConstants.boardLength));

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsTrue(TestUtils.SolvedCorrectly(boardString, result));
        }

        [TestMethod()]
        public void NormalBoard11()
        {
            string boardString = "050908600800006007006020000009000070203000809010000400000030700900800004005604030";
            SolverConstants.boardLength = (int)(Math.Sqrt(boardString.Length));
            SolverConstants.blockLength = (int)(Math.Sqrt(SolverConstants.boardLength));

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsTrue(TestUtils.SolvedCorrectly(boardString, result));
        }

        [TestMethod()]
        public void NormalBoard12()
        {
            string boardString = "000500700095070006000002850100000907007010200908000005063800000700050640001004000";
            SolverConstants.boardLength = (int)(Math.Sqrt(boardString.Length));
            SolverConstants.blockLength = (int)(Math.Sqrt(SolverConstants.boardLength));

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsTrue(TestUtils.SolvedCorrectly(boardString, result));
        }

        [TestMethod()]
        public void NormalBoard13()
        {
            string boardString = "006401500300509001000030000205000703030040020670000058060907080000050000002304900";
            SolverConstants.boardLength = (int)(Math.Sqrt(boardString.Length));
            SolverConstants.blockLength = (int)(Math.Sqrt(SolverConstants.boardLength));

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsTrue(TestUtils.SolvedCorrectly(boardString, result));
        }

        [TestMethod()]
        public void NormalBoard14()
        {
            string boardString = "900500007040060090003009500005000001060000080200000700006700100090020060800003004";
            SolverConstants.boardLength = (int)(Math.Sqrt(boardString.Length));
            SolverConstants.blockLength = (int)(Math.Sqrt(SolverConstants.boardLength));

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsTrue(TestUtils.SolvedCorrectly(boardString, result));
        }

        [TestMethod()]
        public void NormalBoard15()
        {
            string boardString = "040860000009002500620530000150000200090000050003000048000025073002100900000098010";
            SolverConstants.boardLength = (int)(Math.Sqrt(boardString.Length));
            SolverConstants.blockLength = (int)(Math.Sqrt(SolverConstants.boardLength));

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsTrue(TestUtils.SolvedCorrectly(boardString, result));
        }

        [TestMethod()]
        public void NormalBoard16()
        {
            string boardString = "000408000041000820000050000050000030020137040800000009900060005005903600002000400";
            SolverConstants.boardLength = (int)(Math.Sqrt(boardString.Length));
            SolverConstants.blockLength = (int)(Math.Sqrt(SolverConstants.boardLength));

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsTrue(TestUtils.SolvedCorrectly(boardString, result));
        }

        [TestMethod()]
        public void NormalBoard17()
        {
            string boardString = "000070600980000050000003802300080510056000240019050007208100000030000025005060000";
            SolverConstants.boardLength = (int)(Math.Sqrt(boardString.Length));
            SolverConstants.blockLength = (int)(Math.Sqrt(SolverConstants.boardLength));

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsTrue(TestUtils.SolvedCorrectly(boardString, result));
        }

        [TestMethod()]
        public void NormalBoard18()
        {
            string boardString = "030050400800090001002006500013000000000465000000000750006200800200030009001080040";
            SolverConstants.boardLength = (int)(Math.Sqrt(boardString.Length));
            SolverConstants.blockLength = (int)(Math.Sqrt(SolverConstants.boardLength));

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsTrue(TestUtils.SolvedCorrectly(boardString, result));
        }

        [TestMethod()]
        public void NormalBoard19()
        {
            string boardString = "002070000100002000708500001050300002009010600200007010800004306000800004000090100";
            SolverConstants.boardLength = (int)(Math.Sqrt(boardString.Length));
            SolverConstants.blockLength = (int)(Math.Sqrt(SolverConstants.boardLength));

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsTrue(TestUtils.SolvedCorrectly(boardString, result));
        }

        [TestMethod()]
        public void NormalBoard20()
        {
            string boardString = "081902670000000000700080001000010000002030700004608500405000107020040030010000060";
            SolverConstants.boardLength = (int)(Math.Sqrt(boardString.Length));
            SolverConstants.blockLength = (int)(Math.Sqrt(SolverConstants.boardLength));

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsTrue(TestUtils.SolvedCorrectly(boardString, result));
        }

        [TestMethod()]
        public void NormalBoard21()
        {
            string boardString = "0000:=000000000?70050;01:00@90<8900800700004600=60:=080000070002=00030890>?500012;01@:000008007>00001000@0000900000<>?0740000000006@900000>0100002;0600=800<00500070002000000000<0900>?5;4020=0@0020=0@0<0907000>?500400=6000<803<0000002001@:0000=68000?0004020";
            SolverConstants.boardLength = (int)(Math.Sqrt(boardString.Length));
            SolverConstants.blockLength = (int)(Math.Sqrt(SolverConstants.boardLength));

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsTrue(TestUtils.SolvedCorrectly(boardString, result));
        }

        [TestMethod()]
        public void NormalBoard22()
        {
            string boardString = "030000;062<000@0001;:620@80>700400020@8900035=109>0030000;0=0000=000620080003?05:00008000030000<0@000450;<0000090?4500000900>@0004000<:10>600870000000060008040=0200070050?010<0@873050?0:000200;<060000300000=020>070?0000500:60000501000;020000001006;00098030";
            SolverConstants.boardLength = (int)(Math.Sqrt(boardString.Length));
            SolverConstants.blockLength = (int)(Math.Sqrt(SolverConstants.boardLength));

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsTrue(TestUtils.SolvedCorrectly(boardString, result));
        }

        [TestMethod()]
        public void NormalBoard23()
        {
            string boardString = ">000?006;00=20000@000090700:0300300;080000096?0060000=;3000000<90>40000000300000820004<007600005;300@0020>40760:700:05=080009000076?00500820<0000900000:0=03080000209>40000000;30;002@00<0>0:700@002<0000?0000=0500000@040900:00000600350182000>0090:700300;0180";
            SolverConstants.boardLength = (int)(Math.Sqrt(boardString.Length));
            SolverConstants.blockLength = (int)(Math.Sqrt(SolverConstants.boardLength));

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.IsTrue(TestUtils.SolvedCorrectly(boardString, result));
        }
    }
}