using GuessWho.Execution.Dtos;
using System.Threading.Tasks;

namespace GuessWho.Execution.Contracts
{
    public interface IPlayerFetcher
    {
        Task<PlayerDto> GetById(string playerId);
    }
}
