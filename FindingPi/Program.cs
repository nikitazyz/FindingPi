using System;
using System.Diagnostics;

namespace FindingPi
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string[] methods = new string[]
                {
                "Multiple rows",
                "Bailey–Borwein–Plouffe"
                };
                int dialogResult = ShowDialog("Choose method:", methods);
                double result;
                int iterations;
                Stopwatch stopwatch = new Stopwatch();
                switch (dialogResult)
                {
                    case 1:
                        Console.WriteLine("\nCalculating...");
                        stopwatch.Start();
                        result = MultipleRows(out iterations);
                        stopwatch.Stop();
                        break;
                    case 2:
                        Console.WriteLine("\nCalculating...");
                        stopwatch.Start();
                        result = BBP(out iterations);
                        stopwatch.Stop();
                        break;
                    default:
                        return;
                }


                string elapsedTime = stopwatch.ElapsedMilliseconds == 0 ? "< 0" : stopwatch.ElapsedMilliseconds.ToString();
                Console.WriteLine($"\n   Choosed method:   {methods[dialogResult - 1]}\n" +
                                    $"           Result:   {result}\n" +
                                    $"Iterations number:   {iterations}\n" +
                                    $"     Elapsed time:   {elapsedTime}ms");

                ConsoleColor defaultColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("#############################################\n");
                Console.ForegroundColor = defaultColor;
            }
        }

        /// <summary>
        /// Finding PI with Multiple rows method.
        /// </summary>
        /// <param name="iterations">Number of iterations</param>
        /// <returns>PI</returns>
        static double MultipleRows(out int iterations)
        {
            int k = 1;
            double result = 0;
            double old = 0;
            do
            {
                for (int m = 1; m <= k; m++)
                {
                    old = result;
                    result += 1 / (m * Math.Pow(k + 1, 3));
                }
                k++;
            }
            while (result != old);
            result = Math.Pow(360 * result, (double)1 / 4);
            iterations = k;
            return result;
        }

        /// <summary>
        /// Finding PI with Bailey–Borwein–Plouffe method.
        /// </summary>
        /// <param name="iterations">Number of iterations</param>
        /// <returns>PI</returns>
        static double BBP(out int iterations)
        {
            int i = 0;
            double result = 0;
            double old;

            do
            {
                old = result;
                result += Math.Pow(16, -i) * (4.0 / (8 * i + 1) - 2.0 / (8 * i + 4) - 1.0 / (8 * i + 5) - 1.0 / (8 * i + 6));
                i++;
            } while (result != old);
            iterations = i + 1;
            return result;
        }

        /// <summary>
        /// Create dialog in console.
        /// </summary>
        /// <param name="title">Message before parameters</param>
        /// <param name="parameters">Parameters of dialog. Value must be between 1 and 9</param>
        /// <returns>Returns number of choosen parameter (starts with 1) or 0 if escape key pressed. </returns>
        static int ShowDialog(string title, params string[] parameters)
        {
            if (parameters.Length > 9 && parameters.Length < 1) throw new Exception("Parameters count out of range.");
            Console.WriteLine(title);
            for (int i = 0; i < parameters.Length; i++)
            {
                Console.WriteLine($"  {i + 1}. {parameters[i]}");
            }
            Console.WriteLine("   \"Escape\" to exit");
            bool parse;
            Console.Write($"\nEnter number [1-{parameters.Length}]: ");
            int choosedParameter;
            do
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape) return 0;
                parse = int.TryParse(key.KeyChar.ToString(), out choosedParameter) && choosedParameter > 0 && choosedParameter <= parameters.Length;
                if (!parse)
                {
                    Console.Write($"\nValue must be between 1 and {parameters.Length}: ");
                }
            }
            while (!parse);

            Console.WriteLine();
            return choosedParameter;
        }
    }
}

