// <copyright file="IAzureBlobStorage.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.Interfaces
{
    using Microsoft.AspNetCore.Http;

    public interface IAzureBlobStorage
    {
        Task<string> UploadImageAsync(IFormFile file);
    }
}
