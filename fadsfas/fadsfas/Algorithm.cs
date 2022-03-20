using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace fadsfas
{
    public class Algorithm
    {

        private Logs Log = new Logs();
        private int NextVersion;
        private string Type;
        private DiffCheck Check = new DiffCheck();
        private List<string> AddToLog = new List<string>();
        private List<string> OldBU = new List<string>();
        private TimeCheck TimeCheck = new TimeCheck();
        // Jde o to že kazdy backup vytvoří složku s listem zalohovanych složek, ty pouzijeme do ostatnich typů záloh jako base, 
        //budeme vědět co máme zálohovat a co ne

        public void Start(Config Config)
        {
            string time = DateTime.Now.ToString("hh:mm");
            Console.WriteLine(time);
            this.Type = Config.BackUpType;
            Console.WriteLine(this.Type);
            Thread.Sleep(1000);
            string SourcePath = @"C:\Users\adasu\Desktop\Source\";  //Z Configu, nevím jak? Chce to @ ale ve stringu to nejde
            string DestPath = @"C:\Users\adasu\Desktop\Destination\";
            var Src = new DirectoryInfo(SourcePath);
            var BackupFolder = new DirectoryInfo(DestPath);
            //NextBackupVersion(BackupFolder, DestPath); 
            try
            {
                OldBU = System.IO.File.ReadAllLines(@"C:\Users\adasu\Desktop\Destination\filelog.txt").ToList();
            }
            catch
            {
                Log.Error("DIFFERENT BACKUP method UNAVIABLE --> Changing to full");
                this.Type = "FULL";
            }
            
            //Pro testování, ve finále tu bude error(?)
            Log.Success("Config Lodead");
            Copier(Src, BackupFolder);
            using (StreamWriter writer = new StreamWriter(@"C:\Users\adasu\Desktop\Destination\filelog.txt"))//Finalové zapisování do FileLogu
            {
                writer.WriteLine(DateTime.Now.ToString("h:mm"));
                foreach (string item in this.AddToLog)
                {
                    writer.WriteLine(item);
                }
            }


        }
        private void Copier(DirectoryInfo Src, DirectoryInfo Dest) //Zaloha všech dat
        {
            Console.WriteLine(this.OldBU[0]);
            foreach (FileInfo file in Src.GetFiles())
            {
                if (this.Type == "INC") //Base na differencial
                {
                    if (Check.isDifferent(OldBU, file.FullName) == true)
                    {
                        CopyFunc(file, Dest);
                    }
                    else
                    {
                        Console.WriteLine(file.Name + "was backed up in last version!");
                    }
                }
                else if (this.Type == "DIFF")
                {
                    if(TimeCheck.IsNew(this.OldBU[0], file) == true)
                    {
                        CopyFunc(file, Dest);
                    }            
                }
                else if (this.Type == "FULL")
                {
                    CopyFunc(file, Dest);
                }
            }
            foreach (DirectoryInfo dir in Src.GetDirectories())
            {
                this.AddToLog.Add(dir.FullName);
                DirectoryInfo next = Dest.CreateSubdirectory(dir.Name);
                Log.Copy(dir.Name);
                Copier(dir, next);
            }
        }

        private void CopyFunc(FileInfo file, DirectoryInfo Dest)
        {
            this.AddToLog.Add(file.FullName);
            string Path = Dest.FullName + "/" + file.Name;
            Log.Copy(file.Name);
            file.CopyTo(Path, true);
        }
    }
}
