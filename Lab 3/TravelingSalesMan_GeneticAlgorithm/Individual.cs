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

        public void Mutate()
        {
            int index1 = random.Next(1, locations.Count - 3);
            int index2 = random.Next(index1 + 1, locations.Count - 2);

            int numberOfLocationsToMove = index2 - index1 + 1;
          
            for(int i = 0; i < numberOfLocationsToMove / 2; i++)
            {
                Swap(index1 + i, index2 - i);
            }
        }

        private void Swap(int index1, int index2)
        {
            Location temp = locations[index1];
            locations[index1] = locations[index2];
            locations[index2] = temp;
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
