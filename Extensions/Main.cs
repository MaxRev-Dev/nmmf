using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Extensions
{
    public static class ActionExtensions
    {
        /// <summary>
        /// This method implements <see cref="Console.WriteLine()"/>
        /// </summary>
        public static Action _ => Console.WriteLine;
    }
    public static class MatrixExtensions
    {
        public static double[,] MultiplyMatrix(this double[,] A, double[,] B)
        {
            int rA = A.GetLength(0);
            int cA = A.GetLength(1);
            int rB = B.GetLength(0);
            int cB = B.GetLength(1);

            double[,] kHasil = new double[rA, cB];
            if (cA != rB)
            {
                Console.WriteLine("matrices can't be multiplied !!");
            }
            else
            {
                for (int i = 0; i < rA; i++)
                {
                    for (int j = 0; j < cB; j++)
                    {
                        double temp = 0;
                        for (int k = 0; k < cA; k++)
                        {
                            temp += A[i, k] * B[k, j];
                        }
                        kHasil[i, j] = temp;
                    }
                }
                return kHasil;
            }

            return default;
        }
        /// <summary>
        /// Returns the row with number 'row' of this matrix as a 1D-Array.
        /// </summary>
        public static T[] GetRow<T>(this T[,] matrix, int row)
        {
            var rowLength = matrix.GetLength(1);
            var rowVector = new T[rowLength];

            for (var i = 0; i < rowLength; i++)
                rowVector[i] = matrix[row, i];

            return rowVector;
        }



        /// <summary>
        /// Sets the row with number 'row' of this 2D-matrix to the parameter 'rowVector'.
        /// </summary>
        public static void SetRow<T>(this T[,] matrix, int row, T[] rowVector)
        {
            var rowLength = matrix.GetLength(1);

            for (var i = 0; i < rowLength; i++)
                matrix[row, i] = rowVector[i];
        }



        /// <summary>
        /// Returns the column with number 'col' of this matrix as a 1D-Array.
        /// </summary>
        public static T[] GetCol<T>(this T[,] matrix, int col)
        {
            var colLength = matrix.GetLength(0);
            var colVector = new T[colLength];

            for (var i = 0; i < colLength; i++)
                colVector[i] = matrix[i, col];

            return colVector;
        }



        /// <summary>
        /// Sets the column with number 'col' of this 2D-matrix to the parameter 'colVector'.
        /// </summary>
        public static void SetCol<T>(this T[,] matrix, int col, T[] colVector)
        {
            var colLength = matrix.GetLength(0);

            for (var i = 0; i < colLength; i++)
                matrix[i, col] = colVector[i];
        }
    }
    public static class ArrayExtensions
    {
        public static void Print(this IEnumerable<double[]> arr, int tolerance = 5)
        {
            var asm = Assembly.GetEntryAssembly()?.DefinedTypes?.FirstOrDefault();
            Console.WriteLine($"Lab: {asm?.Namespace}");
            var arr0 = arr as double[][] ?? arr.ToArray();
            var a = arr0.Length;
            var b = arr0[0].Length;
            for (int i = a - 1; i >= 0; i--)
            {
                for (int j = 0; j < b; j++)
                {
                    var fx = arr0[i][j].ToString("f" + tolerance);
                    Console.Write((new string(' ', Math.Abs(tolerance * 2 - fx.Length)) + fx + ' '));
                }

                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public static void Print(this double[] arr, int tolerance = 5)
        {
            var a = arr.Length;
            for (int i = a - 1; i >= 0; i--)
            {
                var fx = arr[i].ToString("f" + tolerance);
                Console.Write((new string(' ', tolerance * 2 - fx.Length) + fx + ' '));
            }
            Console.WriteLine();
        }
    }
}
