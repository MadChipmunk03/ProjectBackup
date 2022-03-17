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
        public void Start(Config Config)
        {
            string test = Config.sourcefolder;
            string test2 = $"\"{test}\"";
            Console.WriteLine(test2);
            string test3 = "@" + test2;
            Console.WriteLine(test3);

            string SourcePath = @"C:\Users\adasu\Desktop\Source\";  //Z Configu, nevím jak? Chce to @ ale ve stringu to nejde
            string DestPath = @"C:\Users\adasu\Desktop\Destination\";         
            var Src = new DirectoryInfo(SourcePath);
            var BackupFolder = new DirectoryInfo(DestPath);
            //NextBackupVersion(BackupFolder, DestPath); 
            Log.Success("Config Lodead");
            Copier(Src, BackupFolder);
                   
        }
       private void Copier(DirectoryInfo Src, DirectoryInfo Dest)
        {
            foreach (FileInfo file in Src.GetFiles())
            {
                string Path = Dest.FullName + "/" + file.Name;
                Log.Copy(file.Name);
                file.CopyTo(Path, true);
            }
            foreach (DirectoryInfo dir in Src.GetDirectories())
            {
                DirectoryInfo next = Dest.CreateSubdirectory(dir.Name);
                Log.Copy(dir.Name);
                Copier(dir, next);
            }
        }
        private void NextBackupVersion(DirectoryInfo BackupFolder, string DestPath)
        {
            List<int> VersionList = new List<int>();
            foreach (DirectoryInfo dir in BackupFolder.GetDirectories())
            {
                string[] Version = dir.Name.Split('_');
                VersionList.Add(Convert.ToInt32(Version[1]));
            }
            int NextVersion = VersionList.Max() + 1;
            Directory.CreateDirectory(DestPath + $"Backup_{NextVersion}");
        } 
    }
}
