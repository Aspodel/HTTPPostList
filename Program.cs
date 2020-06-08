using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ListHTTPC_
{
    class Program
    {
        private static readonly HttpClientHandler handler = new HttpClientHandler() { ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator };
        private static List<Image> Images;
        static void Main(string[] args)
        {
            Images = new List<Image>();
            Image img1 = new Image();
            img1.ImageURL = "Dat ga";
            img1.ImageFormat = "dam ba may cai httpcontext";
            Image img2 = new Image();
            img2.ImageURL = "Dat ga 1";
            img2.ImageFormat = "dam ba may cai httpcontext 1";
            Images.Add(img1);
            Images.Add(img2);
            DoPost(Images).GetAwaiter().GetResult();
        }
        public static async Task DoPost(List<Image> i)
        {
            var client = new HttpClient(handler);

            var values = new Dictionary<string, object>
            {
               {"BlogTitle", "Hello"},
               {"BlogContent", "DKMMM"},
               {"ImagesList", i}
            };
            var content = new StringContent(JsonConvert.SerializeObject(values), Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PostAsync("https://localhost:44361/api/Posts/NewBlog", content);
                string Status = response.StatusCode.ToString();
                var responseString = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine(Status);
                if (Status == "OK")
                {
                    System.Console.WriteLine("Cool!! Sent that bitch");
                }
                else
                {
                    System.Console.WriteLine("What the fuck is going on ???");
                }
            }
            catch (Exception)
            {
                throw new Exception("Some thing wrong");
            }
        }
    }
}
