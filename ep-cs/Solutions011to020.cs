using System;
using System.Collections.Generic;
using System.IO;
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
            for (int y=0; y< GRID_HEIGHT; y++)
                for (int startX=0; startX<=GRID_WIDTH-length; startX++)
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
                        product *= grid[startX+offset, startY+offset];
                        maxProduct = Math.Max(maxProduct, product);
                    }
                }
            // Try diagonals (down to the left)
            for (int startX = length-1; startX < GRID_WIDTH; startX++)
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

            while(true)
            {
                triangle += nextTermInc++;
                var primeFactors = GetPrimeFactors(triangle);
                if (GetDivisorCount(primeFactors) > minDivisors)
                    return triangle;
            }
            throw new Exception("never gets here");
        }
        private static int GetDivisorCount(IReadOnlyDictionary<int,int> primeFactors)
        {
            int result = 1;
            foreach (var primeFactor in primeFactors)
                result *= (primeFactor.Value+1);
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
            var collatz = new CollatzSequenceMemoized(max*3);
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
            => DistinctPaths(20,20);

        private static long DistinctPaths(int width, int height)
        {
            // Calculate with a Pascal matrix
            long[,] m = new long[width+1, height + 1];

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
    }
}
