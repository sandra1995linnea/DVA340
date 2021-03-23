using System;
using System.Collections.Generic;

namespace Sudoku
{
    public class Board
    {
        readonly Tile[,] board;

        private List<Tuple<int, int>> emptyTiles;

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

            // TODO fill emptyTiles
        }

        /*private List<Tile> PossibleTiles(Tuple<int, int> spot)
        {

        }
        */
        public void Print()
        {
            // TODO!
            Console.WriteLine("+-----+-----+-----+-----+-----+-----+");
            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    board[i, j].Print();
                   
                    if (j == 2 || j == 5)
                    {
                        Console.Write("  |");
                    }
                }

                if (i == 2 || i == 5)
                {
                    Console.WriteLine("");
                    Console.Write("------------------------------------");
                }

                Console.WriteLine("");
            }
        }

        public void SolveSudoku()
        {
            //var emptyTile = emptyTiles[0];
            //emptyTiles.RemoveAt(0);


        }
    }
}
