using System;
using System.Collections.Generic;

namespace The_traveling_sales_man
{
    class Program
    {
        static void Main(string[] args)
        {
            var myGraph = new Graph();
            var allLocations = myGraph.GenerateLocations();

            var population = new Population(allLocations);

            bool success = population.RunEvolution(10000, 9000);

            

        }
    }
}
