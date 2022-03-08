using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp_Player;
using System.Collections.Generic;

namespace MancalaTests
{
    [TestClass]
    public class PossibleActionsTests
    {
        [TestMethod]
        public void InitialPossibleActions()
        {
            int[] board = new int[14];
            
            for(int i = 0; i < 6; i++)
            {
                board[i] = 4;
            }

            for(int i = 7; i < 13; i++)
            {
                board[i] = 4;
            }

            var expected = new List<int>();
            for(int i = 1; i <= 6; i++)
            {
                expected.Add(i);
            }
            var actual = MancalaPlayer.PossibleActions(board, Player.Max);
            Assert.AreEqual(expected, actual);

            // the same answer is expected for the other playrer in this case
            actual = MancalaPlayer.PossibleActions(board, Player.Min);
            Assert.AreEqual(expected, actual);
        }
    }
}
