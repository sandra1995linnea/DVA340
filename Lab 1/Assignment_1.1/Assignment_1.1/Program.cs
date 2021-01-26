using System;
using System.Collections.Generic;

namespace Assignment_1._1
{
    class Program
    {
        static void ReadItems(string file, out List<Item> items, out int limit)
        {

            limit = 0; // TODO!
            items = new List<Item>(); // TODO!
        }

        static void Main(string[] args)
        {
            string name = "F:/Mälardalens Högskola/DVA340/Lab 1/knapsack.txt";
            string file = System.IO.File.ReadAllText(name);

            List<Item> items;
            int limit;

            ReadItems(file, out items, out limit);

            Tree myTree = new Tree(items, limit);



            Console.WriteLine("Hello World!");
        }
    }
}
