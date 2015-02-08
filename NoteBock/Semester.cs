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
        public DateTime Start;
        public DateTime End;
        public string Name;
        public int weeks;

        public ArrayList classes;
        public ArrayList days;
        public ArrayList DueDates;


        public Semester(DateTime start, DateTime end, string name)
        {
            Start = start;
            End = end;
            Name = name;

            DateTime cur = Start;
            int numdays = 0;
            while (cur != End)
            {
                numdays++;
                cur = cur.AddDays(1);
            }
            weeks = (numdays + 1) / 7;
        }

        public void AddClass(string Class)
        {
            
        }
    }
}
