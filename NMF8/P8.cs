using System;
using System.Linq;
using Extensions;
using static Extensions.ActionExtensions;

namespace NMF8
{
    class Program
    {
        static void Main(string[] args)
        {
            int N = 17, k = 2, w = k * N, a = 3, b = 4;
            var y1 = 0.1;
            var y2 = 0.1;
            double ux0(double y) => y * y + Math.Sin(w * 1.0 / 10);
            double uxL(double y) => y * y - 8 * y - 16 * Math.Cos(w * 1.0 / 10);
            double uy0(double x) => -x * x;
            double uyL(double x) => 9 - 6 * x - x * x;

            var YN = new double[a].Select((x, i) => uxL(i));
            var X0 = new double[b].Select((x, i) => uy0(i));
            var arr = new double[a].Select(x => new double[b]).ToArray();
            arr[0][0] = uy0(0);
            for (int i = 1; i < a; i++)
            {
                arr[0][i] = N * Math.Sin(k * Math.PI / 4) * Math.Cos(i * Math.PI / 4);
            }

            arr[0][b - 1] = uyL(b);
            arr.Print();

        }
    }
}
