using GuessWho.Execution.Dtos;
using System.Threading.Tasks;

namespace GuessWho.Execution.Contracts
{
    public interface IIdolFetcher
    {
        Task<IdolDto> GetIdolById(string themeId, string cardId);
    }
}
