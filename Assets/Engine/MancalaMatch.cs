using System;

namespace Mancala
{
    /********************************************************************/
    /* This class creates two Players and runs a pair of Kalah games,
    /* one with each player starting. The match score is reported.
    /********************************************************************/

    public class MancalaMatch
    {
        private static int timeLimit = 1000;						                // turn time in msec

        private static Player pHuman = new HumanPlayer(Position.Top, timeLimit);
        private static Player pTop = new BonzoPlayer(Position.Top, timeLimit);	// TOP player (MAX)
        private static Player pBot = new mcw33Player(Position.Bottom, timeLimit);	// BOTTOM player	
        private static Board b;			                                // playing surface
        private static int move;


        /* 
         * Play one Kalah game with the two given players, with firstPlayer
         * starting. This function returns TOP's score.
         */
        public static int playGame(Player pTop, Player pBot, Position firstPlayer)
        {
            b = new Board(firstPlayer);

            if (firstPlayer == Position.Top)
				UnityEngine.Debug.Log("Player " + pTop.getName() + " starts.");
            else
				UnityEngine.Debug.Log("Player " + pBot.getName() + " starts.");

            b.display();

            while (!b.gameOver())
            {
                if (b.whoseMove() == Position.Top)
                {
                    move = pTop.chooseMove(b);
					UnityEngine.Debug.Log(pTop.getName() + " chooses move " + move);
                }
                else
                {
                    move = pBot.chooseMove(b);
					UnityEngine.Debug.Log(pBot.getName() + " chooses move " + move);
                }

                b.makeMove(move, true);		// last parameter says to be chatty
                b.display();

                if (b.gameOver())
                {
                    if (b.winner() == Position.Top)
						UnityEngine.Debug.Log("Player " + pTop.getName() +
                        " (TOP) wins " + b.scoreTop() + " to " + b.scoreBot());
                    else if (b.winner() == Position.Bottom)
						UnityEngine.Debug.Log("Player " + pBot.getName() +
                        " (BOTTOM) wins " + b.scoreBot() + " to " + b.scoreTop());
					else UnityEngine.Debug.Log("A tie!");
                }
                else
                    if (b.whoseMove() == Position.Top)
						UnityEngine.Debug.Log(pTop.getName() + " to move.");
                else
						UnityEngine.Debug.Log(pBot.getName() + " to move.");
            }
            return b.scoreTop();
        }

        public static void Main(String[] args)
        {
            int topScore;

			UnityEngine.Debug.Log("\n================ Game 1 ================");
            topScore = playGame(pHuman, pBot, Position.Bottom);

			UnityEngine.Debug.Log("\n================ Game 2 ================");
            topScore += playGame(pHuman, pBot, Position.Top);

			UnityEngine.Debug.Log("\n========================================");
            UnityEngine.Debug.Log("Match result: ");

            int botScore = 96 - topScore;
            if (topScore > 48)
            {
				UnityEngine.Debug.Log(pHuman.getName() + " wins " + topScore + " to " + botScore);
				UnityEngine.Debug.Log(pHuman.gloat());
            }
            else if (botScore > 48)
            {
				UnityEngine.Debug.Log(pBot.getName() + " wins " + botScore + " to " + topScore);
				UnityEngine.Debug.Log(pBot.gloat());
            }
            else
				UnityEngine.Debug.Log("Match was a tie, 48-48!");

            Console.Read();
        }
    }
}
