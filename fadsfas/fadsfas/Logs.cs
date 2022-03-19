using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace fadsfas
{
    public class Logs
    {
        public void Error(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;

        }
        public void Success(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void Copy(string FolderName)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{FolderName} was copied!");
            Console.ForegroundColor = ConsoleColor.White;

        }

        public void CreateLogFolder(string name) //Seznam zalohovanch souboru
        {
            StreamWriter Writer = new StreamWriter(@"C:\Users\adasu\Desktop\Destination\namelog.txt");
            Writer.WriteLine(name);
            Writer.Close();

        }
    }
}
