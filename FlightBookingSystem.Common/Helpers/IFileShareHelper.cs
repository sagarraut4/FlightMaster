using Azure;
using Azure.Storage.Files.Shares.Models;
using System.IO;
using System.Threading.Tasks;

namespace FlightBookingSystem.Common.Helpers
{
    public interface IFileShareHelper
    {
        Task Upload(string folderName, string fileName, Stream inputFileStream);
        Task<ShareFileDownloadInfo> Download(string folderName, string fileName);
        Task<Response> Delete(string folderName, string fileName);
    }
}
