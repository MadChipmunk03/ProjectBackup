using System;

namespace fadsfas
{
    public class Run
    {
        public Config Config = new Config();
        public Algorithm System = new Algorithm();
        public void Launch()
        {
            Config.LoadConfig();   
            System.Start(Config);
        }


        public void TestValues()
        {
            Console.WriteLine(Config.BackUpType);
            foreach (string item in Config.DestinationFolder)
            {
                Console.WriteLine(item);
            }
           /* foreach (string item in Config.sourcefolder)
            {
                Console.WriteLine(item);
            }*/
        }

        
    }
}
