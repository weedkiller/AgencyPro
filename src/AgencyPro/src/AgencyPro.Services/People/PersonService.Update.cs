// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.People.Events;
using AgencyPro.Core.People.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Omu.ValueInjecter;

namespace AgencyPro.Services.People
{
    public partial class PersonService
    {
        public async Task<PersonResult> UpdateProfilePic(Guid personId, IFormFile image)
        {
            var retVal = new PersonResult();

            if (image.Length == 0) throw new ArgumentException("image");
            var path = await UploadProfilePic(personId, image);
            var person = await Repository.Queryable().Where(x => x.Id == personId).FirstAsync();
            person.ImageUrl = path;
            var records = await Repository.UpdateAsync(person, true);

            if (records > 0)
            {
                retVal.Succeeded = true;
                retVal.PersonId = personId;
                await Task.Run(() =>
                {
                    RaiseEvent(new PersonUpdatedEvent
                    {
                        PersonId = personId
                    });
                });
            }
          
            return retVal;
        }
        public async Task<string> UploadProfilePic(Guid personId, IFormFile image)
        {
            if (image.Length == 0) throw new ArgumentException("image");

            var inputStream = new MemoryStream();
            var pngStream = new MemoryStream();

            var path = personId.ToString();

            await image.CopyToAsync(inputStream);

            var bmp = new Bitmap(inputStream);
            bmp.Save(pngStream, ImageFormat.Png);


            CloudStorageAccount storageAccount;

            var storageConnectionString = _configuration
                .GetConnectionString("workforce_AzureStorageConnectionString");
            var person = await Repository.Queryable().Where(x => x.Id == personId)
                .FirstAsync();
            if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                var cloudBlobClient = storageAccount.CreateCloudBlobClient();
                //var properties = await cloudBlobClient.GetServicePropertiesAsync();
                var cloudBlobContainer = cloudBlobClient.GetContainerReference("people");
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
            return null;
        }

  
        public async Task<PersonResult> UpdatePersonDetails(Guid id, PersonDetailsInput input)
        {

            _logger.LogInformation(GetLogMessage("Person ID:{0}"),id);
            var retVal = new PersonResult()
            {
                PersonId = id
            };

            var entity = await Repository.FirstOrDefaultAsync(x => x.Id == id);
            entity.InjectFrom(input);

            var records = await Repository.UpdateAsync(entity, true);

            _logger.LogDebug(GetLogMessage("{0} records updated"));
            if (records > 0)
            {
                await Task.Run(() =>
                {
                    RaiseEvent(new PersonUpdatedEvent
                    {
                        PersonId = id
                    });
                });
            }

            return retVal;
        }
    }
}