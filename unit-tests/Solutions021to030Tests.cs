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
            //Test(Solutions.Solution026, nameof(Solutions.Solution026), 983, TestSpeed.Fast);
            //Test(Solutions.Solution027, nameof(Solutions.Solution027), -59231, TestSpeed.Fast);
            //Test(Solutions.Solution028, nameof(Solutions.Solution028), 669171001, TestSpeed.Fast);
            //Test(Solutions.Solution029, nameof(Solutions.Solution029), 9183, TestSpeed.Fast);
            //Test(Solutions.Solution030, nameof(Solutions.Solution030), 443839, TestSpeed.Fast);
        }
    }
}
