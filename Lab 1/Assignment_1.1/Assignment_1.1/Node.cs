using System.Collections.Generic;

namespace Assignment_1._1
{
    class Node
    {
        private readonly List<Item> allItems;
        private readonly int limit;
        private List<Item> possibleItems;
        private List<Node> childNodes;

        public Node(List<Item> itemsTaken, List<Item> allItems, int limit)
        {
            ItemsTaken = itemsTaken;
            TotalBenifit = 0;
            TotalWeight = 0;
            this.allItems = allItems;
            this.limit = limit;

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


        public List<Item> PossibleItemss
        {
            get
            {
                if (possibleItems == null)
                {
                    possibleItems = new List<Item>();
                    int possibleWeight = limit - TotalWeight;

                    foreach (var item in allItems)
                    {
                        if (!ItemsTaken.Contains(item) && item.Weight <= possibleWeight)
                        {
                            possibleItems.Add(item);
                        }
                    }
                }
                return possibleItems;
            }
        }

        public List<Node> ChildNodes
        {
            get
            {
                if(childNodes == null)
                {
                    childNodes = new List<Node>();

                    //looping through possible items to take
                    foreach (var wayToGo in PossibleItemss)
                    {
                        // this list will contain taken items
                        List<Item> itemsTakenForThisNode = new List<Item>(ItemsTaken);
                        // adding one possible way to the list
                        itemsTakenForThisNode.Add(wayToGo);
                        
                        childNodes.Add(new Node(itemsTakenForThisNode, allItems, limit));
                    }
                }
                return childNodes;
            }
        }
    }
}
