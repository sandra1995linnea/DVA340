using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            // TODO print path

            double shortestDistance = colony.ShortestDistance;

            // print distance
        }
    }
}
