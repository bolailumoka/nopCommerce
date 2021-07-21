using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Nop.Plugin.Api.Processing.Constants;

namespace Nop.Plugin.Api.Processing.Utilities
{
    public static class BlobStorageUtility
    {
        public static async Task UploadFile(string fullFileName, string fileName)
        {
            var container = GetContainerClient();
            BlobClient blob = container.GetBlobClient(fileName);
            await blob.DeleteIfExistsAsync();

            using (FileStream file = File.OpenRead(fullFileName))
            {
                await blob.UploadAsync(file);
            }

        }

        public static async Task DownloadFile(string fileName, string localFilePath)
        {
            try
            {
                var container = GetContainerClient();
                BlobClient blob = container.GetBlobClient("Blob");

                BlobDownloadInfo download = await blob.DownloadAsync();
                using (FileStream file = File.OpenWrite(localFilePath))
                {
                    await download.Content.CopyToAsync(file);
                }
            }
            catch (RequestFailedException ex)
            when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
            {

            }

        }

        private static BlobContainerClient GetContainerClient()
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(AzureStorageConstants.StorageConnectionString);
            BlobContainerClient container = blobServiceClient.GetBlobContainerClient(AzureStorageConstants.ContainerName);
            return container;
    }
    }
}
