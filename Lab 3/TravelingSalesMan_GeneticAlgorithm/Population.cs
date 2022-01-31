using System;
using System.Collections.Generic;

namespace TravelingSalesMan_GeneticAlgorithm
{
    class Population
    {
        private const int POPULATION_SIZE = 100;
        private const int NUMBER_OF_PARENTS = 20;
        private List<Individual> individuals;
        private readonly Random random;

        /// <summary>
        /// The fitness value (total distance) of the best individual
        /// </summary>
        public double BestFittness { get; private set; } = double.MaxValue;

        /// <summary>
        /// Generates an initial population of random individuals
        /// </summary>
        public Population(List<Location> locations)
        {
            random = new Random();
            individuals = new List<Individual>();

            for (int i = 0; i < POPULATION_SIZE; i++)
            {
                var individual = new Individual(locations, random);
                individual.Randomize();
                individuals.Add(individual);
            }
        }

        //select the two best individuals and make them parents
        private List<Individual> Selection()
        {
            // sorting the population from best to worst
            individuals.Sort(Compare);

            // grab the best NUMBER_OF_PARENTS individuals and put them in the 'parents' list:
            List<Individual> parents = individuals.GetRange(0, NUMBER_OF_PARENTS);

            // update best fitness, easy since the parents are sorted
            BestFittness = parents[0].TotalDistance;

            return parents;
        }

        /// <summary>
        /// compares two induvidials totaldistance
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int Compare(Individual x, Individual y)
        {
            return (int)(x.TotalDistance - y.TotalDistance);
        }

        public void NextGeneration()
        {
            int index1, index2;
            List<Individual> nextGeneration = new List<Individual>();

            List<Individual> parents = Selection();

            // Create new generation from the parents
            for (int i = 0; i < POPULATION_SIZE; i++)
            {
                index1 = random.Next(0, parents.Count - 1);
                index2 = random.Next(0, parents.Count - 1);

                // ensure that we take two diffrent parents
                while (index1 == index2)
                {
                    index2 = random.Next(0, parents.Count - 1);
                }

                nextGeneration.Add(Individual.CrossoverAndMutate(parents[index1], parents[index2], random));
            }
            individuals = nextGeneration;
        }
        
    }
}
