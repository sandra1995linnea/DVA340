using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelingSalesMan_GeneticAlgorithm
{
    class Individual
    {
        private double? totalDistance = null;
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

        public double FitnessValue => 1 / TotalDistance;

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

        //select two random positions in the offspring and exchange the elements
        public void Mutate()
        {
            Random random = new Random();
            int index1 = random.Next(1, locations.Count - 3);
            //int index2 = random.Next(index1 + 1, locations.Count - 2);

            //swapping the elemnts at the random indexes
            Location temp = locations[index1];
            locations[index1] = locations[index1 - 1];
            locations[index1 - 1] = temp;
        }

        // fitness function - the lower it returns, the better
        public double TotalDistance
        {
            get
            {
                if(totalDistance == null)
                {
                    totalDistance = 0;
                    Location previous = null;

                    foreach(var location in Locations)
                    {
                        if (previous != null)
                        {
                            totalDistance += previous.DistanceTo(location);
                        }
                        previous = location;
                    }

                    //calculates the distance between the first and the last location
                    totalDistance += Locations.First().DistanceTo(Locations.Last());
                }
                
                return (double)totalDistance;
            }
        }
    }
}
