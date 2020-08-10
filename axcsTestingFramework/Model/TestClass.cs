using System;

namespace TestingFramework.Model
{
    public class TestClass
    {
        public int Nr { get; set; }
        public string TestBrick { get; set; }

        public int InputRow { get; set; }

        public string InputString { get; set; }
        public string InputFromOutput { get; set; }

        public string Output { get; set; }

        public bool Check { get; set; }

        public virtual string ExecuteTestStep()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Std C'tor
        /// </summary>
        public TestClass()
        {
            Nr = -1;
            TestBrick = string.Empty;
            InputRow = -1;
            InputString = string.Empty;
            InputFromOutput = string.Empty;
            Output = string.Empty;
            Check = false;
        }

        /// <summary>
        /// Copy C'tor
        /// </summary>
        /// <param name="oBase"></param>
        public TestClass(TestClass oBase)
        {
            Nr = oBase.Nr;
            TestBrick = oBase.TestBrick;
            InputRow = oBase.InputRow;
            InputString = oBase.InputString;
            InputFromOutput = oBase.InputFromOutput;
            Output = oBase.Output;
            Check = oBase.Check;
        }
    }
}
