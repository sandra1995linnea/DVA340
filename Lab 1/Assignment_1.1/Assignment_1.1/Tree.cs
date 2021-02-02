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
            Print(Start);

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
                        Print(theNode);
                    }
                }
                // remove that node:
                queue.RemoveAt(0);
            }
            return best;
        }

        private void Print(Node node)
        {
            string str = "";
            foreach(var item in node.ItemsTaken)
            {
                str = str + " " + item.Id.ToString();
            }
            Console.WriteLine("Node: " + str);
        }

        void Depth_First_Search()
        {

        }
    }
}
