using System;

namespace Sudoku
{
    public class Tile
    {
        public int Number { get; }

        public Tile(char number)
        {
            Number = Convert.ToInt32(number) - 48;
        }

        public void Print()
        {
            Console.Write("{0,3}", Number);
        }
    }
}
