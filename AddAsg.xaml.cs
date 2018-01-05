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
    public partial class AddAsg : Window
    {
        private string _asgname;
        public string AsgName
        {
            get
            {
                return this._asgname;
            }
        }

        private string _asgdesc;
        public string AsgDesc
        {
            get
            {
                return this._asgdesc;
            }
        }

        public bool closedwithok = false;

        public new void Closing()
        {
            closedwithok = true;
            this.Close();
        }

        public AddAsg()
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
                _asgname = NameBox.Text;
                _asgdesc = DescBox.Text;
                Closing();
            }
        }


    }
}
