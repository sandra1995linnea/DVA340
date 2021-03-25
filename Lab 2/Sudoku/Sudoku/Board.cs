using System;
using System.Collections.Generic;

namespace Sudoku
{
    public class Board
    {
        private readonly Tile[,] board;

        private readonly List<Tuple<int, int>> emptySpots = new List<Tuple<int, int>>();

        public Board(List<Tile> tiles)
        {
            board = new Tile[9, 9];

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    board[i, j] = tiles[0];
                    if(tiles[0].Number == 0) // empty
                    {
                        emptySpots.Add(new Tuple<int, int>(i, j));
                    }
                    tiles.RemoveAt(0);
                }
            }
        }

        /// <summary>
        /// Will return a list of all tiles that could be placed on a spot
        /// </summary>
        /// <param name="spot">A spot, i.e. a Tuple of row and column</param>
        /// <returns>List of tiles</returns>
        private List<Tile> PossibleTiles(Tuple<int, int> spot)
        {
            List<Tile> tiles = new List<Tile>();
            
            for(int number = 1; number <= 9; number++)
            {
                bool found = false;
                /* 1st: check the row from 0 to 9
                 * 2nd check the column from 0 to 9
                 * 3rd check the grid */

                // searching through the row:
                for(int j = 0; j < 9; j++)
                {
                    if(board[spot.Item1, j].Number == number)
                    {
                        found = true;
                        break;
                    }
                }
                if(found)
                {
                    continue;
                }

                // searching through the column
                for (int i = 0; i < 9; i++)
                {
                    if(board[i, spot.Item2].Number == number)
                    {
                        found = true;
                        break;
                    }
                }
                if(found)
                {
                    continue;
                }

                // searching through the mini grid
                int startrow;
                int startcolumn;

                if(spot.Item1 < 3)
                {
                    startrow = 0;
                }
                else if(spot.Item1 < 6)
                {
                    startrow = 3;
                }
                else
                {
                    startrow = 6;
                }

                if (spot.Item2 < 3)
                {
                    startcolumn = 0;
                }
                else if (spot.Item2 < 6)
                {
                    startcolumn = 3;
                }
                else
                {
                    startcolumn = 6;
                }

                // looping through the mini grid
                for(int i = startrow; i <= startrow + 2 && !found; i++)
                {
                    for(int j = startcolumn; j <= startcolumn + 2 && !found; j++)
                    {
                        if(board[i,j].Number == number)
                        {
                            found = true;
                        }
                    }
                }
                if (found)
                {
                    continue;
                }

                tiles.Add(new Tile(number));
            }

            return tiles;
        }
        
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

        public bool SolveSudoku()
        {
            if(emptySpots.Count == 0)
            {
                return true;
            }

            var emptySpot = emptySpots[0];
            emptySpots.RemoveAt(0);

            List<Tile> possibleTiles = PossibleTiles(emptySpot);
            if(possibleTiles.Count == 0)
            {
                // re-add the empty spot as actually empty before back-tracking:
                emptySpots.Insert(0, emptySpot);
                board[emptySpot.Item1, emptySpot.Item2] = new Tile(0);

                return false;
            }

            foreach(var possible in possibleTiles)
            {
                board[emptySpot.Item1, emptySpot.Item2] = possible;

                if(SolveSudoku())
                {
                    return true;
                }
            }
            // re-add the empty spot as actually empty before back-tracking:
            emptySpots.Insert(0, emptySpot);
            board[emptySpot.Item1, emptySpot.Item2] = new Tile(0);

            return false;
        }
    }
}
