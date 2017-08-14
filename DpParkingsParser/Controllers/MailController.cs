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
using Newtonsoft.Json;
using PDFParser_Core;

namespace DpParkingsParser.Controllers
{
    public class MailController : ApiController
    {
        // GET api/values
        public IHttpActionResult Get()
        {
            return NotFound();
        }

        // GET api/values/5
        public IHttpActionResult Get(int id)
        {
            return NotFound();
        }

        // POST api/values
        [HttpPost]
        public IHttpActionResult Post(FormDataCollection model)
        {
            CloudStorageManager manager = new CloudStorageManager();

            string json = model.Get("attachments");

            List<Attachment> attachments = JsonConvert.DeserializeObject<List<Attachment>>(json);
                    AttachmentHandler handler = new AttachmentHandler();
                    handler.DownloadAndParse(attachments);
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