using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Projekt_Daemon.Functions
{
    public class Backup_Versions
    {
        private string DirPath = @"C:\Daemon\BackupLogs\";
        public string Name;
        //                 Backup FORMAT:  ID_TYPEOFBACKUP_TIME
        //                 FileLog FORMAT: NUMBERTOSORT_ID_TYPEOFBACKUP_TIME 

        public List<string> FileLogPath(int BackupType, apiService api)
        {
            List<String> result = new List<string>();
            DirectoryInfo DirInfo = new DirectoryInfo(DirPath);
            List<int> OldVersions = new List<int>();
            try
            {
                foreach (var item in DirInfo.GetFiles())
                {
                    string[] Splitter = item.Name.Split('_');
                    OldVersions.Add(Convert.ToInt32(Splitter[0]));
                }
            }
            catch
            {
                //Pro test. účely  
            }
            OldVersions.Add(0);
            if (BackupType > 1)
            {
                Models.Event ChangeOfType = new Models.Event() { };
            }
            result.Add($"{DirPath}{OldVersions.Max()}_BACKUP_LOG.txt");          //OLD
            result.Add($"{DirPath}{OldVersions.Max() + 1}_BACKUP_LOG.txt");      //NEXT
            return result;
        }




        public string BackupFileName(string Path, int BackupType, int SaveType)
      
        {
            List<int> OldVersions = new List<int>();
            string Type = "UNKNOWN";
            if (BackupType == 1)
            {
                Type = "FULL_BACKUP";
            }
            else if (BackupType == 2)
            {
                Type = "DIFF_BACKUP";
            }
            else if (BackupType == 3)
            {
                Type = "INCR_BACKUP";
            }
            else
            {
                Type = "Unknown_backup";
            }
            try
            {
                DirectoryInfo Info = new DirectoryInfo(Path);
                foreach (var item in Info.GetDirectories())
                {
                    string[] Splitter = item.Name.Split('_');
                    OldVersions.Add(Convert.ToInt32(Splitter[0]));
                }
            }
            catch
            {
                OldVersions.Add(0);
            }
            string file;
            try
            {
                file = Path + $@"{OldVersions.Max() + 1}_{Type}_{DateTime.Now.ToString("ddMMyyyy")}\";
                this.Name = $"{OldVersions.Max() + 1}_{Type}_{DateTime.Now.ToString("ddMMyyyy")}";
            }
            catch


            {
                this.Name = $"1_{Type}_{DateTime.Now.ToString("ddMMyyyy")}";
                file = Path + $@"1_{Type}_{DateTime.Now.ToString("ddMMyyyy")}\";
            }

            if(SaveType != 2)
            {
                Directory.CreateDirectory(Path);
            }
           
            return file;

        }


    }
}
