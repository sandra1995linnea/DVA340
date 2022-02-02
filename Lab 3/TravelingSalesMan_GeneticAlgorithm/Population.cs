using System;
using System.Collections.Generic;

namespace TravelingSalesMan_GeneticAlgorithm
{
    class Population
    {
        private const int POPULATION_SIZE = 100;
        private const int NUMBER_OF_PARENTS = 40;
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

        //select the best individuals
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
            List<double> ProbabilityList = new List<double>();
            double probability = 0.0;
            double sum0fFitness = 0.0;

            List<Individual> parents = Selection();

            foreach(var parent in parents)
            {
                sum0fFitness += parent.FitnessValue;
            }

            foreach(var parent in parents)
            {
                probability = parent.FitnessValue / sum0fFitness;
                ProbabilityList.Add(probability);
            }

            // Create new generation from the parents
            for (int i = 0; i < POPULATION_SIZE; i++)
            {
                ChooseParentIndices(out index1, out index2, ProbabilityList, random);
                
                nextGeneration.Add(Individual.CrossoverAndMutate(parents[index1], parents[index2], random));
            }
            individuals = nextGeneration;
        }

        // choose two indexes
        private void ChooseParentIndices(out int index1, out int index2, List<double> ProbabilityList, Random random)
        {   
            index1 = ChooseIndex(ProbabilityList, random);

            do
            {
                index2 = ChooseIndex(ProbabilityList, random);
            } while (index1 == index2);
        }

        private int ChooseIndex(List<double> probabilityList, Random random)
        {
            double r = random.NextDouble();
            double probability_sum = 0.0;

            for (int i = 0; i < probabilityList.Count; i++)
            {
                probability_sum += probabilityList[i];
                if(probability_sum >= r)
                {
                    return i;
                }
            }

            return probabilityList.Count - 1;
        }
    }
}
