using System;
using System.Collections.Generic;

namespace Assignment_1._1
{
    class Program
    {
        private const string WEIGHT_IDENTIFIER = "MAXIMUM WEIGHT:";

        static void ReadItems(string[] file, out List<Item> items, out int limit)
        {
            // Read limit:
            limit = 0;
            foreach(var line in file)
            {
                if(line.StartsWith(WEIGHT_IDENTIFIER))
                {
                    // first get just the number, skip all letters mentioned in WEIGHT_IDENTIFIER
                    string numberAsString = line.Substring(WEIGHT_IDENTIFIER.Length);

                    // Then parse to int:
                    limit = (int)Double.Parse(numberAsString);
                    
                    break; // stop looking for the limit
                }
            }

            // read items:
            int i, id, benefit, weight;

            for (i = 0; i < file.Length; i++)
            {
                if (file[i].StartsWith("ID b w"))
                {
                    break;
                }
            }

            items = new List<Item>();
            for (int j = i + 1; i < file.Length; j++)
            {
                if(file[j].StartsWith("EOF"))
                {
                    break;
                }
                ;
                string[] values = file[j].Split(' ');
                id = (int)Double.Parse(values[0]);
                benefit = (int)Double.Parse(values[1]);
                weight = (int)Double.Parse(values[2]);
                items.Add(new Item(id, benefit, weight));
            }
        }

        static void Main(string[] args)
        {
            string name = "F:/Mälardalens Högskola/DVA340/Lab 1/knapsack.txt";
            string[] file = System.IO.File.ReadAllLines(name);

            Console.WriteLine("Searching...");

            List<Item> items;
            int limit;

            ReadItems(file, out items, out limit);

            Tree myTree = new Tree(items, limit);

            Node best = myTree.Breadth_First_Search();
            //Node best = myTree.Depth_First_Search();

            Console.WriteLine("Best path");
            foreach(var item in best.ItemsTaken)
            {
                Console.WriteLine("Item ID " + item.Id.ToString());
            }
            Console.WriteLine("Total benifit: " + best.TotalBenifit);
            Console.WriteLine("Total weight: " + best.TotalWeight);
        }
    }
}
