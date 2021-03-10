using System;
using System.Collections.Generic;

namespace Spain_map
{
    public class City
    {
        public City(string name, int distance)
        {
            Name = name;
            DistanceToGoal = distance;
        }

        public string Name { get; }
        public int DistanceToGoal { get; }
        public List<Road> Roads { get; private set; } = new List<Road>();
        public List<City> AdjacentCities
        {
            get
            {
                List<City> cities = new List<City>();

                foreach(var road in Roads)
                {
                    if(this == road.ConnectionCity1)
                    {
                        cities.Add(road.ConnectionCity2);
                    }
                    else
                    {
                        cities.Add(road.ConnectionCity1);
                    }
                }
                return cities;
            }
        }

       public List<Tuple<int, City>> AdjacentCityDistance
        {
            get
            {
                List<Tuple<int, City>> CityDistance = new List<Tuple<int, City>>();

                foreach(var road in Roads)
                {
                    if(this == road.ConnectionCity1)
                    {
                        CityDistance.Add(new Tuple<int, City>(road.Distance, road.ConnectionCity2));
                    }
                    else
                    {
                        CityDistance.Add(new Tuple<int, City>(road.Distance, road.ConnectionCity1));
                    }

                }

                return CityDistance;
            }
        }
    }
}
