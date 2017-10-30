using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ep_cs
{
    public static partial class Solutions
    {
        public static int Solution014()
            => LongestCollzatzSequence(1_000_000);

        private static int LongestCollzatzSequence(int max)
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

    }
}
