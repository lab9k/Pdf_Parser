using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using DpParkingsParser.Models;
using DpParkingsParser.Utilities;

namespace DpParkingsParser
{
    public class RequestManager
    {
        static HttpClient client = new HttpClient();

        public static async Task<List<MailgunEventItem>> GetProductAsync(string path)
        {
            
            List<MailgunEventItem> items = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                items = await response.Content.ReadAsAsync<List<MailgunEventItem>>();
            }
            return items;
        }

        public static MemoryStream DownloadFile(string path)
        {
            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", ConfigurationManager.AppSettings["authorizationKey"]);
            return new MemoryStream(wc.DownloadData(path));

        }
    }
}