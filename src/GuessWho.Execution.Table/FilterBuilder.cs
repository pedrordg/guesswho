using GuessWho.Models;
using Microsoft.Azure.Cosmos.Table;

namespace GuessWho.Execution.Table
{
    public static class FilterBuilder
    {
        public static string CreateForPartitionKeyAndRowKey(string partitionKey, string rowKey)
        {
            return TableQuery.CombineFilters(TableQuery.GenerateFilterCondition(nameof(IdolEntity.PartitionKey), QueryComparisons.Equal, partitionKey), TableOperators.And, TableQuery.GenerateFilterCondition(nameof(IdolEntity.RowKey), QueryComparisons.Equal, rowKey));
        }

        public static string CreateForPartitionKey(string partitionKey)
        {
            return TableQuery.GenerateFilterCondition(nameof(IdolEntity.PartitionKey), QueryComparisons.Equal, partitionKey);
        }
    }
}
