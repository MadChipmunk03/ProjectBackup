using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;

namespace Projekt_Daemon.Functions
{
    public class FTP
    {
        private string BackupName;
        private string Dest;
        private NetworkCredential Login;
        



        public void RunFTP(string FTPDestination, string Source, NetworkCredential Login, apiService api)
        {
            
            this.Dest = FTPDestination;
            this.Login = Login;


            string tempfolder = $@"C:\Daemon\TempFolder\";

            if (Directory.Exists(tempfolder) != true)
            {
                Directory.CreateDirectory(tempfolder);
               
            }
            tempfolder = tempfolder + $"{this.BackupName}.zip";
            if (File.Exists(tempfolder))
            {
                File.Delete(tempfolder);
              
            }

            ZipFile.CreateFromDirectory(Source, tempfolder);

           
            this.BackupName = this.GetNextName();
            using (var client = new WebClient())
            {
                try
                {
                    client.Credentials = new NetworkCredential(Login.UserName, Login.Password);
                    client.UploadFile(FTPDestination + $"{this.BackupName}.zip", WebRequestMethods.Ftp.UploadFile, tempfolder);
                    Console.WriteLine($"[{DateTime.Now}] {this.BackupName}.zip was uploaded to {FTPDestination}    !");
                }
                catch (Exception ex)
                {

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[{DateTime.Now}] {ex}");
                    Console.ForegroundColor = ConsoleColor.White;
                    api.NewEvent(Convert.ToString(ex), 1);
                    

                } 
            }

            File.Delete(tempfolder);
        }
        public string GetNextName()
        {
            FtpWebRequest test = (FtpWebRequest)WebRequest.Create(this.Dest);
            test.Credentials = new NetworkCredential(Login.UserName, Login.Password);
            test.Method = WebRequestMethods.Ftp.ListDirectory;
            FtpWebResponse response = (FtpWebResponse)test.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string names = reader.ReadToEnd();
            reader.Close();
            response.Close();
            try
            {
                List<string> res = names.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                List<int> Versions = new List<int>();
                foreach (string item in res)
                {
                    string[] split = item.Split('_');
                    Versions.Add(Convert.ToInt32(split[0]));

                }
                return $"{Versions.Max() + 1}_BACKUP_{DateTime.Now.ToString("ddMMyyyy")}";
            }
           catch
            {
                return $"{1}_BACKUP_{DateTime.Now.ToString("ddMMyyyy")}";
            } 
        }
    }
}
