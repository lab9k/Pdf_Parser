using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFParser
{
    class DataModel
    {
        public int Transponder  { get; set; }
        public DateTime ActionTime { get; set; }
        public string Device { get; set; }
        public string Facility { get; set; }
        public double Bedrag { get; set; }

        public string CSVHeader
        {
            get { return "Transponder;ActionTime;Device;Facility"; }
        }

        public string CSV
        {
            get
            {
                return Transponder + ";" + ActionTime + ";" + Device + ";" + Facility;
            }
        }

        public string JSON
        {
            get
            {
                return
                    $"{{\n" +
                    $" \"Transponder\": \"{Transponder}\",\n" +
                    $" \"ActionTime\": \"{ActionTime}\",\n" +
                    $" \"Device\": \"{Device}\",\n" +
                    $" \"Facility\": \"{Facility}\"\n" +
                    $"}}";
            }
        }

        private bool IsTransponder(string text)
        {
            int number;
            return text.Length >= 8 && Int32.TryParse(text, out number);
        }

        private bool IsActionTime(string[] words, int index)
        {
            if (index < words.Length - 1)
            {
                DateTime time;
                return DateTime.TryParse(words[index] + " " + words[index + 1], out time);
            }
            else
            {
                return false;
            }
        }

        private bool IsDevice(string[] words, int index)
        {
            return words[index] == "Ingang" || words[index] == "Uitgang";
        }
        private bool IsFacility(string[] words, int index)
        {
            return words[index] == "Public";
        }

        public DataModel(string datamodel)
        {
            string[] words = datamodel.Split(' ');
            if (words.Length > 6)
            {
                for (var i = 0; i < words.Length; i++)
                {

                    if (IsTransponder(words[i]))
                    {
                        Transponder = int.Parse(words[i]);
                    }
                    else if (IsActionTime(words, i))
                    {
                        ActionTime = DateTime.Parse(words[i] + " " + words[i + 1]);
                    }
                    else if (IsDevice(words, i))
                    {
                        Device = words[i - 1] + " " + words[i];
                    }
                    else if (IsFacility(words, i))
                    {
                        Facility = words[i - 1] + " " + words[i] + " " + words[i + 1];
                        if (i + 2 < words.Length && words[i + 2].Length == 1)
                        {
                            Facility += words[i + 2]; //sometimes there is a whitespace between '-' and floornumber
                        }
                    }
                }
            }
        }

        
    }
}
