using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assignment_1._1
{
    public class Node : IEquatable<Node>
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

        public List<Item> PossibleItems
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
                    foreach (var wayToGo in PossibleItems)
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
