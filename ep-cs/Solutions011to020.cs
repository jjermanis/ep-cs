using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;

namespace ep_cs
{
    public static partial class Solutions
    {
        public static int Solution011()
            => GetMaxProductInGrid(4);

        private static int GetMaxProductInGrid(int length)
        {
            const int GRID_WIDTH = 20;
            const int GRID_HEIGHT = 20;

            var grid = ReadGridFromResource("ep_cs.data.Problem11Grid.txt", GRID_WIDTH, GRID_HEIGHT);

            var maxProduct = 0;

            // TODO - clean this up - logic is duplicated.  

            // Try horizontal
            for (int y = 0; y < GRID_HEIGHT; y++)
                for (int startX = 0; startX <= GRID_WIDTH - length; startX++)
                {
                    var product = 1;
                    for (int x = startX; x < startX + length; x++)
                    {
                        product *= grid[x, y];
                        maxProduct = Math.Max(maxProduct, product);
                    }
                }
            // Try vertical
            for (int x = 0; x < GRID_WIDTH; x++)
                for (int startY = 0; startY <= GRID_HEIGHT - length; startY++)
                {
                    var product = 1;
                    for (int y = startY; y < startY + length; y++)
                    {
                        product *= grid[x, y];
                        maxProduct = Math.Max(maxProduct, product);
                    }
                }
            // Try diagonals (down to the right)
            for (int startX = 0; startX < GRID_WIDTH - length; startX++)
                for (int startY = 0; startY <= GRID_HEIGHT - length; startY++)
                {
                    var product = 1;
                    for (int offset = 0; offset < length; offset++)
                    {
                        product *= grid[startX + offset, startY + offset];
                        maxProduct = Math.Max(maxProduct, product);
                    }
                }
            // Try diagonals (down to the left)
            for (int startX = length - 1; startX < GRID_WIDTH; startX++)
                for (int startY = 0; startY <= GRID_HEIGHT - length; startY++)
                {
                    var product = 1;
                    for (int offset = 0; offset < length; offset++)
                    {
                        product *= grid[startX - offset, startY + offset];
                        maxProduct = Math.Max(maxProduct, product);
                    }
                }

            return maxProduct;
        }

        private static int[,] ReadGridFromResource(string resourceName, int width, int height)
        {
            var result = new int[width, height];
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var x = 0;
                while (!reader.EndOfStream)
                {
                    var rowData = reader.ReadLine().Split(' ');
                    for (var y = 0; y < width; y++)
                        result[x, y] = Int32.Parse(rowData[y]);
                    x++;
                }
            }
            return result;
        }


        public static int Solution012()
            => GetFirstTraingleNumberWithDivisors(500);

        private static int GetFirstTraingleNumberWithDivisors(int minDivisors)
        {
            int triangle = 0;
            int nextTermInc = 1;

            while (true)
            {
                triangle += nextTermInc++;
                var primeFactors = GetPrimeFactors(triangle);
                if (GetDivisorCount(primeFactors) > minDivisors)
                    return triangle;
            }
            throw new Exception("never gets here");
        }
        private static int GetDivisorCount(IReadOnlyDictionary<int, int> primeFactors)
        {
            int result = 1;
            foreach (var primeFactor in primeFactors)
                result *= (primeFactor.Value + 1);
            return result;
        }

        public static long Solution013()
            => SumOfNumbers(10);

        private static long SumOfNumbers(int digits)
        {
            var result = 0L;
            foreach (var val in ReadNumbersFromResource13("ep_cs.data.Problem13Numbers.txt"))
                result += val;
            return Int64.Parse(result.ToString().Substring(0, digits));
        }

