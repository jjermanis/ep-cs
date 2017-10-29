using ep_cs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace unit_tests
{
    [TestClass]
    public class Solutions001to010Tests : BaseTest
    {
        /// <summary>
        /// Testing against the results of the specific problems from the site.
        /// </summary>
        [TestMethod]
        public void SolutionsTest()
        {
            Test(Solutions.Solution001, nameof(Solutions.Solution001), 233168, TestSpeed.Fast);
            Test(Solutions.Solution002, nameof(Solutions.Solution002), 4613732, TestSpeed.Fast);
            Test(Solutions.Solution003, nameof(Solutions.Solution003), 6857, TestSpeed.Fast);
            Test(Solutions.Solution004, nameof(Solutions.Solution004), 906609, TestSpeed.Fast);
            Test(Solutions.Solution005, nameof(Solutions.Solution005), 232792560, TestSpeed.Fast);
        }
    }
}
