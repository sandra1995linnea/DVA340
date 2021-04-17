using System;
using System.Collections.Generic;

namespace CSharp_Player
{
    public enum Player { MinPlayer, MaxPlayer };

    public static class Bot
    {
        private const int CUTOFF_DEPTH = 3;

        /// <summary>
        /// Calculates next move based on the current board
        /// </summary>
        /// <param name="board">Array of 14 ints representing the board</param>
        /// <returns>A move in the range "1" to "6"</returns>
        public static string MiniMaxDescision(int[] board, Player player)
        {
            // figure out all actions we could take:
            List<int> actions = Actions(board, player);
            
            int bestAction = actions[0];
            int bestValue = int.MinValue;
            int newValue;

            // find the best action:
            foreach (var action in actions)
            {
                Player newPlayer;
                int[] newBoard;
                // figure out the result of taking this action
                Result(board, player, action, out newBoard, out newPlayer);

                // figure out the value of taking this action
                if(newPlayer == Player.MinPlayer)
                {
                    newValue = MinValue(newBoard, 0);
                }
                else
                {
                    newValue = MaxValue(newBoard, 0);
                }
                
                if (newValue > bestValue)
                {
                    bestValue = newValue;
                    bestAction = action;
                }
            }

            return bestAction.ToString();
        }

        /// <summary>
        /// Finds the max value of a given state
        /// </summary>
        /// <param name="board"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        private static int MaxValue(int[]board, int depth)
        {
            if(CutoffTest(board, depth))
            {
                return Eval(board, Player.MaxPlayer);
            }
            else
            {
                // set value to initally be the lowest possible value for an int
                int value = int.MinValue;
                foreach(var action in Actions(board, Player.MaxPlayer))
                {
                    Player newPlayer;
                    int[] newBoard;
                    Result(board, Player.MaxPlayer, action, out newBoard, out newPlayer);

                    if (newPlayer == Player.MinPlayer)
                    {
                        value = Math.Max(value, MinValue(newBoard, depth + 1));
                    }
                    else
                    {
                        value = Math.Max(value, MaxValue(newBoard, depth + 1));
                    }                    
                }
                return value;
            }
        }

        private static int MinValue(int[] board, int depth)
        {
            if(CutoffTest(board, depth))
            {
                return Eval(board, Player.MinPlayer);
            }
            else
            {
                // set value to initally be the highest possible value for an int
                int value = int.MaxValue;

                foreach (var action in Actions(board, Player.MinPlayer))
                {
                    Player newPlayer;
                    int[] newBoard;

                    Result(board, Player.MinPlayer, action, out newBoard, out newPlayer);

                    if(newPlayer == Player.MaxPlayer)
                    {
                        value = Math.Min(value, MaxValue(newBoard, depth + 1));
                    }
                    else
                    {
                        value = Math.Min(value, MinValue(newBoard, depth + 1));
                    }
                }
                return value;
            }
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
        private static void Result(int[] board, Player player, int action, out int[] updatedBoard, out Player updatedPlayer)
        {
            updatedBoard = new int[board.Length];
            Array.Copy(board, updatedBoard, board.Length);

            int ownNest = 1;
            int opponentNest = 1;
            int pickUpIndex = 0;

            if (action < 1 || action > 6)
            {
                throw new ArgumentException();
            }

            switch(player)
            {
                case Player.MaxPlayer:
                    ownNest = 6;
                    opponentNest = 13;
                    pickUpIndex = action + 6;
                    break;

                case Player.MinPlayer:
                    ownNest = 13;
                    opponentNest = 6;
                    pickUpIndex = action - 1;
                    break;
            }

            // pick up stones from the hole:
            int onHand = updatedBoard[pickUpIndex];
            updatedBoard[pickUpIndex] = 0;

            // place stones:
            int index = pickUpIndex; 
            while(onHand > 0)
            {
                index++;

                // skips opponents store of stones
                if (index == opponentNest)
                {
                    index++;  
                }
                // makes sure that index always is in the range 0 - 13
                index = index % 13;

                // dropping off a stone, therefore one less on hand and the indexspot in the board gets 1 more 
                onHand--;

                // If we're about to drop a stone on an empty hole, on our side:
                if(updatedBoard[index] == 0 && OnMySide(index, player))
                {
                    // pick up the stone that we were dropping and the stones in the opposite hole:
                    updatedBoard[ownNest] += updatedBoard[OppositeIndex(index)] + 1;
                    updatedBoard[OppositeIndex(index)] = 0;
                }
                else // dropping stone on non-empty hole, or not on our side:
                {
                    updatedBoard[index]++;
                }
            }

            // If the last stone we dropped off was any where other than our own nest, it will be the other player's turn
            if (index != ownNest)
            {
                //if player was MaxPlayer it will switch to MinPlayer, otherwise it will switch from MinPlayer to MaxPlayer
                updatedPlayer = (player == Player.MaxPlayer ? Player.MinPlayer : Player.MaxPlayer);
            }
            else // when dropping a stone on our own nest it's our turn again
            {
                updatedPlayer = player;
            }
        }

        /// <summary>
        /// Checks if a index is on the player's side.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        private static bool OnMySide(int index, Player player) =>
            player == Player.MinPlayer ? index <= 5 : index >= 7;


        /// <summary>
        /// Calculates the index a opposite given index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private static int OppositeIndex(int index) => 12 - index;

        /// <summary>
        /// Returns a list of actions that are legal given what the board looks like.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        private static List<int> Actions(int[] board, Player player)
        {
            List<int> actions = new List<int>();

            int offset = player == Player.MaxPlayer ? 6 : -1;

            for(int action = 1; action <= 6; action++)
            {
                if(board[action + offset] != 0)
                {
                    actions.Add(action);
                }
            }
            return actions;
        }

        /// <summary>
        /// Returns an estimate for how good a certain state is for the max player,
        ///  given what the board looks like in this state.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        private static int Eval(int[] board, Player player) => 
            (player == Player.MaxPlayer ? board[6] - board[13] : board[13] - board[6]);

    }
}
