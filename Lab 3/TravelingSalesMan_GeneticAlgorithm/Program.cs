using System;
using System.Collections.Generic;

namespace TravelingSalesMan_GeneticAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            List<Location> locations = graph.GenerateLocations();
            Population population = new Population(locations);

            while(population.BestFittness > 9000)
            {
                population.NextGeneration();

                Console.WriteLine("Best fitness = " + population.BestFittness.ToString());
            }

            //print the best solution:

            Console.WriteLine("Best induvidial: ");
        }
    }
}
