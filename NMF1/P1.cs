using System;
using System.Linq;

namespace NMF1
{
    internal class Program
    {
        private static void Main()
        {

            int b = 5,
                a = 4,
                s = 2;
            double
                //omg = 1,
                tx0 = 12,
                txb = 90,
                k1 = (txb - tx0) / Math.Pow(a - 1, s),
                k2 = tx0;
            double eps = 0.1;

            int i, j;

            double[][] u = new double[a].Select(x => new double[b].ToArray()).ToArray();
            u[0] = new double[b].Select(x => tx0).ToArray();
            u[a - 1] = new double[b].Select(x => txb).ToArray();
            for (i = 1; i < a - 1; i++)
                u[i][0] = u[i][b - 1] = k1 * Math.Pow(i, s) + k2;

            for (i = 1; i < a - 1; i++)
                for (j = 1; j < b - 1; j++)
                    u[i][j] = (u[0][0] + u[a - 1][0] + u[i][0] * 2) * 1.0 / 4;

            for (i = a - 1; i >= 0; i--)
            {
                for (j = 0; j < b; j++)
                {
                    var fx = u[i][j].ToString("f5");
                    Console.Write((new string(' ', 10 - fx.Length) + fx + ' '));
                }

                Console.WriteLine();
            }
            Console.WriteLine();
            double currentVal;
            var it = 0;
            var p = (Math.Cos(Math.PI / a) + Math.Cos(Math.PI / b)) / 2;
            var omg = 2.0 / (1 + Math.Sqrt(1 - Math.Pow(p, 2)));
            do
            {
                currentVal = 0;
                for (i = 1; i < a - 1; i++)
                    for (j = 1; j < b - 1; j++)
                    {
                        var iter = (u[i - 1][j] + u[i + 1][j] + u[i][j - 1] + u[i][j + 1]);
                        var back = u[i][j];
                        u[i][j] = (omg / 4) * iter + (1 - omg) * back;
                        var diff = Math.Abs(u[i][j] - back);
                        if (diff > currentVal)
                            currentVal = diff;
                    }

                ++it;
            }
            while (currentVal > eps);

            for (i = a-1; i >= 0; i--)
            {
                for (j = 0; j < b; j++)
                {
                    var fx = u[i][j].ToString("f5");
                    Console.Write((new string(' ', 10 - fx.Length) + fx + ' '));
                }

                Console.WriteLine();
            }
            Console.WriteLine($"Iterations: {it}");
            Console.ReadKey();
        }
    }
}
