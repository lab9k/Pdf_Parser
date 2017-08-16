using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace DpParkingsParser.Utilities
{
    public class CloudStorageManager
    {
        private CloudStorageAccount storageAccount;
        private CloudBlobClient blobClient;
        private CloudBlobContainer container;

        public CloudStorageManager()
        {
            storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));
            // Create the blob client.
            blobClient = storageAccount.CreateCloudBlobClient();
            // Retrieve reference to a previously created container.
            container = blobClient.GetContainerReference("dgdvobvpst01-parkingdata-dump");
        }

        public void AddBlob(string path, string blobname, bool overwrite = true)
        {
            using (var fileStream = System.IO.File.OpenRead(path))
            {
                AddBlob(fileStream,blobname);
            }

        }

        public void AddBlob(Stream stream, string blobname,string extension = "", bool overwrite = true)
        {
            CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{blobname}{extension}");
            int count = 1;
            while (!overwrite && blockBlob.Exists())
            {
                blockBlob = container.GetBlockBlobReference($"{blobname}-{count++}{extension}");
            }
            blockBlob.UploadFromStream(stream);

        }
    }
}