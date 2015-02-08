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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NoteBock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ArrayList Sems = new ArrayList();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            //TreeViewItem cur = (TreeViewItem)Tree.SelectedItem;
            //if (cur.))
            //this.AddCls.IsEnabled = false;
        }

        private void AddSemClick(object sender, RoutedEventArgs e)
        {
            //New Window for semeseter information input
            var newWindow = new SemInput();
            newWindow.ShowDialog();

            //Make sure OK was pressed, otherwise ignore newwindow
            if (newWindow.closedwithok == true)
            {
                //Create the tree item and add it
                TreeViewItem newS = new TreeViewItem() { Header = newWindow.SemName, Name = "sem_" + newWindow.SemName };
                Tree.Items.Add(newS);

                //Create Semester object, add it to the arraylist
                Semester newSem = new Semester(newWindow.StartDate, newWindow.EndDate, newWindow.SemName);
                Sems.Add(newSem);
            }

            /*Calculate number of weeks
            DateTime cur = newWindow.StartDate;
            int numdays = 0;
            while (cur != newWindow.EndDate)
            {
                numdays++;
                cur = cur.AddDays(1);
            }
            int weeks = (numdays+1)/7;
            //Since start is always sunday, end is always saturday, numdays+1 will always be a multiple of 7


            for (int i = 0; i < weeks; i++)
            {
                //Add Week i for every week of classes for semester
                //Each item is named "week_x-SemName"
                newS.Items.Add(new TreeViewItem() { Header = "Week " + (i + 1), Name = "week_" + (i + 1)});
            }
             * */
            //JK!!  Need to do this on a per-class basis, not per semester
        }

        private void RemSemClick(object sender, RoutedEventArgs e)
        {

        }

        private void AddClsClick(object sender, RoutedEventArgs e)
        {
            //New window for class info input
            var newWindow = new AddCls();
            newWindow.ShowDialog();

            //Must have selected a semester to even click this button, record which semester and create new class
            TreeViewItem cur = (TreeViewItem)Tree.SelectedItem;
            TreeViewItem newCls = new TreeViewItem() { Header = newWindow.ClsName, Name = "cls_" + newWindow.ClsName };
            cur.Items.Add(newCls);

            //Look for Semester to get dates
            for (int i = 0; i < Sems.Count; i++)
            {
                Semester sem = (Semester)Sems[i];
                if (cur.Name.Contains(sem.Name))
                {
                    //Found semester
                    for (int j = 0; j < sem.weeks; j++)
                    {
                        //Add Week i for every week of classes for semester
                        //Each item is named "week_x-SemName"
                        TreeViewItem week = new TreeViewItem() { Header = "Week " + (j + 1), Name = "week_" + (j + 1) };
                        newCls.Items.Add(week);
                        for (int h = 0; h < newWindow.Days.Count; h++)
                        {
                            //Add each day for every week
                            if (newWindow.Days[h] == "Monday")
                            {
                                DateTime dt = sem.Start.AddDays(1+(j*7));
                                week.Items.Add(new TreeViewItem() { Header = (dt.Date) });
                            }
                            else if (newWindow.Days[h] == "Tuesday")
                            {
                                DateTime dt = sem.Start.AddDays(2 + (j * 7));
                                week.Items.Add(new TreeViewItem() { Header = (dt.Date) });
                            }
                            else if (newWindow.Days[h] == "Wednesday")
                            {
                                DateTime dt = sem.Start.AddDays(3 + (j * 7));
                                week.Items.Add(new TreeViewItem() { Header = (dt.Date) });
                            }
                            else if (newWindow.Days[h] == "Thursday")
                            {
                                DateTime dt = sem.Start.AddDays(4 + (j * 7));
                                week.Items.Add(new TreeViewItem() { Header = (dt.Date) });
                            }
                            else if (newWindow.Days[h] == "Friday")
                            {
                                DateTime dt = sem.Start.AddDays(5 + (j * 7));
                                week.Items.Add(new TreeViewItem() { Header = (dt.Date) });
                            }
                            
                        }
                    }
                }
            }
        }

        private void RemClsClick(object sender, RoutedEventArgs e)
        {

        }

        private void AddAssClick(object sender, RoutedEventArgs e)
        {

        }

        private void RemAssClick(object sender, RoutedEventArgs e)
        {

        }

        private void BegNtsClick(object sender, RoutedEventArgs e)
        {

        }

        private void SrchNtsClick(object sender, RoutedEventArgs e)
        {

        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {

        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {

        }

        private void OnSubmenuOpened(object sender, RoutedEventArgs e)
        {
            this.AddCls.IsEnabled = false;
            this.RemSem.IsEnabled = false;
            this.RemCls.IsEnabled = false;
            this.AddAss.IsEnabled = false;
            this.RemAss.IsEnabled = false;
            this.BegNts.IsEnabled = false;
            TreeViewItem cur = (TreeViewItem)Tree.SelectedItem;
            if (cur != null)
            {
                if (cur.Name.StartsWith("sem_"))
                {
                    this.AddCls.IsEnabled = true;
                }
                if (cur.Name.StartsWith("cls_"))
                {
                    this.RemCls.IsEnabled = true;
                }
                if (cur.Name.StartsWith("day_"))
                {
                    this.BegNts.IsEnabled = true;
                    this.AddAss.IsEnabled = true;
                }
            }
        }
    }
}
