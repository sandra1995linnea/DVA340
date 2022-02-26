using System.Collections.Generic;

namespace TravelingSalesMan_AntColony
{
    public interface IAnt
    {
        double TotalDistance { get; }
        List<Location> Visited { get; }

        void Run();
    }
}