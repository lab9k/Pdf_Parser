using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using DpParkingsParser.Models;
using DpParkingsParser.Utilities;
using PDFParser_Core;

namespace DpParkingsParser.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<MailgunEventItem> Get()
        {
            return RequestManager
                .GetProductAsync(
                    @"https://api:key-ca0046b3974b0dd8d6e79ce6f53c984d@api.mailgun.net/v3/mail.lab9k.gent/events")
                .Result;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IHttpActionResult Post(FormDataCollection model)
        {
            CloudStorageManager manager = new CloudStorageManager();
            string s = "data: \n";

            foreach (var key in  model.ReadAsNameValueCollection().AllKeys)
            {
                s += $"{key}: {model.ReadAsNameValueCollection()[key]} \n";
            }
            manager.AddBlob(DataWrapper.GetModelsAsStream(s), "postdata.txt");

            return Ok();
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}