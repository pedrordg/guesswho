using System.Diagnostics.CodeAnalysis;

namespace GuessWho.Infra.Blob.Configuration
{
    /// <summary>
    /// Blob Options
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class BlobOptions
    {
        /// <summary>
        /// Gets or sets the BLOB container URL.
        /// </summary>
        /// <value>
        /// The BLOB container URL.
        /// </value>
        public string BlobContainerName { get; set; }

        /// <summary>
        /// Gets or sets the name of the account.
        /// </summary>
        /// <value>
        /// The name of the account.
        /// </value>
        public string ConnectionString { get; set; }
    }
}
