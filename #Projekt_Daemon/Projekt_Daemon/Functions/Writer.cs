using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Projekt_Daemon.Functions
{
    public class Writer //Zapisovatel si uloží všechny záznamy které pak pushne do logu
    {
        private List<string> ListOfPaths = new List<string>();
        public void Add(string Path)
        {
            this.ListOfPaths.Add(Path);
        }
    


        public void PUSH(string PushLocation, int Type)
        {
         
            string Translated = "FULL";
            if(Type == 2)
            {
                Translated = "DIFF";
            } else if (Type== 3)
            {
                Translated = "INC";
            }
            using (StreamWriter Writer = new StreamWriter(PushLocation))
            {
                Writer.WriteLine(DateTime.Now.ToString());
                Writer.WriteLine($"{Translated} backup");
                foreach (string item in this.ListOfPaths)
                {
                    Writer.WriteLine(item);
                }
                Console.WriteLine($"[{DateTime.Now}] Pushed logfile");
            }
        }
    }
}
