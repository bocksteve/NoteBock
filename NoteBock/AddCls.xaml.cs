using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NoteBock
{
    /// <summary>
    /// Interaction logic for AddCls.xaml
    /// </summary>
    public partial class AddCls : Window
    {
        private ArrayList _days = new ArrayList();
        public ArrayList Days
        {
            get
            {
                return this._days;
            }
        }

        private string _clsname;
        public string ClsName
        {
            get
            {
                return this._clsname;
            }
        }

        public AddCls()
        {
            InitializeComponent();
        }

        public bool closedwithok = false;

        public void Closing()
        {
            closedwithok = true;
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ClsNameBox.Text == "" || (Monday.IsChecked == false && Tuesday.IsChecked == false && Wednesday.IsChecked == false
                && Thursday.IsChecked == false && Friday.IsChecked == false))
            {
                MessageBox.Show("Please enter all fields");
            }
            else
            {
                _clsname = ClsNameBox.Text;
                if (Monday.IsChecked == true)
                {
                    _days.Add("Monday");
                }
                if (Tuesday.IsChecked == true)
                {
                    _days.Add("Tuesday");
                }
                if (Wednesday.IsChecked == true)
                {
                    _days.Add("Wednesday");
                }
                if (Thursday.IsChecked == true)
                {
                    _days.Add("Thursday");
                }
                if (Friday.IsChecked == true)
                {
                    _days.Add("Friday");
                }
                Closing();
            }
        }
    }
}
