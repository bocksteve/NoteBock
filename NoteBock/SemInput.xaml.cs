using System;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SemInput : Window
    {


        public SemInput()
        {
            InitializeComponent();
        }

        private string _semname;
        public string SemName
        {
            get
            {
                return this._semname;
            }
        }

        private DateTime _startdate;
        public DateTime StartDate
        {
            get
            {
                return this._startdate;
            }
        }

        private DateTime _enddate;
        public DateTime EndDate
        {
            get
            {
                return this._enddate;
            }
        }

        public bool closedwithok = false;

        public new void Closing()
        {
            closedwithok = true;
            this.Close();
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            _startdate = (DateTime)Start_Date.SelectedDate;
            _enddate = (DateTime)End_Date.SelectedDate;
            _semname = SemesName.Text;
            if (_semname == "")
            {
                MessageBox.Show("Please insert a semester name");
            }
            else if (_startdate.DayOfWeek != DayOfWeek.Sunday)
            {
                MessageBox.Show("Start date must be Sunday");
            }
            else if (_enddate.DayOfWeek != DayOfWeek.Saturday)
            {
                MessageBox.Show("End date must be Saturday");
            }
            else
            {
                Closing();
            }
        }
    }
}
