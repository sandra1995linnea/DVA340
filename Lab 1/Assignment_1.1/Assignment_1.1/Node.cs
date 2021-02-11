using System.Collections.Generic;

namespace Assignment_1._1
{
    public class Node
    {
        private readonly Item[] allItems;
        private readonly int limit;
        private List<Node> childNodes;

        // Highest index used to create itemsTaken
        private readonly int highestIndexTaken;

        public Node(List<Item> itemsTaken, Item[] allItems, int limit, int highestIndexTaken = -1)
        {
            ItemsTaken = itemsTaken;
            TotalBenifit = 0;
            TotalWeight = 0;
            this.allItems = allItems;
            this.limit = limit;
            this.highestIndexTaken = highestIndexTaken;

            //Calculate TotalBenifit and TotalWeight from itemsTaken
            foreach(var takenItem in itemsTaken)
            {
                TotalBenifit += takenItem.Benefit;
                TotalWeight += takenItem.Weight;
            }
        }

        public List<Item> ItemsTaken { get; }
        public int TotalBenifit { get; }
        public int TotalWeight { get; }

        public List<Node> ChildNodes
        {
            get
            {
                if(childNodes == null)
                {
                    childNodes = new List<Node>();
                    int possibleWeight = limit - TotalWeight;

                    //looping through possible items to take
                    for (int i = highestIndexTaken + 1; i < allItems.Length; i++)
                    {
                        if (allItems[i].Weight <= possibleWeight)
                        {
                            // this list will contain taken items
                            List<Item> itemsTakenForThisNode = new List<Item>(ItemsTaken);
                            // adding one possible way to the list
                            itemsTakenForThisNode.Add(allItems[i]);

                            childNodes.Add(new Node(itemsTakenForThisNode, allItems, limit, i));
                        }
                    }
                }
                return childNodes;
            }
        }
    }
}
