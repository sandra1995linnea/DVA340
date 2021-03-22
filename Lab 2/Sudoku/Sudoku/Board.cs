using System;
using System.Collections.Generic;

namespace Sudoku
{
    public class Board
    {
        readonly Tile[,] board;

        public Board(List<Tile> tiles)
        {
            board = new Tile[9, 9];

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    board[i, j] = tiles[0];
                    tiles.RemoveAt(0);
                }
            }
        }        
    }
}
