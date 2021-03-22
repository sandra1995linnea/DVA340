using System;
using System.Collections.Generic;
using System.Linq;

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

        List<City> visitedCities = new List<City>();

        //Search algorithms
        public void GreedyBestFirst(string nameOfStartCity)
        {
            List<City> relevantCities = new List<City>(); // this list will be kept sorted
            var cityWeAreIn = Cities.Find((x) => x.Name == nameOfStartCity);
            relevantCities.Add(cityWeAreIn);
            
            const string cityToFind = "Valladolid";
            
            while(relevantCities.Count > 0)
            {
                //From the city we are in, expand which is the next city to move on to

                // pops out the first city in the list
                cityWeAreIn = relevantCities[0];

                // expand node:

                Console.Write("In " + cityWeAreIn.Name + ":\t");

                if(cityWeAreIn.Name == cityToFind)
                {
                    return;
                }
                                
                List<City> possibleCities = cityWeAreIn.AdjacentCities;

                // add possibleCities to relevantCities:
                foreach(var city in possibleCities)
                {
                    if(!relevantCities.Contains(city)) // don't add a city we already know about
                    {
                        // add the city at the correct place in relevantCities
                        int index = relevantCities.Count;

                        foreach(var relevant in relevantCities) 
                        {
                            if(relevant.DistanceToGoal > city.DistanceToGoal)
                            {
                                index = relevantCities.IndexOf(relevant);                                
                                break;
                            }
                        }
                        relevantCities.Insert(index, city);

                        Console.Write(city.Name + "\t");
                    }
                }

                Console.WriteLine("");

                relevantCities.Remove(cityWeAreIn);
            }
            Console.WriteLine("Failed to find " + cityToFind);
        }

        public void Astar(string nameOfStartCity)
        {            
            var cityWeAreIn = Cities.Find((x) => x.Name == nameOfStartCity);
            List<Tuple<int, City>> possibleCities = new List<Tuple<int, City>>();
            const string cityToFind = "Valladolid";

            possibleCities = cityWeAreIn.AdjacentCityDistance;

            while (possibleCities.Count > 0)
            {
                Tuple<int, City> bestSoFar = possibleCities[0];

                Console.Write("At " + cityWeAreIn.Name + ", can go to: ");

                foreach (var city in possibleCities)
                {
                    Console.Write(city.Item2.Name + "\t");

                    if (city.Item1 + city.Item2.DistanceToGoal < bestSoFar.Item1 + bestSoFar.Item2.DistanceToGoal)
                    {
                        bestSoFar = city;
                    }                    
                }

                // moving to the best city:
                cityWeAreIn = bestSoFar.Item2;

                Console.WriteLine("Moving to " + cityWeAreIn.Name);

                if (cityWeAreIn.Name == cityToFind)
                {
                    Console.WriteLine("We're in Valladolid now!");
                    return;
                }

                possibleCities = cityWeAreIn.AdjacentCityDistance;
                
            }
            Console.WriteLine("Failed to find " + cityToFind);
        }
    }
}
