
using System.Collections.Generic;
using System.IO;

namespace TravelingSalesMan_AntColony
{
    class Graph
    {
        public List<Location> GenerateLocations()
        {
            //a list that will store all the locations
            List<Location> locations = new List<Location>();

            string locationfile = "F:/Mälardalens Högskola/DVA340/Lab 3/Berlin52.tsp";
            string[] lines = File.ReadAllLines(locationfile); //reads all the lines from the file
            string[] substrings;
            int i = 0;


            // loops until we reach the position int the file we want to take information from
            while (true)
            {
                if (lines[i].StartsWith("NODE_COORD_SECTION"))
                {
                    break;
                }
                i++;
            }
            i++;

            // puts the information we want into the location list and after we return the list
            while (lines[i] != "EOF")
            {
                substrings = lines[i].Split(' ');
                locations.Add(new Location(int.Parse(substrings[0]), double.Parse(substrings[1].Replace('.', ',')), double.Parse(substrings[2].Replace('.', ','))));
                i++;
            }

            return locations;
        }
    }
}
