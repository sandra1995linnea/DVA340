namespace Spain_map
{
    public class Road
    {
        public Road(City city1, City city2, int distance)
        {
            ConnectionCity1 = city1;
            ConnectionCity2 = city2;
            Distance = distance;
        }
        public City ConnectionCity1 { get; }
        public City ConnectionCity2 { get; }
        public int Distance { get; }
    }
}