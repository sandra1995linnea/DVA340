﻿using System;
using System.Collections.Generic;

namespace The_traveling_sales_man
{
    class Population
    {
        private const int SIZE = 100;
        private const int NUM_SURVIVERS = 50;
        private const int NUM_MUTATIONS = 2;

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

                MutatePopulation();
            }
            return false;
        }
    }
}
