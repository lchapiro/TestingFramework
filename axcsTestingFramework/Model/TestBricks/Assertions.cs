
namespace TestingFramework.Model
{
    /// <summary>
    /// Assert Equal Class
    /// </summary>
    class AssertEqual : TestClass
    {
        public AssertEqual(TestClass oBase) : base(oBase)
        {
            TestBrick = "AssertEqual";
        }

        public override string ExecuteTestStep()
        {
            if (InputString == InputFromOutput)
            {
                Logger.Instance.Write("EQ");
                return "EQ";
            }
            else
            {
                Logger.Instance.Write("ERROR");
                return string.Empty;
            }
        }
    }

    /// <summary>
    /// Assert Not Equal Class
    /// </summary>
    class AssertNonEqual : TestClass
    {
        public AssertNonEqual(TestClass oBase) : base(oBase)
        {
            TestBrick = "AssertNonEqual";
        }

        public override string ExecuteTestStep()
        {
            if (InputString != InputFromOutput)
            {
                Logger.Instance.Write("NON EQ");
                return "NON EQ";
            }
            else
            {
                Logger.Instance.Write("ERROR");
                return string.Empty;
            }
        }
    }
}
