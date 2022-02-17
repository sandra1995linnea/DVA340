using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelingSalesMan_AntColony
{
    class PheremoneHandler
    {
        private List<Location> locations;
        /// <summary>
        /// stores the amount of pheremone between pairs of locations (edges)
        /// </summary>
        private Dictionary<Tuple<Location, Location>, float> pheremones = new Dictionary<Tuple<Location, Location>, float>();

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
            foreach(var ant in allAnts)
            {
              //  pheremones.TryGetValue()
                // do magic!
            }
        }

        /// <summary>
        /// Returns how much phermone there is between two locations.
        /// </summary>
        /// <param name="location1"></param>
        /// <param name="location2"></param>
        /// <returns></returns>
        public float Pheremone(Location location1, Location location2)
        {
            //if(vivited == null)
            //{
            //    return 0;
            //}
            // TODO

            return 0;
        }
    }
}
