using System;
using System.Collections.Generic;

namespace TestingFramework.Model
{
    public class TestBricksFactory
    {
        private static readonly object Sync = new object();
        private static TestBricksFactory _instance;
        public List<string> ListTestBricks { get; set; }

        private TestBricksFactory()
        {
            ListTestBricks = new List<string>();

            ListTestBricks.Add("OneStep");
            ListTestBricks.Add("FindRegExp");
            ListTestBricks.Add("AssertEqual");
            ListTestBricks.Add("AssertEmpty");
            ListTestBricks.Add("AssertNonEqual");

            // Add HERE a new test brick!
        }

        public static TestBricksFactory Instance
        {
            get
            {
                lock (Sync)
                {
                    if (_instance == null)
                        _instance = new TestBricksFactory();
                }

                return _instance;
            }
        }

        /// <summary>
        /// Create a new Test Bick dynamically (reflection) by reference to its Name
        /// </summary>
        /// <param name="oBase"></param>
        /// <param name="sName"></param>
        /// <returns></returns>
        public static TestClass CreateTestBrick(TestClass oBase, string sName)
        {
            Logger.Instance.Write("Creating Test Brick: " + sName + " ...");
            string sBrickName = "TestingFramework.Model." + sName;

            var oType = Type.GetType(sBrickName);
            TestClass oClass = null;

            if (oType != null)
                oClass = (TestClass)Activator.CreateInstance(oType, oBase);

            return oClass;
        }
    }
}
