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
            SolverConstants.boardLength = 9;
            SolverConstants.blockLength = 3;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.AreEqual(Board.BoardToString(result), "831529674796814253542637189159783426483296715627145938365471892274958361918362547");

        }

        [TestMethod()]
        public void NormalBoard2()
        {
            string boardString = "003000002080050000700800049000000100006003000900500078009060014000400200100000500";
            SolverConstants.boardLength = 9;
            SolverConstants.blockLength = 3;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.AreEqual(Board.BoardToString(result), "693741852482659731715832649257984163846173925931526478579268314368415297124397586");
        }

        [TestMethod()]
        public void NormalBoard3()
        {
            string boardString = "509600000030807920000300800000016080050000010000000032104030000006709000000000003";
            SolverConstants.boardLength = 9;
            SolverConstants.blockLength = 3;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.AreEqual(Board.BoardToString(result), "589624371431857926267391845743216589652983714918475632194538267326749158875162493");
        }

        [TestMethod()]
        public void NormalBoard4()
        {
            string boardString = "600040010010000003002008040020000004007382600500000020090500100400000070050090002";
            SolverConstants.boardLength = 9;
            SolverConstants.blockLength = 3;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.AreEqual(Board.BoardToString(result), "635249817814756293972138546326915784147382659589467321293574168461823975758691432");
        }

        [TestMethod()]
        public void NormalBoard5()
        {
            string boardString = "100042700200003008057001000000000060090000100000715200000000002018009003900054000";
            SolverConstants.boardLength = 9;
            SolverConstants.blockLength = 3;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.AreEqual(Board.BoardToString(result), "189642735264573918357891426571928364892436157643715289435187692718269543926354871");
        }

        [TestMethod()]
        public void NormalBoard6()
        {
            string boardString = "003080600820000000006075080000000750900050806017006000000007010000500000000064937";
            SolverConstants.boardLength = 9;
            SolverConstants.blockLength = 3;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.AreEqual(Board.BoardToString(result), "753182649821649375496375182268493751934751826517826493642937518379518264185264937");
        }

        [TestMethod()]
        public void NormalBoard7()
        {
            string boardString = "040287000000000000309000002507034600400000008200870009000020090060700400000041006";
            SolverConstants.boardLength = 9;
            SolverConstants.blockLength = 3;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.AreEqual(Board.BoardToString(result), "641287935728593164359416872587934621493162758216875349134628597862759413975341286");
        }

        [TestMethod()]
        public void NormalBoard8()
        {
            string boardString = "507000030000061000108000000620040000000700080000000000010000604300500000000000200";
            SolverConstants.boardLength = 9;
            SolverConstants.blockLength = 3;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.AreEqual(Board.BoardToString(result), "567492138239861745148375926623148579951736482874259361712983654396524817485617293");
        }

        [TestMethod()]
        public void NormalBoard9()
        {
            string boardString = "003080000000350000070000600005000000020009407000000001000000080060000030100004000";
            SolverConstants.boardLength = 9;
            SolverConstants.blockLength = 3;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.AreEqual(Board.BoardToString(result), "453286719691357842872941653715462398328519467946873521234795186567128934189634275");
        }

        [TestMethod()]
        public void NormalBoard10()
        {
            string boardString = "000000000000003085001020000000507000004000100090000000500000073002010000000040009";
            SolverConstants.boardLength = 9;
            SolverConstants.blockLength = 3;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.AreEqual(Board.BoardToString(result), "987654321246173985351928746128537694634892157795461832519286473472319568863745219");
        }

        [TestMethod()]
        public void NormalBoard11()
        {
            string boardString = "050908600800006007006020000009000070203000809010000400000030700900800004005604030";
            SolverConstants.boardLength = 9;
            SolverConstants.blockLength = 3;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.AreEqual(Board.BoardToString(result), "357948621821356947496721385549183276273465819618279453164532798932817564785694132");
        }

        [TestMethod()]
        public void NormalBoard12()
        {
            string boardString = "000500700095070006000002850100000907007010200908000005063800000700050640001004000";
            SolverConstants.boardLength = 9;
            SolverConstants.blockLength = 3;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.AreEqual(Board.BoardToString(result), "612589734895473126374162859136245987547918263928736415463827591789351642251694378");
        }

        [TestMethod()]
        public void NormalBoard13()
        {
            string boardString = "006401500300509001000030000205000703030040020670000058060907080000050000002304900";
            SolverConstants.boardLength = 9;
            SolverConstants.blockLength = 3;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.AreEqual(Board.BoardToString(result), "986471532347529861521836497295168743138745629674293158463917285819652374752384916");
        }

        [TestMethod()]
        public void NormalBoard14()
        {
            string boardString = "900500007040060090003009500005000001060000080200000700006700100090020060800003004";
            SolverConstants.boardLength = 9;
            SolverConstants.blockLength = 3;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.AreEqual(Board.BoardToString(result), "918532647542167398673849512735986421164275983289314756426758139397421865851693274");
        }

        [TestMethod()]
        public void NormalBoard15()
        {
            string boardString = "040860000009002500620530000150000200090000050003000048000025073002100900000098010";
            SolverConstants.boardLength = 9;
            SolverConstants.blockLength = 3;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.AreEqual(Board.BoardToString(result), "745869321389712564621534897154687239897243156263951748918425673432176985576398412");
        }

        [TestMethod()]
        public void NormalBoard16()
        {
            string boardString = "000408000041000820000050000050000030020137040800000009900060005005903600002000400";
            SolverConstants.boardLength = 9;
            SolverConstants.blockLength = 3;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.AreEqual(Board.BoardToString(result), "296418357541379826378652914154896732629137548837245169913764285485923671762581493");
        }

        [TestMethod()]
        public void NormalBoard17()
        {
            string boardString = "000070600980000050000003802300080510056000240019050007208100000030000025005060000";
            SolverConstants.boardLength = 9;
            SolverConstants.blockLength = 3;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.AreEqual(Board.BoardToString(result), "523874691987621453164593872372489516856317249419256387298135764631748925745962138");
        }

        [TestMethod()]
        public void NormalBoard18()
        {
            string boardString = "030050400800090001002006500013000000000465000000000750006200800200030009001080040";
            SolverConstants.boardLength = 9;
            SolverConstants.blockLength = 3;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.AreEqual(Board.BoardToString(result), "637851492854792361192346587513978624728465913469123758946217835285634179371589246");
        }

        [TestMethod()]
        public void NormalBoard19()
        {
            string boardString = "002070000100002000708500001050300002009010600200007010800004306000800004000090100";
            SolverConstants.boardLength = 9;
            SolverConstants.blockLength = 3;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.AreEqual(Board.BoardToString(result), "532471869194682537768539241651348972479215683283967415827154396916823754345796128");
        }

        [TestMethod()]
        public void NormalBoard20()
        {
            string boardString = "081902670000000000700080001000010000002030700004608500405000107020040030010000060";
            SolverConstants.boardLength = 9;
            SolverConstants.blockLength = 3;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.AreEqual(Board.BoardToString(result), "381952674249167853756483291578219346962534718134678529495326187627841935813795462");
        }

        [TestMethod()]
        public void NormalBoard21()
        {
            string boardString = "0000:=000000000?70050;01:00@90<8900800700004600=60:=080000070002=00030890>?500012;01@:000008007>00001000@0000900000<>?0740000000006@900000>0100002;0600=800<00500070002000000000<0900>?5;4020=0@0020=0@0<0907000>?500400=6000<803<0000002001@:0000=68000?0004020\r\n";
            SolverConstants.boardLength = 16;
            SolverConstants.blockLength = 4;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.AreEqual(Board.BoardToString(result), ";412:=6@3<8957>?7>?52;41:=6@93<893<8?57>12;46@:=6@:=<893>?57;412=6@:3<897>?52;412;41@:=693<8?57>57>?12;4@:=6893<893<>?57412;=6@::=6@93<857>?12;412;46@:=893<>?57?57>412;6@:=<893<8937>?5;412:=6@412;=6@:<8937>?5>?57;412=6@:3<893<8957>?2;41@:=6@:=6893<?57>412;");
        }

        [TestMethod()]
        public void NormalBoard22()
        {
            string boardString = "030000;062<000@0001;:620@80>700400020@8900035=109>0030000;0=0000=000620080003?05:00008000030000<0@000450;<0000090?4500000900>@0004000<:10>600870000000060008040=0200070050?010<0@873050?0:000200;<060000300000=020>070?0000500:60000501000;020000001006;00098030\r\n";
            SolverConstants.boardLength = 16;
            SolverConstants.blockLength = 4;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.AreEqual(Board.BoardToString(result), "73?4=1;562<:9>@85=1;:62<@89>73?4<:62>@89?4735=1;9>@83?471;5=<:62=1;<629:87>@3?45:629@87>453?=1;<>@87?453;<=1:6293?451;<=29:6>@87?45=;<:19>62@8731;<:29>673@8?45=629>873@5=?41;<:@87345=?<:1;629>;<:69>@23?8745=129>@73?8=145;<:6873?5=14:6;<29>@45=1<:6;>@29873?");
        }

        [TestMethod()]
        public void NormalBoard23()
        {
            string boardString = ">000?006;00=20000@000090700:0300300;080000096?0060000=;3000000<90>40000000300000820004<007600005;300@0020>40760:700:05=080009000076?00500820<0000900000:0=03080000209>40000000;30;002@00<0>0:700@002<0000?0000=0500000@040900:00000600350182000>0090:700300;0180\r\n";
            SolverConstants.boardLength = 16;
            SolverConstants.blockLength = 4;

            Cell[,] board = Board.BuildBoard(boardString);
            Cell[,] result = SudokuHeuristicsSolver.Solve(board);
            Assert.AreEqual(Board.BoardToString(result), ">4<9?:76;35=2@182@184<9>76?:;35=35=;182@>4<96?:76?:75=;32@18>4<99>4<6?:7=;3582@182@1>4<9:76?=;35;35=@1829>4<76?:76?:35=;82@19>4<:76?;35=182@<9>4<9>476?:5=;3182@182@9>4<?:765=;3=;352@18<9>4:76?@182<9>46?:735=;5=;382@14<9>?:76?:76=;35@1824<9>4<9>:76?35=;@182");
        }
    }
}