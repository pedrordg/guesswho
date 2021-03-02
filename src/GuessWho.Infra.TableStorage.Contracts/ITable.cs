// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITable.cs" company="Five Degrees">
// Copyright (c) Five Degrees. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Azure.Cosmos.Table;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessWho.Infra.TableStorage.Contracts
{
    public interface ITable<TTableEntity> where TTableEntity : class, ITableEntity
    {
        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<TTableEntity> InsertAsync(TTableEntity entity);

        /// <summary>
        /// Batches the insert asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        Task<IEnumerable<TTableEntity>> BatchInsertAsync(IEnumerable<TTableEntity> entities);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<TTableEntity> UpdateAsync(TTableEntity entity);

        /// <summary>
        /// Batches the update asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        Task<IEnumerable<TTableEntity>> BatchUpdateAsync(IEnumerable<TTableEntity> entities);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<TTableEntity> DeleteAsync(TTableEntity entity);

        /// <summary>
        /// Queries the asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="take">The take.</param>
        /// <param name="select">The select.</param>
        /// <returns></returns>
        Task<IEnumerable<TTableEntity>> QueryAsync(string filter, int? take = null, IList<string> select = null);
    }
}
