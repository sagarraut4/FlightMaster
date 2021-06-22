using Azure;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using System;
using System.IO;
using System.Threading.Tasks;


namespace FlightBookingSystem.Common.Helpers
{
    public class FileShareHelper : IFileShareHelper
    {
        private readonly ShareClient fileContainerClient;
        private readonly string fileConnectionString;
        private readonly string fileContainerName;

        public FileShareHelper()
        {
            fileConnectionString = Environment.GetEnvironmentVariable("fileConnectionString");
            fileContainerName = Environment.GetEnvironmentVariable("fileContainerName");
            fileContainerClient = new ShareClient(fileConnectionString, fileContainerName);
            fileContainerClient.CreateIfNotExistsAsync().Wait();

        }
        public async Task Upload(string folderName, string fileName, Stream inputFileStream)
        {

            // Ensure that the share exists
            if (await fileContainerClient.ExistsAsync())
            {

                // Get a reference to the sample directory
                ShareDirectoryClient directory = fileContainerClient.GetDirectoryClient(folderName);

                // Create the directory if it doesn't already exist
                await directory.CreateIfNotExistsAsync();

                // Ensure that the directory exists
                if (await directory.ExistsAsync())
                {
                    ShareFileClient file = directory.GetFileClient(fileName);
                    await file.DeleteIfExistsAsync();
                    

                    file.Create(inputFileStream.Length);
                    file.UploadRange(
                        new HttpRange(0, inputFileStream.Length),
                        inputFileStream);
                }
            }
        }
        public async Task<Response> Delete(string folderName, string fileName)
        {

            // Ensure that the share exists
            if (await fileContainerClient.ExistsAsync())
            {

                // Get a reference to the sample directory
                ShareDirectoryClient directory = fileContainerClient.GetDirectoryClient(folderName);

                // Create the directory if it doesn't already exist
                await directory.CreateIfNotExistsAsync();

                // Ensure that the directory exists
                if (await directory.ExistsAsync())
                {
                    ShareFileClient file = directory.GetFileClient(fileName);
                    // Ensure that the file exists
                    if (await file.ExistsAsync())
                    {

                        return await file.DeleteAsync();

                    }
                }
            }
            return await Task.FromResult<Response>(null);
        }

        public async Task<ShareFileDownloadInfo> Download(string folderName, string fileName)
        {
            // Ensure that the share exists
            if (await fileContainerClient.ExistsAsync())
            {

                // Get a reference to the sample directory
                ShareDirectoryClient directory = fileContainerClient.GetDirectoryClient(folderName);

                // Create the directory if it doesn't already exist
                await directory.CreateIfNotExistsAsync();

                // Ensure that the directory exists
                if (await directory.ExistsAsync())
                {
                    ShareFileClient file = directory.GetFileClient(fileName);
                    // Ensure that the file exists
                    if (await file.ExistsAsync())
                    {

                        ShareFileDownloadInfo download = file.Download();
                        return await Task.FromResult<ShareFileDownloadInfo>(download);

                    }
                }
            }
            return await Task.FromResult<ShareFileDownloadInfo>(null);

        }
    }
}
