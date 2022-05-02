using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Projekt_Daemon.Functions
{
    public class Backup_Versions
    {
        private string DirPath = @"C:\Daemon\BackupLogs\";



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
                
            }

            OldVersions.Add(0);
            if (BackupType > 1)
            {
                Models.Event ChangeOfType = new Models.Event() { };  //DODĚLAT, CHCI SPÁT JSOU 2 RÁNO A TOHLE JE FAKT ZBYTEČNOST
                                                                                                 // api.NewEvent();
            }
           
            result.Add($"{DirPath}{OldVersions.Max()}_BACKUP_LOG.txt");          //OLD
            result.Add($"{DirPath}{OldVersions.Max() + 1}_BACKUP_LOG.txt");      //NEXT

          

            return result;
        }




        public string BackupFileName(string Path, int BackupType)
        //Podobné, ale mohou být případy kdy budeme mít víc destinací a různy backupy,
        //např DIR1 s 6 backupy a DIR2 se 16 backupy -->
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
                Type = "backup";
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
            } catch


            {
                 file = Path + $@"1_{Type}_{DateTime.Now.ToString("ddMMyyyy")}\";
            }

            
            Directory.CreateDirectory(Path);
            return file;

        }

      
    }
}
