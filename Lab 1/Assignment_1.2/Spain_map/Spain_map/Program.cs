using System;
using System.Collections.Generic;
using System.IO;

namespace Spain_map
{
    class Program
    {
        static void Main()
        {
            string name = "F:/Mälardalens Högskola/DVA340/Lab 1/Spain_map.txt";
            string[] lines = File.ReadAllLines(name);
            string[] subStrings;
            int i = 0;
            List<Edge> edges = new List<Edge>();
            List<City> cities = new List<City>();

            while (true)
            {
                if (lines[i].StartsWith("A B Distance"))
                {
                    break;
                }
                i++;
            }
            i++;

            for (; i < lines.Length; i++)
            {
                if (string.Compare("", lines[i]) == 0)
                {
                    break;
                }

                subStrings = lines[i].Replace('\t', ' ').Split(' ');
                edges.Add(new Edge(subStrings[0], subStrings[1], int.Parse(subStrings[2])));
            }
            i += 3;

            for (; i < lines.Length; i++)
            {
                subStrings = lines[i].Replace('\t', ' ').Split(' ');
                cities.Add(new City(subStrings[0], int.Parse(subStrings[1])));
            }

            Graph mygraph = new Graph(cities);

            foreach (var edge in edges)
            {
                mygraph.ConnectCities(edge.City1, edge.City2, edge.Distance);
            }

            Console.WriteLine("Searching...");

            //mygraph.GreedyBestFirst("Malaga");
            mygraph.Astar("Malaga");

            Console.WriteLine("");
            Console.WriteLine("End");
        }
    }
}
