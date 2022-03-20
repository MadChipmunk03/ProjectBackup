using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace fadsfas
{
   public class DiffCheck
    {
        public bool isDifferent(List<string> DestPath, string FilePath)
        {
            bool isDiff;
          
         
            if(DestPath.Contains(FilePath) == true)
            {
                isDiff = false;
                
            }
            else
            {
                isDiff = true;
                
            }
            return isDiff;
        }
    }
}
