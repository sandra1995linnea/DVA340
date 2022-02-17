using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelingSalesMan_AntColony
{
    class Ant
    {
        private double? totaldistance = null;
        private PheremoneHandler pheremoneHandler;
                
        /// <summary>
        /// A list of all locations that exist
        /// </summary>
        private readonly List<Location> locations;

        public Ant(List<Location> locations, PheremoneHandler pheremoneHandler)
        {
            this.locations = locations;
            this.pheremoneHandler = pheremoneHandler;
        }

        /// <summary>
        /// Lets the ant run around and find a path through all locations
        /// </summary>
        public void Run()
        {

            // DO the magic!
        }

        /// <summary>
        /// The locations in the order the ant visited them.
        /// The actual solution that the ant represents.
        /// </summary>
        internal List<Location> Visited { get; private set; }

        public double TotalDistance 
        { 
            get
            {
                if(totaldistance == null)
                {
                    totaldistance = 0;
                    Location previous = null;

                    foreach(var location in Visited)
                    {
                        if(previous != null)
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
