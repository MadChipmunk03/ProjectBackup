using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Daemon.Functions
{
    public class TimeSpecifier
    {
        public bool Timer(string Date,List<int> Da)
        {
          
            if (Date == DateTime.Now.ToString("HH:mm")
                //  &&Da.Contains((int)DateTime.Now.DayOfWeek)
                )

            {
             
                return true;
            }
            else
            {
              
                return false;
            }
        }
    }
}
