﻿using System;
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
                foreach(Node newNode in queue[0].ChildNodes)
                {
                    if (!queue.Contains(newNode))
                    {
                        queue.Add(newNode);
                        Print(newNode);
                    }
                }
                // remove that node:
                queue.RemoveAt(0);
            }
            return best;
        }

        public Node Depth_First_Search()
        {
            //adding the root node 
            Node best;
            Stack<Node> mystack = new Stack<Node>();
            List<Node> visitedNodes = new List<Node>();

            mystack.Push(Start);
            visitedNodes.Add(Start);
            best = Start;
            Print(Start);

            while (mystack.Count > 0)
            {
                Node next = mystack.Pop();

                //keep track of the best node so far
                if(next.TotalBenifit > best.TotalBenifit)
                {
                    best = next;
                }

                //Add only those nodes that are not already in the Stack
                foreach(Node newNode in next.ChildNodes)
                {
                    if (!visitedNodes.Contains(newNode))
                    {
                        mystack.Push(newNode);
                        visitedNodes.Add(newNode);
                    }
                }

                Print(next);
            }
            return best;
        }

        private void Print(Node node)
        {
            //string str = "";
            //foreach(var item in node.ItemsTaken)
            //{
            //    str = str + " " + item.Id.ToString();
            //}
            //Console.WriteLine("Node: " + str);
        }
    }
}
