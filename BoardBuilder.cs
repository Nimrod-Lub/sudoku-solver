﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class BoardBuilder
{
    public static int[,] BuildBoard(string input)
    {
        int boardLength = (int)Math.Sqrt(input.Length); // Sqrt is an integer if passed validity check
        int[,] sudokuBoard = new int[boardLength, boardLength];

        for (int i = 0; i < boardLength; i++)
        {
            for (int j = 0; j < boardLength; j++)
            {
                sudokuBoard[i, j] = input.ElementAt(i * boardLength + j);
            }
        }
        return sudokuBoard;
    }


    //public Board(string input) 
    //{
    //    int boardLength = (int)Math.Sqrt(input.Length); // Sqrt is an integer if passed validity check
    //    board = new int[boardLength,boardLength];

    //    for (int i = 0; i < boardLength; i++)
    //    {
    //        for (int j = 0; j < boardLength; j++)
    //        {
    //            board[i, j] = input.ElementAt(i * boardLength + j);
    //        }
    //    }
    //} 


}
