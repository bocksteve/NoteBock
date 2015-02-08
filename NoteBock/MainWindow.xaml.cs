using System;
using System.IO;
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

        public string Removefromdatestring(string temp)
        {
            //used to remove "/" from date strings so we can use them for treeviewitem names
            for (int i = 0; i < temp.Length; i++)
            {
                if ((int)temp[i] < 48 && 31 < (int)temp[i])
                {
                    temp = temp.Remove(i, 1);
                }
            }
            return temp;
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
                TreeViewItem newS = new TreeViewItem() { Header = newWindow.SemName, Name = "sem_" + Removefromdatestring(newWindow.SemName) };
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
            //Have to have selected a semester to click this menu option
            TreeViewItem cur = (TreeViewItem)Tree.SelectedItem;

            //Find the semester in the ArrayList Sems, then delete it
            for (int i = 0; i < Sems.Count; i++)
            {
                Semester sem = (Semester)Sems[i];
                if (cur.Name.Contains(sem.Name))
                {
                    Sems.Remove(Sems[i]);
                    sem = null;
                }
            }

            //Remove the semester (and all its children) from the tree
            Tree.Items.Remove(cur);

        }

        private void AddClsClick(object sender, RoutedEventArgs e)
        {
            //New window for class info input
            var newWindow = new AddCls();
            newWindow.ShowDialog();

            //If it wasn't closed with ok, just return and do nothing
            if (newWindow.closedwithok == false)
            {
                return;
            }
            //Must have selected a semester to even click this button, record which semester and create new class
            TreeViewItem cur = (TreeViewItem)Tree.SelectedItem;

            //Look for Semester
            for (int i = 0; i < Sems.Count; i++)
            {
                Semester sem = (Semester)Sems[i];
                if (cur.Name.Contains(sem.Name))
                {
                    //Found semester, add the new class
                    TreeViewItem newCls = new TreeViewItem() { Header = newWindow.ClsName, Name = "cls_" + Removefromdatestring(newWindow.ClsName) + "_" + sem.Name };
                    cur.Items.Add(newCls);
                    Class1 newcls = new Class1(newWindow.ClsName);
                    sem.AddClass(newcls);
                    for (int j = 0; j < sem.weeks; j++)
                    {
                        //Add Week i for every week of classes for semester
                        //Each item is named "week_x"
                        TreeViewItem week = new TreeViewItem() { Header = "Week " + (j + 1), Name = "week_" + (j + 1) };
                        newCls.Items.Add(week);
                        for (int h = 0; h < newWindow.Days.Count; h++)
                        {
                            //Add each day for every week
                            if (newWindow.Days[h] == "Monday")
                            {
                                DateTime dt = sem.Start.AddDays(1+(j*7));
                                week.Items.Add(new TreeViewItem() { Header = (dt.ToShortDateString()), Name = "day_" + sem.Name + "_" + newcls.Name });
                                newcls.AddDay(new Day(dt));
                            }
                            else if (newWindow.Days[h] == "Tuesday")
                            {
                                DateTime dt = sem.Start.AddDays(2 + (j * 7));
                                week.Items.Add(new TreeViewItem() { Header = (dt.ToShortDateString()), Name = "day_" + sem.Name + "_" + newcls.Name });
                                newcls.AddDay(new Day(dt));
                            }
                            else if (newWindow.Days[h] == "Wednesday")
                            {
                                DateTime dt = sem.Start.AddDays(3 + (j * 7));
                                week.Items.Add(new TreeViewItem() { Header = (dt.ToShortDateString()), Name = "day_" + sem.Name + "_" + newcls.Name });
                                newcls.AddDay(new Day(dt));
                            }
                            else if (newWindow.Days[h] == "Thursday")
                            {
                                DateTime dt = sem.Start.AddDays(4 + (j * 7));
                                week.Items.Add(new TreeViewItem() { Header = (dt.ToShortDateString()), Name = "day_" + sem.Name + "_" + newcls.Name});
                                newcls.AddDay(new Day(dt));
                            }
                            else if (newWindow.Days[h] == "Friday")
                            {
                                DateTime dt = sem.Start.AddDays(5 + (j * 7));
                                week.Items.Add(new TreeViewItem() { Header = (dt.ToShortDateString()), Name = "day_" + sem.Name + "_" + newcls.Name });
                                newcls.AddDay(new Day(dt));
                            }
                            
                        }
                    }
                }
            }
        }

        private void RemClsClick(object sender, RoutedEventArgs e)
        {
            //Have to have class selected, remove class
            TreeViewItem cur = (TreeViewItem)Tree.SelectedItem;
            TreeViewItem semester = new TreeViewItem();
            //Find which semester this class belongs too
            for (int i = 0; i < Sems.Count; i++)
            {
                Semester sem = (Semester)Sems[i];
                if (cur.Name.Contains(sem.Name))
                {
                    foreach(TreeViewItem tvi in Tree.Items)
                    {
                        if (tvi.Header.ToString() == sem.Name)
                        {
                            semester = tvi;
                            break;
                        }
                    }
                    //Found correct semester, now find correct class
                    for (int j = 0; j < sem.classes.Count; j++)
                    {
                        Class1 cls = (Class1)sem.classes[i];
                        if (cur.Name.Contains(cls.Name))
                        {
                            sem.classes.Remove(cls);
                            cls = null;
                        }
                    }
                }
            }
            semester.Items.Remove(cur);

 


        }

        private void AddAssClick(object sender, RoutedEventArgs e)
        {
            var newWindow = new AddAss();
            newWindow.ShowDialog();

            //If it wasn't closed with ok, just return and do nothing
            if (newWindow.closedwithok == false)
            {
                return;
            }

            TreeViewItem cur = (TreeViewItem)Tree.SelectedItem;

            for (int i = 0; i < Sems.Count; i++)
            {
                //Find semester of cur
                Semester sem = (Semester)Sems[i];
                if (cur.Name.Contains(sem.Name))
                {
                    //Find class of cur
                    for (int j = 0; j < sem.classes.Count; j++)
                    {
                        Class1 cls = (Class1)sem.classes[j];
                        if (cur.Name.Contains(cls.Name))
                        {
                            //cur is a day, find it
                            for (int h = 0; h < cls.days.Count; h++)
                            {
                                Day day = (Day)cls.days[h];
                                if (Removefromdatestring(cur.Header.ToString()) == day.date)
                                {
                                    cur.Items.Add(new TreeViewItem() { Header = newWindow.AssName, Name = "ass_" + sem.Name + 
                                        "_" + cls.Name + "_" + Removefromdatestring(day.date)});
                                    day.AddAss(newWindow.AssName + ": " + newWindow.AssDesc);
                                    sem.DueDates.Add(day.dt);
                                    sem.DueDates.Add(newWindow.AssName);
                                    return;
                                    //stop iterating after adding the assignment
                                }
                            }
                        }
                    }
                }
            }
        }

        private void RemAssClick(object sender, RoutedEventArgs e)
        {

        }

        private void BegNtsClick(object sender, RoutedEventArgs e)
        {
            TreeViewItem cur = (TreeViewItem)Tree.SelectedItem;

            for (int i = 0; i < Sems.Count; i++)
            {
                //Find semester of cur
                Semester sem = (Semester)Sems[i];
                if (cur.Name.Contains(sem.Name))
                {
                    //Find class of cur
                    for (int j = 0; j < sem.classes.Count; j++)
                    {
                        Class1 cls = (Class1)sem.classes[j];
                        if (cur.Name.Contains(cls.Name))
                        {
                            //cur is a day, find it
                            for (int h = 0; h < cls.days.Count; h++)
                            {
                                Day day = (Day)cls.days[h];
                                if (Removefromdatestring(cur.Header.ToString()) == day.date)
                                {
                                    TreeViewItem newtvi = new TreeViewItem()
                                    {
                                        Header = "Notes",
                                        Name = "Notes_" + sem.Name +
                                            "_" + cls.Name + "_" + Removefromdatestring(day.date)
                                    };
                                    cur.Items.Add(newtvi);
                                    cur.IsExpanded = true;
                                    newtvi.IsSelected = true;
                                    LargeTxt.SelectAll();
                                    LargeTxt.Selection.Text = "";
                                    TextRange t = new TextRange(LargeTxt.Document.ContentStart,
                                        LargeTxt.Document.ContentEnd);
                                    FileStream file = new FileStream("Notes_" + sem.Name + "_" + cls.Name + "_" 
                                        + Removefromdatestring(day.date) + ".rtf", FileMode.Create);
                                    t.Save(file, System.Windows.DataFormats.Rtf);
                                    file.Close();

                                    return;
                                    //stop iterating after adding the assignment
                                }
                            }
                        }
                    }
                }
            }
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

        private void TreeClicked(object sender, RoutedEventArgs e)
        {
            TreeViewItem cur = (TreeViewItem)Tree.SelectedItem;
            if (cur == null)
            {
                return;
            }
            if (cur.Name.Contains("ass_"))
            {
                //Clicked on an assignment, display the description!
                for (int i = 0; i < Sems.Count; i++)
                {
                    //Find semester of cur
                    Semester sem = (Semester)Sems[i];
                    if (cur.Name.Contains(sem.Name))
                    {
                        //Find class of cur
                        for (int j = 0; j < sem.classes.Count; j++)
                        {
                            Class1 cls = (Class1)sem.classes[j];
                            if (cur.Name.Contains(cls.Name))
                            {
                                //cur is a day, find it
                                for (int h = 0; h < cls.days.Count; h++)
                                {
                                    Day day = (Day)cls.days[h];
                                    if (cur.Name.Contains(day.date))
                                    {
                                        for (int z = 0; z < day.Assigns.Count; z++)
                                        {
                                            string assign = (string)day.Assigns[z];
                                            if (assign.Contains(cur.Header.ToString()))
                                            {
                                                LargeTxt.SelectAll();
                                                LargeTxt.Selection.Text = "";

                                                LargeTxt.AppendText(assign);
                                                return;
                                            }
                                        }
 
                                        //stop iterating after displaying
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (cur.Name.Contains("Notes_"))
            {
                LargeTxt.SelectAll();
                LargeTxt.Selection.Text = "";
                TextRange t = new TextRange(LargeTxt.Document.ContentStart,
                                LargeTxt.Document.ContentEnd);
                FileStream file = new FileStream(cur.Name + ".rtf", FileMode.Open);
                t.Load(file, System.Windows.DataFormats.Rtf);
                file.Close();
            }
            else if (cur.Name.Contains("sem_"))
            {
                for (int i = 0; i < Sems.Count; i++)
                {
                    Semester sem = (Semester)Sems[i];
                    if (cur.Name.Contains(sem.Name))
                    {
                        if (sem.DueDates.Count > 0)
                        {
                            int closedues = 0;
                            StringBuilder sb = new StringBuilder();
                            for (int j = 0; j < sem.DueDates.Count; j=j+2 )
                            {
                                DateTime dt = (DateTime)sem.DueDates[j];
                                DateTime current = DateTime.Today;
                                for (int h = 0; h < 8; h++)
                                {
                                    DateTime tempcur = current.AddDays(h);
                                    if (tempcur == dt)
                                    {
                                        string temp = (string)sem.DueDates[j+1];
                                        sb.Append(dt.ToShortDateString() + ": " + temp);
                                        sb.Append(Environment.NewLine);
                                        closedues++;
                                    }
                                }
                            }
                            if (closedues > 0)
                            {
                                SmallTxt.Text = "";
                                SmallTxt.Text = sb.ToString();
                            }

                        }
                    }
                }
            }

        }

        private void SaveNtsClick(object sender, RoutedEventArgs e)
        {
            TreeViewItem cur = (TreeViewItem)Tree.SelectedItem;

            for (int i = 0; i < Sems.Count; i++)
            {
                //Find semester of cur
                Semester sem = (Semester)Sems[i];
                if (cur.Name.Contains(sem.Name))
                {
                    //Find class of cur
                    for (int j = 0; j < sem.classes.Count; j++)
                    {
                        Class1 cls = (Class1)sem.classes[j];
                        if (cur.Name.Contains(cls.Name))
                        {
                            //cur is a day, find it
                            for (int h = 0; h < cls.days.Count; h++)
                            {
                                Day day = (Day)cls.days[h];
                                if (cur.Name.Contains(day.date))
                                {
                                    TextRange t = new TextRange(LargeTxt.Document.ContentStart,
                                        LargeTxt.Document.ContentEnd);
                                    FileStream file = new FileStream("Notes_" + sem.Name + "_" + cls.Name + "_" 
                                        + Removefromdatestring(day.date) + ".rtf", FileMode.Create);
                                    t.Save(file, System.Windows.DataFormats.Rtf);
                                    file.Close();
                                    return;
                                    //stop iterating after adding the assignment
                                }
                            }
                        }
                    }
                }
            }
        }

        private void OnSubmenuOpened(object sender, RoutedEventArgs e)
        {
            this.AddCls.IsEnabled = false;
            this.RemSem.IsEnabled = false;
            this.RemCls.IsEnabled = false;
            this.AddAss.IsEnabled = false;
            this.RemAss.IsEnabled = false;
            this.BegNts.IsEnabled = false;
            this.SaveNts.IsEnabled = false;
            TreeViewItem cur = (TreeViewItem)Tree.SelectedItem;
            if (cur != null)
            {
                if (cur.Name.StartsWith("sem_"))
                {
                    this.AddCls.IsEnabled = true;
                    this.RemSem.IsEnabled = true;
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
                if (cur.Name.StartsWith("Notes"))
                {
                    this.SaveNts.IsEnabled = true;
                }
            }
        }
    }
}
