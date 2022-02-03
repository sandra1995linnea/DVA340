using System;
using System.Linq;

namespace TravelingSalesMan_GeneticAlgorithm
{
    class Crossover : ICrossover
    {
        private readonly double mutationProbability;

        public Crossover(double mutationProbability)
        {
            this.mutationProbability = mutationProbability;
        }

        public Individual CrossoverAndMutate(Individual parent1, Individual parent2, Random random)
        {
            Location[] Genes = new Location[parent1.Locations.Count];

            //gives the intervall between the positions we can accept a random index
            int index1 = random.Next(1, parent1.Locations.Count - 3);
            //start from the first random index
            int index2 = random.Next(index1 + 1, parent1.Locations.Count - 2);

            for (int i = index1; i <= index2; i++)
            {
                //adds the elements from parent1 between the random indexes into the array
                Genes[i] = parent1.Locations[i];
            }

            // gets all locations in parent2 that have not already been taken from parent 1
            var fromParent2 = parent2.Locations.FindAll(x => !Genes.Contains(x));

            for (int i = 0; i < index1; i++)
            {
                Genes[i] = fromParent2[i];
            }

            for (int i = 0; i < fromParent2.Count - index1; i++)
            {
                //adds the rest into the array from where we stoped
                Genes[i + index2 + 1] = fromParent2[i + index1];
            }

            Individual individual = new Individual(Genes.ToList(), random);
            if (ShouldMutate(random))
            {
                individual.Mutate();
            }


            return individual;
        }

        private bool ShouldMutate(Random random)
        {
            float randomnumber = random.Next(0, 1);
            return randomnumber < mutationProbability;
        }
    }
}
