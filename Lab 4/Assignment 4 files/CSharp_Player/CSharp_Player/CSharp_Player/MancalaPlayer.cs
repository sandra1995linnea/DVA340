using System;
using System.Collections.Generic;

namespace CSharp_Player
{
    public static class MancalaPlayer
    {
        const int CUTOFF_LIMIT = 3;

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
        /// Returns how the board looks after the last acton, and whose turn it is.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="action">1 to 6</param>
        /// <param name="player">The currently active player</param>
        /// <returns>The board resulting from the given action, an the player whose turn it is next.</returns>
        public static (int[], Player) Result(int[] board, int action, Player player)
        {
            if (action > 6 || action < 1)
            {
                throw new ArgumentException("Incorrect action");
            }

            var newBoard = new int[board.Length];
            Array.Copy(board, newBoard, board.Length);
            int index = 0, opponentNest = 0, Nest= 0, onHand;
            Player nextPlayer = player;

            switch (player)
            {
                case Player.Max:
                    index = action - 1;
                    opponentNest = 13;
                    Nest = 6;
                    nextPlayer = Player.Min;
                    break;

                case Player.Min:
                    index = action + 6;
                    opponentNest = 6;
                    Nest = 13;
                    nextPlayer = Player.Max;
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
                        newBoard[Nest] ++;
                        if(index != Nest)
                        {
                            //moving the stones from the opposite whole of the empty whole to the nest
                            newBoard[Nest] += newBoard[12 - index];
                            newBoard[12 - index] = 0;
                        }
                        break;
                    }

                    // drop a stone in the hole
                    newBoard[index]++;
                    onHand--;
                }
            }

            //if the last is in my nest, i get a free turn
            if (onHand == 1 && index == Nest)
            {
                nextPlayer = player;
            }

            return (newBoard, nextPlayer);
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

        public static int BoardPoints(int[] board, Player player)
        {
            int playerPoints = 0;
            switch(player)
            {
                case Player.Max:
                    for (int i = 0; i <= 6; i++)
                    {
                        playerPoints += board[i];
                    }
                    break;
                case Player.Min:
                    for(int i = 7; i <= 13; i++)
                    {
                        playerPoints += board[i];
                    }
                    break;
            }

            return playerPoints;
        }

        public static int OpponentSide(int[] board, Player player)
        {
            int Nr0fStones = 0;
            switch(player)
            {
                case Player.Max:
                    for(int i = 7; i < 13; i++)
                    {
                        Nr0fStones += board[i];
                    }
                    break;
                case Player.Min:
                    for(int i = 0; i < 6; i++)
                    {
                        Nr0fStones += board[i];
                    }
                    break;
            }

            return Nr0fStones;
        }

        /// <summary>
        /// the utility function - it creates an approximate value of how good a state will be
        /// </summary>
        /// <param name="board"></param>
        /// <returns>1 if that board would be a win, -1 if it would be a loose and something in between if i would be on my way to lose/win</returns>
        public static double Eval(int[] board)
        {
            if(IsTerminal(board))
            {
                int MaxPoints = BoardPoints(board, Player.Max);
                int MinPoints = BoardPoints(board, Player.Min);

                if(MaxPoints > MinPoints)
                {
                    return 1;
                }
                else if(MaxPoints == MinPoints)
                {
                    return 0;
                }
                return -1;
            }

            //MAX will win
            if(board[6] >= 24)
            {
                return 1;
            }
            //MIN will win
            if (board[13] >= 24)
            {
                return -1;
            }

            //MAX is leading
            if (board[6] > board[13])
            {
                return board[6] / 24;
            }
            
            //MIN is leading
            if(board[13] > board[6])
            {
                return board[13] / -24;
            }

            return 0;

        }

        /// <summary>
        /// Returns the move the agent does.
        /// </summary>
        /// <param name="board">A int array of size 14 describing the board</param>
        /// <returns>A string representing the move, from "1" to "6"</returns>
        public static string MiniMax(int[] board, Player player)
        {
            // TODO this function can be simplified...

            List<int> actions = PossibleActions(board, (int)player);
            double bestUtility, utilityValue;
            int bestAction = 0;
            int[] newBoard;
            Player nextPlayer;

            if (player == Player.Max)
            {
                bestUtility = double.MinValue;
                foreach(var action in actions)
                {
                    (newBoard, nextPlayer) = Result(board, action, player);

                    utilityValue = nextPlayer == Player.Min ? MinValue(newBoard, 0) : MaxValue(newBoard, 0);

                    if(utilityValue > bestUtility)
                    {
                        bestUtility = utilityValue;
                        bestAction = action;
                    }
                }
                if (bestAction == 0)
                    throw new Exception("Incorrect action!");
                
                return bestAction.ToString();
            }
            // for min player:

            bestUtility = double.MaxValue;
            foreach (var action in actions)
            {
                (newBoard, nextPlayer) = Result(board, action, player);
                utilityValue = nextPlayer == Player.Min ? MinValue(newBoard, 0) : MaxValue(newBoard, 0);
                if (utilityValue < bestUtility)
                {
                    bestUtility = utilityValue;
                    bestAction = action;
                }
            }

            if (bestAction == 0)
                throw new Exception("Incorrect action!");

            return bestAction.ToString();
        }

        public static double MaxValue(int[] board, int depth)
        {
            if(depth >= CUTOFF_LIMIT || IsTerminal(board))
            {
                return Eval(board);
            }

            int[] newBoard;
            Player nextPlayer;

            double biggest_value = double.MinValue;
            List<int> actions = PossibleActions(board, (int)Player.Max);
            foreach(var action in actions)
            {
                (newBoard, nextPlayer) = Result(board, action, Player.Max);
                
                var value = nextPlayer == Player.Min ? MinValue(newBoard, depth + 1) : MaxValue(newBoard, depth + 1);

                if(value > biggest_value)
                {
                    biggest_value = value;
                }
            }

            return biggest_value;
        }

        public static double MinValue(int[] board, int depth)
        {
            if (depth >= CUTOFF_LIMIT || IsTerminal(board))
            {
                return Eval(board);
            }

            int[] newBoard;
            Player nextPlayer;

            double lowest_value = double.MaxValue;
            List<int> actions = PossibleActions(board, (int)Player.Min);
            foreach(var action in actions)
            {
                (newBoard, nextPlayer) = Result(board, action, Player.Min);

                var value = nextPlayer == Player.Min ? MinValue(newBoard, depth + 1) : MaxValue(newBoard, depth + 1);

                if (value < lowest_value)
                {
                    lowest_value = value;
                }
            }
            return lowest_value;
        }
    }
}
