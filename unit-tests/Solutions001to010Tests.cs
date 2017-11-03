using ep_cs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace unit_tests
{
    [TestClass]
    public class Solutions001to010Tests : BaseTest
    {
        private PrivateType _imp; 

        [TestInitialize]
        public void Init()
        {
            _imp = new PrivateType(typeof(Solutions001to010Tests));
        }

        /// <summary>
        /// Testing against the results of the specific problems from the site.
        /// </summary>
        [TestMethod]
        public void Solutions001to010Test()
        {
            Test(Solutions.Solution001, nameof(Solutions.Solution001), 233168, TestSpeed.Fast);
            Test(Solutions.Solution002, nameof(Solutions.Solution002), 4613732, TestSpeed.Fast);
            Test(Solutions.Solution003, nameof(Solutions.Solution003), 6857, TestSpeed.Fast);
            Test(Solutions.Solution004, nameof(Solutions.Solution004), 906609, TestSpeed.Fast);
            Test(Solutions.Solution005, nameof(Solutions.Solution005), 232792560, TestSpeed.Fast);
            Test(Solutions.Solution006, nameof(Solutions.Solution006), 25164150, TestSpeed.Fast);
            Test(Solutions.Solution007, nameof(Solutions.Solution007), 104743, TestSpeed.Fast);
            Test(Solutions.Solution008, nameof(Solutions.Solution008), 23514624000L, TestSpeed.Fast);
            Test(Solutions.Solution009, nameof(Solutions.Solution009), 31875000, TestSpeed.Fast);
            Test(Solutions.Solution010, nameof(Solutions.Solution010), 142913828922L, TestSpeed.Normal);
        }

        /// <summary>
        /// Testing of more cases related to Solution001 - sum of multiples of 3 or 5
        /// </summary>
        [TestMethod]
        public void Solution001ImplementationTest()
        {
            Assert.AreEqual(23, Solutions.SumMultiples3and5(10));
        }

        /// <summary>
        /// Testing of more cases related to Solution003 - largest prime factor
        /// </summary>
        [TestMethod]
        public void Solution003ImplementationTest()
        {
            Assert.AreEqual(29, Solutions.LargestPrimeFactor(13195));
        }

        /// <summary>
        /// Testing of more cases related to Solution005 - evenly disivible number
        /// </summary>
        [TestMethod]
        public void Solution005ImplementationTest()
        {
            Assert.AreEqual(2520, Solutions.SmallestNumberDivisibleByAll(10));
        }

    }
}
