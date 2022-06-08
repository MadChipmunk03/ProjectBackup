using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Projekt_Daemon
{
    public class Run
    {
        public void Try() //Základ
        {
            Console.Title = "LOADING...";
            apiService Ser = new apiService(); //Objekt
            API.NewRegister Register = new API.NewRegister(Ser);
            Register.Checker();
            Model Config = new Model();
            Functions.TimeSpecifier sp = new Functions.TimeSpecifier();
            Functions.Translator tran = new Functions.Translator();
           
            List<Models.PendingChange> Penlist = new List<Models.PendingChange>();
            int Your_ID = Register.Get_Your_ID();
           
            Thread.Sleep(5000);
            while (true)
            {
                try
                {
                   
                    Penlist = Ser.GetChange(Your_ID).Result;
                   if(Penlist.Count == 0)
                    {
                        throw new Exception();
                    } else
                    {
                        Console.SetCursorPosition(5, 5);
                        Console.Write("".PadRight(10, ' '));
                        Config = Ser.Getmod(Penlist[0].ConfigId).Result; //Získání configu přes ID z pending changes        
                        Console.SetCursorPosition(Console.WindowWidth / 2 - 2, 2);
                        Console.WriteLine(Your_ID);
                        Console.WriteLine($"[{DateTime.Now}] Loaded Config {Config.Id}");
                        Console.Title = $"DAEMON({Your_ID}) CONNECTED";
                        break;
                    }

                        
                    
                   
                }
                catch
                {
                    Console.SetCursorPosition(5, 5);
                    Console.Write($"[{DateTime.Now}]Waiting for JOB ({Your_ID})");
                  
                }
            }
            string BonusMsg = "Version: checkpoint10v2";
            Console.SetCursorPosition(Console.WindowWidth - BonusMsg.Length - 1, 0);
            Console.Write(BonusMsg);

            //Wait for signal   
            DateTime Date = DateTime.Now;

            DirectoryInfo TempLog = new DirectoryInfo(@"C:\TempLog\");
            Thread.Sleep(1000);
            Register.ClearBottom();
            List<Models.PendingChange> TEMPpendlist = new List<Models.PendingChange>();
            while (true)
            {
                Console.CursorVisible = false;
                Console.SetCursorPosition(0, 5);
                Console.Write($"Backup sheduled times: {Config.TimePeriod}");
                Ser.GetConnectionByDaemonID(Your_ID);
                Thread.Sleep(4000);     //Každý 4 sekundy kontrola času 
                TEMPpendlist = Ser.GetChange(Your_ID).Result;
           
           
                if(TEMPpendlist.Count > 0)
                {
                    Config = Ser.Getmod(TEMPpendlist[0].ConfigId).Result;
                   
                }
                if (sp.Timer(tran.GetTime(Config.TimePeriod), tran.GetWeekDay(Config.TimePeriod)))
                {
                    
                    Register.ClearBottom();
                    Functions.Backup B = new Functions.Backup();
                    B.StartBackup(Config, Ser);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Thread.Sleep(2000);
                    Register.ClearBottom();
                    Console.SetCursorPosition(0, 7);
                    Console.WriteLine($"[{DateTime.Now}]Backup (config{Config.Id}) finished!");
                    Ser.NewEvent($"A backup was successful!", 0); //Creates an evnet with ID range(Each deamon has 100 free ID slots in event category*/
                    Console.WriteLine("This message will disapear after 1 minute(or more...idk)");
                    Thread.Sleep(50000); //Minutová pojistka před provedením více BU za sebou
                    if (TempLog.Exists != false)
                    {
                        TempLog.Delete(true);
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    Register.ClearBottom();
                }
            }
        }
    }
}
