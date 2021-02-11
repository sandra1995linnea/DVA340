using System.Collections.Generic;

namespace Assignment_1._1
{
    class Tree
    {
        public Tree(List<Item> allItems, int limit)
        {
            Start = new Node(new List<Item>(), allItems.ToArray(), limit);
        }

        public Node Start { get; }
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

                foreach(Node newNode in queue[0].ChildNodes)
                {
                    queue.Add(newNode);
                    Print(newNode);
                }
                // remove that node:
                queue.RemoveAt(0);
            }

            return best;
        }

        public Node Depth_First_Search()
        {
            Node best;
            Stack<Node> mystack = new Stack<Node>();

            //adding the root node 
            mystack.Push(Start);

            best = Start;

            while (mystack.Count > 0)
            {
                Node next = mystack.Pop();

                //keep track of the best node so far
                if(next.TotalBenifit > best.TotalBenifit)
                {
                    best = next;
                }

                next.ChildNodes.ForEach((newNode) => mystack.Push(newNode));

                Print(next);
            }
            return best;
        }

        private void Print(Node node)
        {
            //string str = "";
            //foreach (var item in node.ItemsTaken)
            //{
            //    str = str + " " + item.Id.ToString();
            //}
            //System.Console.WriteLine("Node: " + str);
        }
    }
}
