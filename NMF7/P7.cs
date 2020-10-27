using System;
using System.Linq;
using Extensions;
using static Extensions.ActionExtensions;

namespace NMF7
{
    class Program
    {
        static void Main()
        {
            int b = 10,
                n = 17;
            double
                l = 0.5,
                h = 0.1,
                T = 0.05;

            var u = new double[b].Select(x => new double[b]).ToArray();
            double[] y0 = new double[b],
                yn = new double[b],
                xn = new double[b],
                x0 = new double[b];
            for (int i = 0; i < b; i++)
            {
                y0[i] = 0.5 * (0.5 * T + i);
                yn[i] = 2.25;
                xn[i] = Math.Pow(h * i, 2) * (h * i + 1);
            }

            for (int i = 1; i < b - 1; i++)
            {
                x0[i] = (x0[i - 1]) * Math.Sin(xn[i]);
            }

            x0[0] = y0[0];
            x0[b - 1] = yn[0];

            for (int i = 0; i < b; i++)
            {
                u[i][0] = y0[i];
                for (int j = 1; j < b - 1; j++)
                {
                    u[i][j] = x0[j] + T * xn[j] + Math.Pow(T, 2) *
                              (x0[j + 1] - 2 * x0[j] + x0[j - 1]) /
                                            (2 * Math.Pow(h, 2));
                }
                u[i][b - 1] = yn[i];
                x0 = u[i];
            }
            _();
            u.Reverse().Print();
        }
    }
}
