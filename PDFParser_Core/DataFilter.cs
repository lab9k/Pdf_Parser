using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFParser
{
    public class DataFilter
    {

        public List<string> FilterLines(string text)
        {
            List<string> lines = text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None).ToList();

            for (int i = 0; i < lines.Count; i++)
            {
                if (!lines[i].StartsWith("DP "))
                {
                    lines.RemoveAt(i);
                }
            }
            return lines;
        }
    }
}
