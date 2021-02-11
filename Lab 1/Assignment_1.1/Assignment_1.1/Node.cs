using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assignment_1._1
{
    public class Node : IEquatable<Node>
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

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is Node node)
            {   
                if (ItemsTaken.Count != node.ItemsTaken.Count)
                    return false;

                List<Item> myItems = new List<Item>(ItemsTaken);
                List<Item> thoseItems = new List<Item>(node.ItemsTaken);

                myItems.Sort();
                thoseItems.Sort();

                for (int i = 0; i < ItemsTaken.Count; i++)
                {
                    if (myItems[i] != thoseItems[i])
                        return false;
                }
                return true;
            }
            else
               return false;
        }

        public bool Equals([AllowNull] Node other)
        {
            if (other is null)
                return false;

            return Equals((object)other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ItemsTaken);
        }

        public static bool operator ==(Node left, Node right) => left.Equals(right);
       /* {
            if (left == null && right == null)
                return true;
            else if ((left != null && right == null) || (left == null && right != null))
                return false;
            else
            {
                if (left.ItemsTaken.Count != right.ItemsTaken.Count)
                    return false;

                for(int i=0; i< left.ItemsTaken.Count; i++)
                {
                    if (left.ItemsTaken[i] != right.ItemsTaken[i])
                        return false;
                }
                return true;
            }
        }*/

        public static bool operator !=(Node left, Node right) => !(left == right);
    }
}
