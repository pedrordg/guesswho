// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigureBlobOptions.cs" company="Five Degrees">
// Copyright (c) Five Degrees. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Matrix.PaymentGateway.Infra.Blob.Configuration
{
    /// <summary>
    /// Configure Blob Options
    /// </summary>
    /// <seealso cref="IConfigureOptions{StorageOptions}" />
    public class ConfigureBlobOptions : IConfigureOptions<BlobOptions>
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureBlobOptions"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public ConfigureBlobOptions(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Invoked to configure a <typeparamref name="TOptions" /> instance.
        /// </summary>
        /// <param name="options">The options instance to configure.</param>
        public void Configure(BlobOptions options)
        {
            _configuration.GetSection("AzureBlob").Bind(options);
        }
    }
}
