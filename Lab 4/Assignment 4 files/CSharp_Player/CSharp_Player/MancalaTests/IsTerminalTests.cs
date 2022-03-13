using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp_Player;

namespace MancalaTests
{
    [TestClass]
    public class IsTerminalTests
    {

        [TestMethod]
        public void UpperSideEmpty()
        {
            int[] board = new int[14];

            for (int i = 7; i < 13; i++)
            {
                board[i] = 4;
            }

            bool expected = true;
            var actual = MancalaPlayer.IsTerminal(board);
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void LowerSideEmpty()
        {
            int[] board = new int[14];

            for(int i = 0; i < 6; i++)
            {
                board[i] = 4;
            }

            bool expected = true;
            var actual = MancalaPlayer.IsTerminal(board);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NoSideEmpty()
        {
            int[] board = new int[14];

            for(int i = 0; i < 6; i++)
            {
                board[i] = 4;
            }

            for (int i = 7; i < 13; i++)
            {
                board[i] = 4;
            }

            bool expected = false;
            var actual = MancalaPlayer.IsTerminal(board);
            Assert.AreEqual(expected, actual);
        }

        
    }
}
