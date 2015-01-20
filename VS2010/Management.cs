using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkullCards
{
    public class Management
    {
        public readonly static int MAX_CARDS = 30;
        public readonly static int WINNINGS = 10000;
        public readonly static int PENALTY = 1000;

        public enum Result
        {
            Win,
            Loss,
            Null
        }

        public static List<List<bool>> GenerateDecks()
        {
            List<List<bool>> decks = new List<List<bool>>();

            List<bool> skullCards = new List<bool>();
            for (int n = 0; n < MAX_CARDS - 2; n++)
            {
                skullCards.Add(false);
            }

            List<bool> deck;

            for (int n = 0; n < MAX_CARDS - 2; n++)
            {
                for (int m = (n + 1); m < MAX_CARDS - 1; m++)
                {
                    deck = new List<bool>(skullCards);
                    deck.Insert(n, true);
                    deck.Insert(m, true);
                    decks.Add(deck);
                }
                deck = new List<bool>(skullCards);
                deck.Insert(n, true);
                deck.Add(true);
                decks.Add(deck);
            }

            deck = new List<bool>(skullCards);
            deck.Add(true);
            deck.Add(true);
            decks.Add(deck);
            return decks;
        }


        public static int ReportWinnings(List<List<bool>> decks, Func<List<bool>, int, IEnumerable<bool>> strat)
        {
            try
            {
                int totalWinnings = 0;

                for (int i = 0; i < decks.Count; i++)
                {
                    Strats.InitializeStrats();
                    totalWinnings += Play(decks[i], strat);
                }
                return totalWinnings;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static int Play(List<bool> deck, Func<List<bool>, int, IEnumerable<bool>> strat)
        {
            int winning = WINNINGS;
            while (true)
            {
                IEnumerable<bool> hand = strat(deck, winning);
                switch (ReportResult(hand))
                {
                    case Result.Win:
                        return winning;
                    case Result.Loss:
                        return 0;
                    case Result.Null:
                        winning = Math.Max(winning - PENALTY, 0);
                        break;
                }
            }
        }

        public static Result ReportResult(IEnumerable<bool> hand)
        {
            bool win = false;
            foreach (bool card in hand)
            {
                if (card && !win)
                {
                    win = true;
                }
                else if (card && win)
                {
                    return Result.Loss;
                }
            }

            if (win)
            {
                return Result.Win;
            }
            else
            {
                return Result.Null;
            }
        }
    }
}
