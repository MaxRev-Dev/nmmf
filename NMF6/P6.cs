using System;
using System.Linq;
using Extensions;
using static Extensions.ActionExtensions;

namespace NMF6
{
    class Program
    {
        static void Main()
        {
            int b = 5,
                layers = 4,
                n = 17;
            double
                l = 0.5,
                h = 0.1,
                T0 = n,
                Tn = 50 * n,
                a1 = 1.0 / n,
                T = 4.192307692,
                s = 2,
                ro = 500,
                lamda = 0.184,
                c = 0.84;
            var apw = lamda * 1.0 / (c * ro);

            var u = new double[layers].Select(x => new double[b + 1]).ToArray();
            double[]
                alfa = new double[b],
                beta = new double[b],
                _a = new double[b],
                _b = new double[b],
                _c = new double[b],
                k = new double[b * 2];

            _();

            double gx(double kv) => Math.Pow(kv, s + 1);
            for (int i = 0; i < b; i++)
            {
                u[0][i] = (Tn - T0) * gx(h * i) / gx(l) + T0;
            }
            for (var i = 1; i < b * 2; i++)
                k[i] = apw * (1 + Math.Exp(-a1 * i));

            u[0][0] = T0;
            u[0][b] = Tn;

            var h2 = Math.Pow(h, 2);
            for (var i = 0; i < b - 1; i++)
            {
                _a[i] = T * k[i * 2 + 1] / h2;
                _b[i] = T * k[i * 2 + 2] / h2;
                _c[i] = 1 + _a[i] + _b[i];
            }

            for (var it = 1; it < layers; it++)
            {
                u[it][0] = T0;
                u[it][b] = Tn;
                beta[0] = T0;
                for (var i = 1; i < b; i++)
                {
                    alfa[i] = _b[i - 1] /
                              (_c[i - 1] - alfa[i - 1] * _a[i - 1]);
                    beta[i] = (_a[i - 1] * beta[i - 1] + u[it - 1][i]) /
                              (_c[i - 1] - alfa[i - 1] * _a[i - 1]);
                }

                for (var j = b - 1; j > 0; j--) u[it][j] = alfa[j] * u[it][j + 1] + beta[j];
            }

            u.Reverse().Print();
        }
    }
}
