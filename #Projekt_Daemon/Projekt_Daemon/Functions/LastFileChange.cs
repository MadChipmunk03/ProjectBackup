using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Projekt_Daemon.Functions
{
   public class LastFileChange
    {
        public bool WasChanged(string FilePath, string LastChange)
        {
            bool result = true;
            FileInfo File = new FileInfo(FilePath);

            int Res = DateTime.Compare(File.LastWriteTime, Convert.ToDateTime(LastChange));

            DateTime date1 = Convert.ToDateTime(LastChange);
            DateTime date2 = File.LastWriteTime;
            Console.WriteLine(date1);
            Console.WriteLine(date2);
            int Compare = DateTime.Compare(date1, date2);
          

            if (Compare < 0)
                result = true;


            else if (Compare == 0)
                result = true;

            else
                result = false;

            Console.WriteLine(result);
            return result;
            
        }
    }
}
