using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkullCards
{
    //Attempted to be adapted from Javascript
    class BruteForce
    {
        private class JavaScriptDeck
        {
            public double Win { get; set; }
            public double Mult { get; set; }
        }

        static void JavaScriptRun()
        {
            JavaScriptDeck[,] deck = new JavaScriptDeck[100, 100];
            for (int i = 2; i <= Management.MAX_CARDS; i++)
            {
                for (int j = 1; j < i; j++)
                {
                    double s = Factorial(i - 2);
                    double ij = Factorial(i - j);
                    double fi = Factorial(i);
                    double sj = Factorial(i - 2 - j);
                    double sj1 = sj * (i - 1 - j);
                    deck[i, j] = new JavaScriptDeck()
                    {
                        Win = ((2 * s * ij) / (fi * sj1)) * j,
                        Mult = (s * ij) / (fi * sj)
                    };
                }
            }

            for (int i = 1; i < Management.MAX_CARDS; i++)
            {
                List<int> steps = new List<int>();
                steps.Add(i);
                JavaScriptIterate(steps, deck[Management.MAX_CARDS, i].Mult, deck[Management.MAX_CARDS, i].Win * 10, deck);
            }
        }

        private static double highScore = 7000;
        private static void JavaScriptIterate(List<int> steps, double mult, double win, JavaScriptDeck[,] deck)
        {
            int size = Management.MAX_CARDS;
            int turn = 10;
            foreach (int step in steps)
            {
                size -= step;
                turn--;
            }

            if (turn <= 0 || size <= 1)
            {
                if (win >= highScore)
                {
                    Console.WriteLine(win);
                    foreach (int step in steps)
                    {
                        Console.WriteLine(step);
                    }
                }
                return;
            }

            for (int i = 1; i < size; i++)
            {
                List<int> newSteps = new List<int>(steps);
                newSteps.Add(i);
                var newPoints = win + deck[size, i].Win * turn * mult;
                JavaScriptIterate(newSteps, deck[size, i].Mult * mult, newPoints, deck);
            }
        }

        static double Factorial(int i)
        {
            return Factorial((double)i);
        }

        static double Factorial(double d)
        {
            if (d <= 1)
            {
                return 1;
            }

            return d * Factorial(d - 1);
        }
    }
}
