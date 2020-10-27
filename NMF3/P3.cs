using System;
using System.Linq;
using Extensions;
using static Extensions.ActionExtensions;

namespace NMF3
{
    class Program
    {
        static void Main()
        {
            int b = 5,
                a = 3,
                s = 2,
                m = a,
                i;
            double
                tx0 = 17,
                txb = 850,
                l = 0.5,
                h = 0.1,
                sigma = 1.0 / 6,
                ro = 500,
                lamda = 0.184,
                c = 0.84;
            var apw = lamda * 1.0 / (c * ro);
            var tau = Math.Pow(h, 2) * sigma / apw;
            double[][] u = new double[a + 1].Select(x => new double[b + 1].ToArray()).ToArray();
             
            double gx(double k) => Math.Pow(k, s + 1);
            for (i = 0; i < (int)(l / h) + 1; i++)
            {
                u[0][i] = (txb - tx0) * gx(h * i) / gx(l) + tx0;
            }

            double[]
                alfa = new double[b],
                beta = new double[b],
                _a = new double[b],
                _b = new double[b],
                _c = new double[b];

            for (int j = 0; j < b; j++)
            {
                _a[j] = _b[j] = apw * tau * 1.0 / Math.Pow(h, 2);
                _c[j] = 1 + sigma * 2;
            }

            _();

            for (int it = 1; it <= m; it++)
            {
                u[it][0] = tx0;
                u[it][b] = txb;
                beta[0] = tx0;

                for (i = 1; i <= b - 1; i++)
                {
                    alfa[i] = _b[i] * 1.0 / (_c[i] - alfa[i - 1] * _a[i]);
                    beta[i] = (_a[i - 1] * beta[i - 1] + u[it - 1][i]) * 1.0 / (_c[i] - alfa[i - 1] * _a[i]);
                } 
                for (int j = b - 1; j > 0; j--)
                {
                    u[it][j] = alfa[j] * u[it][j + 1] + beta[j];
                }
            }

            u.Reverse().Print();
        }
    }
}
