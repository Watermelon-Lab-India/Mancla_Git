﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mancala
{
    /*********************************************************/
    /* A Kalah player that prompts the user for moves. 
    /*********************************************************/
    public class HumanPlayer : Player
    {
        /*
         *call the Player constructor with given position and name "Human"
         */
        public HumanPlayer(Position pos, int timeLimit) : base(pos, "Human", timeLimit) { }

        public override int chooseMove(Board b)
        {
            int move = -1;
            string moveString;

            while (!b.legalMove(move))
            {
                UnityEngine.Debug.Log("Your move: ");
                moveString = Console.ReadLine();

                if (!int.TryParse(moveString, out move) || !b.legalMove(move))
					UnityEngine.Debug.Log("Illegal move. Try again.");
            }
            return move;
        }

        public override string gloat()
        {
            return "I WIN! Humans still rule.";
        }
    }
}
