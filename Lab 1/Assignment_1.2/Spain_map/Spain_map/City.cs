namespace Spain_map
{
    public class City
    {
        public City(string name, int distance)
        {
            Name = name;
            Distance = distance;
        }

        public string Name { get; }
        public int Distance { get; }
    }
}