        // TODO Clean up the logic around reading from files
        private static IList<long> ReadNumbersFromResource13(string resourceName)
        {
            var result = new List<long>();
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    var rowData = reader.ReadLine();
                    result.Add(Int64.Parse(rowData.Substring(0, 14)));
                }
            }
            return result;
        }

        public static int Solution014()
            => LongestCollatzSequence(1_000_000);

        private static int LongestCollatzSequence(int max)
        {
            var collatz = new CollatzSequenceMemoized(max * 3);
            var maxLenNum = 0;
            var maxLen = 0;
            for (int n = 1; n < max; n++)
            {
                var currLen = collatz.GetCollatzSequenceLength(n);
                if (currLen > maxLen)
                {
                    maxLen = currLen;
                    maxLenNum = n;
                }
            }
            return maxLenNum;
        }

        public static long Solution015()
            => DistinctPaths(20, 20);

        private static long DistinctPaths(int width, int height)
        {
            // Calculate with a Pascal matrix
            long[,] m = new long[width + 1, height + 1];

            // First row and first column are all 1
            for (int x = 0; x <= width; x++)
                m[x, 0] = 1;
            for (int y = 0; y <= height; y++)
                m[0, y] = 1;

            // Calcualte each successive row
            for (int y = 1; y <= height; y++)
                for (int x = 1; x <= width; x++)
                    m[x, y] = m[x - 1, y] + m[x, y - 1];
            return m[width, height];
        }

        public static int Solution016()
            => PowerDigitSum(2, 1000);

        private static int PowerDigitSum(int powerBase, int exponent)
        {
            // Very easy to do this using BigInteger
            var power = BigInteger.Pow(new BigInteger(powerBase), exponent);
            int result = 0;

            foreach (char c in power.ToString())
            {
                result += c - '0';
            }
            return result;
        }

        private static readonly IReadOnlyList<int> LETTERS_PER_DIGIT = new List<int>
        {
            4, // zero
            3, // one
            3, // two
            5, // three
            4, // four
            4, // five
            3, // six
            5, // seven
            5, // eight
            4, // nine
        };
        private static readonly IReadOnlyList<int> LETTERS_PER_TEEN = new List<int>
        {
            3, // ten
            6, // eleven
            6, // twelve
            8, // thirteen
            8, // fourteen
            7, // fifteen
            7, // sixteen
            9, // seventeen
            8, // eighteen
            8, // nineteen
        };
        private static readonly IReadOnlyList<int> LETTERS_PER_TEN = new List<int>
        {
            Int32.MinValue, // not valid
            Int32.MinValue, // not valid
            6, // twenty
            6, // thirty
            5, // forty
            5, // fifty
            5, // sixty
            7, // seventy
            6, // eighty
            6, // ninety
        };

        public static int Solution017()
            => WrittenNumberLetterCountForRange(1, 1000);

        private static int WrittenNumberLetterCountForRange(int start, int end)
        {
            var result = 0;
            for (int x = start; x <= end; x++)
                result += WrittenNumberLetterCount(x);
            return result;
        }

        private static int WrittenNumberLetterCount(int x)
        {
            const int MIN = 0;
            const int MAX = 1000;
            if (x < MIN || x > MAX)
                throw new ArgumentOutOfRangeException(
                    nameof(x), x, $"Only supports values between {MIN} and {MAX}");

            // Special case 1000
            if (x == 1000)
                return 11;

            var result = 0;

            var tensAndOnes = x % 100;
            if (tensAndOnes < 10)
                result = LETTERS_PER_DIGIT[tensAndOnes];
            else if (tensAndOnes < 20)
                result = LETTERS_PER_TEEN[tensAndOnes - 10];
            else
            {
                result = LETTERS_PER_TEN[tensAndOnes / 10];
                if (tensAndOnes % 10 != 0)
                    result += LETTERS_PER_DIGIT[tensAndOnes % 10];
            }

            var hundreds = x / 100;
            if (hundreds > 0)
            {
                if (tensAndOnes == 0)
                    result = 0;
                else
                    result += 3; // "and"

                result += 7; // "hundred"
                result += LETTERS_PER_DIGIT[hundreds];
            }
            return result;
        }

        public static int Solution018()
            => MaximumTriangleTotal("ep_cs.data.Problem18Triangle.txt");
        private static int MaximumTriangleTotal(string triangleResourceName)
        {
            var triangle = ReadTriangleFromResource(triangleResourceName);
            for (int row=triangle.Count-2; row>=0; row--)
            {
                for (int x = 0; x < triangle[row].Count; x++)
                    triangle[row][x] += Math.Max(triangle[row + 1][x], triangle[row + 1][x + 1]);
            }
            return triangle[0][0];
        }

        private static IList<IList<int>> ReadTriangleFromResource(string resourceName)
        {
            var result = new List<IList<int>>();
            var assembly = Assembly.GetExecutingAssembly();
            var temp = assembly.GetManifestResourceNames();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    var rawData = reader.ReadLine().Split(' ');
                    result.Add(rawData.Select(n => Int32.Parse(n)).ToList<int>());
                }
            }
            return result;
        }

        public static int Solution019()
            => FirstOfMonthSundaysSince1901(2000);

        private static readonly IReadOnlyList<int> DAYS_PER_MONTH =
            new List<int>(){31,28,31,30,31,30,31,31,30,31,30,31 };
        private static int FirstOfMonthSundaysSince1901(int endYear)
        {
            if (endYear < 1901)
                throw new ArgumentOutOfRangeException(nameof(endYear), endYear, $"{nameof(endYear)} must be 1901 or later");

            var result = 0;
            var dayOfWeek = 2; // 1/1/1901 was a Tuesday
            for (var year=1901; year <= endYear; year++)
            {
                foreach(int month in Enumerable.Range(0,12))
                {
                    var daysInMonth = DAYS_PER_MONTH[month];
                    if (month == 1 && DateTime.IsLeapYear(year))
                        daysInMonth++;
                    dayOfWeek = (dayOfWeek + daysInMonth) % 7;
                    if (dayOfWeek == 0)
                        result++;
                }
            }
            return result;
        }

        public static int Solution020()
            => FactorialDigitSum(100);

        private static int FactorialDigitSum(int n)
        {
            var factorial = Factorial(n);
            var result = 0;
            foreach (char c in factorial.ToString())
                result += c - '0';
            return result;
        }

        private static BigInteger Factorial(int n)
        {
            var result = BigInteger.One;

            for (var i = 2; i < n; i++)
                result = result * i;
            return result;
        }
    }
}
