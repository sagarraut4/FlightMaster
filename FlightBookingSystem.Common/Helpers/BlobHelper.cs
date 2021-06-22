using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.Common.Helpers
{
    public class BlobHelper : IBlobHelper
    {

        private BlobContainerClient _blobContainerClient;
        private string blobConnectionString;
        private string blobContainerName;

        public async Task AddMessageInBlob(string blobName, string message, bool append = true)
        {
            var cloudBlob = await CreateBlobIfNotExists(blobName);
            if (cloudBlob != null)
            {
                var bytes = Encoding.UTF8.GetBytes(message);
                using (var stream = new MemoryStream(bytes))
                {
                    try
                    {
                        await cloudBlob.AppendBlockAsync(stream);
                    }
                    catch (System.Exception)
                    {
                        //suppress error for now
                    }
                }
            }
        }

        public async Task<AppendBlobClient> CreateBlobIfNotExists(string blobName)
        {
            try
            {
                CreateBlobContainerClientIfNoExitsts();
                var cloudBlob = _blobContainerClient.GetAppendBlobClient(blobName);
                await cloudBlob.CreateIfNotExistsAsync();
                return cloudBlob;

            }
            catch (System.Exception)
            {
                //suppress for now
            }
            return null;
        }

        private void CreateBlobContainerClientIfNoExitsts()
        {
            if (_blobContainerClient == null)
            {
                blobConnectionString = Environment.GetEnvironmentVariable("BlobConnectionString");
                blobContainerName = Environment.GetEnvironmentVariable("BlobContainerName");
                _blobContainerClient = new BlobContainerClient(blobConnectionString, blobContainerName);
                _blobContainerClient.CreateIfNotExistsAsync().Wait();
            }
        }

        public async Task DeleteBlob(string blobName)
        {
            try
            {
                var cloudBlob = _blobContainerClient.GetAppendBlobClient(blobName);
                await cloudBlob.DeleteIfExistsAsync();

            }
            catch (System.Exception)
            {
                //suppress for now
            }
        }

        public async Task DeleteBlobContainer()
        {
            try
            {
                if (_blobContainerClient != null)
                {
                    await _blobContainerClient.DeleteIfExistsAsync();
                }
            }
            catch (System.Exception)
            {
                //suppress for now
            }

        }
    }
}
