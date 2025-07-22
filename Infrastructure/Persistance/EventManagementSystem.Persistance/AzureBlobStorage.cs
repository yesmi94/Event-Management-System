// <copyright file="AzureBlobStorage.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Persistance
{
    using Azure.Storage.Blobs;
    using EventManagementSystem.Application.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;

    public class AzureBlobStorage : IAzureBlobStorage
    {
        private readonly BlobContainerClient containerClient;

        public AzureBlobStorage(IConfiguration config)
        {
            var connectionString = config["AzureStorage:ConnectionString"];
            var containerName = config["AzureStorage:ContainerName"];
            this.containerClient = new BlobContainerClient(connectionString, containerName);
            this.containerClient.CreateIfNotExists(); // Optional: ensure container exists
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var blobClient = this.containerClient.GetBlobClient(uniqueFileName);

            using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, overwrite: true);

            return blobClient.Uri.ToString(); // Returns the image URL
        }
    }
}
