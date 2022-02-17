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
        List<Location> locations = new List<Location>();
        List<Location> visited;
        internal List<Location> Locations => locations;


        public double TotalDistance 
        { 
            get
            {
                if(totaldistance == null)
                {
                    totaldistance = 0;
                    Location previous = null;

                    foreach(var location in locations)
                    {
                        if(previous != null)
                        {
                            totaldistance += previous.DistanceTo(location);
                        }
                        visited.Add(previous);
                        previous = location;
                    }

                    totaldistance += Locations.First().DistanceTo(Locations.Last());
                }

                return (double)totaldistance;
            }
        }
    }
}
