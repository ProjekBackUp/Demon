using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BackupClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5244");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }

            //Backup backup = new Backup();
            //backup.BackupData(); 
        }

        static async Task GetById(HttpClient client, string id)
        {
            try
            {
                HttpResponseMessage responce = await client.GetAsync("api/employees" + id);
                responce.EnsureSuccessStatusCode();

                Info info = await responce.Content.ReadAsStringAsync<Info>();

                Console.WriteLine("123");
                      
            }
            catch (HttpRequestException e)
            {

                Console.WriteLine(e.Message);
            }
        }

        static async Task GetALL(HttpClient client)
        {
            try
            {
                HttpResponseMessage responce = await client.GetAsync("api/amployees");
                responce.EnsureSuccessStatusCode();

                List<Info> info = await responce.Content.ReadAsAsync<Info>();

                foreach (Info i in info)
                {
                    Console.WriteLine("123");
                } 

            }
            catch (HttpRequestException e)
            {

                Console.WriteLine(e.Message);
            }
        }

        static async Task Post(HttpClient client)
        {
            try
            {
                HttpResponseMessage responce = await client.GetAsync("api/amployees");
                responce.EnsureSuccessStatusCode();

                List<Info> info = await responce.Content.ReadAsAsync<Info>();

                foreach (Info i in info)
                {
                    Console.WriteLine("123");
                }

            }
            catch (HttpRequestException e)
            {

                Console.WriteLine(e.Message);
            }
        }

    }
}
