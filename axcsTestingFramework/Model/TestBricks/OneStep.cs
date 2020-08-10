using System.Windows;

namespace TestingFramework.Model
{
    class OneStep : TestClass
    {
        public OneStep(TestClass oBase) : base(oBase)
        {
            TestBrick = "OneStep";
        }

        public override string ExecuteTestStep()
        {
            MessageBoxResult res = MessageBox.Show(InputString, 
                "Would you like to continue?",
                MessageBoxButton.YesNo);

            if (MessageBoxResult.Yes == res)
                return "OK";
            else
                return "KO";
        }
    }
}
