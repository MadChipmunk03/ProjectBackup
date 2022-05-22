using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Projekt_Daemon
{
    public class Run
    {
        public void Try() //Základ
        {
            apiService Ser = new apiService(); //Objekt
            API.NewRegister Register = new API.NewRegister(Ser);
            
            Register.Checker();
            Model Config = new Model();
            Functions.TimeSpecifier sp = new Functions.TimeSpecifier();
            Functions.Translator tran = new Functions.Translator();
            int Your_ID = Register.Get_Your_ID();
            Models.PendingChange Change = new Models.PendingChange();
            Change = Ser.GetChange(Your_ID).Result;       
            int errorCon = 1;        
            while (true)
            {
                try
                {     
                    Config = Ser.Getmod(Change.ConfigId).Result; //Získání configu přes ID z pending changes, chybí get na connections
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 2, 2);
                    Console.WriteLine(Your_ID);
                    break;
                }
                catch
                {
                    // Console.SetCursorPosition(0, 3);                
                    if (errorCon > 5)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"It's taking too long :(");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    errorCon++;
                }
            }
        /*    Functions.Backup B = new Functions.Backup();
            B.StartBackup(Config, Ser);*/

            //Wait for signal   
            DateTime Date = DateTime.Now;
             while (true)
               {
                 Thread.Sleep(2000);     //Každý 2 sekundy kontrola času           
                 if(sp.Timer(tran.GetTime(Config.TimePeriod), tran.GetWeekDay(Config.TimePeriod)))
                 {                    
                     Functions.Backup B = new Functions.Backup();
                     B.StartBackup(Config, Ser);
                     if(Change.ChangeType == 0)
                     {
                         Config = Ser.Getmod(Change.ConfigId).Result;
                     }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Ser.NewEvent("A backup was successful!", 0);
                    Console.WriteLine("Backup finished, this message will disapear after 1 minute!");
                    Thread.Sleep(60000); //Počká minutu jako pojistka před loopem
                    Register.ClearBottom();               
                 }
             }
        }
    }
}
