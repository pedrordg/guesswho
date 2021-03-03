using GuessWho.Execution.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessWho.Execution.Contracts
{
    public interface IPlayerFetcher
    {
        Task<PlayerDto> GetById(string playerId);

        Task<IEnumerable<PlayerDto>> GetPlayerFriends(string playerId);
    }
}
