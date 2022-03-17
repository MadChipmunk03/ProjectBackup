using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fadsfas
{
    class Program
    {
       
        static void Main(string[] args)
        {
            Run Daemon = new Run();
            Daemon.Launch();
          /// Daemon.TestValues();
            Console.ReadKey(true);
        }
    }
}
