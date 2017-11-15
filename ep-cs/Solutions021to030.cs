using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ep_cs
{
    public static partial class Solutions
    {
        /// <summary>
        /// Let d(n) be defined as the sum of proper divisors of n (numbers less than n 
        /// which divide evenly into n). If d(a) = b and d(b) = a, where a ≠ b, then 
        /// a and b are an amicable pair and each of a and b are called amicable numbers.
        ///Evaluate the sum of all the amicable numbers under 10000.
        /// </summary>
        public static int Solution021()
            => AmicableNumberForFactorsSum(10_000);

        /// <summary>
        /// 
        /// </summary>
        private static int AmicableNumberForFactorsSum(int n)
        {
            // Calculate all factor sums in range
            var divisorSums = new int[n];
            for (var i = 0; i < n; i++)
                divisorSums[i] = SumProperDivisors(i);

            int result = 0;
            for (var i = 0; i < n; i++)
            {
                var currSum = divisorSums[i];
                if (currSum < n &&
                    currSum != i &&                 // Don't count cases where number is its own pair
                    divisorSums[currSum] == i)
                    result += i;
            }
            return result;
        }

        /// <summary>
        /// Return the sum of all positive divisors of n, which are less than n.
        /// </summary>
        private static int SumProperDivisors(int n)
        {
            int result = 1; // 1 is always a divisor, but n is not
            var factor = 2;
            while (factor*factor<n)
            {
                if (n % factor == 0)
                {
                    // Add the factor, and its cofactor
                    result += factor;
                    result += (n / factor);
                }
                factor++;
            }
            // Handle the case for a perfect square - it's its own cofactor
            if (factor * factor == n)
                result += factor;

            return result;
        }

        public static int Solution022()
            => FileNameScore("ep_cs.data.Problem22Names.txt");

        private static int FileNameScore(string nameFile)
        {
            var names = ReadStringsFromResource(nameFile);
            names.Sort();
            int result = 0;
            for (var i = 0; i < names.Count; i++)
                result += NameScore(names[i]) * (i + 1);
            return result;
        }

        private static int NameScore(string name)
        {
            int result = 0;
            foreach (var c in name.ToUpper())
                result += c - 'A' + 1;
            return result;
        }

        private static readonly char[] TRIM_CHARS = new char[] { '"' };
        /// <summary>
        /// 
        /// </summary>
        private static List<string> ReadStringsFromResource(string resourceName)
        {
            var result = new List<string>();
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {

                while (!reader.EndOfStream)
                {
                    var rowData = reader.ReadLine();
                    var words = rowData.Split(',');
                    foreach (var word in words)
                        result.Add(word.Trim(TRIM_CHARS));
                }
            }
            return result;
        }

        public static int Solution023()
            => SumOfAbundantNumberSums(28123);

        private static int SumOfAbundantNumberSums(int max)
        {
            var abundantNums = AbundantNumbers(max);
            bool[] isSumofAbundants = new bool[max + 1];
            for (int x = 0; x < abundantNums.Count; x++)
                for (int y = x; y < abundantNums.Count; y++)
                {
                    var sum = abundantNums[x] + abundantNums[y];
                    if (sum <= max)
                        isSumofAbundants[sum] = true;
                }
            int result = 0;
            for (int x = 1; x < max; x++)
                if (!isSumofAbundants[x])
                    result += x;
            return result;
        }

        private static List<int> AbundantNumbers(int max)
        {
            var result = new List<int>();
            for (int x = 2; x <= max; x++)
                if (SumProperDivisors(x) > x)
                    result.Add(x);
            return result;
        }

        public static long Solution024()
            => LexographicPermutation(10, 1_000_000);

        private static long LexographicPermutation(int digits, int x)
        {
            long result = 0;
            var remaining = x-1;
            var avail_digits = new List<int>();
            for (int i = 0; i < 10; i++)
                avail_digits.Add(i);

            for (int d=digits-1; d > 0; d--)
            {
                int fact = factorial(d);
                int curr_index = (remaining) / fact;
                remaining = (remaining) % fact;
                int curr_digit = avail_digits[curr_index];
                avail_digits.Remove(curr_digit);
                result = (result * 10) + curr_digit;

            }
            result = (result * 10) + avail_digits[0];
            return result;
        }

        private static int factorial(int n)
        {
            int result = 1;
            for (int i = 1; i <= n; i++)
                result *= i;
            return result;
        }

        public static int Solution025()
            => FibonacciDigits(1000);

        private static int FibonacciDigits(int min_digits)
        {
            const double PHI = 1.6180339887;
            var LOG10PHI = Math.Log10(PHI);

            for (var x=0; true; x++ )
            {
                var digits = x * LOG10PHI - Math.Log10(5.0) / 2.0;
                if (digits > min_digits-1)
                    return x;
            }
        }
    }
}
