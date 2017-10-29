using System;
using System.Collections.Generic;

namespace ep_cs
{
    public static partial class Solutions
    {
        public static int Solution001()
            => SumMultiples3and5(1000);

        private static int SumMultiples3and5(int n)
        {
            int result = 0;
            for (int i = 1; i < n; i++)
                if (i % 3 == 0 ||
                    i % 5 == 0)
                    result += i;
            return result;
        }

        public static int Solution002()
            => SumEvenFibonacciNumbers(4_000_000);

        private static int SumEvenFibonacciNumbers(int max)
        {
            int result = 0;
            // TODO: there is a potential optimization here, since even numbers appear in
            // this series in a predictable pattern (every 3rd element).  This current
            // implementation is well fast enough for current needs.
            foreach (int num in FibonacciNumbers())
            {
                if (num > max)
                    return result;
                if (num % 2 == 0)
                    result += num;
            }
            throw new Exception("never gets here");
        }

        private static IEnumerable<int> FibonacciNumbers()
        {
            yield return 1;
            yield return 2;
            int x = 1;
            int y = 2;
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

        public static long Solution003()
            => LargestPrimeFactor(600851475143);

        private static long LargestPrimeFactor(long n)
        {
            long remaining = n;
            long lastFactor = 1;
            // Find prime factors in ascending order.  The largest factor will be last.
            while (remaining > 1)
            {
                lastFactor = SmallestPrimeFactor(remaining);
                remaining /= lastFactor;
            }
            return lastFactor;
        }

        private static long SmallestPrimeFactor(long n)
        {
            if (n < 2)
                throw new Exception("never gets here");
            if (n % 2 == 0)
                return 2;
            if (n % 3 == 0)
                return 3;
            long factor = 5;
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

        public static int Solution004()
        {
            // Try each unique combination of three-digit numbers, looking for
            // max product that's a paliondrome.
            int maxProduct = 1;
            for (int left=100; left<=999; left++)
                for (int right=left; right<=999; right++)
                    if (left*right > maxProduct &&
                        IsPalindrome(left*right))
                    {
                        maxProduct = left * right;
                    }
            return maxProduct;
        }

        private static bool IsPalindrome(int n)
        {
            // Reverse the number.  If it's the same as the origina;, it's a palindrome
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

        public static int Solution005()
            => SmallestNumberDivisibleByAll(20);

        private static int SmallestNumberDivisibleByAll(int n)
        {
            // Get the prime factors for each numbers between 2 and n, keeping track
            // of repeats _within_each_individual_number.
            Dictionary<int, int> factors = new Dictionary<int, int>();
            for (int i=2; i<=n; i++)
            {
                var currFactors = GetPrimeFactors(i);
                foreach(var currFactor in currFactors)
                {
                    if (!factors.ContainsKey(currFactor.Key))
                        factors[currFactor.Key] = 1;
                    if (currFactor.Value > factors[currFactor.Key])
                        factors[currFactor.Key] = currFactor.Value;
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
    }
}
