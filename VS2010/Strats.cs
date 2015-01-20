using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkullCards
{
    public class Strats
    {
        static bool flip = false;
        static int defaultPratchet = (int)(Management.MAX_CARDS / (Management.WINNINGS / (double)Management.PENALTY));
        static int pratchet = defaultPratchet;
        static int ratchet = 3;
        static int customIndex = 0;
        static int[] customDraws = { 7, 6, 5, 3, 3, 2, 1, 1, 1 };
        public static double customPercent = 0.25;
        public static void InitializeStrats()
        {
            flip = false;
            pratchet = defaultPratchet;
            ratchet = 3;
            customIndex = 0;
        }

        static IEnumerable<bool> Draw(List<bool> deck, int count)
        {
            int toTake = (int)Math.Min(deck.Count, count);
            IEnumerable<bool> hand = deck.Take(toTake).ToArray();
            deck.RemoveRange(0, toTake);
            return hand;
        }

        static IEnumerable<bool> DrawAndReport(List<bool> deck, int count, out Management.Result result)
        {
            IEnumerable<bool> hand = Draw(deck, count);
            result = Management.ReportResult(hand);
            return hand;
        }

        public static IEnumerable<bool> DrawThree(List<bool> deck, int winning)
        {
            int toTake = 3;
            if (deck.Count <= 3)
            {
                toTake = 2;
            }

            return Draw(deck, toTake);
        }

        public static IEnumerable<bool> DrawOne(List<bool> deck, int winning)
        {
            return Draw(deck, 1);
        }

        public static IEnumerable<bool> DrawSeven(List<bool> deck, int winning)
        {
            return Draw(deck, deck.Count > 7 ? 7 : 1);
        }

        public static IEnumerable<bool> BigSmall(List<bool> deck, int winning)
        {
            IEnumerable<bool> hand = Draw(deck, deck.Count > 5 ? (flip ? 5 : 7) : 1);
            flip = true;
            return hand;
        }

        public static IEnumerable<bool> PRatcheting(List<bool> deck, int winning)
        {
            int toTake = pratchet;
            if (deck.Count <= pratchet)
            {
                toTake = pratchet = defaultPratchet - 2;
            }

            Management.Result result;
            IEnumerable<bool> hand = DrawAndReport(deck, toTake, out result);
            if (result == Management.Result.Null)
            {
                pratchet++;
            }

            return hand;
        }

        public static IEnumerable<bool> Ratcheting(List<bool> deck, int winning)
        {
            int toTake = ratchet;
            if (deck.Count <= ratchet)
            {
                toTake = ratchet = 1;
            }

            Management.Result result;
            IEnumerable<bool> hand = DrawAndReport(deck, toTake, out result);
            if (result == Management.Result.Null)
            {
                ratchet++;
            }

            return hand;
        }

        public static IEnumerable<bool> DrawThirdDown(List<bool> deck, int winning)
        {
            int toTake = deck.Count == 3 ? 1 : (int)Math.Max(Math.Floor(deck.Count / 3.0), 2.0);
            return Draw(deck, toTake);
        }

        public static IEnumerable<bool> DrawThirdUp(List<bool> deck, int winning)
        {
            int toTake = deck.Count == 3 ? 1 : (int)Math.Max(Math.Ceiling(deck.Count / 3.0), 2.0);
            return Draw(deck, toTake);
        }

        public static IEnumerable<bool> DrawCustomPercent(List<bool> deck, int winning)
        {
            int toTake = (int)Math.Ceiling(deck.Count * customPercent);
            return Draw(deck, toTake);
        }

        public static IEnumerable<bool> DrawQuarter(List<bool> deck, int winning)
        {
            int toTake = (int)Math.Ceiling(deck.Count * 0.25);
            return Draw(deck, toTake);
        }

        public static IEnumerable<bool> DrawHalf(List<bool> deck, int winning)
        {
            int toTake = deck.Count == 3 ? 1 : (int)Math.Max(Math.Floor(deck.Count / 2.0), 2.0);
            return Draw(deck, toTake);
        }

        public static IEnumerable<bool> DrawCustom(List<bool> deck, int winning)
        {
            return Draw(deck, customDraws[customIndex++]);
        }
    }
}
