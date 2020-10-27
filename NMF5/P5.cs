using System;
using System.Linq;
using Extensions;
using static Extensions.ActionExtensions;

namespace NMF5
{
    internal class Program
    {
        private static void Main()
        {
            int b = 5;
            //int _m = 17;
            //double
            //    _a0 = 10;
            Exec(10, 17);
            Exec(10, 0);
            void Exec(double a0, double m)
            {
                int layers = 4;
                var u = new double[layers].Select(x => new double[b + 1]).ToArray();
                double[]
                    alfa = new double[b],
                    beta = new double[b],
                    _a = new double[b],
                    _b = new double[b],
                    _c = new double[b],
                    kappa = new double[b],
                    rPlus = new double[b],
                    rMinus = new double[b],
                    rAr = new double[b];

                double R(double x) => m * Math.Cos(Math.PI) * x;

                var
                    h = 0.2;
                double Kappa(double x) => 1.0 / (1 + 0.5 * h * Math.Abs(R(x)));

                double Rps(double x) => 0.5 * (R(x) + Math.Abs(R(x)));

                double Rmn(double x) => 0.5 * (R(x) - Math.Abs(R(x)));

                _();

                double U0(double x, bool v = true) =>
                    a0 * Math.Pow(Math.Sin(v ? m * x : x), 2);

                double
                    l = 1,
                    tau = 1;
                for (var i = 0; i < (int)(l / h); i++)
                {
                    var xi = i * h;
                    kappa[i] = Kappa(xi);
                    rPlus[i] = Rps(xi);
                    rMinus[i] = Rmn(xi);
                    rAr[i] = R(xi);
                    u[0][i] = U0(xi);
                }

                u[0][0] = a0;
                u[0][b] = U0(m, false); //a0 * Math.Pow(Math.Sin(m), 2);

                var h2 = Math.Pow(h, 2);
                for (var j = 0; j < b; j++)
                {
                    _a[j] = kappa[j] / h2 - rMinus[j] / h;
                    _b[j] = kappa[j] / h2 + rPlus[j] / h;
                    _c[j] = 1 / tau + 2 * kappa[j] / h2 + rPlus[j] / h - rMinus[j] / h;
                }

                for (var it = 1; it < layers; it++)
                {
                    u[it][0] = a0;
                    u[it][b] = U0(m, false); //a0 * Math.Pow(Math.Sin(m), 2);
                    beta[0] = a0;
                    for (var i = 1; i < b; i++)
                    {
                        alfa[i] = _b[i - 1] /
                                  (_c[i - 1] - alfa[i - 1] * _a[i - 1]);
                        beta[i] = (_a[i - 1] * beta[i - 1] + u[it - 1][i]) /
                                  (_c[i - 1] - alfa[i - 1] * _a[i - 1]);
                    }

                    for (var j = b - 1; j > 0; j--)
                        u[it][j] = alfa[j] * u[it][j + 1] + beta[j];
                }

                u.Reverse().Print();
            }
        }
    }
}
//IEnumerable<double> Uxi(double[][] mx, int layer)
//{
//    for (int index = 0; index < mx[layer].Length - 1; index++)
//        yield return (mx[layer + 1][index + 1] - mx[layer + 1][index]) / h;
//}
//IEnumerable<double> U_xi(double[][] mx, int layer)
//{
//    for (int index = 1; index < mx[layer].Length - 1; index++)
//        yield return (mx[layer + 1][index] - mx[layer + 1][index - 1]) / h;
//}
//IEnumerable<double> U_xxi(double[][] mx, int layer)
//{
//    for (int index = 0; index < mx[layer].Length - 1; index++)
//        yield return (mx[layer + 1][index] - 2 * mx[layer + 1][index] + mx[layer + 1][index + 1]) / Math.Pow(h, 2);
//}