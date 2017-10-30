using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace unit_tests
{
    /// <summary>
    /// Expected performance standards for a test (in milliseconds).  The maximum standard (per 
    /// the site) is that a solution can be computed in under one minute.
    /// </summary>
    public enum TestSpeed
    {
        Fast = 100,
        Normal = 1000,
        Slow = 10000,
        VerySlow = 60000
    };

    public abstract class BaseTest
    {
        // Runs the specified test.  Checks result and performance time.
        protected void Test<T>(Func<T> func, string nameofFunc, T expected, TestSpeed speed)
        {
            long start = DateTime.Now.Ticks;
            var result = func();
            long duration = (DateTime.Now.Ticks - start) / TimeSpan.TicksPerMillisecond;
            Assert.AreEqual(expected, result,
                $"Wrong result for {nameofFunc}.  Expected: {expected}, got: {result}");
            Assert.IsTrue(duration < (int)speed,
                $"{nameofFunc} was too slow.  Took {duration} ms");
        }
    }
}
