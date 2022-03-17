using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace fadsfas
{
    public class Config
    {
        public string BackUpType;
        public string[] DestinationFolder;
        public string sourcefolder;
        private StreamReader Reader = new StreamReader(@"C:\Users\adasu\Desktop\config.txt");
        private Logs LogWriter = new Logs();

        public void LoadConfig()
        {
            this.BackUpType = Reader.ReadLine();
            string DestinationToSplit = Reader.ReadLine();
            string SourceToSplit = Reader.ReadLine();
            if(BackUpType == null)
            {
                LogWriter.Error("File is missing Type of Backup!");
            } 
            if(DestinationToSplit == null)
            {
                LogWriter.Error("File is missing Destination list");
            }
            if (SourceToSplit == null)
            {
                LogWriter.Error("File is missing source List");
            }

            this.DestinationFolder = DestinationToSplit.Split(';');
            this.sourcefolder = SourceToSplit;
            Reader.Close();
           
              
            
          
        }
        
    }
}
