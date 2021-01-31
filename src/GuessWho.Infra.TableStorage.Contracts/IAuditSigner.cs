// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAuditSigner.cs" company="Five Degrees">
// Copyright (c) Five Degrees. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Azure.Cosmos.Table;

namespace Matrix.PaymentGateway.Infra.TableStorage.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuditSigner
    {
        void Sign(TableOperation operation);

        void Sign(TableBatchOperation operationBatch);
    }
}
