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
        /// <param name="n"></param>
        /// <returns></returns>
        public static int AmicableNumberForFactorsSum(int n)
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

        public static int FileNameScore(string nameFile)
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

    }
}
