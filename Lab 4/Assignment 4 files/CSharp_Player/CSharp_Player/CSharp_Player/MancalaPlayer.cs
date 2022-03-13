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
            return "1"; 
        }

        /// <summary>
        /// Returns the set of legal moves given the current board
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public static List<int> PossibleActions(int[] board, int palyerTurn)
        {
            List<int> actions = new List<int>();

            if(palyerTurn == 1)
            {
                for (int i = 0; i < 6; i++)
                {
                    if(board[i] != 0)
                    {
                        actions.Add(i + 1);
                    }
                }
            }

            else
            {
                for(int i = 7; i < 13; i++)
                {
                    if(board[i] != 0)
                    {
                        actions.Add(i - 6);
                    }
                }
            }
            
            return actions;
        }

        /// <summary>
        /// Returns how the board looks after the last acton
        /// </summary>
        /// <param name="board"></param>
        /// <param name="action">1 to 6</param>
        /// <returns>board</returns>
        public static int[] Result(int[] board, int action, Player player)
        {
            if (action > 6 || action < 1)
            {
                throw new ArgumentException("Incorrect action");
            }

            var newBoard = new int[board.Length];
            Array.Copy(board, newBoard, board.Length);
            int index = 0, opponentNest = 0, Nest= 0, onHand;

            // assume player == Max for now
            switch (player)
            {
                case Player.Max:
                    index = action - 1;
                    opponentNest = 13;
                    Nest = 6;
                    break;

                case Player.Min:
                    index = action + 6;
                    opponentNest = 6;
                    Nest = 13;
                    break;
            }

            // pick up stones:
            onHand = newBoard[index];
            newBoard[index] = 0;

            while (onHand > 0)
            {
                index = (index + 1) % 14;
                if (index != opponentNest) // skip opponent's nest
                {
                    if(onHand == 1 && newBoard[index] == 0)
                    {
                        //dropping the last stone onhand in the nest
                        newBoard[Nest] += onHand;
                        //moving the stones from the opposite whole of the empty whole to the nest
                        newBoard[Nest] += newBoard[12 - index];
                        newBoard[12 - index] = 0;
                        break;
                    }
                    // drop a stone in the hole
                    newBoard[index]++;
                    onHand--;
                }
            }

            return newBoard;
        }

        /// <summary>
        /// Returns true if the game is over, other wise false
        /// </summary>
        /// <param name="board"></param>
        /// <returns>true or false</returns>
        public static bool IsTerminal(int[] board)
        {
            List<int> EmptyUnderSpots = new List<int>();
            List<int> EmptyUpperSpots = new List<int>();

            for(int i = 0; i < 6; i++)
            {
                if(board[i] == 0)
                {
                    EmptyUnderSpots.Add(i);
                }
            }

            for(int i = 7; i < 13; i++)
            {
                if(board[i] == 0)
                {
                    EmptyUpperSpots.Add(i);
                }
            }

            if(EmptyUnderSpots.Count == 6 || EmptyUpperSpots.Count == 6)
            {
                return true;
            }
            return false; 
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
