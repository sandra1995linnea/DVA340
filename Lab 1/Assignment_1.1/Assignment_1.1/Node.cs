using System.Collections.Generic;

namespace Assignment_1._1
{
    class Node
    {
        private readonly List<Item> allItems;
        private readonly int limit;
        private List<Item> possibleDirections;

        public Node(List<Item> itemsTaken, List<Item> allItems, int limit)
        {
            ItemsTaken = itemsTaken;
            TotalBenifit = 0;
            TotalWeight = 0;
            this.allItems = allItems;
            this.limit = limit;

            // TODO: Calculate TotalBenifit and TotalWeight from itemsTaken
        }

        public List<Item> ItemsTaken { get; }
        public int TotalBenifit { get; }
        public int TotalWeight { get; }

        public List<Item> PossibleDirections
        {
            get
            {
                if (possibleDirections == null)
                {
                    // TOOD create list!
                    possibleDirections = new List<Item>();
                }
                return possibleDirections;
            }
        }
    }
}
