using System;
using System.Collections.Generic;

namespace The_traveling_sales_man
{
    class Population
    {
        private const int SIZE = 1000;
        private const int NUM_PARENTS = 500;
        private const int NUM_SURVIVERS = 200; // for elitism
        private const int NUM_MUTATIONS = 400;

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
            Individuals.RemoveRange(NUM_PARENTS, SIZE - NUM_PARENTS);
        }

        private void MutatePopulation(List<Individual> toMutate)
        {
            int index;
            Random random = new Random();

            for(int i = 0; i < NUM_MUTATIONS; i++)
            {
                index = random.Next(0, toMutate.Count);
                toMutate[index].Mutate();
            }            
        }

        private void RunMatingSeason()
        {
            Random random = new Random();
            List<Individual> children = new List<Individual>();

            while(children.Count + NUM_SURVIVERS < SIZE)
            {
                // choose two parents that will be allowed to mate:

                int parent1 = random.Next(0, Individuals.Count);
                for (int i = 0; i < Individuals.Count / 10; i++)
                {
                    int newIndex = random.Next(0, Individuals.Count);
                    if(Individuals[newIndex].TotalDistance < Individuals[parent1].TotalDistance)
                    {
                        parent1 = newIndex;
                    }
                }

                int parent2 = random.Next(0, Individuals.Count);
                for (int i = 0; i < Individuals.Count / 10; i++)
                {
                    int newIndex = random.Next(0, Individuals.Count);
                    if (Individuals[newIndex].TotalDistance < Individuals[parent1].TotalDistance && newIndex != parent1)
                    {
                        parent2 = newIndex;
                    }
                }

                Individual child1 = Individual.CrossOver(Individuals[parent1], Individuals[parent2]);
                children.Add(child1);
            }

            // remove parents that are not going to make it to the next generation
            Individuals.RemoveRange(NUM_SURVIVERS, Individuals.Count - NUM_SURVIVERS);

            // run mutation on only the children
            MutatePopulation(children);

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

                double total = 0;
                Individuals.ForEach(x => total += x.TotalDistance);

                Console.WriteLine("Best: " + Individuals[0].TotalDistance.ToString("N0") + "\tAverage: " + (total / Individuals.Count).ToString("N0"));

                RunMatingSeason();
            }
            Individuals.Sort(Individual.Compare);
            return false;
        }

        public Individual BestSolution => Individuals[0];

    }
}
