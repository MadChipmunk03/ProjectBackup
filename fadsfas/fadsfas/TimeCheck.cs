using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace fadsfas
{
   public class TimeCheck
    {
      

        public bool IsNew(string BackUpTime, FileInfo Target)
        {
            bool result;
            DateTime LastBackUp = DateTime.ParseExact(BackUpTime, "h:mm", CultureInfo.CurrentUICulture);
            DateTime TargetTime = File.GetLastWriteTime(Target.FullName);
            if (DateTime.Compare(LastBackUp, TargetTime) > 0)
            {
                result = false;
            }
            else if (DateTime.Compare(LastBackUp, TargetTime) < 0)
            {
                Console.WriteLine($"Last backup was {LastBackUp}, so file with time {TargetTime} is going to be backed up");             
                result = true;
              
            }
            else result = false;
            return result;
        }
    }
}
