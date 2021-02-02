using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_1._1
{
    class Tree
    {
        public Tree(List<Item> allItems, int limit)
        {
            Start = new Node(new List<Item>(), allItems, limit);
        }

        public Node Start { get; }
          
        // TODO Implement tree search algorithms to find optimal item list

        public Node Breadth_First_Search()
        {
            Node best;
            List<Node> queue = new List<Node>();

            // adding the root node
            queue.Add(Start);
            best = Start;

            while (queue.Count > 0)
            {
                // keep track of the best node so far:
                if (queue[0].TotalBenifit > best.TotalBenifit)
                {
                    best = queue[0];
                }

                // Add only those nodes that are NOT already in the queue:
                foreach(Node theNode in queue[0].ChildNodes)
                {
                    if (!queue.Contains(theNode))
                    {
                        queue.Add(theNode);
                    }
                    else
                    {
                        Console.WriteLine("Duplication!");
                    }
                }
                // remove that node:
                queue.RemoveAt(0);
            }
            return best;
        }

        void Depth_First_Search()
        {
            // TODO!
        }
    }
}
