using GuessWho.Execution.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessWho.Execution.Contracts
{
    public interface IIdolFetcher
    {
        Task<IdolDto> GetIdolById(string themeId, string cardId);

        Task<IEnumerable<IdolDto>> GetIdolsByDeck(string deckId);
    }
}
