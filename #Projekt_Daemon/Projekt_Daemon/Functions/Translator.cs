using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Daemon.Functions
{
   public class Translator
    {
        //FORMAT: Mo.Fr.Su;12:00
        public string GetTime(string Input)
        {
            string[] timer = Input.Split(';');
          
            return timer[1];
        }
        public List<int> GetWeekDay(string Input)
        {
            List<int> result = new List<int>();
            string[] Baser = Input.Split(':');
            string[] Days = Baser[0].Split('.');
            foreach (string item in Days)
            {
                result.Add(this.StringToInt(item));

            }
            return result;
        }
        public int StringToInt(string Input)
        {
            int result;
            if (Input== "Mo")
            {
                result = 1;
            }
            else if (Input== "Tu")
            {
                result = 2;
            }
            else if (Input== "We")
            {
                result = 3;
            }
            else if (Input== "Th")
            {
                result = 4;
            }
            else if (Input== "Fr")
            {
                result = 5;
            }
            else if (Input== "Sa")
            {
                result = 6;
            }
            else
            {
                result = 0;
            }
            return result;
        }

    }
}
