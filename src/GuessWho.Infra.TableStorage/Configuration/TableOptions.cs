// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TableOptions.cs" company="Five Degrees">
// Copyright (c) Five Degrees. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

namespace Matrix.PaymentGateway.Infra.TableStorage.Configuration
{
    [ExcludeFromCodeCoverage]
    public class TableOptions
    {
        /// <summary>
        /// Gets or sets the storage URL.
        /// </summary>
        /// <value>
        /// The storage URL.
        /// </value>
        public string StorageUrl { get; set; }

        /// <summary>
        /// Gets or sets the name of the account.
        /// </summary>
        /// <value>
        /// The name of the account.
        /// </value>
        public string AccountName { get; set; }

        /// <summary>
        /// Gets or sets the account key.
        /// </summary>
        /// <value>
        /// The account key.
        /// </value>
        public string AccountKey { get; set; }
    }
}
