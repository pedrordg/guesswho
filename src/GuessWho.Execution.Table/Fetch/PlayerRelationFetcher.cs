using GuessWho.Execution.Contracts;
using GuessWho.Infra.TableStorage.Contracts;
using GuessWho.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessWho.Execution.Table
{
    public class PlayerRelationFetcher : IPlayerRelationFetcher
    {
        private readonly ITable<PlayerRelationEntity> _table;

        public PlayerRelationFetcher(ITable<PlayerRelationEntity> table)
        {
            _table = table;
        }

        public async Task<IEnumerable<string>> GetPlayerFriendIds(string playerId)
        {
            string query = FilterBuilder.CreateForRowKey(playerId);

            IEnumerable<PlayerRelationEntity> relations = await _table.QueryAsync(query);

            return relations.Select(r => r.PartitionKey);
        }
    }
}
