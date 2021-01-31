// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigureBlobExtensions.cs" company="Five Degrees">
// Copyright (c) Five Degrees. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Azure.Storage.Blobs;
using Matrix.PaymentGateway.Infra.Blob.Configuration;
using Matrix.PaymentGateway.Infra.Blob.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Matrix.PaymentGateway.Infra.Blob.Extensions
{
    /// <summary>
    /// Configure Blob Extensions
    /// </summary>
    public static class ConfigureBlobExtensions
    {
        /// <summary>
        /// Configures the storage.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureBlob(this IServiceCollection services)
        {
            services.AddSingleton<IConfigureOptions<BlobOptions>, ConfigureBlobOptions>();
            services.AddTransient(provider =>
            {
                var storageOptions = provider.GetRequiredService<IOptions<BlobOptions>>();
                var uriString = storageOptions.Value.BlobContainerName ?? throw new Exception("BlobContainerName needs to be configured!");
                var connectionString = storageOptions.Value.ConnectionString ?? throw new Exception("BlobContainerName needs to be configured!");

                return new BlobContainerClient(connectionString, uriString);
            });

            services.AddScoped<IBlobReader, BlobReader>();
            services.AddScoped<IBlobWriter, BlobWriter>();
        }
    }
}
