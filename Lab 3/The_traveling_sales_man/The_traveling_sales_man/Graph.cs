using System.Collections.Generic;
using System.IO;

namespace The_traveling_sales_man
{
    class Graph
    {
        public List<Location> GenerateLocations()
        {
            List<Location> locations = new List<Location>();
            string name = "F:/Mälardalens Högskola/DVA340/Lab 3/Berlin52.tsp";

            string[] lines = File.ReadAllLines(name);
            string[] substrings;
            int i = 0;

            while(true)
            {
                if(lines[i].StartsWith("NODE_COORD_SECTION"))
                {
                    break;
                }
                i++;
            }
            i++;

            while(lines[i] != "EOF")
            {
                substrings = lines[i].Split(' ');
                locations.Add(new Location(int.Parse(substrings[0]), double.Parse(substrings[1].Replace('.', ',')), double.Parse(substrings[2].Replace('.', ','))));
                i++;
            }

            return locations;
        }
    }
}
