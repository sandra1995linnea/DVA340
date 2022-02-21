using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelingSalesMan_AntColony
{
    class PheremoneHandler
    {
        /// <summary>
        /// The proportion of the pheremone that evaporates each timestep
        /// </summary>
        private const double evaporationProportion = 0.1;

        private List<Location> locations;

        /// <summary>
        /// stores the amount of pheremone between pairs of locations (edges)
        /// </summary>
        private Dictionary<Tuple<Location, Location>, double> pheremones = new Dictionary<Tuple<Location, Location>, double>();

        public PheremoneHandler(List<Location> locations)
        {
            this.locations = locations;
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
                var previous = enumerator;
                enumerator.MoveNext();

                do
                {
                    var r = previous.Current;
                    var s = enumerator.Current;

                    

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
            //if(vivited == null)
            //{
            //    return 0;
            //}
            // TODO

            return 1;
        }
    }
}
