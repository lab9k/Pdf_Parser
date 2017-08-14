using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using DpParkingsParser.Models;

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
    }
}