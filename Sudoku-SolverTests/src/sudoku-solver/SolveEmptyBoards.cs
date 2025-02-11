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
            Assert.AreEqual(Board.BoardToString(result), "1");
        }

        [TestMethod()]
        public void EmptyBoardLength4()
        {
            string boardString = "0000000000000000";
            SolverConstants.boardLength = 4;
            SolverConstants.blockLength = 2;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.AreEqual(Board.BoardToString(result), "1234341221434321");
        }

        [TestMethod()]
        public void EmptyBoardLength9()
        {
            string boardString = "000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            SolverConstants.boardLength = 9;
            SolverConstants.blockLength = 3;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.AreEqual(Board.BoardToString(result), "123456789456789123789123456231674895875912364694538217317265948542897631968341572");
        }

        [TestMethod()]
        public void EmptyBoardLength16()
        {
            string boardString = "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            SolverConstants.boardLength = 16;
            SolverConstants.blockLength = 4;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.AreEqual(Board.BoardToString(result), "123456789:;<=>?@5678=>?@12349:;<9:;<1234=>?@5678=>?@9:;<56781234241389:5>;<6@?=7?;<>@4162=57839:7=@9>;2?83:164<585:637<=?@49;12>31826597;4@=:<>?<74=;?>1398:2@56>@9:4<=2651?378;6?5;:8@37<2>491=4861<35;:7>2?=@9:3=72@89<?65>;41;9>?714:@8=3<562@<25?=6>419;78:3");
        }

        [TestMethod()]
        public void EmptyBoardLength25()
        {
            string boardString = "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            SolverConstants.boardLength = 25;
            SolverConstants.blockLength = 5;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.AreEqual(Board.BoardToString(result), "123456789:;<=>?@ABCDEFGHI6789:EFGHI12345;<=>?@ABCD;<=>?12345@ABCDEFGHI6789:@ABCD;<=>?EFGHI6789:12345EFGHI@ABCD6789:12345;<=>?25134:;<=6BE>?7FGC@8IHDA9ADEFGH>517I3689<=;?2:@4BC:@C69?GEI4F=<DH>BA137528;HI><B28A@F4C;:G79D563=1?E8?7;=CD93B215A@H:EI4<>6FG31?@2>9CA8=DFE4:57BGH;I<6FGHDC456<1?B@;3I>28=9:E7AI4A8<=@7B2>9:56?HF;EDGC13=;5E>FH:D37GI1CA496<28?@B7:9B6GI?E;H8A2<CD13@=4>5F4B25;9:16HA>7F=3?<DCGE@I8<CDIF3=>8E5:?B146@GHA9;27>86=AI?@7GDHC<29;:EB51F349E@7HAB2;C364G851I=F>?:D<?3:G154DF<9@EI;28>7ACBH6=D6F:E83IG9<427>B@HA;?C5=1G=I28<1;5@:?96ADC4F>B37EHC9;?376F2>GIH=B8E5<14DA:@B>4A7DCH?=851@EG36:9FI<;25H<1@BE4:AC;D3F=I?27869G>");
        }
    }
}
