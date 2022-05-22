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
            
            Run Rn = new Run();
            Rn.Try();
            

           
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Program has ended!  {DateTime.Now}");
            Console.ReadKey(true);
        }


        /*Oh Great Cloud,
        Grant me the courage to write the code I can,
        to escalate the errors I cannot fix,
        and the wisdom to know the difference.
        Programming one day at a time,
        enjoying one function at a time;
        accepting bugs as a pathway to code complete;
        taking this terrible codebase as it is; 
        not as I would have it;
        trusting that all things will be made right
        if I surrender to writing tests and documentation;
        so that I may be reasonably happy at the end of the day
        and somewhat sane at the end of this release..*/


    }
}

