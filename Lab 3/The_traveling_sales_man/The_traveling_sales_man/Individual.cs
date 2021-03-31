using System;
using System.Collections.Generic;

namespace The_traveling_sales_man
{
    public class Individual
    {
        private double? totalDistance = null;

        public Individual(List<Location> locations)
        {
            Locations = new List<Location>(locations);

            Random rng = new Random();
            int n = locations.Count;
            while (n > 2)
            { 
                n--;
                int k = rng.Next(n) + 1;
                Swap(n, k);
            }
        }

        internal void Print()
        {
            //print all id:s in the order we shall choose them
            Console.WriteLine("ID:s for visited locations:");
            Console.WriteLine("");
            foreach (var location in Locations)
            {
                Console.WriteLine(location.Id);
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
                    totalDistance = 0;
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

        public static Individual CrossOver(Individual parent1, Individual parent2)
        {
            List<Location> childsLocations = new List<Location>();
            List<Location> fromParent1 = new List<Location>();
            List<Location> fromParent2 = new List<Location>();
            Random random = new Random();

            int index1 = random.Next(0, parent1.Locations.Count - 1);
            int index2 = random.Next(index1 + 1, parent1.Locations.Count);
            
            // adds the index to a temp list
            for(int i = index1; i != index2; i++)
            {
                fromParent1.Add(parent1.Locations[i]);
            }
            // taking all IDs in parent 2 that has not been chosen from parent 1
            foreach (var parent2location in parent2.Locations)
            {
                if(!fromParent1.Contains(parent2location))
                {
                    fromParent2.Add(parent2location);
                }
            }

            for(int i = 0; i < index1; i++)
            {
                childsLocations.Add(fromParent2[0]);
                fromParent2.RemoveAt(0);
            }

            childsLocations.AddRange(fromParent1);
            childsLocations.AddRange(fromParent2);

            if(childsLocations.Count != parent1.Locations.Count || childsLocations.Count != parent2.Locations.Count)
            {
                throw new Exception("Something wrong!");
            }

            return new Individual(childsLocations);
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
