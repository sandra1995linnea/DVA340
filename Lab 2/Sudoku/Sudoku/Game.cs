using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sudoku
{
    public class Game
    {
        public Game()
        {
            
        }

        public void Run()
        {
            List<Board> boards = GenerateBoards();
            
        }

        public void SolveSudoku()
        {

        }

        public List<Board> GenerateBoards()
        {
            List<Board> boards = new List<Board>();

            string name = "F:/Mälardalens Högskola/DVA340/Lab 2/Sudoku/sudoku.txt";
            string[] lines = File.ReadAllLines(name);
            int i = 0;

            while (i < lines.Length)
            {
                while (true)
                {
                    if(i >= lines.Length)
                    {
                        return boards;
                    }

                    if (lines[i].StartsWith("SUDOKU"))
                    {
                        break;
                    }
                    i++;
                }
                i++;

                List<Tile> tiles = new List<Tile>();
                for (int j = i; j < i + 9; j++)
                {
                    var line = lines[j];
                    for (int k = 0; k < 9; k++)
                    {
                        tiles.Add(new Tile(line[k]));
                    }
                }
                i += 10;

                boards.Add(new Board(tiles));
            }

            return boards;
        }
    }
}
