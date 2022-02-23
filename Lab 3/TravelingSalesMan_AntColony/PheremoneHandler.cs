using System;
using System.Collections.Generic;

namespace TravelingSalesMan_AntColony
{
    class PheremoneHandler
    {
        /// <summary>
        /// The proportion of the pheremone that evaporates each timestep
        /// </summary>
        private const double evaporationProportion = 0.2;
        private const double start_pheremone = 10.0;


        /// <summary>
        /// stores the amount of pheremone between pairs of locations (edges)
        /// </summary>
        private Dictionary<Tuple<Location, Location>, double> pheremones = new Dictionary<Tuple<Location, Location>, double>();

        public PheremoneHandler()
        {
        }

        /// <summary>
        /// update pheremone between two cities
        /// </summary>
        /// <param name="allAnts"></param>
        public void Update(List<Ant> allAnts)
        {
            EvaporatePheremone();

            foreach(var ant in allAnts)
            {
                var enumerator = ant.Visited.GetEnumerator();
                enumerator.MoveNext();

                var previous = enumerator;
                enumerator.MoveNext();

                do
                {
                    var r = previous.Current;
                    var s = enumerator.Current;
                    double currentPheremone;
                
                    if(!pheremones.TryGetValue(new Tuple<Location, Location>(r, s), out currentPheremone))
                    {
                        currentPheremone = start_pheremone;
                    }

                    currentPheremone += 1 / ant.TotalDistance;
                    pheremones[new Tuple<Location, Location>(r, s)] = currentPheremone;

                    previous = enumerator;
                } while (enumerator.MoveNext());
            }
        }

        /// <summary>
        /// Simulates the pheremone evaporating
        /// </summary>
        private void EvaporatePheremone()
        {
            Dictionary<Tuple<Location, Location>, double> new_pheremones = new Dictionary<Tuple<Location, Location>, double>();
            double newValue; 
            foreach(var pheremone in pheremones)
            {
                newValue = (1 - evaporationProportion) * pheremone.Value;
                new_pheremones.Add(pheremone.Key, newValue);
            }
            pheremones = new_pheremones;
        }

        /// <summary>
        /// Returns how much phermone there is between two locations.
        /// </summary>
        /// <param name="location1"></param>
        /// <param name="location2"></param>
        /// <returns></returns>
        public double Pheremone(Location location1, Location location2)
        {
            double val;
            
            if (!pheremones.TryGetValue(new Tuple<Location, Location>(location1, location2), out val))
            {
                val = start_pheremone;
            }

            return val;
        }
    }
}
