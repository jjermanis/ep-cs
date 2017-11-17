using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
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
            => FibonacciTermWithMinDigits(1000);

        public static int FibonacciTermWithMinDigits(int min_digits)
        {
            // Phi is the golden ratio
            const double PHI = 1.6180339887;
            var LOG10PHI = Math.Log10(PHI);

            for (var n=1; true; n++ )
            {
                // The limit of the ratio of consecutive terms in the Fibonacci series approaches
                // the golden ratio.  Use this to approximate the nth term in the series.  Use log10
                // to get the number of digits
                var digits = n * LOG10PHI - Math.Log10(5.0) / 2.0;
                if (digits > min_digits-1)
                    return n;
            }
        }

        public static int Solution026()
            => LongestRepetend(1000);

        public static int LongestRepetend(int max)
        {
            var longest_n = 0;
            var longest_repetend = 0;
            BigInteger TEN = 10;

            for (int n = 2; n < max; n++)
            {
                // Only check prime numbers.  At worst, the repetend will be as long as one
                // of its factors.
                if (IsPrime(n))
                {
                    // The repetend length is the smallest x for which 10^x-1 % n is zero.
                    // TODO: x will always be a factor of n-1... we could check for that
                    for (int x = 1; x < n; x++)
                    {
                        if (BigInteger.ModPow(TEN, x, n).Equals(BigInteger.One))
                        {
                            if (x > longest_repetend)
                            {
                                longest_repetend = x;
                                longest_n = n;
                            }
                            break;
                        }
                    }
                }
            }
            return longest_n;
        }

        public static int Solution027()
            => GetQuadraticCoefficientProductWithMostPrimes(-1000, 1000);

        /// <summary>
        /// The function n^2+an+b can be used to generate primes.  For example,
        /// n^2+n+41 generates 40 primes, for values n=0 through 39.  This function
        /// returns the value of a*b, for the values of a and b that generate the most
        /// prime values starting at n=0.
        /// </summary>
        private static int GetQuadraticCoefficientProductWithMostPrimes(int min, int max)
        {
            // Precaulate primes.  Primes will be checked many, many times.
            const int MAX_PRIMES = 1000;   // This is hard-coded for the specific problem
            var primes = GetPrimes(MAX_PRIMES);
            var primes_sub_max = primes.Where(x => x < max);  

            var max_n = 0;  // This is the most primes found for any a and b
            int result = 0; // This is a*b corresponding to the max_n case

            // a must be odd.  If not, n^2+an+b will alternate between odd and even.
            // b must be prime.  When n=0, n^2+an+b equals b.
            var a_start = (min % 2 == 0) ? min + 1 : min;
            for (int a=a_start; a < max; a+=2)
                foreach (var b in primes_sub_max)
                {
                    var n = 0;
                    while (primes.Contains(n * n + a * n + b))
                        n++;
                    if (n > max_n)
                    {
                        max_n = n;
                        result = a * b;
                    }
                }
            return result;
        }

        public static HashSet<int> GetPrimes(int max)
        {
            var isComposites = new bool[max + 1];

            var curr_prime = 2;
            while (curr_prime * curr_prime <= max)
            {
                for (var curr = 2 * curr_prime; curr <= max; curr += curr_prime)
                    isComposites[curr] = true;
                curr_prime++;
                while (isComposites[curr_prime])
                    curr_prime++;
            }
            // TODO: is there an optimization if we know the size here?
            var result = new HashSet<int>();
            for (int x = 2; x <= max; x++)
                if (!isComposites[x])
                    result.Add(x);
            return result;
        }

        public static int Solution028()
            => SpiralDiagonalSum(1001);

        /// <summary>
        /// A spiral can be formed by adding sequential numbers to a grid in
        /// clockwise order.  Here is a spiral where size = 5:
        /// 21  22  23  24  25
        /// 20   7   8   9  10
        /// 19   6   1   2  11
        /// 18   5   4   3  12
        /// 17  16  15  14  13
        /// This function returns the sum of the numbers on the diagonals.  In the 
        /// above grid, this would return 101.
        /// </summary>
        public static int SpiralDiagonalSum(int size)
        {
            var result = 1;
            for (var curr = 3; curr <= size; curr += 2)
            {
                // For the diagonal going up and to the righ, the numbers are the squares
                // of odd numbers (e.g. 1, 9, 25, 49, etc.).  The other three numbers are
                // each (n-1) from the prior number.  The following is the sum of the four
                // "corners" at a specific grid size.
                result += (4 * curr * curr) - (6 * curr) + 6; 
            }
            return result;
        }

        public static int Solution029()
            => DistinctPowerTerms(100, 100);

        public static int DistinctPowerTerms(int max_a, int max_b)
        {
            var terms = new HashSet<BigInteger>();

            // There are probably clever ways to do this counting factors, but this is
            // blazing fast to solve via brute force (actually calculating the powers)
            // for the problem as stated.
            for (var a=2; a<=max_a; a++)
                for (var b=2; b<=max_b; b++)
                    terms.Add(BigInteger.Pow(a, b));
            return terms.Count;
        }
    }
}
