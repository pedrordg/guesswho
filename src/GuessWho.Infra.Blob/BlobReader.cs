using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using GuessWho.Infra.Blob.Contracts;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GuessWho.Infra.Blob
{
    /// <inheritdoc cref="IBlobReader">
    /// <seealso cref="IBlobReader" />
    public class BlobReader : IBlobReader
    {
        /// <summary>
        /// The BLOB container client
        /// </summary>
        private readonly BlobContainerClient _blobContainerClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlobReader" /> class.
        /// </summary>
        /// <param name="blobContainerClient">The BLOB container client.</param>
        public BlobReader(BlobContainerClient blobContainerClient)
        {
            _blobContainerClient = blobContainerClient;
            _blobContainerClient.CreateIfNotExistsAsync();
        }

        /// <inheritdoc />
        public async Task<byte[]> DownloadContent(string blobPath)
        {
            BlobClient blob = _blobContainerClient.GetBlobClient(blobPath);

            using MemoryStream stream = new MemoryStream();
            await blob.DownloadToAsync(stream);

            return stream.ToArray();
        }

        /// <inheritdoc />
        public bool Exists(Guid id) => GetBlob(id).Any();

        /// <inheritdoc />
        public BlobItem Find(Guid id) => GetBlob(id).FirstOrDefault();

        /// <inheritdoc />
        private Pageable<BlobItem> GetBlob(Guid id) => _blobContainerClient.GetBlobs(BlobTraits.Metadata, prefix: id.ToString());
    }
}
