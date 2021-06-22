using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.Common.Helpers
{
    public interface IBlobHelper
    {
        Task<AppendBlobClient> CreateBlobIfNotExists(string blobName);
        Task AddMessageInBlob(string blobName, string message, bool append = true);
        Task DeleteBlob(string blobName);
        Task DeleteBlobContainer();
    }   
}
