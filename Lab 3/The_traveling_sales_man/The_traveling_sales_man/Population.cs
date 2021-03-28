using System;
using System.Collections.Generic;

namespace The_traveling_sales_man
{
    class Population
    {
        private const int SIZE = 100;
        private const int NUM_SURVIVERS = 50;
        private const int NUM_MUTATIONS = 10;

        public List<Individual> Individuals { get; private set; }

        public Population(List<Location> locations)
        {
            Individuals = new List<Individual>();

            for (int i = 0; i < SIZE; i++)
            {
                Individuals.Add(new Individual(locations));
            }
        }

        /// <summary>
        /// Removes SIZE - NUM_SURVIVERS of the least fit individuals from the population.
        /// </summary>
        private void Selection()
        {
            Individuals.Sort(Individual.Compare);
            Individuals.RemoveRange(SIZE - NUM_SURVIVERS, SIZE - NUM_SURVIVERS);
        }

        private void MutatePopulation()
        {
            int index;
            Random random = new Random();

            for(int i = 0; i < NUM_MUTATIONS; i++)
            {
                index = random.Next(0, Individuals.Count);
                Individuals[index].Mutate();
            }            
        }

        private void RunMatingSeason()
        {
            Random random = new Random();
            List<Individual> children = new List<Individual>();

            while(Individuals.Count + children.Count < SIZE)
            {
                int parent1 = random.Next(0, Individuals.Count);
                int parent2 = random.Next(0, Individuals.Count); // TODO ensure that parent1 != parent2

                Individual child1, child2;
                Individual.CrossOver(Individuals[parent1], Individuals[parent2], out child1, out child2);

                children.Add(child1);
                children.Add(child2);
            }

            Individuals.AddRange(children);
        }

        /// <summary>
        /// The actual Genetic Algorithm
        /// </summary>
        /// <param name="maxGenerations"></param>
        /// <param name="fitnessGoal"></param>
        /// <returns>Return true if the fitness goal was met</returns>
        public bool RunEvolution(int maxGenerations, double fitnessGoal)
        {
            for(int i = 0; i < maxGenerations; i++)
            {
                Selection();

                if (Individuals[0].TotalDistance < fitnessGoal)
                {
                    return true;
                }

                RunMatingSeason();
                MutatePopulation();
            }
            return false;
        }
    }
}
