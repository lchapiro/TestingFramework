using System.Text.RegularExpressions;

namespace TestingFramework.Model
{
    class FindRegExp : TestClass
    {
        public FindRegExp(TestClass oBase) : base(oBase)
        {
            TestBrick = "FindRegExp";
        }

        public override string ExecuteTestStep()
        {
            string sXml = InputFromOutput;
            string sFind = InputString;

            Logger.Instance.Write("Search for " + sFind);

            int nTimes = Regex.Matches(sXml, sFind).Count;
            Logger.Instance.Write("Found " + nTimes + " times");

            return nTimes.ToString();
        }
    }
}
