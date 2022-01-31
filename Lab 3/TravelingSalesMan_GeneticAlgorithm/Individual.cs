using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelingSalesMan_GeneticAlgorithm
{
    class Individual
    {
        private double? totaldistnace = null;
        private readonly Random random;
        private List<Location> locations;
        internal List<Location> Locations => locations;

        /// <summary>
        /// Generates individual with given locations
        /// </summary>
        /// <param name="locations">Locations to be set</param>
        public Individual(List<Location> locations, Random random)
        {
            this.locations = locations;
            this.random = random;
        }

        /// <summary>
        /// Randomizes the DNA of the individual
        /// </summary>
        internal void Randomize()
        {
            List<Location> randomLocations = new List<Location>();
            int insertIndex;

            foreach (var location in locations)
            {
                insertIndex = random.Next(0, randomLocations.Count);
                randomLocations.Insert(insertIndex, location);
            }

            locations = randomLocations; 
        }

        //takes two individuals from the population and creates a new offspring
        public static Individual CrossoverAndMutate(Individual parent1, Individual parent2, Random random)
        {
            Location[] Genes = new Location[parent1.Locations.Count];

            //gives the intervall between the positions we can accept a random index
            int index1 = random.Next(1, parent1.Locations.Count - 3);
            //start from the first random index
            int index2 = random.Next(index1 + 1, parent1.Locations.Count - 2);

            for(int i = index1; i <= index2; i++)
            {
                //adds the elements from parent1 between the random indexes into the array
                Genes[i] = parent1.Locations[i];
            }

            /*
             * Loop through the array of genes and in a nested loop, loop through parent 2
             * Check if the element at the current index in parent 2 is the same as the elements already 
             * inserted into the array from parent1
             * Insert into the array of genes from parent2
             * */

            // gets all locations in parent2 that have not already been taken from parent 1
            var fromParent2 = parent2.Locations.FindAll(x => !Genes.Contains(x));

            for (int i = 0; i < index1; i++)
            {
                Genes[i] = fromParent2[i];
            }

            for(int i = 0; i < fromParent2.Count - index1; i++)
            {
                //adds the rest into the array from where we stoped
                Genes[i + index2 + 1] = fromParent2[i + index1];
            }

            Individual individual = new Individual(Genes.ToList(), random);
            individual.Mutate();
            return individual;
        }

        //select two random positions in the offspring and exchange the elements
        public void Mutate()
        {
            Random random = new Random();
            int index1 = random.Next(1, locations.Count - 3);
            int index2 = random.Next(index1 + 1, locations.Count - 2);

            //swapping the elemnts at the random indexes
            Location temp = locations[index1];
            locations[index1] = locations[index2];
            locations[index2] = temp;
        }


        // fitness function - the lower it returns, the better
        public double TotalDistance
        {
            get
            {
                if(totaldistnace == null)
                {
                    totaldistnace = 0;
                    Location previous = null;

                    foreach(var location in Locations)
                    {
                        if (previous != null)
                        {
                            totaldistnace += previous.DistanceTo(location);
                        }
                        previous = location;
                    }

                    //calculates the distance between the first and the last location
                    totaldistnace += Locations.First().DistanceTo(Locations.Last());
                }
                
                return (double)totaldistnace;
            }
        }

    }
}
