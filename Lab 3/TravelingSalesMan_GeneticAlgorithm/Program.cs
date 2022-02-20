using System;
using System.Collections.Generic;

namespace TravelingSalesMan_GeneticAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            var time = new System.Diagnostics.Stopwatch();
            const double MUTATION_PROBABILITY = 0.2;
            Graph graph = new Graph();
            List<Location> locations = graph.GenerateLocations();
            time.Start();
            Population population = new Population(locations, new Crossover(MUTATION_PROBABILITY));

            while(population.BestFittness > 9000)
            {
                population.NextGeneration();

                Console.WriteLine("Best fitness = " + population.BestFittness.ToString() + $"Time: {time.ElapsedMilliseconds} ms");
            }
            time.Stop();

            //print the best solution:

            Console.WriteLine("Best induvidial: ");
            Console.WriteLine("Press a button to exit");
            Console.ReadKey();
        }
    }
}
