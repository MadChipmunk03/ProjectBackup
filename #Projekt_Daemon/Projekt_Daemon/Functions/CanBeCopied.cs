using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Daemon.Functions
{
    public class CanBeCopied
    { //1 - full, 2 - differential, 3 - incremental
        private int BackupType;
        private string LastLog;
        private GetLastFull DIFF = new GetLastFull();
       // private LastFileChange LFG = new LastFileChange();
        public void PutData(int backupID, string LastLog)
        {
            this.BackupType = backupID;
            this.LastLog = LastLog;
        }
        public bool AllowCpyFile(string Check)
        {
            bool res = false;
            if (this.BackupType == 1)
            {
                res = true;
            }
            else if (BackupType == 3)
            {
                 if (this.ContainsDIFF(Check) == false) //Incremental
                {
                    res = true;                  
                }
                else
                {
                    res = false;
                }       
            }
            else if (BackupType == 2) //DIFF OPROTI FULL
            {
                if(this.DIFF.GetLastFullBackup().Contains(Check))
                {
                    res = true;
                }          }    
            return res;
        }
        


        public bool ContainsDIFF(string Check)
        {
            bool res;
            var logFile = File.ReadAllLines(LastLog);
            var logList = new List<string>(logFile);
            if (logList.Contains(Check) == true)
            {
                res = true;                
            }
            else
            {
                res = false;
            }       
            return res;   
        }
    }
}
