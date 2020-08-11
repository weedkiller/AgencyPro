// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using AgencyPro.Core.Infrastructure.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AgencyPro.Services.Storage
{
    public class StorageService : IStorageService
    {
        private readonly IConfiguration _configuration;

        public StorageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> StorePngImageAtPath(IFormFile image, string container, string path)
        {
            if (image.Length == 0) throw new ArgumentException("image");

            var inputStream = new MemoryStream();
            var pngStream = new MemoryStream();

            await image.CopyToAsync(inputStream);

            var bmp = new Bitmap(inputStream);
            bmp.Save(pngStream, ImageFormat.Png);

            CloudStorageAccount storageAccount;

            var storageConnectionString = _configuration
                .GetConnectionString("workforce_AzureStorageConnectionString");

            if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                var cloudBlobClient = storageAccount.CreateCloudBlobClient();
                //var properties = await cloudBlobClient.GetServicePropertiesAsync();
                var cloudBlobContainer = cloudBlobClient.GetContainerReference(container);
                await cloudBlobContainer.CreateIfNotExistsAsync();

                var permissions = new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                };
                await cloudBlobContainer.SetPermissionsAsync(permissions);

                var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(path + ".png");
                cloudBlockBlob.Properties.ContentType = "image/png";
                cloudBlockBlob.Properties.ContentDisposition = "inline";
                //await cloudBlockBlob.SetPropertiesAsync();
                pngStream.Seek(0, SeekOrigin.Begin);
                await cloudBlockBlob.UploadFromStreamAsync(pngStream);

                path = cloudBlockBlob.Uri.ToString();

                return path;
            }

            throw new Exception("storage account not configured");
        }
    }
}