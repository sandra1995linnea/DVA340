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
            var actual = MancalaPlayer.PossibleActions(board, 1);
            CollectionAssert.AreEqual(expected, actual);

            // the same answer is expected for the other playrer in this case
            actual = MancalaPlayer.PossibleActions(board, 2);
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void OneWholeEmpty()
        {
            int[] board = new int[14];
            var expected1 = new List<int>();
            var expected2 = new List<int>();

            for (int i = 0; i < 6; i++)
            {
                if(i != 2)
                {
                    board[i] = 4;
                    expected1.Add(i + 1);
                }
            }

            for(int i = 7; i < 13; i++)
            {
                if(i != 4)
                {
                    board[i] = 4;
                    expected2.Add(i - 6);
                }
            }

            var actual = MancalaPlayer.PossibleActions(board, 1);
            CollectionAssert.AreEqual(expected1, actual);

            actual = MancalaPlayer.PossibleActions(board, 2);
            CollectionAssert.AreEqual(expected2, actual);
            
        }
    }
}
