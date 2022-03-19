using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace fadsfas
{
    public class Algorithm
    {
        
        private Logs Log = new Logs();
        public int NextVersion;
        private string Type;
        private DiffCheck Check = new DiffCheck();
        private List<string> ForTest = new List<string>();
        // Jde o to že kazdy backup vytvoří složku s listem zalohovanych složek, ty pouzijeme do ostatnich typů záloh jako base, 
        //budeme vědět co máme zálohovat a co ne
        public void Start(Config Config)
        {       
            string SourcePath = @"C:\Users\adasu\Desktop\Source\";  //Z Configu, nevím jak? Chce to @ ale ve stringu to nejde
            string DestPath = @"C:\Users\adasu\Desktop\Destination\";      
            
            var Src = new DirectoryInfo(SourcePath);
            var BackupFolder = new DirectoryInfo(DestPath);
            //NextBackupVersion(BackupFolder, DestPath); 
            Log.Success("Config Lodead");
            Copier(Src, BackupFolder);
                   
        }
       private void Copier(DirectoryInfo Src, DirectoryInfo Dest) //Zaloha všech dat
        {
            foreach (FileInfo file in Src.GetFiles())
            { 
                if(this.Type == "DIFF" && Check.isDifferent(ForTest,file.FullName)) //Base na differencial
                {
                    string Path = Dest.FullName + "/" + file.Name;
                    Log.Copy(file.Name);
                    file.CopyTo(Path, true);
                } else
                {
                    string Path = Dest.FullName + "/" + file.Name;
                    Log.Copy(file.Name);
                    file.CopyTo(Path, true);
                }

            }
            foreach (DirectoryInfo dir in Src.GetDirectories())
            {               
                DirectoryInfo next = Dest.CreateSubdirectory(dir.Name);
                Log.Copy(dir.Name);
                Copier(dir, next);
            }
        }
        private void NextBackupVersion(DirectoryInfo BackupFolder, string DestPath) //Vytvoření a Zjisteni nove backup slozky(pokud budeme chtit pouzit verzovani)
        {
            List<int> VersionList = new List<int>();
            foreach (DirectoryInfo dir in BackupFolder.GetDirectories())
            {
                string[] Version = dir.Name.Split('_');
                VersionList.Add(Convert.ToInt32(Version[1]));
                Log.CreateLogFolder(dir.Name);
            }
            int NextVersion = VersionList.Max() + 1;
            this.NextVersion = NextVersion;
            Directory.CreateDirectory(DestPath + $"Backup_{NextVersion}");
        } 
    }
}
