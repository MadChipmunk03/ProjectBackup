using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using System.Text;
using System.Threading.Tasks;

namespace Projekt_Daemon
{
   public class apiService
    {

        private HttpClient Client;
   
        private int IDtier;

        public  apiService()
        {
            this.IDtier = 515;
            this.Client = new HttpClient();
            this.Client.BaseAddress = new Uri("http://localhost:5502");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 11, 0);
            Console.WriteLine(this.Client.BaseAddress);
            Console.WriteLine("");
        }

        public async Task<Models.PendingChange> GetChange(int Yur_ID)    //Getting config by id from connection
        {
            string result = await this.Client.GetStringAsync($"/api/ForDeamon/pendingchanges/{Yur_ID}"); //<connection config id

            
            if(string.IsNullOrEmpty(result))
            {
                Console.WriteLine("XASA");
            }
            
            Models.PendingChange myobj =    JsonConvert.DeserializeObject<Models.PendingChange>(result.Substring(1, result.Length - 2));
          
           


            return myobj;

        }



        public async Task<Model> Getmod(int configID)    //Getting config by id from connection
        {

            string result = await this.Client.GetStringAsync($"/api/ForDeamon/config/{configID}"); //<connection config id

            Model Output = JsonConvert.DeserializeObject<Model>(result);
            
            string Alias = Output.Alias;
            Console.SetCursorPosition(Console.WindowWidth / 2 - (Alias.Length/2), 1);
            Console.WriteLine(Alias);
            


            return Output;

        }



        public async Task<int> RegisterNewUser(Models.Daemon Info)  //Registrace a taky získání ID
        {
            IDtier++;
            string result = await this.Client.GetStringAsync($"/api/ForDeamon/register?IP_Address={Info.IP_Address}");
            Models.Daemon xx = JsonConvert.DeserializeObject<Models.Daemon>(result);
       

            int DaemonID = xx.Id;
            return DaemonID;
        }

     



        public async Task NewEvent(string message, int type)
        {
            IDtier++;
            //types: 0 - successfulBackup, 1 - error, 2- info
            Models.Event NewEven = new Models.Event() { ConnectionId = 1000, Id = IDtier, Message = message, Status = true, Time = DateTime.Now, Type = type };
            await this.Client.PostAsJsonAsync("/api/ForDeamon/event", NewEven);

        }

       /* public async Task<Models.Connection> GetConnectionByDaemonID(int DaemonID)
        {
            string result = await this.Client.GetStringAsync($"api/ForDeamon/pendingchanges/{DaemonID}"); //daemon id = Your_ID

            Models.Connection Output = JsonConvert.DeserializeObject<Models.Connection>(result);
            return Output;
        }*/
    }
}
