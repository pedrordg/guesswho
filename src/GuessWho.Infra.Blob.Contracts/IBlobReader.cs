using Azure.Storage.Blobs.Models;
using System;
using System.Threading.Tasks;

namespace GuessWho.Infra.Blob.Contracts
{
    /// <summary>
    /// Execute Blob reader operations
    /// </summary>
    public interface IBlobReader
    {
        /// <summary>
        /// Downloads the content.
        /// </summary>
        /// <param name="blobPath">The BLOB path.</param>
        /// <returns></returns>
        Task<byte[]> DownloadContent(string blobPath);

        /// <summary>
        /// Check if the specified identifier exist.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        bool Exists(Guid id);

        /// <summary>
        /// Finds the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        BlobItem Find(Guid id);
    }
}