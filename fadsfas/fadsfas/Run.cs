using System.Threading;

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





    }
}
