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
    


        public void PUSH(string PushLocation, string Type)
        {
            using (StreamWriter Writer = new StreamWriter(PushLocation))
            {
                Writer.WriteLine(DateTime.Now.ToString());
                Writer.WriteLine($"{Type} backup");
                foreach (string item in this.ListOfPaths)
                {
                    Writer.WriteLine(item);
                }
            }
        }
    }
}
