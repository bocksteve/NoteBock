using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBock
{
    class Semester
    {
        public string Removefromdatestring(string temp)
        {
            //used to remove "/" from date strings so we can use them for treeviewitem names
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] == '/')
                {
                    temp = temp.Remove(i, 1);
                }
                else if (temp[i] == ' ')
                {
                    temp = temp.Remove(i, 1);
                }
            }
            return temp;
        }

        public DateTime Start;
        public DateTime End;
        public string Name;
        public int weeks;

        public ArrayList classes = new ArrayList();
        public ArrayList days;
        public ArrayList DueDates = new ArrayList();


        public Semester(DateTime start, DateTime end, string name)
        {
            Start = start;
            End = end;
            Name = Removefromdatestring(name);

            DateTime cur = Start;
            int numdays = 0;
            while (cur != End)
            {
                numdays++;
                cur = cur.AddDays(1);
            }
            weeks = (numdays + 1) / 7;
        }

        public void AddClass(Class1 Class)
        {
            classes.Add(Class);
        }

        public void RemClass(string Name)
        {
            for (int i = 0; i < classes.Count; i++)
            {
                Class1 cls = (Class1)classes[i];
                if (cls.Name == Name)
                {
                    classes.Remove(cls);
                }
            }
        }
    }
}
