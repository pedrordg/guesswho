using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessWho.Execution.Contracts
{
    public interface IPlayerRelationFetcher
    {
        Task<IEnumerable<string>> GetPlayerFriendIds(string playerId);
    }
}
