using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_Player
{
    enum Player { MinPlayer, MaxPlayer };

    public static class Bot
    {
        private const int CUTOFF_DEPTH = 3;

        /// <summary>
        /// Calculates next move based on the current board
        /// </summary>
        /// <param name="board">Array of 14 ints representing the board</param>
        /// <returns>A move in the range "1" to "6"</returns>
        public static string MiniMaxDescision(int[] board)
        {
            int move = 1;
            Player player = Player.MaxPlayer;



            //Bot.Result(ref board, ref player, 1);

            return move.ToString();
        }

        /// <summary>
        /// Checks if a state is a terminal state
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private static bool TerminalTest(int[] state) =>
            state[0] == 0 && state[1] == 0 && state[2] == 0 && state[3] == 0 && state[4] == 0 && state[5] == 0 ||
                state[7] == 0 && state[8] == 0 && state[9] == 0 && state[10] == 0 && state[11] == 0 && state[12] == 0;

        /// <summary>
        /// Determines if a search should be cut off, either from maximum seach depth has been reached
        /// or from the state being a terminal state.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        private static bool CutoffTest(int[] state, int depth) => 
            depth >= CUTOFF_DEPTH || TerminalTest(state);

        /// <summary>
        /// Updates the board, and which player whose turn it is, after a given action has been performed.
        /// </summary>
        /// <param name="board">Initial state</param>
        /// <param name="action">Action, in the range 1..6</param>
        /// <param name="player">Current player</param>
        private static void Result(ref int[] board, ref Player player, int action)
        {



            switch(player)
            {
                case Player.MaxPlayer:

                    break;

                case Player.MinPlayer:
                    
                    break;
            }


        }
    }
}
