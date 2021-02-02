using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assignment_1._1.Tests
{
    [TestClass()]
    public class ItemTests
    {
        [TestMethod()]
        public void EqualsTest()
        {
            Item item1 = new Item(1, 2, 3);
            Item item2 = new Item(2, 2, 3);
            Item item3 = new Item(2, 2, 3);

            Assert.IsFalse(item1.Equals(item2));
            Assert.IsTrue(item2.Equals(item3));
        }
    }
}