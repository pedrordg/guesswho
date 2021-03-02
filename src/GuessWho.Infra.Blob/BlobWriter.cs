using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using GuessWho.Infra.Blob.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GuessWho.Infra.Blob
{
    /// <inheritdoc cref="IBlobWriter">
    /// <seealso cref="IBlobWriter" />
    public class BlobWriter : IBlobWriter
    {
        /// <summary>
        /// The BLOB container client
        /// </summary>
        private readonly BlobContainerClient _blobContainerClient;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<BlobWriter> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlobReader" /> class.
        /// </summary>
        /// <param name="blobContainerClient">The BLOB container client.</param>
        /// <param name="logger">The logger.</param>
        public BlobWriter(BlobContainerClient blobContainerClient, ILogger<BlobWriter> logger)
        {
            _blobContainerClient = blobContainerClient;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task UploadBlobAsync(string blobPath, byte[] content, IDictionary<string, string> metadata)
        {
            try
            {
                BlobClient blob = _blobContainerClient.GetBlobClient(blobPath);

                using (Stream stream = new MemoryStream(content))
                {
                    await blob.UploadAsync(stream);
                    await blob.SetMetadataAsync(metadata);
                }

                _logger.LogDebug("Blob was uploaded successful in path {BlobPath}", blobPath);
            }
            catch (Exception exception)
            {
                string errorMessage = $"An error occurred while trying upload blob to path '{blobPath}'";

                _logger.LogError(exception, errorMessage);

                throw;
            }
        }

        /// <inheritdoc />
        public async Task UploadMetadataAsync(string blobPath, IDictionary<string, string> metadata)
        {
            try
            {
                BlobClient blob = _blobContainerClient.GetBlobClient(blobPath);
                if (!await blob.ExistsAsync().ConfigureAwait(false)) return;

                var prefix = Path.GetDirectoryName(blobPath);
                var blobItem = _blobContainerClient.GetBlobs(BlobTraits.Metadata, prefix: string.IsNullOrWhiteSpace(prefix) ? blobPath : prefix).FirstOrDefault();
                foreach (var entry in metadata)
                {
                    blobItem.Metadata.TryAdd(entry.Key, entry.Value);
                }

                await blob.SetMetadataAsync(blobItem.Metadata).ConfigureAwait(false);

                _logger.LogDebug("Blob metadata was uploaded successful in path {BlobPath}", blobPath);

            }
            catch (Exception exception)
            {
                string errorMessage = $"An error occurred while trying upload blob metadata in path '{blobPath}'";

                _logger.LogError(exception, errorMessage);

                throw;
            }
        }
    }
}
