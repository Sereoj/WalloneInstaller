using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WalloneInstaller.Services
{
    public class Partner
    {
        public string name { get; set; }
        public string icon { get; set; }
        public string category { get; set; }
        public string link { get; set; }
        public string download { get; set; }
    }
    public class RequestRouter
    {
        public static List<Partner> getPartners()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            var client = new HttpClient();
            var task = client.GetAsync("https://dev.w2me.ru/api/v2/partners/app/downloads");
            var response = task.Result;
            var jsonString = response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Partner>>(jsonString.Result);
        }
    }
}