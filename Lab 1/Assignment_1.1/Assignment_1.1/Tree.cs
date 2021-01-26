using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_1._1
{
    class Tree
    {
        private readonly List<Item> allItems;
        private readonly int limit;

        public Tree(List<Item> allItems, int limit)
        {
            this.allItems = allItems;
            this.limit = limit;

            Start = new Node(new List<Item>(), allItems, limit);
        }

        public Node Start { get; }

        public void Search()
        {
            Item toTake = Start.PossibleDirections[0];

            List<Item> newItemsTaken = Start.ItemsTaken;
            newItemsTaken.Add(toTake);

            Node node = new Node(newItemsTaken, allItems, limit);
        }

        // TODO Implement tree search algorithms to find optimal item list
    }
}
