// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StorageTable.cs" company="Five Degrees">
// Copyright (c) Five Degrees. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using GuessWho.Infra.TableStorage.Contracts;
using Microsoft.Azure.Cosmos.Table;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessWho.Infra.TableStorage
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTableEntity">The type of the table entity.</typeparam>
    /// <seealso cref="Matrix.PaymentGateway.Infra.TableStorage.Contracts.ITable{TTableEntity}" />
    public class StorageTable<TTableEntity> : ITable<TTableEntity> where TTableEntity : class, ITableEntity, IAudit, new()
    {
        private readonly CloudTable _table;
        private readonly IAuditSigner _auditSigner;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageTable{TTableEntity}" /> class.
        /// </summary>
        /// <param name="cloudTableClient">The cloud table client.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="auditSigner">The audit signer.</param>
        public StorageTable(CloudTableClient cloudTableClient, string tableName, IAuditSigner auditSigner)
        {
            _table = cloudTableClient.GetTableReference(tableName);
            _auditSigner = auditSigner;
        }

        /// <summary>
        /// Creates if not exists.
        /// </summary>
        /// <param name="requestOptions">The request options.</param>
        /// <param name="operationContext">The operation context.</param>
        /// <param name="serializedIndexingPolicy">The serialized indexing policy.</param>
        /// <param name="throughput">The throughput.</param>
        /// <param name="defaultTimeToLive">The default time to live.</param>
        public void CreateIfNotExists(TableRequestOptions requestOptions = null, OperationContext operationContext = null, string serializedIndexingPolicy = null, int? throughput = null, int? defaultTimeToLive = null)
        {
             _table.CreateIfNotExists(requestOptions, operationContext, serializedIndexingPolicy, throughput, defaultTimeToLive);
        }

        /// <summary>
        /// Executes the operation asynchronous.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <returns></returns>
        protected virtual async Task<TTableEntity> ExecuteOperationAsync(TableOperation operation)
        {
            _auditSigner.Sign(operation);
            TableResult operationResult = await _table.ExecuteAsync(operation);
            return operationResult.Result as TTableEntity;
        }

        /// <summary>
        /// Executes the batch operation asynchronous.
        /// </summary>
        /// <param name="operations">The operations.</param>
        /// <returns></returns>
        protected virtual async Task<IEnumerable<TTableEntity>> ExecuteBatchOperationAsync(IEnumerable<TableOperation> operations)
        {
            List<TTableEntity> result = new List<TTableEntity>();

            foreach (IEnumerable<TableOperation> batchedEntities in operations.Batch(100))
            {
                TableBatchOperation batchOperation = new TableBatchOperation();
                batchedEntities.ToList().ForEach(x => batchOperation.Add(x));

                _auditSigner.Sign(batchOperation);

                TableBatchResult operationResult = await _table.ExecuteBatchAsync(batchOperation);
                result.AddRange(operationResult.Select(or => or.Result as TTableEntity));
            }

            return result;
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="System.NullReferenceException">entity</exception>
        public async Task<TTableEntity> InsertAsync(TTableEntity entity)
        {
            if (entity == null)
            {
                throw new NullReferenceException("entity");
            }

            var operation = TableOperation.Insert(entity);
            return await ExecuteOperationAsync(operation);
        }

        /// <summary>
        /// Batches the insert asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        public async Task<IEnumerable<TTableEntity>> BatchInsertAsync(IEnumerable<TTableEntity> entities)
        {
            if (entities == null || !entities.Any())
            {
                return null;
            }

            return await ExecuteBatchOperationAsync(entities.Select(entity => TableOperation.Insert(entity)));
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="System.NullReferenceException">entity</exception>
        public async Task<TTableEntity> UpdateAsync(TTableEntity entity)
        {
            if (entity == null)
            {
                throw new NullReferenceException("entity");
            }

            var operation = TableOperation.Replace(entity);
            return await ExecuteOperationAsync(operation);
        }

        /// <summary>
        /// Batches the update asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        public async Task<IEnumerable<TTableEntity>> BatchUpdateAsync(IEnumerable<TTableEntity> entities)
        {
            if (entities == null || !entities.Any())
            {
                return null;
            }

            return await ExecuteBatchOperationAsync(entities.Select(entity => TableOperation.Replace(entity)));
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="System.NullReferenceException">entity</exception>
        public async Task<TTableEntity> DeleteAsync(TTableEntity entity)
        {
            if (entity == null)
            {
                throw new NullReferenceException("entity");
            }

            var operation = TableOperation.Delete(entity);
            return await ExecuteOperationAsync(operation);
        }

        /// <summary>
        /// Queries the asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="take">The take.</param>
        /// <param name="select">The select.</param>
        /// <returns></returns>
        public async Task<IEnumerable<TTableEntity>> QueryAsync(string filter, int? take = null, IList<string> select = null)
        {
            var results = new List<TTableEntity>();

            TableContinuationToken continuationToken = default(TableContinuationToken);

            TableQuery<TTableEntity> query = new TableQuery<TTableEntity>().Where(filter);

            if(select != null && select.Any())
            {
                query = query.Select(select);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            do
            {
                TableQuerySegment<TTableEntity> segmentedResult = await _table.ExecuteQuerySegmentedAsync(query, continuationToken);

                results.AddRange(segmentedResult.Results);

                continuationToken = segmentedResult.ContinuationToken;
            }
            while(continuationToken != null);

            return results;
        }
    }
}
