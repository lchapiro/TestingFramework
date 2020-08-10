using TestingFramework.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TestingFramework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWndModel _mainModel;

        public MainWindow()
        {
            InitializeComponent();

            _mainModel = new MainWndModel();
            DataContext = _mainModel;

            WorkerSingleton.Instance.ReadArgs();
            string sInputFile = WorkerSingleton.Instance.GetInputFile();

            if (!string.IsNullOrEmpty(sInputFile))
                _mainModel.BuildModelFromFile(sInputFile);

            CreateDataGrid();
        }

        private void CreateDataGrid()
        {
            // Clear grid
            grdData.ColumnDefinitions.Clear();
            grdData.RowDefinitions.Clear();

            // Create Columns
            int nCol = 0;
            int nRow = 0;

            RowDefinition gridRow = new RowDefinition();
            gridRow.Height = new GridLength(25);
            grdData.RowDefinitions.Add(gridRow);

            // Nr.
            ColumnDefinition cdNr = new ColumnDefinition();
            cdNr.Width = new GridLength(25.00);
            TextBlock txtBlock = new TextBlock();
            txtBlock.Text = "Nr.";
            txtBlock.HorizontalAlignment = HorizontalAlignment.Center;
            grdData.ColumnDefinitions.Add(cdNr);

            Grid.SetRow(txtBlock, nRow);
            Grid.SetColumn(txtBlock, nCol++);

            txtBlock.Tag = null;
            grdData.Children.Add(txtBlock);

            // Test Brick
            ColumnDefinition cdTB = new ColumnDefinition();
            cdTB.Width = new GridLength(125.00);
            txtBlock = new TextBlock();
            txtBlock.Text = "Test Brick";
            txtBlock.HorizontalAlignment = HorizontalAlignment.Center;
            grdData.ColumnDefinitions.Add(cdTB);

            Grid.SetRow(txtBlock, nRow);
            Grid.SetColumn(txtBlock, nCol++);

            txtBlock.Tag = null;
            grdData.Children.Add(txtBlock);

            // Input Row
            ColumnDefinition cdIR = new ColumnDefinition();
            cdIR.Width = new GridLength(75.00);
            txtBlock = new TextBlock();
            txtBlock.Text = "Input Row";
            txtBlock.HorizontalAlignment = HorizontalAlignment.Center;
            grdData.ColumnDefinitions.Add(cdIR);

            Grid.SetRow(txtBlock, nRow);
            Grid.SetColumn(txtBlock, nCol++);

            txtBlock.Tag = null;
            grdData.Children.Add(txtBlock);

            // Input String
            grdData.ColumnDefinitions.Add(new ColumnDefinition());
            txtBlock = new TextBlock();
            txtBlock.Text = "Input String";
            txtBlock.HorizontalAlignment = HorizontalAlignment.Center;

            Grid.SetRow(txtBlock, nRow);
            Grid.SetColumn(txtBlock, nCol++);

            txtBlock.Tag = null;
            grdData.Children.Add(txtBlock);

            // Output
            grdData.ColumnDefinitions.Add(new ColumnDefinition());
            txtBlock = new TextBlock();
            txtBlock.Text = "Output";
            txtBlock.HorizontalAlignment = HorizontalAlignment.Center;

            Grid.SetRow(txtBlock, nRow);
            Grid.SetColumn(txtBlock, nCol++);

            txtBlock.Tag = null;
            grdData.Children.Add(txtBlock);

            // Check (OK/KO)
            ColumnDefinition cdCH = new ColumnDefinition();
            cdCH.Width = new GridLength(50.00);
            txtBlock = new TextBlock();
            txtBlock.Text = "Check";
            txtBlock.HorizontalAlignment = HorizontalAlignment.Center;
            grdData.ColumnDefinitions.Add(cdCH);

            Grid.SetRow(txtBlock, nRow);
            Grid.SetColumn(txtBlock, nCol++);

            txtBlock.Tag = null;
            grdData.Children.Add(txtBlock);

            // Create Data Rows
            foreach (TestClass oTest in _mainModel.GetTestRows())
            {
                nCol = 0;
                ++nRow;

                gridRow = new RowDefinition();
                gridRow.Height = new GridLength(25);

                grdData.RowDefinitions.Add(gridRow);

                // Nr.
                TextBox txt = new TextBox();
                txt.Text = nRow.ToString();
                oTest.Nr = nRow;
                txt.IsEnabled = false;

                Grid.SetRow(txt, nRow);
                Grid.SetColumn(txt, nCol);

                txt.Tag = nRow;
                grdData.Children.Add(txt);
                nCol++;

                // Test Bricks
                ComboBox cmb = new ComboBox();
                
                foreach (string str in TestBricksFactory.Instance.ListTestBricks)
                    cmb.Items.Add(str);

                cmb.IsEditable = false;
                cmb.Text = oTest.TestBrick;
                cmb.SelectionChanged += new SelectionChangedEventHandler(TestBrick_SelectionChanged);
                cmb.Tag = nRow;

                Grid.SetRow(cmb, nRow);
                Grid.SetColumn(cmb, nCol);

                grdData.Children.Add(cmb);
                nCol++;

                // Input Row
                cmb = new ComboBox();
                cmb.Items.Add(string.Empty);

                for (int n = 1; n < nRow; n++)
                    cmb.Items.Add(n);
              
                cmb.IsEditable = false;
                cmb.Text = oTest.InputRow.ToString();
                cmb.SelectionChanged += new SelectionChangedEventHandler(InputRow_SelectionChanged);
                cmb.Tag = nRow;

                if (nRow == 1)
                {
                    cmb.Text = string.Empty;
                    cmb.IsEnabled = false;
                }

                Grid.SetRow(cmb, nRow);
                Grid.SetColumn(cmb, nCol);

                grdData.Children.Add(cmb);
                nCol++;

                // Input String
                txt = new TextBox();
                Grid.SetRow(txt, nRow);
                Grid.SetColumn(txt, nCol);
                txt.Text = oTest.InputString;
                txt.ToolTip = oTest.InputString;

                txt.TextChanged += new TextChangedEventHandler(InputString_TextChanged);
                txt.MouseDoubleClick += new MouseButtonEventHandler(InputString_DoubleClick);

                txt.Tag = nRow;
                grdData.Children.Add(txt);
                nCol++;

                // Output
                txt = new TextBox();
                Grid.SetRow(txt, nRow);
                Grid.SetColumn(txt, nCol);
                txt.Text = oTest.Output;
                txt.ToolTip = oTest.Output;
                //txt.IsEnabled = false;

                txt.TextChanged += new TextChangedEventHandler(Output_TextChanged);

                txt.Tag = nRow;
                grdData.Children.Add(txt);
                nCol++;

                // Check
                CheckBox cb = new CheckBox();
                cb.IsEnabled = false;
                cb.IsChecked = oTest.Check;

                Grid.SetRow(cb, nRow);
                Grid.SetColumn(cb, nCol);

                cb.Tag = nRow;
                cb.HorizontalAlignment = HorizontalAlignment.Center;
                cb.VerticalAlignment = VerticalAlignment.Center;

                grdData.Children.Add(cb);
                nCol++;
            }
        }

        private void InputString_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Control ctrl = (Control)sender;
            if (ctrl is TextBox)
            {
                TextBox tb = (TextBox)ctrl;
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = Environment.CurrentDirectory;

                if (openFileDialog.ShowDialog() == true)
                    tb.Text = openFileDialog.FileName;
            }
        }

        private void Output_TextChanged(object sender, TextChangedEventArgs e)
        {
            Control ctrl = (Control)sender;
            if (ctrl is TextBox)
            {
                TextBox tb = (TextBox)ctrl;
                int nRow = (int)tb.Tag;

                TestClass oTest = _mainModel.GetTestRowNr(nRow-1);
                if (oTest != null)
                    oTest.Output = tb.Text;
            }
        }

        private void InputString_TextChanged(object sender, TextChangedEventArgs e)
        {
            Control ctrl = (Control)sender;
            if (ctrl is TextBox)
            {
                TextBox tb = (TextBox)ctrl;
                int nRow = (int)tb.Tag;

                TestClass oTest = _mainModel.GetTestRowNr(nRow-1);
                if (oTest != null)
                    oTest.InputString = tb.Text;
            }
        }

        private void TestBrick_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Control ctrl = (Control)sender;
            if (ctrl is ComboBox)
            {
                ComboBox cmb = (ComboBox)ctrl;
                int nRow = (int)cmb.Tag;

                TestClass oTest = _mainModel.GetTestRowNr(nRow-1);
                if (oTest != null)
                {
                    oTest = TestBricksFactory.CreateTestBrick(oTest, cmb.SelectedValue as string);
                    _mainModel.SetTestRowNr(oTest, nRow-1);
                }
                  
            }
        }

        private void InputRow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Control ctrl = (Control)sender;
            if (ctrl is ComboBox)
            {
                ComboBox cmb = (ComboBox)ctrl;
                int nRow = (int)cmb.Tag;

                TestClass oTest = _mainModel.GetTestRowNr(nRow-1);

                if (oTest != null)
                {
                    string sTmp = cmb.SelectedValue.ToString();

                    if (Int32.TryParse(sTmp, out nRow))
                        oTest.InputRow = nRow;
                    else
                        oTest.InputRow = -1;
                }
                else
                    oTest.InputRow = -1;
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            _mainModel.NewRow();

            CreateDataGrid();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            PopUpWnd wnd = new PopUpWnd();
            wnd.Owner = this;

            wnd.SetText("Row:");

            List<string> lstOne = new List<string>();

            for (int i = 1; i <= _mainModel.GetTestRows().Count; i++)
                lstOne.Add(i.ToString());

            wnd.SetCmbBoxes(lstOne);

            Nullable<bool> bOk = wnd.ShowDialog();

            if (true == bOk)
            {
                _mainModel.DeleteRow(wnd.GetFirstValue());
                CreateDataGrid();
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            TestClass oTest = null;
            List<TestClass> lst = _mainModel.GetTestRows();
            bool bOk = true;
            int i = 0;

            for (i = 0; i < lst.Count; i++)
            {
                oTest = lst[i];

                Logger.Instance.Write("STEP " + oTest.Nr);
                string sOutput = _mainModel.ExecuteOneStep(oTest);
                
                if (!string.IsNullOrEmpty(sOutput))
                {
                    oTest.Output = sOutput;
                    oTest.Check = true;
                    MoveOutputToInput(oTest);

                    CreateDataGrid();
                }
                else
                {
                    oTest.Output = string.Empty;
                    oTest.Check = false;

                    bOk = false;
                    break;
                }
            }

            if (bOk)
            {
                MessageBox.Show(this, "Test successful!", "OK", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Logger.Instance.Write("**** Test OK! ****");
            }
            else
            {
                string sMsg = string.Format("Error on Step {0}", i + 1);
                MessageBox.Show(this, sMsg, "Error (KO)", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Logger.Instance.Write("**** Test FAILED! ****: ");
                Logger.Instance.Write(sMsg);
            }

        }

        private void MoveOutputToInput(TestClass oCurTest)
        {
            foreach(TestClass oTest in _mainModel.GetTestRows())
            {
                if (oTest.InputRow == oCurTest.Nr)
                    oTest.InputFromOutput = oCurTest.Output;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            // TODO: MessageBox -> Do you want to save?

            Close();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            openFileDialog.Filter = "ETF Files|*.etf";

            string sPath;
            if (openFileDialog.ShowDialog() == true)
            {
                sPath = openFileDialog.FileName;

                if (!_mainModel.BuildModelFromFile(sPath))
                    MessageBox.Show(this, "Error occured!", "Error on Load", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                    CreateDataGrid();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.InitialDirectory = Environment.CurrentDirectory;
            fileDialog.DefaultExt = "etf";
            fileDialog.Filter = "ETF Files|*.etf";

            if (fileDialog.ShowDialog() == true)
            {
                string sPath = fileDialog.FileName;
                if (_mainModel.SaveTest(sPath))
                    MessageBox.Show(this, "Test has been saved!", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show(this, "Error occured!", "Not saved", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
