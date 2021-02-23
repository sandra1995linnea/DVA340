namespace Spain_map
{
    public class Edge
    {
        public Edge(string city1, string city2, int distance)
        {
            City1 = city1;
            City2 = city2;
            Distance = distance;
        }

        public string City1 { get; }
        public string City2 { get; }
        public int Distance { get; }
    }
}
