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
            //Test(Solutions.Solution011, nameof(Solutions.Solution011), 70600674, TestSpeed.Fast);
            //Test(Solutions.Solution012, nameof(Solutions.Solution012), 76576500, TestSpeed.Fast);
            //Test(Solutions.Solution013, nameof(Solutions.Solution013), 5537376230, TestSpeed.Fast);

            Test(Solutions.Solution014, nameof(Solutions.Solution014), 837799, TestSpeed.Normal);

            //Test(Solutions.Solution015, nameof(Solutions.Solution015), 137846528820, TestSpeed.Normal);
        }
    }
}
