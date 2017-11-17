using ep_cs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace unit_tests
{
    [TestClass]
    public class Solutions021to030Tests : BaseTest
    {
        [TestMethod]
        public void Solutions021to030Test()
        {
            Test(Solutions.Solution021, nameof(Solutions.Solution021), 31626, TestSpeed.Fast);
            Test(Solutions.Solution022, nameof(Solutions.Solution022), 871198282, TestSpeed.Fast);
            Test(Solutions.Solution023, nameof(Solutions.Solution023), 4179871, TestSpeed.Normal);
            Test(Solutions.Solution024, nameof(Solutions.Solution024), 2783915460, TestSpeed.Fast);
            Test(Solutions.Solution025, nameof(Solutions.Solution025), 4782, TestSpeed.Fast);
            Test(Solutions.Solution026, nameof(Solutions.Solution026), 983, TestSpeed.Fast);
            Test(Solutions.Solution027, nameof(Solutions.Solution027), -59231, TestSpeed.Fast);
            Test(Solutions.Solution028, nameof(Solutions.Solution028), 669171001, TestSpeed.Fast);
            Test(Solutions.Solution029, nameof(Solutions.Solution029), 9183, TestSpeed.Fast);
            //Test(Solutions.Solution030, nameof(Solutions.Solution030), 443839, TestSpeed.Fast);
        }

        /// <summary>
        /// Testing of more cases related to Solution025 - first Fibonacci term with n digits
        /// </summary>
        [TestMethod]
        public void Solution025ImplementationTest()
        {
            Assert.AreEqual(7, Solutions.FibonacciTermWithMinDigits(2));
            Assert.AreEqual(12, Solutions.FibonacciTermWithMinDigits(3));
        }

        /// <summary>
        /// Testing of more cases related to Solution026 - reciprocal with longest repetend
        /// </summary>
        [TestMethod]
        public void Solution026ImplementationTest()
        {
            Assert.AreEqual(7, Solutions.LongestRepetend(10));
            Assert.AreEqual(29, Solutions.LongestRepetend(46));
        }

        /// <summary>
        /// Testing of more cases related to Solution028 - spiral diagonal sums
        /// </summary>
        [TestMethod]
        public void Solution028ImplementationTest()
        {
            Assert.AreEqual(101, Solutions.SpiralDiagonalSum(5));
        }

        /// <summary>
        /// Testing of more cases related to Solution029 - distinct powers
        /// </summary>
        [TestMethod]
        public void Solution029ImplementationTest()
        {
            Assert.AreEqual(15, Solutions.DistinctPowerTerms(5, 5));
        }
    }
}
