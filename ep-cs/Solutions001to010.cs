using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ep_cs
{
    public static partial class Solutions
    {
        /// <summary>
        /// Find the sum of all the multiples of 3 or 5 below 1000.
        /// </summary>
        /// <returns></returns>
        public static int Solution001()
            => SumMultiples3and5(1000);

        /// <summary>
        /// Find the sum of all integers from 1 to n that are evenly divisible by 3 and/or 5
        /// </summary>
        public static int SumMultiples3and5(int n)
        {
            var result = 0;
            for (var i = 1; i < n; i++)
                if (i % 3 == 0 ||
                    i % 5 == 0)
                    result += i;
            return result;
        }

        /// <summary>
        /// Considering the terms in the Fibonacci sequence whose values do not exceed
        /// four million, find the sum of the even-valued terms.
        /// </summary>
        public static int Solution002()
            => SumEvenFibonacciNumbers(4_000_000);

        /// <summary>
        /// Find the sum of all even numbers that are less than or equal to max
        /// </summary>
        private static int SumEvenFibonacciNumbers(int max)
        {
            var result = 0;
            // NOTE: there is a potential optimization here, since even numbers appear in
            // this series in a predictable pattern (every 3rd element).  This current
            // implementation is well fast enough for current needs.
            foreach (var num in FibonacciNumbers())
            {
                if (num >= max)
                    break;
                if (num % 2 == 0)
                    result += num;
            }
            return result;
        }

        // TODO: use the implementation in math-cs
        private static IEnumerable<int> FibonacciNumbers()
        {
            yield return 1;
            yield return 1;
            int x = 1;
            int y = 1;
            // This will overflow as the series exceeds Int32.MaxValue.
            checked
            {
                while (true)
                {
                    var z = x + y;
                    yield return z;
                    x = y;
                    y = z;
                }
            }
        }

        /// <summary>
        /// What is the largest prime factor of the number 600851475143 ?
        /// </summary>
        public static long Solution003()
            => LargestPrimeFactor(600851475143);

        /// <summary>
        /// Find that largest prime factor of n
        /// </summary>
        public static long LargestPrimeFactor(long n)
        {
            var remaining = n;
            var lastFactor = 1L;
            // Find prime factors in ascending order.  The largest factor will be last.
            while (remaining > 1)
            {
                lastFactor = SmallestPrimeFactor(remaining);
                remaining /= lastFactor;
            }
            return lastFactor;
        }

        /// <summary>
        /// Find the smallest prime factor of n
        /// </summary>
        private static long SmallestPrimeFactor(long n)
        {
            if (n < 2)
                throw new Exception("never gets here");
            if (n % 2 == 0)
                return 2;
            if (n % 3 == 0)
                return 3;
            var factor = 5L;
            while (factor*factor<=n)
            {
                if (n % factor == 0)
                    return factor;
                if (n % (factor + 2) == 0)
                    return factor + 2;
                factor += 6;
            }
            // n must be prime
            return n;
        }

        /// <summary>
        /// Find the largest palindrome made from the product of two 3-digit numbers.
        /// </summary>
        public static int Solution004()
        {
            // Try each unique combination of three-digit numbers, looking for
            // max product that's a paliondrome.
            var maxProduct = 1;
            for (var left=100; left<=999; left++)
                for (var right=left; right<=999; right++)
                    if (left*right > maxProduct &&
                        IsPalindrome(left*right))
                    {
                        maxProduct = left * right;
                    }
            return maxProduct;
        }

        // TODO: move to math-cs?
        private static bool IsPalindrome(int n)
        {
            // Reverse the number.  If it's the same as the original, it's a palindrome
            var reversed = 0;
            var curr = n;
            while (curr > 0)
            {
                reversed *= 10;
                reversed += (curr % 10);
                curr /= 10;
            }
            return reversed == n;
        }

        /// <summary>
        /// What is the smallest positive number that is evenly divisible by all of the
        /// numbers from 1 to 20?
        /// </summary>
        public static int Solution005()
            => SmallestNumberDivisibleByAll(20);

        /// <summary>
        /// What is the smallest number that is evenly divisible by the integers
        /// between 1 and n?
        /// </summary>
        public static int SmallestNumberDivisibleByAll(int n)
        {
            // Get the prime factors for each numbers between 2 and n, keeping track
            // of repeats within each individual number.  We want the max of each
            // factor
            var factors = new Dictionary<int, int>();
            for (int i=2; i<=n; i++)
            {
                var currFactors = GetPrimeFactors(i);
                foreach(var currFactor in currFactors)
                {
                    if (!factors.ContainsKey(currFactor.Key))
                        factors[currFactor.Key] = currFactor.Value;
                    else
                        factors[currFactor.Key] = Math.Max(currFactor.Value, factors[currFactor.Key]);
                }
            }

            // The result is the product of all prime factors, again including repeats.
            int result = 1;
            foreach(var factor in factors)
            {
                for (int i = 0; i < factor.Value; i++)
                    result *= factor.Key;
            }
            return result;
        }

        // TODO - remove this, call out to math-cs
        private static IReadOnlyDictionary<int, int> GetPrimeFactors(int n)
        {
            var remaining = n;

            var result = new Dictionary<int, int>();
            while (remaining > 1)
            {
                var factor = (int)SmallestPrimeFactor(remaining);
                if (result.ContainsKey(factor))
                    result[factor]++;
                else
                    result[factor] = 1;
                remaining /= factor;
            }
            return result;
        }

        /// <summary>
        /// Find the difference between the sum of the squares of the first one hundred 
        /// natural numbers and the square of the sum.
        /// </summary>
        public static int Solution006()
            => SumSquareDifference(100);

        /// <summary>
        /// Returns the difference between the sum of square vs. square of sums for
        /// first n positive integers.
        /// Sum of squares: 1^2+2^2+...+n^2
        /// Square of sums: (1+2+...+n)^2
        /// </summary>
        public static int SumSquareDifference(int n)
        {
            // "Brute force" sum of squares
            var sumOfSquares = 0;
            for (int i = 1; i <= n; i++)
                sumOfSquares += i * i;

            // Use alegbra on square of sums
            var sums = n * (n + 1) / 2;
            var squareofSums = sums * sums;

            return squareofSums - sumOfSquares;
        }

        /// <summary>
        /// What is the 10 001st prime number?
        /// </summary>
        public static int Solution007()
            => GetNthPrime(10_001);


        /// <summary>
        /// Starting from 2, returns the nth prime number
        /// </summary>
        public static int GetNthPrime(int n)
        {
            // TODO - use a better algorithm here, on principle.  Something like Sieve
            // of Eratosthenes (where known prime are tracked) would be muich fast.
            // In reality, this will be more than fast enough for this problem.

            int count = 2;  // We'll skip 2 and 3

            // Check for odd numbers only.  We also skip numbers evenly divisible by 3.
            var curr = 5;
            while (count < n)
            {
                if (IsPrime(curr))
                    if (++count == n)
                        return curr;
                if (IsPrime(curr+2))
                    if (++count == n)
                        return curr+2;
                curr += 6;
            }
            throw new Exception("never gets here");
        }

        // TODO - use math-cs instead
        private static bool IsPrime(int n)
        {
            // 2 is the smallest prime
            if (n < 2)
                return false;

            // true iff n is 2 or 3
            if (n < 4)
                return true;

            // Special handling to check if number is divisible by 2 or 3
            if (n % 2 == 0 || n % 3 == 0)
                return false;

            // Since we have already checked for 2 and 3, we can skip even
            // numbers, and those evenly divisible by 3.
            var factor = 5;
            while (factor * factor <= n &&
                   factor <= 46341)
            {
                if (n % factor == 0 ||
                    n % (factor + 2) == 0)
                    return false;
                factor += 6;
            }
            return true;
        }

        /// <summary>
        /// Find the thirteen adjacent digits in the 1000-digit number that have
        /// the greatest product.
        /// </summary>
        public static long Solution008()
            => LargestAdjacentProduct(13);

        public static long LargestAdjacentProduct(int digits)
        {
            var numbers = ReadNumbersFromResource("ep_cs.data.Problem8Number.txt");

            long max = 0;
            for (int x = 0; x < 1000 - digits; x++)
            {
                long currVal = 1;
                for (int i = 0; i < digits; i++)
                    currVal *= numbers[x + i];
                if (currVal > max)
                    max = currVal;
            }
            return max;
        }

        /// <summary>
        /// There exists exactly one Pythagorean triplet for which a + b + c = 1000.
        /// Find the product abc.
        /// </summary>
        public static int Solution009()
            => ProductOfPythagoreanTripleSummingTo(1000);

        private static int ProductOfPythagoreanTripleSummingTo(int n)
        {
            foreach(var triple in PythagoreanTriples())
            {
                if (triple.Item1 + triple.Item2 + triple.Item3 == n)
                    return triple.Item1 * triple.Item2 * triple.Item3;
            }
            throw new Exception("never gets here");
        }

        // TODO: switch to C# 7 style tuples
        private static IEnumerable<Tuple<int,int,int>> PythagoreanTriples()
        {
            for (int r=2; ; r+=2)
            {
                int rsq = r * r;
                int st = rsq / 2;
                for (int factor=1; factor*factor<st; factor++)
                {
                    if (st % factor == 0)
                    {
                        yield return Tuple.Create(
                            r+factor, r+(st/factor), r+factor+(st/factor));
                    }
                }
            }
        }

        private static IList<int> ReadNumbersFromResource(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
 
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string rawData = reader.ReadToEnd();
                var result = new List<int>(rawData.Length);
                foreach (char digit in rawData)
                    if (digit >= '0' && digit <= '9')
                        result.Add(digit - '0');
                return result;
            }
        }

        /// <summary>
        /// Find the sum of all the primes below two million.
        /// </summary>
        public static long Solution010()
            => SumOfPrimes(2_000_000);

        public static long SumOfPrimes(int max)
        {
            // TODO - see Solution007 - use a better algorithm here, on principle.  
            // As before, this will be more than fast enough.

            long result = 5;  // We'll start with 2 and 3

            // Check for odd numbers only.  We also skip numbers evenly divisible by 3.
            var curr = 5;
            while (curr < max)
            {
                if (IsPrime(curr))
                    result += curr;
                // We don't recheck max on the 2nd value.  This is not a problem for this 
                // specific problem, but can fail for others.
                if (IsPrime(curr + 2))
                    result += curr + 2;
                curr += 6;
            }
            return result;
        }

    }
}
