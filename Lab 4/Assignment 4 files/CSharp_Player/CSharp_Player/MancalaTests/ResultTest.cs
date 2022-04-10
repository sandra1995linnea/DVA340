using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using CSharp_Player;

namespace MancalaTests
{
    [TestClass]
   public class ResultTest
   {
        [TestMethod]
        public void InitialBoardFirstAction()
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


            int[] actual; 
            (actual, _) = MancalaPlayer.Result(board, 1, Player.Max);
            var expected = new int[] { 0, 5, 5, 5, 5, 4, 0, 4, 4, 4, 4, 4, 4, 0};
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void StonesInStore()
        {
            int[] board = new int[14];

            for (int i = 0; i < 6; i++)
            {
                board[i] = 4;
            }

            for (int i = 7; i < 13; i++)
            {
                board[i] = 4;
            }

            int[] actual;
            (actual, _) = MancalaPlayer.Result(board, 4, Player.Max);
            var expected = new int[] { 4, 4, 4, 0, 5, 5, 1, 5, 4, 4, 4, 4, 4, 0 };
            CollectionAssert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void ActionByMinPlayer()
        {
            int[] board = new int[14];

            for (int i = 0; i < 6; i++)
            {
                board[i] = 4;
            }

            for (int i = 7; i < 13; i++)
            {
                board[i] = 4;
            }

            int[] actual;
            (actual, _) = MancalaPlayer.Result(board, 1, Player.Min);
            var expected = new int[] { 4, 4, 4, 4, 4, 4, 0, 0, 5, 5, 5, 5, 4, 0 };
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void StonesInStoreForMin()
        {
            int[] board = new int[14];

            for (int i = 0; i < 6; i++)
            {
                board[i] = 4;
            }

            for (int i = 7; i < 13; i++)
            {
                board[i] = 4;
            }

            int[] actual;
            (actual, _) = MancalaPlayer.Result(board, 4, Player.Min);
            var expected = new int[] { 5, 4, 4, 4, 4, 4, 0, 4, 4, 4, 0, 5, 5, 1 };
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LastPieceInEmptyWhole()
        {
            int[] board = new int[14];

            for(int i = 0; i < 6; i++)
            {
                board[i] = 4;
            }

            for(int i = 7; i < 13; i++)
            {
                if(i < 9)
                {
                    board[i] = 2;
                }
                else
                {
                    board[i] = 0;
                }
            }

            int[] actual;
            Player nextPlayer;
            (actual, nextPlayer) = MancalaPlayer.Result(board, 6, Player.Max);
            var expected = new int[] { 4, 4, 4, 0, 4, 0, 6, 3, 3, 0, 0, 0, 0, 0 };
            CollectionAssert.AreEqual(expected, actual);
            Assert.AreEqual(nextPlayer, Player.Min);
        }

        [TestMethod]
        public void LastPieceInNest_FreeTurn()
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

            int[] actual;
            Player nextPlayer;
            (actual, nextPlayer) = MancalaPlayer.Result(board, 3, Player.Max);
            var expected = new int[] { 4, 4, 0, 5, 5, 5, 1, 4, 4, 4, 4, 4, 4, 0 };
            CollectionAssert.AreEqual(expected, actual);
            Assert.AreEqual(nextPlayer, Player.Max);
        }
   }
}
