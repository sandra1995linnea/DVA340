using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment_1._1;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_1._1.Tests
{
    [TestClass()]
    public class NodeTests
    {
        [TestMethod()]
        public void EqualsTest()
        {
            int limit = 1;
            Item item1 = new Item(1, 2, 3);
            Item item2 = new Item(2, 2, 3);
            Item item3 = new Item(3, 2, 3);
            Item item4 = new Item(4, 2, 3);

            var allItems = new List<Item>()
            {
                item1, item2, item3, item4
            };

            Node node1 = new Node(new List<Item>() { item1 }, allItems, limit);
            Node node2 = new Node(new List<Item>() { item2 }, allItems, limit);
            Node node3 = new Node(new List<Item>() { item2 }, allItems, limit);

            Assert.AreNotEqual(node1, node2);

            Assert.IsTrue(node2 == node3);
            Assert.IsTrue(node1 != node2);
            Assert.IsTrue(node2.Equals(node3));

            var list = new List<Node>() { node2 };

            Assert.IsFalse(list.Contains(node1));
            Assert.IsTrue(list.Contains(node3));
        }
    }
}