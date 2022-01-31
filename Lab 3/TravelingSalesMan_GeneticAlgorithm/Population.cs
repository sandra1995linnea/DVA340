using System;
using System.Collections.Generic;

namespace TravelingSalesMan_GeneticAlgorithm
{
    class Population
    {
        private const int POPULATION_SIZE = 100;
        private const int NUMBER_OF_PARENTS = 20;
        private List<Individual> individuals;

        /// <summary>
        /// The fitness value (total distance) of the best individual
        /// </summary>
        public double BestFittness { get; private set; } = double.MaxValue;

        /// <summary>
        /// Generates an initial population of random individuals
        /// </summary>
        public Population(List<Location> locations)
        {
            individuals = new List<Individual>();

            for (int i = 0; i < POPULATION_SIZE; i++)
            {
                var individual = new Individual(locations);
                individual.Randomize();
                individuals.Add(individual);
            }
        }

        //select the two best individuals and make them parents
        private List<Individual> Selection()
        {
            List<Individual> parents = new List<Individual>();



            /////////////////////
            ///
            individuals.Sort(Compare);

            ///////////////

            foreach (var individual in individuals)
            {
                if(parents.Count < NUMBER_OF_PARENTS)
                {
                    parents.Add(individual);

                    //sorting the parents from worst to best
                    parents.Sort(Compare);
                }
                else
                {
                    // TODO correct this algoritm!
                    foreach(var parent in parents)
                    {
                        //sorting the parents from worst to best
                        parents.Sort(Compare);

                        if(parent.TotalDistance > individual.TotalDistance)
                        {
                            //check which of the parents that has the worst distance, replace
                            parents.Remove(parent);
                            parents.Add(individual);
                            break;
                        }
                    }
                }
            }
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
            Random random = new Random();
            int index1, index2;
            List<Individual> nextGeneration = new List<Individual>();

            List<Individual> parents = Selection(); // TODO: Couldn't Selection() also set BestFittness?

            // find best fitness:
            foreach (var parent in parents)
            {
                if(parent.TotalDistance < BestFittness)
                {
                    BestFittness = parent.TotalDistance;
                }
            }

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

                nextGeneration.Add(Individual.CrossoverAndMutate(parents[index1], parents[index2]));
            }
            individuals = nextGeneration;
        }
        
    }
}
