using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Projekt_Daemon.API
{
    public class NewRegister
    {
        public string FilePath;
        private bool FirstTime;
        public int Your_ID;
        apiService Api = new apiService();
        private Functions.Base_Functions Func = new Functions.Base_Functions();
        public NewRegister(apiService Apiiii)
        {
            this.Api = Apiiii;
            this.FilePath = @"C:\Daemon\Config.txt";
            if (File.Exists(FilePath))
            {
              
                this.FirstTime = false;
            }
            else
            {
               
                Console.WriteLine("Registering a new user");
                Console.WriteLine(Func.GetLocalIPAddress());
               
                Thread.Sleep(2000);
                this.FirstTime = true;
                Console.Clear();
            }

        }
        public void ClearBottom()
        {
            for (int i = 0; i < Console.WindowHeight - 10; i++)
            {
                Console.SetCursorPosition(0, i + 3);
                Console.Write("".PadRight(Console.WindowWidth, ' '));
            }
            Console.SetCursorPosition(0, 4);

        }
        public void Checker()
        {
            if (this.FirstTime == true)
            {
             
                Models.Daemon Daemon = this.CreateUserInfo();


              
                this.Your_ID = Api.RegisterNewUser(Daemon).Result;
                Console.WriteLine($"Welcome! Your id is {Your_ID}");

                Api.NewEvent($"User created, test message id={Your_ID}", 2); 
              
                Directory.CreateDirectory(@"C:\Daemon\");
                using (StreamWriter wr = new StreamWriter(this.FilePath))
                {
                    wr.WriteLine(this.Your_ID);
                    wr.WriteLine(Daemon.Alias);
                    wr.WriteLine(Daemon.Id);
                    wr.WriteLine(Daemon.IP_Address);
                    wr.WriteLine(Daemon.Status);
                }
                Console.WriteLine("Real time update is not implemented yet! Please wait for server admin to finish settings"); //Počkat na Petra na dokončení svého bodu projektu ať to funguje real time
                Thread.Sleep(100000);
            } else
            {
               
                    var Filee = File.ReadAllLines(this.FilePath);
                    var List = new List<string>(Filee);
                    this.Your_ID =Convert.ToInt32(List[0]);
              
                
            }
        }

        public int Get_Your_ID ()
        {
            return this.Your_ID;
        }

        public Models.Daemon CreateUserInfo()
        {
            Models.Daemon result = new Models.Daemon();
            
            result.IP_Address = Func.GetLocalIPAddress();
           
            return result;
        }

    }
}
