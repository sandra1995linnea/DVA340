using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_Player
{

    public static class MancalaPlayer
    {
        /// <summary>
        /// Returns the move the agent does.
        /// </summary>
        /// <param name="board">A int array of size 14 describing the board</param>
        /// <returns>A string representing the move, from "1" to "6"</returns>
        public static string GetMove(int[] board)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the set of legal moves given the current board
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public static List<int> PossibleActions(int[] board, Player player)
        {
            return null; // TODO
        }

        /// <summary>
        /// Returns how the board looks after the last acton
        /// </summary>
        /// <param name="board"></param>
        /// <param name="action"></param>
        /// <returns>board</returns>
        public static int[] Result(int[] board, int action)
        {
            return null; // TODO
        }

        /// <summary>
        /// Returns true if the game is over, other wise false
        /// </summary>
        /// <param name="board"></param>
        /// <returns>true or false</returns>
        public static bool IsTerminal(int[] board)
        {
            return false; // TODO
        }

        /// <summary>
        /// the utility function - it creates an approximate value of how good a state will be
        /// </summary>
        /// <param name="board"></param>
        /// <returns>1 if that board would be a win, -1 if it would be a loose and something in between if i would be on my way to lose/win</returns>
        public static double Eval(int[] board)
        {
            return 0; //TODO
        }
    }
}
