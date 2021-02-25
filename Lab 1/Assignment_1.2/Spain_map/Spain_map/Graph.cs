using System;
using System.Collections.Generic;

namespace Spain_map
{
    public class Graph
    {
        public Graph(List<City> cities)
        {
            Cities = cities;
        }

        public List<City> Cities { get; }

        internal void ConnectCities(string city1, string city2, int distance)
        {
            if (string.Compare(city1, city2) == 0)
            {
                throw new Exception("Can't connect a city to itself!");
            }

            City c1 = Cities.Find((x) => x.Name == city1);
            City c2 = Cities.Find((x) => x.Name == city2);

            if (c1 == null || c2 == null)
            {
                throw new Exception("City not found!");
            }

            Road aRoad = new Road(c1, c2, distance);

            c1.Roads.Add(aRoad);
            c2.Roads.Add(aRoad);
        }
    }
}
