using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Daemon.Functions
{
    public class GetLastFull
    {
        public List<string> GetLastFullBackup()
        {

            //Na tohle nekoukej, to jsem psal na poslední chvíli, hlavně jestli to funguje z tvoji strany projektu


            List<string> vysledek = new List<string>();
            DirectoryInfo Info = new DirectoryInfo(@"C:\Daemon\BackupLogs\");
            List<Models.Files> FileModels = new List<Models.Files>();
            List<int> Ints = new List<int>();

            foreach (var item in Info.GetFiles())
            {
                var logFile = File.ReadAllLines(item.FullName);
                var logList = new List<string>(logFile);
                string[] split = item.ToString().Split('_');

                Models.Files AddModel = new Models.Files { id = Convert.ToInt32(split[0]), Type = logList[1] };
                AddModel.Rest = logList;

                FileModels.Add(AddModel);
            }
        
            foreach (Models.Files item in FileModels)
            {
                if(item.Type == "FULL backup")
                {
                    Ints.Add(Convert.ToInt32(item.id));
                }
            }


            Models.Files Final = Get(Ints.Max(), FileModels);
            vysledek = Final.Rest;
            return vysledek;

        }


        Models.Files Get(int max,List<Models.Files> source)
        {
            Models.Files res = new Models.Files();
            foreach(Models.Files mod in source)
            {
                if(mod.id == max)
                {
                    return mod;
                }
                     
            }


            return res;
        }
    }
}
