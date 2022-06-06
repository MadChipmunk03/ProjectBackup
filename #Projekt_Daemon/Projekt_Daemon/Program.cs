using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Daemon
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Run Rn = new Run();
            Rn.Try();
                 
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[{DateTime.Now}] Program has ended! Press any key to exit...");
            Console.ReadKey(true);
        }
    }
}

