using System;
using System.Collections.Generic;
using System.Linq;

namespace SkullCards
{
    static class Program
    {
        static void Main()
        {
            List<List<bool>> decks = Management.GenerateDecks();

            //RunAndReport(decks, Strats.DrawThree);
            //RunAndReport(decks, Strats.Ratcheting);
            //RunAndReport(decks, Strats.PRatcheting);
            //RunAndReport(decks, Strats.DrawOne);
            //RunAndReport(decks, Strats.DrawSeven);
            //RunAndReport(decks, Strats.BigSmall);
            //RunAndReport(decks, Strats.DrawHalf);
            //RunAndReport(decks, Strats.DrawCustom);
            //RunAndReport(decks, Strats.DrawThirdUp);
            //RunAndReport(decks, Strats.DrawThirdDown);
            //RunAndReport(decks, Strats.DrawCustomPercent);
            RunAndReport(decks, Strats.DrawQuarter);
            Console.ReadLine();
        }

        static void OptimizeCustomPercent(List<List<bool>> decks)
        {
            for (Strats.customPercent = 1; Strats.customPercent > 0; Strats.customPercent -= 0.01)
            {
                Console.Write(Strats.customPercent);
                Console.Write(' ');
                RunAndReport(decks, Strats.DrawCustomPercent);
            }
        }

        static void RunAndReport(List<List<bool>> decks, Func<List<bool>, int, IEnumerable<bool>> strat)
        {
            int r = Management.ReportWinnings(Clone(decks), strat);
            Console.WriteLine("{0} made {1:C}, or {2:C} on average", strat.Method.Name, r, (double)r / decks.Count);
        }

        static List<List<bool>> Clone(List<List<bool>> listToClone)
        {
            return listToClone.Select(item => Clone(item)).ToList();
        }

        static List<bool> Clone(List<bool> listToClone)
        {
            return listToClone.Select(item => item).ToList();
        }
    }
}
