using DpParkingsParser.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using DpParkingsParser.Utilities;
using PDFParser;
using PDFParser_Core;

namespace DpParkingsParser
{
    public class AttachmentHandler
    {
        private CloudStorageManager manager;

        public AttachmentHandler()
        {
            manager = new CloudStorageManager();
        }
        public void DownloadAndParse(List<Attachment> attachments)
        {
            foreach (Attachment att in attachments)
            {
                if (att.ContentType.Contains("pdf"))
                {
                    //Download the attachments and get a reference to their location in memory
                    MemoryStream file = RequestManager.DownloadFile(att.Url);
                    
                    //Save the resulting pdf in blob
                    string filename = FileNameGenerator.Generate($"pdf/{att.Name}");
                    manager.AddBlob(file, filename);

                    //Get pdf as raw string
                    file.Seek(0, SeekOrigin.Begin);
                    PDFReadercs reader = new PDFReadercs();
                    string data = reader.GetStreamAsString(file);
                    file.Close();
                    //Remove junk data and get lines as array
                    DataFilter filter = new DataFilter();
                    List<string> lines = filter.FilterLines(data);

                    //Create List of DataModels from lines
                    List<DataModel> models = new List<DataModel>();
                    foreach (string line in lines)
                    {
                        DataModel model = new DataModel(line);
                        if(model.Transponder != 0)//ignore parsed junk
                            models.Add(model);
                    }

                    //Create stream from list of models as JSON
                    MemoryStream output = DataWrapper.GetModelsAsStream(models, OutputFormat.JSON);

                    //Write Stream to blobstorage
                    manager.AddBlob(output,FileNameGenerator.Generate($"json/{att.Name}"),".json",true);

                    output.Close();
                    //Create stream from list of models as JSON
                    output = DataWrapper.GetModelsAsStream(models, OutputFormat.CSV);

                    //Write Stream to blobstorage
                    manager.AddBlob(output, FileNameGenerator.Generate($"csv/{att.Name}"), ".csv", true);

                    output.Close();



                }
            }
        }

    }
}