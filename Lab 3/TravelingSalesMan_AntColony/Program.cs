using System;
using System.Collections.Generic;

namespace TravelingSalesMan_AntColony
{
    class Program
    {
        
        static void Main(string[] args)
        {
            
            Graph graph = new Graph();
            List<Location> locations = graph.GenerateLocations();

            Colony colony = new Colony(locations, 9000);

            List<Location> bestPath = colony.ShortestPath;

            

            double shortestDistance = colony.ShortestDistance;

            // print distance
            Console.WriteLine("Press a key: ");
            Console.ReadLine();
        }
    }
}
