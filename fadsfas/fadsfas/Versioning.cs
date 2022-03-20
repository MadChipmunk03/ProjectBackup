using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fadsfas
{
    class Versioning
    {
        /* private void NextBackupVersion(DirectoryInfo BackupFolder, string DestPath) 
         * 
         * 
         * 
         * //Vytvoření a Zjisteni nove backup slozky(pokud budeme chtit pouzit verzovani)
        {
            List<int> VersionList = new List<int>();
            foreach (DirectoryInfo dir in BackupFolder.GetDirectories())
            {
                string[] Version = dir.Name.Split('_');
                VersionList.Add(Convert.ToInt32(Version[1]));

            }
            int NextVersion = VersionList.Max() + 1;
            this.NextVersion = NextVersion;
            Directory.CreateDirectory(DestPath + $"Backup_{NextVersion}");
        }*/
    }
}
