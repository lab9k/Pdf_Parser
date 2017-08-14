using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PDFParser;

namespace PDFParser_Core
{
    public class DataWrapper
    {
        /// <summary>
        /// Method to return the data as a stream. Usefull for when the parsed data does not need to be stored locally
        /// </summary>
        /// <param name="models"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static MemoryStream GetModelsAsStream(List<DataModel> models, OutputFormat format)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);

            if (format == OutputFormat.CSV)
            {
                foreach (DataModel m in models)
                {
                    writer.Write(m.CSV);
                }
            }
            else if (format == OutputFormat.JSON)
            {
                foreach (DataModel m in models)
                {
                    writer.Write(m.JSON);
                }
            }
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static MemoryStream GetModelsAsStream(string models)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            
            writer.Write(models);
            
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}