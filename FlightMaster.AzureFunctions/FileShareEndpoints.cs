using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FlightBookingSystem.Common.Helpers;
using Azure;

namespace FlightMaster.AzureFunctions
{
    public static class FileShareEndpoints
    {
        [FunctionName("uploadFile")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "upload/{folderName}")] HttpRequest req, string folderName,
            ILogger log)
        {
            var formdata = await req.ReadFormAsync();
            var file = req.Form.Files[0];
            FileShareHelper fileShareHelper = new FileShareHelper();
            await fileShareHelper.Upload(folderName, file.FileName, file.OpenReadStream());
            return new OkObjectResult(file.FileName + " - " + file.Length.ToString());
        }

        [FunctionName("downloadFile")]
        public static async Task<HttpResponseMessage> downloadFile(
           [HttpTrigger(AuthorizationLevel.Function, "get", Route = "download/{folderName}/{fileName}")] HttpRequest req, string folderName, string fileName,
           ILogger log)
        {
            FileShareHelper fileShareHelper = new FileShareHelper();
            var downloadInfo = await fileShareHelper.Download(folderName,fileName);
            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            response.Content = new StreamContent(downloadInfo.Content);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(downloadInfo.ContentType);
            return response;
        }

        [FunctionName("deleteFile")]
        public static async Task<string> deleteFile(
           [HttpTrigger(AuthorizationLevel.Function, "get", Route = "delete/{folderName}/{fileName}")] HttpRequest req, string folderName, string fileName,
           ILogger log)
        {
            FileShareHelper fileShareHelper = new FileShareHelper();
            var response = await fileShareHelper.Delete(folderName, fileName);
            
            return response.ReasonPhrase;
        }

    }
}
