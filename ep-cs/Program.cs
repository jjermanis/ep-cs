using System;

namespace ep_cs
{
    class Program
    {
        static void Main(string[] args)
        {
            long startTime = DateTime.Now.Ticks;
            var result = Solutions.Solution009();
            var time = (DateTime.Now.Ticks - startTime) / (double)TimeSpan.TicksPerMillisecond;
            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"Duration: {time} ms");
        }
    }
}
