﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBock
{
    class Day
    {
        public ArrayList Assigns = new ArrayList();
        public DateTime dt;
        public string date = "";
        public string Notes;

        public Day(DateTime DT)
        {
            dt = DT;
            string temp = dt.ToShortDateString();
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] == '/')
                {
                    temp = temp.Remove(i, 1);
                }
            }
            date = temp;
        }

        public void AddAss(string AssDesc)
        {
            Assigns.Add(AssDesc);
        }

        public void RemAss(string AssDesc)
        {
            for (int i = 0; i < Assigns.Count; i++)
                if (AssDesc == (string)Assigns[i])
                {
                    Assigns.Remove(Assigns[i]);
                }
        }

        public void SetNotes(string filename)
        {
            Notes = filename;
        }

    }
}
