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
    /// Interaction logic for AddAss.xaml
    /// </summary>
    public partial class AddAss : Window
    {
        private string _assname;
        public string AssName
        {
            get
            {
                return this._assname;
            }
        }

        private string _assdesc;
        public string AssDesc
        {
            get
            {
                return this._assdesc;
            }
        }

        public bool closedwithok = false;

        public new void Closing()
        {
            closedwithok = true;
            this.Close();
        }

        public AddAss()
        {
            InitializeComponent();
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text == "")
            {
                MessageBox.Show("Please insert a name");
            }
            else
            {
                _assname = NameBox.Text;
                _assdesc = DescBox.Text;
                Closing();
            }
        }


    }
}
