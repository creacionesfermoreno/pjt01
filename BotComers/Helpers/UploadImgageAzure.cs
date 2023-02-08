
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Web;

namespace BotComers.Helpers
{
    public class UploadImgageAzure
    {
        public static string UploadFilesAzure(HttpPostedFile upload, string fileName, string subdominio)
        {
            CloudStorageAccount cloudStorageAccount = ConnectionString.GetConnectionString();
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            string strContainerName = subdominio.ToLower().ToString();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(strContainerName);
            if (cloudBlobContainer.CreateIfNotExists())
            {
                cloudBlobContainer.SetPermissionsAsync(
                   new BlobContainerPermissions
                   {
                       PublicAccess = BlobContainerPublicAccessType.Blob
                   }
                   );
            }

            //string imageName = Guid.NewGuid().ToString() + "-" + Path.GetExtension(upload.FileName);

            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
            cloudBlockBlob.Properties.ContentType = upload.ContentType;
            cloudBlockBlob.UploadFromStream(upload.InputStream);

            return cloudBlockBlob.Uri.ToString();
        }
    }

    public static class ConnectionString
    {
        static string account = CloudConfigurationManager.GetSetting("StorageAccountName");
        static string key = CloudConfigurationManager.GetSetting("StorageAccountKey");
        public static CloudStorageAccount GetConnectionString()
        {
            string local = "DefaultEndpointsProtocol=https;AccountName=contenidosappsfit;AccountKey=gZbU56n6ta9JBLP9YS8OFjuaLJRGWPkIs0F0Y/01TBm9vRcLYNLOnDy2kt3myE5HApXWglLmWOgUr/aHxqSNEw==;EndpointSuffix=core.windows.net";
            //string connectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", account, key);
            string connectionString = local;
            return CloudStorageAccount.Parse(connectionString);
        }
    }
}