using GuessWho.Models;
using Microsoft.Azure.Cosmos.Table;
using System.Collections.Generic;

namespace GuessWho.Execution.Table
{
    public static class FilterBuilder
    {
        public static string CreateForPartitionKeyAndRowKey(string partitionKey, string rowKey)
        {
            return TableQuery.CombineFilters(TableQuery.GenerateFilterCondition(nameof(TableEntity.PartitionKey), QueryComparisons.Equal, partitionKey), TableOperators.And, TableQuery.GenerateFilterCondition(nameof(IdolEntity.RowKey), QueryComparisons.Equal, rowKey));
        }

        public static string CreateForPartitionKey(string partitionKey)
        {
            return TableQuery.GenerateFilterCondition(nameof(TableEntity.PartitionKey), QueryComparisons.Equal, partitionKey);
        }

        public static string CreateForPartitionKeys(IEnumerable<string> partitionKeys)
        {
            var filter = string.Empty;

            foreach(var pk in partitionKeys)
            {
                var inFilter = CreateForPartitionKey(pk);
                filter = string.IsNullOrWhiteSpace(filter) ? inFilter : TableQuery.CombineFilters(filter, TableOperators.Or, inFilter);
            }

            return filter;
        }

        public static string CreateForRowKey(string rowKey)
        {
            return TableQuery.GenerateFilterCondition(nameof(TableEntity.RowKey), QueryComparisons.Equal, rowKey);
        }
    }
}
