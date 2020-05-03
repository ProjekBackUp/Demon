using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace BackupClient
{
    public class Backup
    {
        public void BackupData()
        {
            try
            {
                string compName = GetCompName();

                Info info = GetData(compName);
                if (info == null)
                {
                    Registr(compName);
                }
                else
                {
                    if (info.Authorize == false)
                        return;

                    DoIt(info);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("chyba: " + e.Message);  
            }

            Thread.Sleep(5000);
        }
        
        private string GetCompName()
        {
            return Environment.MachineName;
        }
        private void Registr(string CompName)
        {
            using (HttpClient client = new HttpClient())
            {
                Parameter parameter = new Parameter();
                parameter.ComputerName = CompName; 

                string url = "http://localhost:5244/api";
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                JavaScriptSerializer js = new JavaScriptSerializer();
                string jsonData = js.Serialize(parameter);
                
                StringContent content = new StringContent(jsonData, Encoding.UTF8,"application/json");

                var response = client.PostAsync(url + $"/BackupApi/Register", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Registrace: " + response.ReasonPhrase);
                }
                else
                {
                    Console.WriteLine("Registrace:" + response.ReasonPhrase);
                }

                return;
            }
        }
        private Info GetData(string compName)
        {
            using (HttpClient client = new HttpClient())
            {
                Info info = null;

                string url = "http://localhost:5244/api";
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync(url + $"/BackupApi/Data?computername={compName}").Result;

                if (response.IsSuccessStatusCode)
                {
                    string resultContent = response.Content.ReadAsStringAsync().Result;

                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    jss.MaxJsonLength = 33554432;
                    info = jss.Deserialize<Info>(resultContent);
                }
                else
                {
                    Console.WriteLine(response.ReasonPhrase);
                    if (response.StatusCode == HttpStatusCode.NotAcceptable)
                        info = new Info();
                }

                return info;
            }
        }

        private void DoIt(Info info)
        {
            //info.BackupFolderSource

            if (!Directory.Exists(info.BackupFolderSource))
                Console.WriteLine($"Složka {info.BackupFolderSource} která se má zálohovat neexistuje."); 

            Directory.CreateDirectory(info.BacklupFolderTarget);

            foreach(var file in Directory.GetFiles(info.BackupFolderSource))
                File.Copy(file, Path.Combine(info.BacklupFolderTarget, Path.GetFileName(file)),true);


            Console.WriteLine($"Záloha složky {info.BackupFolderSource} byla provedena do složky {info.BacklupFolderTarget}");

            //foreach (var directory in Directory.GetDirectories(info.BackupFolderSource))
            //    File.Copy(directory, Path.Combine(info.BacklupFolderTarget, Path.GetFileName(directory)));
        }
    }
}
