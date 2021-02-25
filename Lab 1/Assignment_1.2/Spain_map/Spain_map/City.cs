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
       
    }
}
