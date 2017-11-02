using ep_cs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace unit_tests
{
    [TestClass]
    public class Solutions011to020Tests : BaseTest
    {
        [TestMethod]
        public void Solutions011to020Test()
        {
            Test(Solutions.Solution011, nameof(Solutions.Solution011), 70600674, TestSpeed.Fast);
            Test(Solutions.Solution012, nameof(Solutions.Solution012), 76576500, TestSpeed.Fast);
            Test(Solutions.Solution013, nameof(Solutions.Solution013), 5537376230L, TestSpeed.Fast);
            Test(Solutions.Solution014, nameof(Solutions.Solution014), 837799, TestSpeed.Normal);
            Test(Solutions.Solution015, nameof(Solutions.Solution015), 137846528820L, TestSpeed.Fast);
            Test(Solutions.Solution016, nameof(Solutions.Solution016), 1366, TestSpeed.Fast);
            Test(Solutions.Solution017, nameof(Solutions.Solution017), 21124, TestSpeed.Fast);
            Test(Solutions.Solution018, nameof(Solutions.Solution018), 1074, TestSpeed.Fast);
            Test(Solutions.Solution019, nameof(Solutions.Solution019), 171, TestSpeed.Fast);
        }
    }
}
