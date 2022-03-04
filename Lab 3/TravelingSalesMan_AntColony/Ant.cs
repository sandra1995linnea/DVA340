using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelingSalesMan_AntColony
{
    class Ant : IAnt
    {
        private double? totaldistance = null;
        private readonly PheremoneHandler pheremoneHandler;
        private readonly Random random;
        private const double alpha = 10;
        private const double beta = 10;

        /// <summary>
        /// A list of all locations that exist
        /// </summary>
        private readonly List<Location> locations;

        public Ant(List<Location> locations, PheremoneHandler pheremoneHandler, Random random)
        {
            this.locations = locations;
            this.pheremoneHandler = pheremoneHandler;
            Visited = new List<Location>();
            this.random = random;
        }

        /// <summary>
        /// Lets the ant run around and find a path through all locations
        /// </summary>
        public void Run()
        {
            var leftToVisit = new List<Location>(locations);
            Visited.Clear();

            // visit the first location
            GoToNextCity(leftToVisit, 0);

            while (leftToVisit.Count > 0)
            {
                // let the ant choose where to go next, and go there

                // r = Visited.Last()
                // s each element of leftToVisit

                // Visited.Last() is always the location the ant is at = r in the lecture notes
                double[] probabilites = Probabilites(Visited.Last(), leftToVisit);

                int index = ChooseIndex(probabilites);

                // go to a given location:
                GoToNextCity(leftToVisit, index);
            }
        }

        private void GoToNextCity(List<Location> leftToVisit, int index)
        {
            Visited.Add(leftToVisit[index]);
            leftToVisit.RemoveAt(index);
        }

        /// <summary>
        /// Randomly chooses an index with some probabilites.
        /// </summary>
        /// <param name="probabilites">The probabilites of choosing a given index</param>
        /// <returns>The chosen index</returns>
        private int ChooseIndex(double[] probabilites)
        {
            double randomProbability = random.NextDouble();
            double sum0fprobabilities = 0.0;

            for (int i = 0; i < probabilites.Length; i++)
            {
                sum0fprobabilities += probabilites[i];
                if (sum0fprobabilities >= randomProbability)
                {
                    return i;
                }
            }

            return probabilites.Length - 1;
        }

        /// <summary>
        /// Calculates the probabilites of each possible location.
        /// </summary>
        /// <param name="currentLocation">Where the ant is</param>
        /// <param name="possibleLocations">All locations the ant could go to</param>
        /// <returns></returns>
        private double[] Probabilites(Location currentLocation, List<Location> possibleLocations)
        {
            double[] probabilities = new double[possibleLocations.Count];
            double[] benifits = new double[possibleLocations.Count];

            double sum0fBenifits = 0.0;
            for (int i = 0; i < possibleLocations.Count; i++)
            {
                benifits[i] = Benifit(currentLocation, possibleLocations[i]);
                sum0fBenifits += benifits[i];
            }

            for (int i = 0; i < possibleLocations.Count; i++)
            {
                probabilities[i] = benifits[i] / sum0fBenifits;
            }

            return probabilities;
        }

        /// <summary>
        /// Calculates the benifit of going from location r to location s.
        /// </summary>
        /// <param name="r">The location the ant is at</param>
        /// <param name="s">The other location</param>
        /// <returns></returns>
        private double Benifit(Location r, Location s)
        {
            double distance = r.DistanceTo(s);
            double pheremone = pheremoneHandler.GetPheremone(r, s);

            pheremone = Math.Pow(pheremone, alpha);
            distance = Math.Pow(distance, beta);

            return pheremone * (1 / distance);
        }

        /// <summary>
        /// The locations in the order the ant visited them.
        /// The actual solution that the ant represents.
        /// </summary>
        public List<Location> Visited { get; private set; }

        public double TotalDistance
        {
            get
            {
                if (totaldistance == null)
                {
                    totaldistance = 0;
                    Location previous = null;

                    foreach (var location in Visited)
                    {
                        if (previous != null)
                        {
                            totaldistance += previous.DistanceTo(location);
                        }
                        previous = location;
                    }

                    totaldistance += Visited.First().DistanceTo(Visited.Last());
                }

                return (double)totaldistance;
            }
        }
    }
}
