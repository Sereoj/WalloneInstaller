using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
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

    public class ListEmails
    {
        public List<Email> emails { get; set; }
    }

    public class Auth
    {
        public string grant_type { get; set; } = "client_credentials";
        public string client_id { get; set; } = "173173ca9ef930ade6d5e2d609fe4b10";
        public string client_secret { get; set; } = "3f5046b9840fefb4ca342f7e17f38fd8";
    }

    public class Token
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
    }
    public class Email
    {
        public string email { get; set; }
        public Email(string email)
        {
            this.email = email;
        }
    }
    public class RequestRouter
    {
        public static List<Partner> getPartners()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            try
            {
                var client = new HttpClient();
                var task = client.GetAsync("https://dev.w2me.ru/api/v2/partners/app/downloads");
                var response = task.Result;
                var jsonString = response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Partner>>(jsonString.Result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Partner>();
            }
        }
        public static string EmailRequest(string email)
        {
            var client = new HttpClient();

            Token token = JsonConvert.DeserializeObject<Token>(Auth());
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.access_token}");

            var emails = new List<Email>
            {
                new Email(email)
            };

            var list = new ListEmails() { emails = emails };

            var content = new StringContent(JsonConvert.SerializeObject(list), Encoding.UTF8, "application/json");

            var task = client.PostAsync("https://api.sendpulse.com/addressbooks/1557856/emails", content);
            var response = task.Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        public static string Auth()
        {
            var client = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(new Auth()), Encoding.UTF8, "application/json");
            var task = client.PostAsync("https://api.sendpulse.com/oauth/access_token", content);
            var response = task.Result;
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}