using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;
namespace Projekt_Daemon.Functions
{
    public class Backup
    {

        private Model config = new Model();
        private Backup_Versions BUV = new Backup_Versions();
        private Writer Writter = new Writer();
        private CanBeCopied Cpy = new CanBeCopied();
        private FTP FTPConnection = new FTP();

        public void StartBackup(Model SourceModel, apiService Api)
        {
            Console.WriteLine(SourceModel.BackupType);

            if (Directory.Exists(@"C:\Daemon\BackupLogs\") == false)
            {
                Console.WriteLine($"[{DateTime.Now}] BackupLog directory created.");
                Directory.CreateDirectory(@"C:\Daemon\BackupLogs\");
            }
            this.config = SourceModel;
            Cpy.PutData(this.config.BackupType, BUV.FileLogPath(config.BackupType, Api)[0]);
            string Dc;
            foreach (var Dest in this.config.Destinations)
            {
                foreach (var Source in this.config.Sources)
                {
                    Dc = BUV.BackupFileName(Dest.Path + "/", this.config.BackupType, Dest.SaveType);
                    if (Dest.SaveType == 2)
                    {
                        NetworkCredential Credent = new NetworkCredential() { Password = Dest.Password, UserName = Dest.Username };
                        Console.WriteLine($"[{DateTime.Now}] Running [{Source}]!");
                        this.FTPConnection.RunFTP(Dest.Path , Source.SourcePath, Credent, Api);

                    }
                    else
                    {
                        this.RunBackup(Source.SourcePath, Dc);
                        this.ZipFileee(Dc, Dest.Path + "/");
                    }
                }
            }
            Writter.PUSH(BUV.FileLogPath(config.BackupType, Api)[1], config.BackupType);
        }
        private void ZipFileee(string Path, string Zip)
        {

            string zippath = Zip + @"\BackupZips\" + BUV.Name + "_ZIP.zip";
            Directory.CreateDirectory(Zip + @"\BackupZips\");
            ZipFile.CreateFromDirectory(Path, zippath); //For some reason this works ? 

            Console.WriteLine($"[{DateTime.Now}] ZIPPED {BUV.Name} to {zippath}!");
            Console.WriteLine($"[{DateTime.Now}] Deleted broken relationships :(");

        }


        public void RunBackup(string Source, String Dest)
        {
            Directory.CreateDirectory(Dest);
            List<string> Directories = this.SourceDirs(Source);
            List<string> Content = this.SourceContet(Source);
            string[] directories = Dest.Split(Path.DirectorySeparatorChar);
            Console.WriteLine($"[{DateTime.Now}] Copying to {directories[directories.Length - 2]}:");
            Thread.Sleep(50);
            foreach (string Dir in Directories)
            {
                string path = Dir;
                path = path.Replace(Source, Dest);
                Console.WriteLine("");
                if (Cpy.AllowCpyFile(Dir) == true)
                {
                    Directory.CreateDirectory(path);
                    Writter.Add(Dir);
                    Console.WriteLine($"[{DateTime.Now}] Copied {Dir}");
                }
                else
                {
                    Console.WriteLine("X");
                }
            }
            foreach (string Contents in Content)
            {
                string path = Contents;
                path = path.Replace(Source, Dest);


                if (Cpy.AllowCpyFile(Contents) == true)
                {
                    File.Copy(Contents, path);
                    Writter.Add(Contents);
                    Console.WriteLine($"[{DateTime.Now}] Copied {Contents};");
                }
                else
                {
                    Console.WriteLine("X");
                }
            }
        }


        public List<string> SourceContet(string SourcePath)
        {
            List<string> result = new List<string>();

            foreach (string item in Directory.GetFiles(SourcePath, "*", SearchOption.AllDirectories))
            {
                result.Add(item);
            }
            return result;
        }
        public List<string> SourceDirs(string SourcePath)
        {
            List<string> result = new List<string>();
            try
            {
                string[] folders = System.IO.Directory.GetDirectories(SourcePath, "*", System.IO.SearchOption.AllDirectories);
                foreach (var item in folders)
                {
                    result.Add(item);
                }
            }
            catch
            {
                Console.WriteLine($"There were one or more errors while accessing to files at {SourcePath}!");
            }


            return result;
        }





    }
}
