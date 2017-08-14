using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace DpParkingsParser.Models
{
    public class Attachment
    {
            public string Url { get; set; }
            [JsonProperty(PropertyName = "content-type")]
            public string ContentType { get; set; }
            public string Name { get; set; }
            public int Size { get; set; }
}
}