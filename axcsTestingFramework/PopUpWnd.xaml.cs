using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TestingFramework
{
    /// <summary>
    /// Interaktionslogik für PopUpWnd.xaml
    /// </summary>
    public partial class PopUpWnd : Window
    {
        private int _nRow;
        public int GetFirstValue() { return _nRow; }

        public PopUpWnd()
        {
            InitializeComponent();
            _nRow = 0;
        }

        public void SetText(string str)
        {
            txtOne.Text = str;
        }

        public void SetCmbBoxes(List<string> lst)
        {
            if (lst == null)
            {
                cbxOne.IsEnabled = false;
                txtOne.IsEnabled = false;
            }
            else
            {
                foreach (string str in lst)
                    cbxOne.Items.Add(str);

                cbxOne.SelectedIndex = 0;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string strOne = cbxOne.Text;

            if (!string.IsNullOrWhiteSpace(strOne))
                Int32.TryParse(strOne, out _nRow);

            DialogResult = true;

            Close();
        }
    }
}
