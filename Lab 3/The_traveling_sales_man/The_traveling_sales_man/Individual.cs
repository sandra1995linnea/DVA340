using System;
using System.Collections.Generic;

namespace The_traveling_sales_man
{
    public class Individual
    {
        private double? totalDistance = null;

        public Individual(List<Location> locations)
        {
            Locations = locations;

            Random rng = new Random();
            int n = locations.Count;
            while (n > 2)
            { 
                n--;
                int k = rng.Next(n) + 1;
                Swap(n, k);
            }
        }

        private void Swap(int index1, int index2)
        {
            Location value = Locations[index1];
            Locations[index1] = Locations[index2];
            Locations[index2] = value;
        }

        public List<Location> Locations { private set; get; }

        public void Mutate()
        {
            Random random = new Random();

            int k = random.Next(Locations.Count - 1) + 1;
            int j = random.Next(Locations.Count - 1) + 1;

            Swap(k, j);

            totalDistance = null;
        }

        // Fitness function, the lower value it returns - the better
        public double TotalDistance
        {
            get
            {
                if (totalDistance == null)
                {
                    double totalDistance = 0;
                    Location previous = null;

                    foreach (var location in Locations)
                    {
                        if (previous != null)
                        {
                            totalDistance += Location.Distance(previous, location);
                        }
                        previous = location;
                    }
                    totalDistance += Location.Distance(Locations[Locations.Count - 1], Locations[0]);
                }
                
                return (double)totalDistance;
            }
        }


        public static void CrossOver(Individual parent1, Individual parent2, out Individual offspring1, out Individual offspring2)
        {
#warning "FIX CROSSOVER!"
            // TODO!
            offspring1 = parent1;
            offspring2 = parent2;
        }

        /// <summary>
        /// Compares two individuals
        /// </summary>
        /// <param name="individual1">One individual</param>
        /// <param name="individual2">The other individual</param>
        /// <returns>Returns negative number if individual1 is better than individual2, 
        /// positive number if individual1 is better than individual2, 0 if they are equal.</returns>
        public static int Compare(Individual individual1, Individual individual2)
        {
            if(individual1.TotalDistance < individual2.TotalDistance)
            {
                return -1;
            }
            else if(individual1.TotalDistance == individual2.TotalDistance)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
