using System;

class Program
{
    static void Main()
    {
        double a = 0.1, b = 1;
        double e = 0.0001;
        int k = 9, n = 25;

        double step = (b - a) / k;

        for (int i = 0; i <= k; i++)
        {
            double x = a + i * step;

            double Y = Math.Exp(x * Math.Cos(Math.PI / 4))
                     * Math.Cos(x * Math.Sin(Math.PI / 4));

            double SN = 1;
            double an = 1;

            for (int j = 1; j <= n; j++)
            {
                an *= x / j;
                SN += Math.Cos(j * Math.PI / 4) * an;
            }

            double SE = 1;
            double ae = 1;
            int m = 1;

            ae *= x / m;

            while (Math.Abs(ae) > e)
            {
                SE += Math.Cos(m * Math.PI / 4) * ae;

                m++;
                ae *= x / m;
            }

            Console.WriteLine(
                $"X = {x:F2}\tSN = {SN:F8}\tSE = {SE:F8}\tY = {Y:F8}"
            );
        }
    }
}