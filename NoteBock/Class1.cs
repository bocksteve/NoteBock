using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBock
{
    //Class for actual school class, not to get the two mixed up
    class Class1
    {
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

        public ArrayList days = new ArrayList();

        public string Name;

        public void AddDay(Day day)
        {
            days.Add(day);
        }

        public Class1(string Namec)
        {
            Name = Removefromdatestring(Namec);
        }
    }
}
