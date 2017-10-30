using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ep_cs
{
    public class CollatzSequenceMemoized
    {
        private IDictionary<long, int> sequenceLengths;

        public CollatzSequenceMemoized(int initialCacheSize)
        {
            sequenceLengths = new Dictionary<long, int>(initialCacheSize);
            sequenceLengths.Add(1, 1);
        }

        public int GetCollatzSequenceLength(long n)
        {
            if (sequenceLengths.ContainsKey(n))
                return sequenceLengths[n];
            int count;
            if (n % 2 == 0)
                count = 1 + GetCollatzSequenceLength(n / 2);
            else
                count = 1 + GetCollatzSequenceLength(3 * n + 1);
            sequenceLengths.Add(n, count);
            return count;
        }
    }
}
