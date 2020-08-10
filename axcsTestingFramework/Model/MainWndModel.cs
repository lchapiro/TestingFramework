using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace TestingFramework.Model
{
    class MainWndModel : INotifyPropertyChanged
    {
        private List<TestClass> _lstTests;
        public List<TestClass> GetTestRows() { return _lstTests; }
        public TestClass GetTestRowNr(int nRow) 
        { 
            if (nRow < _lstTests.Count)
                return _lstTests[nRow]; 
            else
                return null;
        }

        public void SetTestRowNr(TestClass oTest, int nRow)
        {
            if (oTest != null)
                _lstTests[nRow] = oTest;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWndModel()
        {
            _lstTests = new List<TestClass>();
        }

        public void NewRow()
        {
            _lstTests.Add(new TestClass());
        }

        public void DeleteRow(int nRow)
        {
            _lstTests.RemoveAt(nRow - 1);
        }

        public string ExecuteOneStep(TestClass oTest)
        {
            return oTest.ExecuteTestStep();
        }

        public bool BuildModelFromFile(string sPath)
        {
            bool bOk = true;
            _lstTests.Clear();
            TestClass oTest = null;
            int n = 0;

            try
            {
                XmlDocument oDom = new XmlDocument();
                oDom.Load(sPath);

                XmlNode oXmlNode = oDom.FirstChild;
                XmlNode oTestsNode = oXmlNode.NextSibling;

                foreach (XmlNode oNode in oTestsNode.ChildNodes)
                {
                    oTest = new TestClass();

                    foreach (XmlNode oCtrl in oNode.ChildNodes)
                    {
                        if (oCtrl.NodeType == XmlNodeType.Comment)
                            continue;

                        if (oCtrl.Name == "Nr")
                        {
                            if (Int32.TryParse(oCtrl.InnerText, out n))
                                oTest.Nr = n;
                        }

                        if (oCtrl.Name == "TestBrick")
                            oTest = TestBricksFactory.CreateTestBrick(oTest, oCtrl.InnerText);

                        if (oCtrl.Name == "InputString")
                            oTest.InputString = oCtrl.InnerText;

                        if (oCtrl.Name == "InputRow")
                        {
                            if (Int32.TryParse(oCtrl.InnerText, out n))
                                oTest.InputRow = n;
                        }

                    }

                    _lstTests.Add(oTest);
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Write(ex);
                bOk = false;
            }

            return bOk;
        }

        public bool SaveTest(string sPath)
        {
            bool bOk = true;

            try
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.NewLineOnAttributes = true;
                settings.Indent = true;

                using (XmlWriter writer = XmlWriter.Create(sPath, settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("TestSteps");

                    foreach (TestClass oTest in _lstTests)
                    {
                        writer.WriteStartElement("TestStep");
                        writer.WriteElementString("Nr", oTest.Nr.ToString());
                        writer.WriteElementString("TestBrick", oTest.TestBrick);
                        writer.WriteElementString("InputString", oTest.InputString);
                        writer.WriteElementString("InputRow", oTest.InputRow.ToString());
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Write(ex);
                bOk = false;
            }

            return bOk;
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
