using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DpParkingsParser.Models
{
    public class MailgunEventItem
    {
        public List<object> Tags { get; set; }
        public double Timestamp { get; set; }
        public Storage Storage { get; set; }
        public string Id { get; set; }
        public List<object> Campaigns { get; set; }
        public Message Message { get; set; }
        public string Event { get; set; }

        public override string ToString()
        {
            return $"MailgunEvent, From: {Message?.headers?.from ?? "?"}, Url: {Storage?.url ?? "?"}";
        }
    }

    public class Storage
    {
        public string url { get; set; }
        public string key { get; set; }

        
    }

    public class Headers
    {
        public string to { get; set; }
        public string from { get; set; }
        public string subject { get; set; }
    }

    public class Message
    {
        public Headers headers { get; set; }
        public List<object> attachments { get; set; }
        public List<string> recipients { get; set; }
        public int size { get; set; }
    }
}