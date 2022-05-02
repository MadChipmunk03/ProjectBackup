using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Projekt_Daemon.Functions
{
    public class Backup
    {
      
        private Model config = new Model();
        private Backup_Versions BUV = new Backup_Versions();
        private Writer Writter = new Writer();
        private CanBeCopied Cpy = new CanBeCopied();
        public void StartBackup(Model SourceModel, apiService Api)
        {
            
            if (Directory.Exists(@"C:\Daemon\BackupLogs\") == false)
            {
                Directory.CreateDirectory(@"C:\Daemon\BackupLogs\");
            }           
            this.config = SourceModel;
            
            Cpy.PutData(this.config.BackupType,BUV.FileLogPath(config.BackupType, Api)[0]);

           // DestinationModel DestMod = new DestinationModel { Id = 1, ConfigId = 900300, Path = @"C:\Destination2\" };
             //this.config.Destinations.Add(DestMod);
            //this.RunBackup(@"C:\Source\", BUV.BackupFileName(@"C:\Destination\", "FULL")); <>--TEST--<>
           
          
            
            foreach (var Dest in this.config.Destinations)
            {
                foreach(var Source in this.config.Sources)
                {
                    this.RunBackup(Source.SourcePath, BUV.BackupFileName(Dest.Path, this.config.BackupType));
                   
                   
                    
                }
            }
            Writter.PUSH(BUV.FileLogPath(config.BackupType, Api)[1], "FULL");
          //  Models.Event SuprCuprBackup = new Models.Event { Message = "A backup was successfull!"}
           // Api.NewEvent()
           
        }
       
        public void RunBackup(string Source, String Dest) //--------------------MAIN----------------------------------------------------------------------><
        {
            Directory.CreateDirectory(Dest);
            List<string> Directories = this.SourceDirs(Source);
            List<string> Content = this.SourceContet(Source);


            string[] directories = Dest.Split(Path.DirectorySeparatorChar);
            Console.WriteLine($"Copying to {directories[directories.Length - 2]}:");

            foreach (string Dir in Directories) 
            {
                string path = Dir;
                path = path.Replace(Source, Dest);
               
                Console.WriteLine("");


                if (Cpy.AllowCpyFile(Dir) == true)
                {
                    Directory.CreateDirectory(path);
                    Writter.Add(Dir);
                    Console.WriteLine($"{Dir}");
                }
                else
                {
                    Console.WriteLine("X");
                }
               

               
            }
            foreach (string Contents in Content)
            {     
                string path = Contents;
                path = path.Replace(Source, Dest) ;
               

                if (Cpy.AllowCpyFile(Contents) == true)
                {
                    File.Copy(Contents, path);
                    Writter.Add(Contents);
                    Console.WriteLine($"{Contents}");
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
