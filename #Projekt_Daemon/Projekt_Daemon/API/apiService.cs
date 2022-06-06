using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace Projekt_Daemon
{
    public class apiService
    {

        private HttpClient Client;
        public int ConnectionID;
       

        public apiService()
        {   
            this.Client = new HttpClient();
            this.Client.BaseAddress = new Uri("http://localhost:5502");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 11, 0);
            Console.WriteLine("");
            Console.WriteLine("");
        }
  

        public async Task<List<Models.PendingChange>> GetChange(int Yur_ID)    //Getting config by id from connection
        {
            string result = await this.Client.GetStringAsync($"/api/ForDeamon/pendingchanges/{Yur_ID}"); //<connection config id          
            List<Models.PendingChange> myobj = JsonConvert.DeserializeObject < List < Models.PendingChange> > (result);

            return myobj;
        }



        public async Task<Model> Getmod(int configID)    //Getting config by id from connection
        {
            string result = await this.Client.GetStringAsync($"/api/ForDeamon/config/{configID}"); //<connection config id
            Model Output = JsonConvert.DeserializeObject<Model>(result);
            string Alias = Output.Alias;
            Console.SetCursorPosition(Console.WindowWidth / 2 - (Alias.Length / 2), 1);
            Console.WriteLine(Alias);
            return Output;
        }



        public async Task<int> RegisterNewUser(Models.Daemon Info)  //Registrace a taky získání ID
        {     
            string result = await this.Client.GetStringAsync($"/api/ForDeamon/register?IP_Address={Info.IP_Address}");
            Models.Daemon xx = JsonConvert.DeserializeObject<Models.Daemon>(result);
            int DaemonID = xx.Id;
            return DaemonID;
        }





        public async Task NewEvent(string message, int type)
        {
    
            //types: 0 - successfulBackup, 1 - error, 2- info
            Models.Event NewEven = new Models.Event() { ConnectionId = ConnectionID, Message = message, Status = true, Time = DateTime.Now, Type = type };
            Console.WriteLine($"[{DateTime.Now}] Uploaded event;"); 
            await this.Client.PostAsJsonAsync("/api/ForDeamon/event", NewEven);
        }

        public async Task GetConnectionByDaemonID(int DaemonID)
         {
             string result = await this.Client.GetStringAsync($"/api/ForDeamon/connections/{DaemonID}"); //daemon id = Your_ID
             List<Models.Connection> Output = JsonConvert.DeserializeObject<List<Models.Connection>>(result);
            this.ConnectionID = Output[0].Id;
       
           
         }
    }
}
