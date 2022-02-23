using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelingSalesMan_AntColony
{
    class Colony
    {
        private const int NUMBER_OF_ANTS = 300;
        private readonly List<Ant> ants;

        public Colony(List<Location> locations, double goalDistance)
        {
            PheremoneHandler pheremoneHandler = new PheremoneHandler();
            Random random = new Random();

            // create ants!
            ants = new List<Ant>();
            for (int i = 0; i < NUMBER_OF_ANTS; i++)
            {
                ants.Add(new Ant(locations, pheremoneHandler, random));
            }

            do
            {
                // let all ants run around:
                ants.ForEach(ant => ant.Run());

                // update pheremones:
                pheremoneHandler.Update(ants);

                ants = ants.OrderBy(ant => ant.TotalDistance).ToList();
                Console.WriteLine("Best ant: " + ants[0].TotalDistance + "Second best: " + ants[1].TotalDistance + "Third best: " + ants[2].TotalDistance);
            } while (ShortestDistance > goalDistance);
        }


        public List<Location> ShortestPath => ants[0].Visited;
        public double ShortestDistance => ants[0].TotalDistance;
    }
}
