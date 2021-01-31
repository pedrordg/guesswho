// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBlobWriter.cs" company="Five Degrees">
// Copyright (c) Five Degrees. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matrix.PaymentGateway.Infra.Blob.Contracts
{
    /// <summary>
    /// Execute blob upload operations
    /// </summary>
    public interface IBlobWriter
    {
        /// <summary>
        /// Uploads the BLOB asynchronous.
        /// </summary>
        /// <param name="blobPath">The BLOB path.</param>
        /// <param name="content">The content.</param>
        /// <param name="metadata">The metadata.</param>
        /// <returns></returns>
        Task UploadBlobAsync(string blobPath, byte[] content, IDictionary<string, string> metadata);

        /// <summary>
        /// Uploads the metadata asynchronous.
        /// </summary>
        /// <param name="blobPath">The BLOB path.</param>
        /// <param name="metadata">The metadata.</param>
        /// <returns></returns>
        Task UploadMetadataAsync(string blobPath, IDictionary<string, string> metadata);
    }
}
