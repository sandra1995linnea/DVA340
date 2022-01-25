using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelingSalesMan_GeneticAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            List<Location> locations = graph.GenerateLocations();
            Population population = new Population(locations);
            population.Selection();

        }
    }
}
