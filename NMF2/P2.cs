using System;
using System.Linq;
using Extensions;

namespace NMF2
{
    class Program
    {
        static void Main()
        {

            int b = 5,
                a = 4,
                s = 2,
                m = a,i;
            double
                //omg = 1,
                tx0 = 17,
                txb = 850,
                //k1 = (txb - tx0) / Math.Pow(a - 1, s),
                //k2 = tx0,
               // sigma = 1.0 / 6,
                l = 0.5,
                h = 0.1; 


            double[][] u = new double[a + 1].Select(x => new double[b + 1].ToArray()).ToArray();
            var len = (int)(l / h) + 1;
            var f = new double[len];
            var t0 = new double[len];
            double gx(double k) => Math.Pow(k, s + 1);
            for (i = 0; i < len; i++)
            {
                f[i] = h * i;
                t0[i] = (txb - tx0) * gx(f[i]) / gx(l) + tx0;
            }

            for (i = 0; i <= b; i++)
            {
                u[a][i] = f[i];
                u[a - 1][i] = t0[i];
            }

            for (i = m - 2; i >= 0; i--)
            {
                u[i][0] = t0[0];
                u[i][b] = t0[b];

                for (int k = 1; k < b; k++)
                {
                    u[i][k] = (u[i + 1][k - 1] + 4 * u[i + 1][k] + u[i + 1][k + 1]) * 1.0 / 6;
                }
            }
            u.Print();
        }
    }
}
