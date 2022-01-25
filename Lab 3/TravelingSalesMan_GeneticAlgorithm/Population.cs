using System.Collections.Generic;

namespace TravelingSalesMan_GeneticAlgorithm
{
    class Population
    {
        private const int POPULATION_SIZE = 100;

        private List<Individual> individuals;

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
        public List<Individual> Selection()
        {
            List<Individual> parents = new List<Individual>();

            foreach (var individual in individuals)
            {
                if(parents.Count < 2)
                {
                    parents.Add(individual);
                }
                else
                {
                    foreach(var parent in parents)
                    {
                        if(parent.TotalDistance > individual.TotalDistance)
                        {
                            //check which of the parents that has the worst distance, replace
                            parents.Remove(parent);
                            parents.Add(individual);
                        }
                    }
                }
            }
            return parents;
        }

        private void MakeGenerations()
        {
            List<Individual> nextGeneration = new List<Individual>();
            List<Individual> parents = Selection();
            
            for(int i = 0; i < POPULATION_SIZE; i++)
            {
                nextGeneration.Add(Individual.Crossover(parents[0], parents[1]));
            }
            individuals = nextGeneration;
        }
        
        public void RunEvolution()
        {
            
        }
    }
}
