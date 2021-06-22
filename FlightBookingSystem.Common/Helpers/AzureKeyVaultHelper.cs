using Microsoft.Azure.KeyVault;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.Common.Helpers
{
    public static class AzureKeyVaultHelper
    {

        public static string GetConnectionString(string clinetID, string clientSecret, string keyVaultName)
        {

            Task<string> connection = Task.Run<string>(
                                        async () => await AzureKeyVaultHelper.GetConnectionStringFromKeyVaultAsync(clinetID, clientSecret, keyVaultName));
            return connection.Result;
        }
        public static async Task<string> GetConnectionStringFromKeyVaultAsync(string clinetID, string clientSecret, string keyVaultName)
        {
            var client = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(
               async (string auth, string res, string scope) =>
               {
                   var authContext = new AuthenticationContext(auth);
                   var credential = new ClientCredential(clinetID, clientSecret);
                   AuthenticationResult result = await authContext.AcquireTokenAsync(res, credential);
                   return result.AccessToken;
               }
               ));
            var secretData = await client.GetSecretAsync(keyVaultName, "SqlConnectionStringKV");
            return secretData.Value;
        }
    }
}
